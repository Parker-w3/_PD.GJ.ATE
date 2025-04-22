using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading; 
using GJ.PDB;
namespace GJ.Dev.PLC
{
    public class CPLCPara
    {
        /// <summary>
        /// 线程状态
        /// </summary>
        public enum EStatus
        { 
         空闲,
         运行,
         暂停
        }

        #region 内部类
        /// <summary>
        /// 监控寄存器信息
        /// </summary>
        public class CMutiReg
        {
            public EDevType regType;
            public int regStartNo;
            public int regLen;
            public string regName;
        }
        /// <summary>
        /// 寄存器信息
        /// </summary>
        public class CRegBase
        {
            public EDevType regType;
            public int regNo;
            public int regBit;
            public string regName;
            public int regVisable;
        }
        /// <summary>
        /// 写寄存器值
        /// </summary>
        public class CWRegVal
        {
            public CWRegVal(string regName, int regVal)
            {
                this.regName = regName;
                this.regVal = regVal;  
            }
            public string regName;
            public int regVal;
        }
        /// <summary>
        /// 写多个寄存器值
        /// </summary>
        public class CWMutiRegVal
        {
            public CWMutiRegVal(EDevType regType, int regNo, int[] regVal)
            {
                this.regType = regType; 
                this.regNo = regNo;
                this.regVal = regVal;  
            }
            public EDevType regType;
            public int regNo;
            public int[] regVal;
        }
        #endregion

        #region 构造函数
        public CPLCPara(string regSetting = "PlcLog\\PLC.accdb",int idNo=0,string name="PLC",bool autoMode = true,int wordNum = 1)
        {
            try
            {
                this.idNo = 0;
                this.name = name; 
                this.wordNum = wordNum;
                this.autoMode = autoMode;
                if (!System.IO.File.Exists(regSetting))
                    regSetting = "PlcLog\\PLC.accdb";
                string er = string.Empty;
                if (!load(ref er,regSetting))
                    System.Windows.Forms.MessageBox.Show(er);   
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());  
            }
        }
        #endregion

        #region 字段
        private CPLCCom plc = null;
        /// <summary>
        /// 线程编号
        /// </summary>
        private int idNo = 0;
        /// <summary>
        /// 线程名称
        /// </summary>
        private string name = string.Empty; 
        /// <summary>
        /// 自动模式:不间断扫描;手动模式:需手动启动扫描
        /// </summary>
        private volatile bool autoMode = true;
        /// <summary>
        /// 暂停监控
        /// </summary>
        private volatile bool mPaused = false; 
        /// <summary>
        /// 退出线程
        /// </summary>
        private volatile bool mDispose = false;
        /// <summary>
        /// 扫描间隔
        /// </summary>
        private volatile int delayMs = 20;
        /// <summary>
        /// 读写操作状态
        /// </summary>
        private volatile bool conOpStatus = false;
        /// <summary>
        /// 线程状态
        /// </summary>
        private volatile EStatus status = EStatus.空闲;
        /// <summary>
        /// 状态事件
        /// </summary>
        public COnEvent<CPLCConArgs> OnStatusArgs = new COnEvent<CPLCConArgs>();
        /// <summary>
        /// 数据事件
        /// </summary>
        public COnEvent<CPLCDataArgs> OnDataArgs = new COnEvent<CPLCDataArgs>(); 
        /// <summary>
        /// 单字=1-->汇川;双字=2-->欧姆龙
        /// </summary>
        private int wordNum = 1;      
        /// <summary>
        /// 读寄存器
        /// </summary>
        public  Dictionary<string, CRegBase> rREG = new Dictionary<string,CRegBase>();
        /// <summary>
        /// 寄存器读值
        /// </summary>
        public Dictionary<string, int> rREGVal = new Dictionary<string, int>();
        /// <summary>
        /// 扫描寄存器位置
        /// </summary>
        private  List<CMutiReg> scanREG = new List<CMutiReg>();        
        /// <summary>
        /// 寄存器名称与描述绑定
        /// </summary>
        private Dictionary<string, string> rREGMap = new Dictionary<string, string>();
        /// <summary>
        /// 写寄存器
        /// </summary>
        public  Dictionary<string, CRegBase> wREG=new Dictionary<string,CRegBase>();
        /// <summary>
        /// 写寄存器回读值
        /// </summary>
        public Dictionary<string, int> wREGVal = new Dictionary<string, int>();
        /// <summary>
        /// 寄存器名称与描述绑定
        /// </summary>
        private Dictionary<string, string> wREGMap = new Dictionary<string, string>();
        /// <summary>
        /// 写寄存器操作
        /// </summary>
        private volatile List<CWRegVal> wSetREGVal = new List<CWRegVal>();
        /// <summary>
        /// 写多个寄存器操作
        /// </summary>
        private volatile List<CWMutiRegVal> wSetMutiREGVal = new List<CWMutiRegVal>(); 
        #endregion

        #region 属性
        /// <summary>
        /// 线程编号
        /// </summary>
        public int mIdNo
        {
            set { idNo = value; }
            get { return idNo; }
        }
        /// <summary>
        /// 线程名称
        /// </summary>
        public string mName
        {
            set { name = value; }
            get { return name; }
        }
        /// <summary>
        /// 扫描周期
        /// </summary>
        public int mDelayMs
        {
           set { delayMs = value; }
        }
        /// <summary>
        /// 自动模式和手动模式
        /// </summary>
        public bool mAutoMode
        {
            set { autoMode = value; }
        }
        /// <summary>
        /// 线程运行状态
        /// </summary>
        public EStatus mStatus
        {
            get { return status; }
        }
        /// <summary>
        /// 设备通信连接状态
        /// </summary>
        public bool mConStatus
        {
            get
            {
                if (plc == null)
                    return false;
                return plc.mConStatus;
            }
        }
        /// <summary>
        /// 读写操作状态
        /// </summary>
        public bool mConOpStat
        {
            get
            {
                if (plc == null)
                    return false;
                return conOpStatus;
            }
        }
        /// <summary>
        /// 所有写操作完毕
        /// </summary>
        public bool mWriteReady
        {
            get {
                bool isReady = true;
                plcWriteLock.AcquireWriterLock(-1);
                for (int i = 0; i < wSetREGVal.Count; i++)
                {
                    if (wREGVal[wSetREGVal[i].regName] == -1)
                        isReady = false; 
                }
                plcWriteLock.ReleaseWriterLock();
                return isReady; 
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 加载设置
        /// </summary>
        /// <param name="er"></param>
        /// <param name="regSetting"></param>
        /// <returns></returns>
        public bool load(ref string er, string regSetting = "PlcLog\\PLC.accdb")
        {
            try
            {
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, ".", regSetting);
                DataSet ds =null;
                //PLC读寄存器
                string sqlCmd = string.Empty;
                ds = new DataSet();
                sqlCmd = "select * from rREG where regUsed=1 order by idNo";
                if (db.QuerySQL(sqlCmd, ref ds, ref er))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string keyVal = ds.Tables[0].Rows[i]["regDes"].ToString();
                        CRegBase regBase = new CRegBase();                        
                        regBase.regName = ds.Tables[0].Rows[i]["regName"].ToString();
                        regBase.regVisable = System.Convert.ToInt32(ds.Tables[0].Rows[i]["regVisable"].ToString());
                        if (!getRegInfo(regBase))
                            continue;
                        if (!rREG.ContainsKey(keyVal))
                            rREG.Add(keyVal, regBase);
                        else
                            rREG[keyVal] = regBase;
                        if (!rREGMap.ContainsKey(regBase.regName))
                            rREGMap.Add(regBase.regName, keyVal);
                        else
                            rREGMap[regBase.regName] = keyVal; 
                    }
                }
                //扫描寄存器
                ds = new DataSet();
                sqlCmd = "select * from scanREG order by idNo";
                if (db.QuerySQL(sqlCmd, ref ds, ref er))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {                  
                     CMutiReg reg = new CMutiReg();
                     reg.regName = ds.Tables[0].Rows[i]["regName"].ToString();
                     reg.regLen = System.Convert.ToInt32(ds.Tables[0].Rows[i]["regLen"].ToString());
                     if (!getMutiRegInfo(reg))
                         continue;
                     if (!scanREG.Contains(reg))
                         scanREG.Add(reg);
                     if (reg.regType != EDevType.D)
                     {
                         for (int j = 0; j < reg.regLen; j++)
                         {
                             for (int z = 0; z < 8 * wordNum; z++)
                             {
                                 string regName=string.Empty;
                                 if (reg.regType == EDevType.W)
                                     regName = reg.regType.ToString() + (reg.regStartNo + j).ToString() + "." + z.ToString("D2");
                                 else
                                     regName = reg.regType.ToString() + (reg.regStartNo + j * 8 * wordNum + z).ToString(); 
                                 if (rREGMap.ContainsKey(regName))
                                 { 
                                     if(!rREGVal.ContainsKey(rREGMap[regName]))                                      
                                         rREGVal.Add(rREGMap[regName], -1);
                                     else
                                         rREGVal[regName] = -1;
                                 }   
                             }
                         }
                     }
                     else
                     {
                         for (int j = 0; j < reg.regLen; j++)
                         {
                             string regName = reg.regType.ToString() + (reg.regStartNo + j).ToString();
                             if (rREGMap.ContainsKey(regName))
                             {
                                 if (!rREGVal.ContainsKey(rREGMap[regName]))
                                     rREGVal.Add(rREGMap[regName], -1);
                                 else
                                     rREGVal[regName] = -1;
                             }    
                         }
                     }
                    }
                }
                //写寄存器
                ds = new DataSet();
                sqlCmd = "select * from wREG where regUsed=1 order by idNo";
                if (db.QuerySQL(sqlCmd, ref ds, ref er))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string keyVal = ds.Tables[0].Rows[i]["regDes"].ToString();
                        CRegBase reg = new CRegBase();
                        reg.regName = ds.Tables[0].Rows[i]["regName"].ToString();
                        reg.regVisable = System.Convert.ToInt32(ds.Tables[0].Rows[i]["regVisable"].ToString());
                        if (!getRegInfo(reg))
                            continue;
                        if(!wREG.ContainsKey(keyVal))
                            wREG.Add(keyVal,reg);
                        else
                            wREG[keyVal]=reg;
                        if(!wREGMap.ContainsKey(reg.regName))
                            wREGMap.Add(reg.regName,keyVal);
                        else
                            wREGMap[reg.regName]=keyVal; 
                        string regName = string.Empty;
                        if (reg.regType == EDevType.W)
                            regName = reg.regType.ToString() + reg.regNo.ToString() + "." + reg.regBit.ToString("D2");
                        else
                            regName = reg.regType.ToString() + reg.regNo.ToString();
                        if(wREGMap.ContainsKey(regName))
                        {
                          if(!wREGVal.ContainsKey(wREGMap[regName]))
                              wREGVal.Add(wREGMap[regName],-1);
                          else
                              wREGVal[wREGMap[regName]]=-1;
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
        /// 获取寄存器信息
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        private bool getRegInfo(CRegBase reg)
        {
            try
            {
                if(reg.regName.Length<2)
                    return false;
                Dictionary<string, int> devType = new Dictionary<string, int>();
                devType.Add("M",0);
                devType.Add("W",1);
                devType.Add("D",2);
                devType.Add("X",3);
                devType.Add("Y",4);
                string regType = reg.regName.Substring(0, 1); 
                if(!devType.ContainsKey(regType))
                   return false;
                reg.regType = (EDevType)devType[regType];
                string regVal = reg.regName.Substring(1, reg.regName.Length - 1);   
                if (reg.regType != EDevType.W)
                {
                    reg.regNo = System.Convert.ToInt32(regVal);
                    reg.regBit = 0; 
                }
                else
                {
                    string[] regValBit;
                    regValBit = regVal.Split('.');
                    if (regValBit.Length != 2)
                        return false;
                    reg.regNo = System.Convert.ToInt32(regValBit[0]);
                    reg.regBit = System.Convert.ToInt32(regValBit[1]);
                }        
                return true; 
            }
            catch (Exception)
            {
                return false;
            }
        
        }
        /// <summary>
        /// 获取寄存器信息
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        private bool getMutiRegInfo(CMutiReg reg)
        {
            try
            {
                if (reg.regName.Length < 2)
                    return false;
                Dictionary<string, int> devType = new Dictionary<string, int>();
                devType.Add("M", 0);
                devType.Add("W", 1);
                devType.Add("D", 2);
                devType.Add("X", 3);
                devType.Add("Y", 4);
                string regType = reg.regName.Substring(0, 1);
                if (!devType.ContainsKey(regType))
                    return false;
                reg.regType = (EDevType)devType[regType];
                string regVal = reg.regName.Substring(1, reg.regName.Length - 1);
                reg.regStartNo = System.Convert.ToInt32(regVal);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 添加写寄存器操作
        /// </summary>
        /// <param name="regDes"></param>
        /// <param name="regVal"></param>
        /// <returns></returns>
        public bool addREGWrite(string regDes, int regVal)
        {
            try
            {
                plcWriteLock.AcquireWriterLock(-1);
                wREGVal[regDes] = -1;
                wSetREGVal.Add(new CWRegVal(regDes, regVal));                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                plcWriteLock.ReleaseWriterLock();  
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startREG"></param>
        /// <param name="regVal"></param>
        /// <returns></returns>
        public bool addMutiREGWrite(EDevType regType, int regNo, int[] regVal)
        {
            try
            {
                plcWriteLock.AcquireWriterLock(-1);
                string regName = string.Empty;
                for (int i = 0; i < regVal.Length; i++)
                {
                    if (regType == EDevType.D)
                    {
                        regName = regType.ToString() + (regNo + i).ToString();
                        if (wREGMap.ContainsKey(regName))
                        {
                            if (wREGVal.ContainsKey(wREGMap[regName]))
                                wREGVal[wREGMap[regName]] = -1;
                        }
                    }
                    else
                    {
                        for (int z = 0; z < wordNum * 8; z++)
                        {
                            if (regType == EDevType.W)
                                regName = regType.ToString() + (regNo + i).ToString() + "." + z.ToString("D2");
                            else
                                regName = regType.ToString() + (regNo + i).ToString();
                            if (wREGVal.ContainsKey(regName))
                                wREGVal[regName] = -1;
                        }
                    }
                }
                wSetMutiREGVal.Add(new CWMutiRegVal(regType, regNo, regVal));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                plcWriteLock.ReleaseWriterLock();  
            }
        }
        #endregion

        #region 线程
        private Thread threadPLC = null;
        private ReaderWriterLock plcWriteLock = new ReaderWriterLock();
        /// <summary>
        /// 启动线程
        /// </summary>
        /// <param name="plc"></param>
        public void spinUp(CPLCCom plc)
        {
            this.plc = plc;
            if (threadPLC == null)
            {
                mDispose = false;
                if (autoMode)
                {
                    mPaused = false;
                    status = EStatus.运行;
                }
                else
                {
                    mPaused = true;
                    status = EStatus.暂停;
                }
                foreach (string keyVal in rREG.Keys)
                {
                    if (rREGVal.ContainsKey(keyVal))
                        rREGVal[keyVal] = -1;
                }
                foreach (string keyVal in wREG.Keys)
                {
                    if(wREGVal.ContainsKey(keyVal))
                        wREGVal[keyVal]=-1; 
                } 
                threadPLC = new Thread(OnStart);
                threadPLC.Name = name;
                threadPLC.IsBackground = true;
                threadPLC.Start();
                OnStatusArgs.OnEvented(new CPLCConArgs(idNo,name, "创建"+threadPLC.Name + "监控线程"));  
            }
        }
        /// <summary>
        /// 销毁线程
        /// </summary>
        public void spinDown()
        {
            if (threadPLC != null)
            {
                mDispose = true;
                do
                {
                    CMath.WaitMs(20);  
                } while (mDispose);
                threadPLC = null;
            }
        }
        /// <summary>
        /// 暂停线程
        /// </summary>
        public void paused()
        {
            if (!autoMode && threadPLC != null)
            {
                mPaused=true; 
                status = EStatus.暂停;   
            }
        }
        /// <summary>
        /// 恢复线程
        /// </summary>
        public void continued()
        {
            if (!autoMode && threadPLC != null)
            {
                mPaused = false; 
                status = EStatus.运行;
            }           
        }
        /// <summary>
        /// 线程方法
        /// </summary>
        private void OnStart()
        {
            try
            {
                while (true)
                {
                    if (mDispose)
                        return;
                    if (!autoMode && mPaused)
                    {
                        Thread.Sleep(delayMs);  
                        continue;
                    }
                    int scanTime = System.Environment.TickCount;  

                    string er = string.Empty;

                    bool OpStat = true;

                    if (!writeREG(ref er))
                    {
                        OpStat = false;
                        setPLCAlarmEvent(er);
                    }
                    if (!writeMutiREG(ref er))
                    {
                        OpStat = false;
                        setPLCAlarmEvent(er);
                    }   
                    if (!readREG(ref er))
                    {
                        OpStat = false;
                        setPLCAlarmEvent(er);
                    }
                    if (OpStat)
                        clrPLCAlarmEvent();

                    conOpStatus = OpStat;

                    if (!autoMode)
                    {
                        mPaused = true;
                        status = EStatus.暂停;
                        OnDataArgs.OnEvented(new CPLCDataArgs(idNo, name, "扫描时间=" + (System.Environment.TickCount - scanTime).ToString() + "ms"));
                    }                        
                    
                    Thread.Sleep(delayMs); 
                }
            }
            catch (Exception ex)
            {
                OnStatusArgs.OnEvented(new CPLCConArgs(idNo, name, threadPLC.Name + "监控线程异常错误:" + ex.ToString(), true)); 
            }
            finally
            {
                OnStatusArgs.OnEvented(new CPLCConArgs(idNo, name, threadPLC.Name + "监控线程销毁退出", true));
                mDispose = false;
            }
        }
        /// <summary>
        /// 读寄存器
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool readREG(ref string er)
        {
            try
            {
                plcWriteLock.AcquireWriterLock(-1);
                for (int i = 0; i < scanREG.Count; i++)
                {
                    if (mDispose)
                        return false;
                    //读多个寄存器值
                    int rLen = scanREG[i].regLen;
                    if (scanREG[i].regType != EDevType.D)
                        rLen = scanREG[i].regLen * 8 * wordNum;
                    int[] rVal = new int[rLen];
                    if (!plc.read(scanREG[i].regType, scanREG[i].regStartNo, ref rVal, ref er))
                    {
                        er = "[" + scanREG[i].regType.ToString() + scanREG[i].regStartNo.ToString() + "]读操作错误:" + er;
                        return false;
                    }
                    //获取寄存器值
                    if (scanREG[i].regType != EDevType.D)
                    {
                        for (int j = 0; j < scanREG[i].regLen; j++)
                        {
                            for (int z = 0; z < 8 * wordNum; z++)
                            {
                                string regName = string.Empty;
                                if (scanREG[i].regType == EDevType.W)
                                    regName = scanREG[i].regType.ToString() + (scanREG[i].regStartNo + j).ToString() + "." + z.ToString("D2");
                                else
                                    regName = scanREG[i].regType.ToString() + (scanREG[i].regStartNo + j * 8 * wordNum + z).ToString();
                                if (rREGMap.ContainsKey(regName))
                                    rREGVal[rREGMap[regName]] = rVal[j * 8 * wordNum + z];
                                if (wREGMap.ContainsKey(regName))
                                    wREGVal[wREGMap[regName]] = rVal[j * 8 * wordNum + z];
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < scanREG[i].regLen; j++)
                        {
                            string regName = scanREG[i].regType.ToString() + (scanREG[i].regStartNo + j).ToString();
                            if (rREGMap.ContainsKey(regName))
                                rREGVal[rREGMap[regName]] = rVal[j];
                            if (wREGMap.ContainsKey(regName))
                                wREGVal[wREGMap[regName]] = rVal[j];
                        }
                    }
                    Thread.Sleep(5);
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                plcWriteLock.ReleaseWriterLock();  
            }
        }
        /// <summary>
        /// 写单个寄存器
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool writeREG(ref string er)
        {
            try
            {
                plcWriteLock.AcquireWriterLock(-1); 
                for (int i = 0; i < wSetREGVal.Count; )
                {
                    if (mDispose)
                        return false;
                    if (!wREG.ContainsKey(wSetREGVal[i].regName))
                    {
                        er = "[" + wSetREGVal[i].regName + "]写操作错误:该寄存器不存在";
                        return false;
                    }
                    string regKey = wSetREGVal[i].regName;
                    int regVal = wSetREGVal[i].regVal;
                    if (!plc.write(wREG[regKey].regType, wREG[regKey].regNo, regVal, ref er))
                    {
                        er = "[" + regKey + "]写操作错误:" + er;
                        return false;
                    }
                    wSetREGVal.RemoveAt(i);
                    Thread.Sleep(5);
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                plcWriteLock.ReleaseWriterLock();  
            }
        }
        /// <summary>
        /// 写多个寄存器值
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool writeMutiREG(ref string er)
        {
            try
            {
                plcWriteLock.AcquireWriterLock(-1); 
                for (int i = 0; i < wSetMutiREGVal.Count; )
                {
                    if (mDispose)
                        return false;
                    if (!plc.write(wSetMutiREGVal[i].regType, wSetMutiREGVal[i].regNo, wSetMutiREGVal[i].regVal, ref er))
                    {
                        er = "[" + wSetMutiREGVal[i].regNo + "]写操作错误:" + er;
                        return false;
                    }
                    wSetMutiREGVal.RemoveAt(i);
                    Thread.Sleep(5);  
                }
                return true;
            }
            catch (Exception ex)
            {                
                 er = ex.ToString();
                return false; 
            }
            finally
            {
                plcWriteLock.ReleaseWriterLock();
            }
        }
        #endregion

        #region 异常处理
        private List<string> plcAlarmList = new List<string>();
        /// <summary>
        /// 设置PLC报警事件提示
        /// </summary>
        /// <param name="er"></param>
        private void setPLCAlarmEvent(string er)
        {
            if (!plcAlarmList.Contains(er))
            {
                plcAlarmList.Add(er);
                OnDataArgs.OnEvented(new CPLCDataArgs(idNo,name, er, false, true));
            }
        }
        private void clrPLCAlarmEvent()
        {
            plcAlarmList.Clear();
        }
        #endregion

    }
}
