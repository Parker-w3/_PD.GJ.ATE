using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.ERS
{
   public interface IERS
   {
      #region 属性
      string mName
      { set; get; }
      int mIdNo
      { set; get; }
      COM.ICom mCom
      { set; }
      int mCHNo
      { get; }
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
      /// 设置地址
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool SetNewAddr(int wAddr, ref string er);
      /// <summary>
      /// 读版本
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="version"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool ReadVersion(int wAddr, ref string version, ref string er);
      /// <summary>
      /// 设置负载
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="loadPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool SetNewLoad(int wAddr, CERS_SetLoad loadPara, ref string er, bool saveEPROM);
      bool SetNewLoad(int wAddr, int CH, double loadVal, ref string er, bool saveEPROM);
      /// <summary>
      /// 读当前负载值
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="dataVal"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool ReadLoadSet(int wAddr, CERS_ReadLoadSet loadVal, ref string er);
      /// <summary>
      /// 读电压和电流
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="dataVal"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool ReadData(int wAddr, CERS_ReadData dataVal, ref string er);
      /// <summary>
      /// 快充电压上升或下降
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="CH"></param>
      /// <param name="wRaise"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool SetQCMTK(int wAddr, int CH, bool wRaise, ref string er);
      #endregion
   }
}
