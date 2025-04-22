using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.Card
{
    public interface ICARD
    {
        #region 属性
        string mName
        { set; get; }
        int mIdNo
        { set; get; }
        string mVersion
        { set; get; }
        COM.ICom mCom
        { set; }
        #endregion

        #region 方法
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comName"></param>
        /// <param name="setting"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool open(string comName, ref string er, string setting);
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        void close();
        /// <summary>
        /// 读取对应地址的卡号
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecorderSn(int idAddr, ref string rSn, ref string er);
        /// <summary>
        /// 设置卡号的地址
        /// </summary>
        /// <param name="strSn"></param>
        /// <param name="idAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool SetRecorderID(string strSn, int idAddr, ref string er);
        /// <summary>
        /// 读取卡号的地址
        /// </summary>
        /// <param name="strSn"></param>
        /// <param name="idAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecorderID(string strSn, ref int idAddr, ref string er);
        /// <summary>
        /// 读取地址编号卡号资料
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecord(int idAddr, ref string rSn, ref string er);
        /// <summary>
        /// 读取地址编号卡号资料
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecordAgain(int idAddr, ref string rSn, ref string er);
        /// <summary>
        /// 读取触发信号状态和卡片资料(有卡片感应)-触发信号为1
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="rSn"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecordTriggerSignal(int idAddr, ref string rSn, ref bool rTrigger,ref string er);
        /// <summary>
        /// 设置读卡器工作模式(广播)
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool SetRecorderWorkMode(EMode mode, ref string er);
        /// <summary>
        /// 设置读卡器工作模式
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool SetRecorderWorkMode(int idAddr, EMode mode, ref string er);
        /// <summary>
        /// 读取读卡器工作模式
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="mode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetRecordMode(int idAddr, ref EMode mode, ref string er);
        /// <summary>
        /// 读取读卡器触发信号状态
        /// </summary>
        /// <param name="idAddr"></param>
        /// <param name="Trigger"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool GetTriggerSignal(int idAddr, ref bool Trigger, ref string er);

        #endregion
    }
}
