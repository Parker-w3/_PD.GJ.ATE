using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; 
namespace GJ.Dev.Mon
{
    #region 事件定义
    public class CMonConArgs : EventArgs
    {
        public readonly int idNo;
        public readonly string conStatus;
        public readonly bool bErr;
        public CMonConArgs(int idNo,string conStatus, bool bErr = false)
        {
            this.idNo = idNo;
            this.conStatus = conStatus;
            this.bErr = bErr;
        }
    }
    public class CMonDataArgs : EventArgs
    {
        public readonly int idNo;
        public readonly string rData;
        public readonly bool bErr;
        public readonly bool bComplete;
        public CMonDataArgs(int idNo,string rData, bool bComplete = true, bool bErr = false)
        {
            this.idNo = idNo;
            this.rData = rData;
            this.bComplete = bComplete;
            this.bErr = bErr;
        }
    }
    #endregion
    
    public class CGJMonPara
    {
        /// <summary>
        /// 线程状态
        /// </summary>
        public enum EStatus
        {
            空闲,
            启动,
            暂停,
            运行
        }
        /// <summary>
        /// 操作状态
        /// </summary>
        public enum EOP
        { 
           禁用=-1,
           空闲=0,
           执行=1,
           完成=2
        }

        #region 构造函数
        public CGJMonPara(int idNo, string name, int maxAddr = 40, bool autoMode = true)
        {
            this.idNo = idNo;
            this.name = name;
            this.maxAddr = maxAddr;  
            this.autoMode = autoMode;
            for (int i = 0; i < maxAddr; i++)
            {
                CUUT uut = new CUUT();
                uut.wBase.addrId = i + 1;
                uut.rData.bData = EOP.空闲;
                mon.Add(uut);  
            }
        }
        #endregion

        #region 字段
        /// <summary>
        /// 线程id
        /// </summary>
        private int idNo = 0;
        /// <summary>
        /// 线程名称
        /// </summary>
        private string name = string.Empty;
        /// <summary>
        /// 自动模式:不间断扫描;手动模式:需手动启动扫描
        /// </summary>
        private bool autoMode = true;
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
        private volatile int delayMs = 10;
        /// <summary>
        /// 线程状态
        /// </summary>
        private volatile EStatus status = EStatus.空闲;
        /// <summary>
        /// 状态事件
        /// </summary>
        public COnEvent<CMonConArgs> OnStatusArgs = new COnEvent<CMonConArgs>();
        /// <summary>
        /// 数据事件
        /// </summary>
        public COnEvent<CMonDataArgs> OnDataArgs = new COnEvent<CMonDataArgs>();
        /// <summary>
        /// 最大地址
        /// </summary>
        public int maxAddr = 40;
        #endregion

        #region 属性
        public int mIdNo
        {
            get { return idNo; }
        }
        public string mName
        {
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
        /// 线程状态
        /// </summary>
        public EStatus mStatus
        {
            get { return status; }
        }
        /// <summary>
        /// 控制板运行模式
        /// </summary>
        public Mon.ERunMode mRunMode
        {
            set { runMode = value; }
            get { return runMode; } 
        }
        #endregion

        #region 线程
        private Mon.ERunMode runMode = Mon.ERunMode.主控ACONOFF及可控制快充QC2_0模式;
        private const int C_MON_DELAY =10;
        private CGJMonCom devMon = null;
        private Thread threadMon = null;
        private ReaderWriterLock monLock = new ReaderWriterLock();
        /// <summary>
        /// 启动线程
        /// </summary>
        /// <param name="plc"></param>
        public void spinUp(CGJMonCom devMon)
        {
            this.devMon = devMon;
            if (threadMon == null)
            {
                mDispose = false;
                if (autoMode)
                {
                    mPaused = false;
                    status = EStatus.启动;
                }
                else
                {
                    mPaused = true;
                    status = EStatus.暂停;
                }
                threadMon = new Thread(OnStart);
                threadMon.Name = name;
                threadMon.IsBackground = true;
                threadMon.Start();
                OnStatusArgs.OnEvented(new CMonConArgs(idNo,"创建[" + name + "]监控线程"));
            }
        }
        /// <summary>
        /// 销毁线程
        /// </summary>
        public void spinDown()
        {
            if (threadMon != null)
            {
                mDispose = true;
                do
                {
                    CMath.WaitMs(50);
                } while (mDispose);
                threadMon = null;
            }
        }
        /// <summary>
        /// 暂停线程
        /// </summary>
        public void paused()
        {
            if (!autoMode && threadMon != null)
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
            if (!autoMode && threadMon != null)
            {
                mPaused = false;
                status = EStatus.启动;
            }
        }
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
                    if (!writeData(ref er))                    
                        OnDataArgs.OnEvented(new CMonDataArgs(idNo,"["+name+"写操作]"+er, false, true));                    
                    if(!readData(ref er))
                        OnDataArgs.OnEvented(new CMonDataArgs(idNo, "[" + name + "读操作]" + er,false, true));      
                    if (!autoMode)
                    {
                        mPaused = true;
                        status = EStatus.暂停;
                        OnDataArgs.OnEvented(new CMonDataArgs(idNo,"["+name+"扫描时间]=" + (System.Environment.TickCount - scanTime).ToString() + "ms"));
                    }
                    else
                        status = EStatus.运行;
                    Thread.Sleep(delayMs);
                }
            }
            catch (Exception ex)
            {
                OnStatusArgs.OnEvented(new CMonConArgs(idNo,"["+ name + "]监控线程异常错误:" + ex.ToString(), true));
            }
            finally
            {
                mDispose = false;
                OnStatusArgs.OnEvented(new CMonConArgs(idNo, "[" + name + "]监控线程销毁退出", true));
            }
        }
        /// <summary>
        /// 读控制板信号
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool readData(ref string er)
        {
            try
            {
                bool readOK = true;
                er = string.Empty;
                for (int i = 0; i < mon.Count; i++)
                {
                    if (mDispose)
                        return false;
                    if (mon[i].rData.bData == EOP.禁用)
                        continue;
                    string errcode = string.Empty;
                    //读信号
                    monLock.AcquireReaderLock(-1);
                    if (!devMon.ReadRunData(mon[i].wBase.addrId, ref mon[i].rData.signal, ref errcode,runMode))
                    {
                        CMath.delayMs(C_MON_DELAY);
                        if (!devMon.ReadRunData(mon[i].wBase.addrId, ref mon[i].rData.signal, ref errcode, runMode))
                        {
                            readOK = false;
                            mon[i].rData.conOK = false;
                            er += "[" + mon[i].wBase.addrId.ToString("D2") + "]信号;";
                            monLock.ReleaseReaderLock();
                            continue;
                        }
                    }
                    monLock.ReleaseReaderLock();
                    CMath.delayMs(C_MON_DELAY);   
                }
                for (int i = 0; i < mon.Count; i++)
                {
                    if (mDispose)
                        return false;
                    if (mon[i].rData.bData == EOP.禁用)
                        continue;                   
                    string errcode = string.Empty;                  
                    //读电压值
                    monLock.AcquireReaderLock(-1);                    
                    if (!devMon.ReadVolt(mon[i].wBase.addrId, ref mon[i].rData.data, ref errcode,ESynON.同步,runMode))
                    {
                        CMath.delayMs(C_MON_DELAY);
                        if (!devMon.ReadVolt(mon[i].wBase.addrId, ref mon[i].rData.data, ref errcode, ESynON.同步, runMode))
                        {                            
                            readOK = false;
                            mon[i].rData.conOK = false;
                            er += "[" + mon[i].wBase.addrId.ToString("D2") + "]电压;";
                            monLock.ReleaseReaderLock();
                            continue;
                        }                        
                    }
                    mon[i].rData.bData = EOP.执行;
                    monLock.ReleaseReaderLock();
                    CMath.delayMs(C_MON_DELAY);

                    //读ONOFF参数
                    if (mon[i].rData.bOnOff == EOP.执行)
                    {
                        monLock.AcquireReaderLock(-1);                        
                        if (!devMon.ReadOnOffPara(mon[i].wBase.addrId, ref mon[i].rData.onoff, ref errcode))
                        {
                            CMath.delayMs(C_MON_DELAY);
                            if (!devMon.ReadOnOffPara(mon[i].wBase.addrId, ref mon[i].rData.onoff, ref errcode))
                            {
                                readOK = false;
                                mon[i].rData.conOK = false;
                                er += "[" + mon[i].wBase.addrId.ToString("D2") + "]ONOFF;";
                                monLock.ReleaseReaderLock();
                                continue;
                            }                            
                        }
                        mon[i].rData.bOnOff = EOP.完成;
                        monLock.ReleaseReaderLock();
                        CMath.delayMs(C_MON_DELAY);
                    }
                    mon[i].rData.conOK = true;               
                }
                return readOK;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 写控制板信号
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool writeData(ref string er)
        {
            try
            {
                bool writeOK = true;
                er = string.Empty;
                for (int i = 0; i < mon.Count; i++)
                {
                    if (mDispose)
                        return false;
                    if (mon[i].rData.bData == EOP.禁用)
                        continue;                    
                    string errcode = string.Empty;
                    //广播命令
                    if (bRunAllMode == EOP.执行)
                    {
                        monLock.AcquireWriterLock(-1);                                      
                        if (!devMon.SetWorkMode(0, runAllMode, ref errcode))
                        {
                            CMath.delayMs(C_MON_DELAY);
                            if (!devMon.SetWorkMode(0, runAllMode, ref errcode))
                            {
                                writeOK = false;
                                er += "广播运行模式;";
                                monLock.ReleaseWriterLock();
                                continue;
                            }                            
                        }
                        bRunAllMode = EOP.完成;                        
                        monLock.ReleaseWriterLock();
                        CMath.delayMs(3000); 
                    }
                    //设置ONOFF
                    if (mon[i].wData.bOnOff == EOP.执行)
                    {
                        monLock.AcquireWriterLock(-1);                       
                        if (!devMon.SetOnOffPara(mon[i].wBase.addrId, mon[i].wData.onoff, ref errcode))
                        {
                            CMath.delayMs(C_MON_DELAY);
                            if (!devMon.SetOnOffPara(mon[i].wBase.addrId, mon[i].wData.onoff, ref errcode))
                            {
                                writeOK = false;
                                er += "[" + mon[i].wBase.addrId.ToString("D2") + "]ONOFF;";
                                monLock.ReleaseWriterLock();
                                continue;
                            }
                        }
                        mon[i].wData.bOnOff = EOP.完成;
                        monLock.ReleaseWriterLock();
                        CMath.delayMs(300);
                    }
                    //设置快充模式及电压
                    if (mon[i].wData.bQCVolt == EOP.执行)
                    {
                        monLock.AcquireWriterLock(-1);
                        if (!devMon.SetGJM_RunQC_Para(mon[i].wBase.addrId, mon[i].wData.qcVolt, ref errcode))
                        {
                            CMath.delayMs(C_MON_DELAY);
                            if (!devMon.SetGJM_RunQC_Para(mon[i].wBase.addrId, mon[i].wData.qcVolt, ref errcode))
                            {
                                writeOK = false;
                                er += "[" + mon[i].wBase.addrId.ToString("D2") + "]QC;";
                                monLock.ReleaseWriterLock();
                                continue;
                            }
                        }
                        mon[i].wData.bQCVolt = EOP.完成;
                        monLock.ReleaseWriterLock();
                        CMath.delayMs(300);
                    }
                    //启动老化
                    if (mon[i].wData.bRunPara == EOP.执行)
                    {
                        monLock.AcquireWriterLock(-1);
                        if (!devMon.SetRunStart(mon[i].wBase.addrId, mon[i].wData.runPara, ref errcode))
                        { 
                            CMath.delayMs(C_MON_DELAY);
                            if (!devMon.SetRunStart(mon[i].wBase.addrId, mon[i].wData.runPara, ref errcode))
                            {
                                writeOK = false;
                                er += "[" + mon[i].wBase.addrId.ToString("D2") + "]启动;";
                                monLock.ReleaseWriterLock();
                                continue;
                            }
                        }
                        mon[i].wData.bRunPara = EOP.完成;
                        monLock.ReleaseWriterLock();
                        CMath.delayMs(100);
                    }
                    //停止老化
                    if (mon[i].wData.bForceFinish == EOP.执行)
                    {
                        monLock.AcquireWriterLock(-1);                        
                        if (!devMon.ForceFinish(mon[i].wBase.addrId, ref errcode))
                        {
                             CMath.delayMs(C_MON_DELAY);
                             if (!devMon.ForceFinish(mon[i].wBase.addrId, ref errcode))
                             {
                                 writeOK = false;
                                 er += "[" + mon[i].wBase.addrId.ToString("D2") + "]停止;";
                                 monLock.ReleaseWriterLock();
                                 continue;
                             }
                        }
                        mon[i].wData.bForceFinish = EOP.完成;
                        monLock.ReleaseWriterLock();
                        CMath.delayMs(100);
                    }
                    //暂停或继续
                    if (mon[i].wData.bContinun == EOP.执行)
                    {
                        monLock.AcquireWriterLock(-1);
                        if (!devMon.ControlPauseOrContinue(mon[i].wBase.addrId,mon[i].wData.continunRun, ref errcode))
                        {
                            CMath.delayMs(C_MON_DELAY);
                            if (!devMon.ControlPauseOrContinue(mon[i].wBase.addrId, mon[i].wData.continunRun, ref errcode))
                            {
                                writeOK = false;
                                if (mon[i].wData.continunRun==0)
                                    er += "[" + mon[i].wBase.addrId.ToString("D2") + "]暂停;";
                                else
                                    er += "[" + mon[i].wBase.addrId.ToString("D2") + "]继续;";
                                monLock.ReleaseWriterLock();
                                continue;
                            }
                        }
                        mon[i].wData.bContinun = EOP.完成;
                        monLock.ReleaseWriterLock();
                        CMath.delayMs(100);
                    }
                    //设置模式
                    if (mon[i].wData.bRunMode == EOP.执行)
                    {
                        monLock.AcquireWriterLock(-1);                        
                        if (!devMon.SetWorkMode(mon[i].wBase.addrId, mon[i].wData.runMode, ref errcode))
                        {
                            CMath.delayMs(C_MON_DELAY);
                            if (!devMon.SetWorkMode(mon[i].wBase.addrId, mon[i].wData.runMode, ref errcode))
                            {
                                writeOK = false;
                                er += "[" + mon[i].wBase.addrId.ToString("D2") + "]模式;";
                                monLock.ReleaseWriterLock();
                                continue;
                            }                            
                        }
                        mon[i].wData.bRunMode = EOP.完成;
                        monLock.ReleaseWriterLock();
                        CMath.delayMs(100);
                    }
                }
                return writeOK;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 控制板

        #region 内部类
        /// <summary>
        /// 基本信息
        /// </summary>
        public class CBase
        {
            public int addrId;
            public string ver;
        }
        /// <summary>
        /// 读取信息
        /// </summary>
        public class CRData
        {
            /// <summary>
            /// 通信状态
            /// </summary>
            public bool conOK = false;
            /// <summary>
            /// 信号值标志
            /// </summary>
            public EOP bData = EOP.空闲;
            /// <summary>
            /// 信号值
            /// </summary>
            public CReadRunPara signal = new CReadRunPara();
            /// <summary>
            /// 电压值
            /// </summary>
            public CVoltVal data = new CVoltVal();
            /// <summary>
            /// OnOff标志
            /// </summary>
            public EOP bOnOff = EOP.空闲;
            /// <summary>
            /// onoff值
            /// </summary>
            public COnOffPara onoff = new COnOffPara(); 
        }
        /// <summary>
        /// 写操作
        /// </summary>
        public class CWData
        {
            /// <summary>
            /// 启动运行标志
            /// </summary>
            public EOP bRunPara = EOP.空闲;
            /// <summary>
            /// 启动运行
            /// </summary>
            public CWriteRunPara runPara = new CWriteRunPara();
            /// <summary>
            /// OnOff标志
            /// </summary>
            public EOP bOnOff = EOP.空闲;
            /// <summary>
            /// onoff参数
            /// </summary>
            public COnOffPara onoff = new COnOffPara();
            /// <summary>
            /// 工作模式标志
            /// </summary>
            public EOP bRunMode = EOP.空闲;
            /// <summary>
            /// 工作模式
            /// </summary>
            public int runMode = 0;
            /// <summary>
            /// 强制结束
            /// </summary>
            public EOP bForceFinish = EOP.空闲;
            /// <summary>
            /// 设置快充模式及电压
            /// </summary>
            public EOP bQCVolt = EOP.空闲;
            public COnOffPara qcVolt = new COnOffPara();
            /// <summary>
            /// 控制暂停运行或继续运行
            /// </summary>
            public EOP bContinun = EOP.空闲;
            public int continunRun = 0;
        }
        /// <summary>
        /// 控制板单元
        /// </summary>
        public class CUUT
        {
            public CBase wBase = new CBase();
            public CRData rData = new CRData();
            public CWData wData = new CWData();
        }

        /// <summary>
        /// 广播命令设置运行模式
        /// </summary>
        private int runAllMode = 0;
        /// <summary>
        /// 广播命令设置运行模式
        /// </summary>
        private EOP bRunAllMode = EOP.空闲;  

        #endregion

        #region 字段
        public List<CUUT> mon = new List<CUUT>();
        #endregion

        #region 方法
        /// <summary>
        /// 禁用控制板
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setForbit(int wAddr,ref string er,bool bForBit=false)
        {
            try
            {
                er = string.Empty;
                monLock.AcquireWriterLock(-1);           
                if (wAddr < 1 || wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }                 
                if (bForBit)
                    mon[wAddr - 1].rData.bData = EOP.禁用;
                else
                    mon[wAddr - 1].rData.bData = EOP.执行; 
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                monLock.ReleaseWriterLock(); 
            }
        }
        /// <summary>
        /// 暂停和继续控制板
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setContinue(int wAddr, ref string er, int wContinue)
        {
            try
            {
                er = string.Empty;
                monLock.AcquireWriterLock(-1);
                if (wAddr < 1 || wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }
                mon[wAddr - 1].wData.continunRun = wContinue; 
                mon[wAddr - 1].wData.bContinun = EOP.执行;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                monLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 设置ONOFF参数
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="onoffPara"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setOnOff(int wAddr, COnOffPara onoffPara, ref string er)
        {
            try
            {
                er = string.Empty;
                monLock.AcquireWriterLock(-1);
                if (wAddr < 1 || wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }
                mon[wAddr - 1].wData.onoff = onoffPara;
                mon[wAddr - 1].wData.bOnOff = EOP.执行;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                monLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 启动老化
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="runPara"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setRun(int wAddr, CWriteRunPara runPara, ref string er)
        {
            try
            {
                er = string.Empty;
                monLock.AcquireWriterLock(-1);
                if (wAddr < 1 || wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }
                mon[wAddr - 1].wData.runPara = runPara;
                mon[wAddr - 1].wData.bRunPara = EOP.执行;   
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                monLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 停止老化
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setStop(int wAddr, ref string er)
        {
            try
            {
                er = string.Empty;
                monLock.AcquireWriterLock(-1);
                if (wAddr < 1 || wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }
                mon[wAddr - 1].wData.bForceFinish = EOP.执行;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                monLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 设置运行模式
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="runMode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setRunMode(int wAddr, int runMode, ref string er)
        {
            try
            {
                er = string.Empty;
                monLock.AcquireWriterLock(-1);
                if (wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }
                if (wAddr == 0)
                {
                    runAllMode = runMode;
                    bRunAllMode = EOP.执行; 
                }
                else
                {
                    mon[wAddr - 1].wData.runMode = runMode;
                    mon[wAddr - 1].wData.bRunMode = EOP.执行;
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
                monLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 启动读ON/OFF时序
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool startReadOnOff(int wAddr,ref string er)
        {
            try
            {
                er = string.Empty;
                monLock.AcquireWriterLock(-1);
                if (wAddr < 1 || wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }
                mon[wAddr - 1].rData.bOnOff = EOP.执行;   
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                monLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 设置快充运行模式
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="QCPara"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setQCRunPara(int wAddr, COnOffPara QCPara, ref string er)
        {
            try
            {
                er = string.Empty;
                monLock.AcquireWriterLock(-1);
                if (wAddr < 1 || wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }
                mon[wAddr - 1].wData.qcVolt = QCPara;
                mon[wAddr - 1].wData.bQCVolt = EOP.执行;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                monLock.ReleaseWriterLock();
            }
        }
        #endregion

        #endregion

    }
}
