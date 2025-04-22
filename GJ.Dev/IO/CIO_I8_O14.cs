using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Dev;  
namespace GJ.Dev.IO
{
    public class CIO_I8_O14:IIO
    {

        #region 字段
        private string name = "GJIO_I8_O14";
        private int idNo = 0;
        private COM.ICom com;
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
        public COM.ICom mCom
        {
            set { com = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comName"></param>
        /// <param name="setting"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool open(string comName, ref string er, string setting="9600,n,8,1")
        {
            if (!com.open(comName, ref er, setting))
                return false;
            com.mComDataType = 0;
            return true;
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public void close()
        {
            com.close();
        }
        /// <summary>
        /// 读取X1-X8
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="X"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ReadInSingal(int wAddr, ref List<EX> X, ref string er)
        {
            try
            {
                string wCmd = string.Empty;
                string rData = string.Empty;
                string rVal = string.Empty;

                wCmd = wAddr.ToString("X2") + "00" + "05" + "04";
                wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
                int rLen = 8;
                if (!com.send(wCmd, rLen, ref rData, ref er))
                    return false;
                if (!ToCheckSum(rData, ref rVal))
                {
                    er = "检验和错误:" + rData;
                    return false;
                }
                int inputSgn =System.Convert.ToInt32(rVal,16);
                
                for (int i = 0; i < 8; i++)
			    {
			       if((inputSgn & (1<<i))==(1<<i))
                       X.Add(EX.XOFF);
                   else
                       X.Add(EX.XON); 
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
        /// 控制K1--K14只能唯一ON iRly=0时 All Relay is OFF
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="relayNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool CtrlRelayByCmd1(int wAddr, int relayNo, ref string er)
        {
            try
            {
                string wCmd = string.Empty;
                string rData = string.Empty;
                string rVal = string.Empty;
                wCmd = wAddr.ToString("X2") + "00" + "01" + "05"+relayNo.ToString("X2");
                wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
                int rLen = 0;
                if (!com.send(wCmd, rLen, ref rData, ref er))
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
        /// 任意控制K1--K14 ON/OFF
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="relayNo"></param>
        /// <param name="onOff"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool CtrlRelayByCmd2(int wAddr, int relayNo, EY onOff, ref string er)
        {
            try
            {
                string wCmd = string.Empty;
                string rData = string.Empty;
                string rVal = string.Empty;
                wCmd = wAddr.ToString("X2") + "00" + "04" + "06" + relayNo.ToString("X2")+ ((int)onOff).ToString("X2");
                wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
                int rLen = 0;
                if (!com.send(wCmd, rLen, ref rData, ref er))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 通信协议
        private const string SOI = "9C";
        private const string EOI = "9D";
        private const string ROI = "9B";
        /// <summary>
        /// 计算检验和
        /// </summary>
        /// <param name="wCmd"></param>
        /// <returns></returns>
        private string CalCheckSum(string wCmd)
        {
            int sum = 0;
            for (int i = 0; i < wCmd.Length / 2; i++)
                sum += System.Convert.ToInt16(wCmd.Substring(i * 2, 2), 16);
            sum = sum % 0x100;
            string chkSum = sum.ToString("X2");
            if (chkSum == SOI || chkSum == EOI)
                chkSum = ROI;
            return chkSum;
        }
        /// <summary>
        /// 检查检验和
        /// </summary>
        /// <param name="wCmd"></param>
        /// <param name="rVal">有效数据</param>
        /// <returns></returns>
        private bool ToCheckSum(string wCmd, ref string rVal)
        {
            int s1 = wCmd.IndexOf(SOI);
            int s2 = wCmd.LastIndexOf(EOI);
            if (s2 == 0 || s1 >= s2 || s2 - s1 < 12)
                return false;
            wCmd = wCmd.Substring(s1, s2 - s1 + 2);
            int sum = 0;
            for (int i = 1; i <= (wCmd.Length - 6) / 2; i++)
            {
                sum += System.Convert.ToInt16(wCmd.Substring(i * 2, 2), 16);
            }
            sum = sum % 256;
            string calSum = sum.ToString("X2");
            if (calSum == SOI || calSum == EOI)
                calSum = ROI;
            string getSum = wCmd.Substring(wCmd.Length - 4, 2);
            if (calSum != getSum)
                return false;
            rVal = wCmd.Substring(10, wCmd.Length - 14);
            return true;
        }
        /// <summary>
        /// 检查数据是否为桢头和桢尾
        /// </summary>
        /// <param name="wCmd"></param>
        /// <returns></returns>
        private string chkSOIEOI(string wCmd)
        {
            string rCmd = string.Empty;
            int len = wCmd.Length / 2;
            for (int i = 0; i < len; i++)
            {
                string temp = wCmd.Substring(i * 2, 2);
                if (temp == SOI || temp == EOI)
                    rCmd += ROI;
                else
                    rCmd += temp;
            }
            return rCmd;
        }
        #endregion
    }
}
