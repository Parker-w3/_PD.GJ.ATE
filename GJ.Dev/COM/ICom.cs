using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.COM
{
   public interface ICom
   {
      #region 属性
      /// <summary>
      /// 设备id
      /// </summary>
      int mIdNo
      { get; set; }
      /// <summary>
      /// 设备名称
      /// </summary>
      string mName
      { get; set; }
      /// <summary>
      /// 设备通信状态
      /// </summary>
      bool mConStatus
      { get; }
      #endregion
      /// <summary>
      /// 设备数据格式：0->16进制字符;1->ASCII
      /// </summary>
      int mComDataType
      { set; }
      #region 方法
      /// <summary>
      /// 打开设备
      /// </summary>
      /// <returns></returns>
      bool open(string comName, ref string er, string setting);
      /// <summary>
      /// 关闭设备
      /// </summary>
      /// <returns></returns>
      bool close();
      /// <summary>
      /// 设置波特率
      /// </summary>
      /// <param name="setting"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool setBaud(ref string er, string setting);
      /// <summary>
      /// 发送数据
      /// </summary>
      /// <param name="wData"></param>
      /// <param name="rLen"></param>
      /// <param name="rData"></param>
      /// <param name="er"></param>
      /// <param name="timeOut"></param>
      /// <returns></returns>
      bool send(string wData, int rLen, ref string rData, ref string er, int timeOut = 2000);
      /// <summary>
      /// 发送数据
      /// </summary>
      /// <param name="wData"></param>
      /// <param name="rEOI"></param>
      /// <param name="rData"></param>
      /// <param name="er"></param>
      /// <param name="timeOut"></param>
      /// <returns></returns>
      bool send(string wData, string rEOI, ref string rData, ref string er, int timeOut = 2000);
       /// <summary>
       /// 设置串口DTR RTS属性
       /// </summary>
       /// <param name="Dtr"></param>
       /// <param name="Rts"></param>
       /// <param name="er"></param>
       /// <returns></returns>
      bool SetDtrRts(bool Dtr, bool Rts, ref string er);
      #endregion
   }
}
