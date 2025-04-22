using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading; 

namespace GJ.Dev.COM
{
   public class CSerialPort:ICom
   {
      #region 字段
      private int idNo = 0;
      private string name = "SerialCom";
      private int comDataType = 0;
      private SerialPort rs232;
      private bool conStatus = false;
      private ReaderWriterLock comLock = new ReaderWriterLock();
      #endregion

      #region 属性
      public int mIdNo
      {
         get{return idNo;}
         set{idNo = value;}
      }
      public string mName
      {
         get{return name;}
         set{name = value;}
      }
      public int mComDataType
      {
         set{comDataType = value;}
      }
      public bool mConStatus
      {
          get { return conStatus; }
      }
      #endregion

      #region 方法

      public bool SetDtrRts( bool Dtr , bool Rts,ref string er)
      {
          try
          {

              rs232.DtrEnable = Dtr;
              rs232.RtsEnable = Rts;
              return true;
          }

          catch (Exception ex)
          {
              er = ex.ToString();
              return false ;
          }
      }
      /// <summary>
      /// 打开串口
      /// </summary>
      /// <param name="comName"></param>
      /// <param name="setting">9600,n,8,1</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool open(string comName,ref string er,string setting)
      {
         try
         {
            string[] arrayPara = setting.Split(',');
            if (arrayPara.Length != 4)
            {
               er = "串口设置参数错误";
               return false;
            }
            int bandRate = System.Convert.ToInt32(arrayPara[0]);
            Parity parity = Parity.None;
            switch (arrayPara[1].ToUpper())
            {
               case "O":
                  parity = Parity.Odd;
                  break;
               case "E":
                  parity = Parity.Even;
                  break;
               case "M":
                  parity = Parity.Mark;
                  break;
               case "S":
                  parity = Parity.Space;
                  break;
               default:
                  break;
            }
            int dataBit = System.Convert.ToInt32(arrayPara[2]);
            StopBits stopBits = StopBits.One;
            switch (arrayPara[3])
            {
               case "0":
                  stopBits = StopBits.None;
                  break;
               case "1.5":
                  stopBits = StopBits.OnePointFive;
                  break;
               case "2":
                  stopBits = StopBits.Two;
                  break;
               default:
                  break;
            }
            if (rs232 != null)
            {
               if (rs232.IsOpen)
                  rs232.Close();
               rs232 = null;
            }
            rs232 = new SerialPort(comName, bandRate, parity, dataBit, stopBits);
           // rs232.DtrEnable = true;
            rs232.RtsEnable = true  ;  
            rs232.Open();
            conStatus = true;
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
      }
      /// <summary>
      /// 关闭串口
      /// </summary>
      /// <returns></returns>
      public bool close()
      {
         if (rs232 != null)
         {
            if (rs232.IsOpen)
               rs232.Close();
            rs232 = null;
            conStatus = false;
         }
         return true;
      }
   
      /// <summary>
      /// 设置波特率
      /// </summary>
      /// <param name="setting"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool setBaud(ref string er, string setting)
      {
          try
          {
              if (rs232 == null)
              {
                  er = "串口未初始化";
                  return false;
              }
              string[] arrayPara = setting.Split(',');
              if (arrayPara.Length != 4)
              {
                  er = "串口设置参数错误";
                  return false;
              }
              int bandRate = System.Convert.ToInt32(arrayPara[0]);
              Parity parity = Parity.None;
              switch (arrayPara[1].ToUpper())
              {
                  case "O":
                      parity = Parity.Odd;
                      break;
                  case "E":
                      parity = Parity.Even;
                      break;
                  case "M":
                      parity = Parity.Mark;
                      break;
                  case "S":
                      parity = Parity.Space;
                      break;
                  default:
                      break;
              }
              int dataBit = System.Convert.ToInt32(arrayPara[2]);
              StopBits stopBits = StopBits.One;
              switch (arrayPara[3])
              {
                  case "0":
                      stopBits = StopBits.None;
                      break;
                  case "1.5":
                      stopBits = StopBits.OnePointFive;
                      break;
                  case "2":
                      stopBits = StopBits.Two;
                      break;
                  default:
                      break;
              }
              rs232.BaudRate = bandRate;
              rs232.Parity = parity;
              rs232.DataBits = dataBit;
              rs232.StopBits = stopBits;
              return true; 
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
      }


      /// <summary>
      /// 发送数据及接收数据
      /// </summary>
      /// <param name="wData"></param>
      /// <param name="rLen"></param>
      /// <param name="rData"></param>
      /// <param name="er"></param>
      /// <param name="timeOut"></param>
      /// <returns></returns>
      public bool send(string wData, int rLen, ref string rData, ref string er, int timeOut = 200)
      {
          try
          {
              if (rs232 == null)
              {
                  er = "串口未打开";
                  return false;
              }
              comLock.AcquireWriterLock(-1);
              byte[] wByte = null;
              int wByteLen = 0;
              if (comDataType == 0)
              {
                  wByteLen = wData.Length / 2;
                  wByte = new byte[wByteLen];
                  for (int i = 0; i < wByteLen; i++)
                      wByte[i] = System.Convert.ToByte(wData.Substring(i * 2, 2), 16);
              }
              else
              {
                  wByteLen = System.Text.Encoding.Default.GetByteCount(wData);
                  wByte = new byte[wByteLen];
                  wByte = System.Text.Encoding.Default.GetBytes(wData);
              }
              rs232.DiscardInBuffer();
              rs232.DiscardOutBuffer();
              rs232.Write(wByte, 0, wByteLen);
              if (rLen == 0)
                  return true;
              int waitTime = Environment.TickCount;
              do
              {
                  System.Threading.Thread.Sleep(2);
              } while ((rs232.BytesToRead < rLen) && (Environment.TickCount - waitTime) < timeOut);
              if (rs232.BytesToRead == 0)
              {
                  er = "接收数据超时";
                  return false;
              }
              int rByteLen = rs232.BytesToRead;
              byte[] rByte = new byte[rByteLen];
              rs232.Read(rByte, 0, rByteLen);
              if (comDataType == 0)
              {
                  for (int i = 0; i < rByteLen; i++)
                      rData += rByte[i].ToString("X2");
              }
              else
                  rData = System.Text.Encoding.Default.GetString(rByte);
              if (rByteLen != rLen)
              {
                  er = "接收数据长度错误:" + rData;
                  return false;
              }
              return true;
          }
          catch (Exception e)
          {
              er = e.ToString();
              return false;
          }
          finally
          {
              comLock.ReleaseWriterLock(); 
          }
      }
      public bool send(string wData, string rEOI, ref string rData, ref string er, int timeOut = 200)
      {
          try
          {
              if (rs232 == null)
              {
                  er = "串口未打开";
                  return false;
              }
              comLock.AcquireWriterLock(-1);

              byte[] wByte = null;
              int wByteLen = 0;

              wByteLen = System.Text.Encoding.Default.GetByteCount(wData);
              wByte = new byte[wByteLen];
              wByte = System.Text.Encoding.Default.GetBytes(wData);

              rData = string.Empty;
              rs232.DiscardInBuffer();
              rs232.DiscardOutBuffer();
              rs232.Write(wByte, 0, wByteLen);

              if (rEOI == string.Empty)  //不接收数据
                  return true;

              int rLen = rEOI.Length;

              int waitTime = Environment.TickCount;
              do
              {
                  if (rs232.BytesToRead > 0)
                      rData += rs232.ReadExisting();
                  if (rData.Length > rLen)
                  {
                      if (rData.Substring(rData.Length - rLen, rLen) == rEOI)
                          break;
                  }
                  System.Threading.Thread.Sleep(2);
              } while ((Environment.TickCount - waitTime) < timeOut);

              if (rData.Length == 0 || rData.Substring(rData.Length - rLen, rLen) != rEOI)
              {
                  er = "接收数据超时:" + rData;
                  return false;
              }

              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
          finally
          {
              comLock.ReleaseWriterLock();
          }
      }
      #endregion

   }
}
