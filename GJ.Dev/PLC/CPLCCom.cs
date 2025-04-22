using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.PLC
{
   public enum EConType
   {
      Rs232,
      Tcp
   }
   public enum EPLCType
   {
      OmronCOM,
      InovanceCOM,
      InovanceTCP
   }
   /// <summary>
   /// 线圈:M,X,Y;寄存器:D
   /// </summary>
   public enum EDevType
   {
      M,
      W,
      D,
      X,
      Y
   }
   public class CPLCCom
   {

      #region 常量定义
       public const int ON = 1;
       public const int OFF = 0;
       public const int OK = 1;
       public const int Go = 2; //过站
       public const int NG = 3;//失败
       public const int ScanFail = 4; //扫码不良
       #endregion

      #region 构造函数
       public CPLCCom(EPLCType plcType)
      {
         switch (plcType)
         {
            case EPLCType.OmronCOM:
                  plc = new Omron_COM();
                  plc.mCom = new COM.CSerialPort(); 
                  break;
            case EPLCType.InovanceCOM:
                  plc = new Inovance_COM();
                  plc.mCom = new COM.CSerialPort(); 
                  break;
             case EPLCType.InovanceTCP:
                  plc = new Inovance_TCP();
                  plc.mCom = new COM.CTcpPort();  
                  break;
            default:
                  break;
         }
         if (plc == null)
            return;
      }
      #endregion

      #region 字段
      private IPLC plc = null;
      #endregion

      #region 属性
      public string mplcName
      {
         set
         {
            plc.mName = value;  
         }
         get
         {
            return plc.mName;  
         }
      }
      public int mPlcNo
      {
         set
         {
            plc.mIdNo = value;  
         }
         get
         {
            return plc.mIdNo;  
         }
      }
      public bool mConStatus
      {
          get { return plc.mCom.mConStatus; }
      }
      #endregion

      #region 方法
      /// <summary>
      /// 打开串口
      /// </summary>
      /// <param name="comName"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool open(string comName, ref string er, string setting="502")
      {
         if (plc == null)
            return false;
         return plc.open(comName, ref er, setting);
      }
      /// <summary>
      /// 关闭串口
      /// </summary>
      public void close()
      {
         if (plc == null)
            return;
         plc.close();
      }
      /// <summary>
      /// 读线圈和寄存器值
      /// 从机地址(1Byte)+功能码(1Byte)+寄存器地址(2Byte)+地址数量(2Byte)+CRC检验(2Byte)
      /// </summary>
      /// <param name="devType">地址类型</param>
      /// <param name="startAddr">开始地址</param>
      /// <param name="N">地址长度</param>
      /// <param name="rData">16进制字符:数据值高位在前,低位在后</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool read(EDevType devType, int startAddr, int N, ref string rData, ref string er)
      {
         return plc.read(devType, startAddr, N, ref rData, ref er); 
      }
      /// <summary>
      /// 返回单个数值
      /// </summary>
      /// <param name="devType"></param>
      /// <param name="startAddr"></param>
      /// <param name="N"></param>
      /// <param name="rVal">开始地址值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool read(EDevType devType, int startAddr, ref int rVal, ref string er)
      {
         return plc.read(devType, startAddr,ref rVal, ref er); 
      }
      /// <summary>
      /// 返回数值
      /// </summary>
      /// <param name="devType"></param>
      /// <param name="startAddr"></param>
      /// <param name="N"></param>
      /// <param name="rVal">地址N值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool read(EDevType devType, int startAddr, ref int[] rVal, ref string er)
      {
         return plc.read(devType, startAddr, ref rVal, ref er);
      }
      /// <summary>
      /// 单写线圈和寄存器值
      /// 从机地址(1Byte)+功能码(1Byte)+寄存器地址(2Byte)+地址数量(2Byte)+字节数(1Byte)+数据+CRC检验(2Byte)
      /// </summary>
      /// <param name="devType">地址类型</param>
      /// <param name="startAddr">开始地址</param>
      /// <param name="wVal">单个值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool write(EDevType devType, int startAddr, int wVal, ref string er)
      {
         return plc.write(devType, startAddr, wVal, ref er);   
      }
      /// <summary>
      /// 写多个线圈和寄存器
      /// </summary>
      /// <param name="devType">地址类型</param>
      /// <param name="startAddr">开始地址</param>
      /// <param name="wVal">多个值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool write(EDevType devType, int startAddr, int[] wVal, ref string er)
      {
         return plc.write(devType, startAddr, wVal, ref er);  
      }
      #endregion

   }
}
