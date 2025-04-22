using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.RemIO
{

    public enum ERunMode
    {
        自动控制,
        重计时运行,
        接续运行,
        暂停,
        停止,
        手动
    }

    public enum EIOType
    {
        IO_24_16,
        IO_16_32
    }
    /// <summary>
    /// 线圈:X,Y
    /// </summary>
    public enum ECoilType
    {
        X,
        Y,
        D,
        A,      //AC
        T,      //Temp
        L,      //LTemp
        U,      //TUse
        B,      //TBu
        R       //DORun
    }
    public class CIOCom
    {

      #region IO电平定义
    public const int XON = 1;
    public const int XOFF = 0;
    public const int YON = 1;
    public const int YOFF = 0;
    #endregion

      #region 构造函数
      public CIOCom(EIOType ioType)
      {
          switch (ioType)
         {
             case EIOType.IO_24_16:
                 io = new IO_24_16();
                 io.mCom = new COM.CSerialPort(); 
                 break;          
            default:
                  break;
         }
          if (io == null)
            return;
      }
      #endregion

      #region 字段
      private IIO io = null;
      #endregion

      #region 属性
      public string mName
      {
         set
         {
             io.mName = value;  
         }
         get
         {
             return io.mName;  
         }
      }
      public int mIdNo
      {
         set
         {
             io.mIdNo = value;  
         }
         get
         {
             return io.mIdNo;  
         }
      }
      #endregion

      #region 方法
      /// <summary>
      /// 打开串口
      /// </summary>
      /// <param name="comName"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool open(string comName, ref string er,string setting="115200,n,8,1")
      {
          if (io == null)
            return false;
          return io.open(comName, ref er, setting);
      }
      /// <summary>
      /// 关闭串口
      /// </summary>
      public void close()
      {
         if (io == null)
            return;
         io.close();
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
      public bool read(int devAddr, ECoilType coilType, int startAddr, int N, ref string rData, ref string er)
      {
         return io.read(devAddr,coilType, startAddr, N, ref rData, ref er); 
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
      public bool read(int devAddr, ECoilType coilType, int startAddr, ref int rVal, ref string er)
      {
         return io.read(devAddr,coilType,startAddr,ref rVal, ref er); 
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
      public bool read(int devAddr, ECoilType coilType, int startAddr, ref int[] rVal, ref string er)
      {
         return io.read(devAddr,coilType,startAddr, ref rVal, ref er);
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
      public bool write(int devAddr, ECoilType coilType, int startAddr, int wVal, ref string er)
      {
         return io.write(devAddr,coilType,startAddr, wVal, ref er);   
      }
      /// <summary>
      /// 写多个线圈和寄存器
      /// </summary>
      /// <param name="devType">地址类型</param>
      /// <param name="startAddr">开始地址</param>
      /// <param name="wVal">多个值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool write(int devAddr, ECoilType coilType, int startAddr, int[] wVal, ref string er)
      {
         return io.write(devAddr,coilType,startAddr, wVal, ref er);  
      }
      #endregion

      #region 专用功能
      /// <summary>
      /// 读地址
      /// </summary>
      /// <param name="curAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool readAddr(ref int curAddr, ref string er)
      {
         return io.readAddr(ref curAddr,ref er); 
      }
      /// <summary>
      /// 设置地址
      /// </summary>
      /// <param name="curAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool setAddr(int curAddr, ref string er)
      {
        return io.setAddr(curAddr,ref er);  
      }
      /// <summary>
      /// 读波特率
      /// </summary>
      /// <param name="baud"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool readBaud(int curAddr, ref int baud, ref string er)
      {
         return io.readBaud(curAddr,ref baud,ref er);  
      }
      /// <summary>
      /// 设置波特率
      /// </summary>
      /// <param name="baud"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool setBaud(int curAddr, int baud, ref string er)
      {
         return io.setBaud(curAddr,baud,ref er); 
      }
      /// <summary>
      /// 读错误码
      /// </summary>
      /// <param name="rVal"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool readErrCode(int curAddr, ref int rVal, ref string er)
      {
         return io.readErrCode(curAddr,ref rVal,ref er); 
      }
      /// <summary>
      /// 读错误码
      /// </summary>
      /// <param name="rVal"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool readVersion(int curAddr, ref int rVal, ref string er)
      {
          return io.readVersion(curAddr,ref rVal,ref er); 
      }
      #endregion
    }
}
