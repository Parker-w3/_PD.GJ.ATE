//#define USER_DEBUG
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
using GJ.UI;
using GJ.Mes;
using GJ.Para.Base;
using GJ.Para.Udc;
using GJ.Para.Udc.HIPOT;
using GJ.Dev.HIPOT;
using GJ.Dev.RemIO;
using GJ.Dev.TCP;
using GJ.Dev .PLC ;
namespace GJ.Para.HIPOT
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

        }
        #endregion

        #region 参数常量
        private int C_SLOT_MAX = 8;
        private int C_DEV_CHAN_MAX = 8;
        #endregion

        #region 参数路径
        private string iniFile = "sysLog\\" + CGlobal.CFlow.flowGUID + ".ini";
        private string sysFile = "sysLog\\" + CGlobal.CFlow.flowGUID + ".xml";
        private string sysDB = "DBLog\\" + CGlobal.CFlow.flowGUID + ".accdb";        
        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {

            udcHpInfo = new udcHPInfo();
            udcHpInfo.Dock = DockStyle.Fill;
            udcHpInfo.OnBtnArgs.OnEvent += new COnEvent<udcHPInfo.COnBtnClickArgs>.OnEventHandler(OnHpInfoBtn);  
            panel2.Controls.Add(udcHpInfo, 0, 0);

            udcHpResult = new udcHPResult();
            udcHpResult.Dock = DockStyle.Fill;
            udcHpResult.Margin = new Padding(0);
            tabPage1.Controls.Add(udcHpResult);

            udcHpData = new udcHPData();
            udcHpData.Dock = DockStyle.Fill;
            udcHpData.Margin = new Padding(0);
            tabPage2.Controls.Add(udcHpData);

            runLog = new udcRunLog();
            runLog.mSaveName = "runLog"; 
            runLog.Dock = DockStyle.Fill;
            panel2.Controls.Add(runLog, 1, 0);

            tcpLog = new udcRunLog();
            tcpLog.mTitle = "HIPOT TCP/IP";
            tcpLog.mSaveName = "tcpLog1";
            tcpLog.Dock = DockStyle.Fill;
            tcpLog.Margin = new Padding(0);
            panel3.Controls.Add(tcpLog, 0, 0);

            tcpShow = new udcTcpRecv();
            tcpShow.Dock = DockStyle.Fill;
            tcpShow.Margin = new Padding(0);
            panel3.Controls.Add(tcpShow, 1, 0);


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
            panelModelInfo.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panelModelInfo, true, null);
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

            if (CSysPara.mVal.C_HIPOT_MAX == 2)
                C_SLOT_MAX = 8;
            else
                C_SLOT_MAX = 8;

            devHP = new HYESA140[2];

            statHP = new CStat[2];
            statHP[0] = new CStat(0, "<HP前工位>", C_SLOT_MAX);
            statHP[1] = new CStat(0, "<HP后工位>", C_SLOT_MAX);

            uutHP = new CDev[2];
            uutHP[0] = new CDev(0, "<HP仪器1>", C_DEV_CHAN_MAX);
            uutHP[1] = new CDev(1, "<HP仪器2>", C_DEV_CHAN_MAX);
          //  uutHP = new CDev(0, "<ATE" + CSysPara.mVal.StatHipotMode.ToString() + "测试设备>", C_SLOT_MAX);
            tcpName = "HIPOT" + (CSysPara.mVal.StatHipotMode+1).ToString();
        }
        #endregion

        #region 面板操作

        #region 面板控件
        private udcHPInfo udcHpInfo = null;
        private udcHPResult udcHpResult = null;
        private udcHPData udcHpData = null;        
        private udcRunLog runLog = null;
        private udcRunLog tcpLog = null;
        private udcTcpRecv tcpShow = null;
        #endregion

        private void udcRun_Load(object sender, EventArgs e)
        {
            if (udcHpData != null && udcHpInfo.mRunModel != null)
            {
                runModel = udcHpInfo.mRunModel;
                udcHpData.refreshUI(runModel.step);
                showModelInfo();
            }
        }

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
        /// HP选机种和调式按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHpInfoBtn(object sender, udcHPInfo.COnBtnClickArgs e)
        {
            string er = string.Empty;

            int idNo = 0;
           
            switch (e.idNo)
            {
                case 0:   //调式高压机1

                    idNo = 0;

                    if (HPStartWorker1.IsBusy)
                    {
                        MessageBox.Show("高压机1处于测试,请停止.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
#if !USER_DEBUG
                    if (string.IsNullOrEmpty(udcHpInfo.mRunModel.model))
                    {
                        MessageBox.Show("Please Choose One Model For TEST!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (ConnectToHP(ref er))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        runLog.Log(er, udcRunLog.ELog.NG);
                        udcHpInfo.SetDebugBtn(idNo, 0);
                        return;
                    }
                    if (ConnectToIO (ref er))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        runLog.Log(er, udcRunLog.ELog.NG);
                        udcHpInfo.SetDebugBtn(idNo, 0);
                        return;
                    }
                    runLog.Log("开始加载HIPOT参数[" + udcHpInfo.mRunModel.model + "],请稍等..", udcRunLog.ELog.Action);
                  //  string er = string.Empty;
                    if (!SetHipotPara(idNo,udcHpInfo.mRunModel.model, udcHpInfo.mRunModel.step, ref er))
                    {
                        runLog.Log("加载HIPOT参数[" + udcHpInfo.mRunModel.model + "]失败:[" + er + "]", udcRunLog.ELog.NG);
                        mhpIniPara = 2;
                    }
                    else
                    {
                        runLog.Log("成功加载HIPOT参数[" + udcHpInfo.mRunModel.model + "].", udcRunLog.ELog.Action);
                        mhpIniPara = 1;
                    }
#endif 
                    uutHP[idNo].doRun = EDoRun.就绪;  
                    uutHP[idNo].curFixStep = -1;
                    string sNowTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");                    
                    for (int i = 0; i < uutHP[idNo].chanNum; i++)
			        {
                        uutHP[idNo].serialNo[i] = sNowTime1 + (i + 1).ToString("D2");
                        uutHP[idNo].result[i] = (int)EResult.空闲;
                        uutHP[idNo].HpResult[i] = new CHPSetting.CStepVal();
			        }
                    udcHpResult.SetFix("0000000000", uutHP[idNo].serialNo, idNo);
                    udcHpResult.clrResult(idNo);
                    udcHpData.clrData();
                    mHPDebug[idNo] = true;
                    if(!HPStartWorker1.IsBusy)  
                       HPStartWorker1.RunWorkerAsync(); 
                    break;
                case 1:   //调式高压机2

                    if (CSysPara.mVal.C_HIPOT_MAX != 2)
                    {
                        MessageBox.Show("系统设置只有1台高压机", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (string.IsNullOrEmpty(udcHpInfo.mRunModel.model))
                    {
                        MessageBox.Show("Please Choose One Model For TEST!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    idNo = 1;

                    if (HPStartWorker2.IsBusy)
                    {
                        MessageBox.Show("高压机2处于测试,请停止.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
#if !USER_DEBUG

                  

                    if (ConnectToHP(ref er))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        runLog.Log(er, udcRunLog.ELog.NG);
                        udcHpInfo.SetDebugBtn(idNo, 0);
                        return;
                    }
                    if (ConnectToIO (ref er))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        runLog.Log(er, udcRunLog.ELog.NG);
                        udcHpInfo.SetDebugBtn(idNo, 0);
                        return;
                    }
                    runLog.Log("开始加载HIPOT参数[" + udcHpInfo.mRunModel.model + "],请稍等..", udcRunLog.ELog.Action);
                  //  string er = string.Empty;
                    if (!SetHipotPara(idNo,udcHpInfo.mRunModel.model, udcHpInfo.mRunModel.step, ref er))
                    {
                        runLog.Log("加载HIPOT参数[" + udcHpInfo.mRunModel.model + "]失败:[" + er + "]", udcRunLog.ELog.NG);
                        mhpIniPara = 2;
                    }
                    else
                    {
                        runLog.Log("成功加载HIPOT参数[" + udcHpInfo.mRunModel.model + "].", udcRunLog.ELog.Action);
                        mhpIniPara = 1;
                    }
#endif 
                    uutHP[idNo].doRun = EDoRun.就绪;  
                    uutHP[idNo].curFixStep = -1;
                    string sNowTime2 = DateTime.Now.ToString("yyyyMMddHHmmss");                    
                    for (int i = 0; i < uutHP[idNo].chanNum; i++)
			        {
                        uutHP[idNo].serialNo[i] = sNowTime2 + (i + 1).ToString("D2");
                        uutHP[idNo].result[i] = (int)EResult.空闲;
                        uutHP[idNo].HpResult[i] = new CHPSetting.CStepVal();
			        }
                    udcHpData.setTestVal(uutHP[idNo].serialNo, uutHP[idNo].HpResult, idNo,CSysPara.mVal.C_HIPOT_MAX);
                    udcHpResult.SetFix("0000000000", uutHP[idNo].serialNo, idNo);
                    udcHpResult.clrResult(idNo);
                    udcHpData.clrData();
                    mHPDebug[idNo] = true;
                    HPStartWorker2.RunWorkerAsync(); 
                    break;
                case 2:   //选机种
                    runModel = udcHpInfo.mRunModel;
                    udcHpInfo.SetStatus(udcHPInfo.ERun.Initialize);
                    udcHpData.refreshUI(runModel.step);
                    udcHpInfo.SetStatus(udcHPInfo.ERun.Idle);
                    showModelInfo();
                    break;
                default:
                    break;
            }
        }

        private void showModelInfo()
        {
            try
            {
                panelModelInfo.Controls.Clear();
                for (int i = 0; i < runModel.step.Count; i++)
                {
                    Label labmsg = new Label();
                    labmsg.Dock = DockStyle.Fill;
                    labmsg.TextAlign = ContentAlignment.MiddleCenter;
                    labmsg.Text = runModel.step[i].name.ToString();
                    labmsg.ForeColor = Color.Blue;
                    panelModelInfo.Controls.Add(labmsg, i*2, 0);


                    for (int j = 0; j < runModel.step[i].para.Count; j++)
                    {
                        Label labinfo = new Label();
                        labinfo.Dock = DockStyle.Fill;
                        labinfo.TextAlign = ContentAlignment.MiddleCenter;
                        labinfo.Text = runModel.step[i].para[j].name;
                        panelModelInfo.Controls.Add(labinfo, i*2, j + 1);


                        Label labstepVal = new Label();
                        labstepVal.Dock = DockStyle.Fill;
                        labstepVal.TextAlign = ContentAlignment.MiddleLeft;
                        labstepVal.ForeColor = Color.Blue;
                        labstepVal.Text = runModel.step[i].para[j].setVal.ToString() + runModel.step[i].para[j].unitDes;
                        panelModelInfo.Controls.Add(labstepVal, i*2 + 1, j + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                runLog.Log("显示高压机种信息失败："+ ex.ToString (), udcRunLog.ELog.NG);
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

        #region 测试参数
        private enum EResult
        { 
          报警=-2,
          空闲=-1,
          良品=0,
          不良=1        
        }
        private enum EDoRun
        {
            空闲,
            到位,
            就绪,
            测试,
            结束,
            过站,
            报警
        }       
        /// <summary>
        /// 工位状态
        /// </summary>
        private class CStat
        {
            public CStat(int statId, string statName = "", int slotNum = 8)
            { 
              this.statId=statId;
              this.statName=statName;
              this.slotNum = slotNum;
              for (int i = 0; i < slotNum; i++)
              {
                  serialNo.Add("");
                  internalSN.Add("");
                  result.Add(0);
                  HpResult.Add(new CHPSetting.CStepVal());
              }
            }        
            public int statId = 0;
            public string statName = "";
            public int slotNum = 8;
            public string modelName = string.Empty;
            public string idCard = string.Empty;
            public List<string> serialNo = new List<string>();
            public List<int> result = new List<int>();
            public List<string> internalSN = new List<string>();
            public int ready = 0;
            public EDoRun doRun = EDoRun.空闲;
            public int startTimes = 0;
            public int runTimes = 0;
            public List<CHPSetting.CStepVal> HpResult = new List<CHPSetting.CStepVal>();
        }
        /// <summary>
        /// 测试设备
        /// </summary>
        private class CDev
        {
            public CDev(int devId, string devName = "", int chanNum = 8)
            {
                this.devId = devId;
                this.devName = devName;
                this.chanNum = chanNum; 
                for (int i = 0; i < chanNum; i++)
                {
                    serialNo.Add("");
                    result.Add(0);
                    HpResult.Add(new CHPSetting.CStepVal());
                }
            }
            public int chanNum = 8;    
            public int devId = 0;
            public string devName = "";                   
            public List<string> serialNo = new List<string>();
            public List<int> result = new List<int>();
            public List<string> failInfo = new List<string>();
            public EDoRun doRun = EDoRun.空闲;
            public int curFixStep = -1;
            public bool runTriger = false;
            public int startTimes = 0;
            public int runTimes = 0;
            public int testTimes = 0;
            public bool resetStart = false;
            public int failTestCount = 0;
            public List<CHPSetting.CStepVal> HpResult = new List<CHPSetting.CStepVal>();
        }
        private CModelPara runModel = new CModelPara();
        private CStat[] statHP = null;
       private CDev[] uutHP = null;
       //  private CDev uutHP = null;
        private int mhpIniPara = 0;
        #endregion

        #region 通信设备参数
        private HYESA140[] devHP = null;
        private CIOCom devIO = null;
        private CChromaPLC devHPPLC = null;

       // private HYESA140 devHPHY = null;
        private SajetMES MesSajet = null;
        private CPLCCom devPLC = null;
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
        /// 连接HIPOT
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ConnectToHP(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;
                if (connect)
                {
                    for (int i = 0; i < CSysPara.mVal.C_HIPOT_MAX; i++)
                    {
                        if (devHP[i] == null)
                        {
                            devHP[i] = new HYESA140();
                            string mHipotCom = string.Empty;
                            if (i == 0)
                                mHipotCom = CSysPara.mVal.hipotCom1;
                            else
                                mHipotCom = CSysPara.mVal.hipotCom2;

                            if (!devHP[i].open(mHipotCom , ref er))
                            {
                                er = "打开高压机串口失败:" + er;
                                devHP[i].close();
                                devHP[i] = null;
                                return false;
                            }
                            if (!devHP[i].init(ref er))
                            {
                                CMath.delayMs(1000);
                                if (!devHP[i].init(ref er))
                                {
                                    er = "初始化高压机失败:" + er;
                                    devHP[i].close();
                                    devHP[i] = null;
                                    return false;
                                }
                            }
                            er = "成功打开高压机,初始化高压机";
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < CSysPara.mVal.C_HIPOT_MAX; i++)
                    {
                        if (devHP[i] != null)
                        {
                            devHP[i].close();
                            devHP[i] = null;
                            er = "关闭高压机串口";
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
        /// 连接控制IO板
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ConnectToIO(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;
                if (connect)
                {
                    if (devIO == null)
                    {
                        devIO = new CIOCom(EIOType.IO_24_16);
                        if (!devIO.open(CSysPara.mVal.ioCom, ref er))
                        {
                            er = "打开控制IO板串口失败:" + er;
                            devIO.close(); 
                            devIO = null;
                            return false;
                        }
                        int mVal = 0;
                        if (!devIO.read(1, ECoilType.Y, 1, ref mVal, ref er))
                        {
                            Thread.Sleep(50);
                            if (!devIO.read(1, ECoilType.Y, 1, ref mVal, ref er))
                            {
                                er = "连接控制IO板失败:" + er;
                                devIO.close();
                                devIO = null;
                                return false;
                            }
                        }                
                        er = "成功控制IO板串口,检测控制IO板通信正常.";
                    }
                }
                else
                {
                    if (devIO != null)
                    {
                        devIO.close();
                        devIO = null;
                        er = "关闭控制IO板串口";
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
        /// 连接HP-PLC-COM
        /// </summary>
        /// <param name="er"></param>
        /// <param name="connect"></param>
        /// <returns></returns>
        public bool ConnectToHPPLC(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;
                if (connect)
                {
                    if (devHPPLC == null)
                    {
                        devHPPLC = new CChromaPLC();
                        if (!devHPPLC.open(CSysPara.mVal.ioCom, ref er))
                        {
                            er = "打开高压机IO串口失败:" + er;
                            devHPPLC.close();
                            devHPPLC = null;
                            return false;
                        }
                        if (!devHPPLC.checkIn(ref er))
                        {
                            devHPPLC.close();
                            devHPPLC = null;
                            return false;

                        }
                        er = "成功打开高压机IO控制板";
                    }
                }
                else
                {
                    if (devHPPLC != null)
                    {
                        devHPPLC.close();
                        devHPPLC = null;
                        er = "关闭高压机IO控制板";
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
        /// 设置HIPOT程序
        /// </summary>
        /// <param name="step"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetHipotPara(int idNo ,string prgName, List<CHPSetting.CStep> step ,ref string er)
        {
            try
            {
                if (devHP[idNo ] == null)
                {
                    devHP[idNo ] = new HYESA140();
                    string mHipotCom = string.Empty;
                    if (idNo == 0)
                        mHipotCom = CSysPara.mVal.hipotCom1;
                    else
                        mHipotCom = CSysPara.mVal.hipotCom2;
                    if (!devHP[idNo].open(CSysPara.mVal.hipotCom1, ref er))
                    {
                        er = "打开高压机串口失败:" + er;
                        devHP[idNo].close();
                        devHP[idNo ] = null;
                        return false;
                    }
                    if (!devHP[idNo].init(ref er))
                    {
                        er = "初始化高压机失败:" + er;
                        devHP[idNo].close();
                        devHP[idNo] = null;
                        return false;
                    }                    
                }
                if (!CSysPara.mVal.chkImpPrg)  
                {
                    //重新加载程序
                    string sendreaddata = string.Empty;
                    if (!devHP[idNo].setTestPara(step, ref er, runModel.model, false, ref  sendreaddata))
                        return false;
                    if (! SaveHipotLogData(idNo, sendreaddata, ref er))
                    {
                        runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                    }
                }
                else
                {
                    //导入高压机内部程序
                    if (!devHP[idNo].importProgram(prgName, ref er))
                        return false;
                    //比对程序是否一致
                    for (int i = 0; i < runModel.step.Count; i++)
                    {
                        CHPSetting.EStepName stepName = CHPSetting.EStepName.AC;   
                        List<double> stepVal=new List<double>();
                        if (!devHP[idNo].readStepSetting(i + 1, ref stepName, ref stepVal, ref er))
                            return false;
                        if (stepName != runModel.step[i].name)
                        {
                            er = "测试步骤" + (i + 1).ToString() + "不一致:" +
                                 "机种[" + runModel.step[i].name + "]与高压机[" + stepName + "]";
                            return false;
                        }
                        for (int j = 0; j < runModel.step[i].para.Count; j++)
                        {
                            if (runModel.step[i].para[j].setVal != stepVal[j])
                            {
                                er = "测试步骤" + (i + 1).ToString() + "参数" + runModel.step[i].para[j].name+ "不一致:" +
                                   "机种[" + runModel.step[i].para[j].setVal + "]与高压机[" + stepVal[j] + "]";
                                return false;
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
        /// 自动调用HIPOT机种参数
        /// </summary>
        /// <param name="prgName"></param>
        /// <param name="step"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool AutoSelectHipot(string prgName, ref string er)
        {
            try
            {
                er = string.Empty; 
                string modelPath = CSysPara.mVal.modelPath + "\\" + prgName + ".hp";
                if (!File.Exists(modelPath))
                {
                    er = "找不到HIPOT机种文件["+modelPath+"],请确定其路径.";
                    return false;
                } 
                CModelSet<CModelPara>.load(modelPath, ref runModel);
                udcHpInfo.SetStatus(udcHPInfo.ERun.Initialize);
                udcHpInfo.SetNewModel(runModel, modelPath);                    
                udcHpData.refreshUI(udcHpInfo.mRunModel.step);
                mhpIniPara = 0;
                IntHPWorker.RunWorkerAsync();
                while (mhpIniPara == 0)
                {
                    Application.DoEvents();
                }
                if (mhpIniPara != 1)
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

                        Thread.Sleep(3000);
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

        #region TCP/IP
        /// <summary>
        /// 测试主机应答状态
        /// </summary>
        private class CSerReponse
        {
            public int ready = 0;
            public string idCard = string.Empty;
            public List<string> serialNo = new List<string>();
            public string modelName = string.Empty;
        }
        private string tcpName = "HIPOT";
        private CClient tcpClient = null;
        /// <summary>
        /// 连接测试服务器
        /// </summary>
        /// <param name="idNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool connectToServer(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;

                if (connect)
                {
                    if (tcpClient != null)
                    {
                        tcpClient.close();
                        tcpClient = null;
                    }
                    tcpClient = new CClient(0, tcpName);
                    tcpClient.OnConEvent.OnEvent += new COnEvent<CTcpConArgs>.OnEventHandler(OnTcpCon);
                    tcpClient.OnRecvAgrs.OnEvent += new COnEvent<CTcpRecvArgs>.OnEventHandler(OnTcpRecv);
                    tcpClient.connect(CSysPara.mVal.serIP, CSysPara.mVal.serPort);
                    int waitTimes = System.Environment.TickCount;
                    while (tcpClient.mReady != 1 && (System.Environment.TickCount - waitTimes) < 2000)
                    {
                        Application.DoEvents();
                    }
                    if (tcpClient.mReady != 1)
                    {
                        er = tcpClient.mName + "无法连接测试服务器[" +
                             CSysPara.mVal.serIP + "]:" + CSysPara.mVal.serPort.ToString();
                        tcpClient.close();
                        tcpClient = null;
                        return false;
                    }
                    er = tcpClient.mName + "成功连接测试服务器[" +
                          CSysPara.mVal.serIP + "]:" + CSysPara.mVal.serPort.ToString();

                }
                else
                {
                    if (tcpClient != null)
                    {
                        er = tcpClient.mName + "断开连接测试服务器[" +
                         CSysPara.mVal.serIP + "]:" + CSysPara.mVal.serPort.ToString();
                        tcpClient.OnConEvent.OnEvent -= new COnEvent<CTcpConArgs>.OnEventHandler(OnTcpCon);
                        tcpClient.OnRecvAgrs.OnEvent -= new COnEvent<CTcpRecvArgs>.OnEventHandler(OnTcpRecv);
                        tcpClient.close();
                        tcpClient = null;
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
        /// 查询命令
        /// </summary>
        /// <param name="statName">站别名</param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool QUERY_STATE(ref CSerReponse serReponse, ref string er)
        {
            try
            {
                if (tcpClient == null)
                    return false;

                string cmd = "QUERY_STATE_" + tcpClient.mName;

                tcpLog.Log(cmd, udcRunLog.ELog.OK);

                tcpClient.recvData = string.Empty;

                if (!tcpClient.SendMsg(cmd))
                {
                    er = "发送数据失败:" + cmd;
                    return false;
                }

                int waitTimes = System.Environment.TickCount;

                while (tcpClient.recvData == string.Empty && (System.Environment.TickCount - waitTimes) < 1000)
                {
                    Application.DoEvents();
                }

                if (tcpClient.recvData == string.Empty)
                {
                    er = "接收数据超时";
                    return false;
                }

                string rtnRecv = "STATE_" + tcpClient.mName;

                if (tcpClient.recvData.Length < rtnRecv.Length || tcpClient.recvData.Substring(0, rtnRecv.Length) != rtnRecv)
                {
                    er = "数据异常:" + tcpClient.recvData;
                    return false;
                }

                //STATE_XXXX|Ready|Model|ID|Sn1;Sn2....

                string[] recvList = tcpClient.recvData.Split('|');

                serReponse.ready = System.Convert.ToInt32(recvList[1]);

                if (serReponse.ready != 0)
                {
                    serReponse.modelName = recvList[2];

                    serReponse.idCard = recvList[3];

                    string[] serialNos = recvList[4].Split(';');

                    for (int i = 0; i < serialNos.Length; i++)
                        serReponse.serialNo.Add(serialNos[i]);
                }
                else
                {
                    serReponse.modelName = string.Empty;
                    serReponse.idCard = string.Empty;
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
        /// 启动测试
        /// </summary>
        /// <param name="statId"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool START_TEST(ref string er)
        {
            try
            {
                if (tcpClient == null)
                    return false;

                string cmd = "START_TEST_" + tcpClient.mName;

                tcpLog.Log(cmd, udcRunLog.ELog.OK);

                tcpClient.recvData = string.Empty;

                if (!tcpClient.SendMsg(cmd))
                {
                    er = "发送数据失败:" + cmd;
                    return false;
                }

                int waitTimes = System.Environment.TickCount;

                while (tcpClient.recvData == string.Empty && (System.Environment.TickCount - waitTimes) < 1000)
                {
                    Application.DoEvents();
                }

                if (tcpClient.recvData == string.Empty)
                {
                    er = "接收数据超时";
                    return false;
                }

                string rtnRecv = "START_" + tcpClient.mName + "_OK";

                if (tcpClient.recvData != rtnRecv)
                {
                    er = "数据异常:" + tcpClient.recvData;
                    return false;
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
        /// 测试结束
        /// </summary>
        /// <param name="statId"></param>
        /// <param name="idCard"></param>
        /// <param name="result"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool END_TEST(int statId, string idCard, List<int> result, ref string er)
        {
            try
            {
                if (tcpClient == null)
                    return false;

                //END_TEST_XXXX|ID|result1;result2...

                string cmd = "END_TEST_" + tcpClient.mName + "|" + idCard + "|";

                for (int i = 0; i < result.Count; i++)
                {
                    if (i < result.Count - 1)
                        cmd += result[i] + ";";
                    else
                        cmd += result[i];
                }

                tcpLog.Log(cmd, udcRunLog.ELog.OK);

                tcpClient.recvData = string.Empty;

                if (!tcpClient.SendMsg(cmd))
                {
                    er = "发送数据失败:" + cmd;
                    return false;
                }

                int waitTimes = System.Environment.TickCount;

                while (tcpClient.recvData == string.Empty && (System.Environment.TickCount - waitTimes) < 1000)
                {
                    Application.DoEvents();
                }

                if (tcpClient.recvData == string.Empty)
                {
                    er = "接收数据超时";
                    return false;
                }

                string rtnRecv = "END_TEST_" + tcpClient.mName + "_OK";

                if (tcpClient.recvData != rtnRecv)
                {
                    er = "数据异常:" + tcpClient.recvData;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        private void OnTcpCon(object sender, CTcpConArgs e)
        {
            if (!e.bErr)
                tcpLog.Log(e.conStatus, udcRunLog.ELog.Action);
            else
                tcpLog.Log(e.conStatus, udcRunLog.ELog.NG);
        }
        private void OnTcpRecv(object sender, CTcpRecvArgs e)
        {
            tcpClient.recvData = e.recvData;
            tcpLog.Log(e.recvData, udcRunLog.ELog.Content);
        }
        #endregion

        #region TCP/IP连接检查
        /// <summary>
        /// 等待连接次数
        /// </summary>
        private int C_TCP_WAIT_TIMES = 10;
        /// <summary>
        /// 接收数据超时
        /// </summary>
        private int TCP_RECV_TIMEOUT = 0;
        /// <summary>
        /// TCP/IP重新连接
        /// </summary>
        private int TCP_CONNECT = 0;
        /// <summary>
        /// TCP/IP连接检查
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private bool checkTCPIP()
        {
            try
            {
                string er = string.Empty;

                if (tcpClient == null)
                {
                    if (TCP_CONNECT < C_TCP_WAIT_TIMES)
                    {
                        TCP_CONNECT++;
                        System.Threading.Thread.Sleep(1000);
                        return false;
                    }
                    TCP_CONNECT = 0;
                    runLog.Log("尝试重新连接服务器[" + CSysPara.mVal.serIP + ":" +
                              CSysPara.mVal.serPort.ToString() + "]", udcRunLog.ELog.NG);
                    connectToServer(ref er);
                }
                if (tcpClient.mReady == 0)
                {
                    if (TCP_CONNECT < C_TCP_WAIT_TIMES)
                    {
                        TCP_CONNECT++;
                        System.Threading.Thread.Sleep(1000);
                        return false;
                    }
                    TCP_CONNECT = 0;
                    runLog.Log("尝试重新连接服务器[" + CSysPara.mVal.serIP + ":" +
                              CSysPara.mVal.serPort.ToString() + "]", udcRunLog.ELog.NG);
                    connectToServer(ref er);
                    return false;
                }
                else if (TCP_RECV_TIMEOUT > C_TCP_WAIT_TIMES)
                {
                    TCP_RECV_TIMEOUT = 0;
                    runLog.Log("尝试重新连接服务器[" + CSysPara.mVal.serIP + ":" +
                                  CSysPara.mVal.serPort.ToString() + "]", udcRunLog.ELog.NG);
                    connectToServer(ref er);
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 开始测试
        /// <summary>
        /// 启动监控
        /// </summary>
        /// <param name="run"></param>
        private bool StartRun(bool run = true)
        {
            try
            {
                if (run)
                {
                    mhpIniPara = 0;
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
                    udcHpInfo.SetStatus(udcHPInfo.ERun.Ready);
                }
                else
                {
                    if (!StartThread(run))
                        return false;
                    if (!ConnectToDevice(run))
                        return false;
                    udcHpInfo.SetStatus(udcHPInfo.ERun.Idle);
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
        /// 初始化测试状态
        /// </summary>
        private void IniStatusPara()
        {
            statHP[0].doRun = EDoRun.空闲;
            statHP[1].doRun = EDoRun.空闲;

            uutHP[0].doRun = EDoRun.空闲;
            uutHP[0].curFixStep =-1;
            uutHP[1].doRun = EDoRun.空闲;
            uutHP[1].curFixStep =-1;

            mHPDebug[0] = false;
            mHPDebug[1] = false;

            if (CSysPara.mVal.chkSaveTcpLog)
                tcpLog.mSaveEnable = true;
            else
                tcpLog.mSaveEnable = false;
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
                if (connect)
                {
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
                    if (ConnectToHP(ref er, connect))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
                    }

                    if (ConnectToIO(ref er, connect))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        runLog.Log(er, udcRunLog.ELog.NG);
                        chkDev = false;
                    }
                    if (connectToServer(ref er, connect))
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
                else
                {
                    if (connectToServer(ref er, connect))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
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
                    if (ConnectToHP(ref er, connect))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
                    }

                    if (ConnectToIO(ref er,connect))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        runLog.Log(er, udcRunLog.ELog.NG);
                        chkDev = false;
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
        private bool StartThread(bool run = true)
        {
            try
            {
                if (run)
                {
                    if (!IntHPWorker.IsBusy)
                        IntHPWorker.RunWorkerAsync();
                    if (!TcpRecvWorker.IsBusy)
                        TcpRecvWorker.RunWorkerAsync();
                }
                else
                {
                    if (HPStartWorker1.IsBusy)
                        HPStartWorker1.CancelAsync();
                    if (HPStartWorker2.IsBusy)
                        HPStartWorker2.CancelAsync();
                    if (TcpRecvWorker.IsBusy)
                        TcpRecvWorker.CancelAsync();
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

        #region 线程

        #region 字段
        private bool[] mHPDebug = new bool[2];
        #endregion

        #region 方法

        /// <summary>
        /// 高压机初始化线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntHPWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                runLog.Log("开始加载HIPOT参数[" + udcHpInfo.mRunModel.model + "],请稍等..", udcRunLog.ELog.Action);
                string er = string.Empty;
                bool setPara = true;
                for (int i =0;i<CSysPara.mVal.C_HIPOT_MAX;i++)
                {
                    if (!SetHipotPara(i,udcHpInfo.mRunModel.model, udcHpInfo.mRunModel.step, ref er))
                    {
                        runLog.Log("加载HIPOT" + (i+1).ToString() +  "参数[" + udcHpInfo.mRunModel.model + "]失败:[" + er + "]", udcRunLog.ELog.NG);
                        setPara = false;
                        //mhpIniPara = 2;
                    }
                    else
                    {
                        runLog.Log("成功加载HIPOT" + (i + 1).ToString() + "参数[" + udcHpInfo.mRunModel.model + "].", udcRunLog.ELog.Action);
                        //mhpIniPara = 1;
                    }
                    Thread.Sleep(500);
                }
                Thread.Sleep(1000);
                if (setPara)
                    mhpIniPara = 1;
                else
                    mhpIniPara = 2;
            }
            catch (Exception ex)
            {
                mhpIniPara = 2;
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// TCP/IP主监控线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpRecvWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string threadName = "<TCP/IP主线程>";

            runLog.Log(threadName + "启动监控测试", udcRunLog.ELog.Action);

            try
            {
                while (true)
                {
                    string er = string.Empty;

                    if (TcpRecvWorker.CancellationPending)
                        return;

                    if (!getStatReady())
                    {
                        Thread.Sleep(CSysPara.mVal.monDelayMs);
                        continue;
                    }

                    for (int idNo = 0; idNo < 2; idNo++)
                        statOnRunning(idNo);

                    Thread.Sleep(CSysPara.mVal.monDelayMs);
                }
            }
            catch (Exception ex)
            {
                runLog.Log(threadName + ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                runLog.Log(threadName + "销毁退出", udcRunLog.ELog.NG);
            }
        }
        /// <summary>
        /// 检测工位到位信号
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool getStatReady()
        {
            try
            {
                string er = string.Empty;

                int idNo = 0;

                if (!checkTCPIP())
                    return false;

                CSerReponse serReponse = new CSerReponse();
                if (!QUERY_STATE(ref serReponse, ref er))
                {
                    TCP_RECV_TIMEOUT++;
                    runLog.Log(er, udcRunLog.ELog.NG);
                    return false;
                }
                tcpShow.setStatus(tcpClient.recvData);
                TCP_RECV_TIMEOUT = 0;
                TCP_CONNECT = 0;                

                if (serReponse.ready == 0)
                {
                    for (int i = 0; i < statHP.Length; i++)
                    {
                         statHP[i].doRun = EDoRun.空闲;
                         statHP[i].idCard = string.Empty;
                         for (int z = 0; z < statHP[i].serialNo.Count; z++)
                             statHP[i].serialNo[z] = string.Empty;  
                    }
                    return false;
                }
                else if (serReponse.ready == 1)
                {
                    statHP[1].doRun = EDoRun.空闲;

                    idNo = 0;

                    if (statHP[idNo].doRun == EDoRun.空闲)
                    {
                        statHP[idNo].idCard = serReponse.idCard;
                        statHP[idNo].modelName = serReponse.modelName;
                        for (int i = 0; i < serReponse.serialNo.Count-1; i++)
                            statHP[idNo].serialNo[i] = serReponse.serialNo[i];

                        
                        //高压分测模式,高压1测15，高压2测234678
                        if (CSysPara.mVal.chkSpHP)
                        {
                            if (CSysPara.mVal.StatHipotMode == 0)
                            {
                                statHP[idNo].serialNo[1] = string.Empty;
                            //    statHP[idNo].serialNo[2] = string.Empty;
                                statHP[idNo].serialNo[3] = string.Empty;
                                statHP[idNo].serialNo[5] = string.Empty;
                             //   statHP[idNo].serialNo[6] = string.Empty;
                                statHP[idNo].serialNo[7] = string.Empty;
                            }
                            else
                            {
                                statHP[idNo].serialNo[0] = string.Empty;
                                statHP[idNo].serialNo[2] = string.Empty;
                                statHP[idNo].serialNo[4] = string.Empty;
                                statHP[idNo].serialNo[6] = string.Empty;
                            }
                            //for (int i = 0 + ((CSysPara.mVal.StatHipotMode + 1) % 2) * 4; i < 4 + ((CSysPara.mVal.StatHipotMode + 1) % 2) * 4; i++)
                            //{
                            //    statHP[idNo].serialNo[i] = string.Empty;
                            //}
                        }

                        if (CSysPara.mVal.conMes)
                        {
                            for (int i = 0; i < statHP[idNo].serialNo.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(statHP[idNo].serialNo[i]))
                                {
                                    string mInternalSn = string.Empty;
                                    if (MesSajet.Read_internalSN(statHP[idNo].serialNo[i], ref mInternalSn, ref er))
                                    {
                                        statHP[idNo].internalSN[i] = mInternalSn;
                                    }
                                    else
                                    {
                                        runLog.Log("获取条码【" + statHP[idNo].serialNo[i] + "】内码失败：" + er, udcRunLog.ELog.NG);
                                    }

                                }
                                Thread.Sleep(50);
                            }
                        }

                        udcHpResult.SetFix(statHP[idNo].idCard, statHP[idNo].serialNo);
                        udcHpResult.clrResult();
                        udcHpData.clrData();
                        statHP[idNo].doRun = EDoRun.到位;
                        udcHpInfo.SetStatus(udcHPInfo.ERun.Ready);
                    }
                    return true;
                }
                else if (serReponse.ready == 2)
                {
                    statHP[0].doRun = EDoRun.空闲;

                    idNo = 1;
                    if (statHP[idNo].doRun == EDoRun.空闲)
                    {
                        statHP[idNo].idCard = serReponse.idCard;
                        statHP[idNo].modelName = serReponse.modelName;
                        for (int i = 0; i < serReponse.serialNo.Count-1; i++)
                            statHP[idNo].serialNo[i] = serReponse.serialNo[i];

                        //高压分测模式,高压1测15，高压2测234678
                        if (CSysPara.mVal.chkSpHP)
                        {
                            if (CSysPara.mVal.StatHipotMode == 0)
                            {
                                statHP[idNo].serialNo[1] = string.Empty;
                            //    statHP[idNo].serialNo[2] = string.Empty;
                                statHP[idNo].serialNo[3] = string.Empty;
                                statHP[idNo].serialNo[5] = string.Empty;
                            //    statHP[idNo].serialNo[6] = string.Empty;
                                statHP[idNo].serialNo[7] = string.Empty;
                            }
                            else
                            {
                                statHP[idNo].serialNo[0] = string.Empty;
                                statHP[idNo].serialNo[2] = string.Empty;
                                statHP[idNo].serialNo[4] = string.Empty;
                                statHP[idNo].serialNo[6] = string.Empty;
                            }
                            //for (int i = 0 + ((CSysPara.mVal.StatHipotMode + 1) % 2) * 4; i < 4 + ((CSysPara.mVal.StatHipotMode + 1) % 2) * 4; i++)
                            //{
                            //    statHP[idNo].serialNo[i] = string.Empty;
                            //}
                        }
                        ////高压分测模式,高压1测1234，高压2测5678
                        //if (CSysPara.mVal.chkSpHP)
                        //{
                        //    for (int i = 0 + ((CSysPara.mVal.StatHipotMode + 1) % 2) * 4; i < 4 + ((CSysPara.mVal.StatHipotMode + 1) % 2) * 4; i++)
                        //    {
                        //        statHP[idNo].serialNo[i] = string.Empty;
                        //    }
                        //}
                        if (CSysPara.mVal.conMes)
                        {
                            for (int i = 0; i < statHP[idNo].serialNo.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(statHP[idNo].serialNo[i]))
                                {
                                    string mInternalSn = string.Empty;
                                    if (MesSajet.Read_internalSN(statHP[idNo].serialNo[i], ref mInternalSn, ref er))
                                    {
                                        statHP[idNo].internalSN[i] = mInternalSn;
                                    }
                                    else
                                    {
                                        runLog.Log("获取条码【" + statHP[idNo].serialNo[i] + "】内码失败：" + er, udcRunLog.ELog.NG);
                                    }

                                }
                                Thread.Sleep(50);
                            }
                        }
                        udcHpResult.SetFix(statHP[idNo].idCard, statHP[idNo].serialNo);
                        udcHpResult.clrResult();
                        udcHpData.clrData();
                        statHP[idNo].doRun = EDoRun.到位;
                        udcHpInfo.SetStatus(udcHPInfo.ERun.Ready);
                    }
                    return true;
                }
                else
                {
                    runLog.Log("接收测试工位到位信号异常【" + serReponse.ready.ToString() + "】", udcRunLog.ELog.NG);
                    statHP[0].doRun = EDoRun.空闲;
                    statHP[1].doRun = EDoRun.空闲;
                    Thread.Sleep(2000);
                    return false;
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
        }
        /// <summary>
        /// 控制工位测试
        /// </summary>
        /// <returns></returns>
        private void statOnRunning(int idNo)
        {
            try
            {
                string er = string.Empty;

                switch (statHP[idNo].doRun)
                {
                    case EDoRun.空闲:
                        break;
                    case EDoRun.到位:
                        if (mhpIniPara != 1)
                            break;
                        if (statHP[idNo].modelName != "SpotCheckFix" &&  statHP[idNo].modelName != runModel.model)
                        {
                            if (MessageBox.Show("高压机检测到当前治具MES机种名:"+ statHP[idNo].modelName + "与当前高压机种名不一致,请检查" , "高压机种异常！",
                                MessageBoxButtons.OK) == DialogResult.OK)
                            {
                                return;
                            }
                        }
                        for (int i = 0; i < CSysPara.mVal.C_HIPOT_MAX; i++)
                        {
                            for (int CH = 0; CH < uutHP[i].chanNum; CH++)
                            {
                              //  uutHP[i].serialNo[CH] = statHP[idNo].serialNo[CH + i * uutHP[i].chanNum];
                                uutHP[i].serialNo[CH] = statHP[idNo].serialNo[CH];
                                uutHP[i].result[CH] = (int)EResult.空闲;
                                uutHP[i].HpResult[CH] = new CHPSetting.CStepVal();
                            }
                            uutHP[i].curFixStep = -1;
                            uutHP[i].doRun = EDoRun.就绪;                               
                        }
                        if (!HPStartWorker1.IsBusy)
                             HPStartWorker1.RunWorkerAsync();
                        if (CSysPara.mVal.C_HIPOT_MAX == 2 && !HPStartWorker2.IsBusy)
                        {
                            HPStartWorker2.RunWorkerAsync();
                        }
                        statHP[idNo].doRun = EDoRun.就绪;
                        statHP[idNo].startTimes = System.Environment.TickCount;
                        runLog.Log(statHP[idNo].statName + "治具[" + statHP[idNo].idCard +
                                   "]到位就绪,等待启动测试", udcRunLog.ELog.OK);
                        break;
                    case EDoRun.就绪:
                        statHP[idNo].runTimes = System.Environment.TickCount - statHP[idNo].startTimes;
                        udcHpInfo.SetTimes(statHP[idNo].runTimes);
                        if (uutHP[0].doRun == EDoRun.测试 || uutHP[0].doRun == EDoRun.结束 || uutHP[1].doRun == EDoRun.测试 || uutHP[1].doRun == EDoRun.结束)
                        {
                            if (!START_TEST(ref er))
                            {
                                runLog.Log(statHP[idNo].statName + "发送TCP测试信号错误:" + er, udcRunLog.ELog.NG);
                                break;
                            }
                            statHP[idNo].doRun = EDoRun.测试;
                            udcHpInfo.SetStatus(udcHPInfo.ERun.Testing);
                        }
                        break;
                    case EDoRun.测试:
                        statHP[idNo].runTimes = System.Environment.TickCount - statHP[idNo].startTimes;
                        udcHpInfo.SetTimes(statHP[idNo].runTimes);
                        for (int i = 0; i < CSysPara.mVal.C_HIPOT_MAX; i++)
                        {
                            if (uutHP[i].doRun == EDoRun.结束)
                            {
                                for (int CH = 0; CH < uutHP[i].chanNum / CSysPara.mVal.C_HIPOT_MAX; CH++)
                                {
                                  
                                   
                                    if (CSysPara.mVal.C_HIPOT_MAX == 2 && i==1)
                                    {
                                        statHP[idNo].result[CH+4] = uutHP[i].result[CH +4];
                                    }
                                    else
                                        statHP[idNo].result[CH] = uutHP[i].result[CH];
                                }
                                uutHP[i].doRun = EDoRun.过站;
                            }
                            //else
                            //    return  ;
                        }
                        if (CSysPara.mVal.C_HIPOT_MAX==1)
                            uutHP[1].doRun = EDoRun.过站;

                        if (uutHP[0].doRun == EDoRun.过站 && uutHP[1].doRun == EDoRun.过站)
                        {
                            //获取高压机产品结果
                            bool uutPass = true;
                            int ttNum = 0;
                            int passNum = 0;
                            for (int i = 0; i < statHP[idNo].slotNum; i++)
                            {
                                if (statHP[idNo].serialNo[i] == string.Empty)
                                    statHP[idNo].result[i] = 0;
                                else
                                {
                                    ttNum++;
                                    string MesResult = string.Empty;
                                    if (statHP[idNo].result[i] == 0)
                                    {
                                        passNum++;
                                        MesResult = "PASS";

                                        if (CSysPara.mVal.conMes && (CSysPara.mVal.StatHipotMode == 1 || CSysPara.mVal.chkSpHP) && statHP[idNo].modelName != "SpotCheckFix")
                                        {

                                            if (MesSajet.Tran_SN(statHP[idNo].serialNo[i], MesResult, ref er))
                                                runLog.Log("上传条码" + statHP[idNo].serialNo[i] + "成功", udcRunLog.ELog.Content);
                                            else
                                            {
                                                if (er.Contains("EMP ERR"))
                                                {
                                                    if (MesSajet.Login("222", "222", ref er))
                                                    {
                                                        runLog.Log("系统检测到断网，重新登录成功", udcRunLog.ELog.NG);
                                                        if (MesSajet.Tran_SN(statHP[idNo].serialNo[i], MesResult, ref er))
                                                            runLog.Log("上传条码" + statHP[idNo].serialNo[i] + "成功", udcRunLog.ELog.Content);
                                                        else
                                                        {
                                                            statHP[idNo].result[i] = 1;
                                                            runLog.Log("上传条码" + statHP[idNo].serialNo[i] + "错误:" + er, udcRunLog.ELog.NG);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        statHP[idNo].result[i] = 1;
                                                        runLog.Log("系统检测到断网，重新连接失败" + er, udcRunLog.ELog.NG);
                                                    }

                                                }
                                                else
                                                {
                                                    statHP[idNo].result[i] = 1;
                                                    runLog.Log("上传条码" + statHP[idNo].serialNo[i] + "错误:" + er, udcRunLog.ELog.NG);
                                                }
                                            }
                                            CMath.WaitMs(CSysPara.mVal.MesDelayMs);
                                        }
                                    }
                                    else
                                    {
                                     
                                        for (int stepNo = 0; stepNo <  uutHP[idNo].HpResult[i].mVal.Count; stepNo++)
                                         {                        
                     
                                            if ( uutHP[idNo].HpResult[i].mVal[stepNo].result != 0)
      
                                            {
                              
                                                if( uutHP[idNo].HpResult[i].mVal[stepNo].code.Contains("ARC"))
                                                    MesResult="HIPOT" + (CSysPara .mVal .StatHipotMode+1).ToString () + "-NG-1";
                                                else if (  uutHP[idNo].HpResult[i].mVal[stepNo].code.Contains("HI"))
                                                    MesResult="HIPOT" + (CSysPara .mVal .StatHipotMode+1).ToString () + "-NG-2";
                                                else
                                                    MesResult="HIPOT" + (CSysPara .mVal .StatHipotMode+1).ToString () + "-NG-3";

                                            }

                                        }
                                        if (CSysPara.mVal.conMes && !CSysPara.mVal.ChkMesFail && statHP[idNo].modelName != "SpotCheckFix")
                                        {

                                            if (MesSajet.Tran_SN(statHP[idNo].serialNo[i], MesResult, ref er))
                                                runLog.Log("上传条码" + statHP[idNo].serialNo[i] + "成功", udcRunLog.ELog.Content);
                                            else
                                            {
                                                if (er.Contains("EMP ERR"))
                                                {
                                                    if (MesSajet.Login("222", "222", ref er))
                                                    {
                                                        runLog.Log("系统检测到断网，重新登录成功", udcRunLog.ELog.NG);
                                                        if (MesSajet.Tran_SN(statHP[idNo].serialNo[i], MesResult, ref er))
                                                            runLog.Log("上传条码" + statHP[idNo].serialNo[i] + "成功", udcRunLog.ELog.Content);
                                                        else
                                                            runLog.Log("上传条码" + statHP[idNo].serialNo[i] + "错误:" + er, udcRunLog.ELog.NG);

                                                    }
                                                    else
                                                        runLog.Log("系统检测到断网，重新连接失败" + er, udcRunLog.ELog.NG);

                                                }
                                                else
                                                    runLog.Log("上传条码" + statHP[idNo].serialNo[i] + "错误:" + er, udcRunLog.ELog.NG);
                                            }
                                            CMath.WaitMs(CSysPara.mVal.MesDelayMs);
                                        }
                                        statHP[idNo].result[i] = CGlobal.CFlow.flowId;
                                        uutPass = false;
                                    }
                                   
                                }
                            }
                            List<int> resultUUT = new List<int>();
                            for (int i = 0; i < CSysPara.mVal.C_SLOT_MAX; i++)
                                resultUUT.Add(statHP[idNo].result[i]);

                            //发TCP过站信号    
                            if (!END_TEST(idNo, statHP[idNo].idCard, resultUUT, ref er))
                            {
                                runLog.Log(statHP[idNo].statName + "发送TCP过站信号错误:" + er, udcRunLog.ELog.NG);
                                break;
                            }
                            if (uutPass)
                            {
                                udcHpInfo.SetStatus(udcHPInfo.ERun.Pass);
                                runLog.Log(statHP[idNo].statName + "治具[" + statHP[idNo].idCard +
                                          "]高压测试:PASS,准备过站", udcRunLog.ELog.OK);
                            }
                            else
                            {
                                udcHpInfo.SetStatus(udcHPInfo.ERun.Fail);
                                runLog.Log(statHP[idNo].statName + "治具[" + statHP[idNo].idCard +
                                             "]高压测试:FAIL,准备过站", udcRunLog.ELog.NG);
                            }
                            if (!SaveTestData(idNo, ref er))
                            {
                                runLog.Log(statHP[idNo].statName + "治具[" + statHP[idNo].idCard +
                                            "]存报表错误："+er, udcRunLog.ELog.NG);
                            }
                            udcHpInfo.SetNum(ttNum, passNum);
                            udcHpInfo.AddConnectorTimes();
                            statHP[idNo].doRun = EDoRun.过站;
                        }
                        break;
                    case EDoRun.结束:
                        break;
                    case EDoRun.报警:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                runLog.Log(ex.ToString(), udcRunLog.ELog.Err);
            }
        }

        /// <summary>
        /// 高压设置1测试线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HPStartWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int idNo = 0;
            try
            {
                runLog.Log(uutHP[idNo].devName + "开始启动测试", udcRunLog.ELog.Action);

                uutHP[idNo].testTimes = System.Environment.TickCount;

                string er = string.Empty;
                string sendreaddata = string.Empty;
                devHP[idNo].stop( ref er ,ref sendreaddata );
                if (!SaveHipotLogData(idNo, sendreaddata, ref er))
                {
                    runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                }
                Thread.Sleep(100);

                while (true)
                {
                    if (HPStartWorker1.CancellationPending)
                        return;
                    if (!onHipotTesting(idNo))
                        return;
                    if (uutHP[idNo].doRun == EDoRun.结束)
                        return;
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                runLog.Log(uutHP[idNo].devName + ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                double runTimes = ((double)(System.Environment.TickCount - uutHP[idNo].testTimes)) / 1000;

                runLog.Log(uutHP[idNo].devName + "测试结束:" + runTimes.ToString("0.0") + "秒", udcRunLog.ELog.Action);

                if (mHPDebug[idNo])
                {
                    //if ((idNo == 0 && !mHPDebug[1]) || (idNo == 1 && !mHPDebug[0]))
                    //{
                    //    string en = string.Empty;
                    //    ConnectToHPPLC(ref en, false);
                    //}
                    udcHpInfo.SetDebugBtn(idNo, 0);
                    mHPDebug[idNo] = false;
                    udcHpInfo.SetDebugBtn(idNo, 0);
                    mHPDebug[idNo] = false;
                }
            }
        }
        /// <summary>
        /// 高压设置2测试线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HPStartWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            int idNo = 1;
            try
            {
                runLog.Log(uutHP[idNo].devName + "开始启动测试", udcRunLog.ELog.Action);

                uutHP[idNo].testTimes = System.Environment.TickCount;

                string er = string.Empty;
                string sendreaddata = string.Empty;
                devHP[idNo].stop(ref er,ref sendreaddata );
                if (!SaveHipotLogData(idNo, sendreaddata, ref er))
                {
                    runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                }
                CMath .WaitMs (100+ CSysPara.mVal .Hp2DelayMs );

                while (true)
                {
                    if (HPStartWorker2.CancellationPending)
                        return;
                    if (!onHipotTesting(idNo))
                        return;
                    if (uutHP[idNo].doRun == EDoRun.结束)
                        return;
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                runLog.Log(uutHP[idNo].devName + ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                double runTimes = ((double)(System.Environment.TickCount - uutHP[idNo].testTimes)) / 1000;

                runLog.Log(uutHP[idNo].devName + "测试结束:" + runTimes.ToString("0.0") + "秒", udcRunLog.ELog.Action);

                if (mHPDebug[idNo])
                {
                    //if ((idNo == 0 && !mHPDebug[1]) || (idNo == 1 && !mHPDebug[0]))
                    //{
                    //    string en = string.Empty;
                    //    ConnectToHPPLC(ref en, false);
                    //}
                    udcHpInfo.SetDebugBtn(idNo, 0);
                    mHPDebug[idNo] = false;
                }
            }
        }
        /// <summary>
        /// 高压测试中
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private bool onHipotTesting(int idNo)
        {
            try
            {
                string er = string.Empty;
               string sendreaddata = string.Empty;
                if (uutHP[idNo].doRun == EDoRun.结束)
                    return true;

                switch (uutHP[idNo].doRun)
                {
                    case EDoRun.空闲:
                        break;

                    case EDoRun.就绪:

                        //获取当前高压测试通道
                        int curStep = -1;

                        for (int i =  idNo * 4; i < uutHP[idNo].chanNum / CSysPara.mVal.C_HIPOT_MAX + idNo * 4; i++)
                        {
                            if (uutHP[idNo].serialNo[i] != string.Empty)
                            {
                                curStep = i;
                                break;
                            }
                        }

                        if (curStep == -1)
                        {
                            uutHP[idNo].doRun = EDoRun.结束;
                            return true;
                        }
                        uutHP[idNo].curFixStep = curStep;
                        //切换高压测试通道
                        if (!SetIOAndHPEvent(idNo,uutHP[idNo].curFixStep, ref er))
                        {
                            runLog.Log(uutHP[idNo].devName + "设置测试通道" + (uutHP[idNo].curFixStep + 1).ToString() +
                                        "错误:" + er, udcRunLog.ELog.NG);
                            uutHP[idNo].doRun = EDoRun.报警;
                            uutHP[idNo].result[uutHP[idNo].curFixStep] = (int)EResult.报警;
                            return false;
                        }
               
                        System.Threading.Thread.Sleep(CSysPara.mVal.ioDelayMs);
                         sendreaddata = string.Empty;
                        //启动高压机测试
                        if (!devHP[idNo].start( ref er,ref sendreaddata  ))
                        {
                            CMath.WaitMs(300);
                            if (!devHP[idNo].start(ref er ,ref sendreaddata  ))
                            {
                                runLog.Log(uutHP[idNo].devName + "启动高压测试错误:" + er, udcRunLog.ELog.NG);
                                uutHP[idNo].doRun = EDoRun.报警;
                                uutHP[idNo].result[uutHP[idNo].curFixStep] = (int)EResult.报警;
                                return false;
                            }
                        }
                        if (!SaveHipotLogData(idNo, sendreaddata, ref er))
                        {
                            runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                        }
                        runLog.Log(uutHP[idNo].devName + "测试通道【" + (uutHP[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutHP[idNo].startTimes = System.Environment.TickCount;
                        uutHP[idNo].runTriger = false;
                        uutHP[idNo].resetStart = false;  
                        uutHP[idNo].doRun = EDoRun.测试;
                        
                        break;
                    case EDoRun.测试:
                         int runTimes = System.Environment.TickCount - uutHP[idNo].startTimes;
                         udcHpInfo.SetTimes(runTimes);
                       // uutHP[idNo].runTimes = System.Environment.TickCount - uutHP[idNo].startTimes;

                        //int testing = 0;

                        //int testEnd = 0;

                        //int result = 0;

                        List<int> outPos = new List<int>();
                        EHPStatus hpStatus = EHPStatus.STOPPED;
                         sendreaddata = string.Empty;
                        if (!devHP[idNo].readStatus(ref hpStatus, ref er,ref sendreaddata ))
                        {
                            System.Threading.Thread.Sleep(50);
                            if (!devHP[idNo].readStatus(ref hpStatus, ref er,ref sendreaddata ))
                            {
                                runLog.Log("读高压机运行状态错误:" + er, udcRunLog.ELog.NG);
                                break;
                            }
                        }
                        if (!SaveHipotLogData(idNo, sendreaddata, ref er))
                        {
                            runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                        }
                        //检测高压机是否启动测试?
                        if (!uutHP[idNo].runTriger)
                        {
                            if (hpStatus == EHPStatus.RUNNING || hpStatus == EHPStatus.ABORT )
                                uutHP[idNo].runTriger = true;
                            else
                            {
                                if (runTimes > 10000)
                                {
                                    if (!uutHP[idNo].resetStart)
                                    {
                                        uutHP[idNo].resetStart = true;
                                         sendreaddata = string.Empty;
                                        //启动高压机测试
                                        if (!devHP[idNo].start(ref er,ref sendreaddata ))
                                        {
                                            if (!devHP[idNo].start(ref er,ref sendreaddata ))
                                            {
                                                runLog.Log(uutHP[idNo].devName + "启动高压测试错误:" + er, udcRunLog.ELog.NG);
                                                uutHP[idNo].doRun = EDoRun.报警;
                                                uutHP[idNo].result[uutHP[idNo].curFixStep] = (int)EResult.报警;
                                                return false;
                                            }
                                        }
                                        if (!SaveHipotLogData(idNo, sendreaddata, ref er))
                                        {
                                            runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                                        }
                                        return true;
                                    }
                                    else
                                    {

                                        runLog.Log("启动高压机测试超时.", udcRunLog.ELog.NG);
                                        uutHP[idNo].doRun = EDoRun.报警;
                                    }
                                }
                            }
                            break;
                        }
                        //检查高压机是否测试结束？
                        if (hpStatus == EHPStatus.RUNNING)
                            break;

                     if (!GetResultFromHP (idNo,uutHP[idNo].curFixStep ,ref er))
                     {
                         uutHP[idNo].HpResult[uutHP[idNo].curFixStep].result = 1;
                         runLog.Log("获取高压测试结果错误:" + er, udcRunLog.ELog.NG);
                         //uutHP[idNo].doRun = EDoRun.报警;
                        // return false ;
                     }

                     if (uutHP[idNo].HpResult[uutHP[idNo].curFixStep].result != 0)
                     {
                        //不良重测
                        if (uutHP[idNo].failTestCount < CSysPara.mVal.FailTestCount)
                        {
                             
                                if (!SetIOAndHPEvent(idNo, uutHP[idNo].curFixStep, ref er))
                                {
                                    runLog.Log(
                                        uutHP[idNo].devName + "设置测试通道" + (uutHP[idNo].curFixStep + 1).ToString() +
                                        "错误:" + er, udcRunLog.ELog.NG);
                                    uutHP[idNo].doRun = EDoRun.报警;
                                    uutHP[idNo].result[uutHP[idNo].curFixStep] = (int) EResult.报警;
                                    return false;
                                }

                                System.Threading.Thread.Sleep(CSysPara.mVal.ioDelayMs);
                                sendreaddata = string.Empty;
                                //启动高压机测试
                                if (!devHP[idNo].start(ref er,ref sendreaddata ))
                                {
                                    if (!devHP[idNo].start(ref er,ref sendreaddata ))
                                    {
                                        runLog.Log(uutHP[idNo].devName + "启动高压测试错误:" + er, udcRunLog.ELog.NG);
                                        uutHP[idNo].doRun = EDoRun.报警;
                                        uutHP[idNo].result[uutHP[idNo].curFixStep] = (int) EResult.报警;
                                        return false;
                                    }
                                }
                                if (!SaveHipotLogData(idNo, sendreaddata, ref er))
                                {
                                    runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                                }
                                uutHP[idNo].failTestCount++;
                                break;
                        }
                        else
                        {
                            uutHP[idNo].failTestCount = 0;
                        }
                     }
                   
                     uutHP[idNo].result[uutHP[idNo].curFixStep] = uutHP[idNo].HpResult[uutHP[idNo].curFixStep].result;
                     udcHpResult.SetResult(idNo, uutHP[idNo].curFixStep, uutHP[idNo].HpResult[uutHP[idNo].curFixStep].result);
                     udcHpData.setTestVal(uutHP[idNo].serialNo, uutHP[idNo].HpResult, uutHP[idNo].curFixStep, CSysPara.mVal.C_HIPOT_MAX);

                     //按客户要求，做一个手动确认功能
                     if (CSysPara.mVal.chkhandleFail)
                     {
                         if (uutHP[idNo].HpResult[uutHP[idNo].curFixStep].result != 0)
                         {
                             if (statHP[0].doRun !=EDoRun .空闲)
                                 devPLC.write(EDevType.D, 5222 + CSysPara.mVal.StatHipotMode, 3, ref er);
                             else
                                 devPLC.write(EDevType.D, 5232 + CSysPara.mVal.StatHipotMode, 3, ref er);
                             if (MessageBox.Show("高压" + (idNo == 0 ? "一" : "二") + "测试FAIL，请确认是否重测", "高压测试不良",
                                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                             {

                                 if (!SetIOAndHPEvent(idNo, uutHP[idNo].curFixStep, ref er))
                                 {
                                     runLog.Log(
                                         uutHP[idNo].devName + "设置测试通道" + (uutHP[idNo].curFixStep + 1).ToString() +
                                         "错误:" + er, udcRunLog.ELog.NG);
                                     uutHP[idNo].doRun = EDoRun.报警;
                                     uutHP[idNo].result[uutHP[idNo].curFixStep] = (int)EResult.报警;
                                     return false;
                                 }

                                 System.Threading.Thread.Sleep(CSysPara.mVal.ioDelayMs);
                                 sendreaddata = string.Empty;
                                 //启动高压机测试
                                 if (!devHP[idNo].start(ref er, ref sendreaddata))
                                 {
                                     if (!devHP[idNo].start(ref er, ref sendreaddata))
                                     {
                                         runLog.Log(uutHP[idNo].devName + "启动高压测试错误:" + er, udcRunLog.ELog.NG);
                                         uutHP[idNo].doRun = EDoRun.报警;
                                         uutHP[idNo].result[uutHP[idNo].curFixStep] = (int)EResult.报警;
                                         return false;
                                     }
                                 }
                                 break;
                             }
                         }
                     }
                        //if (result != 0)
                        //{
                        //    devHPPLC.clrRunStop(idNo, ref er);
                        //    System.Threading.Thread.Sleep(1000);
                        //}

                     //判断是否测试结束?
                     int curCH = uutHP[idNo].curFixStep;
                     curStep = -1;
                     for (int i = curCH + 1; i < uutHP[idNo].chanNum / CSysPara.mVal.C_HIPOT_MAX + idNo * 4; i++)
                     {
                         if (uutHP[idNo].serialNo[i] != string.Empty)
                         {
                             curStep = i;
                             break;
                         }
                     }
                     if (curStep == -1)
                     {
                         uutHP[idNo].doRun = EDoRun.结束;
                         return true;
                     }
                     uutHP[idNo].curFixStep = curStep;
                     if (!SetIOAndHPEvent(idNo,uutHP[idNo].curFixStep, ref er))
                     {
                              runLog.Log(uutHP[idNo].devName + "设置测试通道" + (uutHP[idNo].curFixStep + 1).ToString() +
                                          "错误:" + er, udcRunLog.ELog.NG);
                              uutHP[idNo].doRun = EDoRun.报警;
                              uutHP[idNo].result[uutHP[idNo].curFixStep] = (int)EResult.报警;
                              return false;
                     }
         
                     System.Threading.Thread.Sleep(CSysPara.mVal.ioDelayMs);
                        sendreaddata = string.Empty;
                        //启动高压机测试
                        if (!devHP[idNo].start( ref er,ref sendreaddata ))
                        {
                            if (!devHP[idNo].start(ref er,ref sendreaddata ))
                            {
                                runLog.Log(uutHP[idNo].devName + "启动高压测试错误:" + er, udcRunLog.ELog.NG);
                                uutHP[idNo].doRun = EDoRun.报警;
                                uutHP[idNo].result[uutHP[idNo].curFixStep] = (int)EResult.报警;
                                return false;
                            }
                        }
                        if (!SaveHipotLogData(idNo, sendreaddata, ref er))
                        {
                            runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                        }
                        runLog.Log(uutHP[idNo].devName + "测试通道【" + (uutHP[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutHP[idNo].startTimes = System.Environment.TickCount;
                        uutHP[idNo].runTriger = false;
                        uutHP[idNo].resetStart = false;  
                        uutHP[idNo].doRun = EDoRun.测试;
                        break;
                    case EDoRun.结束:
                        break;
                    case EDoRun.报警:
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                runLog.Log(uutHP[idNo].devName + ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #endregion

        #region 高压机HYESA-140
        private int c_HP_MaxCH = 1;
        private int c_HP_Fix_Step = 1;
        private ReaderWriterLock mThreadLock = new ReaderWriterLock();
        
        /// <summary>
        /// 获取当前测试步骤->避免单次测试全为空条码
        /// </summary>
        /// <param name="curStep"></param>
        /// <returns></returns>
        private int getCurStep(int idNo)
        {
            int curStep = c_HP_Fix_Step;
            for (int i = uutHP[idNo].curFixStep; i < c_HP_Fix_Step; i++)
            {
                for (int CH = 0; CH < c_HP_Fix_Step; CH++)
                {
                    int uutNo = i * c_HP_Fix_Step + CH;
                    if (uutHP[idNo].serialNo[uutNo] != string.Empty)
                    {
                        curStep = i;
                        break;
                    }
                }
                if(curStep!=c_HP_Fix_Step)
                    break;
            }
            return curStep;
        }
        /// <summary>
        /// 设置高压启动条件
        /// </summary>
        /// <param name="curFixStep"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool SetIOAndHPEvent(int idNo ,int curFixStep,ref string er,bool setCHEnable=false)
        {

            try
            {
                mThreadLock.AcquireWriterLock(-1);
                er = string.Empty;

                //  int chan = 0;
                CMath.WaitMs(30);
                //设置高压机有效通道
                string sendreaddata = string.Empty;
                if (!devHP[idNo].stop(ref er ,ref sendreaddata ))
                    return false;
                if (!SaveHipotLogData(idNo, sendreaddata, ref er))
                {
                    runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                }
   
                //关闭所有IO通道
                int[] mVal = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < 8; i++)
                {
                    if (CSysPara.mVal.C_HIPOT_MAX == 2)
                    {
                        if (uutHP[0].curFixStep == i)
                        {
                            mVal[i] = 1;
                            mVal[i + 8] = 1;
                        }

                        if (uutHP[1].curFixStep == i)
                        {
                            mVal[i] = 1;
                            mVal[i + 8] = 1;
                        }
                    }
                    else
                    {

                        if (uutHP[0].curFixStep == i)
                        {
                            mVal[i] = 1;
                            mVal[i + 8] = 1;
                        }

                    }

                }

                devIO.write(1, ECoilType.Y, 0, mVal, ref er);
                System.Threading.Thread.Sleep(CSysPara.mVal.ioDelayMs);
                devIO.write(1, ECoilType.Y, 0, mVal, ref er);
                System.Threading.Thread.Sleep(CSysPara.mVal.ioDelayMs);
           

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }

            finally
            {
                mThreadLock.ReleaseWriterLock();
            }
        }

        private bool CloseAllIO(ref string er)
        {
            try
            {
                mThreadLock.AcquireWriterLock(-1);
                er = string.Empty;

           

                //关闭所有IO通道
                int[] mVal = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            
                devIO.write(1, ECoilType.Y, 0, mVal, ref er);
                System.Threading.Thread.Sleep(CSysPara.mVal.ioDelayMs);
                devIO.write(1, ECoilType.Y, 0, mVal, ref er);

                System.Threading.Thread.Sleep(CSysPara.mVal.ioDelayMs);

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }

            finally
            {
                mThreadLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 获取高压测试结果
        /// </summary>
        /// <param name="curFixStep"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool GetResultFromHP(int idNo,int curFixStep, ref string er)
        {
            try
            {
                er = string.Empty;

                int uutNo = 0;

                int chanResult = 0;

                for (int i = 0; i < c_HP_MaxCH; i++)
                {
                    uutNo = curFixStep * c_HP_MaxCH + i;
                    
                    if (runModel.uutHpCH[uutNo] == 0)
                        continue;

                    List<int> stepResult = new List<int>();

                    List<string> stepCode = new List<string>();

                    List<CHPSetting.EStepName> stepMode = new List<CHPSetting.EStepName>();

                    List<string> stepVal = new List<string>();

                    List<string> stepUnit = new List<string>();

                    List<string> stepHpVolt = new List<string>();

                    int uutRealNo = curFixStep;
                    string sendreaddata = string.Empty;
                   // int mCount = runModel.step.Count;
                    if (!devHP[idNo].readResult(runModel.step.Count, ref chanResult, ref stepResult, ref stepCode,
                                                                   ref stepMode, ref stepVal, ref stepUnit, ref er,ref stepHpVolt,ref sendreaddata  ))
                    {

                        uutHP[idNo].HpResult[uutRealNo].result = 1;//出错结果为FAIL
                        return false;

                    }

                    if (!SaveHipotLogData(idNo, sendreaddata, ref er))
                    {
                        runLog.Log("保存高压读写日志出错" + er, udcRunLog.ELog.NG);
                    }
                    uutHP[idNo].HpResult[uutRealNo].chanNo = runModel.uutHpCH[uutNo];
                   
                    uutHP[idNo].HpResult[uutRealNo].result = chanResult;
                    uutHP[idNo].HpResult[uutRealNo].mVal.Clear(); 

                    for (int stepNo = 0; stepNo < stepMode.Count; stepNo++)
                    {
                        CHPSetting.CVal ItemVal = new CHPSetting.CVal();
                        //现场出现高压机电压与设定电压不一致的情况，现做比对处理（只有良品时比对，不良时电压不匹配）
                        if (CSysPara.mVal.chkHpVolt)
                        {
                            if (stepResult[stepNo] == 0)
                            {
                                try
                                {
                                    if (Math .Abs(runModel.step[stepNo].para[0].setVal - double.Parse(stepHpVolt[stepNo])) > 0.2)
                                    {
                                        //高压机在大于1000V时读出来的单位为KV，小于时读出来的单位为V
                                        if ( Math.Abs(runModel.step[stepNo].para[0].setVal*1000 -
                                                     double.Parse(stepHpVolt[stepNo])) > 1)
                                        {

                                            MessageBox.Show(
                                                "系统检测到高压机高压值异常，设定值【" + runModel.step[stepNo].para[0].setVal.ToString() +
                                                "】高压实际运行值【" +
                                                stepHpVolt[stepNo] + "】不一致，请检查高压机！", "提示", MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    runLog.Log("高压值比对出错：" + ex.ToString(), udcRunLog.ELog.NG);
                                }
                            }
                        }
                        ItemVal.name = stepMode[stepNo];

                        ItemVal.result = stepResult[stepNo];

                        ItemVal.code = stepCode[stepNo];

                        //if (ItemVal.name == CHPSetting.EStepName.AC)
                        //    ItemVal.value = stepVal[stepNo] + CSysPara.mVal.offsetAC[curFixStep];
                        //else if (ItemVal.name == CHPSetting.EStepName.DC)
                        //    ItemVal.value = stepVal[stepNo] + CSysPara.mVal.offsetDC[curFixStep];
                        //else
                        ItemVal.value = stepVal[stepNo];  

                        ItemVal.unit = stepUnit[stepNo];

                        uutHP[idNo].HpResult[uutRealNo].mVal.Add(ItemVal);    
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

        #region 测试数据
        /// <summary>
        /// 保存测试报表
        /// </summary>
        /// <param name="idNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool SaveTestData(int idNo,ref string er)
        {
            try
            {
                if (!CSysPara.mVal.saveReport)
                    return true;

                if (CSysPara.mVal.reportPath == string.Empty)
                    CSysPara.mVal.reportPath = Application.StartupPath + "\\Report";   
 
                if (!Directory.Exists(CSysPara.mVal.reportPath))
                    Directory.CreateDirectory(CSysPara.mVal.reportPath);

                string reportPath = CSysPara.mVal.reportPath+"\\";
                reportPath += DateTime.Now.ToString("yyyyMMdd") + "\\";
                reportPath += runModel.model;

                bool firstsave = false;
                if (!Directory.Exists(reportPath))
                {
                    Directory.CreateDirectory(reportPath);
                    firstsave = true;
                }

                string fileName = "HIPOT" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                string savePath = reportPath + "\\" + fileName;

                string strWrite = string.Empty;
                StreamWriter sw = new StreamWriter(savePath, true, Encoding.UTF8);

                if (firstsave)
                {
                    strWrite = "机种名,";
                    strWrite += "测试项目,";
                    strWrite += "测试规格,";
                    strWrite += "条码," ;
                    strWrite += "内码,";
                    for (int ItemNo = 0; ItemNo < runModel.step.Count; ItemNo++)
                    {
                        strWrite += runModel.step[ItemNo].name.ToString() + ",";
                    }
                    strWrite += "结果," + "时间,";
                    sw.WriteLine(strWrite);
                }
                for (int i = 0; i < statHP[idNo].slotNum; i++)
                {
                    if (!string.IsNullOrEmpty(uutHP[idNo].serialNo[i]))
                    {
                        strWrite = runModel.model  + ",";

                        for (int ItemNo = 0; ItemNo < runModel.step.Count; ItemNo++)
                        {
                            strWrite += runModel.step[ItemNo].name.ToString()+"  "  ;
                          
                               
                                if (runModel.step[ItemNo].name == CHPSetting.EStepName.AC)
                                {

                                    strWrite += runModel.step[ItemNo].para[0].setVal + "KV" + "  " ;
                                    strWrite += runModel.step[ItemNo].para[1].setVal.ToString() + "mA" + "  " + runModel.step[ItemNo].para[2].setVal.ToString() + "mA" + "  ";
                                    strWrite += "ARC"+runModel.step[ItemNo].para[3].setVal + "mA" + "  ";
                                }

                                else if (runModel.step[ItemNo].name == CHPSetting.EStepName.DC)
                                {
                                    strWrite += runModel.step[ItemNo].para[0].setVal + "KV" + "  ";
                                    strWrite += runModel.step[ItemNo].para[1].setVal.ToString() + "uA" + "  " + runModel.step[ItemNo].para[2].setVal.ToString() + "uA" + "  ";
                                    strWrite += "ARC" + runModel.step[ItemNo].para[3].setVal + "uA" + "  ";
                                }

                                else if (runModel.step[ItemNo].name == CHPSetting.EStepName.IR)
                                {
                                    strWrite += runModel.step[ItemNo].para[0].setVal + "KV" + "  ";
                                    strWrite += runModel.step[ItemNo].para[1].setVal.ToString() + "MΩ" + "  " + runModel.step[ItemNo].para[2].setVal.ToString() + "MΩ" + "  ";
                          
                                }
                                else
                                {
                                    strWrite += runModel.step[ItemNo].para[0].setVal + "KV" + "  ";
                                    strWrite += runModel.step[ItemNo].para[1].setVal.ToString() + "MΩ" + "  " + runModel.step[ItemNo].para[2].setVal.ToString() + "MΩ" + "  ";
                                }
                            
                              strWrite += ",";
                        }

                        for (int ItemNo = 0; ItemNo < runModel.step.Count; ItemNo++)
                        {
                            if (runModel.step[ItemNo].name == CHPSetting.EStepName.AC)
                            {
                                strWrite += runModel.step[ItemNo].para[1].setVal.ToString() + "mA" + "," + runModel.step[ItemNo].para[2].setVal.ToString() + "mA" + "  ";
                            }

                            else if (runModel.step[ItemNo].name == CHPSetting.EStepName.DC)
                            {
                                strWrite += runModel.step[ItemNo].para[1].setVal.ToString() + "uA" + "," + runModel.step[ItemNo].para[2].setVal.ToString() + "uA" + "  ";
                            }

                            else if (runModel.step[ItemNo].name == CHPSetting.EStepName.IR)
                            {
                                strWrite += runModel.step[ItemNo].para[1].setVal.ToString() + "MΩ" + "," + runModel.step[ItemNo].para[2].setVal.ToString() + "MΩ" + "  ";
                            }
                            else
                            {
                                strWrite += runModel.step[ItemNo].para[1].setVal.ToString() + "MΩ" + "," + runModel.step[ItemNo].para[2].setVal.ToString() + "MΩ" + "  ";
                            }

                            strWrite += ",";
                        }

                        strWrite +=  statHP[idNo].serialNo[i] +",";
                        strWrite += statHP[idNo].internalSN[i] + ",";
                        for (int ItemNo = 0; ItemNo < runModel.step.Count; ItemNo++)
                        {
                            if (CSysPara.mVal.C_HIPOT_MAX == 2)
                            {
                                if (i > 3)
                                {
                                    if (uutHP[1].HpResult[i].mVal[ItemNo].name == CHPSetting.EStepName.IR)
                                        strWrite += uutHP[1].HpResult[i].mVal[ItemNo].value +
                                                    uutHP[1].HpResult[i].mVal[ItemNo].unit + ",";
                                    else
                                        strWrite += uutHP[1].HpResult[i].mVal[ItemNo].value +
                                                    uutHP[1].HpResult[i].mVal[ItemNo].unit + ",";
                                }
                                else
                                {
                                    if (uutHP[0].HpResult[i].mVal[ItemNo].name == CHPSetting.EStepName.IR)
                                        strWrite += uutHP[0].HpResult[i].mVal[ItemNo].value +
                                                    uutHP[0].HpResult[i].mVal[ItemNo].unit + ",";
                                    else
                                        strWrite += uutHP[0].HpResult[i].mVal[ItemNo].value +
                                                    uutHP[0].HpResult[i].mVal[ItemNo].unit + ",";

                                }
                            }
                            else
                            {
                                if (uutHP[idNo].HpResult[i].mVal[ItemNo].name == CHPSetting.EStepName.IR)
                                    strWrite += uutHP[idNo].HpResult[i].mVal[ItemNo].value +
                                                uutHP[idNo].HpResult[i].mVal[ItemNo].unit + ",";
                                else
                                    strWrite += uutHP[idNo].HpResult[i].mVal[ItemNo].value +
                                                uutHP[idNo].HpResult[i].mVal[ItemNo].unit + ",";
                            }
                        }

                        if (uutHP[idNo].HpResult[i].result == 0)
                            strWrite += "PASS" + ",";
                        else
                            strWrite += "FAIL" + ",";

                        strWrite += DateTime.Now.ToString("HH:mm:ss") + ",";
                        sw.WriteLine(strWrite);
                    }
                }
                //strWrite = "测试项目,";
                //for (int i = 0; i < statHP[idNo].slotNum; i++)                
                //    strWrite += "产品"+(i + 1).ToString("D2") + ",";                
                //sw.WriteLine(strWrite);

                //for (int ItemNo = 0; ItemNo < runModel.step.Count; ItemNo++)
                //{
                //    strWrite = runModel.step[ItemNo].name.ToString()+",";
                //    for (int i = 0; i < statHP[idNo].slotNum; i++)
                //    {
                //        if (!string.IsNullOrEmpty(uutHP[idNo].serialNo[i]))
                //        {
                //            if (CSysPara.mVal.C_HIPOT_MAX == 2)
                //            {
                //                if (i > 3)
                //                {
                //                    if (uutHP[1].HpResult[i].mVal[ItemNo].name == CHPSetting.EStepName.IR)
                //                        strWrite += uutHP[1].HpResult[i].mVal[ItemNo].value +
                //                                    uutHP[1].HpResult[i].mVal[ItemNo].unit + ",";
                //                    else
                //                        strWrite += uutHP[1].HpResult[i].mVal[ItemNo].value +
                //                                    uutHP[1].HpResult[i].mVal[ItemNo].unit + ",";
                //                }
                //                else
                //                {
                //                    if (uutHP[0].HpResult[i].mVal[ItemNo].name == CHPSetting.EStepName.IR)
                //                        strWrite += uutHP[0].HpResult[i].mVal[ItemNo].value +
                //                                    uutHP[0].HpResult[i].mVal[ItemNo].unit + ",";
                //                    else
                //                        strWrite += uutHP[0].HpResult[i].mVal[ItemNo].value +
                //                                    uutHP[0].HpResult[i].mVal[ItemNo].unit + ",";

                //                }
                //            }
                //            else
                //            {
                //                if (uutHP[idNo].HpResult[i].mVal[ItemNo].name == CHPSetting.EStepName.IR)
                //                    strWrite += uutHP[idNo].HpResult[i].mVal[ItemNo].value +
                //                                uutHP[idNo].HpResult[i].mVal[ItemNo].unit + ",";
                //                else
                //                    strWrite += uutHP[idNo].HpResult[i].mVal[ItemNo].value +
                //                                uutHP[idNo].HpResult[i].mVal[ItemNo].unit + ",";
                //            }
                //        }
                //        else
                //        {
                //            strWrite += "--" + ",";
                //        }
                //    }
                //    sw.WriteLine(strWrite);
                //}
                //strWrite = "测试结果,";
                //for (int i = 0; i < uutHP[idNo].HpResult.Count; i++)
                //{
                //    if (!string.IsNullOrEmpty(uutHP[idNo].serialNo[i]))
                //    {
                //        if (uutHP[idNo].HpResult[i].result == 0)
                //            strWrite += "PASS" +",";
                //        else
                //            strWrite += "FAIL"+ ",";
                //    }
                //    else
                //    {
                //        strWrite += "--"+ ",";
                //    }
                //}                    
                //sw.WriteLine(strWrite);

                sw.Flush();
                sw.Close();
                sw = null;
                
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }

      /// <summary>
      /// 保存高压LOG
      /// </summary>
      /// <param name="er"></param>
      /// <returns></returns>
        private bool SaveHipotLogData(int idNo,string writedata,ref string er)
        {
            try
            {
                if (!CSysPara.mVal.chksaveHipotData )
                    return true;
                string hipotname = "高压一";
                if (idNo != 0)
                {
                    hipotname = "高压二";
                }
                if (CSysPara.mVal.reportPath == string.Empty)
                    CSysPara.mVal.reportPath = Application.StartupPath + "\\Report";

                if (!Directory.Exists(CSysPara.mVal.reportPath))
                    Directory.CreateDirectory(CSysPara.mVal.reportPath);

                string reportPath = CSysPara.mVal.reportPath + "\\";
                reportPath += DateTime.Now.ToString("yyyyMMdd") + "\\";
                reportPath += runModel.model;

                bool firstsave = false;
                if (!Directory.Exists(reportPath))
                {
                    Directory.CreateDirectory(reportPath);
                    firstsave = true;
                }

                string fileName =hipotname+ "SendReadData" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                string savePath = reportPath + "\\" + fileName;

                string strWrite = string.Empty;
                StreamWriter sw = new StreamWriter(savePath, true, Encoding.UTF8);

          
                strWrite +=  writedata;
                sw.WriteLine(strWrite);
            

                sw.Flush();
                sw.Close();
                sw = null;

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
