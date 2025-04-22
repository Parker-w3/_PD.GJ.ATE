using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.IO
{
    public interface IIO
    {
        #region 属性
        string mName
        { set; get; }
        int mIdNo
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
        /// 读取X1-X8
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="X"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool ReadInSingal(int wAddr, ref List<EX> X, ref string er);
        /// <summary>
        /// 控制K1--K14只能唯一ON iRly=0时 All Relay is OFF
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="relayNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool CtrlRelayByCmd1(int wAddr,int relayNo,ref string er);
        /// <summary>
        /// 任意控制K1--K14 ON/OFF
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="relayNo"></param>
        /// <param name="onOff"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool CtrlRelayByCmd2(int wAddr, int relayNo, EY onOff, ref string er);
        #endregion
    }
}
