using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using GJ.UI;
using GJ.PDB;
using GJ.Para.Base;
using GJ.Para.Udc.BURNIN;
using GJ.Dev.PLC;
using GJ.Dev.Card;
using GJ.Dev.Mon;
using GJ.Dev.ERS;
using GJ.Mes;
   
namespace GJ.Para.BURNIN
{
    public partial class udcRun : udcMainBase
    {
        #region 构造函数
        public udcRun()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

            loadIniFile();

            loadSysPara();

            loadModelPara();

            loadRunPara();
        }
        #endregion

        #region 参数常量
        /// <summary>
        /// 线程扫描间隔
        /// </summary>
        private const int C_THREAD_DELAY = 20;
        /// <summary>
        /// 异常报警延时
        /// </summary>
        private const int C_ALARM_DELAY = 1000;
        /// <summary>
        /// 系统报警次数
        /// </summary>
        private int C_SYS_ALARM_COUNT = 0;
        /// <summary>
        /// 系统报警上限
        /// </summary>
        private int C_SYS_ALARM_LIMIT = 5;
        /// <summary>
        /// 设备启动扫描
        /// </summary>
        private bool C_SCAN_DEV_START = false;
        /// <summary>
        /// 设备扫描时间
        /// </summary>
        private int C_SCAN_DEV_TIME = 0;
        /// <summary>
        /// 行数
        /// </summary>
        private int C_ROW_MAX = 5;
        /// <summary>
        /// 列数
        /// </summary>
        private int C_COL_MAX = 24;
        /// <summary>
        /// 母治具数
        /// </summary>
        private int C_MONTHER_MAX = 120;
        /// <summary>
        /// 子治具数
        /// </summary>
        private const int C_UUT_MAX = 240;
        /// <summary>
        /// 治具槽位数
        /// </summary>
        private const int C_SLOT_MAX = 8;
        /// <summary>
        /// 读卡器数量
        /// </summary>
        public const int C_ID_MAX = 2;
        /// <summary>
        /// 控制板数量
        /// </summary>
        private int[] C_MON_MAX = new int[] { 40,40,40 }; 
        /// <summary>
        /// ERS数量
        /// </summary>
        private int[] C_ERS_MAX = new int[] {30,30};
        /// <summary>
        /// 监控报警
        /// </summary>
        private const int C_MON_ALARM_TIME = 3;
        #endregion

        #region 参数路径
        private string iniFile = "sysLog\\" + CGlobal.CFlow.flowGUID + ".ini";
        private string sysFile = "sysLog\\" + CGlobal.CFlow.flowGUID + ".xml";
        private string sysDB = "DBLog\\" + CGlobal.CFlow.flowGUID + ".accdb";
        private string plcDB = "PlcLog\\PLC_" + CGlobal.CFlow.flowGUID + ".accdb";
        private string plcTempDB = "PlcLog\\PLCTEMP_" + CGlobal.CFlow.flowGUID + ".accdb";
        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            try
            {
                runLog.Dock = DockStyle.Fill;
                panel2.Controls.Add(runLog, 0, 6);  

                //槽位编号
                for (int i = 0; i < C_COL_MAX; i++)
                {
                    Label labId = new Label();
                    labId.Dock = DockStyle.Fill;
                    labId.Text = (i + 1).ToString("D2");
                    labId.Font = new Font("宋体", 10);
                    labId.Margin = new Padding(0);
                    labId.TextAlign = ContentAlignment.MiddleCenter;   
                    panel1.Controls.Add(labId, 1 + i, 0);
                }
                //母治具
                udcMotherPanel = new TableLayoutPanel[C_MONTHER_MAX];
                for (int iRow = 0; iRow < C_ROW_MAX; iRow++)
                {
                    for (int iCol = 0; iCol < C_COL_MAX; iCol++)
                    {
                        int idNo = iRow * C_COL_MAX + iCol;
                        udcMotherPanel[idNo] = new TableLayoutPanel();
                        udcMotherPanel[idNo].GetType().GetProperty("DoubleBuffered",
                                              System.Reflection.BindingFlags.Instance |
                                              System.Reflection.BindingFlags.NonPublic)
                                              .SetValue(udcMotherPanel[idNo], true, null);
                        udcMotherPanel[idNo].RowCount = 2;
                        for (int i = 0; i < udcMotherPanel[idNo].RowCount; i++)
                            udcMotherPanel[idNo].RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        udcMotherPanel[idNo].ColumnCount = 1;
                        for (int i = 0; i < udcMotherPanel[idNo].ColumnCount; i++)
                            udcMotherPanel[idNo].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                        udcMotherPanel[idNo].CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                        udcMotherPanel[idNo].Dock = DockStyle.Fill;
                        udcMotherPanel[idNo].Margin = new Padding(1);
                        panel1.Controls.Add(udcMotherPanel[idNo], iCol + 1, iRow + 1);
                    }
                }
                //子治具
                udcUUT = new udcUUTSlot[C_UUT_MAX];
                int uutNo = 0;
                for (int i = 0; i < C_MONTHER_MAX; i++)
                {
                    for (int row = 0; row < 2; row++)
                    {
                        udcUUT[uutNo] = new udcUUTSlot(uutNo);
                        udcUUT[uutNo].Dock = DockStyle.Fill;
                        udcUUT[uutNo].Margin = new Padding(2, 1, 2, 1);
                        udcUUT[uutNo].menuClick.OnEvent += new COnEvent<udcUUTSlot.CSetMenuArgs>.OnEventHandler(OnMenuClick);
                        udcMotherPanel[i].Controls.Add(udcUUT[uutNo], 0, row);
                        uutNo++;
                    }
                }
                //机种规格
                labVName = new Label[4];
                labISet = new Label[4];
                labVSpec = new Label[4];
                labISpec = new Label[4];
                for (int i = 0; i < 4; i++)
                {
                    labVName[i] = new Label();
                    labVName[i].Dock = DockStyle.Fill;
                    labVName[i].TextAlign = ContentAlignment.MiddleCenter;
                    labVName[i].Margin = new Padding(0);
                    labVName[i].Visible = false;
                    panel4.Controls.Add(labVName[i], 0, 1 + i);

                    labISet[i] = new Label();
                    labISet[i].Dock = DockStyle.Fill;
                    labISet[i].TextAlign = ContentAlignment.MiddleCenter;
                    labISet[i].Margin = new Padding(0);
                    labISet[i].Visible = false;
                    panel4.Controls.Add(labISet[i], 1, 1 + i);

                    labVSpec[i] = new Label();
                    labVSpec[i].Dock = DockStyle.Fill;
                    labVSpec[i].TextAlign = ContentAlignment.MiddleCenter;
                    labVSpec[i].Margin = new Padding(0);
                    labVSpec[i].Visible = false;
                    panel4.Controls.Add(labVSpec[i], 2, 1 + i);

                    labISpec[i] = new Label();
                    labISpec[i].Dock = DockStyle.Fill;
                    labISpec[i].TextAlign = ContentAlignment.MiddleCenter;
                    labISpec[i].Margin = new Padding(0);
                    labISpec[i].Visible = false;
                    panel4.Controls.Add(labISpec[i], 3, 1 + i);
                }
                
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }         
        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {

            splitContainer1.Panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(splitContainer1.Panel1, true, null);
            splitContainer1.Panel2.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(splitContainer1.Panel2, true, null);

            panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel1, true, null);
            panel2.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel2, true, null);
            panel3.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel3, true, null);
            panel4.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel4, true, null);
            panel5.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel5, true, null);
            panel6.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel6, true, null);
        }
        /// <summary>
        /// 加载Ini文件
        /// </summary>
        private void loadIniFile()
        {
            try
            {
                string stemp = string.Empty;
                stemp = CIniFile.ReadFromIni("Parameter", "InBIFixNo", iniFile);
                if (stemp != "")
                    runFix.InBIFixNo = System.Convert.ToInt32(stemp);
                stemp = CIniFile.ReadFromIni("Parameter", "OutBIFixNo", iniFile);
                if (stemp != "")
                    runFix.OutBIFixNo = System.Convert.ToInt32(stemp);

                stemp = CIniFile.ReadFromIni("Parameter", "BITTNum", iniFile);
                if (stemp != "")
                    runFix.BITTNum = System.Convert.ToInt32(stemp);

                stemp = CIniFile.ReadFromIni("Parameter", "BIPASSNum", iniFile);
                if (stemp != "")
                    runFix.BIPASSNum = System.Convert.ToInt32(stemp);
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }
        /// <summary>
        /// 加载系统参数
        /// </summary>
        private void loadSysPara()
        {
            try
            {
                CSysSet<CSysPara>.load(sysFile, ref CSysPara.mVal);
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }     
        }
        private delegate void loadModelParaHanlder();
        /// <summary>
        /// 加载机种参数
        /// </summary>
        private void loadModelPara()
        {
            if(this.InvokeRequired)
                this.Invoke(new loadModelParaHanlder(loadModelPara));
            else
            {
                try
                {
                    string modelPath = CIniFile.ReadFromIni("Parameter", "ModelPath", iniFile);
                    if (!File.Exists(modelPath))
                        return;
                    CModelSet<CModelPara>.load(modelPath, ref runModel);
                    labModel.Text = runModel.model;
                    labACV.Text = runModel.ACV.ToString()+"V";
                    labBITime.Text = runModel.BITime.ToString("0.0") + "H";
                    labTSet.Text = runModel.TSet.ToString() + "℃ [" + (runModel.TSet - runModel.TLP).ToString() +
                                   "℃--" + (runModel.TSet + runModel.THP).ToString() + "℃]";
                    for (int i = 0; i < 4; i++)
                    {
                        labVName[i].Visible = false;
                        labISet[i].Visible = false;
                        labVSpec[i].Visible = false;
                        labISpec[i].Visible = false;
                    }
                    for (int i = 0; i < runModel.DCVList.Count; i++)
                    {
                        labVName[i].Text = runModel.DCVList[i].Vname;
                        labVName[i].Visible = true;

                        labISet[i].Text = runModel.DCVList[i].ISet.ToString() + "A";
                        labISet[i].Visible = true;

                        labVSpec[i].Text = runModel.DCVList[i].Vmin.ToString() + "--" + runModel.DCVList[i].Vmax.ToString();
                        labVSpec[i].Visible = true;

                        labISpec[i].Text = runModel.DCVList[i].Imin.ToString() + "--" + runModel.DCVList[i].Imax.ToString();
                        labISpec[i].Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                }     
            }           
        }     
        /// <summary>
        /// 加载测试参数
        /// </summary>
        private void loadRunPara()
        {
            try
            {
                CGJMES.mesDB = CSysPara.mVal.mySqlIp; 

                for (int i = 0; i < C_UUT_MAX; i++)
                    runUUT.Add(new CUUT());
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);
                string er = string.Empty;
                string sqlCmd = string.Empty;                
                DataSet ds = new DataSet();
                sqlCmd = "select * from RUN_BASE order by UUTNO";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return;
                if (ds.Tables[0].Rows.Count == 0)
                    return;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    runUUT[i].wBase.uutNo = System.Convert.ToInt32(ds.Tables[0].Rows[i]["UUTNO"].ToString());
                    runUUT[i].wBase.fixNo = System.Convert.ToInt32(ds.Tables[0].Rows[i]["FixNo"].ToString());
                    runUUT[i].wBase.subNo = System.Convert.ToInt32(ds.Tables[0].Rows[i]["subNo"].ToString());
                    runUUT[i].wBase.iRow = System.Convert.ToInt32(ds.Tables[0].Rows[i]["iRow"].ToString());
                    runUUT[i].wBase.iCol = System.Convert.ToInt32(ds.Tables[0].Rows[i]["iCol"].ToString());
                    runUUT[i].wBase.localName = ds.Tables[0].Rows[i]["LocalName"].ToString();
                    runUUT[i].wBase.ctrlCom = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrliCOM"].ToString());
                    runUUT[i].wBase.ctrlAddr = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlAddr"].ToString());
                    runUUT[i].wBase.ersCom = System.Convert.ToInt32(ds.Tables[0].Rows[i]["ERSCom"].ToString());
                    runUUT[i].wBase.ersAddr = System.Convert.ToInt32(ds.Tables[0].Rows[i]["ERSAddr"].ToString());
                    runUUT[i].wBase.ersCH = System.Convert.ToInt32(ds.Tables[0].Rows[i]["ERSCH"].ToString()); 
                }
                ds = new DataSet();
                sqlCmd = "select * from RUN_PARA order by UUTNO";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return;
                if (ds.Tables[0].Rows.Count == 0)
                    return;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    runUUT[i].wPara.doRun =(EDoRun)System.Convert.ToInt32(ds.Tables[0].Rows[i]["doRun"].ToString());
                    runUUT[i].wPara.isNull = System.Convert.ToInt32(ds.Tables[0].Rows[i]["IsNull"].ToString());
                    runUUT[i].wPara.modelName = ds.Tables[0].Rows[i]["ModelName"].ToString();
                    runUUT[i].wPara.idCard = ds.Tables[0].Rows[i]["IDCard"].ToString();
                    runUUT[i].wPara.burnTime = System.Convert.ToInt32(ds.Tables[0].Rows[i]["BurnTime"].ToString());
                    runUUT[i].wPara.runTime = System.Convert.ToInt32(ds.Tables[0].Rows[i]["RunTime"].ToString());
                    runUUT[i].wPara.runFlag = System.Convert.ToInt32(ds.Tables[0].Rows[i]["RunFlag"].ToString());
                    runUUT[i].wPara.startTime = ds.Tables[0].Rows[i]["StartTime"].ToString();
                    runUUT[i].wPara.endTime = ds.Tables[0].Rows[i]["EndTime"].ToString();
                    runUUT[i].wPara.alarmErr = (EAlarmCode)System.Convert.ToInt32(ds.Tables[0].Rows[i]["AlarmErr"].ToString());
                    runUUT[i].wPara.alarmInfo  = ds.Tables[0].Rows[i]["AlarmInfo"].ToString();
                    runUUT[i].wPara.runQCStepNo = System.Convert.ToInt32(ds.Tables[0].Rows[i]["runStepNo"].ToString());
                    runUUT[i].wPara.usedNum = System.Convert.ToInt32(ds.Tables[0].Rows[i]["UsedNum"].ToString());
                    runUUT[i].wPara.saveDataTime = ds.Tables[0].Rows[i]["SaveDataTime"].ToString();
                    runUUT[i].wPara.saveFileName = ds.Tables[0].Rows[i]["SaveFileName"].ToString();
                    runUUT[i].wPara.savePathName = ds.Tables[0].Rows[i]["SavePathName"].ToString();
                    runUUT[i].wPara.ctrlUUTONLine = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlUUTONLine"].ToString());
                    runUUT[i].wPara.ctrlRunFlag = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlRunFlag"].ToString());
                    runUUT[i].wPara.ctrlStartFlag = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlStartFlag"].ToString());
                    runUUT[i].wPara.ctrlBIFinishFlag = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlBIFinishFlag"].ToString());
                    runUUT[i].wPara.ctrlRunTime = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlRunTime"].ToString());
                    runUUT[i].wPara.ctrlOnOff_YXDH = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlOnOff_YXDH"].ToString());
                    runUUT[i].wPara.ctrlOnOff_Cnt = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlOnOff_Cnt"].ToString());
                    runUUT[i].wPara.ctrlRunError = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlRunError"].ToString());
                    runUUT[i].wPara.ctrlRunOnOff = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlRunOnOff"].ToString());
                    runUUT[i].wPara.ctrlQCStepNo = System.Convert.ToInt32(ds.Tables[0].Rows[i]["CtrlRunStepNo"].ToString());
                    runUUT[i].wPara.QCTYPE = (EQCV)System.Convert.ToInt32(ds.Tables[0].Rows[i]["QCTYPE"].ToString());
                    runUUT[i].wPara.MTK_SPEC_OK = System.Convert.ToInt32(ds.Tables[0].Rows[i]["MTK_SPEC_OK"].ToString());                        
                    string OnOffTemp = ds.Tables[0].Rows[i]["wOnOff"].ToString();
                    if (OnOffTemp != "")
                    {
                        string[] OnOffList = OnOffTemp.Split(';');
                        if (OnOffList.Length >= runUUT[i].wOnOff.C_ONOFF_MAX)
                        {
                            for (int step = 0; step < runUUT[i].wOnOff.C_ONOFF_MAX; step++)
                            {
                                string[] OnOffStep = OnOffList[step].Split(',');
                                if (OnOffStep.Length >= 4)
                                {
                                    runUUT[i].wOnOff.ACV[step] = System.Convert.ToInt32(OnOffStep[0]);
                                    runUUT[i].wOnOff.OnOffTimes[step] = System.Convert.ToInt32(OnOffStep[1]);
                                    runUUT[i].wOnOff.OnTime[step] = System.Convert.ToInt32(OnOffStep[2]);
                                    runUUT[i].wOnOff.OffTime[step] = System.Convert.ToInt32(OnOffStep[3]);
                                }
                            }                        
                        }                       
                    }
                }
                 ds = new DataSet();
                 sqlCmd = "select * from RUN_DATA order by LEDNO";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return;
                if (ds.Tables[0].Rows.Count != C_UUT_MAX*C_SLOT_MAX)
                    return;
                for (int z = 0; z < C_UUT_MAX; z++)
                {
                    for (int k = 0; k < C_SLOT_MAX; k++)
                    {
                        int i = z * C_SLOT_MAX + k;
                        runUUT[z].wLed[k].serialNo = ds.Tables[0].Rows[i]["SerialNo"].ToString();
                        for (int CH = 0; CH < 4; CH++)
                        {
                            runUUT[z].wLed[k].vName[CH] = ds.Tables[0].Rows[i]["VName"+CH.ToString()].ToString();
                            runUUT[z].wLed[k].vMin[CH] = System.Convert.ToDouble(ds.Tables[0].Rows[i]["Vmin" + CH.ToString()].ToString());
                            runUUT[z].wLed[k].vMax[CH] = System.Convert.ToDouble(ds.Tables[0].Rows[i]["Vmax" + CH.ToString()].ToString());
                            runUUT[z].wLed[k].iSet[CH] = System.Convert.ToDouble(ds.Tables[0].Rows[i]["ISET" + CH.ToString()].ToString());
                            runUUT[z].wLed[k].iMin[CH] = System.Convert.ToDouble(ds.Tables[0].Rows[i]["IMin" + CH.ToString()].ToString());
                            runUUT[z].wLed[k].iMax[CH] = System.Convert.ToDouble(ds.Tables[0].Rows[i]["IMax" + CH.ToString()].ToString());
                            runUUT[z].wLed[k].QCV[CH] = System.Convert.ToInt32(ds.Tables[0].Rows[i]["QCV" + CH.ToString()].ToString()); 
                        }
                        
                        runUUT[z].wLed[k].vinFlag = System.Convert.ToInt32(ds.Tables[0].Rows[i]["VinFlag"].ToString());
                        runUUT[z].wLed[k].unitV = System.Convert.ToDouble(ds.Tables[0].Rows[i]["UnitV"].ToString());
                        runUUT[z].wLed[k].unitA = System.Convert.ToDouble(ds.Tables[0].Rows[i]["UnitA"].ToString());
                        runUUT[z].wLed[k].passResult = System.Convert.ToInt32(ds.Tables[0].Rows[i]["passResult"].ToString());
                        runUUT[z].wLed[k].failResult = System.Convert.ToInt32(ds.Tables[0].Rows[i]["failResult"].ToString());
                        runUUT[z].wLed[k].failEnd = System.Convert.ToInt32(ds.Tables[0].Rows[i]["FailEnd"].ToString());
                        runUUT[z].wLed[k].failTime = ds.Tables[0].Rows[i]["FailTime"].ToString();
                        runUUT[z].wLed[k].failInfo = ds.Tables[0].Rows[i]["FailStr"].ToString();
                        runUUT[z].wLed[k].vBack = System.Convert.ToDouble(ds.Tables[0].Rows[i]["UnitV"].ToString());
                        runUUT[z].wLed[k].iBack = System.Convert.ToDouble(ds.Tables[0].Rows[i]["UnitA"].ToString());
                        if (runUUT[z].wLed[k].failResult == 1)
                        {
                            runUUT[z].wLed[k].vFailNum = CSysPara.mVal.failTimes+1;
                            runUUT[z].wLed[k].iFailNum = CSysPara.mVal.failTimes+1; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);    
            }          
        }
        #endregion

        #region 状态定义
        /// <summary>
        /// PLC IO输入
        /// </summary>
        private enum EPLCINP
        {
            PLC系统运行,
            PLC异常报警,
            老化出口处平台空闲,
            老化入口上层请求读取ID卡,
            老化入口下层请求读取ID卡,
            老化入口平台准备OK,
            老化入口处上层治具到位信号,
            老化入口处下层治具到位信号,
            机械手状态,
            TC1_1,
            TC1_2,
            TC2_1,
            TC2_2,
            TC3_1,
            TC3_2,
            TC4_1,
            TC4_2,
            TC5_1,
            TC5_2,
            TC6_1,
            TC6_2,
            TC7_1,
            TC7_2,
            老化入口位下层ID卡信息0,
            老化入口位下层ID卡信息1,
            老化入口位下层ID卡信息2,
            老化入口位下层ID卡信息3,
            老化入口位下层ID卡信息4,
            老化入口位下层ID卡信息5,
            老化入口位下层ID卡信息6,
            老化入口位下层ID卡信息7,
            老化入口位下层ID卡信息8,
            老化入口位下层ID卡信息9,
            老化入口位上层ID卡信息0,
            老化入口位上层ID卡信息1,
            老化入口位上层ID卡信息2,
            老化入口位上层ID卡信息3,
            老化入口位上层ID卡信息4,
            老化入口位上层ID卡信息5,
            老化入口位上层ID卡信息6,
            老化入口位上层ID卡信息7,
            老化入口位上层ID卡信息8,
            老化入口位上层ID卡信息9
        }
        /// <summary>
        /// PLC IO输出
        /// </summary>
        private enum EPLCOUT
        {
            老化入口平台上层_ID读取成功,
            老化入口平台下层_ID读取成功,
            老化入口平台上层_ID读取失败,
            老化入口平台下层_ID读取失败,
            上层取治具层数,
            下层取治具层数,
            上层取治具列数,
            下层取治具列数,
            上层放治具层数,
            下层放治具层数,
            上层放治具列数,
            下层放治具列数,
            发送层列完成,
            温度设定值,
            下限偏差,
            上限偏差,
            超温上限偏差,
            启动排风温度,
            停止排风温度
        }
        /// <summary>
        /// 返回PLC输入名称
        /// </summary>
        /// <param name="inpIo"></param>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private string InpPLC(EPLCINP inpIo, int idNo)
        {
            return ((EPLCINP)((int)inpIo + idNo)).ToString();
        }
        /// <summary>
        /// 返回PLC输出名称
        /// </summary>
        /// <param name="outIo"></param>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private string OutPLC(EPLCOUT outIo, int idNo)
        {
            return ((EPLCOUT)((int)outIo + idNo)).ToString();
        }
        #endregion

        #region 面板操作

        #region 面板控件
        private TableLayoutPanel[] udcMotherPanel;
        private udcUUTSlot[] udcUUT = null;
        private udcRunLog runLog= new udcRunLog();
        private Label[] labVName = null;
        private Label[] labISet = null;
        private Label[] labVSpec = null;
        private Label[] labISpec = null;
        #endregion

        #region 面板回调函数
        private void udcRun_Load(object sender, EventArgs e)
        {
            labInNum.Text = runFix.InBIFixNo.ToString();
            labOutNum.Text = runFix.OutBIFixNo.ToString();
            updateUUTBIUI(runFix.BITTNum, runFix.BIPASSNum);    
            for (int i = 0; i < C_UUT_MAX; i++)
               
                udcUUT[i].SetRunStatus(runUUT[i]);
        }
        private void btnModel_Click(object sender, EventArgs e)
        {
            string modelPath = string.Empty;
            string fileDirectry = string.Empty;
            fileDirectry = CSysPara.mVal.modelPath;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = fileDirectry;
            dlg.Filter = "BI files (*.bi)|*.bi";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            modelPath = dlg.FileName;
            CModelSet<CModelPara>.load(modelPath, ref runModel);
            CIniFile.WriteToIni("Parameter", "ModelPath",modelPath, iniFile);
            loadModelPara();
            runFix.tempRequest = true;  
        }
        private void chkIsNull_CheckedChanged(object sender, EventArgs e)
        {
            runFix.chkOutIsNull = chkIsNull.Checked; 
        }
        private void btnClrNum_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清除统计数量?", "清除统计", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                runFix.InBIFixNo = 0;
                runFix.OutBIFixNo = 0;
                updateInBIFixNum(0);
                updateOutBIFixNum(0);
            }
        }
        #endregion        

        #region 面板按钮事件
        private bool BtnF10 = false;
        public override void OnFrmMainBtnClick(object sender, CFormBtnArgs e)
        {
            switch (e.btnName)
            {
                case CFormBtnArgs.EBtnName.启动:
                    if (!StartRun())
                        return;
                    C_RUNNING = true;
                    BtnF10 = true;
                    OnBtnTriger(EFormStatus.Auto);
                    break;
                case CFormBtnArgs.EBtnName.暂停:
                    if (!StartRun(false))
                        return;
                    C_RUNNING = false;
                    BtnF10 = false;
                    OnBtnTriger(EFormStatus.Idel);
                    break;
                case CFormBtnArgs.EBtnName.退出:
                    StartRun(false);
                    Application.Exit();
                    break;
                case CFormBtnArgs.EBtnName.F9:
                    if (BtnF10)
                        return;
                    if (!StartRun())
                        return;
                    C_RUNNING = true;
                    BtnF10 = true;
                    OnBtnTriger(EFormStatus.Auto);
                    break;
                case CFormBtnArgs.EBtnName.F10:
                    if (!StartRun(false))
                        return;
                    C_RUNNING = false;
                    BtnF10 = false;
                    OnBtnTriger(EFormStatus.Idel);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 面板按钮触发
        private void OnBtnTriger(EFormStatus status)
        {
            OnFrmMainStatus.OnEvented(new CFormRunArgs(status));
        }      
        #endregion

        #region 状态触发
        private void OnMenuClick(object sender, udcUUTSlot.CSetMenuArgs e)
        {
            int idNo = e.idNo;
            int icom = 0;
            int iAddr = 0;
            string er = string.Empty;
            int uutNo = 0;
            if (idNo % 2 == 0) //治具1
                uutNo = idNo;
            else               //治具2
                uutNo = idNo - 1;
            switch (e.menuInfo)
            {
                case udcUUTSlot.ESetMenu.显示信息:
                    udcUUTInfo uutInfo = new udcUUTInfo(runUUT[e.idNo]);
                    uutInfo.UIRefresh.OnEvent += new COnEvent<udcUUTInfo.CUIRefreshArgs>.OnEventHandler(OnRefreshVoltInfo);  
                    uutInfo.Show(); 
                    break;
                case udcUUTSlot.ESetMenu.位置空闲:
                    for (int i = 0; i < 2; i++)
                    {
                        runUUT[uutNo + i].wPara.doRun = EDoRun.位置空闲;
                        runUUT[uutNo + i].wPara.alarmErr = EAlarmCode.正常;
                        runUUT[uutNo + i].wPara.alarmTimes = 0;
                        runUUT[uutNo + i].wPara.alarmInfo = string.Empty;
                        runUUT[uutNo + i].wPara.isNull = 0; 
                        udcUUT[uutNo + i].SetRunStatus(runUUT[uutNo+i]);
                        updateUUTDBStatus(runUUT[uutNo + i], ref er);
                    }                    
                    break;
                case udcUUTSlot.ESetMenu.禁用位置:
                    for (int i = 0; i < 2; i++)
                    {
                        runUUT[uutNo + i].wPara.doRun = EDoRun.位置禁用;
                        runUUT[uutNo + i].wPara.alarmErr = EAlarmCode.正常;
                        runUUT[uutNo + i].wPara.alarmTimes = 0;
                        runUUT[uutNo + i].wPara.alarmInfo = string.Empty;
                        udcUUT[uutNo + i].SetRunStatus(runUUT[uutNo+i]);
                        updateUUTDBStatus(runUUT[uutNo + i], ref er);
                    }               
                    break;
                case udcUUTSlot.ESetMenu.启动老化:                  
                    for (int i = 0; i < 2; i++)
                    {
                        if (runUUT[uutNo + i].wPara.ctrlUUTONLine == 0)
                        {
                            MessageBox.Show(runUUT[uutNo + i].wBase.localName + ":检测不到到位信号,不能启动老化", "手动模式",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        runUUT[uutNo + i].wPara.doRun = EDoRun.进机完毕;
                        runUUT[uutNo + i].wPara.alarmErr = EAlarmCode.正常;
                        runUUT[uutNo + i].wPara.alarmTimes = 0;
                        runUUT[uutNo + i].wPara.alarmInfo = string.Empty;
                        udcUUT[uutNo + i].SetRunStatus(runUUT[uutNo+i]);
                        for (int slot = 0; slot < C_SLOT_MAX; slot++)
                        {
                            runUUT[uutNo + i].wLed[slot].failResult = 0;
                            runUUT[uutNo + i].wLed[slot].passResult = 0;
                            runUUT[uutNo + i].wLed[slot].failEnd = 0;
                            runUUT[uutNo + i].wLed[slot].vFailNum = 0;
                            runUUT[uutNo + i].wLed[slot].iFailNum = 0; 
                            runUUT[uutNo + i].wLed[slot].unitV = runUUT[uutNo + i].wLed[slot].vMin[0] + 0.1;
                            runUUT[uutNo + i].wLed[slot].unitA = runUUT[uutNo + i].wLed[slot].iSet[0];
                            runUUT[uutNo + i].wLed[slot].vBack = runUUT[uutNo + i].wLed[slot].unitV;
                            runUUT[uutNo + i].wLed[slot].iBack = runUUT[uutNo + i].wLed[slot].unitA;
                        }
                        updateUUTDBStatus(runUUT[uutNo + i], ref er);
                    }                                        
                    break;
                case udcUUTSlot.ESetMenu.停止老化:
                    if (idNo % 2 == 0) //治具1
                        uutNo=idNo;
                    else               //治具2
                        uutNo=idNo-1;
                    icom = runUUT[uutNo].wBase.ctrlCom;
                    iAddr = runUUT[uutNo].wBase.ctrlAddr;
                    if (ThreadMon == null)
                    {
                        MessageBox.Show("强制老化结束需在监控模式下", "手动模式", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                                              
                        return;
                    }
                    if (ThreadMon[icom] == null)
                    {
                        MessageBox.Show("强制老化结束需在监控模式下", "手动模式", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //for (int i = 0; i < 2; i++)
                    //{
                    //    if (runUUT[uutNo + i].wPara.ctrlUUTONLine == 0)
                    //    {
                    //        MessageBox.Show(runUUT[uutNo + i].wBase.localName + ":检测不到到位信号,不能停止老化", "手动模式",
                    //                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //        return;
                    //    }
                    //}
                    //强制老化结束
                    ThreadMon[icom].setStop(iAddr, ref er);
                    //治具1
                    if (runUUT[uutNo].wPara.isNull == 0 || runUUT[uutNo].wPara.isNull == 2)
                    {
                        runUUT[uutNo].wPara.doRun = EDoRun.正在老化;
                        runUUT[uutNo].wPara.alarmErr = EAlarmCode.正常;
                        runUUT[uutNo].wPara.alarmTimes = 0;
                        runUUT[uutNo].wPara.alarmInfo = string.Empty;
                        udcUUT[uutNo].SetRunStatus(runUUT[uutNo]);
                        updateUUTDBStatus(runUUT[uutNo], ref er);
                    }
                    //治具2
                    if (runUUT[uutNo+1].wPara.isNull == 0 || runUUT[uutNo+1].wPara.isNull == 1)
                    {
                        runUUT[uutNo + 1].wPara.doRun = EDoRun.正在老化;
                        runUUT[uutNo + 1].wPara.alarmErr = EAlarmCode.正常;
                        runUUT[uutNo + 1].wPara.alarmTimes = 0;
                        runUUT[uutNo + 1].wPara.alarmInfo = string.Empty;
                        udcUUT[uutNo + 1].SetRunStatus(runUUT[uutNo + 1]);
                        updateUUTDBStatus(runUUT[uutNo + 1], ref er);
                    }
                    break;
                case udcUUTSlot.ESetMenu.解除报警:
                    if (runUUT[uutNo].wPara.alarmErr == EAlarmCode.针盘使用寿命已到)
                    {
                        udcClrDealTime dlg = new udcClrDealTime();
                        if (dlg.ShowDialog() != DialogResult.OK)
                            return;
                        runUUT[uutNo].wPara.usedNum = 0;
                        runUUT[uutNo+1].wPara.usedNum = 0;
                        clearUUTUsedNum(uutNo,ref er);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        runUUT[uutNo + i].wPara.alarmErr = EAlarmCode.正常;
                        runUUT[uutNo + i].wPara.alarmTimes = 0;
                        runUUT[uutNo + i].wPara.alarmInfo = string.Empty;
                        udcUUT[uutNo + i].SetRunStatus(runUUT[uutNo + i]);
                        updateUUTDBStatus(runUUT[uutNo + i], ref er);
                    }
                    break;
                case udcUUTSlot.ESetMenu.清除不良:
                    for (int i = 0; i < 2; i++)
                    {
                        if (runUUT[uutNo + i].wPara.doRun == EDoRun.正在老化 || runUUT[uutNo + i].wPara.doRun == EDoRun.老化结束)
                        {
                            for (int slot = 0; slot < C_SLOT_MAX; slot++)
                            {
                                runUUT[uutNo + i].wLed[slot].passResult = 0;
                                runUUT[uutNo + i].wLed[slot].failResult = 0;
                                runUUT[uutNo + i].wLed[slot].vFailNum = 0; 
                                runUUT[uutNo + i].wLed[slot].iFailNum = 0;
                                runUUT[uutNo + i].wLed[slot].failEnd = 0; 
                            }
                            setUUTTestResult(runUUT[uutNo + i], ref er);
                            runUUT[uutNo + i].wPara.doRun = EDoRun.正在老化;
                            udcUUT[uutNo + i].SetRunStatus(runUUT[uutNo + i]);
                            runUUT[uutNo + i].wPara.MTK_SPEC_OK = 0;
                            runUUT[uutNo + i].wPara.MTK_RUNNING = false;
                            runUUT[uutNo + i].wPara.MTK_STEP_NO = 0;  
                        }
                    }
                    break;
                case udcUUTSlot.ESetMenu.复位老化:
                    if (idNo % 2 == 0) //治具1
                        uutNo=idNo;
                    else               //治具2
                        uutNo=idNo-1;
                    icom = runUUT[uutNo].wBase.ctrlCom;
                    iAddr = runUUT[uutNo].wBase.ctrlAddr;
                     if (ThreadMon == null)
                    {
                        MessageBox.Show("复位老化需在监控模式下", "手动模式", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                                              
                        return;
                    }
                    if (ThreadMon[icom] == null)
                    {
                        MessageBox.Show("复位老化需在监控模式下", "手动模式", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        if (runUUT[uutNo + i].wPara.ctrlUUTONLine == 0)
                        {
                            MessageBox.Show(runUUT[uutNo + i].wBase.localName + ":检测不到到位信号,不能复位老化", "手动模式",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        runUUT[uutNo + i].wPara.MTK_RUNNING = false;
                        runUUT[uutNo + i].wPara.MTK_SPEC_OK = 0;
                        runUUT[uutNo + i].wPara.MTK_STEP_NO = 0; 
                        runUUT[uutNo + i].wPara.alarmErr = EAlarmCode.正常;
                        runUUT[uutNo + i].wPara.alarmTimes = 0;
                        runUUT[uutNo + i].wPara.alarmInfo = string.Empty;
                        udcUUT[uutNo + i].SetRunStatus(runUUT[uutNo+i]);
                        for (int slot = 0; slot < C_SLOT_MAX; slot++)
                        {
                            runUUT[uutNo + i].wLed[slot].failResult = 0;
                            runUUT[uutNo + i].wLed[slot].passResult = 0;
                            runUUT[uutNo + i].wLed[slot].failEnd = 0;
                            runUUT[uutNo + i].wLed[slot].vFailNum = 0;
                            runUUT[uutNo + i].wLed[slot].iFailNum = 0; 
                            runUUT[uutNo + i].wLed[slot].unitV = runUUT[uutNo + i].wLed[slot].vMin[0] + 0.1;
                            runUUT[uutNo + i].wLed[slot].unitA = runUUT[uutNo + i].wLed[slot].iSet[0];
                            runUUT[uutNo + i].wLed[slot].vBack = runUUT[uutNo + i].wLed[slot].unitV;
                            runUUT[uutNo + i].wLed[slot].iBack = runUUT[uutNo + i].wLed[slot].unitA;
                        }
                        updateUUTDBStatus(runUUT[uutNo + i], ref er);
                    }
                    ThreadMon[icom].setContinue(iAddr, ref er,0);
                    break;
                case udcUUTSlot.ESetMenu.指定位置老化:
                    if (idNo % 2 == 0) //治具1
                        uutNo = idNo;
                    else               //治具2
                        uutNo = idNo - 1;
                    icom = runUUT[uutNo].wBase.ctrlCom;
                    iAddr = runUUT[uutNo].wBase.ctrlAddr;
                    if (ThreadMon == null)
                    {
                        MessageBox.Show("强制老化结束需在监控模式下", "手动模式", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (ThreadMon[icom] == null)
                    {
                        MessageBox.Show("强制老化结束需在监控模式下", "手动模式", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //for (int i = 0; i < 2; i++)
                    //{
                    //    if (runUUT[uutNo + i].wPara.ctrlUUTONLine == 0)
                    //    {
                    //        MessageBox.Show(runUUT[uutNo + i].wBase.localName + ":检测不到到位信号,不能停止老化", "手动模式",
                    //                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //        return;
                    //    }
                    //}
                    udcSelLocal seldlg = new udcSelLocal();
                    if (seldlg.ShowDialog() != DialogResult.OK)
                        return;

                    if (udcSelLocal.row < 1 || udcSelLocal.row > C_ROW_MAX)
                        return;
                    if (udcSelLocal.col < 1 || udcSelLocal.col > C_COL_MAX)
                        return;
                    int InUUTNo = (udcSelLocal.row - 1) * C_COL_MAX + (udcSelLocal.col - 1)*2;

                    setFixOutIntoBI(uutNo, InUUTNo, ref er);

                    for (int i = 0; i < 2; i++)
                    {
                        runUUT[InUUTNo + i].wPara.idCard = runUUT[uutNo + i].wPara.idCard;
                        runUUT[InUUTNo + i].wPara.isNull = runUUT[uutNo + i].wPara.isNull;
                        runUUT[InUUTNo + i].wPara.modelName = runUUT[uutNo + i].wPara.modelName;
                        runUUT[InUUTNo + i].wPara.burnTime = runUUT[uutNo + i].wPara.burnTime;
                        runUUT[InUUTNo + i].wPara.QCTYPE = runUUT[uutNo + i].wPara.QCTYPE;
                        runUUT[InUUTNo + i].wPara.runFlag = runUUT[uutNo + i].wPara.runFlag;
                        runUUT[InUUTNo + i].wPara.runQCStepNo = 0;
                        runUUT[InUUTNo + i].wPara.runTime = 0;
                        runUUT[InUUTNo + i].wOnOff.C_ONOFF_MAX = runUUT[uutNo + i].wOnOff.C_ONOFF_MAX;
                        for (int z = 0; z < runUUT[uutNo + i].wOnOff.ACV.Count; z++)
                        {
                            runUUT[InUUTNo + i].wOnOff.ACV[z] = runUUT[uutNo + i].wOnOff.ACV[z];
                            runUUT[InUUTNo + i].wOnOff.OnOffTimes[z] = runUUT[uutNo + i].wOnOff.OnOffTimes[z];
                            runUUT[InUUTNo + i].wOnOff.OffTime[z] = runUUT[uutNo + i].wOnOff.OffTime[z];
                            runUUT[InUUTNo + i].wOnOff.OnTime[z] = runUUT[uutNo + i].wOnOff.OnTime[z];
                            runUUT[InUUTNo + i].wOnOff.QCV[z] = runUUT[uutNo + i].wOnOff.QCV[z];
                        }
                        for (int z = 0; z < runUUT[uutNo + i].wLed.Count; z++)
                        {
                            runUUT[InUUTNo + i].wLed[z].serialNo = runUUT[uutNo + i].wLed[z].serialNo;
                            for (int row = 0; row < runUUT[InUUTNo + i].wLed[z].iSet.Count; row++)
                            {
                                runUUT[InUUTNo + i].wLed[z].iSet[row] = runUUT[uutNo + i].wLed[z].iSet[row];
                                runUUT[InUUTNo + i].wLed[z].iMax[row] = runUUT[uutNo + i].wLed[z].iMax[row];
                                runUUT[InUUTNo + i].wLed[z].iMin[row] = runUUT[uutNo + i].wLed[z].iMin[row];
                                runUUT[InUUTNo + i].wLed[z].vName[row] = runUUT[uutNo + i].wLed[z].vName[row];
                                runUUT[InUUTNo + i].wLed[z].vMin[row] = runUUT[uutNo + i].wLed[z].vMin[row];
                                runUUT[InUUTNo + i].wLed[z].vMax[row] = runUUT[uutNo + i].wLed[z].vMax[row];
                            }
                        }
                    }
                    updateMontherInfo(InUUTNo, ref er);
                    setFixOnOff(InUUTNo, ref er);
                    runUUT[uutNo].wPara.doRun = EDoRun.正在出机;
                    runUUT[uutNo + 1].wPara.doRun = EDoRun.正在出机;

                    runUUT[InUUTNo].wPara.doRun = EDoRun.正在进机;
                    runUUT[InUUTNo + 1].wPara.doRun = EDoRun.正在进机;

                    //强制老化结束
                    ThreadMon[icom].setStop(iAddr, ref er);
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region 请求更新数据
        private void OnRefreshVoltInfo(object sender, udcUUTInfo.CUIRefreshArgs e)
        {
            e.runUUT = runUUT[e.idNo]; 
        }
        #endregion

        #endregion

        #region 测试参数
        /// <summary>
        /// 系统运行中
        /// </summary>
        public static bool C_RUNNING = false;        
       
        #region 动作参数
        private enum EFixRun
        { 
          空闲,
          就绪,
          进站,
          报警
        }
        public enum EHandStat
        { 
          待机,
          忙碌,
          空闲,
          异常,
          进机,
          出机
        }
        /// <summary>
        /// 老化入口治具状态
        /// </summary>
        private class CInFix
        {
            public EFixRun doRun = EFixRun.空闲;
            public string modelName = string.Empty;
            public string idCard = string.Empty;            
            public List<string> serialNo = new List<string>();
            public int IsFixNull = 0;
            public int iReady = 0;
            public CInFix()
            {
                for (int i = 0; i < C_SLOT_MAX; i++)
                    serialNo.Add(""); 
            }
        }
        private class CFixRun
        {
            public bool IniStart = true;
            public string sysStat =string.Empty;
            public EFixRun inDoRun = EFixRun.空闲;
            /// <summary>
            /// 入口治具状态
            /// </summary>
            public List<CInFix> inFix = new List<CInFix>();
            /// <summary>
            /// 机械手状态
            /// </summary>
            public EHandStat handStat = EHandStat.空闲;
            /// <summary>
            /// 设置温度请求
            /// </summary>
            public bool tempRequest = true;
            /// <summary>
            /// 进机就绪
            /// </summary>
            public int rInReady = 0;           
            /// <summary>
            /// 出机就绪
            /// </summary>
            public int rOutReady = 0;
            /// <summary>
            /// 进出机动作
            /// </summary>
            public EHandStat InOutOP = EHandStat.空闲;
            /// <summary>
            /// 动作操作中
            /// </summary>
            public bool InOuting = false;
            /// <summary>
            /// 进出机时间
            /// </summary>
            public int InOutTime = 0;
            /// <summary>
            /// 进出机位置
            /// </summary>
            public int InOutPos = -1;
            /// <summary>
            /// 交替进出机
            /// </summary>
            public bool alternant = false;
            /// <summary>
            /// 平均温度值
            /// </summary>
            public double rTemp = 0;
            /// <summary>
            /// 温度读值
            /// </summary>
            public List<double> rTempPoint = new List<double>();
            /// <summary>
            /// 允许出空治具
            /// </summary>
            public bool chkOutIsNull = false;
            /// <summary>
            /// 进BI治具数量
            /// </summary>
            public int InBIFixNo = 0;
            /// <summary>
            /// 出BI治具数量
            /// </summary>
            public int OutBIFixNo = 0;
            /// <summary>
            /// 当前老化产品数
            /// </summary>
            public int BITTNum = 0;
            /// <summary>
            /// 当前老化良品数
            /// </summary>
            public int BIPASSNum = 0;
            public CFixRun()
            {
                for (int i = 0; i < 14; i++)                
                    rTempPoint.Add(0);
                for (int i = 0; i < 2; i++)
                    inFix.Add(new CInFix());                 
            }
        }
        #endregion

        #region 治具参数

        public enum EDoRun
        {
            异常报警=-2,
            位置禁用 = -1,
            位置空闲 = 0,
            正在进机 = 1,
            进机完毕 = 2,
            启动老化 = 3,
            老化自检 = 4,
            正在老化 = 5,
            老化结束 = 6,
            正在出机 = 7,
            空治具到位 = 8,
        }
        public enum EAlarmCode
        {
            正常,
            治具到位信号异常,
            气缸升到位信号异常,
            AC同步信号异常,
            治具1AC不通,
            治具2AC不通,
            治具12AC都不通,
            快充电压与Y输出异常,
            有负载回路不良,
            控制板通信异常,
            ERS通信异常,
            无治具有到位信号,
            有治具无到位信号,
            进机结束检测不到到位,
            出机结束到位信号存在,
            针盘使用寿命已到,
            位置禁用
        }
        public class CBase
        {
            public int uutNo;
            public int fixNo;
            public int subNo;
            public int iRow;
            public int iCol;
            public string localName;
            public int ctrlCom;
            public int ctrlAddr;
            public int ersCom;
            public int ersAddr;
            public int ersCH;
        }
        public class CPara
        {
            public EDoRun doRun = EDoRun.位置空闲;
            public int isNull = 0;
            public string idCard = string.Empty;
            public string modelName = string.Empty;
            public int burnTime = 0;
            public int runTime = 0;
            public string startTime = string.Empty;
            public string endTime = string.Empty;
            /// <summary>
            /// 1：治具1老化;2:治具2老化 3:治具1,2老化
            /// </summary>
            public int runFlag = 3;
            public int runQCStepNo = 0;
            public int runQCWaitTimes = 0;
            public int alarmTimes = 0;
            public EAlarmCode alarmErr = EAlarmCode.正常;
            public string alarmInfo = string.Empty;
            public int usedNum = 0;
            public string saveDataTime = string.Empty;
            public string saveFileName = string.Empty;
            public string savePathName = string.Empty;

            public int ctrlUUTONLine = 0;
            public int ctrlRunOnOff = 0;
            public int ctrlRunFlag = 0;
            public int ctrlStartFlag = 0;
            public int ctrlBIFinishFlag = 0;
            public int ctrlRunTime = 0;
            public int ctrlOnOff_YXDH = 0;
            public int ctrlOnOff_Cnt = 0;
            public int ctrlRunError = 0;
            public int ctrlQCStepNo = 0;

            public string waitInfo = string.Empty;
            public bool waitAlarm = false; 
            public string waitStartTime = string.Empty;
            public int waitTimes = 0;

            public EQCV QCTYPE = EQCV.QC2_0;
            /// <summary>
            /// MTK电压在范围内
            /// </summary>
            public int MTK_SPEC_OK = 0;
            /// <summary>
            /// MTK电压上升和下降过程中
            /// </summary>
            public bool MTK_RUNNING = false;
            /// <summary>
            /// 电压上升过程中:0->需降电压到+5V,1:上升电压 2:下降电压
            /// </summary>
            public int MTK_RAISED = 0;
            /// <summary>
            /// MTK电压上升/下降次数
            /// </summary>
            public int MTK_STEP_NO = 0;
            /// <summary>
            /// 中途AC断电
            /// </summary>
            public int MTK_SHUTDOWN = 0;
            /// <summary>
            /// 中途AC断电
            /// </summary>
            public bool ShutDownAC = false;
            /// <summary>
            /// QC2.0/QC3.0重启次数
            /// </summary>
            public int ResetQCVoltTimes =0;
        }
        public class CLED
        {
            public string serialNo = string.Empty;
            public int vinFlag = 0;
            public List<string> vName = new List<string>();
            public List<double> vMin = new List<double>();
            public List<double> vMax = new List<double>();
            public List<double> iSet = new List<double>();
            public List<double> iMin = new List<double>();
            public List<double> iMax = new List<double>();
            public List<int> QCV = new List<int>();  
            public int passResult = 0;
            public int failResult = 0;
            public double unitV = 0;
            public double unitA = 0;
            public int vFailNum = 0;
            public double vBack = 0;
            public int iFailNum = 0;
            public double iBack = 0;
            public int failEnd = 0;
            public string failTime = string.Empty;
            public string failInfo = string.Empty;
            public CLED()
            {
                for (int i = 0; i < 4; i++)
                {
                    vName.Add("");
                    vMin.Add(0);
                    vMax.Add(0);
                    iSet.Add(0);
                    iMin.Add(0);
                    iMax.Add(0);
                    QCV.Add(0);  
                }
            }
        }
        public class COnOff
        {
            public int C_ONOFF_MAX = 4;
            public List<int> ACV = new List<int>();
            public List<int> OnOffTimes = new List<int>(); //次数
            public List<int> OnTime = new List<int>();
            public List<int> OffTime = new List<int>();
            public List<int> QCV = new List<int>();
            public COnOff()
            {
                for (int i = 0; i < C_ONOFF_MAX; i++)
                {
                    ACV.Add(220);
                    OnOffTimes.Add(0);
                    OnTime.Add(0);
                    OffTime.Add(0);
                    QCV.Add(0); 
                } 
            }
        }
        public class CUUT
        {
            public CBase wBase = new CBase();
            public CPara wPara = new CPara();
            public COnOff wOnOff = new COnOff();
            public List<CLED> wLed = new List<CLED>();            
            public CUUT()
            {
                for (int i = 0; i < C_SLOT_MAX; i++)
                    wLed.Add(new CLED());
            }
        }

        #endregion

        #region 参数定义

        private CModelPara runModel = new CModelPara();
        private CFixRun runFix =new CFixRun();
        private List<CUUT> runUUT = new List<CUUT>();
        /// <summary>
        /// 数据库备份时间30Min
        /// </summary>
        private int g_DB_BAK_TIME = 1800;
        /// <summary>
        /// 数据库运行时间
        /// </summary>
        private string g_DB_BAK_STARTTIMES = string.Empty;
        #endregion

        #endregion

        #region 设备参数
        /// <summary>
        /// 线体PLC
        /// </summary>
        private CPLCCom devPLC = null;
        /// <summary>
        /// 温度PLC
        /// </summary>
        private CPLCCom devPLCTemp = null;
        /// <summary>
        /// 读卡器
        /// </summary>
        private CCardCom devIDCard = null;
        /// <summary>
        /// 监控板
        /// </summary>
        private CGJMonCom[] devMon = new CGJMonCom[3];
        /// <summary>
        /// ERS
        /// </summary>
        private CERSCom[] devERS = new CERSCom[2];

        private SajetMES MesSajet = null;
        #endregion

        #region 设备初始化
        /// <summary>
        /// 打开PLC设备
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect"></param>
        /// <returns></returns>
        private bool OpenPLC(ref string er, bool connect = true)
        {
            try
            {
                if (connect)
                {
                    if (devPLC == null)
                    {
                        devPLC = new CPLCCom(EPLCType.InovanceTCP);
                        if (!devPLC.open(CSysPara.mVal.plcIp, ref er, CSysPara.mVal.plcPort.ToString()))
                        {
                            er = "连接到测试PLC[" + CSysPara.mVal.plcIp + "]失败:" + er;
                            devPLC = null;
                            return false;
                        }
                        er = "成功连接到测试PLC主机[" + CSysPara.mVal.plcIp +"]";
                    }
                    if (devPLCTemp  == null)
                    {
                        devPLCTemp = new CPLCCom(EPLCType.InovanceTCP);
                        if (!devPLCTemp.open(CSysPara.mVal.plcTempIp ,ref er, CSysPara.mVal.plcTempPort.ToString()))
                        {
                            er = "连接到温度PLC[" + CSysPara.mVal.plcTempIp + "]失败:" + er;
                            devPLCTemp = null;
                            return false;
                        }
                        er = "成功连接到温度PLC主机[" + CSysPara.mVal.plcTempIp + "]";
                    }
                }
                else
                {
                    if (devPLC != null)
                    {
                        devPLC.close();
                        devPLC = null;
                        er = "断开连接到测试PLC主机[" + CSysPara.mVal.plcIp + "]";
                    }

                    if (devPLCTemp != null)
                    {
                        devPLCTemp.close();
                        devPLCTemp = null;
                        er = "断开连接到测试PLC主机[" + CSysPara.mVal.plcTempIp + "]";
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 打开IDCard
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect"></param>
        /// <returns></returns>
        private bool OpenIDCard(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;
                if (connect)
                {
                    if (devIDCard == null)
                    {
                        devIDCard = new CCardCom();
                        if (!devIDCard.open(CSysPara.mVal.idCom, ref er))
                        {
                            er = "打开读卡器串口" + CSysPara.mVal.idCom + "失败:" + er;
                            devIDCard = null;
                            return false;
                        }
                        string rSn = string.Empty;
                        for (int i = 0; i < C_ID_MAX; i++)
                        {
                            CMath.delayMs(200);
                            if (!devIDCard.GetRecorderSn(1 + i, ref rSn, ref er))
                            {
                                CMath.delayMs(200);
                                if (!devIDCard.GetRecorderSn(1 + i, ref rSn, ref er))
                                {
                                    er = "读取读卡器地址[" + (1 + i).ToString("D2") + "]数据失败:" + er;
                                    devIDCard.close();
                                    devIDCard = null;
                                    return false;
                                }
                            }
                        }
                        er = "成功打开读卡器串口" + CSysPara.mVal.idCom;
                    }
                }
                else
                {
                    if (devIDCard != null)
                    {
                        devIDCard.close();
                        devIDCard = null;
                        er = "关闭读卡器串口" + CSysPara.mVal.idCom;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 打开控制板
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect"></param>
        /// <returns></returns>
        private bool OpenGJMon(int idNo, ref string er, bool connect = true)
        {
            try
            { 
                string[] iLayName = new string[] { "C1-8", "C9-16","C17-24"};
                er = string.Empty;
                if (connect)
                {
                    if (devMon[idNo] == null)
                    {
                        devMon[idNo] = new CGJMonCom();
                        if (!devMon[idNo].open(CSysPara.mVal.monCom[idNo], ref er))
                        {
                            er = "打开控制板" + (idNo + 1).ToString() + "串口" + CSysPara.mVal.monCom[idNo] + "失败:" + er;
                            devMon[idNo] = null;
                            return false;
                        }
                        bool monFlag = false;
                        string rVersion = string.Empty;
                        for (int i = 0; i < C_MON_MAX[idNo]; i++)
                        {
                            if (!devMon[idNo].ReadVersion(i + 1, ref rVersion, ref er))
                            {
                                CMath.delayMs(50);
                                if (!devMon[idNo].ReadVersion(i + 1, ref rVersion, ref er))
                                {
                                    runLog.Log(iLayName[idNo] + "控制板[" + (i + 1).ToString("D2") + "]通信异常", udcRunLog.ELog.NG);
                                    continue;
                                }
                            }
                            monFlag = true;
                        }
                        if (!monFlag)
                        {
                            er = "打开" + iLayName[idNo] + "控制板串口" + CSysPara.mVal.monCom[idNo] + "失败";
                            devMon[idNo].close();
                            devMon[idNo] = null;
                            return false;
                        }
                        er = "成功打开" + iLayName[idNo] + "控制板串口" + CSysPara.mVal.monCom[idNo];
                    }
                }
                else
                {
                    if (devMon[idNo] != null)
                    {
                        devMon[idNo].close();
                        devMon[idNo] = null;
                        er = "关闭" + iLayName[idNo] + "控制板串口" + CSysPara.mVal.monCom[idNo];
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 打开ERS
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect"></param>
        /// <returns></returns>
        private bool OpenGJERS(int idNo,ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;
                string[] iLayName = new string[] { "C1-8","C17-24" };
                if (connect)
                {
                    if (devERS[idNo] == null)
                    {
                        devERS[idNo] = new CERSCom(EERSType.GJ272_4);
                        if (!devERS[idNo].open(CSysPara.mVal.ersCom[idNo], ref er))
                        {
                            er = "打开ERS串口" + CSysPara.mVal.ersCom[idNo] + "失败:" + er;
                            devERS[idNo] = null;
                            return false;
                        }
                        bool monFlag = false;
                        string rVersion = string.Empty;
                        for (int i = 0; i < C_ERS_MAX[idNo]; i++)
                        {
                            if (!devERS[idNo].ReadVersion(i + 1, ref rVersion, ref er))
                            {
                                CMath.delayMs(50);
                                if (!devERS[idNo].ReadVersion(i + 1, ref rVersion, ref er))
                                {
                                    runLog.Log(iLayName[idNo] + "ERS地址[" + (i + 1).ToString("D2") + "]通信异常", udcRunLog.ELog.NG);
                                    continue;
                                }
                            }
                            monFlag = true;
                        }
                        if (!monFlag)
                        {
                            er = "打开" +iLayName[idNo] + "ERS串口" + CSysPara.mVal.ersCom[idNo] + "失败";
                            devERS[idNo].close();
                            devERS[idNo] = null;
                            return false;
                        }
                        er = "成功打开" + iLayName[idNo] + "ERS串口" + CSysPara.mVal.ersCom[idNo];
                    }
                }
                else
                {
                    if (devERS[idNo] != null)
                    {
                        devERS[idNo].close();
                        devERS[idNo] = null;
                        er = "关闭" + iLayName[idNo] + "ERS串口" + CSysPara.mVal.ersCom;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 连接MES
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect"></param>
        /// <returns></returns>
        private bool ConnectToMes(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;
                if (connect)
                {
                    if (MesSajet == null)
                    {
                        MesSajet = new SajetMES();
                        if (MesSajet.StartConnect(5))
                            er = "成功连接到MES";
                        else
                        {
                            er = "启动MES失败！";
                            return false;
                        }

                        Thread.Sleep (3000);
                        if (MesSajet.Login("222", "222", ref er))
                            er = "登录到MES成功";
                        else
                        {
                            er = "登录MES失败！错误:" + er;
                            return false;
                        }
                        //string mModename =string.Empty ;
                        //string mStation =string.Empty ;
                        //if(MesSajet.Chk_SN ("FTH1501000391",ref mModename ,ref mStation ,ref er))
                        //{
                        //    er = "MES检查条码成功";
                        //}
                        //else
                        //{
                        //    er = "MES检查条码失败！" + er;
                        //}
                        //if (MesSajet.Tran_SN ("FTH1501000391", "PASS", ref er))
                        //{
                        //    er = "成功连接到MES";
                        //}
                        //else
                        //{
                        //    er = "启动MES失败！";
                        //}
                    }
                }
                else
                {
                    if (MesSajet != null)
                    {
                        MesSajet.StopConnect();
                        MesSajet = null;
                        er = "关闭MES连接";
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 开始测试
        /// <summary>
        /// 启动测试
        /// </summary>
        /// <param name="run"></param>
        /// <returns></returns>
        private bool StartRun(bool run = true)
        {
            try
            {
                if (run)
                {
                    IniStatusPara();
                    if (!ConnectToDevice(run))
                    {
                        ConnectToDevice(false);
                        return false;
                    }
                    if (!StartThread(run))
                    {
                        StartThread(false);
                        ConnectToDevice(false);
                        return false;
                    }                        
                    btnModel.Enabled = false;  
                }
                else
                {
                    if (!StartThread(run))
                        return false;
                    if (!ConnectToDevice(run))
                        return false;
                    btnModel.Enabled = true;  
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 初始化测试状态
        /// </summary>
        private void IniStatusPara()
        {
            C_SCAN_DEV_START = false;
            runFix.inDoRun = EFixRun.空闲;
            runFix.IniStart = true;
            for (int i = 0; i < runFix.inFix.Count; i++)
                runFix.inFix[i].doRun = EFixRun.空闲; 
        }
        /// <summary>
        /// 打开测试设备
        /// </summary>
        /// <param name="connect"></param>
        /// <returns></returns>
        private bool ConnectToDevice(bool connect = true)
        {
            try
            {
                bool chkDev = true;
                string er = string.Empty;
                if (CSysPara.mVal.conMes)
                {

                    if (ConnectToMes(ref er, connect))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
                    }
                }

                if (OpenPLC(ref er, connect))
                    runLog.Log(er, udcRunLog.ELog.Action);
                else
                {
                    chkDev = false;
                    runLog.Log(er, udcRunLog.ELog.NG);
                }
                if (OpenIDCard(ref er, connect))
                    runLog.Log(er, udcRunLog.ELog.Action);
                else
                {
                    chkDev = false;
                    runLog.Log(er, udcRunLog.ELog.NG);
                }
                for (int i = 0; i < C_MON_MAX.Length ; i++)
                {
                    if (OpenGJMon(i, ref er, connect))
                        runLog.Log(er, udcRunLog.ELog.Action);
                    else
                    {
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
                    }
                }
                for (int i = 0; i < C_ERS_MAX.Length; i++)
                {
                    if (OpenGJERS(i,ref er, connect))
                        runLog.Log(er, udcRunLog.ELog.Action);
                    else
                    {
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
                    }

                }

                if (!chkDev)
                    return false;
                if (connect)
                {
                    if (CGJMES.CheckMySQL(ref er))
                        runLog.Log("成功连接MES远端服务器【" + CSysPara.mVal.mySqlIp + "】", udcRunLog.ELog.Action);
                    else
                    {
                        chkDev = false;
                        runLog.Log("连接MES远端服务器【" + CSysPara.mVal.mySqlIp + "】错误:" + er, udcRunLog.ELog.NG);
                    }
                }
                return chkDev;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }
        /// <summary>
        /// 启动线程
        /// </summary>
        /// <param name="connect"></param>
        /// <returns></returns>
        private bool StartThread(bool connect = true)
        {
            try
            {
                string er = string.Empty;
                if (connect)
                {
                    if (ThreadPLC == null)
                    {
                        ThreadPLC = new CPLCPara(plcDB);
                        ThreadPLC.OnStatusArgs.OnEvent += new COnEvent<CPLCConArgs>.OnEventHandler(OnPLCConArgs);
                        ThreadPLC.OnDataArgs.OnEvent += new COnEvent<CPLCDataArgs>.OnEventHandler(OnPLCDataArgs);
                        ThreadPLC.spinUp(devPLC);
                        runLog.Log("PLC监控线程启动", udcRunLog.ELog.Content);
                    }

                    if (ThreadPLCTemp == null)
                    {
                        ThreadPLCTemp = new CPLCPara(plcTempDB);
                        ThreadPLCTemp.OnStatusArgs.OnEvent += new COnEvent<CPLCConArgs>.OnEventHandler(OnPLCConArgs);
                        ThreadPLCTemp.OnDataArgs.OnEvent += new COnEvent<CPLCDataArgs>.OnEventHandler(OnPLCDataArgs);
                        ThreadPLCTemp.spinUp(devPLCTemp);
                        runLog.Log("温度PLC监控线程启动", udcRunLog.ELog.Content);
                    }

                    for (int i = 0; i < C_MON_MAX.Length; i++)
                    {
                        ThreadMon[i] = new CGJMonPara(i, "控制板总线"+(i+1).ToString(), C_MON_MAX[i], false);
                        ThreadMon[i].OnStatusArgs.OnEvent += new COnEvent<CMonConArgs>.OnEventHandler(OnMonConArgs);
                        ThreadMon[i].OnDataArgs.OnEvent += new COnEvent<CMonDataArgs>.OnEventHandler(OnMonDataArgs);
                        ThreadMon[i].spinUp(devMon[i]);
                        runLog.Log("控制板总线"+(i+1).ToString()+"监控线程启动", udcRunLog.ELog.Content);
                    }
                   for (int i = 0; i < C_ERS_MAX.Length ;i++)
                    {
                        ThreadERS[i] = new CERSPara(i, "ERS总线" + (i + 1).ToString(), C_ERS_MAX[i], false);
                        ThreadERS[i].OnStatusArgs.OnEvent += new COnEvent<CERSConArgs>.OnEventHandler(OnERSConArgs);
                        ThreadERS[i].OnDataArgs.OnEvent += new COnEvent<CERSDataArgs>.OnEventHandler(OnERSDataArgs);
                        ThreadERS[i].spinUp(devERS[i]);
                        runLog.Log("ERS总线" + (i + 1).ToString() + "监控线程启动", udcRunLog.ELog.Content);
                    }
                    //测试线程
                    if (ThreadMontor == null)
                    {
                        MontorCancel = false;
                        ThreadMontor = new Thread(OnMontorStart);
                        ThreadMontor.IsBackground = true;
                        ThreadMontor.Start();
                        runLog.Log("测试监控线程启动", udcRunLog.ELog.Content);
                    }
                    SetBIRunMode();
                }
                else
                {
                    if (ThreadMontor != null)
                    {
                        MontorCancel = true;
                        while (MontorCancel)
                            Application.DoEvents();
                        ThreadMontor = null;
                    }
                    if (ThreadPLC != null)
                    {
                        ThreadPLC.spinDown();
                        ThreadPLC = null;
                        runLog.Log("线体PLC监控线程销毁", udcRunLog.ELog.Content);
                    }
                    if (ThreadPLCTemp != null)
                    {
                        ThreadPLCTemp.spinDown();
                        ThreadPLCTemp = null;
                        runLog.Log("温控PLC监控线程销毁", udcRunLog.ELog.Content);
                    }
                    if (ThreadMon != null)
                    {

                        if (ThreadMon[0] != null)
                        {
                            ThreadMon[0].spinDown();
                            ThreadMon[0] = null;
                            runLog.Log("控制板总线1监控线程销毁", udcRunLog.ELog.Content);
                        }
                        if (ThreadMon[1] != null)
                        {
                            ThreadMon[1].spinDown();
                            ThreadMon[1] = null;
                            runLog.Log("控制板总线2监控线程销毁", udcRunLog.ELog.Content);
                        }

                        if (ThreadMon[2] != null)
                        {
                            ThreadMon[2].spinDown();
                            ThreadMon[2] = null;
                            runLog.Log("控制板总线2监控线程销毁", udcRunLog.ELog.Content);
                        }

                    }
                    if (ThreadERS != null)
                    {
                        if (ThreadERS[0]!=null)
                        {
                            ThreadERS[0].spinDown();
                            ThreadERS[0] = null;
                            runLog.Log("ERS总线监控线程销毁", udcRunLog.ELog.Content);
                        }

                        if (ThreadERS[0] != null)
                        {
                            ThreadERS[0].spinDown();
                            ThreadERS[0] = null;
                            runLog.Log("ERS总线监控线程销毁", udcRunLog.ELog.Content);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }
        #endregion

        #region 线程参数
        /// <summary>
        /// PLC监控线程
        /// </summary>
        private CPLCPara ThreadPLC = null;
        private CPLCPara ThreadPLCTemp = null;
        private CGJMonPara[] ThreadMon = new CGJMonPara[3];
        private CERSPara[] ThreadERS = new CERSPara[2];
        private Thread ThreadMontor = null;
        private volatile bool MontorCancel = false;
        #endregion

        #region 测试线程
        /// <summary>
        /// 测试线程
        /// </summary>
        private void OnMontorStart()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        string er = string.Empty;

                        if (MontorCancel)
                            return;

                        if (!checkSystem(ref er))
                        {
                            Thread.Sleep(C_ALARM_DELAY);
                            continue;
                        }                        
                        
                        if (!RefreshSignalUI(ref er))
                            runLog.Log(er, udcRunLog.ELog.Err);

                        if (!SetIniBIPara(ref er))
                            runLog.Log(er, udcRunLog.ELog.Err);

                        if (ThreadPLC.rREGVal[EPLCINP.PLC系统运行.ToString()] != CPLCCom.ON)
                        {
                            if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                                C_SYS_ALARM_COUNT++;
                            else
                            {
                                C_SYS_ALARM_COUNT = 0;
                                runLog.Log("线体PLC未启动运行,请启动PLC", udcRunLog.ELog.NG);
                            }
                            Thread.Sleep(C_ALARM_DELAY);
                            continue;
                        }

                        if (ThreadPLC.rREGVal[EPLCINP.PLC异常报警.ToString()] == CPLCCom.ON)
                        {
                            if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                                C_SYS_ALARM_COUNT++;
                            else
                            {
                                C_SYS_ALARM_COUNT = 0;
                                runLog.Log("线体PLC异常报警,请检查报警", udcRunLog.ELog.NG);
                            }
                            Thread.Sleep(C_ALARM_DELAY);
                            continue;
                        }                       
 
                        if(!UpdateFixIn(ref er))
                            runLog.Log(er, udcRunLog.ELog.Err); 

                        //等待监控扫描周期完成
                        if (!ScanMonComplete(ref er))
                        {
                            Thread.Sleep(C_THREAD_DELAY);
                            continue;
                        }
                        if (!UpdateTestData(ref er))
                           runLog.Log(er, udcRunLog.ELog.Err);
                        if (!UpdateDataBase(ref er))
                            runLog.Log(er, udcRunLog.ELog.Err);
                        if (!AssignInOutTask(ref er))
                           runLog.Log(er, udcRunLog.ELog.Err);   
                        if(!CheckSlot(ref er))
                           runLog.Log(er, udcRunLog.ELog.Err);   
                        if(!UpdateSlot(ref er))
                           runLog.Log(er, udcRunLog.ELog.Err); 
                        if(!UpdateUUTBINum(ref er))
                            runLog.Log(er, udcRunLog.ELog.Err); 
                        if(!SaveTestReport(ref er))
                            runLog.Log(er, udcRunLog.ELog.Err); 
                        Thread.Sleep(C_THREAD_DELAY);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                MontorCancel = false;
                runLog.Log("测试监控线程销毁", udcRunLog.ELog.Content);
            }
        }

        #region 方法
        /// <summary>
        /// 设置控制板运行模式3->为快充模式
        /// </summary>
        private void SetBIRunMode()
        {
            string er = string.Empty;

            ThreadMon[0].setRunMode(0, 4, ref er);

            ThreadMon[1].setRunMode(0, 4, ref er);

            ThreadMon[2].setRunMode(0, 4, ref er);
        }
        /// <summary>
        /// 初始化老化房参数:老化输入电压和温度
        /// </summary>
        /// <param name="er"></param>
        private bool SetIniBIPara(ref string er)
        {
            try
            {
                er = string.Empty;

                if (runFix.tempRequest)
                {
                    ThreadPLCTemp.addREGWrite(EPLCOUT.温度设定值.ToString(), (int)(runModel.TSet * 10));
                    ThreadPLCTemp.addREGWrite(EPLCOUT.下限偏差.ToString(), (int)(runModel.THP * 10));
                    ThreadPLCTemp.addREGWrite(EPLCOUT.上限偏差.ToString(), (int)(runModel.TLP * 10));
                    ThreadPLCTemp.addREGWrite(EPLCOUT.超温上限偏差.ToString(), (int)((runModel.THAlarm - runModel.TSet) * 10));
                    ThreadPLCTemp.addREGWrite(EPLCOUT.启动排风温度.ToString(), (int)(runModel.TOPEN * 10));
                    ThreadPLCTemp.addREGWrite(EPLCOUT.停止排风温度.ToString(), (int)(runModel.TCLOSE * 10));
                    runFix.tempRequest = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 刷新测试信号
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool RefreshSignalUI(ref string er)
        {
            try
            {
                er = string.Empty;

                if (ThreadPLC.rREGVal[EPLCINP.PLC系统运行.ToString()] == CPLCCom.OFF)
                    runFix.sysStat = "未运行";
                else
                {
                    if (ThreadPLC.rREGVal[EPLCINP.PLC异常报警.ToString()] == CPLCCom.ON)
                        runFix.sysStat = "报警中";
                    else
                        runFix.sysStat = "运行中";
                }
                //获取平均温度值
                double rTemp = 0;
                for (int i = 0; i < runFix.rTempPoint.Count; i++)
                {
                    runFix.rTempPoint[i] = ((double)ThreadPLCTemp.rREGVal[InpPLC(EPLCINP.TC1_1, i)]) / 10;
                    rTemp += runFix.rTempPoint[i];
                }
                runFix.rTemp = rTemp / runFix.rTempPoint.Count;
                //获取机械手状态
                runFix.handStat = (EHandStat)ThreadPLC.rREGVal[EPLCINP.机械手状态.ToString()];             
                //获取入口和出口状态
                if (ThreadPLC.rREGVal[EPLCINP.老化入口处上层治具到位信号.ToString()] == CPLCCom.ON &&
                    ThreadPLC.rREGVal[EPLCINP.老化入口处下层治具到位信号.ToString()] == CPLCCom.ON)
                    runFix.rInReady = 1;
                else
                    runFix.rInReady = 0;
                runFix.rOutReady = ThreadPLC.rREGVal[EPLCINP.老化出口处平台空闲.ToString()];

                updateSignalUI();

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 老化入口治具
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool UpdateFixIn(ref string er)
        {
            try
            {
                if (ThreadPLC.rREGVal[EPLCINP.老化入口处上层治具到位信号.ToString()] == CPLCCom.OFF &&
                    ThreadPLC.rREGVal[EPLCINP.老化入口处下层治具到位信号.ToString()] == CPLCCom.OFF && 
                    ThreadPLC.rREGVal[EPLCINP.老化入口上层请求读取ID卡.ToString()] == CPLCCom.OFF &&
                    ThreadPLC.rREGVal[EPLCINP.老化入口下层请求读取ID卡.ToString()] == CPLCCom.OFF)
                {
                    for (int i = 0; i < runFix.inFix.Count; i++)
                        runFix.inFix[i].doRun = EFixRun.空闲;
                    runFix.inDoRun = EFixRun.空闲;
                    return true;
                }

                if (runFix.inDoRun == EFixRun.报警)
                {
                    for (int i = 0; i < runFix.inFix.Count; i++)
                    {
                        ThreadPLC.addREGWrite(OutPLC(EPLCOUT.老化入口平台上层_ID读取成功, i), 1);
                    }
                    if (ThreadPLC.rREGVal[EPLCINP.老化入口平台准备OK.ToString()] == CPLCCom.ON)
                    {
                        ThreadPLC.addREGWrite(EPLCOUT.上层取治具层数.ToString(), 29);
                        ThreadPLC.addREGWrite(EPLCOUT.上层取治具列数.ToString(), 30);
                        ThreadPLC.addREGWrite(EPLCOUT.下层取治具层数.ToString(), 30);
                        ThreadPLC.addREGWrite(EPLCOUT.下层取治具列数.ToString(), 30);
                        //放位置
                        ThreadPLC.addREGWrite(EPLCOUT.上层放治具层数.ToString(), 31);
                        ThreadPLC.addREGWrite(EPLCOUT.上层放治具列数.ToString(), 31);
                        ThreadPLC.addREGWrite(EPLCOUT.下层放治具层数.ToString(), 32);
                        ThreadPLC.addREGWrite(EPLCOUT.下层放治具列数.ToString(), 31);
                        //取位置->重发
                        ThreadPLC.addREGWrite(EPLCOUT.上层取治具层数.ToString(), 29);
                        ThreadPLC.addREGWrite(EPLCOUT.上层取治具列数.ToString(), 30);
                        ThreadPLC.addREGWrite(EPLCOUT.下层取治具层数.ToString(), 30);
                        ThreadPLC.addREGWrite(EPLCOUT.下层取治具列数.ToString(), 30);
                        //放位置->重发
                        ThreadPLC.addREGWrite(EPLCOUT.上层放治具层数.ToString(), 31);
                        ThreadPLC.addREGWrite(EPLCOUT.上层放治具列数.ToString(), 31);
                        ThreadPLC.addREGWrite(EPLCOUT.下层放治具层数.ToString(), 32);
                        ThreadPLC.addREGWrite(EPLCOUT.下层放治具列数.ToString(), 31);

                        ThreadPLC.addREGWrite(EPLCOUT.发送层列完成.ToString(), 1);
                        runFix.inDoRun = EFixRun.就绪;
                       //
                    }
                }
                if (ThreadPLC.rREGVal[EPLCINP.老化入口上层请求读取ID卡.ToString()] != CPLCCom.ON ||
                    ThreadPLC.rREGVal[EPLCINP.老化入口下层请求读取ID卡.ToString()] != CPLCCom.ON)
                    return true;

                if (runFix.inDoRun != EFixRun.空闲)
                    return true;

                runFix.IniStart = false;
                er = string.Empty;
                string statName = "入口进机位";

                //1.读取入口治具1和治具2信息
                for (int i = 0; i < runFix.inFix.Count; i++)
                {
                    string rIdCard = string.Empty;
                    devIDCard.GetRecord(1 + i, ref rIdCard, ref er);
                    Thread.Sleep(300);
                    // if (!readIdFromBIPLC(i, ref rIdCard, ref er))
                    if (!devIDCard.GetRecord(1 + i, ref rIdCard, ref er))  //改成读取ID卡
                    {
                        Thread.Sleep(300);
                        if (!devIDCard.GetRecord(1 + i, ref rIdCard, ref er))  //改成读取ID卡
                        {
                            runFix.inDoRun = EFixRun.报警;
                            runFix.inFix[i].doRun = EFixRun.报警;
                            ThreadPLC.addREGWrite(OutPLC(EPLCOUT.老化入口平台上层_ID读取失败, i), 1);
                            runLog.Log("<" + statName + ">读取治具" + (i + 1).ToString() + "ID卡失败1.", udcRunLog.ELog.NG);
                            continue;
                        }
                    }
                    Thread.Sleep(300);
                    runFix.inFix[i].idCard = rIdCard;
                    runLog.Log("<" + statName + ">治具" + (i + 1).ToString() + "[" + runFix.inFix[i].idCard + "]就绪,检查治具流程.",
                                udcRunLog.ELog.Action);
                    //检查治具流程
                    Dictionary<int, CGJMES.CPara> uutPara = new Dictionary<int, CGJMES.CPara>();
                    CGJMES.EFlowCode flowCode = CGJMES.EFlowCode.正常流程;
                    if (!CGJMES.checkFixFlow(CGlobal.CFlow.flowId, runFix.inFix[i].idCard, ref uutPara, ref flowCode, ref er))
                    {
                        if (flowCode == CGJMES.EFlowCode.空治具过站)
                        {
                            runFix.inFix[i].IsFixNull = 1;
                            runFix.inFix[i].doRun = EFixRun.进站;
                            runLog.Log("<" + statName + ">治具" + (i + 1).ToString() + "[" + runFix.inFix[i].idCard + "]为空治具,准备进站.",
                                        udcRunLog.ELog.OK);
                            continue;
                        }
                        else
                        {
                           
                            runFix.inDoRun = EFixRun.报警;
                            runFix.inFix[i].doRun = EFixRun.报警;
                       //     ThreadPLC.addREGWrite(OutPLC(EPLCOUT.老化入口平台上层_ID读取失败, i), 1);
                            runLog.Log("<" + statName + ">治具" + (i + 1).ToString() + "[" + runFix.inFix[i].idCard + "]:" + er,
                                        udcRunLog.ELog.NG);
                            continue;
                        }
                    }
                    runFix.inFix[i].IsFixNull = 0;
                    //获取产品条码->屏蔽不良产品条码
                    bool HaveUUT = false;
                    foreach (int slotNo in uutPara.Keys)
                    {
                        if (uutPara[slotNo].result == 0)
                        {
                            runFix.inFix[i].serialNo[slotNo] = uutPara[slotNo].serialNo;
                            HaveUUT = true;
                        }
                        else
                            runFix.inFix[i].serialNo[slotNo] = "";
                    }
                    if (!HaveUUT)
                    {
                        runFix.inDoRun = EFixRun.报警;
                        runFix.inFix[i].doRun = EFixRun.报警;
                      //  ThreadPLC.addREGWrite(OutPLC(EPLCOUT.老化入口平台上层_ID读取失败, i), 1);
                        runLog.Log("<" + statName + ">治具" + (i + 1).ToString() + "[" + runFix.inFix[i].idCard + "]无可测产品.",
                                    udcRunLog.ELog.NG);
                        continue;
                    }
                    
                    //获取机种名
                    runFix.inFix[i].modelName = uutPara[0].flowModel;
                    runFix.inFix[i].doRun = EFixRun.进站;
                    runLog.Log("<" + statName + ">治具" + (i + 1).ToString() + "[" + runFix.inFix[i].idCard + "]流程检查OK.",
                                        udcRunLog.ELog.OK);
                }
                //2.综合两个治具检查机种参数
                if (runFix.inDoRun == EFixRun.空闲)
                {
                    string modelName = string.Empty;
                    if (runFix.inFix[0].IsFixNull == 0 && runFix.inFix[1].IsFixNull == 0)
                    {
                        if (runFix.inFix[0].modelName != runFix.inFix[1].modelName)
                        {
                            for (int i = 0; i < runFix.inFix.Count; i++)
                            {
                                runFix.inFix[i].doRun = EFixRun.报警;
                            //   ThreadPLC.addREGWrite(OutPLC(EPLCOUT.老化入口平台上层_ID读取失败, i), 1);
                            }
                            runLog.Log("<" + statName + ">治具1与治具2老化机种不一致.", udcRunLog.ELog.NG);
                            runFix.inDoRun = EFixRun.报警;
                        }
                        else
                            modelName = runFix.inFix[0].modelName;
                    }
                    else if (runFix.inFix[1].IsFixNull == 0) //治具1为空治具
                    {
                        modelName = runFix.inFix[1].modelName;
                    }
                    else if (runFix.inFix[0].IsFixNull == 0) //治具2为空治具
                    {
                        modelName = runFix.inFix[1].modelName;
                    }
                    else  //治具1,2为空治具
                    {
                        modelName = runModel.model;
                    }
                    //自动调用机种参数
                    if (modelName != runModel.model)
                    {
                        string modelPath = CSysPara.mVal.modelPath + "\\" + modelName + ".bi";
                        if (!File.Exists(modelPath))
                            runLog.Log("<" + statName + ">无法调用机种参数[" + modelPath + "]", udcRunLog.ELog.NG);
                        else
                        {
                            CModelSet<CModelPara>.load(modelPath, ref runModel);
                            CIniFile.WriteToIni("Parameter", "ModelPath", modelPath, iniFile);
                            runFix.tempRequest = true;
                            runLog.Log("<" + statName + ">重新调机种参数[" + runModel.model + "]", udcRunLog.ELog.Action);
                            loadModelPara();
                        }
                    }
                    runFix.inFix[0].modelName = runModel.model;
                    runFix.inFix[1].modelName = runModel.model;
                    //设置进站标志
                    if (runFix.inDoRun == EFixRun.空闲)
                    {
                        for (int i = 0; i < runFix.inFix.Count; i++)
                            ThreadPLC.addREGWrite(OutPLC(EPLCOUT.老化入口平台上层_ID读取成功, i), 1);
                        runFix.inDoRun = EFixRun.进站;
                    }
                }
       
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true;
            }
        }
        /// <summary>
        /// 监控线程同步完成
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool ScanMonComplete(ref string er)
        {
            try
            {
                if (ThreadMon[0].mStatus != CGJMonPara.EStatus.暂停)
                    return false;
                if (ThreadMon[1].mStatus != CGJMonPara.EStatus.暂停)
                    return false;
                if (ThreadMon[2].mStatus !=CGJMonPara.EStatus .暂停)
                    return false;
                if (ThreadERS[0].mStatus != CERSPara.EStatus.暂停)
                    return false;
                if (ThreadERS[1].mStatus != CERSPara.EStatus.暂停)
                    return false;
                if (!C_SCAN_DEV_START)   //未启动扫描->启动扫描
                {
                    ThreadMon[0].continued();
                    ThreadMon[1].continued();
                    ThreadMon[2].continued();
                    ThreadERS[0].continued();
                    ThreadERS[1].continued();
                    C_SCAN_DEV_START = true;
                    C_SCAN_DEV_TIME = System.Environment.TickCount;
                    return false;
                }
                C_SCAN_DEV_START = false;
                double timeS = ((double)(System.Environment.TickCount - C_SCAN_DEV_TIME)) / 1000;
                runLog.Log("监控扫描周期=" + timeS.ToString("0.0") + "秒", udcRunLog.ELog.Action);
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 修改测试状态
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool UpdateTestData(ref string er)
        {
            try
            {
                //解析测试数据   
                for (int i = 0; i < C_UUT_MAX; i++)
                {
                   // if (i == 108)
                     //   continue;

                    //获取信息
                    int iCom = runUUT[i].wBase.ctrlCom;
                    int iAddr = runUUT[i].wBase.ctrlAddr - 1;
                    int subNo = ((i) % 2);
                    runUUT[i].wPara.ctrlStartFlag = ThreadMon[iCom].mon[iAddr].rData.signal.startFlag;
                    runUUT[i].wPara.ctrlBIFinishFlag = ThreadMon[iCom].mon[iAddr].rData.signal.biFinishFlag;
                    runUUT[i].wPara.ctrlRunFlag = ThreadMon[iCom].mon[iAddr].rData.signal.runTypeFlag;
                    if (runUUT[i].wPara.ctrlStartFlag == 3) //老化中更新时间
                    {
                        runUUT[i].wPara.ctrlRunTime = ThreadMon[iCom].mon[iAddr].rData.signal.runTolTime * 60 +
                                                         ThreadMon[iCom].mon[iAddr].rData.signal.secMinCnt;
                        runUUT[i].wPara.runTime = runUUT[i].wPara.ctrlRunTime;
                        if (runUUT[i].wPara.ctrlRunOnOff == 0)  //AC OFF
                            runUUT[i].wPara.ShutDownAC = true;
                    }
                    runUUT[i].wPara.ctrlRunError = (int)ThreadMon[iCom].mon[iAddr].rData.signal.errCode;
                    runUUT[i].wPara.ctrlOnOff_Cnt = ThreadMon[iCom].mon[iAddr].rData.signal.onoff_Cnt;
                    runUUT[i].wPara.ctrlOnOff_YXDH = ThreadMon[iCom].mon[iAddr].rData.signal.onoff_YXDH;
                    runUUT[i].wPara.ctrlRunOnOff = ThreadMon[iCom].mon[iAddr].rData.data.onOffFlag;
                    runUUT[i].wPara.ctrlUUTONLine = ThreadMon[iCom].mon[iAddr].rData.signal.x[1 + subNo * 2];
                    runUUT[i].wPara.ctrlQCStepNo = ThreadMon[iCom].mon[iAddr].rData.signal.QC_VOLT;
                    //检测是否老化中和AC ON状态中->刷新数据
                    if (runUUT[i].wPara.ctrlStartFlag != 3 || runUUT[i].wPara.ctrlUUTONLine != 1 ||
                        runUUT[i].wPara.ctrlRunOnOff != 1)
                        continue;
                    //检查QC模式是否需重启电压功能?
                    if (runUUT[i].wPara.QCTYPE != EQCV.MTK && runUUT[i].wPara.ResetQCVoltTimes > 0)
                    {
                        ////解析对应输出电压电流规格通道
                        int stepQCNo = 0;
                        for (int CH = 0; CH < runUUT[i].wLed[0].vName.Count; CH++)
                        {
                            if (runUUT[i].wPara.runQCStepNo == runUUT[i].wLed[0].QCV[CH])
                            {
                                stepQCNo = CH;
                                break;
                            }
                        }
                        ////获取产品不良数量
                        int uutIsFailNum = 0;
                        for (int slot = 0; slot < C_SLOT_MAX; slot++)
                        {
                            double unitV = ThreadMon[iCom].mon[iAddr].rData.data.volt[slot + C_SLOT_MAX * subNo];
                            if (runUUT[i].wLed[slot].serialNo != "")
                            {
                                //电压不良
                                if (unitV > 2 &&
                                     (unitV > runUUT[i].wLed[slot].vMax[stepQCNo] || unitV < runUUT[i].wLed[slot].vMin[stepQCNo])
                                    )
                                {
                                    uutIsFailNum++;
                                }
                            }
                        }
                        //不良重启
                        if (uutIsFailNum > 0)
                        {
                            ThreadMon[iCom].setContinue(iAddr, ref er, 0);
                            runUUT[i].wPara.ResetQCVoltTimes--;
                            continue;
                        }
                    }
                    //正常赋值于判断参数
                    for (int slot = 0; slot < C_SLOT_MAX; slot++)
                    {
                        runUUT[i].wLed[slot].unitV = ThreadMon[iCom].mon[iAddr].rData.data.volt[slot + C_SLOT_MAX * subNo*2];
                        int ersCom = runUUT[i].wBase.ersCom;
                        int ersAddr = runUUT[i].wBase.ersAddr - 1;
                        int ersCH = runUUT[i].wBase.ersCH - 1;
                        runUUT[i].wLed[slot].unitA = ThreadERS[ersCom].mon[ersAddr].rData.data.cur[ersCH] *2;
                    }
                }
                //解析测试结果    
                for (int i = 0; i < C_UUT_MAX; i++)
                {
                    if (runUUT[i].wPara.ctrlStartFlag == 3 && runUUT[i].wPara.ctrlUUTONLine == 1 &&
                        runUUT[i].wPara.ctrlRunOnOff == 1 && runUUT[i].wPara.doRun == EDoRun.正在老化)
                    {
                        ////快充电压发生变化-->需要处理负载设置
                        if (runUUT[i].wPara.QCTYPE == EQCV.MTK)   //MTK
                        {
                            //当为治具2测试数据才处理MTK
                            if (i % 2 == 0)
                            {
                                if (runUUT[i].wPara.runQCStepNo != runUUT[i].wPara.ctrlQCStepNo)
                                {
                                    runUUT[i].wPara.runQCStepNo = runUUT[i].wPara.ctrlQCStepNo;
                                    runUUT[i].wPara.MTK_SPEC_OK = 0;
                                    runUUT[i + 1].wPara.runQCStepNo = runUUT[i].wPara.ctrlQCStepNo;
                                    runUUT[i + 1].wPara.MTK_SPEC_OK = 0;
                                    setFixLoadVal(i, false, ref er);
                                    continue;
                                }
                                setMTK(i, ref er);
                            }
                            //电压调正未完成->等待MTK电压调正
                            if (runUUT[i].wPara.MTK_SPEC_OK == 0)
                                continue;
                        }
                        else                                     //QC2.0,QC3.0                   
                        {
                            if (runUUT[i].wPara.runQCStepNo != runUUT[i].wPara.ctrlQCStepNo)
                            {
                                runUUT[i].wPara.runQCWaitTimes = 0;
                                runUUT[i].wPara.runQCStepNo = runUUT[i].wPara.ctrlQCStepNo;
                                setFixLoadVal(i, false, ref er);
                                continue;
                            }
                            //负载设置完成:等待3个扫描周期
                            if (runUUT[i].wPara.runQCWaitTimes < 2)
                            {
                                runUUT[i].wPara.runQCWaitTimes++;
                                continue;
                            }
                        }
                        ////解析对应输出电压电流规格通道
                        int stepNo = 0;
                        for (int CH = 0; CH < runUUT[i].wLed[0].vName.Count; CH++)
                        {
                            if (runUUT[i].wPara.runQCStepNo == runUUT[i].wLed[0].QCV[CH])
                            {
                                stepNo = CH;
                                break;
                            }
                        }
                        ////判断电压电流规格
                        if (CSysPara.mVal.VLP == 0)
                            CSysPara.mVal.VLP = 1;
                        if (CSysPara.mVal.VHP == 0)
                            CSysPara.mVal.VHP = 1;
                        if (CSysPara.mVal.ILP == 0)
                            CSysPara.mVal.ILP = 1;
                        if (CSysPara.mVal.IHP == 0)
                            CSysPara.mVal.IHP = 1;

                        for (int slot = 0; slot < C_SLOT_MAX; slot++)
                        {
                            bool uutPass = true;

                            string failInfo = string.Empty;

                            if (runUUT[i].wLed[slot].serialNo != "")
                            {
                                //电压处理
                                if (runUUT[i].wLed[slot].unitV > runUUT[i].wLed[slot].vMax[stepNo] ||
                                    runUUT[i].wLed[slot].unitV < runUUT[i].wLed[slot].vMin[stepNo])
                                {
                                    if (runUUT[i].wLed[slot].passResult != 0) //原先产品不良
                                    {
                                        failInfo = "电压=" + runUUT[i].wLed[slot].unitV.ToString("0.000") + "V;";
                                        uutPass = false;
                                    }
                                    else
                                    {
                                        //产品原先PASS,处理当前当机状态
                                        if (runUUT[i].wLed[slot].unitV > runUUT[i].wLed[slot].vMin[stepNo] * CSysPara.mVal.VLP &&
                                            runUUT[i].wLed[slot].unitV < runUUT[i].wLed[slot].vMin[stepNo])  //下限补偿                                          
                                            runUUT[i].wLed[slot].unitV = runUUT[i].wLed[slot].vMin[stepNo] + 0.1;
                                        else if (runUUT[i].wLed[slot].unitV > runUUT[i].wLed[slot].vMax[stepNo] &&   //上限补偿 
                                                runUUT[i].wLed[slot].unitV < runUUT[i].wLed[slot].vMax[stepNo] * CSysPara.mVal.VHP)
                                            runUUT[i].wLed[slot].unitV = runUUT[i].wLed[slot].vMax[stepNo] - 0.1;
                                        else
                                        {
                                            if (runUUT[i].wLed[slot].vFailNum > CSysPara.mVal.failTimes)
                                            {
                                                runUUT[i].wLed[slot].passResult = 1;
                                                failInfo = "电压=" + runUUT[i].wLed[slot].unitV.ToString("0.000") + "V;";
                                                uutPass = false;
                                            }
                                            else
                                            {
                                                runUUT[i].wLed[slot].vFailNum++;
                                                runUUT[i].wLed[slot].unitV = runUUT[i].wLed[slot].vBack;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    runUUT[i].wLed[slot].vFailNum = 0;
                                }
                                runUUT[i].wLed[slot].vBack = runUUT[i].wLed[slot].unitV;

                                //计算输出功率>20W->处理电流值
                                double Power = runUUT[i].wLed[slot].iSet[stepNo] * runUUT[i].wLed[slot].vMax[stepNo];
                                if (Power > 20)
                                    runUUT[i].wLed[slot].unitA = runUUT[i].wLed[slot].unitA + CSysPara.mVal.IOFFSET;
                                //电流处理
                                if (!CSysPara.mVal.chkNoJugdeCur)
                                {
                                    if (runUUT[i].wLed[slot].unitA > runUUT[i].wLed[slot].iMax[stepNo] ||
                                        runUUT[i].wLed[slot].unitA < runUUT[i].wLed[slot].iMin[stepNo])
                                    {
                                        if (runUUT[i].wLed[slot].passResult != 0) //原先产品不良
                                        {
                                            runUUT[i].wLed[slot].passResult = 2;
                                            failInfo = failInfo + "电流=" + runUUT[i].wLed[slot].unitA.ToString("0.00") + "A;";
                                            uutPass = false;
                                        }
                                        else              //PASS
                                        {
                                            if (runUUT[i].wLed[slot].unitA > runUUT[i].wLed[slot].iMin[stepNo] * CSysPara.mVal.ILP &&
                                                runUUT[i].wLed[slot].unitA < runUUT[i].wLed[slot].iMin[stepNo])  //下限补偿                                          
                                                runUUT[i].wLed[slot].unitA = runUUT[i].wLed[slot].iMin[stepNo] + 0.1;
                                            else if (runUUT[i].wLed[slot].unitA > runUUT[i].wLed[slot].iMax[stepNo] &&   //上限补偿 
                                                    runUUT[i].wLed[slot].unitA < runUUT[i].wLed[slot].iMax[stepNo] * CSysPara.mVal.IHP)
                                                runUUT[i].wLed[slot].unitA = runUUT[i].wLed[slot].iMax[stepNo] - 0.1;
                                            else
                                            {
                                                if (runUUT[i].wLed[slot].iFailNum > CSysPara.mVal.failTimes)
                                                {
                                                    runUUT[i].wLed[slot].passResult = 2;
                                                    failInfo = failInfo + "电流=" + runUUT[i].wLed[slot].unitA.ToString("0.00") + "A;";
                                                    uutPass = false;
                                                }
                                                else
                                                {
                                                    runUUT[i].wLed[slot].iFailNum++;
                                                    runUUT[i].wLed[slot].unitA = runUUT[i].wLed[slot].iBack;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        runUUT[i].wLed[slot].iFailNum = 0;
                                    }
                                    runUUT[i].wLed[slot].iBack = runUUT[i].wLed[slot].unitA;
                                }
                                if (runUUT[i].wLed[slot].unitV < 2)
                                    runUUT[i].wLed[slot].unitA = 0;
                                //综合处理测试结果
                                if (uutPass)
                                {
                                    if (CSysPara.mVal.chkNoLockFail) //不锁住当机->可恢复为良品
                                    {
                                        runUUT[i].wLed[slot].passResult = 0;
                                        runUUT[i].wLed[slot].failResult = 0;
                                        runUUT[i].wLed[slot].failEnd = 0;
                                        runUUT[i].wLed[slot].failTime = "";
                                        runUUT[i].wLed[slot].failInfo = "";
                                    }
                                    else
                                        runUUT[i].wLed[slot].passResult = 0;
                                }
                                else
                                {
                                    runUUT[i].wLed[slot].failResult = 1;
                                    if (runUUT[i].wLed[slot].failEnd == 0)
                                    {
                                        runUUT[i].wLed[slot].failEnd = 1;
                                        runUUT[i].wLed[slot].failTime = DateTime.Now.ToString("HH:mm:ss");
                                        runUUT[i].wLed[slot].failInfo = failInfo;
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool UpdateDataBase(ref string er)
        {
            try
            {
                int waitTimes = System.Environment.TickCount;

                CDBCom db = new CDBCom(CDBCom.EDBType.Access, ".", sysDB);

                //修改测试状态
                string sqlCmd = "select UUTNO,doRun,RunTime,runStepNo,CtrlRunOnOff,CtrlUUTONLine,CtrlRunFlag,CtrlStartFlag," +
                                "CtrlBIFinishFlag,CtrlRunTime,CtrlOnOff_YXDH,CtrlOnOff_Cnt,CtrlRunError,CtrlRunStepNo,AlarmErr,AlarmInfo," +
                                "MTK_SPEC_OK" +
                               " from RUN_PARA order by UUTNO";
                DataTable dt = new DataTable("RUN_PARA");
                for (int i = 0; i < 17; i++)
                    dt.Columns.Add();
                for (int i = 0; i < runUUT.Count; i++)
                {
                    dt.Rows.Add(runUUT[i].wBase.uutNo, (int)runUUT[i].wPara.doRun, runUUT[i].wPara.runTime, runUUT[i].wPara.runQCStepNo,
                                runUUT[i].wPara.ctrlRunOnOff, runUUT[i].wPara.ctrlUUTONLine, runUUT[i].wPara.ctrlRunFlag, runUUT[i].wPara.ctrlStartFlag,
                                runUUT[i].wPara.ctrlBIFinishFlag, runUUT[i].wPara.ctrlRunTime, runUUT[i].wPara.ctrlOnOff_YXDH, runUUT[i].wPara.ctrlOnOff_Cnt,
                                runUUT[i].wPara.ctrlRunError, runUUT[i].wPara.ctrlQCStepNo, (int)runUUT[i].wPara.alarmErr, runUUT[i].wPara.alarmInfo,
                                runUUT[i].wPara.MTK_SPEC_OK);
                }
                if (!db.updateTableSQL(sqlCmd, dt, ref er))
                    return false;

                //修改测试数据
                sqlCmd = "select LEDNO,UnitV,UnitA,passResult,failResult,FailEnd,FailTime,FailStr from RUN_DATA order by LEDNO";
                dt = new DataTable("RUN_DATA");
                for (int i = 0; i < 8; i++)
                    dt.Columns.Add();
                int ledNo = 0;
                for (int i = 0; i < C_UUT_MAX; i++)
                {
                    for (int slot = 0; slot < C_SLOT_MAX; slot++)
                    {
                        ledNo++;
                        dt.Rows.Add(ledNo, runUUT[i].wLed[slot].unitV.ToString("0.000"), runUUT[i].wLed[slot].unitA.ToString("0.00"), runUUT[i].wLed[slot].passResult,
                                    runUUT[i].wLed[slot].failResult, runUUT[i].wLed[slot].failEnd, runUUT[i].wLed[slot].failTime, runUUT[i].wLed[slot].failInfo);

                    }
                }
                if (!db.updateTableSQL(sqlCmd, dt, ref er))
                    return false;

                string spanTimes = (System.Environment.TickCount - waitTimes).ToString() + "ms";

                runLog.Log("更新数据时间=" + spanTimes, udcRunLog.ELog.Action);

                //备份数据库
                if (g_DB_BAK_STARTTIMES == string.Empty)
                    g_DB_BAK_STARTTIMES = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                if (CMath.DifTime(g_DB_BAK_STARTTIMES) > g_DB_BAK_TIME)
                {
                    g_DB_BAK_STARTTIMES = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    Access.CompactDatabase(sysDB, true);
                    runLog.Log("压缩和备份测试数据库:" + Path.GetFileName(sysDB), udcRunLog.ELog.Action);
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 分配进出机任务
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool AssignInOutTask(ref string er)
        {
            try
            {
                if (runFix.handStat == EHandStat.忙碌)
                    runFix.InOuting = false;

                if (runFix.handStat != EHandStat.空闲)
                    return true;

                if (ThreadPLC.wREGVal[EPLCOUT.发送层列完成.ToString()] == -1)
                    return true;
                if (ThreadPLC.wREGVal[EPLCOUT.上层放治具层数.ToString()] != 0)
                    return true;
                if (ThreadPLC.wREGVal[EPLCOUT.上层放治具列数.ToString()] != 0)
                    return true;
                if (ThreadPLC.wREGVal[EPLCOUT.上层取治具层数.ToString()] != 0)
                    return true;
                if (ThreadPLC.wREGVal[EPLCOUT.上层取治具列数.ToString()] != 0)
                    return true;
                if (ThreadPLC.wREGVal[EPLCOUT.下层放治具层数.ToString()] != 0)
                    return true;
                if (ThreadPLC.wREGVal[EPLCOUT.下层放治具列数.ToString()] != 0)
                    return true;
                if (ThreadPLC.wREGVal[EPLCOUT.下层取治具层数.ToString()] != 0)
                    return true;
                if (ThreadPLC.wREGVal[EPLCOUT.下层取治具列数.ToString()] != 0)
                    return true;

                if (runFix.InOuting) //防止发送进出机命令异常
                {
                    if (System.Environment.TickCount - runFix.InOutTime < 20000)
                        return true;
                    runLog.Log(runFix.InOutOP.ToString() + "操作[" + runUUT[runFix.InOutPos - 1].wBase.localName +
                               "]异常超时,请检查", udcRunLog.ELog.NG);
                    runFix.InOuting = false;
                }

                bool getInOp = false;
                bool getOutOp = false;
                int getInPos = 0;
                int getOutPos = 0;
                runFix.InOutOP = EHandStat.空闲;
                runFix.InOutPos = -1;
                if (runFix.rInReady == 1 && runFix.inDoRun == EFixRun.进站)
                {
                    if (assignInPos(ref getInPos, ref er))
                        getInOp = true;
                }
                if (runFix.rOutReady == 1)
                {
                    if (assignOutPos(runFix.chkOutIsNull, ref getOutPos, ref er))
                        getOutOp = true;
                }
                if (getInOp && !getOutOp)      //单进机
                {
                    runFix.InOutOP = EHandStat.进机;
                    runFix.InOutPos = getInPos;
                    runFix.alternant = true;
                }
                else if (!getInOp && getOutOp) //单出机
                {
                    runFix.InOutOP = EHandStat.出机;
                    runFix.InOutPos = getOutPos;
                    runFix.alternant = false;
                }
                else if (getInOp && getOutOp) //交替进出机
                {
                    if (!runFix.alternant)
                    {
                        runFix.InOutOP = EHandStat.进机;
                        runFix.InOutPos = getInPos;
                        runFix.alternant = true;
                    }
                    else
                    {
                        runFix.InOutOP = EHandStat.出机;
                        runFix.InOutPos = getOutPos;
                        runFix.alternant = false;
                    }
                }
                if (runFix.InOutOP == EHandStat.进机 && CSysPara.mVal.chkHandIn)
                {
                    udcHandInPos handInDlg = new udcHandInPos();
                    if (handInDlg.ShowDialog() != DialogResult.OK)
                        runFix.InOutOP = EHandStat.空闲;
                    int uutNo = udcHandInPos.C_UUT_POS - 1;
                    if (runUUT[uutNo].wPara.ctrlUUTONLine == 1 ||
                        runUUT[uutNo].wPara.alarmErr != EAlarmCode.正常 ||
                        runUUT[uutNo + 1].wPara.ctrlUUTONLine == 1 ||
                        runUUT[uutNo + 1].wPara.alarmErr != EAlarmCode.正常)
                    {
                        runFix.InOutOP = EHandStat.空闲;
                    }
                    runFix.InOutPos = udcHandInPos.C_UUT_POS;
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 分配进机位置
        /// </summary>
        /// <returns></returns>
        private bool assignInPos(ref int inPos, ref string er)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);

                DataSet ds = new DataSet();

                string sqlCmd = "select * from RUN_PARA where AlarmErr=0 and doRun=0 and CtrlUUTONLine=0 and subNo=1" +
                                " order by UsedNum,UUTNO";

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                if (ds.Tables[0].Rows.Count == 0)
                    return false;

                inPos = System.Convert.ToInt32(ds.Tables[0].Rows[0]["UUTNO"]);

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 获取出机位置
        /// </summary>
        /// <param name="outPos"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool assignOutPos(bool chkIsNull, ref int outPos, ref string er)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);

                DataSet ds = null;

                string sqlCmd = string.Empty;

                //1.优先出空治具->治具1和治具2都是空治具
                if (chkIsNull)
                {
                    ds = new DataSet();

                    //sqlCmd = "select * from RUN_PARA where AlarmErr=0 and doRun=" + ((int)EDoRun.空治具到位).ToString() +
                    //         " and IsNull=3 and CtrlUUTONLine=1 and subNo=1" +
                    //         " order by StartTime,UUTNO";
                    sqlCmd = "select * from RUN_PARA where AlarmErr=0 and doRun=" + ((int)EDoRun.空治具到位).ToString() +
                             " and IsNull=3 and CtrlUUTONLine=1 and subNo=1" +
                             " order by UsedNum,UUTNO";
                    if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                        return false;
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        outPos = System.Convert.ToInt32(ds.Tables[0].Rows[0]["UUTNO"]);
                        return true;
                    }
                }

                //2.优先出治具1为老化结束,治具2为空治具

                ds = new DataSet();

                sqlCmd = "select * from RUN_PARA where AlarmErr=0 and doRun=" + ((int)EDoRun.老化结束).ToString() +
                         " and IsNull=2 and CtrlUUTONLine=1 and subNo=1" +
                         " order by StartTime,UUTNO";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                if (ds.Tables[0].Rows.Count != 0)
                {
                    outPos = System.Convert.ToInt32(ds.Tables[0].Rows[0]["UUTNO"]);
                    return true;
                }

                //3.优先出治具1为空治具,治具2为老化结束


                ds = new DataSet();

                sqlCmd = "select * from RUN_PARA where AlarmErr=0 and doRun=" + ((int)EDoRun.老化结束).ToString() +
                         " and IsNull=1 and CtrlUUTONLine=1 and subNo=2" +
                         " order by StartTime,UUTNO";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                if (ds.Tables[0].Rows.Count != 0)
                {
                    outPos = System.Convert.ToInt32(ds.Tables[0].Rows[0]["UUTNO"]) - 1;
                    return true;
                }

                //4.治具1为老化结束,治具2为老化结束

                ds = new DataSet();

                sqlCmd = "select * from RUN_PARA where AlarmErr=0 and doRun=" + ((int)EDoRun.老化结束).ToString() +
                         " and IsNull=0 and CtrlUUTONLine=1 and subNo=1" +
                         " order by StartTime,UUTNO";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                if (ds.Tables[0].Rows.Count != 0)
                {
                    outPos = System.Convert.ToInt32(ds.Tables[0].Rows[0]["UUTNO"]);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 检查槽位状态
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool CheckSlot(ref string er)
        {
            try
            {
                for (int i = 0; i < C_UUT_MAX; i++)
                {
                    int icom = runUUT[i].wBase.ctrlCom;
                    int iAddr = runUUT[i].wBase.ctrlAddr - 1;
                    int ersCom = runUUT[i].wBase.ersCom;
                    int ersAddr = runUUT[i].wBase.ersAddr - 1;

                    if (ThreadMon[icom] == null)
                        return true;

                    //检查控制板通信状态
                    if (!ThreadMon[icom].mon[iAddr].rData.conOK)
                    {
                        if (runUUT[i].wPara.alarmErr != EAlarmCode.控制板通信异常)
                        {
                            if (runUUT[i].wPara.alarmTimes < C_MON_ALARM_TIME)
                                runUUT[i].wPara.alarmTimes++;
                            else
                                runUUT[i].wPara.alarmErr = EAlarmCode.控制板通信异常;
                        }
                        continue;
                    }
                    if (ThreadERS[ersCom] == null)
                        return true;

                    //检查ERS通信状态
                    if (!ThreadERS[ersCom].mon[ersAddr].rData.conOK)
                    {
                        if (runUUT[i].wPara.alarmErr != EAlarmCode.ERS通信异常)
                        {
                            if (runUUT[i].wPara.alarmTimes < C_MON_ALARM_TIME)
                                runUUT[i].wPara.alarmTimes++;
                            else
                                runUUT[i].wPara.alarmErr = EAlarmCode.ERS通信异常;
                        }
                        continue;
                    }
                    //检测到位信号-->无子治具但有到位信号
                    if (runUUT[i].wPara.doRun == EDoRun.位置空闲)
                    {
                        if (runUUT[i].wPara.ctrlUUTONLine == 1)
                        {
                            if (runUUT[i].wPara.alarmErr != EAlarmCode.无治具有到位信号)
                            {
                                if (runUUT[i].wPara.alarmTimes < C_MON_ALARM_TIME)
                                    runUUT[i].wPara.alarmTimes++;
                                else
                                    runUUT[i].wPara.alarmErr = EAlarmCode.无治具有到位信号;
                            }
                            continue;
                        }
                    }
                    //有子治具但没有到位信号
                    if (runUUT[i].wPara.doRun > EDoRun.正在进机 && runUUT[i].wPara.doRun < EDoRun.正在出机)
                    {
                        if (runUUT[i].wPara.ctrlUUTONLine == 0)
                        {
                            if (runUUT[i].wPara.alarmErr != EAlarmCode.有治具无到位信号)
                            {
                                if (runUUT[i].wPara.alarmTimes < C_MON_ALARM_TIME)
                                    runUUT[i].wPara.alarmTimes++;
                                else
                                    runUUT[i].wPara.alarmErr = EAlarmCode.有治具无到位信号;
                            }
                            continue;
                        }
                    }
                    //位置禁用
                    if (runUUT[i].wPara.doRun == EDoRun.位置禁用)
                    {
                        if (runUUT[i].wPara.alarmErr != EAlarmCode.位置禁用)
                        {
                            runUUT[i].wPara.alarmErr = EAlarmCode.位置禁用;
                            ThreadMon[runUUT[i].wBase.ctrlCom].setForbit(runUUT[i].wBase.ctrlAddr, ref er, true);
                            continue;
                        }
                    }
                    //针盘使用寿命
                    if (runUUT[i].wPara.usedNum > CSysPara.mVal.fixTimes)
                    {
                        if (runUUT[i].wPara.alarmErr != EAlarmCode.针盘使用寿命已到)
                        {
                            runUUT[i].wPara.alarmErr = EAlarmCode.针盘使用寿命已到;
                            continue;
                        }
                    }
                    //获取控制板状态                   
                    runUUT[i].wPara.alarmErr = (EAlarmCode)runUUT[i].wPara.ctrlRunError;
                    if (runUUT[i].wPara.alarmErr != EAlarmCode.正常)
                        continue;

                    //检测控制板启动异常->检测子治具01状态
                    bool breStart = false;
                    int runTime = 0;
                    if (i % 2 == 0)
                    {
                        if (runUUT[i].wPara.doRun == EDoRun.启动老化 ||
                            runUUT[i].wPara.doRun == EDoRun.老化自检 ||
                            runUUT[i].wPara.doRun == EDoRun.正在老化 ||
                            runUUT[i + 1].wPara.doRun == EDoRun.启动老化 ||
                            runUUT[i + 1].wPara.doRun == EDoRun.老化自检 ||
                            runUUT[i + 1].wPara.doRun == EDoRun.正在老化)
                        {
                            if (runUUT[i].wPara.ctrlStartFlag == 4 && runUUT[i].wPara.ctrlBIFinishFlag == 0)//控制板未能成功启动老化
                            {
                                if (runUUT[i].wPara.alarmTimes < C_MON_ALARM_TIME)
                                {
                                    runUUT[i].wPara.alarmTimes++;
                                    continue;
                                }
                                breStart = true;
                            }
                            else if (runUUT[i].wPara.ctrlStartFlag == 0 && runUUT[i].wPara.ctrlBIFinishFlag == 0)//更换控制板继续老化测试
                            {
                                if (runUUT[i].wPara.alarmTimes < C_MON_ALARM_TIME)
                                {
                                    runUUT[i].wPara.alarmTimes++;
                                    continue;
                                }
                                runTime = runUUT[i].wPara.runTime;
                                breStart = true;
                            }
                            else if (runUUT[i].wPara.ctrlStartFlag == 0 && runUUT[i].wPara.ctrlBIFinishFlag == 1)//控制板断电继续老化测试
                            {
                                if (runUUT[i].wPara.alarmTimes < C_MON_ALARM_TIME)
                                {
                                    runUUT[i].wPara.alarmTimes++;
                                    continue;
                                }
                                runTime = runUUT[i].wPara.runTime;
                                breStart = true;
                            }
                            else if (runUUT[i].wPara.ctrlStartFlag == 6) //继续老化
                            {
                                if (runUUT[i].wPara.alarmTimes < C_MON_ALARM_TIME)
                                {
                                    runUUT[i].wPara.alarmTimes++;
                                    continue;
                                }
                                ThreadMon[runUUT[i].wBase.ctrlCom].setContinue(runUUT[i].wBase.ctrlAddr, ref er, 1);
                                runUUT[i].wPara.alarmTimes = 0;
                            }
                        }
                    }
                    runUUT[i].wPara.alarmTimes = 0;
                    if (breStart)
                    {
                        int ctrlCom = runUUT[i].wBase.ctrlCom;
                        int ctrlAddr = runUUT[i].wBase.ctrlAddr;
                        CWriteRunPara runPara = new CWriteRunPara();
                        runPara.onoff_Cnt = 0;
                        runPara.onoff_YXDH = 1;
                        runPara.onoff_Flag = 1;
                        runPara.secMinCnt = 0;
                        runPara.runTolTime = runTime / 60;
                        runPara.onoff_RunTime = runTime % 60;
                        runPara.runTypeFlag = runUUT[i].wPara.runFlag;
                        runPara.startFlag = 1;
                        ThreadMon[ctrlCom].setRun(ctrlAddr, runPara, ref er);
                        continue;
                    }
                    runUUT[i].wPara.alarmTimes = 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
         /// <summary>修改治具测试状态
    /// 修改治具测试状态
    /// </summary>
    /// <param name="er"></param>
    /// <returns></returns>
        private bool UpdateSlot(ref string er)
        {
            try
            {
                for (int i = 0; i < C_UUT_MAX; i++)
                {
                    string localName = "<" + runUUT[i].wBase.localName + ">";

                    runUUT[i].wPara.alarmInfo = string.Empty;

                    if (runUUT[i].wPara.alarmErr == EAlarmCode.正常)
                    {
                        switch (runUUT[i].wPara.doRun)
                        {
                            case EDoRun.位置禁用:
                                break;
                            case EDoRun.位置空闲:
                                if (runFix.InOutOP == EHandStat.进机 && runFix.InOutPos == i + 1) //当前位置进机
                                {
                                    if (i % 2 != 0)
                                    {
                                        runUUT[i].wPara.doRun = EDoRun.异常报警;
                                        runUUT[i].wPara.alarmInfo = "进机错误";
                                        runLog.Log(localName + "获取进机位置错误:该位置不能进机", udcRunLog.ELog.NG);
                                        break;
                                    }
                              
                                    if (ThreadPLC.rREGVal[EPLCINP.老化入口平台准备OK.ToString()] == CPLCCom.ON)
                                    {

                                        //if (runFix.inDoRun == EFixRun.报警 && ThreadPLC.rREGVal[EPLCINP.老化出口处平台空闲.ToString()] == CPLCCom.ON)//如果进机错误，则拉到出口位置
                                        //{
                                        //}
                                        if (!setFixIntoBI(i, ref er))
                                        {
                                            runUUT[i].wPara.doRun = EDoRun.异常报警;
                                            runUUT[i].wPara.alarmInfo = "进机异常";
                                            runLog.Log(localName + "进机异常:" + er, udcRunLog.ELog.NG);
                                            break;
                                        }
                                        updateInPosUI(runUUT[i].wBase.localName);
                                        runUUT[i].wPara.doRun = EDoRun.正在进机;
                                        runUUT[i + 1].wPara.doRun = EDoRun.正在进机;
                                        setTimer(i, "治具[" + runUUT[i].wPara.idCard + "]正在进机");
                                        setTimer(i + 1, "治具[" + runUUT[i + 1].wPara.idCard + "]正在进机");
                                    }
                                }
                                break;
                            case EDoRun.正在进机:
                                if (runUUT[i].wPara.ctrlUUTONLine == 0)
                                {
                                    if (!readTimerOut(i))
                                        runUUT[i].wPara.alarmInfo = "进机超时";
                                    break;
                                }
                                runUUT[i].wPara.doRun = EDoRun.进机完毕;
                                break;
                            case EDoRun.进机完毕:
                                if (i % 2 != 0) //偶数子治具不启动老化
                                    break;
                                if (runUUT[i + 1].wPara.doRun == EDoRun.进机完毕)
                                {
                                    runUUT[i].wPara.runQCStepNo = 0;
                                    runUUT[i].wPara.runQCWaitTimes = 0;
                                    runUUT[i].wPara.MTK_SPEC_OK = 0;
                                    runUUT[i].wPara.MTK_RUNNING = false;
                                    runUUT[i].wPara.MTK_STEP_NO = 0;
                                    runUUT[i + 1].wPara.runQCStepNo = 0;
                                    runUUT[i + 1].wPara.runQCWaitTimes = 0;
                                    runUUT[i + 1].wPara.MTK_SPEC_OK = 0;
                                    runUUT[i + 1].wPara.MTK_RUNNING = false;
                                    runUUT[i + 1].wPara.MTK_STEP_NO = 0;
                                    if (!setFixLoadVal(i, true, ref er))
                                    {
                                        runUUT[i].wPara.doRun = EDoRun.异常报警;
                                        runUUT[i].wPara.alarmInfo = "设置负载";
                                        runLog.Log(localName + "设置负载异常:" + er, udcRunLog.ELog.NG);
                                        break;
                                    }
                                
                                    if (runUUT[i].wPara.isNull == 1 || runUUT[i].wPara.isNull == 3)
                                    {
                                        runUUT[i].wPara.doRun = EDoRun.空治具到位;
                                        setTimer(i, "空治具到位");
                                    }
                                    else
                                    {
                                        if (runUUT[i].wPara.QCTYPE != EQCV.MTK)
                                            runUUT[i].wPara.ResetQCVoltTimes = CSysPara.mVal.C_RESET_QCV_TIMES;
                                        else
                                            runUUT[i].wPara.ResetQCVoltTimes = 0;
                                        if (!setFixStartBI(i, ref er))
                                        {
                                            runUUT[i].wPara.doRun = EDoRun.异常报警;
                                            runUUT[i].wPara.alarmInfo = "启动异常";
                                            runLog.Log(localName + "启动老化测试异常:" + er, udcRunLog.ELog.NG);
                                            break;
                                        }
                                        runUUT[i].wPara.doRun = EDoRun.启动老化;
                                     
                                        setTimer(i, "启动老化");
                                    }
                                    if (runUUT[i + 1].wPara.isNull == 2 || runUUT[i].wPara.isNull == 3)
                                    {
                                        runUUT[i + 1].wPara.doRun = EDoRun.空治具到位;
                                        setTimer(i + 1, "空治具到位");
                                    }
                                    else
                                    {
                                        if (runUUT[i + 1].wPara.QCTYPE != EQCV.MTK)
                                            runUUT[i + 1].wPara.ResetQCVoltTimes = CSysPara.mVal.C_RESET_QCV_TIMES;
                                        else
                                            runUUT[i + 1].wPara.ResetQCVoltTimes = 0;

                                        if (!setFixStartBI(i, ref er))
                                        {
                                            runUUT[i].wPara.doRun = EDoRun.异常报警;
                                            runUUT[i].wPara.alarmInfo = "启动异常";
                                            runLog.Log(localName + "启动老化测试异常:" + er, udcRunLog.ELog.NG);
                                            break;
                                        }
                                        runUUT[i + 1].wPara.doRun = EDoRun.启动老化;
                                        setTimer(i + 1, "启动老化");
                                    }
                                    setFixUsedNum(i, ref er);
                                    updateInBIFixNum(2);
                                }
                                break;
                            case EDoRun.启动老化:
                                if (runUUT[i].wPara.ctrlStartFlag == 2 || runUUT[i].wPara.ctrlStartFlag == 3)
                                {
                                    runUUT[i].wPara.doRun = EDoRun.老化自检;
                                    setTimer(i, "等待老化自检");
                                }
                                else
                                {
                                    if (!readTimerOut(i, "启动老化超时"))
                                        runUUT[i].wPara.alarmInfo = "启动超时";
                                }
                                break;
                            case EDoRun.老化自检:
                                if (runUUT[i].wPara.ctrlStartFlag == 3)
                                {
                                    if (!setFixBIBegin(i, ref er))
                                    {
                                        runUUT[i].wPara.doRun = EDoRun.异常报警;
                                        runUUT[i].wPara.alarmInfo = "时间异常";
                                        runLog.Log(localName + "设置老化开始时间错误:" + er, udcRunLog.ELog.NG);
                                        break;
                                    }
                                    runUUT[i].wPara.doRun = EDoRun.正在老化;
                                    setTimer(i, "开始老化测试");
                                    setFixLoadVal(i, false, ref er);
                                }
                                else
                                {
                                    if (!readTimerOut(i, "老化自检超时"))
                                        runUUT[i].wPara.alarmInfo = "自检超时";
                                }
                                break;
                            case EDoRun.正在老化:
                                if (runUUT[i].wPara.ctrlStartFlag == 4 && runUUT[i].wPara.ctrlBIFinishFlag == 0)
                                {
                                    if (!setFixBIEnd(i, ref er))
                                    {
                                        runUUT[i].wPara.doRun = EDoRun.异常报警;
                                        runUUT[i].wPara.alarmInfo = "结束异常";
                                        runLog.Log(localName + "设置老化结束异常:" + er, udcRunLog.ELog.NG);
                                        break;
                                    }
                                   int icom = runUUT[i].wBase.ctrlCom;
                                   int iAddr = runUUT[i].wBase.ctrlAddr;
                                   ThreadMon[icom].setStop(iAddr, ref er);
                                    runUUT[i].wPara.doRun = EDoRun.老化结束;
                                    runLog.Log(localName + "老化结束,等待出机.", udcRunLog.ELog.Action);
                                }
                                break;
                            case EDoRun.老化结束:
                                if (i % 2 != 0) //偶数子治具不启动老化
                                    break;
                                if (runUUT[i + 1].wPara.doRun == EDoRun.老化结束 || runUUT[i + 1].wPara.doRun == EDoRun.空治具到位)
                                {
                                    if (runFix.InOutOP == EHandStat.出机 && runFix.InOutPos == i + 1) //当前位置出机
                                    {
                                        if (!setFixOutFromBI(i, ref er))
                                        {
                                            runUUT[i].wPara.doRun = EDoRun.异常报警;
                                            runUUT[i].wPara.alarmInfo = "出机异常";
                                            runLog.Log(localName + "出机异常:" + er, udcRunLog.ELog.NG);
                                            break;
                                        }
                                        updateOutPosUI(runUUT[i].wBase.localName);
                                        updateOutBIFixNum(2);
                                        runUUT[i].wPara.isNull = 0;
                                        runUUT[i].wPara.runQCStepNo = 0;
                                        runUUT[i].wPara.doRun = EDoRun.正在出机;
                                        runUUT[i + 1].wPara.isNull = 0;
                                        runUUT[i + 1].wPara.runQCStepNo = 0;
                                        runUUT[i + 1].wPara.doRun = EDoRun.正在出机;
                                        setTimer(i, "正在出机中");
                                        setTimer(i + 1, "正在出机中");
                                    }
                                }
                                break;
                            case EDoRun.正在出机:
                                if (runUUT[i].wPara.ctrlUUTONLine == 0)
                                {
                                    runUUT[i].wPara.doRun = EDoRun.位置空闲;
                                    setTimer(i, "出机完毕,等待下一治具");
                                }
                                else
                                {
                                    if (!readTimerOut(i, "出机超时"))
                                        runUUT[i].wPara.alarmInfo = "出机超时";

                                }
                                break;
                            case EDoRun.空治具到位:
                                if (i % 2 != 0) //偶数子治具不启动老化
                                    break;
                                if (runFix.InOutOP == EHandStat.出机 && runFix.InOutPos == i + 1) //当前位置出机
                                {
                                    if (!setFixOutFromBI(i, ref er))
                                    {
                                        runUUT[i].wPara.doRun = EDoRun.异常报警;
                                        runUUT[i].wPara.alarmInfo = "出机异常";
                                        runLog.Log(localName + "出机异常:" + er, udcRunLog.ELog.NG);
                                        break;
                                    }
                                    updateOutPosUI(runUUT[i].wBase.localName);
                                    updateOutBIFixNum(2);
                                    runUUT[i].wPara.isNull = 0;
                                    runUUT[i].wPara.runQCStepNo = 0;
                                    runUUT[i].wPara.doRun = EDoRun.正在出机;
                                    runUUT[i + 1].wPara.isNull = 0;
                                    runUUT[i + 1].wPara.runQCStepNo = 0;
                                    runUUT[i + 1].wPara.doRun = EDoRun.正在出机;
                                    setTimer(i, "正在出机中");
                                    setTimer(i + 1, "正在出机中");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    udcUUT[i].SetRunStatus(runUUT[i]);
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        } 
        /// <summary>
        /// 修改母治具状态
        /// </summary>
        /// <param name="uutNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool updateMontherInfo(int uutNo, ref string er)
        {
            try
            {
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);

                List<string> sqlCmdList = new List<string>();

                string sqlCmd = string.Empty;

                for (int subNo = 0; subNo < 2; subNo++)
                {

                    string OnOffSpec = string.Empty;

                    for (int step = 0; step < runUUT[uutNo + subNo].wOnOff.C_ONOFF_MAX; step++)
                    {
                        OnOffSpec += runUUT[uutNo + subNo].wOnOff.ACV[step] + ",";
                        OnOffSpec += runUUT[uutNo + subNo].wOnOff.OnOffTimes[step] + ",";
                        OnOffSpec += runUUT[uutNo + subNo].wOnOff.OnTime[step] + ",";
                        OnOffSpec += runUUT[uutNo + subNo].wOnOff.OffTime[step] + ",";
                        if (step != runUUT[uutNo + subNo].wOnOff.C_ONOFF_MAX - 1)
                            OnOffSpec += runUUT[uutNo + subNo].wOnOff.QCV[step] + ";";
                        else
                            OnOffSpec += runUUT[uutNo + subNo].wOnOff.QCV[step];
                    }

                    sqlCmd = "update RUN_PARA Set IDCard='" + runUUT[uutNo + subNo].wPara.idCard + "',ModelName='" +
                            runUUT[uutNo + subNo].wPara.modelName + "',IsNull=" + runUUT[uutNo + subNo].wPara.isNull +
                            ",RunFlag=" + runUUT[uutNo + subNo].wPara.runFlag + ",runStepNo=0,BurnTime=" +
                             runUUT[uutNo + subNo].wPara.burnTime + ",RunTime=0" +
                            ",wOnOff='" + OnOffSpec + "',QCTYPE=" + ((int)runUUT[uutNo + subNo].wPara.QCTYPE).ToString() +
                            " where UUTNO=" + (uutNo + subNo + 1);
                    sqlCmdList.Add(sqlCmd);

                    for (int slot = 0; slot < C_SLOT_MAX; slot++)
                    {
                        string specVal = string.Empty;

                        for (int CH = 0; CH < 4; CH++)
                        {
                            specVal += "Vname" + CH.ToString() + "='" + runUUT[uutNo + subNo].wLed[slot].vName[CH] + "',";
                            specVal += "Vmin" + CH.ToString() + "=" + runUUT[uutNo + subNo].wLed[slot].vMin[CH].ToString() + ",";
                            specVal += "Vmax" + CH.ToString() + "=" + runUUT[uutNo + subNo].wLed[slot].vMax[CH].ToString() + ",";
                            specVal += "ISET" + CH.ToString() + "=" + runUUT[uutNo + subNo].wLed[slot].iSet[CH].ToString() + ",";
                            specVal += "IMin" + CH.ToString() + "=" + runUUT[uutNo + subNo].wLed[slot].iMin[CH].ToString() + ",";
                            specVal += "IMax" + CH.ToString() + "=" + runUUT[uutNo + subNo].wLed[slot].iMax[CH].ToString() + ",";
                            specVal += "QCV" + CH.ToString() + "=" + runUUT[uutNo + subNo].wLed[slot].QCV[CH].ToString() + ",";
                        }

                        sqlCmd = "update RUN_DATA set SerialNo='" + runUUT[uutNo + subNo].wLed[slot].serialNo + "'," +
                                 specVal + "UnitV=" + runUUT[uutNo + subNo].wLed[slot].unitV.ToString() + "," +
                                 "UnitA=" + runUUT[uutNo + subNo].wLed[slot].unitA.ToString() +
                                 ",passResult=0,failResult=0,FailEnd=0,FailTime='',FailStr=''" +
                                 " where UUTNO=" + (uutNo + subNo + 1).ToString() + " and SlotNo=" + (slot + 1).ToString();
                        sqlCmdList.Add(sqlCmd);
                    }
                }
                if (!db.excuteSQLArray(sqlCmdList, ref er))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置进机槽位状态
        /// </summary>
        /// <param name="uutNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setFixIntoBI(int uutNo, ref string er)
        {
            try
            {
                er = string.Empty;

                //更新进机状态

                for (int subNo = 0; subNo < 2; subNo++)
                {
                    runUUT[uutNo + subNo].wPara.modelName = runModel.model;
                    runUUT[uutNo + subNo].wPara.idCard = runFix.inFix[subNo].idCard;
                    runUUT[uutNo + subNo].wPara.isNull = runFix.inFix[subNo].IsFixNull;
                    runUUT[uutNo + subNo].wPara.runQCStepNo = 0;
                    runUUT[uutNo + subNo].wPara.burnTime = (int)(runModel.BITime * 3600);
                    runUUT[uutNo + subNo].wPara.runTime = 0;
                    runUUT[uutNo + subNo].wPara.QCTYPE = runModel.QC_TYPE;

                    for (int step = 0; step < runUUT[uutNo + subNo].wOnOff.C_ONOFF_MAX; step++)
                    {
                        runUUT[uutNo + subNo].wOnOff.ACV[step] = runModel.ACV;
                        runUUT[uutNo + subNo].wOnOff.OnOffTimes[step] = 0;
                        runUUT[uutNo + subNo].wOnOff.OnTime[step] = 0;
                        runUUT[uutNo + subNo].wOnOff.OffTime[step] = 0;
                        runUUT[uutNo + subNo].wOnOff.QCV[step] = 0;
                    }
                    for (int step = 0; step < runModel.OnOffList.Count; step++)
                    {
                        runUUT[uutNo + subNo].wOnOff.OnOffTimes[step] = runModel.OnOffList[step].OnOffTime;
                        runUUT[uutNo + subNo].wOnOff.OnTime[step] = runModel.OnOffList[step].OnTime;
                        runUUT[uutNo + subNo].wOnOff.OffTime[step] = runModel.OnOffList[step].OffTime;
                        runUUT[uutNo + subNo].wOnOff.QCV[step] = runModel.OnOffList[step].QC_VOLT;
                    }

                    for (int slot = 0; slot < C_SLOT_MAX; slot++)
                    {
                        runUUT[uutNo + subNo].wLed[slot].serialNo = runFix.inFix[subNo].serialNo[slot];
                        runUUT[uutNo + subNo].wLed[slot].passResult = 0;
                        runUUT[uutNo + subNo].wLed[slot].failResult = 0;
                        runUUT[uutNo + subNo].wLed[slot].vFailNum = 0;
                        runUUT[uutNo + subNo].wLed[slot].iFailNum = 0;
                        runUUT[uutNo + subNo].wLed[slot].failEnd = 0;
                        runUUT[uutNo + subNo].wLed[slot].failTime = "";
                        runUUT[uutNo + subNo].wLed[slot].failInfo = "";
                        runUUT[uutNo + subNo].wLed[slot].unitV = runModel.DCVList[0].Vmin + 0.5;
                        runUUT[uutNo + subNo].wLed[slot].unitA = runModel.DCVList[0].ISet;
                        runUUT[uutNo + subNo].wLed[slot].vBack = runModel.DCVList[0].Vmin + 0.5;
                        runUUT[uutNo + subNo].wLed[slot].iBack = runModel.DCVList[0].ISet;
                        for (int CH = 0; CH < 4; CH++)
                            runUUT[uutNo + subNo].wLed[slot].vName[CH] = string.Empty;
                        for (int CH = 0; CH < runModel.DCVList.Count; CH++)
                        {
                            runUUT[uutNo + subNo].wLed[slot].vName[CH] = runModel.DCVList[CH].Vname;
                            runUUT[uutNo + subNo].wLed[slot].vMax[CH] = runModel.DCVList[CH].Vmax;
                            runUUT[uutNo + subNo].wLed[slot].vMin[CH] = runModel.DCVList[CH].Vmin;
                            runUUT[uutNo + subNo].wLed[slot].iSet[CH] = runModel.DCVList[CH].ISet;
                            runUUT[uutNo + subNo].wLed[slot].iMax[CH] = runModel.DCVList[CH].Imax;
                            runUUT[uutNo + subNo].wLed[slot].iMin[CH] = runModel.DCVList[CH].Imin;
                            runUUT[uutNo + subNo].wLed[slot].QCV[CH] = runModel.DCVList[CH].QC_VOLT;
                        }
                    }
                }
                if (runFix.inFix[0].IsFixNull == 1 && runFix.inFix[1].IsFixNull == 1) //治具1为空治具,治具2为空治具
                {
                    runUUT[uutNo].wPara.runFlag = 4;
                    runUUT[uutNo + 1].wPara.runFlag = 4;
                    runUUT[uutNo].wPara.isNull = 3;
                    runUUT[uutNo + 1].wPara.isNull = 3;
                }
                else if (runFix.inFix[0].IsFixNull == 0 && runFix.inFix[1].IsFixNull == 0) //治具1为治具,治具2为治具
                {
                    runUUT[uutNo].wPara.runFlag = 3;
                    runUUT[uutNo + 1].wPara.runFlag = 3;
                    runUUT[uutNo].wPara.isNull = 0;
                    runUUT[uutNo + 1].wPara.isNull = 0;
                }
                else if (runFix.inFix[0].IsFixNull == 1 && runFix.inFix[1].IsFixNull == 0) //治具1为空治具,治具2为治具
                {
                    runUUT[uutNo].wPara.runFlag = 2;
                    runUUT[uutNo + 1].wPara.runFlag = 2;
                    runUUT[uutNo].wPara.isNull = 1;
                    runUUT[uutNo + 1].wPara.isNull = 1;
                }
                else if (runFix.inFix[0].IsFixNull == 0 && runFix.inFix[1].IsFixNull == 1) //治具1为治具,治具2为空治具
                {
                    runUUT[uutNo].wPara.runFlag = 1;
                    runUUT[uutNo + 1].wPara.runFlag = 1;
                    runUUT[uutNo].wPara.isNull = 2;
                    runUUT[uutNo + 1].wPara.isNull = 2;
                }
                if (!updateMontherInfo(uutNo, ref er))
                    return false; 
                //设置ONOFF参数
                if (!setFixOnOff(uutNo, ref er))
                    return false;
                //设置负载
                if (!setFixLoadVal(uutNo, true, ref er))
                    return false;
                //发进机命令L1-L5===>L1-L10
                int iRow = runUUT[uutNo].wBase.iRow*2-1;
                int iCol = runUUT[uutNo].wBase.iCol;
                //取位置
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具层数.ToString(), 29);
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具列数.ToString(), 30);
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具层数.ToString(), 30);
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具列数.ToString(), 30);
                //放位置
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具层数.ToString(), iRow);
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具列数.ToString(), iCol);
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具层数.ToString(), iRow+1);
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具列数.ToString(), iCol);
                //取位置->重发
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具层数.ToString(), 29);
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具列数.ToString(), 30);
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具层数.ToString(), 30);
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具列数.ToString(), 30);
                //放位置->重发
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具层数.ToString(), iRow);
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具列数.ToString(), iCol);
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具层数.ToString(), iRow+1);
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具列数.ToString(), iCol);

                ThreadPLC.addREGWrite(EPLCOUT.发送层列完成.ToString(), 1);

                runFix.InOutTime = System.Environment.TickCount;
                runFix.InOuting = true;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置治具出机
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setFixOutFromBI(int uutNo, ref string er)
        {
            try
            {
                int iRow = runUUT[uutNo].wBase.iRow*2-1;
                int iCol = runUUT[uutNo].wBase.iCol;
                //治具1出机位置
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具层数.ToString(), iRow);
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具列数.ToString(), iCol);
                //治具2出机位置
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具层数.ToString(), iRow+1);
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具列数.ToString(), iCol);
                //治具1放机位置
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具层数.ToString(), 31);
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具列数.ToString(), 31);
                //治具2放机位置
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具层数.ToString(), 32);
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具列数.ToString(), 31);
                //治具1出机位置->重发
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具层数.ToString(), iRow);
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具列数.ToString(), iCol);
                //治具2出机位置->重发
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具层数.ToString(), iRow+1);
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具列数.ToString(), iCol);
                //治具1放机位置->重发
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具层数.ToString(), 31);
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具列数.ToString(), 31);
                //治具2出机位置->重发
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具层数.ToString(), 32);
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具列数.ToString(), 31);
                //PC发送机械手取放坐标完成
                ThreadPLC.addREGWrite(EPLCOUT.发送层列完成.ToString(), 1);
                runFix.InOuting = true;
                runFix.InOutTime = System.Environment.TickCount;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 进指定位置
        /// </summary>
        /// <param name="outUUTNo"></param>
        /// <param name="inUUTNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setFixOutIntoBI(int outUUTNo, int inUUTNo, ref string er)
        {
            try
            {
                int iRow = runUUT[outUUTNo].wBase.iRow*2-1;
                int iCol = runUUT[outUUTNo].wBase.iCol;
                //治具1出机位置
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具层数.ToString(), iRow);
                ThreadPLC.addREGWrite(EPLCOUT.上层取治具列数.ToString(), iCol);
                //治具2出机位置
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具层数.ToString(), iRow+1);
                ThreadPLC.addREGWrite(EPLCOUT.下层取治具列数.ToString(), iCol);
                iRow = runUUT[inUUTNo].wBase.iRow;
                iCol = runUUT[inUUTNo].wBase.iCol;
                //治具1放机位置
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具层数.ToString(), iRow);
                ThreadPLC.addREGWrite(EPLCOUT.上层放治具列数.ToString(), iCol);
                //治具2放机位置
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具层数.ToString(), iRow+1);
                ThreadPLC.addREGWrite(EPLCOUT.下层放治具列数.ToString(), iCol);
                //PC发送机械手取放坐标完成
                ThreadPLC.addREGWrite(EPLCOUT.发送层列完成.ToString(), 1);
                runFix.InOuting = true;
                runFix.InOutTime = System.Environment.TickCount;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置治具负载
        /// </summary>
        /// <param name="uutNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setFixLoadVal(int uutNo, bool clrLoad, ref string er)
        {
            try
            {
                ////解析对应输出电压电流规格通道
                int stepNo = 0;
                for (int CH = 0; CH < runUUT[uutNo].wLed[0].vName.Count; CH++)
                {
                    if (runUUT[uutNo].wPara.runQCStepNo == runUUT[uutNo].wLed[0].QCV[CH])
                    {
                        stepNo = CH;
                        break;
                    }
                }
                int ersCom = runUUT[uutNo].wBase.ersCom;
                int ersAddr = runUUT[uutNo].wBase.ersAddr;
                int ersCH = runUUT[uutNo].wBase.ersCH;
                double loadVal = runUUT[uutNo].wLed[0].iSet[stepNo];
                //计算输出功率>45W->处理电流值
                double Power = runUUT[uutNo].wLed[0].iSet[stepNo] * runUUT[uutNo].wLed[0].vMax[stepNo];
                if (CSysPara.mVal.PwrLimit>0 && Power > CSysPara.mVal.PwrLimit)
                    loadVal = loadVal - CSysPara.mVal.IOFFSET;
                if (runUUT[uutNo].wPara.QCTYPE != EQCV.MTK && !CSysPara.mVal.comLoad && clrLoad)
                    loadVal = 0;
             //   return ThreadERS[ersCom].setCHLoad(ersAddr, ersCH, loadVal, ref er);
                return ThreadERS[ersCom].setCHLoad(ersAddr, ersCH, loadVal/2, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置治具OnOff参数
        /// </summary>
        /// <param name="uutNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setFixOnOff(int uutNo, ref string er)
        {
            try
            {
                int ctrlCom = runUUT[uutNo].wBase.ctrlCom;
                int ctrlAddr = runUUT[uutNo].wBase.ctrlAddr;

                COnOffPara onoffPara = new COnOffPara();

                onoffPara.BIToTime = (int)(runModel.BITime * 60);

                onoffPara.wQCType = (int)runModel.QC_TYPE;

                for (int i = 0; i < 4; i++)
                {
                    onoffPara.wOnOff[i] =0;
                    onoffPara.wON[i] = 0;
                    onoffPara.wOFF[i] = 0;
                    onoffPara.wQCVolt[i] = 0;
                }

                for (int i = 0; i < runModel.OnOffList.Count; i++)
                {
                    onoffPara.wOnOff[i] = runModel.OnOffList[i].OnOffTime;
                    onoffPara.wON[i] = runModel.OnOffList[i].OnTime;
                    onoffPara.wOFF[i] = runModel.OnOffList[i].OffTime;
                    onoffPara.wQCVolt[i] = runModel.OnOffList[i].QC_VOLT;
                }
                //设置ON/OFF
                if (!ThreadMon[ctrlCom].setOnOff(ctrlAddr, onoffPara, ref er))
                    return false;
                //设置快充模式
                if (!ThreadMon[ctrlCom].setQCRunPara(ctrlAddr, onoffPara, ref er))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 启动老化测试
        /// </summary>
        /// <param name="uutNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setFixStartBI(int uutNo, ref string er)
        {
            try
            {
                int ctrlCom = runUUT[uutNo].wBase.ctrlCom;
                int ctrlAddr = runUUT[uutNo].wBase.ctrlAddr;
                CWriteRunPara runPara = new CWriteRunPara();
                runPara.onoff_Cnt = 0;
                runPara.onoff_YXDH = 1;
                runPara.onoff_Flag = 1;
                runPara.runTolTime = 0;
                runPara.secMinCnt = 0;
                runPara.onoff_RunTime = 0;
                runPara.runTypeFlag = runUUT[uutNo].wPara.ctrlRunFlag;
                runPara.startFlag = 1;
                ThreadMon[ctrlCom].setRun(ctrlAddr, runPara, ref er);
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置治具使用次数
        /// </summary>
        /// <param name="uutNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setFixUsedNum(int uutNo, ref string er)
        {
            try
            {
                er = string.Empty;
                runUUT[uutNo].wPara.usedNum++;
                runUUT[uutNo + 1].wPara.usedNum++;
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);
                string sqlCmd = "Update RUN_PARA set UsedNum=UsedNum+1 where UUTNO>" + uutNo + " and UUTNO<" + (uutNo + 3);
                return db.excuteSQL(sqlCmd, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置老化开始
        /// </summary>
        /// <param name="uuutNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setFixBIBegin(int uutNo, ref string er)
        {
            try
            {
                runUUT[uutNo].wPara.startTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                runUUT[uutNo].wPara.saveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                runUUT[uutNo].wPara.saveFileName = runUUT[uutNo].wPara.modelName + "_" + runUUT[uutNo].wPara.idCard +
                                                 "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                if (CSysPara.mVal.reportPath == string.Empty)
                    CSysPara.mVal.reportPath = Application.StartupPath + "\\Report";
                if (!System.IO.Directory.Exists(CSysPara.mVal.reportPath))
                    System.IO.Directory.CreateDirectory(CSysPara.mVal.reportPath);
                runUUT[uutNo].wPara.savePathName = CSysPara.mVal.reportPath + "\\" + DateTime.Now.ToString("yyyyMMdd") +
                                                   "\\" + runUUT[uutNo].wPara.modelName;
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);
                string sqlCmd = "Update RUN_PARA set StartTime='" + runUUT[uutNo].wPara.startTime + "',SaveDataTime='" +
                                runUUT[uutNo].wPara.saveDataTime + "',SaveFileName='" + runUUT[uutNo].wPara.saveFileName + "'," +
                                "SavePathName='" + runUUT[uutNo].wPara.savePathName + "'" +
                                " where UUTNO=" + (uutNo + 1);
                return db.excuteSQL(sqlCmd, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置老化结束
        /// </summary>
        /// <param name="uutNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setFixBIEnd(int uutNo, ref string er)
        {
            try
            {
                string endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                runUUT[uutNo].wPara.endTime = endTime;
                runUUT[uutNo].wPara.runTime = runUUT[uutNo].wPara.burnTime;
                //写入老化结束时间
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);
                string sqlCmd = "update RUN_PARA set RunTime=BurnTime,EndTime='" + endTime + "'" +
                              " where UUTNO=" + (uutNo + 1);
                if (!db.excuteSQL(sqlCmd, ref er))
                    return false;
                //写入老化结果
                List<string> serialNos = new List<string>();
                List<int> result = new List<int>();
                for (int i = 0; i < runUUT[uutNo].wLed.Count; i++)
                {
                    serialNos.Add(runUUT[uutNo].wLed[i].serialNo);
                    if (runUUT[uutNo].wLed[i].serialNo == "")
                        result.Add(0);
                    else
                    {
                        string MesResult = string.Empty;
                        if (runUUT[uutNo].wLed[i].failResult == 0)
                        {
                            result.Add(0);
                            MesResult = "PASS";
                        }
                        else
                        {
                            result.Add(CGlobal.CFlow.flowId);
                            MesResult = "B/I-NG-01";
                        }
                        if( CSysPara .mVal .conMes)
                        {
                            if (MesSajet.Tran_SN(runUUT[uutNo].wLed[i].serialNo, MesResult , ref er))
                                runLog.Log("条码" + runUUT[uutNo].wLed[i].serialNo + "上传成功", udcRunLog.ELog.Content);
                            else
                                runLog.Log("条码" + runUUT[uutNo].wLed[i].serialNo + "上传失败，错误:" + er, udcRunLog.ELog.NG);
                        }
                    }
                }

                if (!CGJMES.updateStatTTNum(runUUT[uutNo].wPara.idCard, CGlobal.CFlow.flowId,
                                            serialNos, result, ref er))
                    return false;
                if (!CGJMES.updateFixResult(CGlobal.CFlow.flowId, runUUT[uutNo].wPara.idCard,
                                              serialNos, result, ref er))
                    return false;
                //记录产品信息
                List<CGJMES.CSnRecord> snRecord = new List<CGJMES.CSnRecord>();
                for (int i = 0; i < runUUT[uutNo].wLed.Count; i++)
                {
                    if (runUUT[uutNo].wLed[i].serialNo != "")
                    {
                        CGJMES.CSnRecord snItem = new CGJMES.CSnRecord();
                        snItem.serialNo = runUUT[uutNo].wLed[i].serialNo;
                        snItem.idCard = runUUT[uutNo].wPara.idCard;
                        snItem.slotNo = i + 1;
                        snItem.statId = CGlobal.CFlow.flowId;
                        snItem.statName = "BURNIN";
                        snItem.startTime = runUUT[uutNo].wPara.startTime;
                        snItem.endTime = runUUT[uutNo].wPara.endTime;
                        snItem.testResult = result[i];
                        snItem.testTime = runUUT[uutNo].wPara.runTime;
                        snRecord.Add(snItem);
                    }
                }
                if (!CGJMES.recordSnFlow(snRecord, ref er))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 修改当前老化产品数量
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool UpdateUUTBINum(ref string er)
        {
            try
            {
                runFix.BITTNum = 0;
                runFix.BIPASSNum = 0;
                for (int i = 0; i < C_UUT_MAX; i++)
                {
                    if (runUUT[i].wPara.doRun == EDoRun.正在老化 || runUUT[i].wPara.doRun == EDoRun.老化结束)
                    {
                        for (int slot = 0; slot < C_SLOT_MAX; slot++)
                        {
                            if (runUUT[i].wLed[slot].serialNo != "")
                            {
                                if (runUUT[i].wLed[slot].failResult == 0)
                                    runFix.BIPASSNum++;
                                runFix.BITTNum++;
                            }
                        }
                    }
                }
                updateUUTBIUI(runFix.BITTNum, runFix.BIPASSNum);

                CIniFile.WriteToIni("Parameter", "BITTNum", runFix.BITTNum.ToString(), iniFile);

                CIniFile.WriteToIni("Parameter", "BIPASSNum", runFix.BIPASSNum.ToString(), iniFile);

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true;
            }
        }
        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="idNo"></param>
        private void setTimer(int idNo, string info)
        {
            string localName = "<" + runUUT[idNo].wBase.localName + ">";
            runUUT[idNo].wPara.waitAlarm = false;
            runUUT[idNo].wPara.waitStartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            runUUT[idNo].wPara.waitTimes = 0;
            runUUT[idNo].wPara.waitInfo = localName + info;
            runLog.Log(runUUT[idNo].wPara.waitInfo, udcRunLog.ELog.Action);
        }
        /// <summary>
        /// 读设置时间超时
        /// </summary>
        /// <param name="idNo"></param>
        /// <param name="info"></param>
        /// <param name="wTimeOut"></param>
        /// <returns></returns>
        private bool readTimerOut(int idNo, string info = "", int wTimeOut = 180)
        {

            if (runUUT[idNo].wPara.waitStartTime == string.Empty)
                runUUT[idNo].wPara.waitStartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            runUUT[idNo].wPara.waitTimes = GJ.CMath.DifTime(runUUT[idNo].wPara.waitStartTime);
            if (runUUT[idNo].wPara.waitTimes > wTimeOut)
            {
                if (!runUUT[idNo].wPara.waitAlarm)
                {
                    runUUT[idNo].wPara.waitAlarm = true;
                    if (info != "")
                    {
                        string localName = "<" + runUUT[idNo].wBase.localName + ">";
                        runLog.Log(localName + info, udcRunLog.ELog.NG);
                    }
                    else
                        runLog.Log(runUUT[idNo].wPara.waitInfo, udcRunLog.ELog.NG);
                }
                return false;
            }
            return true;
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="runUUT"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool updateUUTDBStatus(CUUT runUUT, ref string er)
        {
            try
            {
                er = string.Empty;
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);
                string sqlCmd = "update RUN_PARA Set AlarmErr=" + (int)runUUT.wPara.alarmErr + ",AlarmInfo='" +
                               runUUT.wPara.alarmInfo + "',doRun=" + (int)runUUT.wPara.doRun + ",IsNull=" + runUUT.wPara.isNull +
                               " where UUTNO=" + runUUT.wBase.uutNo;
                return db.excuteSQL(sqlCmd, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 清除针盘使用次数
        /// </summary>
        /// <param name="idNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool clearUUTUsedNum(int uutNo, ref string er)
        {
            try
            {
                er = string.Empty;
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);
                string sqlCmd = "update RUN_PARA Set UsedNum=0" +
                               " where UUTNO>" + uutNo + " and UUTNO<" + (uutNo + 3);
                return db.excuteSQL(sqlCmd, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 修改测试结果
        /// </summary>
        /// <param name="runUUT"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setUUTTestResult(CUUT runUUT, ref string er)
        {
            try
            {
                er = string.Empty;
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);
                string sqlCmd = string.Empty;
                List<string> sqlCmdList = new List<string>();
                for (int i = 0; i < C_SLOT_MAX; i++)
                {
                    sqlCmd = "Update RUN_DATA Set passResult=" + runUUT.wLed[i].passResult + ",failResult=" +
                            runUUT.wLed[i].failResult + ",FailEnd=" + runUUT.wLed[i].failEnd +
                            ",FailTime='" + runUUT.wLed[i].failTime + "',FailStr='" + runUUT.wLed[i].failInfo + "'" +
                            " where UUTNO=" + runUUT.wBase.uutNo + " and slotNo=" + (i + 1).ToString();
                    sqlCmdList.Add(sqlCmd);
                }
                return db.excuteSQLArray(sqlCmdList, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 从PLC中读取idcard信息
        /// </summary>
        /// <param name="idNo"></param>
        /// <param name="idcard"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool readIdFromBIPLC(int idNo, ref string idcard, ref string er)
        {
            try
            {
                byte[] rBit = new byte[10];

                if (idNo == 0)  //上层
                {
                    for (int i = 0; i < rBit.Length; i++)                    
                        rBit[i] = System.Convert.ToByte(ThreadPLC.rREGVal[InpPLC(EPLCINP.老化入口位上层ID卡信息0, i)]); 
                    
                }
                else           //下层
                {
                    for (int i = 0; i < rBit.Length; i++)
                        rBit[i] = System.Convert.ToByte(ThreadPLC.rREGVal[InpPLC(EPLCINP.老化入口位下层ID卡信息0, i)]); 
                }
                idcard = System.Text.Encoding.ASCII.GetString(rBit);     
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion
       
        #endregion

        #region 系统方法
        /// <summary>
        /// 检查系统运行状态
        /// </summary>
        /// <param name="bShow"></param>
        /// <returns></returns>
        private bool checkSystem(ref string er)
        {
            try
            {
                er = string.Empty;

                //检测系统通信状态
                if (!ThreadPLC.mConStatus)
                {
                    if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                        C_SYS_ALARM_COUNT++;
                    else
                    {
                        C_SYS_ALARM_COUNT = 0;
                        runLog.Log("测试PLC通信异常,请检查PLC线路?", udcRunLog.ELog.NG);
                    }
                    return false;
                }
                if (!ThreadPLC.mConOpStat)
                {
                    if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                        C_SYS_ALARM_COUNT++;
                    else
                    {
                        C_SYS_ALARM_COUNT = 0;
                        runLog.Log("测试PLC通信异常,请检查PLC线路?", udcRunLog.ELog.NG);
                    }
                    return false;
                }
                if (ThreadPLC.mStatus != CPLCPara.EStatus.运行)
                {
                    if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                        C_SYS_ALARM_COUNT++;
                    else
                    {
                        C_SYS_ALARM_COUNT = 0;
                        runLog.Log("测试PLC线程未运行,请检查软件是否启动线程?", udcRunLog.ELog.NG);
                    }
                    return false;
                }

                //检测系统通信状态
                if (!ThreadPLCTemp.mConStatus)
                {
                    if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                        C_SYS_ALARM_COUNT++;
                    else
                    {
                        C_SYS_ALARM_COUNT = 0;
                        runLog.Log("温度PLC通信异常,请检查PLC线路?", udcRunLog.ELog.NG);
                    }
                    return false;
                }
                if (!ThreadPLCTemp.mConOpStat)
                {
                    if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                        C_SYS_ALARM_COUNT++;
                    else
                    {
                        C_SYS_ALARM_COUNT = 0;
                        runLog.Log("温度PLC通信异常,请检查PLC线路?", udcRunLog.ELog.NG);
                    }
                    return false;
                }
                if (ThreadPLCTemp.mStatus != CPLCPara.EStatus.运行)
                {
                    if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                        C_SYS_ALARM_COUNT++;
                    else
                    {
                        C_SYS_ALARM_COUNT = 0;
                        runLog.Log("温度PLC线程未运行,请检查软件是否启动线程?", udcRunLog.ELog.NG);
                    }
                    return false;
                }
                C_SYS_ALARM_COUNT = 0;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion

        #region MTK
        /// <summary>
        /// MTK控制
        /// </summary>
        /// <param name="uutNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setMTK(int uutNo, ref string er)
        {
            try
            {
                er = string.Empty;

                //获取当前电压规格
                int MTK_STEP = 0;
                for (int CH = 0; CH < runUUT[uutNo].wLed[0].vName.Count; CH++)
                {
                    if (runUUT[uutNo].wPara.runQCStepNo == runUUT[uutNo].wLed[0].QCV[CH])
                    {
                        MTK_STEP = CH;
                        break;
                    }
                }
                double MTK_VOLT_MAX = runUUT[uutNo].wLed[0].vMax[MTK_STEP];
                double MTK_VOLT_MIN = runUUT[uutNo].wLed[0].vMin[MTK_STEP];

                double UUT_VOLT_MIN = 25;
                double UUT_VOLT_MAX = 3;

                bool HAVE_UUT = false;
                bool UUT_PASS = false;
                //获取产品输出大于3V的最小电压值->保证产品有输出
                for (int slot = 0; slot < C_SLOT_MAX; slot++)
                {
                    if (runUUT[uutNo].wLed[slot].serialNo != "") //治具1
                    {
                        if (runUUT[uutNo].wLed[slot].unitV > 3)
                        {
                            HAVE_UUT = true;
                            if (runUUT[uutNo].wLed[slot].unitV < UUT_VOLT_MIN)
                                UUT_VOLT_MIN = runUUT[uutNo].wLed[slot].unitV;
                            if (runUUT[uutNo].wLed[slot].unitV > UUT_VOLT_MAX)
                                UUT_VOLT_MAX = runUUT[uutNo].wLed[slot].unitV;
                            if (runUUT[uutNo].wLed[slot].unitV >= MTK_VOLT_MIN &&
                                runUUT[uutNo].wLed[slot].unitV <= MTK_VOLT_MAX)
                                UUT_PASS = true;                             
                        }
                    }
                    if (runUUT[uutNo+1].wLed[slot].serialNo != "") //治具2
                    {
                        if (runUUT[uutNo+1].wLed[slot].unitV > 3)
                        {
                            HAVE_UUT = true;
                            if (runUUT[uutNo + 1].wLed[slot].unitV < UUT_VOLT_MIN)
                                UUT_VOLT_MIN = runUUT[uutNo + 1].wLed[slot].unitV;
                            if (runUUT[uutNo + 1].wLed[slot].unitV > UUT_VOLT_MAX)
                                UUT_VOLT_MAX = runUUT[uutNo + 1].wLed[slot].unitV;
                            if (runUUT[uutNo + 1].wLed[slot].unitV >= MTK_VOLT_MIN &&
                                runUUT[uutNo + 1].wLed[slot].unitV <= MTK_VOLT_MAX)
                                UUT_PASS = true;   
                        }
                    }
                }
                //所有产品无输出
                if (!HAVE_UUT)
                    return true;
                //可能AC断电引起产品重启
                if (!runUUT[uutNo].wPara.MTK_RUNNING && !UUT_PASS && runUUT[uutNo].wPara.MTK_SHUTDOWN==1)
                {
                    runUUT[uutNo].wPara.MTK_SHUTDOWN = 0;
                    runUUT[uutNo].wPara.MTK_SPEC_OK = 0;
                    runUUT[uutNo].wPara.MTK_RUNNING = false;
                    runUUT[uutNo].wPara.MTK_STEP_NO = 0;
                    runUUT[uutNo + 1].wPara.MTK_SHUTDOWN = 0;
                    runUUT[uutNo + 1].wPara.MTK_SPEC_OK = 0;
                    runUUT[uutNo + 1].wPara.MTK_RUNNING = false;
                    runUUT[uutNo + 1].wPara.MTK_STEP_NO = 0;
                }
                ///MTK快充电压达标
                if (runUUT[uutNo].wPara.MTK_SPEC_OK == 1 && runUUT[uutNo + 1].wPara.MTK_SPEC_OK == 1)
                {
                    runUUT[uutNo].wPara.MTK_SHUTDOWN = 1;
                    runUUT[uutNo+1].wPara.MTK_SHUTDOWN = 1; 
                    return true;
                } 
                //判断所有产品都在MTK范围
                if (UUT_VOLT_MIN >= MTK_VOLT_MIN-0.5 && UUT_VOLT_MIN <= MTK_VOLT_MAX+0.5 &&
                    UUT_VOLT_MAX >= MTK_VOLT_MIN-0.5 && UUT_VOLT_MAX <= MTK_VOLT_MAX+0.5)
                {
                    runUUT[uutNo].wPara.MTK_SPEC_OK = 1;
                    runUUT[uutNo].wPara.MTK_RUNNING = false;
                    runUUT[uutNo].wPara.MTK_STEP_NO = 0;
                    runUUT[uutNo + 1].wPara.MTK_SPEC_OK = 1;
                    runUUT[uutNo + 1].wPara.MTK_RUNNING = false;
                    runUUT[uutNo + 1].wPara.MTK_STEP_NO = 0;  
                    return true;
                }
               
                int MTK_TIME_OUT_1 = CSysPara.mVal.getMTKCoutn;
                int MTK_TIME_OUT_2 = CSysPara.mVal.getMTKCoutn*2;

                //负载正在调正电压过程...
                if (runUUT[uutNo].wPara.MTK_RUNNING && runUUT[uutNo + 1].wPara.MTK_RUNNING)
                {
                    //超过调正1段次数->停止调正
                    if (!CSysPara.mVal.chkgetMTK && runUUT[uutNo].wPara.MTK_STEP_NO > MTK_TIME_OUT_1)
                    {
                        runUUT[uutNo].wPara.MTK_SPEC_OK = 1;
                        runUUT[uutNo].wPara.MTK_RUNNING = false;
                        runUUT[uutNo].wPara.MTK_STEP_NO = 0;
                        runUUT[uutNo + 1].wPara.MTK_SPEC_OK = 1;
                        runUUT[uutNo + 1].wPara.MTK_RUNNING = false;
                        runUUT[uutNo + 1].wPara.MTK_STEP_NO = 0;
                        return true;
                    }
                    //超过调正次数->停止调正
                    if (runUUT[uutNo].wPara.MTK_STEP_NO > MTK_TIME_OUT_2)
                    {
                        runUUT[uutNo].wPara.MTK_SPEC_OK = 1;
                        runUUT[uutNo].wPara.MTK_RUNNING = false;
                        runUUT[uutNo].wPara.MTK_STEP_NO = 0;
                        runUUT[uutNo + 1].wPara.MTK_SPEC_OK = 1;
                        runUUT[uutNo + 1].wPara.MTK_RUNNING = false;
                        runUUT[uutNo + 1].wPara.MTK_STEP_NO = 0;
                        return true;
                    }
                    //超过第1次调正次数->取有产品在范围次数
                    if (runUUT[uutNo].wPara.MTK_STEP_NO > MTK_TIME_OUT_1)
                    {
                        if (UUT_VOLT_MIN > MTK_VOLT_MIN && UUT_VOLT_MIN < MTK_VOLT_MAX)
                        {
                            runUUT[uutNo].wPara.MTK_SPEC_OK = 1;
                            runUUT[uutNo].wPara.MTK_RUNNING = false;
                            runUUT[uutNo].wPara.MTK_STEP_NO = 0;
                            runUUT[uutNo + 1].wPara.MTK_SPEC_OK = 1;
                            runUUT[uutNo + 1].wPara.MTK_RUNNING = false;
                            runUUT[uutNo + 1].wPara.MTK_STEP_NO = 0;
                            return true;
                        }
                    }
                    //判断产品电压状态
                    if (runUUT[uutNo].wPara.MTK_RAISED == 0)  //所有电压降到7V以下
                    {
                        if (UUT_VOLT_MIN < 7 && UUT_VOLT_MAX < 7)
                        {
                            runUUT[uutNo].wPara.MTK_RAISED = 1;
                            runUUT[uutNo].wPara.MTK_RUNNING = true;
                            runUUT[uutNo].wPara.MTK_STEP_NO = 0;
                            runUUT[uutNo + 1].wPara.MTK_RAISED = 1;
                            runUUT[uutNo + 1].wPara.MTK_RUNNING = true;
                            runUUT[uutNo + 1].wPara.MTK_STEP_NO = 0;
                        }
                        else
                        {
                            runUUT[uutNo].wPara.MTK_STEP_NO++;
                            runUUT[uutNo + 1].wPara.MTK_STEP_NO++;
                            setMTKVOLT(uutNo, runUUT[uutNo].wPara.MTK_RAISED, ref er);
                        }
                    }
                    else if (runUUT[uutNo].wPara.MTK_RAISED == 1) //调正电压上升
                    {
                        runUUT[uutNo].wPara.MTK_STEP_NO++;
                        runUUT[uutNo + 1].wPara.MTK_STEP_NO++;
                        setMTKVOLT(uutNo, runUUT[uutNo].wPara.MTK_RAISED, ref er);
                    }
                    else if (runUUT[uutNo].wPara.MTK_RAISED == 2)  //调正电压下降
                    {
                        runUUT[uutNo].wPara.MTK_STEP_NO++;
                        runUUT[uutNo + 1].wPara.MTK_STEP_NO++;
                        setMTKVOLT(uutNo, runUUT[uutNo].wPara.MTK_RAISED, ref er);
                    }
                    return false;
                }
                //--------------开始调正电压。。。。
                //当产品电压存在在范围且有不在范围内:需下降电压值-->直到所有电压低于+5.5V
                if (UUT_VOLT_MAX > MTK_VOLT_MIN && UUT_VOLT_MIN < MTK_VOLT_MIN) 
                {
                    runUUT[uutNo].wPara.MTK_RAISED = 0;                     
                    runUUT[uutNo].wPara.MTK_RUNNING = true;
                    runUUT[uutNo].wPara.MTK_STEP_NO = 0;
                    runUUT[uutNo + 1].wPara.MTK_RAISED = 0;
                    runUUT[uutNo + 1].wPara.MTK_RUNNING = true;
                    runUUT[uutNo + 1].wPara.MTK_STEP_NO = 0;                    
                }
                //当产品电压都在范围下限:上升电压值
                else if (UUT_VOLT_MIN < MTK_VOLT_MIN && UUT_VOLT_MAX < MTK_VOLT_MIN)
                {                     
                    runUUT[uutNo].wPara.MTK_RAISED = 1;
                    runUUT[uutNo].wPara.MTK_RUNNING = true;
                    runUUT[uutNo].wPara.MTK_STEP_NO = 0;
                    runUUT[uutNo + 1].wPara.MTK_RAISED = 1;
                    runUUT[uutNo + 1].wPara.MTK_RUNNING = true;
                    runUUT[uutNo + 1].wPara.MTK_STEP_NO = 0; 
                }
                //当产品电压都在范围上限:下升电压值
                else if (UUT_VOLT_MIN > MTK_VOLT_MAX && UUT_VOLT_MAX < MTK_VOLT_MAX)
                {                     
                    runUUT[uutNo].wPara.MTK_RAISED = 2;
                    runUUT[uutNo].wPara.MTK_RUNNING = true;
                    runUUT[uutNo].wPara.MTK_STEP_NO = 0;
                    runUUT[uutNo + 1].wPara.MTK_RAISED = 2;
                    runUUT[uutNo + 1].wPara.MTK_RUNNING = true;
                    runUUT[uutNo + 1].wPara.MTK_STEP_NO = 0; 
                }
                setMTKVOLT(uutNo, runUUT[uutNo].wPara.MTK_RAISED, ref er); 
                return false;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置MTK电压上升或下降
        /// </summary>
        /// <param name="uutNo"></param>
        /// <param name="wRaise"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool setMTKVOLT(int uutNo, int wRaise, ref string er)
        {
            try
            {
                int ersCom = runUUT[uutNo].wBase.ersCom;
                int ersAddr = runUUT[uutNo].wBase.ersAddr;
                int ersCH = runUUT[uutNo].wBase.ersCH;
                bool voltRaise = false;
                if (wRaise == 1)
                    voltRaise = true;
                return ThreadERS[ersCom].setMTKRaise(ersAddr, ersCH, voltRaise, ref er);    
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }        
        }
        #endregion

        #region 委托
        private delegate void updateSignalUIHandler();
        /// <summary>
        /// 刷新动作状态显示
        /// </summary>
        private void updateSignalUI()
        {
            if (this.InvokeRequired)
                this.Invoke(new updateSignalUIHandler(updateSignalUI));
            else
            {
                labSysStatus.Text = runFix.sysStat;
                if (runFix.sysStat == "运行中")
                    labSysStatus.ForeColor = Color.Blue;
                else
                    labSysStatus.ForeColor = Color.Red;
                labTRead.Text = runFix.rTemp.ToString("0.0");
                if (runFix.rTemp <runModel.TSet - runModel.TLP)
                    labTRead.ForeColor = Color.Yellow;
                else if (runFix.rTemp > runModel.TSet + runModel.THP)
                    labTRead.ForeColor = Color.Red;
                else
                    labTRead.ForeColor = Color.Lime;
                if (runFix.rInReady == 1)
                {
                    labInStatus.Text = "就绪";
                    labInStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labInStatus.Text = "空闲";
                    labInStatus.ForeColor = Color.Red;
                }
                if (runFix.rOutReady == 1)
                {
                    labOutStatus.Text = "空闲";
                    labOutStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labOutStatus.Text = "忙碌";
                    labOutStatus.ForeColor = Color.Red;
                }
                if (runFix.inFix[0].iReady == 1)
                {
                    labFixReady1.Text = "就绪";
                    labFixReady1.ForeColor = Color.Blue;
                }
                else
                {
                    labFixReady1.Text = "空闲";
                    labFixReady1.ForeColor = Color.Red;
                }
                if (runFix.inFix[1].iReady == 1)
                {
                    labFixReady2.Text = "就绪";
                    labFixReady2.ForeColor = Color.Blue;
                }
                else
                {
                    labFixReady2.Text = "空闲";
                    labFixReady2.ForeColor = Color.Red;
                }
                if (runFix.handStat == EHandStat.空闲)
                {
                    labHandStatus.Text = runFix.handStat.ToString();
                    labHandStatus.ForeColor = Color.Blue;  
                }
                else
                {
                    if (runFix.handStat == EHandStat.忙碌)
                    {                        
                        labHandStatus.Text = runFix.handStat.ToString();
                        labHandStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        labHandStatus.Text = runFix.handStat.ToString();
                        labHandStatus.ForeColor = Color.Red;  
                    }
                    
                }
            }
        }
        private delegate void updateInPosUIHandler(string localName);
        /// <summary>
        /// 进机位置
        /// </summary>
        /// <param name="localName"></param>
        private void updateInPosUI(string localName)
        {
            if (this.InvokeRequired)
                this.Invoke(new updateInPosUIHandler(updateInPosUI), localName);
            else
            {
                labInPos.Text = localName;
                labInPos.ForeColor = Color.Blue;  
            }
        }
        private delegate void updateOutPosUIHandler(string localName);
        /// <summary>
        /// 出机位置
        /// </summary>
        /// <param name="localName"></param>
        private void updateOutPosUI(string localName)
        {
            if (this.InvokeRequired)
                this.Invoke(new updateOutPosUIHandler(updateOutPosUI), localName);
            else
            {
                labOutPos.Text = localName;
                labOutPos.ForeColor = Color.Blue;
            }
        }
        private delegate void updateInOutBIFixNumHandler(int uutNum);
        /// <summary>
        /// 修改进BI治具数量
        /// </summary>
        /// <param name="uutNo"></param>
        private void updateInBIFixNum(int uutNum)
        {
            if (this.InvokeRequired)
                this.Invoke(new updateInOutBIFixNumHandler(updateInBIFixNum),uutNum);
            else
            {
                runFix.InBIFixNo += uutNum;
                labInNum.Text = runFix.InBIFixNo.ToString();
                CIniFile.WriteToIni("Parameter", "InBIFixNo", runFix.InBIFixNo.ToString(), iniFile);  
            }
        }
        /// <summary>
        /// 修改出BI治具数量
        /// </summary>
        /// <param name="uutNo"></param>
        private void updateOutBIFixNum(int uutNum)
        {
            if (this.InvokeRequired)
                this.Invoke(new updateInOutBIFixNumHandler(updateOutBIFixNum), uutNum);
            else
            {
                runFix.OutBIFixNo += uutNum;
                labOutNum.Text = runFix.OutBIFixNo.ToString();
                CIniFile.WriteToIni("Parameter", "OutBIFixNo", runFix.OutBIFixNo.ToString(), iniFile);
            }
        }
        private delegate void updateUUTBINumHandler(int ttNum, int passNum);
        /// <summary>
        /// 设置老化产品数量
        /// </summary>
        /// <param name="ttNum"></param>
        /// <param name="passNum"></param>
        private void updateUUTBIUI(int ttNum, int passNum)
        {
            if (this.InvokeRequired)
                this.Invoke(new updateUUTBINumHandler(updateUUTBIUI), ttNum, passNum);
            else
            {
                double failRate = 0;
                if (ttNum > 0)                
                    failRate = ((double)(ttNum - passNum))*100 / ((double)ttNum);
                labTTNum.Text = ttNum.ToString();
                labPassNum.Text = passNum.ToString();
                labFailRate.Text = failRate.ToString("0.0")+"%";   
            }
        }
        #endregion

        #region 线程事件
        private void OnPLCConArgs(object sender, CPLCConArgs e)
        {
            if (e.bErr)
                runLog.Log(e.conStatus, udcRunLog.ELog.NG);
        }
        private void OnPLCDataArgs(object sender, CPLCDataArgs e)
        {
            if (e.bErr)
                runLog.Log(e.rData, udcRunLog.ELog.NG);
        }
        private void OnMonConArgs(object sender, CMonConArgs e)
        {
            if (e.bErr)
                runLog.Log(e.conStatus, udcRunLog.ELog.NG);
        }
        private void OnMonDataArgs(object sender, CMonDataArgs e)
        {
            if (e.bErr)
                runLog.Log(e.rData, udcRunLog.ELog.NG);
        }
        private void OnERSConArgs(object sender, CERSConArgs e)
        {
            if (e.bErr)
                runLog.Log(e.conStatus, udcRunLog.ELog.NG);
        }
        private void OnERSDataArgs(object sender, CERSDataArgs e)
        {
            if (e.bErr)
                runLog.Log(e.rData, udcRunLog.ELog.NG);
        }
        #endregion

        #region 测试报表
        /// <summary>
        /// 测试保存报表
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool SaveTestReport(ref string er)
        {
            try
            {
                if (!CSysPara.mVal.saveReport)
                    return true;
                if(!Directory.Exists(CSysPara.mVal.reportPath))     
                    Directory.CreateDirectory(CSysPara.mVal.reportPath);
                for (int i = 0; i < C_UUT_MAX; i++)
                {
                    if (runUUT[i].wPara.doRun != EDoRun.正在老化)
                        continue;
                    if (runUUT[i].wPara.savePathName == string.Empty)
                        continue;
                    if (runUUT[i].wPara.saveFileName == string.Empty)
                        continue;
                    if (!Directory.Exists(runUUT[i].wPara.savePathName))
                        Directory.CreateDirectory(runUUT[i].wPara.savePathName);
                    if (runUUT[i].wPara.saveDataTime == "")
                    {
                        runUUT[i].wPara.saveDataTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        continue;
                    }
                    if (CMath.DifTime(runUUT[i].wPara.saveDataTime) < CSysPara.mVal.saveReportTimes * 60)
                        continue;
                    //获取保存数据时间
                    DateTime saveTime = (System.Convert.ToDateTime(runUUT[i].wPara.saveDataTime)).AddMinutes(CSysPara.mVal.saveReportTimes);
                    runUUT[i].wPara.saveDataTime = saveTime.ToString("yyyy/MM/dd HH:mm:ss");
                    string filePath = runUUT[i].wPara.savePathName + "\\" + runUUT[i].wPara.saveFileName;
                    bool IsExist = true;
                    if (!File.Exists(filePath))
                        IsExist = false;
                    StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8);
                    string strWrite = string.Empty;
                    string strTemp = string.Empty;
                    if (!IsExist)
                    { 
                       strWrite = "机种名称:," + runUUT[i].wPara.modelName;  
                       sw.WriteLine(strWrite);
                       strWrite = "老化位置:," + runUUT[i].wBase.localName;
                       sw.WriteLine(strWrite);
                       strWrite = "开始时间:," + runUUT[i].wPara.startTime;
                       sw.WriteLine(strWrite);
                       DateTime endTime = (System.Convert.ToDateTime(runUUT[i].wPara.startTime)).AddHours(runUUT[i].wPara.burnTime);
                       strWrite = "结束时间:," + endTime.ToString("yyyy/MM/dd HH:mm:ss") ;
                       sw.WriteLine(strWrite);
                       strWrite = "老化时间(H):," + (runUUT[i].wPara.burnTime/3600).ToString("0.0");   
                       sw.WriteLine(strWrite); 
                       for (int CH = 0; CH <4; CH++)
			           {
			             if(runUUT[i].wLed[0].vName[CH]!=string.Empty)
                         {
                             strWrite = "电压" + (CH + 1).ToString("D2") + ":," + runUUT[i].wLed[0].vName[CH] +
                                        "," + runUUT[i].wLed[0].vMin[CH] + "V~" + runUUT[i].wLed[0].vMax[CH]+"V"; 
                             sw.WriteLine(strWrite);
                             strWrite = "电流"+ (CH+1).ToString("D2") +":,"+ runUUT[i].wLed[0].iSet[CH]+"A," +
                                         runUUT[i].wLed[0].iMin[CH]+ "A~"+runUUT[i].wLed[0].iMax[CH]+"A"; 
                             sw.WriteLine(strWrite);
                         } 
			           }
                       for (int slot = 0; slot < runUUT[i].wLed.Count; slot++)                       
                           strWrite = "Sn" + (slot+1).ToString("D2")+ ":,"  + runUUT[i].wLed[slot].serialNo;
                       strWrite = string.Empty;                      
                       strTemp = "扫描时间,";
                       strWrite += strTemp;
                       for (int slot = 0; slot < C_SLOT_MAX; slot++)
			           {
			              strTemp=(slot+1).ToString("D2")+"(V)"+",";
                          strWrite += strTemp;
			           }
                        for (int slot = 0; slot < C_SLOT_MAX; slot++)
			           {
			              strTemp=(slot+1).ToString("D2")+"(A)" +",";
                          strWrite += strTemp;
			           }
                        sw.WriteLine(strWrite); 
                    }
                    strWrite = string.Empty; 
                    strTemp = runUUT[i].wPara.saveDataTime+",";
                    strWrite += strTemp;
                    for (int slot = 0; slot < C_SLOT_MAX; slot++)
                    {
                        if (runUUT[i].wLed[slot].serialNo == string.Empty)
                            strTemp = "----";
                        else
                            strTemp = runUUT[i].wLed[slot].unitV.ToString("0.000");
                        strTemp += ",";
                        strWrite += strTemp;
                    }
                    for (int slot = 0; slot < C_SLOT_MAX; slot++)
                    {
                        if (runUUT[i].wLed[slot].serialNo == string.Empty)
                            strTemp = "----";
                        else
                            strTemp = runUUT[i].wLed[slot].unitA.ToString("0.00");
                        strTemp += ",";
                        strWrite += strTemp;
                    }
                    sw.WriteLine(strWrite); 
                    sw.Flush();
                    sw.Close();
                    sw = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion

    }
}
