using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.ELoad
{
  public interface IELBase
  {
     #region 属性
     string mName
     { set; get; }
     int mIdNo
     { set; get; }
     int mELCH
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
     /// 设置地址
     /// </summary>
     /// <param name="wAddr"></param>
     /// <param name="er"></param>
     /// <returns></returns>
     bool SetNewAddr(int wAddr, ref string er);
     /// <summary>
     /// 设置负载
     /// </summary>
     /// <param name="wAddr"></param>
     /// <param name="wDataSet"></param>
     /// <param name="er"></param>
     /// <returns></returns>
     bool SetELData(int wAddr, CEL_SetPara wDataSet, ref string er);
     /// <summary>
     /// 读取负载设置
     /// </summary>
     /// <param name="wAddr"></param>
     /// <param name="rLoadVal"></param>
     /// <param name="er"></param>
     /// <returns></returns>
     bool ReadELLoadSet(int wAddr, CEL_ReadSetPara rDataSet, ref string er);
     /// <summary>
     /// 读取负载数值
     /// </summary>
     /// <param name="wAddr"></param>
     /// <param name="rDataVal"></param>
     /// <param name="er"></param>
     /// <returns></returns>
     bool ReadELData(int wAddr, CEL_ReadData rDataVal, ref string er);
     #endregion
  }
}
