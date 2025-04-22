using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GJ.Dev.ERS
{
    #region 事件定义
    public class CERSConArgs : EventArgs
    {
        public readonly string conStatus;
        public readonly bool bErr;
        public CERSConArgs(string conStatus, bool bErr = false)
        {
            this.conStatus = conStatus;
            this.bErr = bErr;
        }
    }
    public class CERSDataArgs : EventArgs
    {
        public readonly string rData;
        public readonly bool bErr;
        public readonly bool bComplete;
        public CERSDataArgs(string rData, bool bComplete = true, bool bErr = false)
        {
            this.rData = rData;
            this.bComplete = bComplete;
            this.bErr = bErr;
        }
    }
    #endregion

    public class CERSPara
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
            禁用 = -1,
            空闲 = 0,
            执行 = 1,
            完成 = 2
        }

        #region 构造函数
        public CERSPara(int idNo, string name, int maxAddr = 20, bool autoMode = true)
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
        public COnEvent<CERSConArgs> OnStatusArgs = new COnEvent<CERSConArgs>();
        /// <summary>
        /// 数据事件
        /// </summary>
        public COnEvent<CERSDataArgs> OnDataArgs = new COnEvent<CERSDataArgs>();
        /// <summary>
        /// 最大地址
        /// </summary>
        public int maxAddr = 40;
        /// <summary>
        /// 负载通道
        /// </summary>
        private const int C_MAX_CH = 4;
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
        private const int C_MON_DELAY = 50;
        private CERSCom devERS = null;
        private Thread threadMon = null;
        private ReaderWriterLock monLock = new ReaderWriterLock();
        /// <summary>
        /// 启动线程
        /// </summary>
        /// <param name="plc"></param>
        public void spinUp(CERSCom devMon)
        {
            this.devERS = devMon;
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
                OnStatusArgs.OnEvented(new CERSConArgs("创建" + threadMon.Name + "监控线程"));
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
                        OnDataArgs.OnEvented(new CERSDataArgs(er, false, true));
                    if (!readData(ref er))
                        OnDataArgs.OnEvented(new CERSDataArgs(er, false, true));
                    if (!autoMode)
                    {
                        mPaused = true;
                        status = EStatus.暂停;
                        OnDataArgs.OnEvented(new CERSDataArgs("扫描时间=" + (System.Environment.TickCount - scanTime).ToString() + "ms"));
                    }
                    else
                        status = EStatus.运行;
                    Thread.Sleep(delayMs);
                }
            }
            catch (Exception ex)
            {
                OnStatusArgs.OnEvented(new CERSConArgs(threadMon.Name + "监控线程异常错误:" + ex.ToString(), true));
            }
            finally
            {
                mDispose = false;
                OnStatusArgs.OnEvented(new CERSConArgs(threadMon.Name + "监控线程销毁退出", true));
            }
        }
        /// <summary>
        /// 读ERS信号
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
                    //读电压值
                    if (mon[i].rData.bLoad == EOP.执行)
                    {
                        CMath.delayMs(C_MON_DELAY);
                        monLock.AcquireReaderLock(-1);
                        if (!devERS.ReadLoadSet(mon[i].wBase.addrId, mon[i].rData.load, ref er))
                        {
                            readOK = false;
                            mon[i].rData.conOK = false;  
                            er += "[" + mon[i].wBase.addrId.ToString("D2") + "]负载;";
                            monLock.ReleaseReaderLock();
                            continue;
                        }
                        mon[i].rData.bLoad = EOP.完成;
                        monLock.ReleaseReaderLock();
                    }
                    CMath.delayMs(C_MON_DELAY);
                    if (!devERS.ReadData(mon[i].wBase.addrId,mon[i].rData.data,ref er))
                    {
                        CMath.delayMs(C_MON_DELAY);
                         monLock.AcquireReaderLock(-1);
                         if (!devERS.ReadData(mon[i].wBase.addrId, mon[i].rData.data, ref er))
                         {
                             readOK = false;
                             mon[i].rData.conOK = false;
                             er += "[" + mon[i].wBase.addrId.ToString("D2") + "]数据;";
                             monLock.ReleaseReaderLock();
                             continue;
                         }
                         monLock.ReleaseReaderLock();
                    }
                    mon[i].rData.conOK = true;  
                    mon[i].rData.bData = EOP.执行;                   
                    
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
        /// 写ERS信号
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
                    
                    for (int CH = 0; CH < C_MAX_CH; CH++)
                    {
                        //设置通道负载
                        if (mon[i].wData.bLoad[CH] == EOP.执行)
                        {                            
                            monLock.AcquireWriterLock(-1);
                            if (!devERS.SetNewLoad(mon[i].wBase.addrId,CH,mon[i].wData.loadVal[CH], ref errcode))
                            {
                                CMath.delayMs(C_MON_DELAY); 
                                if (!devERS.SetNewLoad(mon[i].wBase.addrId, CH, mon[i].wData.loadVal[CH], ref errcode))
                                {
                                    monLock.ReleaseWriterLock();
                                    writeOK = false;
                                    er += "[" + mon[i].wBase.addrId.ToString("D2") + "_" + (CH + 1).ToString() + "]设载;";
                                    continue;
                                }                                
                            }
                            mon[i].wData.bLoad[CH] = EOP.完成;
                            monLock.ReleaseWriterLock();
                            CMath.delayMs(300);
                        }
                        //设置MTK快充电压上升或下降
                        if (mon[i].wData.bMTK[CH] == EOP.执行)
                        {
                            monLock.AcquireWriterLock(-1);
                            if (!devERS.SetQCMTK(mon[i].wBase.addrId, CH, mon[i].wData.mtkRaise[CH], ref errcode))
                            {
                                CMath.delayMs(C_MON_DELAY);
                                if (!devERS.SetQCMTK(mon[i].wBase.addrId, CH, mon[i].wData.mtkRaise[CH], ref errcode))
                                {
                                    monLock.ReleaseWriterLock();
                                    writeOK = false;
                                    if (mon[i].wData.mtkRaise[CH])
                                        er += "[" + mon[i].wBase.addrId.ToString("D2") + "_" + (CH + 1).ToString() + "]MTK_ON;";
                                    else
                                        er += "[" + mon[i].wBase.addrId.ToString("D2") + "_" + (CH + 1).ToString() + "]MTK_OFF;";
                                    continue;
                                }
                            }
                            mon[i].wData.bMTK[CH] = EOP.完成;
                            monLock.ReleaseWriterLock();
                            CMath.delayMs(300);
                        }
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

        #region ERS负载

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
            public bool conOK = true;
            /// <summary>
            /// 读标志
            /// </summary>
            public EOP bData = EOP.空闲; 
            /// <summary>
            /// 读电压电流
            /// </summary>
            public CERS_ReadData data = new CERS_ReadData();
            /// <summary>
            /// 读标志
            /// </summary>
            public EOP bLoad = EOP.空闲;
            /// <summary>
            /// 读负载设定值
            /// </summary>
            public CERS_ReadLoadSet load = new CERS_ReadLoadSet();
        }
        /// <summary>
        /// 写操作
        /// </summary>
        public class CWData
        {
            public EOP[] bLoad = new EOP[C_MAX_CH];
            public double[] loadVal = new double[C_MAX_CH];

            public EOP[] bMTK = new EOP[C_MAX_CH];
            public bool[] mtkRaise = new bool[C_MAX_CH]; 
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
        #endregion

        #region 字段
        public List<CUUT> mon = new List<CUUT>();
        #endregion

        #region 方法
        /// <summary>
        /// 设置通道负载
        /// </summary>
        /// <param name="wAddr">1-40</param>
        /// <param name="CH">1-4</param>
        /// <param name="loadVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setCHLoad(int wAddr, int CH, double loadVal, ref string er)
        {
            try
            {
                er = string.Empty;
                if (wAddr < 1 || wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }
                if (CH < 1 || CH > C_MAX_CH)
                {
                    er = "负载通道不在范围内:1-" + C_MAX_CH.ToString();
                    return false;
                }
                monLock.AcquireWriterLock(-1);
                mon[wAddr - 1].wData.loadVal[CH - 1] = loadVal;
                mon[wAddr - 1].wData.bLoad[CH - 1] = EOP.执行;
                monLock.ReleaseWriterLock();
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置快充电压上升或下降
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="CH"></param>
        /// <param name="wRaise"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setMTKRaise(int wAddr, int CH, bool wRaise, ref string er)
        {
            try
            {
                er = string.Empty;
                if (wAddr < 1 || wAddr > maxAddr)
                {
                    er = "地址不在范围内:1-" + maxAddr.ToString();
                    return false;
                }
                if (CH < 1 || CH > C_MAX_CH)
                {
                    er = "负载通道不在范围内:1-" + C_MAX_CH.ToString();
                    return false;
                }
                monLock.AcquireWriterLock(-1);
                mon[wAddr - 1].wData.mtkRaise[CH - 1] = wRaise;
                mon[wAddr - 1].wData.bMTK[CH - 1] = EOP.执行;
                monLock.ReleaseWriterLock();
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
    }

}
