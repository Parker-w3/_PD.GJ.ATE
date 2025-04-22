using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Dev.COM;
namespace GJ.Dev.Card
{
    /// <summary>
    /// ID Card公共类
    /// </summary>
    public class CCardCom
    {
        #region 构造函数
        public CCardCom()
        {
            idCard = new CMFID();
            idCard.mCom = new CSerialPort();  
         }
        #endregion
        
       
        #region 字节
        private ICARD idCard = null;
        #endregion

        #region 属性
        public string mName
        {
            get {
                 if (idCard == null)
                    return null;
                 return idCard.mName; 
                }
            set { 
                if(idCard!=null)
                   idCard.mName = value; 
                }
        }
        public int mIdNo
        {
            get {
                if (idCard == null)
                    return 0;
                 return idCard.mIdNo;
                 }
            set {
                if (idCard != null)
                    idCard.mIdNo = value;
                 }
        }
        public string mVersion
        {
            get 
            {
                if (idCard == null)
                    return string.Empty;
                return idCard.mVersion ;
            }
            set {
                if (idCard != null)
                    idCard.mVersion = value;
            }
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
        public bool open(string comName, ref string er, string setting="19200,E,8,1")
        {
            return idCard.open(comName, ref er, setting);  
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public void close()
        {
            idCard.close(); 
        }
        /// <summary>
        /// 读取对应地址的卡号
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecorderSn(int idAddr, ref string rSn, ref string er)
        { 
         return idCard.GetRecorderSn(idAddr,ref rSn,ref er);  
        }
        /// <summary>
        /// 设置卡号的地址
        /// </summary>
        /// <param name="strSn"></param>
        /// <param name="idAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetRecorderID(string strSn, int idAddr, ref string er)
        {
            return idCard.SetRecorderID(strSn, idAddr, ref er);  
        }
        /// <summary>
        /// 读取卡号的地址
        /// </summary>
        /// <param name="strSn"></param>
        /// <param name="idAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecorderID(string strSn, ref int idAddr, ref string er)
        {
            return idCard.GetRecorderID(strSn, ref idAddr, ref er);  
        }
        /// <summary>
        /// 读取地址编号卡号资料
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecord(int idAddr, ref string rIDCard, ref string er)
        {
            return idCard.GetRecord(idAddr, ref rIDCard, ref er);  
        }
        /// <summary>
        /// 读取地址编号卡号资料
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecordAgain(int idAddr, ref string rIDCard, ref string er)
        {
            return idCard.GetRecordAgain(idAddr, ref rIDCard, ref er);   
        }
        /// <summary>
        /// 读取触发信号状态和卡片资料(有卡片感应)-触发信号为1
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecordTriggerSignal(int idAddr, ref string rIDCard, ref bool rTrigger,ref string er)
        {
            return idCard.GetRecordTriggerSignal(idAddr, ref rIDCard,ref rTrigger, ref er);  
        }
        /// <summary>
        /// 设置读卡器工作模式(广播)
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetRecorderWorkMode(EMode mode, ref string er)
        {
            return idCard.SetRecorderWorkMode(mode, ref er);  
        }
        /// <summary>
        /// 设置读卡器工作模式
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetRecorderWorkMode(int idAddr, EMode mode, ref string er)
        {
            return idCard.SetRecorderWorkMode(idAddr, mode, ref er);    
        }
        /// <summary>
        /// 读取读卡器工作模式
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetRecordMode(int idAddr, ref EMode mode, ref string er)
        {
            return idCard.GetRecordMode(idAddr, ref mode, ref er);  
        }
        /// <summary>
        /// 读取读卡器触发信号状态
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="Trigger"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetTriggerSignal(int idAddr, ref bool Trigger, ref string er)
        {
            return idCard.GetTriggerSignal(idAddr, ref Trigger, ref er);    
        }
        #endregion
    }
}
