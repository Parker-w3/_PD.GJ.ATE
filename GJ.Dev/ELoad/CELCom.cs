using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.ELoad
{
   #region 枚举
   public enum EConType
   {
      Rs232,
      Tcp
   }
   public enum ELoadType
   {
      GJEL_20_16,
      GJEL_100_4L
   }
   public enum EMode
   {
      CC,
      CV,
      LED
   }
   #endregion

   #region 参数类
   /// <summary>
   ///设置电子负载参数
   /// </summary>
   public class CEL_SetPara
   {
      private const int ELMaxCH = 4;
      /// <summary>
      /// 写入EEPROM 0:不擦除旧数据及写新数据 1:擦除旧数据 2:写新数据
      /// </summary>
      public int SaveEEPROM = 2;
      /// <summary>
      /// OTP_START温度(90-130)
      /// </summary>
      public  int OTP_Start = 94;
      /// <summary>
      /// OTP_STOP温度(65-100)
      /// </summary>
      public  int OTP_Stop = 70;
      /// <summary>
      /// 0:PWM_STOP 1:PWM_START 
      /// </summary>
      public  int PWM_Status = 1;
      /// <summary>
      /// PWM频率 
      /// </summary>
      public  int PWM_Freq = 100;
      /// <summary>
      /// PWM占空比
      /// </summary>
      public  int PWM_DutyCycle = 1;
      /// <summary>
      /// 工作状态 0:停止 1：启动 
      /// </summary>
      public  int Run_Status = 1;
      /// <summary>
      /// 工作功率 0:20W/100W 1:40W/150W   
      /// </summary>
      public  int[] Run_Power = new int[ELMaxCH];
      /// <summary>
      /// 工作模式 0:CC模式 1:CV模式 2:LED模式
      /// </summary>
      public  EMode[] Run_Mode = new EMode[ELMaxCH];
      /// <summary>
      /// 置工作数据
      /// </summary>
      public  double[] Run_Val = new double[ELMaxCH];
      /// <summary>
      /// 设置Von
      /// </summary>
      public  double[] Run_Von = new double[ELMaxCH];
   }
   /// <summary>
   /// 回读电子负载设置
   /// </summary>
   public class CEL_ReadSetPara
   {
      private const int ELMaxCH = 4;
      public  string[] status = new string[ELMaxCH];
      public  EMode[] LoadMode = new EMode[ELMaxCH];
      public  double[] LoadVal = new double[ELMaxCH];
      public  double[] Von = new double[ELMaxCH];
   }
   /// <summary>
   /// 回读电子负载读值
   /// </summary>
   public class CEL_ReadData
   {
      private const int ELMaxCH = 4;
      /// <summary>
      /// 温度0
      /// </summary>
      public int NTC_0;
      /// <summary>
      /// 温度1
      /// </summary>
      public int NTC_1;
      /// <summary>
      /// ON OFF
      /// </summary>
      public int ONOFF;
      public int OCP;
      public int OVP;
      public int OPP;
      public int OTP;
      /// <summary>
      /// 状态指示
      /// </summary>
      public string Status;
      /// <summary>
      /// Vs电压
      /// </summary>
      public double[] Vs = new double[ELMaxCH];
      /// <summary>
      /// Load电压
      /// </summary>
      public double[] Volt = new double[ELMaxCH];
      /// <summary>
      /// 负载读值
      /// </summary>
      public double[] Load = new double[ELMaxCH];
   }
   #endregion

   public class CELCom
   {
      #region 构造函数
      public CELCom(ELoadType loadType,EConType conType=EConType.Rs232)
      {
         switch (loadType)
         {
            case ELoadType.GJEL_20_16:
               eLoad=new GJEL_20_16();  
               break;
            case ELoadType.GJEL_100_4L:
               eLoad = new GJEL_100_04(); 
               break;
            default:
               break;
         }
         if (eLoad == null)
            return;
         switch (conType)
         {
            case EConType.Rs232:
               eLoad.mCom = new COM.CSerialPort();  
               break;
            case EConType.Tcp:
               break;
            default:
               break;
         } 
      }
      #endregion

      #region 字段
      private IELBase eLoad=null;
      #endregion

      #region 属性
      /// <summary>
      /// 负载通道数
      /// </summary>
      public int mELCH
      {
         get
         {
            if (eLoad == null)
               return 0;
            else
               return eLoad.mELCH;  
         }
      }
      public string mName
      {
         get { return eLoad.mName; }
         set { eLoad.mName = value; }
      }
      public int mIdNo
      {
         get { return eLoad.mIdNo; }
         set { eLoad.mIdNo = value; }
      }
      #endregion

      #region 方法
      public bool open(string comName, ref string er, string setting = "57600,n,8,1")
      {
         if (eLoad == null)
            return false;
         return eLoad.open(comName, ref er, setting); 
      }
      public void close()
      {
         if (eLoad == null)
            return;
         eLoad.close();  
      }
      /// <summary>
      /// 设置地址
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetNewAddr(int wAddr, ref string er)
      {
         if (eLoad == null)
            return false;
         return eLoad.SetNewAddr(wAddr, ref er);  
      }
      /// <summary>
      /// 设置负载
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wDataSet"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetELData(int wAddr, CEL_SetPara wDataSet, ref string er)
      {
         if (eLoad == null)
            return false;
         return eLoad.SetELData(wAddr, wDataSet, ref er);  
      }
      /// <summary>
      /// 读取负载设置
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="rLoadVal"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ReadELLoadSet(int wAddr, CEL_ReadSetPara rDataSet, ref string er)
      {
         if (eLoad == null)
            return false;
         return eLoad.ReadELLoadSet(wAddr, rDataSet, ref er); 
      }
      /// <summary>
      /// 读取负载数值
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="rDataVal"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ReadELData(int wAddr, CEL_ReadData rDataVal, ref string er)
      {
         if (eLoad == null)
            return false;
         return eLoad.ReadELData(wAddr, rDataVal, ref er);  
      }
      #endregion

   }

}
