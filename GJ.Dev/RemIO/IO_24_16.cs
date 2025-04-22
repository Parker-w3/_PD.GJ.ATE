using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Dev.COM;
namespace GJ.Dev.RemIO
{
    public class IO_24_16:IIO
    {
      #region 构造函数
      public IO_24_16()
      {
         com = new CSerialPort();
         com.mComDataType =0;
      }
      public IO_24_16(ICom com, int dataType)
      {
         this.com = com;
         com.mComDataType = dataType; 
      }
      #endregion

      #region 字段
      private ICom com;
      private string name = "IO_24_16";
      private int idNo = 0;
      #endregion
      
      #region 属性
      public string mName
      {
         get { return name; }
         set { name = value; }
      }
      public int mIdNo
      {
          get { return idNo; }
          set { idNo = value; }
      }
      public COM.ICom mCom
      { set { com = value; } }
      #endregion

      #region 方法

      /// <summary>
      /// 打开串口
      /// </summary>
      /// <param name="comName">115200,n,8,1</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool open(string comName, ref string er,string setting="115200,n,8,1")
      {
          if (!com.open(comName, ref er,setting))
            return false;
         return true;
      }
      /// <summary>
      /// 关闭串口
      /// </summary>
      public void close()
      {
         com.close();
      }
      /// <summary>
      /// 读线圈
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
         try
         {
             string wCmd = devAddr.ToString("X2");
             int rLen = 0;

             if (coilType != ECoilType.D)
             {
                 wCmd += "01";          //线圈功能码为01
                 rLen = (N + 7) / 8;    //8个线圈为1Byte:前8个线圈存第1字节,最小线圈存最低位
             }
             else
             {
                 wCmd += "03";      //寄存器功能码为03
                 rLen = N * 2;
             }
             wCmd += formatDevAddr(coilType, startAddr);  //开始地址
             wCmd += N.ToString("X4");                   //读地址长度
             wCmd += CMath.crc16(wCmd);                  //CRC16 低位前,高位后            
             if (!com.send(wCmd, 5 + rLen, ref rData, ref er))
                 return false;
             if (!checkCRC(rData))
             {
                 er = "crc16检验和错误:" + rData;
                 return false;
             }
             string temp = rData.Substring(6, rLen * 2);
             if (coilType != ECoilType.D)
             {
                 //转化线圈数据为 Mn,Mn-1..M1,M0(高位在前,低位在后)
                 string coil_HL = string.Empty;
                 int coilLen = temp.Length / 2;
                 for (int i = 0; i < coilLen; i++)
                 {
                     coil_HL += temp.Substring(temp.Length - (i + 1) * 2, 2);
                 }
                 rData = coil_HL;
             }
             else
             {
                 rData = temp;     //2个字节为寄存器值，高在前,低位在后，寄存器小排最前面；
                 //转换为寄存器小排最后
                 rData = string.Empty;
                 for (int i = 0; i < temp.Length / 4; i++)
                 {
                     rData = temp.Substring(i * 4, 4) + rData;
                 }
             }
             return true;
         }
         catch (Exception e)
         {
             er = e.ToString();
             return false;
         }
      }
      /// <summary>
      /// 读单个线圈
      /// </summary>
      /// <param name="devType"></param>
      /// <param name="startAddr"></param>
      /// <param name="N"></param>
      /// <param name="rVal">开始地址值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool read(int devAddr, ECoilType coilType, int startAddr, ref int rVal, ref string er)
      {
          try
          {
              int N = 1;
              string rData = string.Empty;
              if (!read(devAddr,coilType, startAddr, N, ref rData, ref er))
                  return false;
              if ((coilType != ECoilType.D))
              {
                  for (int i = 0; i < rData.Length / 2; i++)
                  {
                      int valByte = System.Convert.ToInt32(rData.Substring(rData.Length - (i + 1) * 2, 2), 16);
                      if ((valByte & (1 << 0)) == (1 << 0))
                          rVal = 1;
                      else
                          rVal = 0;
                  }
              }
              else
                  rVal = System.Convert.ToInt32(rData.Substring(rData.Length - 4, 4), 16);
              return true;
          }
          catch (Exception e)
          {
              er = e.ToString();
              return false;
          }
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
          try
          {
              string rData = string.Empty;
              int N = rVal.Length;
              if (!read(devAddr,coilType,startAddr, N, ref rData, ref er))
                  return false;
              if (coilType != ECoilType.D)
              {
                  for (int i = 0; i < rData.Length / 2; i++)
                  {
                      int valByte = System.Convert.ToInt32(rData.Substring(rData.Length - (i + 1) * 2, 2), 16);
                      for (int j = 0; j < 8; j++)
                      {
                          if ((j + 8 * i) < N)
                          {
                              if ((valByte & (1 << j)) == (1 << j))
                                  rVal[j + i * 8] = 1;
                              else
                                  rVal[j + i * 8] = 0;
                          }
                      }
                  }
              }
              else
              {
                  for (int i = 0; i < N; i++)
                      rVal[i] = System.Convert.ToInt32(rData.Substring(rData.Length - (i + 1) * 4, 4), 16);

              }
              return true;
          }
          catch (Exception e)
          {
              er = e.ToString();
              return false;
          }
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
          try
          {
              int N = 1;   //单写1个值
              string wCmd = devAddr.ToString("X2");
              int rLen = 0;
              int wLen = 0;
              string wData = string.Empty;
              if (coilType != ECoilType.D)
              {
                  wCmd += "0F";          //线圈功能码为15
                  wLen = (7 + N) / 8;    //写入字节数
                  rLen = 8;              //回读长度
                  wData = wVal.ToString("X" + wLen * 2);
              }
              else
              {
                  wCmd += "10";        //寄存器功能码为16
                  wLen = N * 2;          //写入字节数
                  rLen = 8;           //回读长度
                  wData = wVal.ToString("X" + wLen * 2);
              }
              wCmd += formatDevAddr(coilType, startAddr);  //开始地址
              wCmd += N.ToString("X4");                  //读地址长度
              wCmd += wLen.ToString("X2");               //写入字节数  
              wCmd += wData;                             //写入数据
              wCmd += CMath.crc16(wCmd);                //CRC16 低位前,高位后   
              string rData = string.Empty;
              if (!com.send(wCmd, rLen, ref rData, ref er))
                  return false;
              if (!checkCRC(rData))
              {
                  er = "crc16检验和错误:" + rData;
                  return false;
              }
              return true;
          }
          catch (Exception e)
          {
              er = e.ToString();
              return false;
          }      
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
          try
          {
              int N = wVal.Length;   //单写多个值
              string wCmd = devAddr.ToString("X2");
              int rLen = 0;
              int wLen = 0;
              string wData = string.Empty;
              if (coilType != ECoilType.D)
              {
                  wCmd += "0F";          //线圈功能码为15
                  wLen = (7 + N) / 8;    //写入字节数
                  rLen = 8;              //回读长度
                  for (int i = 0; i < wLen; i++)
                  {
                      int val = 0;
                      for (int j = 0; j < 8; j++)
                      {
                          if (i * 8 + j < N)
                          {
                              int bit = (wVal[i * 8 + j] & 0x1) << j;
                              val += bit;
                          }
                      }
                      wData += val.ToString("X2");
                  }
              }
              else
              {
                  wCmd += "10";        //寄存器功能码为16
                  wLen = N * 2;          //写入字节数
                  rLen = 8;           //回读长度
                  for (int i = 0; i < N; i++)
                  {
                      wData += wVal[i].ToString("X4");
                  }

              }
              wCmd += formatDevAddr(coilType, startAddr);  //开始地址
              wCmd += N.ToString("X4");                  //读地址长度
              wCmd += wLen.ToString("X2");               //写入字节数  
              wCmd += wData;                             //写入数据
              wCmd += CMath.crc16(wCmd);                //CRC16 低位前,高位后   
              string rData = string.Empty;
              if (!com.send(wCmd, rLen, ref rData, ref er))
                  return false;
              if (!checkCRC(rData))
              {
                  er = "crc16检验和错误:" + rData;
                  return false;
              }
              return true;
          }
          catch (Exception e)
          {
              er = e.ToString();
              return false;
          }
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
          try
          {
              string rData=string.Empty;
              if (!read(1, ECoilType.D, 0xF000, 1, ref rData, ref er))
                  return false;
              curAddr = System.Convert.ToInt32(rData,16);  
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false; 
          }
      }
      /// <summary>
      /// 设置地址
      /// </summary>
      /// <param name="curAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool setAddr(int curAddr, ref string er)
      {
          try
          {
              return write(1, ECoilType.D, 0xF000, curAddr, ref er);
          }
          catch (Exception ex)
          {
              er=ex.ToString();
              return false; 
          }
      }
      /// <summary>
      /// 读波特率
      /// </summary>
      /// <param name="baud"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool readBaud(int curAddr, ref int baud, ref string er)
      {
          try
          {
              string rData = string.Empty;
              if (!read(curAddr, ECoilType.D, 0xF400, 1, ref rData, ref er))
                  return false;
              baud = System.Convert.ToInt32(rData,16);
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
      }
      /// <summary>
      /// 设置波特率
      /// </summary>
      /// <param name="baud"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool setBaud(int curAddr, int baud, ref string er)
      {
          try
          {
              return write(curAddr, ECoilType.D, 0xF400, baud, ref er);
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
      }
      /// <summary>
      /// 读错误码
      /// </summary>
      /// <param name="rVal"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool readErrCode(int curAddr, ref int rVal, ref string er)
      {
          try
          {
              string rData = string.Empty;
              if (!read(curAddr, ECoilType.D, 0xF700, 1, ref rData, ref er))
                  return false;
              rVal = System.Convert.ToInt32(rData,16);
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
      }
      /// <summary>
      /// 读错误码
      /// </summary>
      /// <param name="rVal"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool readVersion(int curAddr, ref int rVal, ref string er)
      {
          try
          {
              string rData = string.Empty;
              if (!read(curAddr, ECoilType.D, 0xFA00, 1, ref rData, ref er))
                  return false;
              rVal = System.Convert.ToInt32(rData,16);
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
      }
      #endregion

      #region ModBus-RTU通信协议
      /// <summary>
      /// 格式化地址段
      /// </summary>
      /// <param name="devType">
      /// X0-X255:起始地址->0xF800
      /// Y0-Y255:起始地址->0xFC00
      /// </param>
      /// <param name="coilAddr"></param>
      /// <returns></returns>
      private string formatDevAddr(ECoilType coilType, int coilAddr)
      {
         int addr = 0;
         switch (coilType)
         {
            case ECoilType.X:
               addr = coilAddr + 0xF800;
               break;
            case ECoilType.Y:
               addr = coilAddr + 0xFC00;
               break;
             case ECoilType.D:
               addr = coilAddr;
               break;
            default:
               break;
         }
         return addr.ToString("X4");
      }
      /// <summary>
      /// 检查CRC
      /// </summary>
      /// <param name="wCmd"></param>
      /// <returns></returns>
      private bool checkCRC(string wCmd)
      {
         string crc = CMath.crc16(wCmd.Substring(0, wCmd.Length - 4));
         if (crc != wCmd.Substring(wCmd.Length - 4, 4))
            return false;
         return true;
      }
      #endregion


    }
}
