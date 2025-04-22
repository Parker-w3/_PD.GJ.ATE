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
using GJ;
using GJ.Dev.PLC;
using GJ.Dev.Card;
using GJ.Dev.Mon;
using GJ.Dev.Meter;
using GJ.UI;
using GJ.Para.Base;
using GJ.Para.Udc;
using GJ.Para.Udc.LOADUP;
using GJ.Mes;
using GJ.Dev.ELoad;
namespace GJ.Para.LOADUP
{
    public partial class udcRun : udcMainBase
    {
        #region 构造函数
        public udcRun()
        {
            InitializeComponent();

            loadSysPara();

            loadRunPara();

            IntialControl();

            SetDoubleBuffered();

            loadModelPara(".spec");  

        }
        #endregion

        #region 参数常量
        /// <summary>
        /// 线程扫描间隔
        /// </summary>
        private const int C_THREAD_DELAY = 100;
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
        private int C_SYS_ALARM_LIMIT = 10;
        /// <summary>
        /// 读卡器数量
        /// </summary>
        public const int C_ID_MAX = 1;
        /// <summary>
        /// 治具最大槽位数
        /// </summary>
        private const int C_SLOT_MAX = 8;
        #endregion

        #region 参数路径
        private string iniFile = "sysLog\\" + CGlobal.CFlow.flowGUID + ".ini";
        private string sysFile = "sysLog\\" + CGlobal.CFlow.flowGUID + ".xml";
        private string sysDB = "DBLog\\" + CGlobal.CFlow.flowGUID + ".accdb";
        private string plcDB = "PlcLog\\PLC_" + CGlobal.CFlow.flowGUID + ".accdb";
        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            //uiSnBand = new udcFixtureBand();
            //uiSnBand.Dock = DockStyle.Fill;
            //uiSnBand.mHandBand = CSysPara.mVal.handBandSn;
            //uiSnBand.mSnLen = CSysPara.mVal.snLen;
            //uiSnBand.mSnSpec = CSysPara.mVal.snSpec;
            //tableLayoutPanel3.Controls.Add(uiSnBand, 0, 0);
            labUUT = new Label[]{
                                 labUUT1,labUUT2,labUUT3,labUUT4,labUUT5,labUUT6,labUUT7,labUUT8
                                 };
            labNo = new CheckBox[C_SLOT_MAX];
            labSn = new Label[C_SLOT_MAX];
            for (int i = 0; i < labNo.Length; i++)
            {
                labNo[i] = new CheckBox();
                labNo[i].Name = "labNo" + i.ToString();
                labNo[i].Dock = DockStyle.Fill;
                labNo[i].Margin = new Padding(3, 0, 3, 0);
                labNo[i].Text = (i + 1).ToString("D2");
                labNo[i].CheckAlign = ContentAlignment.MiddleLeft;
                labNo[i].Checked = true;

                labSn[i] = new Label();
                labSn[i].Name = "labSn" + i.ToString();
                labSn[i].Dock = DockStyle.Fill;
                labSn[i].TextAlign = ContentAlignment.MiddleCenter;
                labSn[i].Margin = new Padding(0);
                labSn[i].Text = "";
                labSn[i].Font = new Font("宋体", 15);

                panelSn.Controls.Add(labNo[i], 0, i + 1);
                panelSn.Controls.Add(labSn[i], 1, i + 1);

            }
            uiSnInfo = new udcSnBand();
            uiSnInfo.OnBtnClick.OnEvent += new COnEvent<udcSnBand.COnBtnClick>.OnEventHandler(OnSnBtnClick);
            uiSnInfo.Dock = DockStyle.Fill;
            tableLayoutPanel3.Controls.Add(uiSnInfo, 0, 0);

        
            runLog = new udcRunLog();
            runLog.Dock = DockStyle.Fill;

            tableLayoutPanel3.Controls.Add(runLog,0,1);

        //    txtSnPress.Enabled = false;

        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
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
            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered",
                                       System.Reflection.BindingFlags.Instance |
                                       System.Reflection.BindingFlags.NonPublic)
                                       .SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel2.GetType().GetProperty("DoubleBuffered",
                                       System.Reflection.BindingFlags.Instance |
                                       System.Reflection.BindingFlags.NonPublic)
                                       .SetValue(tableLayoutPanel2, true, null);
                                            
            tableLayoutPanel3.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(tableLayoutPanel3, true, null);
               
            panel5.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel5, true, null);
            panelUUT.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panelUUT, true, null);
            panelSn.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panelSn, true, null);
        }
        /// <summary>
        /// 加载系统参数
        /// </summary>
        private void loadSysPara()
        {
            CSysSet<CSysPara>.load(sysFile, ref CSysPara.mVal);
        }
        /// <summary>
        /// 加载测试参数
        /// </summary>
        private void loadRunPara()
        {
            statSnBand = new CStat("SnBand");
            statPreTest = new CStat("PreTest");
            statInBI = new CStat("InBI");

            CGJMES.mesDB = CSysPara.mVal.mySqlIp;
        }
        /// <summary>
        /// 加载机种参数
        /// </summary>
        private void loadModelPara(string extentName)
        {
            string strVal = CIniFile.ReadFromIni("Parameter", "Model", iniFile);
            if (strVal == "")
                return;
            if (Path.GetExtension(strVal).ToUpper() != extentName.ToUpper())
                return;
            if (!File.Exists(strVal))
                return;
            CModelSet<CModelPara>.load(strVal, ref RunModel);

            uiSnInfo.ShowModel(RunModel);
           // uiTestInfo.mReTestTimes = CSysPara.mVal.testTimes;
            //uiTestInfo.mVmin = RunModel.vMin;
            //uiTestInfo.mVmax = RunModel.vMax;
            //uiTestInfo.mImin = RunModel.loadMin;
            //uiTestInfo.mImax = RunModel.loadMax;

        }
        #endregion

        #region 面板操作

        #region 面板控件

      //  private udcFixtureBand uiSnBand = null;
        private udcSnBand uiSnInfo = null;

       // private udcFixture uiPreTest = null;
        private udcPreTest uiTestInfo = null;

       // private udcFixture uiInBI = null;
        private udcRunLog runLog = null;

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
                    BtnF10 = true;
                    OnBtnTriger(EFormStatus.Auto);
                    break;
                case CFormBtnArgs.EBtnName.暂停:
                    if (!StartRun(false))
                        return;
                    BtnF10 = false;
                    OnBtnTriger(EFormStatus.Idel);
                    break;
                case CFormBtnArgs.EBtnName.退出:
                    if (!StartRun(false))
                        return;
                    Application.Exit();
                    break;
                case CFormBtnArgs.EBtnName.F9:
                    if (BtnF10)
                        return;
                    if (!StartRun())
                        return;
                    BtnF10 = true;
                    OnBtnTriger(EFormStatus.Auto);
                    break;
                case CFormBtnArgs.EBtnName.F10:
                    if (!StartRun(false))
                        return;
                    BtnF10 = false;
                    OnBtnTriger(EFormStatus.Idel);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 按钮消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSnBtnClick(object sender, udcSnBand.COnBtnClick e)
        {
            switch (e.btnNo)
            {
                case udcSnBand.EBtnNo.选机种:
                    string fileDirectry = string.Empty;
                    if (CSysPara.mVal.modelPath != "")
                        fileDirectry = CSysPara.mVal.modelPath;
                    else
                    {
                        fileDirectry = Application.StartupPath + "\\Model";
                        if (!Directory.Exists(fileDirectry))
                            Directory.CreateDirectory(fileDirectry);
                    }
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.InitialDirectory = fileDirectry;
                    dlg.Filter = "spec files (*.spec)|*.spec";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        CIniFile.WriteToIni("Parameter", "Model", dlg.FileName, iniFile);

                        loadModelPara(".spec");
                    }
                    break;
                case udcSnBand.EBtnNo.清除数量:
                    if (statSnBand.idCard == "")
                    {
                        MessageBox.Show("请放置要清除数量治具就绪", "清除数量", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    }
                    if (MessageBox.Show("确定要清除治具[" + statSnBand.idCard + "]数量?", "清除数量", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) != DialogResult.Yes)
                        break;
                    string er = string.Empty;
                    if (CGJMES.setFixtureUsedNum(statSnBand.idCard, ref er, true))
                    {
                        uiSnInfo.ShowUUTNum(0, 0);
                        MessageBox.Show("成功清除治具[" + statSnBand.idCard + "]数量", "清除数量", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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

        #endregion

        #region PLC状态定义
        private enum EPLCINP
        {
    
            上机位治具到位需要读取ID卡,


            PLC系统运行,
            PLC异常报警
            
        }
        private enum EPLCOUT
        {
           
            上机位ID卡读取结果,
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
        /// <summary>
        /// 写PLC寄存器
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="val"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private bool writePlc(EPLCOUT reg, int val, int offset = 0)
        {
            try
            {
                if (threadPLC == null)
                    return false;
                return threadPLC.addREGWrite(OutPLC(reg, offset), val);
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 读PLC寄存器
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private int readPlc(EPLCINP reg, int offset = 0)
        {
            try
            {
                if (threadPLC == null)
                    return -1;
                return threadPLC.rREGVal[InpPLC(reg, offset)];
            }
            catch (Exception)
            {

                return -1;
            }
        }
        #endregion

        #region 测试参数

        /// <summary>
        /// 测试状态
        /// </summary>
        private enum EDoRun
        {
            空闲,
            就绪,
            测试,
            过站,
            报警,
            检查
        }
        /// <summary>
        /// 测试位信息
        /// </summary>
        private class CStat
        {
            public CStat(string statName = "", int statId = 0, int statTcpId = 0)
            {
                this.statName = statName;
                this.statId = statId;
                this.statTcpId = statTcpId;
                for (int i = 0; i < slotNum; i++)
                {
                    serialNo.Add("");
                    result.Add(0);
                    failInfo.Add("");
                }
            }
            public string statName = "";
            public int statId = 0;
            public int statTcpId = 0;
            public int slotNum = C_SLOT_MAX;
            public bool setLoad = true;
            public int bReady = 0;
            public EDoRun doRun = EDoRun.空闲;
            public string idCard = string.Empty;
            public List<string> serialNo = new List<string>();
            public List<int> result = new List<int>();
            public List<string> failInfo = new List<string>();
            public int delayMs = 0;
            public string startTime = string.Empty;
            public string endTime = string.Empty;
            public int testTime = 0;
        }

        private CModelPara RunModel = new CModelPara();
        private CStat statSnBand = null;
        private CStat statPreTest = null;
        private CStat statInBI = null;

        #endregion

        #region 设备参数
        private CPLCCom devPLC = null;
        private CCardCom devIDCard = null;
      //  private GJMon32 devMonitor = null;
        //private GJEL_100_04 devGJEL = null;
        //private CPRU80_R1_2A_AC devMeter = null;

        private SajetMES MesSajet = null;


        #endregion

        #region 设备初始化
        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ConnectToPLC(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;
                if (connect)
                {
                    if (devPLC == null)
                    {
                        devPLC = new CPLCCom(EPLCType.InovanceTCP);
                        if (!devPLC.open(CSysPara.mVal.plcIp, ref er, CSysPara.mVal.plcPort.ToString()))
                        {
                            er = "连接PLC通信失败:" + er;
                            devPLC = null;
                            return false;
                        }
                        er = "成功连接到PLC主机";
                    }
                }
                else
                {
                    if (devPLC != null)
                    {
                        devPLC.close();
                        devPLC = null;
                        er = "断开连接PLC主机";
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
        /// 读卡器串口
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect"></param>
        /// <returns></returns>
        private bool ConnectToIDCard(ref string er, bool connect = true)
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

                        //Thread.Sleep(3000);
                        //if (MesSajet.Login("222", "222", ref er))
                        //    er = "登录到MES成功";
                        //else
                        //{
                        //    er = "登录MES失败！错误:" + er;
                        //    return false;
                        //}
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
                        MesSajet .StopConnect();
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

        #region 线程参数
        private CPLCPara threadPLC = null;
        private Thread threadSnBand = null;
        private volatile bool mSnBandCancel = false;
        private Thread threadPreTest = null;
        private volatile bool mPreTestCancel = false;
        private Thread threadInBI = null;
        private volatile bool mInBICancel = false;
        private int mSNCount = 0;
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
                    InitialStatus();
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
                }
                else
                {
                    if (!StartThread(run))
                        return false;
                    if (!ConnectToDevice(run))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }
        /// <summary>
        /// 初始化状态
        /// </summary>
        private void InitialStatus()
        {
            mHandBand = CSysPara.mVal.handBandSn;
            mSnLen = CSysPara.mVal.snLen;
             mSnSpec = CSysPara.mVal.snSpec;
            mScanReady = 0;
            mScanSlot = 0;

            statSnBand.doRun = EDoRun.空闲;
            statSnBand.idCard = string.Empty;
            for (int i = 0; i < statSnBand.serialNo.Count; i++)
            {
                statSnBand.serialNo[i] = string.Empty;
                statSnBand.result[i] = 0;
                statSnBand.failInfo[i] = string.Empty;
            }
            statPreTest.doRun = EDoRun.空闲;
            statPreTest.idCard = string.Empty;
            statPreTest.bReady = 0;
            for (int i = 0; i < statPreTest.serialNo.Count; i++)
            {
                statPreTest.serialNo[i] = string.Empty;
                statPreTest.result[i] = 0;
                statPreTest.failInfo[i] = string.Empty;
            }
            statInBI.doRun = EDoRun.空闲;
            statInBI.idCard = string.Empty;
            for (int i = 0; i < statInBI.serialNo.Count; i++)
            {
                statInBI.serialNo[i] = string.Empty;
                statInBI.result[i] = 0;
                statInBI.failInfo[i] = string.Empty;
            }
            if (CGlobal.User.pwrLevel[0] == 1)
            {
              setUIEnable(true);
               setUIEnable(true);
                uiSnInfo.setUIEnable(true);
            }
            else
            {
               setUIEnable(false);
                uiTestInfo.setUIEnable(false);
                uiSnInfo.setUIEnable(false);
            }


        }
        /// <summary>
        /// 连接测试设备
        /// </summary>
        /// <param name="er"></param>
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

                    if (ConnectToMes (ref er, connect))
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

                if (ConnectToPLC(ref er, connect))
                {
                    if (er != string.Empty)
                        runLog.Log(er, udcRunLog.ELog.Action);
                }
                else
                {
                    chkDev = false;
                    runLog.Log(er, udcRunLog.ELog.NG);
                }
                if (ConnectToIDCard(ref er, connect))
                {
                    if (er != string.Empty)
                        runLog.Log(er, udcRunLog.ELog.Action);
                }
                else
                {
                    chkDev = false;
                    runLog.Log(er, udcRunLog.ELog.NG);
                }
         
                if (connect && chkDev)
                {
                    if (CGJMES.CheckMySQL(ref er))
                    {
                        if (!CGJMES.deleteSnRecord(CSysPara.mVal.dayTimeOut, ref er))
                        {
                            chkDev = false;
                            runLog.Log(er, udcRunLog.ELog.Err);
                        }
                        else
                            runLog.Log("成功连接MES远端服务器【" + CSysPara.mVal.mySqlIp + "】", udcRunLog.ELog.Action);
                    }
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
        /// <param name="run"></param>
        /// <returns></returns>
        private bool StartThread(bool run = true)
        {
            try
            {
                if (run)
                {
                    if (threadPLC == null)
                    {
                        threadPLC = new CPLCPara(plcDB);
                        threadPLC.spinUp(devPLC);
                        runLog.Log("启动PLC监控线程.", udcRunLog.ELog.Action);
                    }
                    if (threadSnBand == null)
                    {
                        mSnBandCancel = false;
                        threadSnBand = new Thread(SnBandOnStart);
                        threadSnBand.IsBackground = true;
                        threadSnBand.Start();
                        runLog.Log("启动绑定检查位测试线程.", udcRunLog.ELog.Action);
                    }
           
                }
                else
                {
                    if (threadSnBand != null)
                    {
                        mSnBandCancel = true;
                        while (mSnBandCancel)
                            Application.DoEvents();
                        threadSnBand = null;
                    }
              
                    if (threadPLC != null)
                    {
                        threadPLC.spinDown();
                        threadPLC = null;
                        runLog.Log("PLC监控线程销毁.", udcRunLog.ELog.NG);
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
        /// <summary>
        /// 启动监控
        /// </summary>
        /// <param name="run"></param>
        #endregion

        #region 线程

        #region 绑定检查位
        private void SnBandOnStart()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        if (mSnBandCancel)
                            return;
                        string er = string.Empty;
                        if (!checkSystem(ref er, true))
                        {
                            if (er != string.Empty)
                                runLog.Log(er, udcRunLog.ELog.Err);
                            Thread.Sleep(C_ALARM_DELAY);
                            continue;
                        }
                        //治具未到位就绪
                        if (readPlc(EPLCINP.上机位治具到位需要读取ID卡) != CPLCCom.ON)
                        {
                           mScanReady = 0;
                           mScanSlot = 0;
                           statSnBand.doRun = EDoRun.空闲;
                            //if (readPlc(EPLCINP.上机位治具光电信号) != CPLCCom.ON)
                            //{
                                statSnBand.idCard = string.Empty;
                                for (int i = 0; i < statSnBand.serialNo.Count; i++)
                                {
                                    statSnBand.serialNo[i] = string.Empty;
                                    statSnBand.result[i] = 0;
                                }
                                SetClr();
                                mSNCount = 0;

                          // }
                            Thread.Sleep(C_THREAD_DELAY);
                            continue;
                        }
                        switch (statSnBand.doRun)
                        {
                            case EDoRun.空闲:
                                //治具到位就绪                             
                                string rIdCard = string.Empty;
                                //清除上一次ID卡
                                devIDCard.GetRecord(1, ref rIdCard, ref er);
                                CMath.delayMs(200);
                                if (!devIDCard.GetRecord(1, ref rIdCard, ref er))
                                {
                                    CMath.delayMs(500);
                                    if (!devIDCard.GetRecord(1, ref rIdCard, ref er))
                                    {
                                        statSnBand.doRun = EDoRun.报警;
                                        writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom .NG);
                                        SetRun(ESTATUS.异常报警, "读不到治具ID", statSnBand.serialNo, "读取治具ID失败");
                                        runLog.Log("<绑定检查位>读取治具ID失败:" + er, udcRunLog.ELog.NG);
                                        break;
                                    }
                                }
                                statSnBand.idCard = rIdCard;
                                for (int i = 0; i < statSnBand.serialNo.Count; i++)
                                    statSnBand.serialNo[i] = "";
                                SetRun(ESTATUS.就绪, statSnBand.idCard, statSnBand.serialNo);
                                runLog.Log("<绑定检查位>治具[" + statSnBand.idCard + "]到位就绪,等待检查.", udcRunLog.ELog.Action);
                                //检查治具使用状态
                                int ttNum = 0;
                                int failNum = 0;
                                Dictionary<int, string> snFailInfo = new Dictionary<int, string>();
                                if (!CGJMES.getFixtureUsedNum(statSnBand.idCard, ref ttNum, ref failNum, ref snFailInfo, ref er))
                                {
                                    statSnBand.doRun = EDoRun.报警;
                                    writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.NG);
                                   SetRun(ESTATUS.异常报警, statSnBand.idCard, statSnBand.serialNo, "获取该治具使用次数失败.");
                                    runLog.Log("<绑定检查位>获取治具[" + statSnBand.idCard + "]使用次数失败:" + er, udcRunLog.ELog.NG);
                                
                                    break;
                                }
                                uiSnInfo.ShowUUTNum(ttNum, failNum);
                                if (CSysPara.mVal.fixtureTimes != 0 && ttNum > CSysPara.mVal.fixtureTimes)
                                {
                                    statSnBand.doRun = EDoRun.报警;
                                    writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.NG);

                                    SetRun(ESTATUS.异常报警, statSnBand.idCard, statSnBand.serialNo, "该治具超过使用次数.");
                                    runLog.Log("<绑定检查位>治具[" + statSnBand.idCard + "]超过使用次数,请更换保养.", udcRunLog.ELog.NG);
                                    break;
                                }
                                List<string> labTip = new List<string>();
                                for (int i = 0; i < statSnBand.serialNo.Count; i++)
                                {
                                    if (snFailInfo.ContainsKey(i))
                                        labTip.Add(snFailInfo[i]);
                                    else
                                        labTip.Add("");
                                }
                                SetToolTip(labTip);
                                if (CSysPara.mVal.failTimes != 0 && failNum > CSysPara.mVal.failTimes)
                                {
                                    for (int i = 0; i < statSnBand.serialNo.Count; i++)
                                    {
                                        if (snFailInfo.ContainsKey(i))
                                            statSnBand.serialNo[i] = snFailInfo[i];
                                        else
                                            statSnBand.serialNo[i] = "";
                                    }
                                    statSnBand.doRun = EDoRun.报警;
                                    writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.NG);

                                    SetRun(ESTATUS.异常报警, statSnBand.idCard, statSnBand.serialNo, "该治具不良次数超过设置值.");
                                    runLog.Log("<绑定检查位>治具[" + statSnBand.idCard + "]不良次数超过设置值,请检查.", udcRunLog.ELog.NG);
                                    if (CGlobal.User.pwrLevel[0]==1)
                                    {
                                        if (MessageBox.Show("是否清除不良统计？", "确认消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            if (!CGJMES.setFixtureUsedNum(statSnBand.idCard, ref er, true))
                                            {

                                                statSnBand.doRun = EDoRun.报警;
                                                runLog.Log("<绑定检查位>治具[" + statSnBand.idCard + "]清除不良统计次数错误." + er , udcRunLog.ELog.NG);

                                            }
                                            else
                                                runLog.Log("<绑定检查位>治具[" + statSnBand.idCard + "]清除不良统计次数成功.请重新放置下治具清除异常", udcRunLog.ELog.OK);

                                        }

                                    }
                                    break;
                                }
                                //获取是否为空治具
                                int mFixIsNull = 0;
                                List<bool> mSnIsExist = new List<bool>();
                                for (int i = 0; i < statSnBand.serialNo.Count; i++)
                                    mSnIsExist.Add(true);
                                mFixIsNull = uiSnInfo.mFixIsNull;
                                setLabIsFixNull(mFixIsNull);
                                if (mFixIsNull == 1)
                                {
                                    if (!CGJMES.bandFlow(statSnBand.idCard, ref er, statSnBand.serialNo, mFixIsNull, RunModel.model, CGlobal.CFlow.flowGUID))
                                    {
                                        statSnBand.doRun = EDoRun.报警;
                                        writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.NG);

                                        runLog.Log("<绑定检查位>绑定治具产品条码失败:" + er, udcRunLog.ELog.NG);
                                        break;
                                    }
                                    SetRun(ESTATUS.空治具, statSnBand.idCard, statSnBand.serialNo);
                                    statSnBand.doRun = EDoRun.过站;
                                    writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.OK);

                                    runLog.Log("<绑定检查位>治具[" + statSnBand.idCard + "]绑定【空治具】,准备过站.", udcRunLog.ELog.OK);
                                    break;
                                }
                                //获取需要绑定条码位置
                                getSnIsBand(ref mSnIsExist);

                                bool isSpotCheck = uiSnInfo.mSpotCheckFix;
                                if (isSpotCheck)
                                {
                                   
                                    for (int i = 0; i < statSnBand.serialNo.Count; i++)
                                    {
                                        if (mSnIsExist[i])
                                            statSnBand.serialNo[i] =CSysPara .mVal .SpotCheckSn [i];
                                        else
                                            statSnBand.serialNo[i] = "";
                                    }
                                    //绑定点检治具
                                    if (!CGJMES.bandFlow(statSnBand.idCard, ref er, statSnBand.serialNo, mFixIsNull, "SpotCheckFix", CGlobal.CFlow.flowGUID))
                                    {
                                        statSnBand.doRun = EDoRun.报警;
                                        writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.NG);

                                        runLog.Log("<绑定检查位>绑定治具产品条码失败:" + er, udcRunLog.ELog.NG);
                                        break;
                                    }
                                    SetRun(ESTATUS.测试结束, statSnBand.idCard, statSnBand.serialNo);
                                    statSnBand.doRun = EDoRun.过站;
                                    writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.OK);

                                    runLog.Log("<绑定检查位>点检治具[" + statSnBand.idCard + "]绑定OK,准备过站.", udcRunLog.ELog.OK);
                                    break;
                                }
                         

                                //是否自动生成产品条码
                                if (!CSysPara.mVal.handBandSn)
                                {
                                    string sNowTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    for (int i = 0; i < statSnBand.serialNo.Count; i++)
                                    {
                                        if (mSnIsExist[i])
                                            statSnBand.serialNo[i] = sNowTime + (i + 1).ToString("D2");
                                        else
                                            statSnBand.serialNo[i] = "";
                                    }
                                    if (!CGJMES.bandFlow(statSnBand.idCard, ref er, statSnBand.serialNo, mFixIsNull, RunModel.model, CGlobal.CFlow.flowGUID))
                                    {
                                        statSnBand.doRun = EDoRun.报警;
                                        writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.NG);

                                        runLog.Log("<绑定检查位>绑定治具产品条码失败:" + er, udcRunLog.ELog.NG);
                                        break;
                                    }
                                   SetRun(ESTATUS.测试结束, statSnBand.idCard, statSnBand.serialNo);
                                    statSnBand.doRun = EDoRun.过站;
                                    writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.OK);

                                    runLog.Log("<绑定检查位>治具[" + statSnBand.idCard + "]绑定OK,准备过站.", udcRunLog.ELog.OK);
                                }
                                else
                                {
                                    mScanReady = 1;

                                    statSnBand.doRun = EDoRun.就绪;
                                    SetRun(ESTATUS.就绪, statSnBand.idCard, statSnBand.serialNo);
                                    runLog.Log("<绑定检查位>治具[" + statSnBand.idCard + "]到位就绪,等待扫描.", udcRunLog.ELog.Action);
                                }
                                break;
                            case EDoRun.就绪:
                                //if (CSysPara.mVal.conMes)
                                //{
                                //    string mModeName = string.Empty;
                                //    string mStation = string.Empty;
                                //    if (mSNCount != uiSnBand.mChkSerialNo.Count)
                                //    {
                                //        for (int i = mSNCount; i < uiSnBand.mChkSerialNo.Count; i++)
                                //        {
                                //            if (!MesSajet.Chk_SN(uiSnBand.mChkSerialNo[i], ref mModeName, ref mStation, ref er))
                                //            {
                                //                statSnBand.doRun = EDoRun.报警;
                                //                writePlc(EPLCOUT.上机扫码失败, 1);

                                //                runLog.Log("<绑定检查位>绑定治具产品条码:" + uiSnBand.mChkSerialNo[i] + "失败:" + er, udcRunLog.ELog.NG);
                                //                break;
                                //            }

                                //        }
                                //    }
                                //    mSNCount = uiSnBand.mChkSerialNo.Count;
                                //  }
                             
                                if (mScanReady == 2)
                                {
                                   
                                    for (int i = 0; i < statSnBand.serialNo.Count; i++)
                                    {
                                        statSnBand.serialNo[i] = mSerialNo[i];
                                        //做个上传处理，调试时群光把包装线的上机位在MES改成了SNCHECK站，有时需必须上传
                                        if (statSnBand.serialNo[i] != string.Empty)
                                        {
                                            if (CSysPara.mVal.PTconMes)
                                            {
                                                if (!MesSajet.Tran_SN(statSnBand.serialNo[i], "PASS", ref er))
                                                {
                                                    runLog.Log("上传条码" + statSnBand.serialNo[i] + "失败" + er, udcRunLog.ELog.NG);
                                                }
                                                else
                                                    runLog.Log("上传条码" + statSnBand.serialNo[i] + "成功", udcRunLog.ELog.Content);
                                                CMath.WaitMs(CSysPara.mVal.MesDelatTime);
                                            }
                                        }
                                    }
                                    if (!CGJMES.bandFlow(statSnBand.idCard, ref er, statSnBand.serialNo, 0, RunModel.model, CGlobal.CFlow.flowGUID))
                                    {
                                        statSnBand.doRun = EDoRun.报警;
                                        writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.NG);

                                        runLog.Log("<绑定检查位>绑定治具产品条码失败:" + er, udcRunLog.ELog.NG);
                                        break;
                                    }


                                    if (!CGJMES.updateFixResult(CGlobal.CFlow.flowId, statSnBand.idCard,
                                                                statSnBand.serialNo, statSnBand.result, ref er,
                                                                CGlobal.CFlow.flowGUID, "", true))
                                    {
                                        statSnBand.doRun = EDoRun.报警;
                                        runLog.Log("<绑定检查位>更新测试结果失败:" + er, udcRunLog.ELog.NG);
                                        Thread.Sleep(C_THREAD_DELAY);
                                        break;
                                    }
                                    //记录产品信息
                                    statSnBand.endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                    statSnBand.testTime = CMath.DifTime(statSnBand.startTime);
                                    List<CGJMES.CSnRecord> snRecord = new List<CGJMES.CSnRecord>();
                                    for (int i = 0; i < statSnBand.serialNo.Count; i++)
                                    {
                                        if (statSnBand.serialNo[i] != "")
                                        {
                                            CGJMES.CSnRecord snItem = new CGJMES.CSnRecord();
                                            snItem.serialNo = statSnBand.serialNo[i];
                                            snItem.idCard = statSnBand.idCard;
                                            snItem.slotNo = i + 1;
                                            snItem.statId = CGlobal.CFlow.flowId +1;
                                            snItem.statName = "BURNIN";
                                            snItem.startTime = statSnBand.startTime;
                                            snItem.endTime = statSnBand.endTime;
                                            snItem.testResult = statSnBand.result[i];
                                            snItem.testTime = statSnBand.testTime;
                                            snRecord.Add(snItem);
                                        }
                                    }
                                    if (!CGJMES.recordSnFlow(snRecord, ref er))
                                    {
                                        statSnBand.doRun = EDoRun.报警;
                                        runLog.Log("<绑定检查位>更新条码信息失败:" + er, udcRunLog.ELog.NG);
                                        Thread.Sleep(C_THREAD_DELAY);
                                        break;
                                    }
                                    if (!CGJMES.updateStatTTNum(statSnBand.idCard, CGlobal.CFlow.flowId,
                                                                statSnBand.serialNo, statSnBand.result, ref er))
                                    {
                                        runLog.Log("记录治具槽位测试状态:" + er, udcRunLog.ELog.NG);
                                    }
                                   SetRun(ESTATUS.测试结束, statSnBand.idCard, statSnBand.serialNo);
                                    statSnBand.doRun = EDoRun.过站;
                                    writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.OK);

                                    mSNCount =0;

                                    runLog.Log("<绑定检查位>治具[" + statSnBand.idCard + "]绑定OK,准备过站.", udcRunLog.ELog.OK);
                                }
                                break;
                            default:
                                break;
                        }
                        Thread.Sleep(C_THREAD_DELAY);
                    }
                    catch (Exception ex)
                    {
                        runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
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
                mSnBandCancel = false;
                runLog.Log("绑定检查位测试线程销毁.", udcRunLog.ELog.NG);
            }
        }
        #endregion


        #region 系统方法
        /// <summary>
        /// 检查系统运行状态
        /// </summary>
        /// <param name="bShow"></param>
        /// <returns></returns>
        private bool checkSystem(ref string er, bool bShow = false)
        {
            try
            {
                er = string.Empty;

                if (!threadPLC.mConStatus)
                {
                    if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                        C_SYS_ALARM_COUNT++;
                    else
                    {
                        C_SYS_ALARM_COUNT = 0;
                        runLog.Log("PLC通信异常1,请检查PLC线路?", udcRunLog.ELog.NG);
                    }
                    return false;
                }
                if (!threadPLC.mConOpStat)
                {
                    if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                        C_SYS_ALARM_COUNT++;
                    else
                    {
                        C_SYS_ALARM_COUNT = 0;
                        runLog.Log("PLC通信异常2,请检查PLC线路?", udcRunLog.ELog.NG);
                    }
                    return false;
                }
                if (threadPLC.mStatus != CPLCPara.EStatus.运行)
                {
                    if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                        C_SYS_ALARM_COUNT++;
                    else
                    {
                        C_SYS_ALARM_COUNT = 0;
                        runLog.Log("PLC线程未运行,请检查软件是否启动线程?", udcRunLog.ELog.NG);
                    }
                    return false;
                }
                //系统运行状态
                if (readPlc(EPLCINP.PLC系统运行) == CPLCCom.OFF)
                {
                    if (bShow)
                    {
                        if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                            C_SYS_ALARM_COUNT++;
                        else
                        {
                            C_SYS_ALARM_COUNT = 0;
                            runLog.Log("PLC处于未运行状态,请启动PLC.", udcRunLog.ELog.NG);
                        }
                    }
                    return false;
                }
                if (readPlc(EPLCINP.PLC异常报警) == CPLCCom.ON)
                {
                    if (bShow)
                    {
                        if (C_SYS_ALARM_COUNT < C_SYS_ALARM_LIMIT)
                            C_SYS_ALARM_COUNT++;
                        else
                        {
                            C_SYS_ALARM_COUNT = 0;
                            runLog.Log("PLC异常报警,请查看PLC触摸屏.", udcRunLog.ELog.NG);
                        }
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
        /// <summary>
        /// 延时等待
        /// </summary>
        /// <param name="DelayMs"></param>
        private void MSWait(int DelayMs)
        {
            int waitTimes = System.Environment.TickCount;
            while (System.Environment.TickCount - waitTimes < DelayMs)
            {
                Application.DoEvents();
            }
        }
        #endregion

        #endregion

        #region 委托
        private delegate void setLabIsFixNullHandler(int isNull);
        /// <summary>
        /// 设置空治具
        /// </summary>
        /// <param name="isNull"></param>
        private void setLabIsFixNull(int isNull)
        {
            if (this.InvokeRequired)
                this.Invoke(new setLabIsFixNullHandler(setLabIsFixNull), isNull);
            else
            {
                if (isNull == 1)
                {
                    labBand.BackColor = Color.Red;
                }
                else
                {
                    labBand.BackColor = Button.DefaultBackColor;
                }
            }
        }
        #endregion



        #region 上机扫条码

        #region 枚举
        public enum ESTATUS
        {
            空闲,
            就绪,
            测试中,
            测试结束,
            异常报警,
            空治具
        }
        #endregion

        #region 字段
        // private const int C_SLOT_MAX = 8;
        private Label[] labUUT = null;
        private CheckBox[] labNo = null;
        private Label[] labSn = null;
        private bool clrFailWait = false;
        private bool handBand = false;
        private int scanReady = 0;
        private int scanSlot = 0;
        private List<string> serialList = new List<string>();
        private int snLen = 0;
        private string snSpec = string.Empty;
        #endregion

        #region 属性
        /// <summary>
        /// 解除报警
        /// </summary>
        public bool mClrFailWait
        {
            set
            {
                clrFailWait = value;
                SetFailWait(clrFailWait);
            }
            get { return clrFailWait; }
        }
        /// <summary>
        /// 人工扫描条码
        /// </summary>
        public bool mHandBand
        {
            set
            {
                handBand = value;
                if (handBand)
                {
                    txtSnPress.Enabled = true;
                    txtSnPress.Focus();
                }
                else
                    txtSnPress.Enabled = false;

            }
            get { return handBand; }
        }
        /// <summary>
        /// 启动扫描:0->未就绪 1:就绪 2：扫描结束
        /// </summary>
        public int mScanReady
        {
            set
            {
                scanReady = value;
                if (scanReady == 1)
                    serialList.Clear();
            }
            get
            {
                return scanReady;
            }
        }
        /// <summary>
        /// 扫描条码位置
        /// </summary>
        public int mScanSlot
        {
            set
            {
                scanSlot = value;
            }

            get
            {
                return scanSlot;
            }
        }
        /// <summary>
        /// 条码长度
        /// </summary>
        public int mSnLen
        {
            set { snLen = value; }
            get { return snLen; }
        }
        /// <summary>
        /// 条码规则
        /// </summary>
        public string mSnSpec
        {
            set { snSpec = value; }
            get { return snSpec; }
        }

        public List<string> mChkSerialNo
        {
            get
            {
                return serialList;
            }
        }
        /// <summary>
        /// 返回产品条码
        /// </summary>
        public List<string> mSerialNo
        {
            get
            {
                List<string> snList = new List<string>();
                for (int i = 0; i < labSn.Length; i++)
                    snList.Add(labSn[i].Text);
                return snList;
            }
        }
        #endregion

        #region 构造函数
        //public udcFixtureBand()
        //{
        //    InitializeComponent();

        //    IntialControl();

        //    SetDoubleBuffered();
        //}
        /// <summary>
        /// 绑定控件
        /// </summary>
        //private void IntialControl1()
        //{
        

        //    //是否显示提示信息
        //    tlTip.Active = true;
        //    //是否显示提示信息，当窗体没有获得焦点时
        //    tlTip.ShowAlways = true;
        //    //工具提示”窗口显示之前，鼠标指针必须在控件内保持静止的时间（以毫秒计）
        //    tlTip.InitialDelay = 200;
        //    // 提示信息刷新时间 
        //    tlTip.ReshowDelay = 300;
        //    //提示信息延迟时间
        //    tlTip.AutomaticDelay = 200;
        //    // 提示信息弹出时间
        //    tlTip.AutoPopDelay = 10000;
        //    // 提示信息
        //    tlTip.ToolTipTitle = "产品信息";
        //}
    

        #region 面板回调函数
        private void udcFixture_Load(object sender, EventArgs e)
        {
            clrFailWait = false;
            SetFailWait(false);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否已确定不良,需继续下一步测试?", "确定不良", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                clrFailWait = false;
                SetFailWait(false);
            }
        }
        private void txtSnPress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                TextBox txtBox = (TextBox)sender;
                string serialNo = txtBox.Text;
                if (scanReady == 0)
                {
                    labStatus.Text = "治具未到位就绪,请等待";
                    labStatus.ForeColor = Color.Red;
                    txtSnPress.SelectAll();
                    e.Handled = true;
                    return;
                }
                if (scanReady == 2)
                {
                    labStatus.Text = "条码扫描完毕,等待过站";
                    labStatus.ForeColor = Color.Blue;
                    txtSnPress.SelectAll();
                    e.Handled = true;
                    return;
                }
                if (!handBand)
                {
                    labStatus.Text = "设置为【自动生成条码】,不需扫描";
                    labStatus.ForeColor = Color.Red;
                    txtSnPress.SelectAll();
                    e.Handled = true;
                    return;
                }
                if (snLen != 0 && snLen != serialNo.Length)
                {
                    labStatus.Text = "条码长度错误:" + serialNo.Length.ToString();
                    labStatus.ForeColor = Color.Red;
                    txtSnPress.SelectAll();
                    e.Handled = true;
                    return;
                }
                if (snSpec != string.Empty && snSpec != serialNo.Substring(0, snSpec.Length))
                {
                    labStatus.Text = "条码规则[" + snSpec + "]错误";
                    labStatus.ForeColor = Color.Red;
                    txtSnPress.SelectAll();
                    e.Handled = true;
                    return;
                }
                if (serialList.Contains(serialNo))
                {
                    labStatus.Text = "条码重复扫描,请重新扫描";
                    labStatus.ForeColor = Color.Red;
                    txtSnPress.SelectAll();
                    e.Handled = true;
                    return;
                }
                if (statSnBand.doRun ==EDoRun.报警)
                {     
                    runLog.Log("<绑定检查位>绑定治具产品条码异常错误，请清除后继续！", udcRunLog.ELog.NG);
                    return;
                }
                if (CSysPara.mVal.conMes)
                {
                    string mModeName = string.Empty;
                    string mStation = string.Empty;
                    string er = string.Empty;
                    
                    if (MesSajet.Chk_SN(serialNo, ref mModeName, ref mStation, ref er))
                    {
                            
                        if (!CSysPara.mVal.MesStation.Contains(mStation))
                        {
                            statSnBand.doRun = EDoRun.报警;
                            writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom .ScanFail );
                            labStatus.ForeColor = Color.Red;
                            
                            txtSnPress.SelectAll();
                            e.Handled = true;
                            labStatus.Text = "条码检查站位错误" + mStation;
                            runLog.Log("<绑定检查位>绑定治具产品条码:" + serialNo + "站位错误:" + mStation , udcRunLog.ELog.NG);
                            MessageBox.Show("条码检查错误，请检查！", "条码检查错误，请检查！", MessageBoxButtons.OK);

                            return;
                        }

                    }
                    else
                    {
                        if (er.Contains("EMP ERR")) //做一个重新登录
                        {
                            if (MesSajet.Login("222", "222", ref er))
                            {
                                if (MesSajet.Chk_SN(serialNo, ref mModeName, ref mStation, ref er))
                                {
                                      
                                    if (!CSysPara.mVal.MesStation.Contains(mStation))
                                    {
                                        statSnBand.doRun = EDoRun.报警;
                                        writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.ScanFail);
                                        labStatus.ForeColor = Color.Red;
                                        txtSnPress.SelectAll();
                                        e.Handled = true;
                                        labStatus.Text = "条码检查站位错误" + mStation;
                                        runLog.Log("<绑定检查位>绑定治具产品条码:" + serialNo + "站位错误:" + mStation, udcRunLog.ELog.NG);
                                        MessageBox.Show("条码检查错误，请检查！", "条码检查错误，请检查！", MessageBoxButtons.OK);
                                        return;
                                    }

                                }
                                else
                                {
                                    writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.ScanFail);
                                    labStatus.ForeColor = Color.Red;
                                    txtSnPress.SelectAll();
                                    e.Handled = true;
                                    labStatus.Text = "条码检查连接MES错误，请检查连接" + er;
                                    runLog.Log("<绑定检查位>绑定治具产品条码:" + serialNo + "站位错误:" + er, udcRunLog.ELog.NG);
                                    return;
                                }


                            }
                            else
                            {
                                writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.ScanFail);
                                labStatus.ForeColor = Color.Red;
                                txtSnPress.SelectAll();
                                e.Handled = true;
                                labStatus.Text = "条码检查连接MES错误，请检查连接" + er;
                                runLog.Log("<绑定检查位>绑定治具产品条码:" + serialNo + "站位错误:" + er, udcRunLog.ELog.NG);
                                return;
                            }

                        }
                        else
                        {
                            writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.ScanFail);
                            labStatus.ForeColor = Color.Red;
                            txtSnPress.SelectAll();
                            e.Handled = true;
                            labStatus.Text = "条码检查连接MES错误，请检查连接" + er;
                            runLog.Log("<绑定检查位>绑定治具产品条码:" + serialNo + "站位错误:" + er, udcRunLog.ELog.NG);
                            return;
                        }
                    }


                    if (mModeName != RunModel.model)
                    {
                        statSnBand.doRun = EDoRun.报警;
                        writePlc(EPLCOUT.上机位ID卡读取结果, CPLCCom.ScanFail);
                        labStatus.ForeColor = Color.Red;
                        txtSnPress.SelectAll();
                        e.Handled = true;
                        labStatus.Text = "条码"+ mSerialNo +"机种名信息错误！" ;
                        runLog.Log("<绑定检查位>条码" + serialNo + "在MES中获取的机种名：" + mModeName + "与本地设置的机种名：" + RunModel.model + "不一致，请检查！", udcRunLog.ELog.NG);
                        MessageBox.Show("条码检查错误，请检查！", "条码检查错误，请检查！", MessageBoxButtons.OK);
                        return;
                    }

                }
                if (statSnBand.doRun == EDoRun.报警)
                {
                    runLog.Log("<绑定检查位>绑定治具产品条码异常错误，请清除后继续！", udcRunLog.ELog.NG);
                    return;
                }
                //写入条码
                for (int i = mScanSlot ; i < labSn.Length; i++)
                {
                    if (labNo[i].Checked)
                    {
                        mScanSlot = i+1;
                        serialList.Add(serialNo);
                        labUUT[i].ImageKey = "PASS";
                        labSn[i].Text = serialNo;
                        labSn[i].ForeColor = Color.Blue;
                      
                        break;
                    }
                }
                txtSnPress.Text = "";
                int nextSlot = -1;
                for (int i = mScanSlot; i < labSn.Length; i++)
                {
                    if (labNo[i].Checked)
                    {
                        nextSlot = i;
                        break;
                    }
                }
                if (nextSlot != -1)
                {
                    labStatus.Text = "等待扫描位置【" + (nextSlot + 1).ToString("D2") + "】";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "条码扫描完毕,等待过站";
                    labStatus.ForeColor = Color.Blue;
                    mScanReady = 2;
                    mScanSlot = 0;
                }
          
                txtSnPress.Focus();
            }
        }
        #endregion

        #region 委托
        private delegate void SetRunHandler(ESTATUS status, string idCard, List<string> serialNo, string alarmInfo);
        private delegate void SetToolTipHandler(List<string> uutInfo);
        private delegate void SetClrHandler();
        private delegate void SetStatusHandler(string status, bool bNG);
        private delegate void SetFailWaitHandler(bool clrFail);
        #endregion

        #region 方法
        /// <summary>
        /// 获取绑定条码位置
        /// </summary>
        /// <param name="rIsExist"></param>
        public void getSnIsBand(ref List<bool> rIsExist)
        {
            for (int i = 0; i < labNo.Length; i++)
                rIsExist[i] = labNo[i].Checked;
        }
        /// <summary>
        /// 设置治具状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="idCard"></param>
        /// <param name="serialNo"></param>
        /// <param name="result"></param>
        /// <param name="alarmInfo"></param>
        public void SetRun(ESTATUS status, string idCard, List<string> serialNo, string alarmInfo = "")
        {
            if (this.InvokeRequired)
                this.Invoke(new SetRunHandler(SetRun), status, idCard, serialNo, alarmInfo);
            else
            {
                switch (status)
                {
                    case ESTATUS.空闲:
                        panelUUT.Visible = false;
                        labIdCard.Text = "";
                        labStatus.Text = "等待治具到位..";
                        labStatus.ForeColor = Color.Blue;
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            labSn[i].Text = "";
                        }
                        break;
                    case ESTATUS.空治具:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Blue;
                        labStatus.Text = "空治具准备过站..";
                        labStatus.ForeColor = Color.Blue;
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            labSn[i].Text = "";
                            labUUT[i].ImageKey = null;
                        }
                        panelUUT.Visible = true;
                        break;
                    case ESTATUS.就绪:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Blue;
                        labStatus.Text = "治具到位就绪,等待扫描..";
                        labStatus.ForeColor = Color.Blue;
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            labUUT[i].ImageKey = null;
                            labSn[i].Text = serialNo[i];
                            labSn[i].ForeColor = Color.Black;
                        }
                        panelUUT.Visible = true;
                        break;
                    case ESTATUS.测试中:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Blue;
                        labStatus.Text = "治具到位就绪,产品测试中..";
                        labStatus.ForeColor = Color.Blue;
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            if (serialNo[i] != "")
                                labUUT[i].ImageKey = "READY";
                            else
                                labUUT[i].ImageKey = null;
                        }
                        panelUUT.Visible = true;
                        break;
                    case ESTATUS.测试结束:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Blue;
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            labSn[i].Text = serialNo[i];
                            if (serialNo[i] != "")
                            {
                                labUUT[i].ImageKey = "PASS";
                                labSn[i].ForeColor = Color.Blue;
                            }
                            else
                            {
                                labUUT[i].ImageKey = null;
                                labSn[i].ForeColor = Color.Black;
                            }
                        }
                        labStatus.Text = "条码绑定OK,准备过站..";
                        labStatus.ForeColor = Color.Blue;
                        panelUUT.Visible = true;
                        break;
                    case ESTATUS.异常报警:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Red;
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            labSn[i].Text = serialNo[i];
                            if (serialNo[i] == "")
                            {
                                labUUT[i].ImageKey = null;
                                labSn[i].ForeColor = Color.Black;
                            }
                            else
                            {
                                labUUT[i].ImageKey = "FAIL";
                                labSn[i].ForeColor = Color.Red;
                            }
                        }
                        labStatus.Text = alarmInfo;
                        labStatus.ForeColor = Color.Red;
                        panelUUT.Visible = true;
                        break;
                    default:
                        break;
                }
                this.Refresh();
            }
        }
        /// <summary>
        /// 设置提示信息
        /// </summary>
        /// <param name="uutInfo"></param>
        public void SetToolTip(List<string> uutInfo)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetToolTipHandler(SetToolTip), uutInfo);
            else
            {
                tlTip.RemoveAll();
                for (int i = 0; i < uutInfo.Count; i++)
                {
                    if (uutInfo[i] != "")
                        tlTip.SetToolTip(labUUT[i], uutInfo[i]);
                }
            }
        }
        /// <summary>
        /// 置空闲状态
        /// </summary>
        public void SetClr()
        {
            if (this.InvokeRequired)
                this.Invoke(new SetClrHandler(SetClr));
            else
            {
                labIdCard.Text = "";
                labStatus.Text = "等待治具到位..";
                labStatus.ForeColor = Color.Blue;
                panelUUT.Visible = false;
                for (int i = 0; i < labSn.Length; i++)
                {
                    labSn[i].Text = "";
                }
                tlTip.RemoveAll();
            }
        }
        /// <summary>
        /// 设置测试状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="bNG"></param>
        public void SetStatus(string status, bool bNG = false)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetStatusHandler(SetStatus), status, bNG);
            else
            {
                labStatus.Text = status;
                if (!bNG)
                    labStatus.ForeColor = Color.Blue;
                else
                    labStatus.ForeColor = Color.Red;
            }
        }
        /// <summary>
        /// 不良确定
        /// </summary>
        /// <param name="clrFail"></param>
        public void SetFailWait(bool clrFail)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetFailWaitHandler(SetFailWait), clrFail);
            else
            {
                if (!clrFail)
                {
                    panel3.ColumnStyles[3].SizeType = SizeType.Absolute;
                    panel3.ColumnStyles[3].Width = 2;
                    btnOK.Visible = false;
                }
                else
                {
                    panel3.ColumnStyles[3].SizeType = SizeType.Absolute;
                    panel3.ColumnStyles[3].Width = 60;
                    btnOK.Visible = true;
                }
            }
        }

        private delegate void setUIEnableHandler(bool enabled);
        public void setUIEnable(bool enabled)
        {
            if (this.InvokeRequired)
                this.Invoke(new setUIEnableHandler(setUIEnable), enabled);
            else
            {
                if (enabled)
                {
                    for (int i = 0; i < labNo.Length; i++)
                        labNo[i].Enabled = true;
                }
                else
                {
                    for (int i = 0; i < labNo.Length; i++)
                        labNo[i].Enabled = false;
                }
            }
        }
        #endregion



   

      

        # endregion

    }
}
        # endregion