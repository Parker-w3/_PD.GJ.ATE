using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.RemIO
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
        /// 读IO
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="N"></param>
        /// <param name="rData"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool read(int devAddr, ECoilType coilType, int startAddr, int N, ref string rData, ref string er);
        bool read(int devAddr, ECoilType coilType, int startAddr, ref int rVal, ref string er);
        bool read(int devAddr, ECoilType coilType, int startAddr, ref int[] rVal, ref string er);
        /// <summary>
        /// 写IO
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="wVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool write(int devAddr, ECoilType coilType, int startAddr, int wVal, ref string er);
        bool write(int devAddr, ECoilType coilType, int startAddr, int[] wVal, ref string er);

        #endregion

        #region 专用功能
        /// <summary>
        /// 读地址
        /// </summary>
        /// <param name="curAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool readAddr(ref int curAddr, ref string er);
        /// <summary>
        /// 设置地址
        /// </summary>
        /// <param name="curAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool setAddr(int curAddr, ref string er);
        /// <summary>
        /// 读波特率
        /// </summary>
        /// <param name="baud"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool readBaud(int curAddr, ref int baud, ref string er);
        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="baud"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool setBaud(int curAddr, int baud, ref string er);
        /// <summary>
        /// 读错误码
        /// </summary>
        /// <param name="rVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool readErrCode(int curAddr, ref int rVal, ref string er);
        /// <summary>
        /// 读错误码
        /// </summary>
        /// <param name="rVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        bool readVersion(int curAddr, ref int rVal, ref string er);
        #endregion
    }
}
