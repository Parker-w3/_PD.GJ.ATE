using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Dev.IO;
using GJ.PDB;
using System.Data;
using System.Threading;
namespace GJ.Dev.RemIO
{
    public class CIOPara
    {
        #region 构造函数
        public CIOPara(string regSetting = "PlcLog\\PLC.accdb",bool autoMode = true)
        {
            try
            {
                this.autoMode = autoMode;
                if (!System.IO.File.Exists(regSetting))
                    regSetting = "PlcLog\\PLC.accdb";
                string er = string.Empty;
                if (!load(ref er, regSetting))
                    System.Windows.Forms.MessageBox.Show(er);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
           
        }
        #endregion

        #region 内部类
        /// <summary>
        /// IO基本类
        /// </summary>
        public class CIOBase
        {
            public int address;
            public ECoilType coilType;
            public int coilId;
            public string name;
            public int visabled;
        }
        /// <summary>
        /// 多寄存器
        /// </summary>
        public class CIOMutiBase
        {
            public string name;
            public int address;
            public ECoilType coilType;
            public int startCoil;
            public int len;
        }
        /// <summary>
        /// 写寄存器值
        /// </summary>
        public class CWIOVal
        {
            public CWIOVal(string IoName, int IoVal)
            {
                this.IoName = IoName;
                this.IoVal = IoVal;
            }
            public string IoName;
            public int IoVal;
        }
        /// <summary>
        /// 写多个寄存器值
        /// </summary>
        public class CWMutiIOVal
        {
            public CWMutiIOVal(ECoilType coilType,int address, int IoNo, int[] IoVal)
            {
                this.coilType = coilType;
                this.address = address;  
                this.IoNo = IoNo;
                this.IoVal = IoVal;
            }
            public ECoilType coilType;
            public int address;
            public int IoNo;
            public int[] IoVal;
        }
        #endregion

        #region 字段
        /// <summary>
        /// IO读寄存器
        /// </summary>
        public Dictionary<string, CIOBase> rIOBit = new Dictionary<string, CIOBase>();
        /// <summary>
        /// IO读寄存器值
        /// </summary>
        public Dictionary<string, int> rIOVal = new Dictionary<string, int>();
        /// <summary>
        /// IO读寄存器绑定:寄存器名与描述
        /// </summary>
        private Dictionary<string, string> rIOMap = new Dictionary<string, string>();
        /// <summary>
        /// 扫描寄存器位置
        /// </summary>
        private List<CIOMutiBase> scanIO = new List<CIOMutiBase>();  
        /// <summary>
        /// IO写寄存器
        /// </summary>
        public Dictionary<string, CIOBase> wIOBit = new Dictionary<string, CIOBase>();
        /// <summary>
        /// IO写寄存器值
        /// </summary>
        public Dictionary<string, int> wIOVal = new Dictionary<string, int>();
        /// <summary>
        /// IO写寄存器绑定:寄存器名与描述
        /// </summary>
        private Dictionary<string, string> wIOMap = new Dictionary<string, string>();
        /// <summary>
        /// 写寄存器操作
        /// </summary>
        private volatile List<CWIOVal> wSetIOVal = new List<CWIOVal>();
        /// <summary>
        /// 写多个寄存器操作
        /// </summary>
        private volatile List<CWMutiIOVal> wSetMutiIOVal = new List<CWMutiIOVal>(); 
        #endregion

        #region 方法
        /// <summary>
        /// 加载设置
        /// </summary>
        /// <param name="er"></param>
        /// <param name="regSetting"></param>
        /// <returns></returns>
        public bool load(ref string er,string regSetting = "PlcLog\\PLC.accdb")
        {
            try
            {
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, ".", regSetting);
                DataSet ds = null;
                //读IO
                string sqlCmd = string.Empty;
                ds = new DataSet();
                sqlCmd = "select * from rIO where IOUsed=1 order by idNo";
                if (db.QuerySQL(sqlCmd, ref ds, ref er))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string keyVal = ds.Tables[0].Rows[i]["IODesc"].ToString();
                        CIOBase regBase = new CIOBase();
                        regBase.name = ds.Tables[0].Rows[i]["IOName"].ToString();
                        regBase.visabled = System.Convert.ToInt32(ds.Tables[0].Rows[i]["IOVisable"].ToString());
                        if (!getRegInfo(regBase))
                            continue;
                        if (!rIOBit.ContainsKey(keyVal))
                            rIOBit.Add(keyVal, regBase);
                        else
                            rIOBit[keyVal] = regBase;
                        if (!rIOMap.ContainsKey(regBase.name))
                            rIOMap.Add(regBase.name, keyVal);
                        else
                            rIOMap[regBase.name] = keyVal;                       
                    }
                }
                //扫描寄存器
                ds = new DataSet();
                sqlCmd = "select * from scanIO order by idNo";
                if (db.QuerySQL(sqlCmd, ref ds, ref er))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CIOMutiBase reg = new CIOMutiBase();
                        reg.name = ds.Tables[0].Rows[i]["IOName"].ToString();
                        reg.len = System.Convert.ToInt32(ds.Tables[0].Rows[i]["IOLen"].ToString());
                        if (!getMutiRegInfo(reg))
                            continue;
                        if (!scanIO.Contains(reg))
                            scanIO.Add(reg);    
                         for (int j = 0; j < reg.len; j++)
                         {
                            for (int z = 0; z < 8; z++)
                            {
                                string regName = string.Empty;
                                regName = reg.name.Substring(0, 4)+(reg.startCoil+j * 8+z).ToString();  
                                if (rIOMap.ContainsKey(regName))
                                {
                                    if (!rIOVal.ContainsKey(rIOMap[regName]))
                                        rIOVal.Add(rIOMap[regName], -1);
                                    else
                                        rIOVal[regName] = -1;
                                }
                            }
                        }                 
                    }
                }
                //写寄存器
                ds = new DataSet();
                sqlCmd = "select * from wIO where IOUsed=1 order by idNo";
                if (db.QuerySQL(sqlCmd, ref ds, ref er))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string keyVal = ds.Tables[0].Rows[i]["IODesc"].ToString();
                        CIOBase reg = new CIOBase();
                        reg.name = ds.Tables[0].Rows[i]["IOName"].ToString();
                        reg.visabled = System.Convert.ToInt32(ds.Tables[0].Rows[i]["IOVisable"].ToString());
                        if (!getRegInfo(reg))
                            continue;
                        if (!wIOBit.ContainsKey(keyVal))
                            wIOBit.Add(keyVal, reg);
                        else
                            wIOBit[keyVal] = reg;
                        if (!wIOMap.ContainsKey(reg.name))
                            wIOMap.Add(reg.name, keyVal);
                        else
                            wIOMap[reg.name] = keyVal;
                        if (wIOMap.ContainsKey(reg.name))
                        {
                            if (!wIOVal.ContainsKey(wIOMap[reg.name]))
                                wIOVal.Add(wIOMap[reg.name], -1);
                            else
                                wIOVal[wIOMap[reg.name]] = -1;
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
        /// 添加写寄存器操作
        /// </summary>
        /// <param name="regDes"></param>
        /// <param name="regVal"></param>
        /// <returns></returns>
        public bool addIoWrite(string IoDes, int IoVal)
        {
            try
            {
                wSetIOVal.Add(new CWIOVal(IoDes, IoVal));
                wIOVal[IoDes] = -1;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startREG"></param>
        /// <param name="regVal"></param>
        /// <returns></returns>
        public bool addMutiIOWrite(ECoilType coilType,int address, int IoNo, int[] IoVal)
        {
            try
            {
                string regName = string.Empty;
                for (int i = 0; i < IoVal.Length; i++)
                {
                    for (int z = 0; z < 8; z++)
                    {
                        regName = address.ToString("D2") + "_" + coilType.ToString() + (IoNo + i).ToString();
                        if (wIOVal.ContainsKey(regName))
                            wIOVal[regName] = -1;
                    }
                    
                }
                wSetMutiIOVal.Add(new CWMutiIOVal(coilType,address, IoNo, IoVal));
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
        private bool getRegInfo(CIOBase reg)
        {
            try
            {
                if (reg.name.Length < 5)
                    return false; ;
                string regAddr = reg.name.Substring(0, 2);
                reg.address = System.Convert.ToInt32(regAddr);
                string regType = reg.name.Substring(3, 1);
                if (regType == "X")
                    reg.coilType = ECoilType.X;
                else if (regType == "Y")
                    reg.coilType = ECoilType.Y;
                else
                    reg.coilType = ECoilType.D;
                string regId = reg.name.Substring(4, reg.name.Length - 4);
                reg.coilId = System.Convert.ToInt32(regId);
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
        private bool getMutiRegInfo(CIOMutiBase reg)
        {
            try
            {
                if (reg.name.Length < 2)
                    return false;
                reg.address = System.Convert.ToInt32(reg.name.Substring(0, 2));   
                string regType = reg.name.Substring(3, 1);
                if (regType == "X")
                    reg.coilType = ECoilType.X;
                else if (regType == "Y")
                    reg.coilType = ECoilType.Y;
                else
                    reg.coilType = ECoilType.D;
                reg.startCoil =System.Convert.ToInt32(reg.name.Substring(4, reg.name.Length - 4));
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion

        #region 属性
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
        /// 线程状态
        /// </summary>
        public EStatus mStatus
        {
            get { return status; }
        }
        #endregion

        #region 线程

        /// <summary>
        /// 线程状态
        /// </summary>
        public enum EStatus
        {
            空闲,
            运行,
            暂停
        }

        private CIOCom io = null;
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
        private volatile int delayMs = 50;
        /// <summary>
        /// 线程状态
        /// </summary>
        private volatile EStatus status = EStatus.空闲;
        /// <summary>
        /// 状态事件
        /// </summary>
        public COnEvent<CIOConArgs> OnStatusArgs = new COnEvent<CIOConArgs>();
        /// <summary>
        /// 数据事件
        /// </summary>
        public COnEvent<CIODataArgs> OnDataArgs = new COnEvent<CIODataArgs>(); 
        private Thread threadIO= null;
        private ReaderWriterLock IoWriteLock = new ReaderWriterLock();
        /// <summary>
        /// 启动线程
        /// </summary>
        /// <param name="plc"></param>
        public void spinUp(CIOCom  io)
        {
            this.io = io;
            if (threadIO == null)
            {
                mDispose = false;
                if (autoMode)
                {
                    mPaused = false;                   
                }
                else
                {
                    mPaused = true;
                    status = EStatus.暂停;
                }
                foreach (string keyVal in rIOBit.Keys)
                {
                    if (rIOVal.ContainsKey(keyVal))
                        rIOVal[keyVal] = -1;
                }
                foreach (string keyVal in wIOBit.Keys)
                {
                    if (wIOVal.ContainsKey(keyVal))
                        wIOVal[keyVal] = -1;
                }
                threadIO = new Thread(OnStart);
                threadIO.Name = "远程IO";
                threadIO.IsBackground = true;
                threadIO.Start();
                OnStatusArgs.OnEvented(new CIOConArgs("创建" + threadIO.Name + "监控线程"));
            }
        }
        /// <summary>
        /// 销毁线程
        /// </summary>
        public void spinDown()
        {
            if (threadIO != null)
            {
                mDispose = true;
                do
                {
                    CMath.WaitMs(20);  
                } while (mDispose);
                threadIO = null;
            }
        }
        /// <summary>
        /// 暂停线程
        /// </summary>
        public void paused()
        {
            if (!autoMode && threadIO != null)
            {
                mPaused = true;
                status = EStatus.暂停;
            }
        }
        /// <summary>
        /// 恢复线程
        /// </summary>
        public void continued()
        {
            if (!autoMode && threadIO != null)
            {
                mPaused = false;
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
                    if (!writeIO(ref er))                    
                        OnDataArgs.OnEvented(new CIODataArgs(er, false, true));
                    if (!writeMutiIO(ref er))                    
                        OnDataArgs.OnEvented(new CIODataArgs(er, false, true));
                    if (!readIO(ref er))                    
                        OnDataArgs.OnEvented(new CIODataArgs(er, false, true));                  
                    status = EStatus.运行;
                    if (!autoMode)
                    {
                        mPaused = true;
                        status = EStatus.暂停;
                        OnDataArgs.OnEvented(new CIODataArgs("扫描时间=" + (System.Environment.TickCount - scanTime).ToString() + "ms"));
                    }
                    Thread.Sleep(delayMs);
                }
            }
            catch (Exception ex)
            {
                OnStatusArgs.OnEvented(new CIOConArgs(threadIO.Name + "监控线程异常错误:" + ex.ToString(), true));
            }
            finally
            {
                mDispose = false;
                OnStatusArgs.OnEvented(new CIOConArgs(threadIO.Name + "监控线程销毁退出",true));
            }
        }
        /// <summary>
        /// 读寄存器
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool readIO(ref string er)
        {
            try
            {
                IoWriteLock.AcquireWriterLock(-1);
                bool flag = true;
                for (int i = 0; i < scanIO.Count; i++)
                {
                    bool regOk = true;

                    if (mDispose)
                        return false;
                    //读多个寄存器值
                    int rLen = scanIO[i].len * 8;
                    int[] rVal = new int[rLen];
                    if (!io.read(scanIO[i].address, scanIO[i].coilType, scanIO[i].startCoil, ref rVal, ref er))
                    {
                        er = "[" + scanIO[i].address.ToString("D2") + "_" + scanIO[i].coilType.ToString() + scanIO[i].startCoil.ToString() + "]读操作错误:" + er;
                        flag = false;
                        regOk = false; 
                    }
                    //获取寄存器值  
                    if (regOk)
                    {
                        for (int j = 0; j < scanIO[i].len; j++)
                        {
                            for (int z = 0; z < 8; z++)
                            {
                                string regName = string.Empty;
                                regName = scanIO[i].address.ToString("D2") + "_" + scanIO[i].coilType.ToString() +
                                         (scanIO[i].startCoil + j * 8 + z).ToString();
                                if (rIOMap.ContainsKey(regName))
                                    rIOVal[rIOMap[regName]] = rVal[j * 8 + z];
                                if (wIOMap.ContainsKey(regName))
                                    wIOVal[wIOMap[regName]] = rVal[j * 8 + z];
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < scanIO[i].len; j++)
                        {
                            for (int z = 0; z < 8; z++)
                            {
                                string regName = string.Empty;
                                regName = scanIO[i].address.ToString("D2") + "_" + scanIO[i].coilType.ToString() +
                                         (scanIO[i].startCoil + j * 8 + z).ToString();
                                if (rIOMap.ContainsKey(regName))
                                    rIOVal[rIOMap[regName]] = -1;
                                if (wIOMap.ContainsKey(regName))
                                    wIOVal[wIOMap[regName]] = -1;
                            }
                        }
                    }                   
                    Thread.Sleep(30);
                }
                return flag;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                IoWriteLock.ReleaseWriterLock(); 
            }
        }
        /// <summary>
        /// 写单个寄存器
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool writeIO(ref string er)
        {
            try
            {
                IoWriteLock.AcquireWriterLock(-1);
                bool flag = true;
                for (int i = 0; i < wSetIOVal.Count; )
                {
                    bool regOk = true;
                    if (mDispose)
                        return false;
                    if (!wIOBit.ContainsKey(wSetIOVal[i].IoName))
                    {
                        er = "[" + wSetIOVal[i].IoName + "]写操作错误:该寄存器不存在";
                        flag = false;
                        regOk = false;
                    }
                    string regKey = wSetIOVal[i].IoName;
                    int regVal = wSetIOVal[i].IoVal;
                    if (!io.write(wIOBit[regKey].address, wIOBit[regKey].coilType, wIOBit[regKey].coilId, regVal, ref er))
                    {
                        er = "[" + regKey + "]写操作错误:" + er;
                        flag = false;
                        regOk = false; 
                    }
                    if (regOk)
                       wSetIOVal.RemoveAt(i);
                    Thread.Sleep(30);
                }
                return flag;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                IoWriteLock.ReleaseWriterLock();  
            }
        }
        /// <summary>
        /// 写多个寄存器值
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool writeMutiIO(ref string er)
        {
            try
            {
                IoWriteLock.AcquireWriterLock(-1);
                for (int i = 0; i < wSetMutiIOVal.Count; )
                {
                    if (mDispose)
                        return false;
                    if (!io.write(wSetMutiIOVal[i].address, wSetMutiIOVal[i].coilType,
                                  wSetMutiIOVal[i].IoNo, wSetMutiIOVal[i].IoVal, ref er))
                    {
                        er = "[" + wSetMutiIOVal[i].IoNo + "]写操作错误:" + er;
                        return false;
                    }
                    wSetMutiIOVal.RemoveAt(i);
                    Thread.Sleep(30);
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
                IoWriteLock.ReleaseWriterLock();  
            }

        }
        #endregion


    }
}
