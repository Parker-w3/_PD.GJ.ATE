using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Dev.COM;

namespace GJ.Dev.PLC
{
    public class Inovance_TCP:IPLC
    {
      #region 构造函数
      public Inovance_TCP()
      {
         com = new CTcpPort();  
         com.mComDataType =0;
      }
      public Inovance_TCP(ICom com, int dataType)
      {
         this.com = com;
         com.mComDataType = dataType; 
      }
      #endregion

      #region 字段
      private ICom com;
      private string name = "Inovance_TCP";
      private int plcNo = 1;
      #endregion
      
      #region 属性
      public string mName
      {
         get { return name; }
         set { name = value; }
      }
      public int mIdNo
      {
         get { return plcNo; }
         set { plcNo = value; }
      }
      public COM.ICom mCom
      {
          get { return com; }
          set { com = value; } }     
      #endregion

      #region 方法

      /// <summary>
      /// 打开串口
      /// </summary>
      /// <param name="comName">115200,n,8,2</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool open(string comName, ref string er, string setting)
      {
          if (!com.open(comName,ref er,setting))
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
      /// 读线圈和寄存器值
      /// MBAP报文(7Byte)+功能码(1Byte)+寄存器地址(2Byte)+地址数量(2Byte)
      /// MBAP报文=事务标识(2Bytes)+协议标识(2Bytes:Modbus=0;UNI-TE=1)+长度(2Byte:后续字节长度)+单元标识(0xFF)
      /// </summary>
      /// <param name="devType">地址类型</param>
      /// <param name="startAddr">开始地址</param>
      /// <param name="N">地址长度</param>
      /// <param name="rData">16进制字符:数据值高位在前,低位在后</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool read(EDevType devType, int startAddr, int N, ref string rData, ref string er)
      {
         try
         {

            string wCmd = string.Empty;

            int rLen = 0;

            if (devType != EDevType.D)
            {
               wCmd += "01";          //线圈功能码为01
               rLen = (N + 7) / 8;    //8个线圈为1Byte:前8个线圈存第1字节,最小线圈存最低位
            }
            else
            {
               wCmd += "03";      //寄存器功能码为03
               rLen = N * 2;
            }
            wCmd += formatDevAddr(devType, startAddr);  //开始地址
            wCmd += N.ToString("X4");                   //读地址长度   

            int MBAPLen = wCmd.Length / 2 + 1;

            int nowTime = System.Environment.TickCount;

            string MBAPSOI = nowTime.ToString("D4");

            MBAPSOI = MBAPSOI.Substring(0, 4);   

            wCmd = MBAPSOI + "0000" + MBAPLen.ToString("X4") + "FF" + wCmd; 

            if (!com.send(wCmd, 9+ rLen, ref rData, ref er))
               return false;
            if (rData.Substring(0, 4) != MBAPSOI)
                return false;
            if (rData.Length < 14)
                return false;

            string temp = rData.Substring(rData.Length - rLen * 2, rLen * 2);
            if (devType !=EDevType.D)
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
         try
         {
            int N = 1;
            string rData = string.Empty;
            if (!read(devType, startAddr, N, ref rData, ref er))
               return false;
            if (devType != EDevType.D)
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
      public bool read(EDevType devType, int startAddr, ref int[] rVal, ref string er)
      {
         try
         {
            string rData = string.Empty;
            int N = rVal.Length;
            if (!read(devType, startAddr, N, ref rData, ref er))
               return false;
            if (devType != EDevType.D)
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
      /// MBAP报文(7Byte)+功能码(1Byte)+寄存器地址(2Byte)+地址数量(2Byte)+字节数(1Byte)+数据+CRC检验(2Byte)
      /// MBAP报文=事务标识(2Bytes)+协议标识(2Bytes:Modbus=0;UNI-TE=1)+长度(1Byte:后续字节长度)+单元标识(0xFF)
      /// </summary>
      /// <param name="devType">地址类型</param>
      /// <param name="startAddr">开始地址</param>
      /// <param name="wVal">单个值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool write(EDevType devType, int startAddr, int wVal, ref string er)
      {
         try
         {
            int N = 1;   //单写1个值            
            int rLen = 0;
            int wLen = 0;
            string wCmd = string.Empty;
            string wData = string.Empty;
            if (devType != EDevType.D)
            {
               wCmd += "0F";          //线圈功能码为15
               wLen = (7 + N) / 8;    //写入字节数
               rLen = 12;              //回读长度
               wData = wVal.ToString("X" + wLen * 2);
            }
            else
            {
               wCmd += "10";        //寄存器功能码为16
               wLen = N * 2;          //写入字节数
               rLen = 12;           //回读长度
               wData = wVal.ToString("X" + wLen * 2);
            }
            wCmd += formatDevAddr(devType, startAddr);  //开始地址
            wCmd += N.ToString("X4");                  //读地址长度
            wCmd += wLen.ToString("X2");               //写入字节数  
            wCmd += wData;                             //写入数据

            int MBAPLen = wCmd.Length / 2 + 1;

            int nowTime = System.Environment.TickCount;

            string MBAPSOI = nowTime.ToString("D4");

            MBAPSOI = MBAPSOI.Substring(0, 4);

            wCmd = MBAPSOI + "0000" + MBAPLen.ToString("X4") + "FF" + wCmd; 

            string rData = string.Empty;
            if (!com.send(wCmd, rLen, ref rData, ref er))
               return false;
            if (rData.Substring(0, 4) != MBAPSOI)
                return false;
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
      public bool write(EDevType devType, int startAddr, int[] wVal, ref string er)
      {
         try
         {
            int N = wVal.Length;   //单写多个值            
            int rLen = 0;
            int wLen = 0;
            string wCmd = string.Empty;
            string wData = string.Empty;
            if (devType !=EDevType.D)
            {
               wCmd += "0F";          //线圈功能码为15
               wLen = (7 + N) / 8;    //写入字节数
               rLen = 12;              //回读长度
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
               rLen = 12;           //回读长度
               for (int i = 0; i < N; i++)
               {
                  wData += wVal[i].ToString("X4");
               }
            }
            wCmd += formatDevAddr(devType, startAddr);  //开始地址
            wCmd += N.ToString("X4");                  //读地址长度
            wCmd += wLen.ToString("X2");               //写入字节数  
            wCmd += wData;                             //写入数据

            int MBAPLen = wCmd.Length / 2 + 1;

            wCmd = plcNo.ToString("X4") + "0000" + MBAPLen.ToString("X4") + "FF" + wCmd; 

            string rData = string.Empty;
            if (!com.send(wCmd, rLen, ref rData, ref er))
               return false;
            if (rData.Substring(0, 4) != plcNo.ToString("X4"))
                return false;
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
      }
      #endregion

      #region ModBus-RTU通信协议
      /// <summary>
      /// 格式化地址段
      /// </summary>
      /// <param name="devType">
      /// M0-M3071:起始地址->0x0
      /// M8000-M8255:起始地址->0x1F40
      /// X0-X255:起始地址->0xF800
      /// Y0-Y255:起始地址->0xFC00
      /// D0-D8255:起始地址->0x0
      /// </param>
      /// <param name="devAddr"></param>
      /// <returns></returns>
      private string formatDevAddr(EDevType devType, int devAddr)
      {
         int addr = 0;
         switch (devType)
         {
            case EDevType.M:
               addr = devAddr;
               break;
            case EDevType.X:
               addr = devAddr + 0xF800;
               break;
            case EDevType.Y:
               addr = devAddr + 0xFC00;
               break;
            case EDevType.D:
               addr = devAddr;
               break;
            default:
               break;
         }
         return addr.ToString("X4");
      }
      #endregion

    }
}
