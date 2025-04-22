using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GJ.Dev.RemIO;
 
namespace GJ.Dev.HIPOT
{
    public class CChromaPLC
    {
        #region 字段
        private string name = "Chroma-PLC";
        private int idNo = 0;
        private IO_24_16 com = new IO_24_16();
        private static ReaderWriterLock ioInLock = new ReaderWriterLock();
        #endregion

        #region 属性
        public string mName
        {
            get { return name; }
            set { name = value; }
        }
        public int mIdNo
        {
            get { return idNo; }
            set { idNo = value; }
        }      
        #endregion

        #region 方法
        public bool open(string comName, ref string er, string setting = "115200,n,8,1")
        {
            if (com == null)
                return false;
            return com.open(comName, ref er, setting);
        }
        public void close()
        {
            com.close();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="devNo">0-1</param>
        /// <param name="chanNo">0-7</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool init(int devNo,int chanNo,ref string er)
        {
            try
            {
                ioInLock.AcquireWriterLock(-1);

                int devAddr = devNo + 1;

                //切换通道

                int startAddr = 1;

                int[] wBit=new int[8];

                for (int i = 0; i < wBit.Length; i++)
                {
                    if (chanNo == i)
                        wBit[i] = 1;
                }

                bool ret = com.write(devAddr, ECoilType.Y, startAddr, wBit, ref er);

                Thread.Sleep(50);

                return ret;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                ioInLock.ReleaseWriterLock(); 
            }
        }
        /// <summary>
        /// 启动Y9;停止Y10
        /// </summary>
        /// <param name="devNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool start(int devNo, ref string er)
        {
            try
            {
                ioInLock.AcquireWriterLock(-1);

                int devAddr = devNo + 1;

                int startAddr = 9;

                int[] wBit = new int[] { 1, 0 };

                if (!com.write(devAddr, ECoilType.Y, startAddr, wBit, ref er))
                    return false;

                Thread.Sleep(50);

                return true;

            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                ioInLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="devNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool stop(int devNo, ref string er)
        {
            try
            {
                ioInLock.AcquireWriterLock(-1);

                int devAddr = devNo + 1;

                int startAddr = 9;

                int[] wBit = new int[] { 0, 1 };

                if (!com.write(devAddr, ECoilType.Y, startAddr, wBit, ref er))
                    return false;

                Thread.Sleep(50);

                return true;

            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                ioInLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 清除启动和停止信号
        /// </summary>
        /// <param name="devNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool clrRunStop(int devNo, ref string er)
        {
            try
            {
                ioInLock.AcquireWriterLock(-1);

                int devAddr = devNo + 1;

                int startAddr = 9;

                int[] wBit = new int[] { 0, 0 };

                if (!com.write(devAddr, ECoilType.Y, startAddr, wBit, ref er))
                    return false;

                Thread.Sleep(10);

                return true;

            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                ioInLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// 检测IO板状态 
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool checkIn(ref string er)
        {
            try
            {

                ioInLock.AcquireReaderLock(-1); 

                er = string.Empty;

                int rVal = 0;

                if (!com.read(1, ECoilType.X, 0, ref rVal, ref er))
                {
                    er = "高压IO板【地址1】通信异常";

                    return false;
                }
                Thread.Sleep(50);
                if (!com.read(2, ECoilType.X, 0, ref rVal, ref er))
                {
                    er = "高压IO板【地址2】通信异常";

                    return false;
                }
                Thread.Sleep(50);
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                ioInLock.ReleaseReaderLock(); 
            }
        }
        /// <summary>
        /// 读取测试状态:X18->FAIL;X19->PASS;X20->TESTING
        /// </summary>
        /// <param name="devNo"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public bool readStatus(int devNo,ref int testing,ref int testEnd, ref int result,ref List<int> outPos,ref string er)
        {
            try
            {
                ioInLock.AcquireReaderLock(-1); 

                int devAddr = devNo + 1;

                int startAddr = 0;

                int[] rVal=new int[24];

                outPos.Clear();
                for (int i = 0; i < 16; i++)
                    outPos.Add(-1); 
                
                if(!com.read(devAddr,ECoilType.X,startAddr,ref rVal,ref er))
                    return false;
                
                Thread.Sleep(20);

                for (int i = 0; i < outPos.Count; i++)
                    outPos[i] = rVal[i + 1];

                testEnd = 0;

                testing = 0;

                if (rVal[20] == 1)   
                    testing = 1;

                if (rVal[18] == 1 && rVal[19] == 0)
                {
                   testEnd=1;
                   result = 1;
                }
                else if (rVal[18] == 0 && rVal[19] == 1)
                {
                    testEnd = 1;
                    result = 0;
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
                ioInLock.ReleaseReaderLock();
            }
        }
        #endregion

    }
}
