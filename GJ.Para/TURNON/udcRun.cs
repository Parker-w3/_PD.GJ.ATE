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
//using GJ.Dev.ELoad;
//using GJ.Dev.IO ;
using GJ.Dev.TCP;
using GJ.Dev.ChromaEload;
using GJ.Para.Udc.TURNON;
using GJ.Dev.ATEXY;
using GJ.Dev.PLC;
using GJ.Dev .RemIO ;
using GJ.Dev .Mon ;
namespace GJ.Para.TURNON
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

           loadModelPara(".TurnOn");  
        }
        #endregion

        #region 参数常量
        private int C_SLOT_MAX = 8;
        private int C_DEV_CHAN_MAX = 8;
        #endregion

        #region 参数路径
        private string iniFile = Application.StartupPath + "\\" + "sysLog\\" + CGlobal.CFlow.flowGUID + ".ini";
        private string sysFile = "sysLog\\" + CGlobal.CFlow.flowGUID + ".xml";
        private string sysDB = "DBLog\\" + CGlobal.CFlow.flowGUID + ".accdb";        
        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {

            //udcHpInfo = new udcHPInfo();
            //udcHpInfo.Dock = DockStyle.Fill;
            //udcHpInfo.OnBtnArgs.OnEvent += new COnEvent<udcHPInfo.COnBtnClickArgs>.OnEventHandler(OnHpInfoBtn);
            //panel2.Controls.Add(udcHpInfo, 0, 0);

            udcTurnOnInfo = new udcTURNON();
            udcTurnOnInfo.Dock = DockStyle.Fill;
            udcTurnOnInfo.OnBtnClick.OnEvent += new COnEvent<udcTURNON.COnBtnClick>.OnEventHandler(OnTurnONBtnClick);
            udcTurnOnInfo.OnBtnArgs.OnEvent += new COnEvent<udcTURNON.COnBtnClickArgs>.OnEventHandler(OnTurnONBtn);
            panel2.Controls.Add(udcTurnOnInfo, 0, 0);

            udcChromaResult = new udcTurnOnResult();
            udcChromaResult.Dock = DockStyle.Fill;
            udcChromaResult.Margin = new Padding(0);
            tabPage1.Controls.Add(udcChromaResult);

            udcChromaData = new udcTurnOnData();
            udcChromaData.Dock = DockStyle.Fill;
            udcChromaData.Margin = new Padding(0);
            tabPage2.Controls.Add(udcChromaData);

            runLog = new udcRunLog();
            runLog.mSaveName = "runLog"; 
            runLog.Dock = DockStyle.Fill;
            panel2.Controls.Add(runLog, 1, 0);

            tcpLog = new udcRunLog();
            tcpLog.mTitle = "Chroma TCP/IP";
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

            //if (CSysPara.mVal.C_Chroma_MAX == 2)
            //    C_SLOT_MAX = 16;
            //else
                C_SLOT_MAX = 8;

            statChroma = new CStat[2];
            statChroma[0] = new CStat(0, "<Chroma电子负载前工位>", C_SLOT_MAX);
            statChroma[1] = new CStat(0, "<Chroma电子负载后工位>", C_SLOT_MAX);

            uutChroma = new CDev[2];
            uutChroma[0] = new CDev(0, "<Chroma仪器1>", C_DEV_CHAN_MAX);
            uutChroma[1] = new CDev(1, "<Chroma仪器2>", C_DEV_CHAN_MAX);
          //  uutHP = new CDev(0, "<ATE" + CSysPara.mVal.StatHipotMode.ToString() + "测试设备>", C_SLOT_MAX);
            tcpName = "TurnON";
        }
        #endregion

        #region 面板操作

        #region 面板控件
      //  private udcHPInfo udcHpInfo = null;
        private udcTurnOnResult udcChromaResult = null;
        private udcTurnOnData udcChromaData = null;        
        private udcRunLog runLog = null;
        private udcRunLog tcpLog = null;
        private udcTcpRecv tcpShow = null;

        private udcTURNON udcTurnOnInfo = null;
        #endregion

        private void udcRun_Load(object sender, EventArgs e)
        {
            //if (udcHpData != null && udcHpInfo.mRunModel != null)
            //{
            //    runModel = udcHpInfo.mRunModel;
            //    udcHpData.refreshUI(runModel.step); 
            //}
        
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

            udcTurnOnInfo.ShowModel(RunModel);
            udcTurnOnInfo.mReTestTimes = 0;
            udcTurnOnInfo.mVmin = RunModel.vMin[0];
            udcTurnOnInfo.mVmax = RunModel.vMax[0];
            udcTurnOnInfo.mImin = RunModel.loadMin[0];
            udcTurnOnInfo.mImax = RunModel.loadMax[0];
            udcTurnOnInfo.mIDmin = RunModel.IDmin[0];
            udcTurnOnInfo.mIDmax = RunModel.IDmax[0];


           
            udcChromaData.mVmax = RunModel.vMax;
            udcChromaData.mVmin = RunModel.vMin;
            udcChromaData.mImax = RunModel.loadMax;
            udcChromaData.mImin = RunModel.loadMin;
            udcChromaData.mIDmax = RunModel.IDmax;
            udcChromaData.mIDmin = RunModel.IDmin;

            udcChromaData.mChkID = RunModel.ChkID;
         //   udcChromaData.mChkA = CSysPara.mVal.ChkCur;
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
        /// 按钮消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnONBtnClick(object sender, udcTURNON .COnBtnClick e)
        {
            switch (e.btnNo)
            {
                case udcTURNON.EBtnNo.选机种:
                    RunModel = udcTurnOnInfo.mRunModel;
                
 
                    udcChromaData.mVmax = RunModel.vMax;
                    udcChromaData.mVmin = RunModel.vMin;
                    udcChromaData.mImax = RunModel.loadMax;
                    udcChromaData.mImin = RunModel.loadMin;
                    udcChromaData.mIDmax = RunModel.IDmax;
                    udcChromaData.mIDmin = RunModel.IDmin;

                    udcChromaData.mChkID = RunModel.ChkID;
                    break;
          
                default:
                    break;
            }
        }

          /// <summary>
        /// TURNON选机种和调式按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnONBtn(object sender,udcTURNON.COnBtnClickArgs e)
        {
            string er = string.Empty;
            if (ChromaStartWorker1.IsBusy)
            {
                er = "TURNON正在测试中";
                    return ;
            }
            int idNo = 0;
            
          //  RunModel = udcTurnOnInfo.mRunModel;
            bool connect = true;
            if (ConnectToChroma(ref er, connect))
            {
                if (er != string.Empty)
                    runLog.Log(er, udcRunLog.ELog.Action);
            }
            else
            {
          
                runLog.Log(er, udcRunLog.ELog.NG);
                return;
            }


            if (ConnectToIO(ref er, connect))
            {
                if (er != string.Empty)
                    runLog.Log(er, udcRunLog.ELog.Action);
            }
            else
            {

                runLog.Log(er, udcRunLog.ELog.NG);
                return;
            }
            if (ConnectToMon(ref er, connect))
            {
                if (er != string.Empty)
                    runLog.Log(er, udcRunLog.ELog.Action);
            }
            else
            {

                runLog.Log(er, udcRunLog.ELog.NG);
                return;
            }

            //List<int> mPort = new List<int>();
            //mPort.Add(1);
            //mPort.Add(3);
            //mPort.Add(5);
            //mPort.Add(7);
            //string mMode = string.Empty;
            //if (RunModel.loadMode[0] == 0)
            //    mMode = "CCH";
            //else if (RunModel.loadMode[0] == 1)
            //    mMode = "CV";
            //else
            //    mMode = "CPH";

            //if (devChroma6334.SetPara(mPort, mMode, RunModel.loadSet[0], ref er))
            //{
            //    if (er != string.Empty)
            //        runLog.Log(er, udcRunLog.ELog.Action);
            //}
            //else
            //{

            //    runLog.Log(er, udcRunLog.ELog.NG);
            //    return;
            //}

            //if (devChromaID.SetPara(mPort, mMode, 0, ref er))
            //{
            //    if (er != string.Empty)
            //        runLog.Log(er, udcRunLog.ELog.Action);
            //}
            //else
            //{

            //    runLog.Log(er, udcRunLog.ELog.NG);
            //    return;
            //}

            uutChroma[idNo].doRun = EDoRun.就绪;
            uutChroma [idNo].curFixStep = 0;
            string sNowTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
            for (int i = 0; i < uutChroma[idNo].chanNum; i++)
            {
                uutChroma[idNo].serialNo[i] = sNowTime1 + (i + 1).ToString("D2");
                uutChroma[idNo].result[i] = (int)EResult.空闲;
                for (int j = 0; j < 3; j++)
                {
                    uutChroma[idNo].Volt[j][i] = -1;
                    uutChroma[idNo].Cur[j][i] = -1;
                    uutChroma[idNo].ID[j][i] = -1;
                }
            }
            udcChromaResult.SetFix("0000000000", uutChroma[idNo].serialNo, idNo);
            udcChromaResult.clrResult(idNo);
            udcChromaData.SetClr();
            mChromaDebug[idNo] = true;
    
            if (!ChromaStartWorker1.IsBusy)
                ChromaStartWorker1.RunWorkerAsync(); 
            //if (!SetChroma6334Para())
            //{
            //}
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
            放电,
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
               //   HpResult.Add(new CHPSetting.CStepVal());
              }
            }        
            public int statId = 0;
            public string statName = "";
            public int slotNum = 8;
            public string modelName = string.Empty;
            public string idCard = string.Empty;
            public List<string> serialNo = new List<string>();
            public List<int> result = new List<int>();
            public List <string > internalSN = new List<string>();
            public int ready = 0;
            public EDoRun doRun = EDoRun.空闲;
            public int startTimes = 0;
            public int runTimes = 0;
           // public List<CHPSetting.CStepVal> HpResult = new List<CHPSetting.CStepVal>();
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
                for (int j = 0; j < 3; j++)
                {
                    Volt[j] = new List<double>();
                    Cur[j] = new List<double>();
                    ID[j] = new List<double>();
                }
               
                for (int i = 0; i < chanNum; i++)
                {
                    serialNo.Add("");
                    result.Add(0);
                    for (int j = 0; j < 3; j++)
                    {  
                        Volt[j].Add(0);
                        Cur[j].Add(0);
                        ID[j].Add(0);
                    }
                    //Volt1.Add(0);
                    //Cur1.Add(0);
                    //ID1.Add(0);
                    //Volt2.Add(0);
                    //Cur2.Add(0);
                    //ID2.Add(0);
                     
                }
            }
            public int chanNum = 8; //通道
            public int chanStep = 2;   //次数
            public int devId = 0;
            public string devName = "";                   
            public List<string> serialNo = new List<string>();
            public List<int> result = new List<int>();
            public List<double>[] Volt =new List<double>[3];
            public List<double>[] Cur = new List<double>[3];
            public List<double>[] ID = new List<double>[3];

            //public List<double> Volt1 = new List<double>();
            //public List<double> Cur1 = new List<double>();
            //public List<double> ID1 = new List<double>();
            //public List<double> Volt2 = new List<double>();
            //public List<double> Cur2 = new List<double>();
            //public List<double> ID2 = new List<double>();

            public List<string> failInfo = new List<string>();
            public EDoRun doRun = EDoRun.空闲;
            public int curFixStep = 0;
            public bool runTriger = false;
            public int startTimes = 0;
            public int runTimes = 0;
            public int testTimes = 0;
            public bool resetStart = false;
           // public List<CHPSetting.CStepVal> HpResult = new List<CHPSetting.CStepVal>();
        }
      //  private CModelPara runModel = new CModelPara();
        private CStat[] statChroma = null;
        private CDev[] uutChroma = null;
       
        private int mChromaIniPara = 0;
        #endregion

        #region 通信设备参数
    
        private IO_24_16 devIO = null;
        private Chroma6334 devChroma6334 = null;
        private Chroma6334 devChromaID = null;
        private CModelPara RunModel = new CModelPara();
        private SajetMES MesSajet = null;
        private CGJMonCom devMon32 = null;
        #endregion

        #region 设备初始化
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
        /// <summary>
        /// 连接电子负载
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ConnectToChroma(ref string er, bool connect = true)
        {
            try
            {
                er = string.Empty;
                if (connect)
                {
                    if (devChroma6334  == null)
                    {
                        devChroma6334 = new Chroma6334();
                        if (!devChroma6334.open(CSysPara.mVal.ChromaEloadCom , ref er))
                        {
                            er = "打开Chroma电子负载串口失败:" + er;
                            devChroma6334.close();
                            devChroma6334 = null;
                            return false;
                        }
                       
                        if (!devChroma6334.init(ref er))
                        {
                            CMath.delayMs(1000);
                            if (!devChroma6334.init(ref er))
                            {
                                er = "初始化Chroma电子负载失败:" + er;
                                devChroma6334.close();
                                devChroma6334 = null;
                                return false;
                            }
                        }
                        
                        er = "成功打开Chroma开机电子负载,初始化Chroma开机电子负载成功";
                    }
                    if (CSysPara.mVal.ChkID)
                    {
                        if (devChromaID == null)
                        {
                            devChromaID = new Chroma6334();
                            if (!devChromaID.open(CSysPara.mVal.IDCom, ref er))
                            {
                                er = "打开Chroma ID电子负载串口失败:" + er;
                                devChromaID.close();
                                devChromaID = null;
                                return false;
                            }
                            if (!devChromaID.init(ref er))
                            {
                                CMath.delayMs(1000);
                                if (!devChromaID.init(ref er))
                                {
                                    er = "初始化Chroma ID电子负载失败:" + er;
                                    devChromaID.close();
                                    devChromaID = null;
                                    return false;
                                }
                            }
                            er = "成功打开Chroma ID电子负载,初始化Chroma ID电子负载成功";
                        }
                    }
                }
                else
                {
                    if (devChroma6334 != null)
                    {
                        devChroma6334.close();
                        devChroma6334 = null;
                        er = "关闭Chroma电子负载串口";
                    }
                    if (CSysPara.mVal.ChkID)
                    {
                        if (devChromaID != null)
                        {
                            devChromaID.close();
                            devChromaID = null;
                            er = "关闭Chroma ID电子负载串口";
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
                        devIO = new IO_24_16();

                        
                        if (!devIO.open(CSysPara.mVal.ioCom, ref er))
                        {
                            er = "打开控制IO板串口失败:" + er;
                            devIO.close(); 
                            devIO = null;
                            return false;
                        }
                        Thread.Sleep(30);
                        int vers = 0;
                        if (!devIO.readVersion(1,ref vers,ref er))
                        {
                            er = "连接控制IO板失败:" + er;
                            devIO.close();
                            devIO = null;
                            return false;
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
        /// 设置Chroma6334程序
        /// </summary>
        /// <param name="step"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetChroma6334Para(string prgName, CModelPara runModel, ref string er)
        {
            try
            {
                if (devChroma6334  == null)
                {
                    devChroma6334 = new Chroma6334 ();
                    if (!devChroma6334.open(CSysPara.mVal.ChromaEloadCom , ref er))
                    {
                        er = "打开Chroma电子负载串口失败:" + er;
                        devChroma6334.close();
                        devChroma6334 = null;
                        return false;
                    }
                    if (!devChroma6334.init(ref er))
                    {
                        er = "初始化Chroma电子负载失败:" + er;
                        devChroma6334.close();
                        devChroma6334 = null;
                        return false;
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
        /// 连接Mon
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ConnectToMon(ref string er, bool connect = true)
        {
        
            try
            {
                
                if (RunModel !=null && !RunModel.ChkID) //不测ID机种，不需要连接监控版
                {
                    return true ;
                }
                er = string.Empty;
             
                if (connect)
                {

                    if (devMon32  == null)
                    {
                        devMon32 = new CGJMonCom();
                        if (!devMon32.open(CSysPara.mVal.MonCom , ref er))
                        {
                            er = "连接监控板通信失败:" + er;
                            devMon32 = null;
                            return false;
                        }
                        string ver = string.Empty;
                        if (!devMon32.ReadVersion(1, ref ver, ref er))
                        {
                            CMath.WaitMs(30);
                            if (!devMon32.ReadVersion(1, ref ver, ref er))
                            {
                                CMath.WaitMs(30);
                                if (!devMon32.ReadVersion(1, ref ver, ref er))
                                {
                                    CMath.WaitMs(30);
                                    er = "连接监控板通信失败:" + er;
                                    devMon32 = null;
                                    return false;
                                }
                            }

                        }
                        er = "成功连接到监控板";
                    }
                }
                else
                {
                    if (devMon32 != null)
                    {
                        devMon32.close();
                        devMon32 = null;
                        er = "断开连接监控板";
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
                    udcTurnOnInfo.SetStatus(udcTURNON.ERun.Ready);
                }
                else
                {
                    if (!StartThread(run))
                        return false;
                    if (!ConnectToDevice(run))
                        return false;
                    udcTurnOnInfo.SetStatus(udcTURNON.ERun.Idle);
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
            statChroma[0].doRun = EDoRun.空闲;
            statChroma[1].doRun = EDoRun.空闲;

            uutChroma[0].doRun = EDoRun.空闲;
            uutChroma[0].curFixStep = 0;
            uutChroma[1].doRun = EDoRun.空闲;
            uutChroma[1].curFixStep = 0;

            mChromaDebug[0] = false;
            mChromaDebug[1] = false;

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
                    if (ConnectToChroma(ref er, connect))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
                    }

              
                    if (ConnectToMon(ref er, connect))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
                    }   
                 
                     if ( ConnectToIO(ref er, connect))
                    {
                        if (er != string.Empty)
                            runLog.Log(er, udcRunLog.ELog.Action);
                    }
                    else
                    {
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
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
                    if (ConnectToMon(ref er, connect))
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
                        chkDev = false;
                        runLog.Log(er, udcRunLog.ELog.NG);
                    }   
                    if (ConnectToChroma(ref er, connect))
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
                    if (!TcpRecvWorker.IsBusy)
                        TcpRecvWorker.RunWorkerAsync();
                }
                else
                {
                    if (ChromaStartWorker1.IsBusy)
                        ChromaStartWorker1.CancelAsync();
                    if (ChromaStartWorker2.IsBusy)
                        ChromaStartWorker2.CancelAsync();
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
        private bool[] mChromaDebug = new bool[2];
        #endregion

        #region 方法

        /// <summary>
        /// 电子负载初始化线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntChromaWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                runLog.Log("开始加载Chroma参数[" + RunModel.model + "],请稍等..", udcRunLog.ELog.Action);
                string er = string.Empty;
                List<int> mPort = new List<int> {1,3,5,7 };
                string mMode = string.Empty;
                if (RunModel.loadMode[0] == 0)
                    mMode = "CCH";
                else if (RunModel.loadMode[0] == 1)
                    mMode = "CV";
                else
                    mMode = "CPH";
                if (devChroma6334.SetPara(mPort, mMode, RunModel.loadSet[0], RunModel.loadVon[0], ref er))  
                {
                    runLog.Log("加载Chroma参数[" + RunModel.model + "]失败:[" + er + "]", udcRunLog.ELog.NG);
                    mChromaIniPara = 2;
                }
                else
                {
                    runLog.Log("成功加载Chroma参数[" + RunModel.model + "].", udcRunLog.ELog.Action);
                    mChromaIniPara = 1;
                }
            }
            catch (Exception ex)
            {
                mChromaIniPara = 2;
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
                    for (int i = 0; i < statChroma.Length; i++)
                    {
                         statChroma[i].doRun = EDoRun.空闲;
                         statChroma[i].idCard = string.Empty;
                         for (int z = 0; z < statChroma[i].serialNo.Count; z++)
                             statChroma[i].serialNo[z] = string.Empty;  
                    }
                    return false;
                }
                else if (serReponse.ready == 1)
                {
                    statChroma[1].doRun = EDoRun.空闲;

                    idNo = 0;

                    if (statChroma[idNo].doRun == EDoRun.空闲)
                    {
                        statChroma[idNo].idCard = serReponse.idCard;
                        statChroma[idNo].modelName = serReponse.modelName;
                        for (int i = 0; i < serReponse.serialNo.Count-1; i++)
                            statChroma[idNo].serialNo[i] = serReponse.serialNo[i];

                        if (CSysPara.mVal.conMes)
                        {
                            for (int i = 0; i < statChroma[idNo].serialNo.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(statChroma[idNo].serialNo[i]))
                                {
                                    string mInternalSn = string.Empty;
                                    if (MesSajet.Read_internalSN(statChroma[idNo].serialNo[i], ref mInternalSn, ref er))
                                    {
                                        statChroma[idNo].internalSN[i] = mInternalSn;
                                    }
                                    else
                                    {
                                        runLog.Log("获取条码【" + statChroma[idNo].serialNo[i] + "】内码失败：" + er, udcRunLog.ELog.NG);
                                    }

                                }
                                Thread.Sleep(50);
                            }
                        }

                        udcChromaResult.SetFix(statChroma[idNo].idCard, statChroma[idNo].serialNo);
                        udcChromaResult.clrResult();
                        udcChromaData.SetClr();
                        statChroma[idNo].doRun = EDoRun.到位;

                        udcTurnOnInfo.SetStatus(udcTURNON.ERun.Ready);
                    }
                    return true;
                }
                else if (serReponse.ready == 2)
                {
                    statChroma[0].doRun = EDoRun.空闲;

                    idNo = 1;
                    if (statChroma[idNo].doRun == EDoRun.空闲)
                    {
                        statChroma[idNo].idCard = serReponse.idCard;
                        statChroma[idNo].modelName = serReponse.modelName;
                        for (int i = 0; i < serReponse.serialNo.Count-1; i++)
                            statChroma[idNo].serialNo[i] = serReponse.serialNo[i];

                        if (CSysPara.mVal.conMes)
                        {
                            for (int i = 0; i < statChroma[idNo].serialNo.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(statChroma[idNo].serialNo[i]))
                                {
                                    string mInternalSn = string.Empty;
                                    if (MesSajet.Read_internalSN(statChroma[idNo].serialNo[i], ref mInternalSn, ref er))
                                    {
                                        statChroma[idNo].internalSN[i] = mInternalSn;
                                    }
                                    else
                                    {
                                        runLog.Log("获取条码【" + statChroma[idNo].serialNo[i] + "】内码失败：" + er, udcRunLog.ELog.NG);
                                    }

                                }
                                Thread.Sleep(50);
                            }
                        }
                        udcChromaResult.SetFix(statChroma[idNo].idCard, statChroma[idNo].serialNo);
                        udcChromaResult.clrResult();
                        statChroma[idNo].doRun = EDoRun.到位;
                        udcTurnOnInfo.SetStatus(udcTURNON.ERun.Ready);
                    }
                    return true;
                }
                else
                {
                    runLog.Log("接收测试工位到位信号异常【" + serReponse.ready.ToString() + "】", udcRunLog.ELog.NG);
                    statChroma[0].doRun = EDoRun.空闲;
                    statChroma[1].doRun = EDoRun.空闲;
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

                switch (statChroma[idNo].doRun)
                {
                    case EDoRun.空闲:
                        break;
                    case EDoRun.到位:
                        for (int i = 0; i < CSysPara.mVal.C_Chroma_MAX; i++)
                        {
                            for (int CH = 0; CH < uutChroma[i].chanNum; CH++)
                            {
                                uutChroma[i].serialNo[CH] = statChroma[idNo].serialNo[CH + i * uutChroma[i].chanNum];
                                uutChroma[i].result[CH] = (int)EResult.空闲;
                                for (int j = 0; j < 3; j++)
                                {
                                    uutChroma[i].Volt[j][CH] = 0;
                                    uutChroma[i].Cur[j][CH] = 0;
                                    uutChroma[i].ID[j][CH] = 0;
                                }
                              
                            }
                            uutChroma[i].curFixStep = 0;
                            uutChroma[i].doRun = EDoRun.就绪;                               
                        }
                        if (!ChromaStartWorker1.IsBusy)
                             ChromaStartWorker1.RunWorkerAsync();
                        if (CSysPara.mVal.C_Chroma_MAX==2 && !ChromaStartWorker2.IsBusy)
                             ChromaStartWorker2.RunWorkerAsync();  
                        statChroma[idNo].doRun = EDoRun.就绪;
                        statChroma[idNo].startTimes = System.Environment.TickCount;
                        runLog.Log(statChroma[idNo].statName + "治具[" + statChroma[idNo].idCard +
                                   "]到位就绪,等待启动测试", udcRunLog.ELog.OK);
                        break;
                    case EDoRun.就绪:
                        statChroma[idNo].runTimes = System.Environment.TickCount - statChroma[idNo].startTimes;
                        udcTurnOnInfo.ShowTestTimes(statChroma[idNo].runTimes);
                        if (uutChroma[0].doRun == EDoRun.测试 || uutChroma[1].doRun == EDoRun.测试)
                        {
                            if (!START_TEST(ref er))
                            {
                                runLog.Log(statChroma[idNo].statName + "发送TCP测试信号错误:" + er, udcRunLog.ELog.NG);
                                break;
                            }
                            statChroma[idNo].doRun = EDoRun.测试;
                            udcTurnOnInfo.SetStatus(udcTURNON.ERun.Testing);
                        }
                        break;
                    case EDoRun.测试:
                        statChroma[idNo].runTimes = System.Environment.TickCount - statChroma[idNo].startTimes;
                        udcTurnOnInfo.ShowTestTimes(statChroma[idNo].runTimes);
                        for (int i = 0; i < CSysPara.mVal.C_Chroma_MAX; i++)
                        {
                            if (uutChroma[i].doRun == EDoRun.结束)
                            {
                                for (int CH = 0; CH < uutChroma[i].chanNum; CH++)
                                {
                                    statChroma[i].result[i * uutChroma[i].chanNum + CH] = uutChroma[i].result[CH];
                                }
                                uutChroma[i].doRun = EDoRun.过站;
                            }
                            else
                                return ;
                        }
                        if (CSysPara.mVal.C_Chroma_MAX==1)
                            uutChroma[1].doRun = EDoRun.过站;

                        if (uutChroma[0].doRun == EDoRun.过站 && uutChroma[1].doRun == EDoRun.过站)
                        {
                            //获取电子负载产品结果
                            bool uutPass = true;
                            int ttNum = 0;
                            int passNum = 0;
                            for (int i = 0; i < statChroma[idNo].slotNum; i++)
                            {
                                if (statChroma[idNo].serialNo[i] == string.Empty)
                                    statChroma[idNo].result[i] = 0;
                                else
                                {
                                    ttNum++;
                                    string MesResult = string.Empty;

                                    if (statChroma[idNo].result[i] == 0)
                                    {
                                        passNum++;
                                        MesResult = "PASS";
                                    }
                                    else
                                    {
                                        if (uutChroma[idNo].Volt[0][i] < 0.5)
                                            MesResult = "TURN-NG-001";
                                        else if (uutChroma[idNo].Volt[0][i] < RunModel.vMin[0])
                                            MesResult = "TURN-NG-005";
                                        else if (uutChroma[idNo].Volt[0][i] > RunModel.vMax[0])
                                            MesResult = "TURN-NG-002";
                                        else
                                            MesResult = "TURN-NG-001";

                                        statChroma[idNo].result[i] = CGlobal.CFlow.flowId;
                                        uutPass = false;
                                    }
                                    if (CSysPara.mVal.conMes)
                                    {
                                        if (MesSajet.Tran_SN(statChroma[idNo].serialNo[i], MesResult, ref er))
                                            runLog.Log("上传条码" + statChroma[idNo].serialNo[i] + "成功", udcRunLog.ELog.Content);
                                        else
                                        {
                                            if (er.Contains("EMP ERR"))
                                            {
                                                if (MesSajet.Login("222", "222", ref er))
                                                {
                                                    runLog.Log("系统检测到断网，重新登录成功", udcRunLog.ELog.NG);
                                                    if (MesSajet.Tran_SN(statChroma[idNo].serialNo[i], MesResult, ref er))
                                                        runLog.Log("上传条码" + statChroma[idNo].serialNo[i] + "成功", udcRunLog.ELog.Content);
                                                    else
                                                    {
                                                        if (statChroma[idNo].result[i] == 0)
                                                        {
                                                            if (!(er.Contains("HIPOT") || er.Contains("ATE")))
                                                                statChroma[idNo].result[i] = CGlobal.CFlow.flowId;
                                                        }
                                                        runLog.Log("上传条码" + statChroma[idNo].serialNo[i] + "错误:" + er, udcRunLog.ELog.NG);
                                                    }

                                                }
                                                else
                                                    runLog.Log("系统检测到断网，重新连接失败" + er, udcRunLog.ELog.NG);

                                            }
                                            else
                                            {
                                                runLog.Log("上传条码" + statChroma[idNo].serialNo[i] + "错误:" + er, udcRunLog.ELog.NG);
                                                if (statChroma[idNo].result[i] == 0)
                                                {
                                                    if (!(er.Contains("HIPOT") || er.Contains("ATE")))
                                                        statChroma[idNo].result[i] = CGlobal.CFlow.flowId;
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            List<int> resultUUT = new List<int>();
                            for (int i = 0; i < CSysPara.mVal.C_SLOT_MAX; i++)
                                resultUUT.Add(statChroma[idNo].result[i]);
                     

                            //发TCP过站信号    
                            if (!END_TEST(idNo, statChroma[idNo].idCard, resultUUT, ref er))
                            {
                                runLog.Log(statChroma[idNo].statName + "发送TCP过站信号错误:" + er, udcRunLog.ELog.NG);
                                break;
                            }
                            if (uutPass)
                            {
                                udcTurnOnInfo.SetStatus(udcTURNON.ERun.Pass);
                                runLog.Log(statChroma[idNo].statName + "治具[" + statChroma[idNo].idCard +
                                          "]电子负载测试:PASS,准备过站", udcRunLog.ELog.OK);
                            }
                            else
                            {
                                udcTurnOnInfo.SetStatus(udcTURNON.ERun.Fail);
                                runLog.Log(statChroma[idNo].statName + "治具[" + statChroma[idNo].idCard +
                                             "]电子负载测试:FAIL,准备过站", udcRunLog.ELog.NG);
                            }

                            if (!SaveTestData(idNo, ref er))
                            {
                                runLog.Log(statChroma[idNo].statName + "治具[" + statChroma[idNo].idCard +
                                            "]存报表错误：" + er, udcRunLog.ELog.NG);
                            }
                            
                            //  udcTurnOnInfo.ShowUUTNum(ttNum, passNum);
                            udcTurnOnInfo.AddNum(statChroma[idNo].serialNo, statChroma[idNo].result);
                            udcTurnOnInfo.AddConnectorTimes();
                            statChroma[idNo].doRun = EDoRun.过站;
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
        /// 电子负载设置1测试线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChromaStartWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int idNo = 0;
            try
            {
                runLog.Log(uutChroma[idNo].devName + "开始启动测试", udcRunLog.ELog.Action);

                uutChroma[idNo].testTimes = System.Environment.TickCount;
                udcChromaData.mChkA = CSysPara.mVal.ChkCur;
                string er = string.Empty;

                Thread.Sleep(100);

                while (true)
                {
                    if (ChromaStartWorker1.CancellationPending)
                        return;
                    if (RunModel.ChkTypeC)
                    {
                        if (!onChromaTestingTypeC(idNo))
                            return;
                    }
                    else if (RunModel.ChkTwoLoad)
                    {
                        if (!onChromaTestingTwoLoad(idNo))
                            return;
                    }
                    else if (RunModel.ChkID)
                    {
                        if (!onChromaTestingID(idNo))
                            return;
                    }
                    else
                    {
                        if (!onChromaTesting(idNo))
                            return;
                    }
                    if (uutChroma[idNo].doRun == EDoRun.结束)
                        return;
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                runLog.Log(uutChroma[idNo].devName + ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                double runTimes = ((double)(System.Environment.TickCount - uutChroma[idNo].testTimes)) / 1000;

                runLog.Log(uutChroma[idNo].devName + "测试结束:" + runTimes.ToString("0.0") + "秒", udcRunLog.ELog.Action);

                if (mChromaDebug[idNo])
                {
                    if ((idNo == 0 && !mChromaDebug[1]) || (idNo == 1 && !mChromaDebug[0]))
                    {
                        string en = string.Empty;
                        ConnectToChroma(ref en, false);
                    }
                    udcTurnOnInfo.SetDebugBtn(idNo, 0);
                    mChromaDebug[idNo] = false;
                    udcTurnOnInfo.SetDebugBtn(idNo, 0);
                    mChromaDebug[idNo] = false;
                }
            }
        }
        /// <summary>
        /// 电子负载设置2测试线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChromaStartWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            int idNo = 1;
            try
            {
                runLog.Log(uutChroma[idNo].devName + "开始启动测试", udcRunLog.ELog.Action);

                uutChroma[idNo].testTimes = System.Environment.TickCount;

                string er = string.Empty;

               

                Thread.Sleep(100);

                while (true)
                {
                    if (ChromaStartWorker2.CancellationPending)
                        return;
                    if (RunModel.ChkTypeC)
                    {
                        if (!onChromaTestingTypeC(idNo))
                            return;
                    }
                    else if (RunModel.ChkTwoLoad)
                    {
                        if (!onChromaTestingTwoLoad(idNo))
                            return;
                    }
                    else if (RunModel.ChkID )
                    {
                        if (!onChromaTestingID(idNo))
                            return;
                    }
                    else 
                    {
                        if (!onChromaTesting(idNo))
                            return;
                    }
                    if (uutChroma[idNo].doRun == EDoRun.结束)
                        return;
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                runLog.Log(uutChroma[idNo].devName + ex.ToString(), udcRunLog.ELog.Err);
            }
            finally
            {
                double runTimes = ((double)(System.Environment.TickCount - uutChroma[idNo].testTimes)) / 1000;

                runLog.Log(uutChroma[idNo].devName + "测试结束:" + runTimes.ToString("0.0") + "秒", udcRunLog.ELog.Action);

                if (mChromaDebug[idNo])
                {
                    if ((idNo == 0 && !mChromaDebug[1]) || (idNo == 1 && !mChromaDebug[0]))
                    {
                        string en = string.Empty;
                        ConnectToChroma(ref en, false);
                    }
                    udcTurnOnInfo.SetDebugBtn(idNo, 0);
                    mChromaDebug[idNo] = false;
                }
            }
        }
        /// <summary>
        /// 电子负载测试中  正常模式（不测试ID及TypeC）
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private bool onChromaTesting(int idNo)
        {
            try
            {
               
                string er = string.Empty;

                if (uutChroma[idNo].doRun == EDoRun.结束)
                    return true;

                switch (uutChroma[idNo].doRun)
                {
                    case EDoRun.空闲:
                        break;

                    case EDoRun.就绪:

                        //获取当前电子负载测试通道
                        int curStep = -1;
                        uutChroma[idNo].curFixStep = 0;
                        for (int i = 0; i < uutChroma[idNo].chanNum ; i++)
                        {
                            if (uutChroma[idNo].serialNo[i] != string.Empty)
                            {
                                curStep = i /4;
                                break;
                            }
                        }

                        if (curStep == -1)
                        {
                            uutChroma[idNo].doRun = EDoRun.结束;
                            return true;
                        }
                        //AC ON  Y0
                        if (!SetIO(0, true,ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置AC ON错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }
                        uutChroma[idNo].curFixStep = curStep;
                        //切换电子负载测试通道
                        if (!SetIO(1, false , ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置测试通道" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                      "错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }

           
                        runLog.Log(uutChroma[idNo].devName + "测试通道【" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutChroma[idNo].startTimes = System.Environment.TickCount;
                        uutChroma[idNo].runTriger = false;
                        uutChroma[idNo].resetStart = false;  
                        uutChroma[idNo].doRun = EDoRun.测试;

                       
                        
                        break;
                    case EDoRun.测试:
                        
                       // uutHP[idNo].runTimes = System.Environment.TickCount - uutHP[idNo].startTimes;

                      //  List<int> outPos = new List<int>();
                         for (int step = 0; step < 3; step++)
                         {
                            
                             if (RunModel.ChkModel[step])
                             {
                               
                                 System.Threading.Thread.Sleep(50);
                                 if (!SetChromaModelPara(step, ref er))
                                 {
                                     System.Threading.Thread.Sleep(100);
                                     if (!SetChromaModelPara(step, ref er))
                                     {
                                         runLog.Log(uutChroma[idNo].devName + "设置Chroma负载参数" + RunModel.model +
                                                    "步骤" + (step + 1).ToString() + "错误:" + er, udcRunLog.ELog.NG);
                                         uutChroma[idNo].doRun = EDoRun.报警;
                                         uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                         return false;
                                     }

                                 }
                                 runLog.Log("成功加载Chroma负载参数[" + RunModel.model + "]步骤" + (step +1 ).ToString(), udcRunLog.ELog.Action);
                                 System.Threading.Thread.Sleep(CSysPara.mVal.StepDelayTime);
                                 int runTimes = System.Environment.TickCount - uutChroma[idNo].startTimes;
                                 udcTurnOnInfo.ShowTestTimes(runTimes);

                                 List<double> mVolt = new List<double>();
                                 if (!devChroma6334.readVolt(ref mVolt, ref er))
                                 {
                                     System.Threading.Thread.Sleep(50);
                                     if (!devChroma6334.readVolt(ref mVolt, ref er))
                                     {
                                         runLog.Log(uutChroma[idNo].devName + "启动开机电子负载测试错误:" + er, udcRunLog.ELog.NG);
                                         uutChroma[idNo].doRun = EDoRun.报警;
                                         uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                         return false;
                                     }
                                 }
                                 List<double> mCur = new List<double>();
                                 if (!devChroma6334.readCur(ref mCur, ref er))
                                 {
                                     System.Threading.Thread.Sleep(50);
                                     if (!devChroma6334.readCur(ref mCur, ref er))
                                     {
                                         runLog.Log(uutChroma[idNo].devName + "启动开机电子负载测试错误:" + er, udcRunLog.ELog.NG);
                                         uutChroma[idNo].doRun = EDoRun.报警;
                                         uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                         return false;
                                     }
                                 }

                                 //大功率2组并联
                                 if (RunModel.ChkHightPower)
                                 {
                                     mCur[0] = mCur[0] + mCur[1];
                                     mCur[2] = mCur[2] + mCur[3];
                                 }
                                
                                 List<double> mID = new List<double>();
                        
                                 for (int i = 0; i < 4; i++)
                                 {
                                     uutChroma[idNo].Volt[step][i + uutChroma[idNo].curFixStep * 4] = mVolt[i];
                                     uutChroma[idNo].Cur[step][i + uutChroma[idNo].curFixStep * 4] = mCur[i];
                             
                                     if (uutChroma[idNo].Volt[step][i + uutChroma[idNo].curFixStep * 4] < RunModel.vMin[step] || uutChroma[idNo].Volt[step][i + uutChroma[idNo].curFixStep * 4] > RunModel.vMax[step])
                                     {
                                         uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 1;

                                     }       
                                     else
                                     {
                                         if (uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] != 1)
                                             uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 0;
                                     }

                                     if (CSysPara.mVal.ChkCur)
                                     {
                                         if (uutChroma[idNo].Cur[step][i + uutChroma[idNo].curFixStep * 4] < RunModel.loadMin[step] || uutChroma[idNo].Cur[step][i + uutChroma[idNo].curFixStep * 4] > RunModel.loadMax[step])
                                         {
                                             uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 1;

                                         }
                                         else
                                         {
                                             if (uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] != 1)
                                                 uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 0;
                                         }
                                     }
                                  
                                     uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4];
                                     
                                 }

                                 udcChromaData.ShowData(uutChroma[idNo].serialNo, uutChroma[idNo].Volt[step], uutChroma[idNo].Cur[step], uutChroma[idNo].ID[step], step, uutChroma[idNo].curFixStep);
                                 if (!SetChromaLoadOff(ref er))
                                 {
                                     CMath.WaitMs(300);
                                     if (!SetChromaLoadOff(ref er))
                                     {
                                         runLog.Log(uutChroma[idNo].devName + "设置电子负载LOADOFF错误:" + er, udcRunLog.ELog.NG);
                                     }
                                 }
                                 CMath.WaitMs(200);
                             }
                         }

                        for (int i = 0; i < 4; i++)
                        {
                            udcChromaResult.SetResult(idNo, i + uutChroma[idNo].curFixStep * 4, uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4]);
                        }
                        //判断是否测试结束?

                        uutChroma[idNo].curFixStep += 1;
                        if (uutChroma[idNo].curFixStep >= uutChroma[idNo].chanStep)
                         {
                             uutChroma[idNo].doRun = EDoRun.放电;
                           //  uutChroma[idNo].curFixStep=0;
                             return true;
                         }

                        curStep = -1;
                        for (int i = uutChroma[idNo].curFixStep*4; i < uutChroma[idNo].chanNum ; i++)
                        {
                             if (uutChroma[idNo].serialNo[i] != string.Empty)
                             {
                                 curStep = i / 4;
                                 break;
                             }
                        }
                        if (curStep == -1)
                        {
                          uutChroma[idNo].doRun = EDoRun.放电;
                          return true;
                        }

                         uutChroma[idNo].curFixStep = curStep;
                         if (!SetIO(curStep, true, ref er))
                          {
                                 runLog.Log(uutChroma[idNo].devName + "设置测试通道" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                           "错误:" + er, udcRunLog.ELog.NG);
                                 uutChroma[idNo].doRun = EDoRun.报警;
                                 uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                 return false;
                         
                          }
                        CMath.WaitMs(CSysPara.mVal.ioDelayMs);

                
                        runLog.Log(uutChroma[idNo].devName + "测试通道【" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutChroma[idNo].startTimes = System.Environment.TickCount;
                        uutChroma[idNo].runTriger = false;
                        uutChroma[idNo].resetStart = false;  
                        uutChroma[idNo].doRun = EDoRun.测试;
                        break;
                    case EDoRun .放电:
                        ///开启放电
                        if (!CloseIO(true, ref er))
                        {
                            runLog.Log(
                               uutChroma[idNo].devName + "设置测试通道全部关闭错误:" + er, udcRunLog.ELog.NG);
                        }
                        CMath.WaitMs(CSysPara.mVal.disChargerTime);
                     
                    
                        uutChroma[idNo].doRun = EDoRun.结束;
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
                runLog.Log(uutChroma[idNo].devName + ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
            finally
            {

            }
        }


        /// <summary>
        /// 电子负载测试中 (TypeC ,检测CC1,CC2信号)
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private bool onChromaTestingTypeC(int idNo)
        {
            try
            {

                string er = string.Empty;

                if (uutChroma[idNo].doRun == EDoRun.结束)
                    return true;

                switch (uutChroma[idNo].doRun)
                {
                    case EDoRun.空闲:
                        break;

                    case EDoRun.就绪:

                        //获取当前电子负载测试通道
                        int curStep = -1;
                        uutChroma[idNo].curFixStep = 0;
                        for (int i = 0; i < uutChroma[idNo].chanNum; i++)
                        {
                            if (uutChroma[idNo].serialNo[i] != string.Empty)
                            {
                                curStep = i / 4;
                                break;
                            }
                        }

                        if (curStep == -1)
                        {
                            uutChroma[idNo].doRun = EDoRun.结束;
                            return true;
                        }
                        ///AC ON  Y0
                        if (!SetIO(0, true, ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置AC ON错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }
                        uutChroma[idNo].curFixStep = curStep;
                        //切换电子负载测试通道
                        if (!SetIO(1, false, ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置测试通道" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                      "错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }

                        runLog.Log(uutChroma[idNo].devName + "测试通道【" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutChroma[idNo].startTimes = System.Environment.TickCount;
                        uutChroma[idNo].runTriger = false;
                        uutChroma[idNo].resetStart = false;
                        uutChroma[idNo].doRun = EDoRun.测试;
                        System.Threading.Thread.Sleep(50);
                        if (RunModel.ChkTwoLoad)
                        {
                            if (!SetChromaModelParaTwoLoad(ref er))
                            {
                                System.Threading.Thread.Sleep(100);
                                if (!SetChromaModelParaTwoLoad(ref er))
                                {
                                    runLog.Log(uutChroma[idNo].devName + "设置Chroma负载参数" + RunModel.model +
                                               "步骤" + (0 + 1).ToString() + "错误:" + er, udcRunLog.ELog.NG);
                                    uutChroma[idNo].doRun = EDoRun.报警;
                                    uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                    return false;
                                }

                            }
                        }
                        else
                        {
                            if (!SetChromaModelPara(0, ref er))
                            {
                                System.Threading.Thread.Sleep(100);
                                if (!SetChromaModelPara(0, ref er))
                                {
                                    runLog.Log(uutChroma[idNo].devName + "设置Chroma负载参数" + RunModel.model +
                                               "步骤" + (0 + 1).ToString() + "错误:" + er, udcRunLog.ELog.NG);
                                    uutChroma[idNo].doRun = EDoRun.报警;
                                    uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int) EResult.报警;
                                    return false;
                                }

                            }
                        }
                        runLog.Log("成功加载Chroma负载参数[" + RunModel.model + "]" , udcRunLog.ELog.Action);
                        break;
                    case EDoRun.测试:

                        for (int step = 0; step < RunModel.TypeCSum+1; step++)
                        {
                            //是否测试2组CC信号
                            if (RunModel.TypeCSum == 0)
                            {

                                SetIO(2, true, ref er);
                                CMath.WaitMs(CSysPara.mVal.ioDelayMs);
                                SetIO(3, true, ref er);
                            }
                            else
                            {
                                if (step == 0)
                                {
                                    SetIO(2, true, ref er);
                                    CMath.WaitMs(CSysPara.mVal.ioDelayMs);
                                    SetIO(3, false, ref er);

                                }
                                else
                                {
                                    SetIO(2, false, ref er);
                                    CMath.WaitMs(CSysPara.mVal.ioDelayMs);
                                    SetIO(3, true, ref er);
                                }
                            }
                            CMath .WaitMs (CSysPara.mVal.StepDelayTime);
                            int runTimes = System.Environment.TickCount - uutChroma[idNo].startTimes;
                            udcTurnOnInfo.ShowTestTimes(runTimes);

                            List<double> mVolt = new List<double>();
                            if (!devChroma6334.readVolt(ref mVolt, ref er))
                            {
                                System.Threading.Thread.Sleep(50);
                                if (!devChroma6334.readVolt(ref mVolt, ref er))
                                {
                                    runLog.Log(uutChroma[idNo].devName + "启动开机电子负载测试错误:" + er, udcRunLog.ELog.NG);
                                    uutChroma[idNo].doRun = EDoRun.报警;
                                    uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                    return false;
                                }
                            }
                            List<double> mCur = new List<double>();
                            if (!devChroma6334.readCur(ref mCur, ref er))
                            {
                                System.Threading.Thread.Sleep(50);
                                if (!devChroma6334.readCur(ref mCur, ref er))
                                {
                                    runLog.Log(uutChroma[idNo].devName + "启动开机电子负载测试错误:" + er, udcRunLog.ELog.NG);
                                    uutChroma[idNo].doRun = EDoRun.报警;
                                    uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                    return false;
                                }
                            }

                            List<double> mID = new List<double>();
                     

                            for (int i = 0; i < 4; i++)
                            {
                                double vMin = RunModel.vMin[0];
                                double VmAX = RunModel.vMax[0];
                                double loadMin = RunModel.loadMin[0];
                                double loadMax = RunModel.loadMax[0];
                                //是否为TypeC +USB 2组输出产品
                                if (RunModel.ChkTwoLoad)
                                {
                                    vMin = RunModel.vMin[i % 2];
                                    VmAX = RunModel.vMax[i % 2];
                                    loadMin = RunModel.loadMin[i % 2];
                                    loadMax = RunModel.loadMax[i % 2];
                                }
                                uutChroma[idNo].Volt[step][i + uutChroma[idNo].curFixStep * 4] = mVolt[i];
                                uutChroma[idNo].Cur[step][i + uutChroma[idNo].curFixStep * 4] = mCur[i];

                                if (uutChroma[idNo].Volt[step][i + uutChroma[idNo].curFixStep * 4] < vMin || uutChroma[idNo].Volt[step][i + uutChroma[idNo].curFixStep * 4] > VmAX )
                                {
                                    uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 1;

                                }
                                else
                                {
                                    if (uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] != 1)
                                        uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 0;
                                }

                                if (CSysPara.mVal.ChkCur)
                                {
                                    if (uutChroma[idNo].Cur[step][i + uutChroma[idNo].curFixStep * 4] < loadMin  || uutChroma[idNo].Cur[step][i + uutChroma[idNo].curFixStep * 4] > loadMax )
                                    {
                                        uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 1;
                                    }
                                    else
                                    {
                                        if (uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] != 1)
                                            uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 0;
                                    }
                                }
                        
                                uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4];

                            }
                   
                                udcChromaData.ShowDataTypeC(uutChroma[idNo].serialNo, uutChroma[idNo].Volt[step],
                                    uutChroma[idNo].Cur[step], 0, uutChroma[idNo].curFixStep,RunModel .ChkTwoLoad );
                            
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            udcChromaResult.SetResult(idNo, i + uutChroma[idNo].curFixStep * 4, uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4]);
                        }
                        //判断是否测试结束?

                        uutChroma[idNo].curFixStep += 1;
                        if (uutChroma[idNo].curFixStep >= uutChroma[idNo].chanStep)
                        {
                            uutChroma[idNo].doRun = EDoRun.放电;
                            //  uutChroma[idNo].curFixStep=0;
                            return true;
                        }

                        curStep = -1;
                        for (int i = uutChroma[idNo].curFixStep * 4; i < uutChroma[idNo].chanNum; i++)
                        {
                            if (uutChroma[idNo].serialNo[i] != string.Empty)
                            {
                                curStep = i / 4;
                                break;
                            }
                        }
                        if (curStep == -1)
                        {
                            uutChroma[idNo].doRun = EDoRun.放电;
                            return true;
                        }

                        uutChroma[idNo].curFixStep = curStep;
                        if (!SetIO(curStep, true, ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置测试通道" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                      "错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }
                        CMath.WaitMs(CSysPara.mVal.ioDelayMs);


                        runLog.Log(uutChroma[idNo].devName + "测试通道【" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutChroma[idNo].startTimes = System.Environment.TickCount;
                        uutChroma[idNo].runTriger = false;
                        uutChroma[idNo].resetStart = false;
                        uutChroma[idNo].doRun = EDoRun.测试;
                        break;
                    case EDoRun.放电:
                        ///开启放电
                        if (!CloseIO(true, ref er))
                        {
                            runLog.Log(
                               uutChroma[idNo].devName + "设置测试通道全部关闭错误:" + er, udcRunLog.ELog.NG);
                        }
                        CMath.WaitMs(CSysPara.mVal.disChargerTime);


                        uutChroma[idNo].doRun = EDoRun.结束;
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
                runLog.Log(uutChroma[idNo].devName + ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
            finally
            {

            }
        }


        /// <summary>
        /// 电子负载测试中 (测试ID电压)
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private bool onChromaTestingID(int idNo)
        {
            try
            {

                string er = string.Empty;

                if (uutChroma[idNo].doRun == EDoRun.结束)
                    return true;

                switch (uutChroma[idNo].doRun)
                {
                    case EDoRun.空闲:
                        break;

                    case EDoRun.就绪:

                        //获取当前电子负载测试通道
                        int curStep = -1;
                        uutChroma[idNo].curFixStep = 0;
                        for (int i = 0; i < uutChroma[idNo].chanNum; i++)
                        {
                            if (uutChroma[idNo].serialNo[i] != string.Empty)
                            {
                                curStep = i / 4;
                                break;
                            }
                        }

                        if (curStep == -1)
                        {
                            uutChroma[idNo].doRun = EDoRun.结束;
                            return true;
                        }
                        ///AC ON  Y0
                        if (!SetIO(0, true, ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置AC ON错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }
                        uutChroma[idNo].curFixStep = curStep;
                        //切换电子负载测试通道
                        if (!SetIO(1, false, ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置测试通道" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                      "错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }
                        CMath.WaitMs(50);
                        SetIO(4, false  , ref er);
                        //if (!SetChromaModelPara(0, ref er))
                        //{
                        //    System.Threading.Thread.Sleep(100);
                        //    if (!SetChromaModelPara(0, ref er))
                        //    {
                        //        runLog.Log(uutChroma[idNo].devName + "设置Chroma负载参数" + RunModel.model +
                        //                    "步骤" + (0 + 1).ToString() + "错误:" + er, udcRunLog.ELog.NG);
                        //        uutChroma[idNo].doRun = EDoRun.报警;
                        //        uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                        //        return false;
                        //    }

                        //}

                        runLog.Log(uutChroma[idNo].devName + "测试通道【" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutChroma[idNo].startTimes = System.Environment.TickCount;
                        uutChroma[idNo].runTriger = false;
                        uutChroma[idNo].resetStart = false;
                        uutChroma[idNo].doRun = EDoRun.测试;
           
                        runLog.Log("成功加载Chroma负载参数[" + RunModel.model + "]", udcRunLog.ELog.Action);
                        break;
                    case EDoRun.测试:
                      
                          //第一步，测试LOAD电压，
                        SetIO(2, true   , ref er);
                        CMath.WaitMs(CSysPara.mVal.ioDelayMs);
                        SetIO(3, true   , ref er);
                        for (int step = 0; step < 3; step++)
                        {
                            List<double> mID = new List<double>();
                            List<double> mVolt = new List<double>();
                            List<double> mCur = new List<double>();
                            if (RunModel.ChkModel[step])
                            {
                                System.Threading.Thread.Sleep(50);
                                if (!SetChromaModelPara(step, ref er))
                                {
                                    System.Threading.Thread.Sleep(100);
                                    if (!SetChromaModelPara(step, ref er))
                                    {
                                        runLog.Log(uutChroma[idNo].devName + "设置Chroma负载参数" + RunModel.model +
                                                   "步骤" + (step + 1).ToString() + "错误:" + er, udcRunLog.ELog.NG);
                                        uutChroma[idNo].doRun = EDoRun.报警;
                                        uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                        return false;
                                    }

                                }
                                runLog.Log("成功加载Chroma负载参数[" + RunModel.model + "]步骤" + (step + 1).ToString(), udcRunLog.ELog.Action);
                                CMath.WaitMs(CSysPara.mVal.StepDelayTime);
                                int runTimes = System.Environment.TickCount - uutChroma[idNo].startTimes;
                                udcTurnOnInfo.ShowTestTimes(runTimes);

                                if (!devChroma6334.readVolt(ref mVolt, ref er))
                                {
                                    System.Threading.Thread.Sleep(50);
                                    if (!devChroma6334.readVolt(ref mVolt, ref er))
                                    {
                                        runLog.Log(uutChroma[idNo].devName + "启动开机电子负载测试错误:" + er, udcRunLog.ELog.NG);
                                        uutChroma[idNo].doRun = EDoRun.报警;
                                        uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int) EResult.报警;
                                        return false;
                                    }
                                }

                                if (!devChroma6334.readCur(ref mCur, ref er))
                                {
                                    System.Threading.Thread.Sleep(50);
                                    if (!devChroma6334.readCur(ref mCur, ref er))
                                    {
                                        runLog.Log(uutChroma[idNo].devName + "启动开机电子负载测试错误:" + er, udcRunLog.ELog.NG);
                                        uutChroma[idNo].doRun = EDoRun.报警;
                                        uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int) EResult.报警;
                                        return false;
                                    }
                                }

                                devMon32.SetScanAll();
                                CMath.WaitMs(CSysPara.mVal.StepDelayTime);
                                CVoltVal RVolt = new CVoltVal();
                                if (!  devMon32.ReadVolt(1, ref RVolt, ref er, ESynON.异步))
                                {
                                    System.Threading.Thread.Sleep(50);
                                    if (!devMon32.ReadVolt(1, ref RVolt, ref er, ESynON.异步))
                                    {
                                        System.Threading.Thread.Sleep(50);
                                        if (!devMon32.ReadVolt(1, ref RVolt, ref er, ESynON.异步))
                                        {
                                            runLog.Log(uutChroma[idNo].devName + "读取ID电压错误:" + er, udcRunLog.ELog.NG);
                                            uutChroma[idNo].doRun = EDoRun.报警;
                                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int) EResult.报警;
                                        }
                                    }

                                }
                                for (int i = 0; i < 4; i++)
                                {
                                    mID.Add((RVolt.volt[i + 4] > RVolt.volt[i]) ? RVolt.volt[i + 4] : RVolt.volt[i]);
                                    if (mID[i] < RunModel.IDmin[step] / 2) //清除显示
                                    {
                                        mID[i] = 0;
                                    }

                                }



                                for (int i = 0; i < 4; i++)
                                {
                                    uutChroma[idNo].Volt[step][i + uutChroma[idNo].curFixStep*4] = mVolt[i];
                                    uutChroma[idNo].Cur[step][i + uutChroma[idNo].curFixStep * 4] = mCur[i];
                                    uutChroma[idNo].ID[step][i + uutChroma[idNo].curFixStep * 4] = mID[i];
                                    if (uutChroma[idNo].Volt[step][i + uutChroma[idNo].curFixStep * 4] < RunModel.vMin[step] ||
                                        uutChroma[idNo].Volt[step][i + uutChroma[idNo].curFixStep * 4] > RunModel.vMax[step])
                                    {
                                        uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] = 1;
                                    }
                                    else
                                    {
                                        if (uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] != 1)
                                            uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] = 0;
                                    }

                                    if (uutChroma[idNo].ID[step][i + uutChroma[idNo].curFixStep * 4] < RunModel.IDmin[step] ||
                                        uutChroma[idNo].ID[step][i + uutChroma[idNo].curFixStep * 4] > RunModel.IDmax[step])
                                    {
                                        uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] = 1;
                                    }
                                    else
                                    {
                                        if (uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] != 1)
                                            uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] = 0;
                                    }

                                    if (CSysPara.mVal.ChkCur)
                                    {
                                        if (uutChroma[idNo].Cur[step][i + uutChroma[idNo].curFixStep * 4] <
                                            RunModel.loadMin[step] ||
                                            uutChroma[idNo].Cur[step][i + uutChroma[idNo].curFixStep * 4] >
                                            RunModel.loadMax[step])
                                        {
                                            uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] = 1;
                                        }
                                        else
                                        {
                                            if (uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] != 1)
                                                uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] = 0;
                                        }
                                    }

                                    uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4] =
                                        uutChroma[idNo].result[i + uutChroma[idNo].curFixStep*4];

                                }

                                udcChromaData.ShowData(uutChroma[idNo].serialNo, uutChroma[idNo].Volt[step],
                                    uutChroma[idNo].Cur[step], uutChroma[idNo].ID[step], step, uutChroma[idNo].curFixStep);

                                if (!SetChromaLoadOff(ref er))
                                {
                                    CMath.WaitMs(300);
                                    if (!SetChromaLoadOff(ref er))
                                    {
                                        runLog.Log(uutChroma[idNo].devName + "设置电子负载LOADOFF错误:" + er, udcRunLog.ELog.NG);
                                    }
                                }
                                CMath.WaitMs(200);
                            }
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            udcChromaResult.SetResult(idNo, i + uutChroma[idNo].curFixStep * 4, uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4]);
                        }
                       
            
                        //判断是否测试结束?
                        uutChroma[idNo].curFixStep += 1;
                        if (uutChroma[idNo].curFixStep >= uutChroma[idNo].chanStep)
                        {
                            uutChroma[idNo].doRun = EDoRun.放电;
                            //  uutChroma[idNo].curFixStep=0;
                            return true;
                        }

                        curStep = -1;
                        for (int i = uutChroma[idNo].curFixStep * 4; i < uutChroma[idNo].chanNum; i++)
                        {
                            if (uutChroma[idNo].serialNo[i] != string.Empty)
                            {
                                curStep = i / 4;
                                break;
                            }
                        }
                        if (curStep == -1)
                        {
                            uutChroma[idNo].doRun = EDoRun.放电;
                            return true;
                        }

                        uutChroma[idNo].curFixStep = curStep;
             
                        if (!SetIO(curStep, true, ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置测试通道" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                      "错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;
                        }
                        CMath.WaitMs(CSysPara.mVal.ioDelayMs); 
                        SetIO(4, true   , ref er);

                        runLog.Log(uutChroma[idNo].devName + "测试通道【" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutChroma[idNo].startTimes = System.Environment.TickCount;
                        uutChroma[idNo].runTriger = false;
                        uutChroma[idNo].resetStart = false;
                        uutChroma[idNo].doRun = EDoRun.测试;
                        break;
                    case EDoRun.放电:
                        ///开启放电
                        if (!CloseIO(true, ref er))
                        {
                            runLog.Log(
                               uutChroma[idNo].devName + "设置测试通道全部关闭错误:" + er, udcRunLog.ELog.NG);
                        }
                        CMath.WaitMs(CSysPara.mVal.disChargerTime);


                        uutChroma[idNo].doRun = EDoRun.结束;
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
                runLog.Log(uutChroma[idNo].devName + ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
            finally
            {

            }
        }


        /// <summary>
        /// 电子负载测试中  产品双组输出模式（及TypeC）
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        private bool onChromaTestingTwoLoad(int idNo)
        {
            try
            {

                string er = string.Empty;

                if (uutChroma[idNo].doRun == EDoRun.结束)
                    return true;

                switch (uutChroma[idNo].doRun)
                {
                    case EDoRun.空闲:
                        break;

                    case EDoRun.就绪:

                        //获取当前电子负载测试通道
                        int curStep = -1;
                        uutChroma[idNo].curFixStep = 0;
                        for (int i = 0; i < uutChroma[idNo].chanNum; i++)
                        {
                            if (uutChroma[idNo].serialNo[i] != string.Empty)
                            {
                                curStep = i / 4;
                                break;
                            }
                        }

                        if (curStep == -1)
                        {
                            uutChroma[idNo].doRun = EDoRun.结束;
                            return true;
                        }
                        //AC ON  Y0
                        if (!SetIO(0, true, ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置AC ON错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }
                        uutChroma[idNo].curFixStep = curStep;
                        //切换电子负载测试通道
                        if (!SetIO(1, false, ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置测试通道" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                      "错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }

                        runLog.Log(uutChroma[idNo].devName + "测试通道【" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutChroma[idNo].startTimes = System.Environment.TickCount;
                        uutChroma[idNo].runTriger = false;
                        uutChroma[idNo].resetStart = false;
                        uutChroma[idNo].doRun = EDoRun.测试;

                        break;
                    case EDoRun.测试:
                        System.Threading.Thread.Sleep(50);
                        if (!SetChromaModelParaTwoLoad( ref er))
                        {
                            System.Threading.Thread.Sleep(100);
                            if (!SetChromaModelParaTwoLoad( ref er))
                            {
                                runLog.Log(uutChroma[idNo].devName + "设置Chroma负载参数" + RunModel.model +
                                            "错误:" + er, udcRunLog.ELog.NG);
                                uutChroma[idNo].doRun = EDoRun.报警;
                                uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                return false;
                            }

                        }
                        runLog.Log("成功加载Chroma负载参数[" + RunModel.model + "]" , udcRunLog.ELog.Action);
                        System.Threading.Thread.Sleep(CSysPara.mVal.StepDelayTime);
                        int runTimes = System.Environment.TickCount - uutChroma[idNo].startTimes;
                        udcTurnOnInfo.ShowTestTimes(runTimes);

                        List<double> mVolt = new List<double>();
                        if (!devChroma6334.readVolt(ref mVolt, ref er))
                        {
                            System.Threading.Thread.Sleep(50);
                            if (!devChroma6334.readVolt(ref mVolt, ref er))
                            {
                                runLog.Log(uutChroma[idNo].devName + "启动开机电子负载测试错误:" + er, udcRunLog.ELog.NG);
                                uutChroma[idNo].doRun = EDoRun.报警;
                                uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                return false;
                            }
                        }
                        List<double> mCur = new List<double>();
                        if (!devChroma6334.readCur(ref mCur, ref er))
                        {
                            System.Threading.Thread.Sleep(50);
                            if (!devChroma6334.readCur(ref mCur, ref er))
                            {
                                runLog.Log(uutChroma[idNo].devName + "启动开机电子负载测试错误:" + er, udcRunLog.ELog.NG);
                                uutChroma[idNo].doRun = EDoRun.报警;
                                uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                                return false;
                            }
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            double vMin = RunModel.vMin [i % 2];
                            double VmAX = RunModel.vMax[i % 2];
                            double loadMin = RunModel.loadMin[i % 2];
                            double loadMax = RunModel.loadMax[i % 2];
                                  
                            uutChroma[idNo].Volt[0][i + uutChroma[idNo].curFixStep * 4] = mVolt[i];
                            uutChroma[idNo].Cur[0][i + uutChroma[idNo].curFixStep * 4] = mCur[i];

                            if (uutChroma[idNo].Volt[0][i + uutChroma[idNo].curFixStep * 4] < vMin || uutChroma[idNo].Volt[0][i + uutChroma[idNo].curFixStep * 4] >VmAX )
                            {
                                uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 1;

                            }
                            else
                            {
                                if (uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] != 1)
                                    uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 0;
                            }

                            if (CSysPara.mVal.ChkCur)
                            {
                                if (uutChroma[idNo].Cur[0][i + uutChroma[idNo].curFixStep * 4] < loadMin || uutChroma[idNo].Cur[0][i + uutChroma[idNo].curFixStep * 4] >loadMax)
                                {
                                    uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 1;

                                }
                                else
                                {
                                    if (uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] != 1)
                                        uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = 0;
                                }
                            }
                   
                            uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4] = uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4];

                        }

                        udcChromaData.ShowDataTwoLoad(uutChroma[idNo].serialNo, uutChroma[idNo].Volt[0], uutChroma[idNo].Cur[0], 0, uutChroma[idNo].curFixStep);

                        for (int i = 0; i < 4; i++)
                        {
                            udcChromaResult.SetResult(idNo, i + uutChroma[idNo].curFixStep * 4, uutChroma[idNo].result[i + uutChroma[idNo].curFixStep * 4]);
                        }
                        //判断是否测试结束?

                        uutChroma[idNo].curFixStep += 1;
                        if (uutChroma[idNo].curFixStep >= uutChroma[idNo].chanStep)
                        {
                            uutChroma[idNo].doRun = EDoRun.放电;
                            //  uutChroma[idNo].curFixStep=0;
                            return true;
                        }

                        curStep = -1;
                        for (int i = uutChroma[idNo].curFixStep * 4; i < uutChroma[idNo].chanNum; i++)
                        {
                            if (uutChroma[idNo].serialNo[i] != string.Empty)
                            {
                                curStep = i / 4;
                                break;
                            }
                        }
                        if (curStep == -1)
                        {
                            uutChroma[idNo].doRun = EDoRun.放电;
                            return true;
                        }

                        uutChroma[idNo].curFixStep = curStep;
                        if (!SetIO(curStep, true, ref er))
                        {
                            runLog.Log(uutChroma[idNo].devName + "设置测试通道" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                      "错误:" + er, udcRunLog.ELog.NG);
                            uutChroma[idNo].doRun = EDoRun.报警;
                            uutChroma[idNo].result[uutChroma[idNo].curFixStep] = (int)EResult.报警;
                            return false;

                        }
                        CMath.WaitMs(CSysPara.mVal.ioDelayMs);


                        runLog.Log(uutChroma[idNo].devName + "测试通道【" + (uutChroma[idNo].curFixStep + 1).ToString() +
                                   "】开始测试", udcRunLog.ELog.OK);
                        uutChroma[idNo].startTimes = System.Environment.TickCount;
                        uutChroma[idNo].runTriger = false;
                        uutChroma[idNo].resetStart = false;
                        uutChroma[idNo].doRun = EDoRun.测试;
                        break;
                    case EDoRun.放电:
                        ///开启放电
                        if (!CloseIO(true, ref er))
                        {
                            runLog.Log(
                               uutChroma[idNo].devName + "设置测试通道全部关闭错误:" + er, udcRunLog.ELog.NG);
                        }
                        CMath.WaitMs(CSysPara.mVal.disChargerTime);


                        uutChroma[idNo].doRun = EDoRun.结束;
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
                runLog.Log(uutChroma[idNo].devName + ex.ToString(), udcRunLog.ELog.Err);
                return false;
            }
            finally
            {

            }
        }
        #endregion

        #endregion

        #region 电子负载Chroma6334A-4
        private int c_Chroma_MaxCH = 4;
        private int c_Chroma_Fix_Step = 4;
        /// <summary>
        /// 设置电子负载机种参数
        /// </summary>
        /// <param name="mModel"></param>
        /// <param name="mLoadSet"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool SetChromaModelParaID(int step, ref string er)
        {
            try
            {
                List<int> mPort = new List<int> { 1, 5};
                string mMode = string.Empty;
                if (RunModel.loadMode[step] == 0)
                {
                    mMode = "CCH";
                }
                else if (RunModel.loadMode[step] == 1)
                    mMode = "CV";
                else
                    mMode = "CPH";
                double loadSet = RunModel.loadSet[step];

                if (!devChroma6334.SetPara(mPort, mMode, loadSet,RunModel.loadVon[step], ref er))
                {
                    return false;
                }

                mPort = new List<int> { 3, 7};
                mMode = "CCH";
     
                loadSet = 0;

                if (!devChroma6334.SetParaOFF(mPort, ref er))
                {
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
        /// 切换模式
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool SetChromaLoadOff(ref string er)
        {

            List<int> mPort = new List<int> { 1, 3,5,7 };
        
            if (!devChroma6334.SetParaOFF(mPort, ref er))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置电子负载机种参数
        /// </summary>
        /// <param name="mModel"></param>
        /// <param name="mLoadSet"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool SetChromaModelPara( int step, ref string er,bool clearLoad=false )
        {
            try
            {
                List<int> mPort = new List<int> { 1, 3, 5, 7 };
                string mMode = string.Empty;
                if (RunModel.loadMode[step] == 0)
                {
                   mMode = "CCH";
                  // mMode = "CCL";
                }
                else if (RunModel.loadMode[step] == 1)
                    mMode = "CV";
                else
                    mMode = "CPH";

                //大功率2组并联
                double loadSet=RunModel.loadSet[step];
                if (RunModel.ChkHightPower)
                {
                    loadSet = RunModel.loadSet[step] / 2;
                }
                if (clearLoad)
                {
                    mMode = "CCH";
                    loadSet = 0;
                }
                if (!devChroma6334.SetPara(mPort, mMode, loadSet,RunModel.loadVon[step], ref er))
                {
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
        /// 产品双组输出
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool SetChromaModelParaTwoLoad(ref string er)
        {
            try
            {
                  int step = 0;
                List<int> mPort = new List<int> {1, 5};
                string mMode = string.Empty;

                if (RunModel.loadMode[step] == 0)
                    mMode = "CCH";
                else if (RunModel.loadMode[step] == 1)
                    mMode = "CV";
                else
                    mMode = "CPH";
                if (!devChroma6334.SetPara(mPort, mMode, RunModel.loadSet[step], RunModel.loadVon[step],ref er))
                {
                    return false;
                }

                CMath.WaitMs(50);
                step = 1;

                mPort = new List<int> {  3,  7 };

                if (RunModel.loadMode[step] == 0)
                    mMode = "CCH";
                else if (RunModel.loadMode[step] == 1)
                    mMode = "CV";
                else
                    mMode = "CPH";
                if (!devChroma6334.SetPara(mPort, mMode, RunModel.loadSet[step],RunModel.loadVon[step], ref er))
                {
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
        /// 获取当前测试步骤->避免单次测试全为空条码
        /// </summary>
        /// <param name="curStep"></param>
        /// <returns></returns>
        private int getCurStep(int idNo)
        {
            int curStep = c_Chroma_Fix_Step;
            for (int i = uutChroma[idNo].curFixStep; i < c_Chroma_Fix_Step; i++)
            {
                for (int CH = 0; CH < c_Chroma_Fix_Step; CH++)
                {
                    int uutNo = i * c_Chroma_Fix_Step + CH;
                    if (uutChroma[idNo].serialNo[uutNo] != string.Empty)
                    {
                        curStep = i;
                        break;
                    }
                }
                if(curStep!=c_Chroma_Fix_Step)
                    break;
            }
            return curStep;
        }
        /// <summary>
        /// 设置电子负载启动条件
        /// </summary>
        /// <param name="curFixStep"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool SetIO(int curFixStep,bool bOpen ,ref string er)
        {
            try
            {
                er = string.Empty;
                
                //设置IO板对应产品通道
                int setVal = 1;
                if (!bOpen)
                {
                    setVal = 0;
                }
                if (!devIO.write(1, ECoilType.Y, curFixStep, setVal, ref er))
                {
                    CMath .WaitMs (30);
                    if (!devIO.write(1, ECoilType.Y, curFixStep, setVal, ref er))
                    {
                        CMath.WaitMs(50);
                        if (!devIO.write(1, ECoilType.Y, curFixStep, setVal, ref er))
                        {
                            return false;

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
        /// 关闭IO 
        /// </summary>
        /// <param name="AllClose"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool CloseIO(bool AllClose, ref string er)
        {

            try
            {
                er = string.Empty;
                //关闭所有IO输出
                if (AllClose)
                {
                    int[] mVal = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    if (!devIO.write(1, ECoilType.Y, 0, mVal, ref er))
                    {
                        CMath.WaitMs(30);
                        if (!devIO.write(1, ECoilType.Y, 0, mVal, ref er))
                        {
                            CMath.WaitMs(50);
                            if (!devIO.write(1, ECoilType.Y, 0, mVal, ref er))
                            {
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
        /// 获取电子负载测试结果
        /// </summary>
        /// <param name="curFixStep"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool GetResultFromHP(int idNo,int curFixStep, ref string er)
        {
            try
            {
                er = string.Empty;

               
        
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
                reportPath += RunModel.model;
                bool firstsave = false;
                if (!Directory.Exists(reportPath))
                {
                    Directory.CreateDirectory(reportPath);
                    firstsave = true;
                }

                string fileName = "TURNON" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                string savePath = reportPath + "\\" + fileName;

                string strWrite = string.Empty;
                StreamWriter sw = new StreamWriter(savePath, true, Encoding.UTF8);


                if (firstsave)
                {
                     
                    strWrite = "条码," +"内码," + "第一步电压(V)," + "第一步电流(A),";
                    if (RunModel.ChkID)
                    {
                        strWrite += "第一步ID电压(V),";
                    }
                    strWrite += "第二步电压(V)," + "第二步电流(A),";
                    if (RunModel.ChkID)
                    {
                        strWrite += "第二步ID电压(V),";
                    }
                    strWrite += "第三步电压(V)," + "第三步电流(A),";
                    if (RunModel.ChkID)
                    {
                        strWrite += "第三步ID电压(V),";
                    }
                    strWrite += "结果," + "时间," + "治具ID,"; ;
                    sw.WriteLine(strWrite);
                }
         

                for (int i = 0; i < statChroma[idNo].slotNum; i++)
                {
                    if (!string.IsNullOrEmpty(statChroma[idNo].serialNo[i]))
                    {
                        strWrite = statChroma[idNo].serialNo[i] + ",";
                        strWrite = statChroma[idNo].internalSN [i] + ",";
                        for (int step = 0; step < 3; step ++)
                        {
                            if (RunModel.ChkModel[step])
                            {
                                strWrite += uutChroma[idNo].Volt[step][i] + ",";
                                strWrite += uutChroma[idNo].Cur[step][i] + ",";
                                if (RunModel.ChkID)
                                {
                                    strWrite += uutChroma[idNo].ID[step][i] + ",";
                                }
                            }
                            else
                            {
                                strWrite +="--" + ",";
                                strWrite += "--" + ",";
                                if (RunModel.ChkID)
                                {
                                    strWrite += "--" + ",";
                                }
                            }
                        }
                        if (statChroma[idNo].result[i] == 0)
                            strWrite += "PASS" + ",";
                        else
                            strWrite += "FAIL" + ",";
                        strWrite += DateTime.Now.ToString("HH:mm:ss") + ",";
                        strWrite += statChroma[idNo].idCard + ",";
                        sw.WriteLine(strWrite);
                    }
                }
              
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
