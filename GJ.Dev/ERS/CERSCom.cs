using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.ERS
{
   #region 枚举
   public enum EERSType
   {
      GJ4850,
      GJ272_2,
      GJ272_4
   }
   #endregion

   #region 参数类
   /// <summary>
   /// 设置负载
   /// </summary>
   public class CERS_SetLoad
   {
      public double[] loadVal = new double[4];
   }
   /// <summary>
   /// 读取负载设置
   /// </summary>
   public class CERS_ReadLoadSet
   {
      public double[] loadVal = new double[4];
   }
   /// <summary>
   /// 读取数据
   /// </summary>
   public class CERS_ReadData
   {
      public double[] volt = new double[4];
      public double[] cur = new double[4];
   }
   #endregion

   public class CERSCom
   {
       #region 字段
       private IERS ers = null;
       #endregion

       #region 构造函数
       public CERSCom(EERSType ersType)
       {
           switch (ersType)
           {
               case EERSType.GJ4850:

                   break;
               case EERSType.GJ272_2:
                   break;
               case EERSType.GJ272_4:
                   ers = new GJ272_4();                   
                   ers.mCom = new COM.CSerialPort();
                   break;
               default:
                   break;
           }
       }
       #endregion

       #region 属性
       /// <summary>
       /// 负载通道数
       /// </summary>
       public int mCHNo
       {
           get
           {
               if (ers == null)
                   return 0;
               else
                   return ers.mCHNo;
           }
       }
       public string mName
       {
           get { return ers.mName; }
           set { ers.mName = value; }
       }
       public int mIdNo
       {
           get { return ers.mIdNo; }
           set { ers.mIdNo = value; }
       }
       #endregion

      #region 方法
      public bool open(string comName, ref string er, string setting="9600,n,8,1")
      {
         if (ers == null)
            return false;
         return ers.open(comName, ref er, setting); 
      }
      public void close()
      {
         if (ers == null)
            return;
         ers.close();
      }
      public bool SetNewAddr(int wAddr, ref string er)
      {
         if (ers == null)
            return false;
         return ers.SetNewAddr(wAddr, ref er); 
      }
      public bool ReadVersion(int wAddr, ref string version, ref string er)
      {
         if (ers == null)
            return false;
         return ers.ReadVersion(wAddr,ref version, ref er); 
      }
      public bool SetNewLoad(int wAddr, CERS_SetLoad loadPara, ref string er, bool saveEPROM = true)
      {
         if (ers == null)
            return false;
         return ers.SetNewLoad(wAddr,loadPara, ref er,saveEPROM);
      }
      public bool SetNewLoad(int wAddr, int CH, double loadVal, ref string er, bool saveEPROM = true)
      {
          if (ers == null)
              return false;
          return ers.SetNewLoad(wAddr, CH, loadVal,ref er, saveEPROM);
      }
      public bool ReadLoadSet(int wAddr, CERS_ReadLoadSet loadVal, ref string er)
      {
          if (ers == null)
              return false;
          return ers.ReadLoadSet(wAddr, loadVal, ref er);
      }
      public bool ReadData(int wAddr, CERS_ReadData dataVal, ref string er)
      {
          if (ers == null)
              return false;
          return ers.ReadData(wAddr, dataVal, ref er);
      }
      /// <summary>
      /// 快充电压上升或下降
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="CH">0,1,2,3</param>
      /// <param name="wRaise"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetQCMTK(int wAddr, int CH, bool wRaise, ref string er)
      {
          if (ers == null)
              return false;
          return ers.SetQCMTK(wAddr, CH,wRaise,ref er);
      }
      #endregion
   }
}
