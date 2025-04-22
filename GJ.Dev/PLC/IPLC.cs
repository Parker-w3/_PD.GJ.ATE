using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.PLC
{
   public interface IPLC
   {
      #region 属性
      string mName
      { set; get; }
      int mIdNo
      { set; get; }
      COM.ICom mCom
      { set; get; }
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

      bool read(EDevType devType, int startAddr, int N, ref string rData, ref string er);
      bool read(EDevType devType, int startAddr, ref int rVal, ref string er);
      bool read(EDevType devType, int startAddr, ref int[] rVal, ref string er);

      bool write(EDevType devType, int startAddr, int wVal, ref string er);
      bool write(EDevType devType, int startAddr, int[] wVal, ref string er);

      #endregion
   }
}
