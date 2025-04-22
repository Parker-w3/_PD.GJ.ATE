using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.ERS
{
   public class GJ272_4:IERS 
   {
      #region 构造函数
      public GJ272_4()
      {
         com = new COM.CSerialPort();
         com.mComDataType=0; 
      }
      #endregion

      #region 字段
      private string name = "GJ272_4";
      private int idNo=0;
      private COM.ICom com;
      private const int CHNo = 4;
      #endregion

      #region 属性
      public string mName
      {
         get
         {
            return name;
         }
         set
         {
            name = value;
         }
      }
      public int mIdNo
      {
         get
         {
            return idNo;
         }
         set
         {
            idNo = value;
         }
      }
      public int mCHNo
      {
         get
         {
            return CHNo;
         }
      }
      public COM.ICom mCom
      {
         set { com = value; }
      }
      #endregion

      #region 方法
      public bool open(string comName, ref string er, string setting="9600,n,8,1")
      {
         if (com == null)
            return false;
         return com.open(comName, ref er, setting); 
      }
      public void close()
      {
         if (com == null)
            return;
         com.close();
      }
      public bool SetNewAddr(int wAddr, ref string er)
      {
          try
          {
              string cmd0 = "0F";
              string wData = wAddr.ToString("X2");
              wData = CalDataFromERS272Cmd(0, cmd0, wData);
              string rData = string.Empty;
              int rLen = 8;
              if (!SendCmdToERS272(wData, rLen, ref rData, ref er))
                  return false;
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false; 
          }       
      }
      public bool ReadVersion(int wAddr, ref string version, ref string er)
      {
          try
          {
              string cmd0 = "11";
              string wData = wAddr.ToString("X2");
              wData = CalDataFromERS272Cmd(wAddr, cmd0, wData);
              string rData = string.Empty;
              int rLen = 9;
              if (!SendCmdToERS272(wData, rLen, ref rData, ref er))
                  return false;
              string temp = string.Empty;
              string rVal = string.Empty;
              if (!checkErrorCode(rData, ref rVal, ref temp))
                  return false;
              version = System.Convert.ToInt32(rVal, 16).ToString();    
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false; 
          }        
      }
      public bool SetNewLoad(int wAddr, CERS_SetLoad loadPara, ref string er, bool saveEPROM = true)
      {
          try
          {
              bool setOK = true;
              string cmd0 = string.Empty;
              string wData = string.Empty;
              string rData = string.Empty;
              for (int i = 0; i < CHNo; i++)
              {
                  if (saveEPROM)
                      cmd0 = (i + 3).ToString("X2");
                  else
                      cmd0 = (i + 16).ToString("X2");
                  wData = ((int)(loadPara.loadVal[i] * 1024)).ToString("X4");
                  wData = CalDataFromERS272Cmd(wAddr, cmd0, wData);
                  int rLen = 8;
                  if (!SendCmdToERS272(wData, rLen, ref rData, ref er))
                  {
                      setOK = false;
                      continue;
                  }                     
                  string temp = string.Empty;
                  string rVal = string.Empty; 
                  if (!checkErrorCode(rData,ref rVal, ref temp))
                  {
                      er += temp;
                      setOK = false;
                     continue;
                  }
              }
              return setOK;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false; 
          }        
      }
      /// <summary>
      /// 设置负载
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="CH">0-3</param>
      /// <param name="loadVal"></param>
      /// <param name="er"></param>
      /// <param name="saveEPROM"></param>
      /// <returns></returns>
      public bool SetNewLoad(int wAddr, int CH, double loadVal, ref string er, bool saveEPROM = true)
      {
          try
          {
              string cmd0 = string.Empty;
              string wData = string.Empty;
              string rData = string.Empty;

              if (saveEPROM)
                cmd0 = (CH + 3).ToString("X2");
              else
                cmd0 = (CH + 16).ToString("X2");
               wData = ((int)(loadVal * 1024)).ToString("X4");
               wData = CalDataFromERS272Cmd(wAddr, cmd0, wData);
               int rLen = 7;
               if (!SendCmdToERS272(wData, rLen, ref rData, ref er))                
                  return false;
                string temp = string.Empty;
                string rVal = string.Empty;
                if (!checkErrorCode(rData, ref rVal, ref temp))
                  return false;              
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }        
      }
      public bool ReadLoadSet(int wAddr, CERS_ReadLoadSet loadVal, ref string er)
      {
          try
          {
              string cmd0 = "12";
              string wData = wAddr.ToString("X2");
              wData = CalDataFromERS272Cmd(wAddr, cmd0, wData);
              string rData = string.Empty;
              int rLen = 48;
              if (!SendCmdToERS272(wData, rLen, ref rData, ref er))
                  return false;
              string rVal = string.Empty;
              if (!checkErrorCode(rData, ref rVal, ref er))
                  return false;
              for (int i = 0; i < 4; i++)
              {
                  int valTemp = System.Convert.ToInt16(rVal.Substring(i * 4, 4), 16);
                  loadVal.loadVal[i] = ((double)valTemp) / 1024;
              }
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false; 
          }
      }
      public bool ReadData(int wAddr, CERS_ReadData dataVal, ref string er)
      {
          try
          {
              string cmd0 = "12";
              string wData = wAddr.ToString("X2");
              wData = CalDataFromERS272Cmd(wAddr, cmd0, wData);
              string rData = string.Empty;
              int rLen = 48;
              if (!SendCmdToERS272(wData, rLen, ref rData, ref er))
                  return false;
              string rVal=string.Empty;
              if (!checkErrorCode(rData,ref rVal, ref er))
                  return false;
              int valTemp = System.Convert.ToInt16(rVal.Substring(8, 4), 16);
              dataVal.cur[0] = ((double)valTemp) / 1024;
              valTemp = System.Convert.ToInt16(rVal.Substring(12, 4), 16);
              dataVal.cur[1] = ((double)valTemp) / 1024;
              valTemp = System.Convert.ToInt16(rVal.Substring(64, 4), 16);
              dataVal.cur[2] = ((double)valTemp) / 1024;
              valTemp = System.Convert.ToInt16(rVal.Substring(68, 4), 16);
              dataVal.cur[3] = ((double)valTemp) / 1024;              
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
      }
      /// <summary>
      /// 设置快充MTK电压上升或下降
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="CH">0,1,2,3</param>
      /// <param name="wRaise"></param>
      /// <returns></returns>
      public bool SetQCMTK(int wAddr, int CH, bool wRaise,ref string er)
      {
          try
          {
              string cmd0 = string.Empty;
              string wData = string.Empty;
              string rData = string.Empty;
              cmd0 = "0C";
              wData = CH.ToString("X2");
              if(wRaise) 
                wData +="01";
              else
                wData +="02";
              wData = CalDataFromERS272Cmd(wAddr, cmd0, wData);
              int rLen = 7;
              if (!SendCmdToERS272(wData, rLen, ref rData, ref er))
                  return false;
              string temp = string.Empty;
              string rVal = string.Empty;
              if (!checkErrorCode(rData, ref rVal, ref temp))
                  return false;
              return true;
          }
          catch (Exception ex)
          {
              er = ex.ToString();
              return false;
          }
      }
      #endregion

      #region 协议
      /* 
       * 发送:桢头(7E)+地址+命令01+命令02+长度+数据+检验和+桢尾(7F)
       * 应答:桢头(7E)+地址+长度+数据+检验和+桢尾(7F)         
      */
      private string SOI = "7E";
      private string EOI = "7F";
      private string ROI = "7D";
      private bool SendCmdToERS272(string wData, int rLen, ref string rData, ref string er, int wTimeOut = 500)
      {
         string recvData = string.Empty;
         if (!com.send(wData, rLen, ref recvData, ref er, wTimeOut))
            return false;
         rData = recvData;
         return true;
      }
      private string CalDataFromERS272Cmd(int wAddr, string wCmd, string wData)
      {
         string cmd = string.Empty;
         int len = 3 + wData.Length / 2;
         string chkData = string.Empty;
         for (int i = 0; i < wData.Length / 2; i++)
         {
            if (wData.Substring(i * 2, 2) == SOI || wData.Substring(i * 2, 2) == EOI)
               chkData += ROI;
            else
               chkData += wData.Substring(i * 2, 2);
         }
         cmd = wAddr.ToString("X2") + wCmd + len.ToString("X2") + chkData;
         cmd = SOI + cmd + CalCheckSum(cmd) + EOI;
         return cmd;
      }
      private bool ERS_CheckSum(string wData, ref string er)
      {
         if (wData.Substring(0, 2) != SOI)
         {
            er = "数据桢头错误:" + wData;
            return false;
         }
         if (wData.Substring(wData.Length - 2, 2) != EOI)
         {
            er = "数据桢尾错误:" + wData;
            return false;
         }
         if (wData.Length / 2 < 6)
         {
            er = "数据长度小于6:" + wData;
            return false;
         }
         int rLen = System.Convert.ToInt16(wData.Substring(6, 2), 16);
         if ((wData.Length / 2) != (rLen + 4))
         {
            er = "数据长度错误:" + wData;
            return false;
         }
         string chkStr = wData.Substring(2, wData.Length - 8);
         string chkSum = wData.Substring(wData.Length - 6, 4);
         if (chkSum != CalCheckSum(chkStr))
         {
            er = "数据CheckSum错误:" + wData;
            return false;
         }
         return true;
      }
      ///// <summary>
      ///// 检验和-(地址+命令01+命令02+长度+数据)%256
      ///// </summary>
      ///// <param name="wData"></param>
      ///// <returns></returns>
      //private string CalCheckSum(string wData)
      //{
      //   int sum = 0;
      //   for (int i = 0; i < wData.Length / 2; i++)
      //      sum += System.Convert.ToInt32(wData.Substring(i * 2, 2), 16);
      //   sum = sum % 0x10000;
      //   string checkSum = sum.ToString("X4");
      //   if (checkSum.Substring(0, 2) == SOI || checkSum.Substring(0, 2) == EOI)
      //      checkSum = ROI + checkSum.Substring(2, 2);
      //   if (checkSum.Substring(2, 2) == SOI || checkSum.Substring(2, 2) == EOI)
      //      checkSum = checkSum.Substring(0, 2) + ROI;
      //   return checkSum;
      //}
      /// <summary>
      /// 检验和-(地址+命令01+命令02+长度+数据)%256
      /// </summary>
      /// <param name="wData"></param>
      /// <returns></returns>
      private string CalCheckSum(string wData)
      {
          int sum = 0;
          for (int i = 0; i < wData.Length / 2; i++)
              sum += System.Convert.ToInt16(wData.Substring(i * 2, 2), 16);
          sum = sum % 0x10000;
          string checkSum = sum.ToString("X4");
          string chk_H = checkSum.Substring(0, 2);
          string chk_L = checkSum.Substring(2, 2);
          checkSum = chk_H.Replace(SOI, ROI) + chk_L.Replace(SOI, ROI);
          return checkSum;
      }

      private bool checkErrorCode(string wData,ref string rVal, ref string er)
      {
         er = string.Empty;
         if (!ERS_CheckSum(wData, ref er))
             return false;
         string chkFlag = wData.Substring(4,2);
         switch (chkFlag)
         {
            case "F0":
                 rVal = wData.Substring(8, wData.Length - 14);
                 return true;
            case "F1":
               er = "CHKSUM错误";  
               break;
            case "F2":
               er = "LENGTH错误"; 
               break;
            case "F3":
               er ="CID无效"; 
               break;
            case "F4":
               er = "无效数据"; 
               break;
            case "F5":
               er = "模块地址太大"; 
               break;
            case "F6":
               er = "DSP变量个数设置错误";
               break;
            default:
               er = "异常错误";
               break;
         }
         return false;
      }
      #endregion
      
   }
}
