using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.Mon
{
   public class GJMon32:IGJMON
   {
      #region 构造函数
      public GJMon32()
      {
         com = new COM.CSerialPort();
         com.mComDataType=0; 
      }
      #endregion

      #region 字段
      private string name = "GJMon32_V4";
      private int idNo=0;
      private COM.ICom com;
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
      public COM.ICom mCom
      {
         set { com = value; }
         get { return com; }
      }
      #endregion

      #region 方法
      /// <summary>
      /// 打开串口
      /// </summary>
      /// <param name="comName"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool open(string comName, ref string er, string setting = "57600,n,8,1")
      {
         if (!com.open(comName, ref er,setting))
            return false;
         com.mComDataType = 0;
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
      /// 设置波特率
      /// </summary>
      /// <param name="setting"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool setBaud(ref string er, string setting = "57600,n,8,1")
      {
          return com.setBaud(ref er,setting);
      }
      /// <summary>
      /// 设置新地址
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetNewAddr(int wAddr, ref string er)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            wCmd = "00" + "00" + "00" + "05" + wAddr.ToString("X2");
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 7;
            if (!com.send(wCmd, rLen, ref rData, ref er))
               return false;
            if (!ToCheckSum(rData, ref rVal))
            {
               er = "检验和错误:" + rData;
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
      /// 读版本
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="version"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ReadVersion(int wAddr, ref string version, ref string er)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            wCmd = wAddr.ToString("X2") + "02" + "05" + "04";
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 8;
            if (!com.send(wCmd, rLen, ref rData, ref er))
               return false;
            if (!ToCheckSum(rData, ref rVal))
            {
               er = "检验和错误:" + rData;
               return false;
            }
            double ver = ((double)System.Convert.ToInt16(rVal, 16)) / 10;
            version = ver.ToString("0.0");
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
      }
      /// <summary>
      /// 设定工作模式
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wMode">
      /// 00：主控ACONOFF的工作模式 
      /// 01：从控ACONOFF的工作模式
      /// 02：ACStatus仅同步In+In-信号的工作模式
      /// 03：主控ACONOFF及可控制快充QC2.0模式
      /// </param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetWorkMode(int wAddr, int wMode, ref string er)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            if (wAddr == 0)
               wCmd = "00" + "00" + "02" + "05" + wMode.ToString("X2");
            else
               wCmd = wAddr.ToString("X2") + "01" + "01" + "05" + wMode.ToString("X2");
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 0;
            if (!com.send(wCmd, rLen, ref rData, ref er))
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
      /// 设置ON/OFF参数
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetOnOffPara(int wAddr, COnOffPara wPara, ref string er)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            wCmd += wPara.BIToTime.ToString("X4");
            for (int i = 0; i < 4; i++)
            {
               wCmd += wPara.wON[i].ToString("X4");
               wCmd += wPara.wOFF[i].ToString("X4");
               wCmd += wPara.wOnOff[i].ToString("X2");
            }
            wCmd = chkSOIEOI(wCmd);
            wCmd = wAddr.ToString("X2") + "01" + "05" + "1A" + wCmd;
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 0;
            if (!com.send(wCmd, rLen, ref rData, ref er))
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
      /// 启动老化测试
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetRunStart(int wAddr, CWriteRunPara wPara, ref string er)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            wCmd += wPara.runTolTime.ToString("X4");
            wCmd += wPara.secMinCnt.ToString("X2");
            wCmd += wPara.runTypeFlag.ToString("X2");
            wCmd += wPara.startFlag.ToString("X2");
            wCmd += wPara.onoff_RunTime.ToString("X4");
            wCmd += wPara.onoff_YXDH.ToString("X2");
            wCmd += wPara.onoff_Cnt.ToString("X2");
            wCmd += wPara.onoff_Flag.ToString("X2");
            wCmd = chkSOIEOI(wCmd);
            wCmd = wAddr.ToString("X2") + "01" + "06" + "0E" + wCmd;
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 0;
            if (!com.send(wCmd, rLen, ref rData, ref er))
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
      /// 强制结束
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ForceFinish(int wAddr, ref string er)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            wCmd = wAddr.ToString("X2") + "01" + "07" + "04";
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 0;
            if (!com.send(wCmd, rLen, ref rData, ref er))
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
      /// 控制暂停运行或继续运行
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wContinue">1:继续 0:暂停</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ControlPauseOrContinue(int wAddr,int wContinue,ref string er)
      {
       try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            if(wAddr==0)
                wCmd = wAddr.ToString("X2") + "00" + "04" + "05" + wContinue.ToString("X2");
            else 
               wCmd = wAddr.ToString("X2") + "01" + "0F" + "05"+ wContinue.ToString("X2");
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 0;
            if (!com.send(wCmd, rLen, ref rData, ref er))
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
      /// 启动AC ON/AC OFF
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool RemoteACOnOff(int wAddr, EOnOff wOnOff, ref string er)
      {

         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            wCmd = wAddr.ToString("X2") + "01" + "0A" + "05" + ((int)wOnOff).ToString("X2");
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 0;
            if (!com.send(wCmd, rLen, ref rData, ref er))
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
      /// 控制通道Relay ON
      /// </summary>
      /// <param name="wAddr">iAdrs=0：为广播命令，iRlyNo=(1~32), 当iRlyN=101时,All Relay OFF</param>
      /// <param name="wOnOff"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetRelayOn(int wAddr, int wRelayNo, ref string er)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            if (wAddr == 0)
               wCmd = wAddr.ToString("X2") + "00" + "06" + "05" + wRelayNo.ToString("X2");
            else
               wCmd = wAddr.ToString("X2") + "01" + "12" + "05" + wRelayNo.ToString("X2");
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 0;
            if (!com.send(wCmd, rLen, ref rData, ref er))
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
      /// 回读ON/OFF参数
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="rPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ReadOnOffPara(int wAddr, ref COnOffPara rPara, ref string er)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            wCmd = wAddr.ToString("X2") + "02" + "08" + "04";
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 1 + 1 + 2 + 1 + 22 + 1 + 1;
            if (!com.send(wCmd, rLen, ref rData, ref er))
               return false;
            if (!ToCheckSum(rData, ref rVal))
            {
               er = "检验和错误:" + rData;
               return false;
            }
            rPara.BIToTime = System.Convert.ToInt32(rVal.Substring(0, 4), 16);
            for (int i = 0; i < 4; i++)
            {
               rPara.wON[i] = System.Convert.ToInt32(rVal.Substring(4 + i * 10, 4), 16);
               rPara.wOFF[i] = System.Convert.ToInt32(rVal.Substring(4 + i * 10 + 4, 4), 16);
               rPara.wOnOff[i] = System.Convert.ToInt32(rVal.Substring(4 + i * 10 + 8, 2), 16);
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
      /// 从监控板读取电压及各个状态数据，电压数据基于同步AC ON/OFF信号
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="rVolt"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ReadVolt(int wAddr, ref CVoltVal rVolt, ref string er, ESynON synNo = ESynON.同步, ERunMode mode = ERunMode.主控ACONOFF的工作模式)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            //if (wAddr == 12)
            //    er = "";
        //       return true ;
            if (mode == ERunMode.从控ACONOFF的工作模式)
            {
               wCmd = "00" + "00" + "05" + "04";
               wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
               if (!com.send(wCmd, 0, ref rData, ref er))
                  return false;
               System.Threading.Thread.Sleep(500);
            }
            if (synNo == ESynON.异步)
               wCmd = wAddr.ToString("X2") + "02" + "01" + "04";
            else
               wCmd = wAddr.ToString("X2") + "02" + "02" + "04";
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 1 + 1 + 2 + 1 + 32 * 3 + 1 + 2 + 1 + 1 + 1;
            if (!com.send(wCmd, rLen, ref rData, ref er))
               return false;
            if (!ToCheckSum(rData, ref rVal))
            {
               er = "检验和错误:" + rData;
               return false;
            }
            string pol = rVal.Substring(32 * 4 - 1, 64);
            string status = rVal.Substring(rVal.Length - 8, 8);
            for (int i = 0; i < 32; i++)
            {
               int valTemp = System.Convert.ToInt32(rVal.Substring(i * 4, 4), 16);
               rVolt.volt[i] = ((double)valTemp) / 1000;
               if (pol.Substring(i * 2, 2) == "2D")
                  rVolt.volt[i] *= -1;
            }
            rVolt.onOffFlag = System.Convert.ToInt32(status.Substring(0, 2), 16);
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
      }
      /// <summary>
      /// 读取控制板测试信号数据
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="rPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ReadRunData(int wAddr, ref CReadRunPara rPara, ref string er, ERunMode mode = ERunMode.主控ACONOFF及可控制快充QC2_0模式)
      {
         try
         {
            string wCmd = string.Empty;
            string rData = string.Empty;
            string rVal = string.Empty;
            wCmd = wAddr.ToString("X2") + "02" + "09" + "04";
            wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
            int rLen = 1 + 1 + 2 + 1 + 14 + 1 + 1;
            if (!com.send(wCmd, rLen, ref rData, ref er))
               return false;
            if (!ToCheckSum(rData, ref rVal))
            {
               er = "检验和错误:" + rData;
               return false;
            }
            rPara.runTolTime = System.Convert.ToInt32(rVal.Substring(0,4),16);
            rPara.secMinCnt= System.Convert.ToInt32(rVal.Substring(4, 2),16);
            rPara.runTypeFlag = System.Convert.ToInt32(rVal.Substring(6, 2),16);
            rPara.startFlag = System.Convert.ToInt32(rVal.Substring(8, 2),16);
            rPara.biFinishFlag = System.Convert.ToInt32(rVal.Substring(10, 2),16);
            rPara.onoff_RunTime = System.Convert.ToInt32(rVal.Substring(12, 4),16);
            rPara.onoff_YXDH = System.Convert.ToInt32(rVal.Substring(16, 2),16);
            rPara.onoff_Cnt  = System.Convert.ToInt32(rVal.Substring(18, 2), 16);
            rPara.onoff_Flag  = System.Convert.ToInt32(rVal.Substring(20, 2), 16);
            int sgnVal = System.Convert.ToInt32(rVal.Substring(22,4), 16); 
            rPara.s1=((sgnVal & 0x1)==0x1)?1:0;
            rPara.s2 = ((sgnVal & 0x2) == 0x2) ? 1 : 0;
            rPara.ac_Sync = ((sgnVal & 0x4) == 0x4) ? 1 : 0;
            rPara.ac_on = ((sgnVal & 0x8) == 0x8) ? 1 : 0;
            int QC_V0 = ((sgnVal & 0x10) == 0x10) ? 1 : 0;
            int QC_V1 = ((sgnVal & 0x20) == 0x20) ? 1 : 0;
            int QC_T0 = ((sgnVal & 0x40) == 0x40) ? 1 : 0;
            int QC_T1 = ((sgnVal & 0x80) == 0x80) ? 1 : 0;
            rPara.QC_TYPE = QC_T1 * 2 + QC_T0;
            rPara.QC_VOLT = QC_V1 * 2 + QC_V0;           
            for (int i = 0; i < 8; i++)
               rPara.x[i]= ((sgnVal & (1<<(i+7))) == (1<<(i+7))) ? 1 : 0;
            rPara.x[8] = ((sgnVal & 0x80) == 0x80) ? 1 : 0;
           
             rPara.Y[1] = rPara.x[2];
            rPara.Y[2] = rPara.x[4];
            rPara.Y[3] = rPara.x[5];
            rPara.Y[4] = rPara.x[6];
            rPara.Y[5] = rPara.x[7];
            rPara.Y[6] = rPara.x[8];

            int errCode = System.Convert.ToInt32(rVal.Substring(rVal.Length - 2, 2), 16);
            if (errCode > 8)
                rPara.errCode = EErrCode.有负载回路不良;
            else
                rPara.errCode = (EErrCode)errCode;

            if (rPara.Y[1] == 0 && rPara.Y[2] == 0 && rPara.Y[3] == 1 &&
                rPara.Y[4] == 0 && rPara.Y[5] == 0 && rPara.Y[6] == 1)
                rPara.QC_Y_VOLT = 0;  //+5V 
            else if (rPara.Y[1] == 1 && rPara.Y[2] == 0 && rPara.Y[3] == 0 &&
                     rPara.Y[4] == 1 && rPara.Y[5] == 0 && rPara.Y[6] == 0)
                rPara.QC_Y_VOLT = 1; //+7V 
            else if (rPara.Y[1] == 0 && rPara.Y[2] == 0 && rPara.Y[3] == 0 &&
                     rPara.Y[4] == 0 && rPara.Y[5] == 0 && rPara.Y[6] == 0)
                rPara.QC_Y_VOLT = 2; //+9V 
            else if (rPara.Y[1] == 1 && rPara.Y[2] == 1 && rPara.Y[3] == 0 &&
                 rPara.Y[4] == 1 && rPara.Y[5] == 1 && rPara.Y[6] == 0)
                rPara.QC_Y_VOLT = 3; //+12V 
            else
                rPara.QC_Y_VOLT = -1;

            //if (mode == ERunMode.主控ACONOFF及可控制快充QC2_0模式)
            //{
            //    if (rPara.errCode == EErrCode.正常 && rPara.startFlag == 3 && rPara.ac_on==1)
            //    {
            //        if (rPara.QC_Y_VOLT!=-1 && rPara.QC_VOLT != rPara.QC_Y_VOLT)
            //            rPara.errCode = EErrCode.快充电压与Y输出异常;  
            //    }                
            //}

            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
      }
      /// <summary>
      /// 发扫描命令
      /// </summary>
      public void SetScanAll()
      {
         string wCmd = string.Empty;
         string rData = string.Empty;
         string rVal = string.Empty;
         string er = string.Empty;
         wCmd = "00" + "00" + "05" + "04";
         wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
         if (!com.send(wCmd, 0, ref rData, ref er))
            return;
      }
      /// <summary>
      /// 设定快充参数
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="rPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetGJM_RunQC_Para(int wAddr,COnOffPara wPara, ref string er)
      {
          try
          {
              string wCmd = string.Empty;
              string rData = string.Empty;
              string rVal = string.Empty;
              wCmd += wPara.wQCType.ToString("X2");
              for (int i = 0; i < 4; i++)
                  wCmd += wPara.wQCVolt[i].ToString("X2");
              wCmd = chkSOIEOI(wCmd);
              wCmd = wAddr.ToString("X2") + "01" + "15" + "09" + wCmd;
              wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
              int rLen = 0;
              if (!com.send(wCmd, rLen, ref rData, ref er))
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
      /// 
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ReadGJM_RunQC_Para(int wAddr,ref COnOffPara rPara, ref string er)
      {
          try
          {
              string wCmd = string.Empty;
              string rData = string.Empty;
              string rVal = string.Empty;
              wCmd = wAddr.ToString("X2") + "02" + "0E" + "04";
              wCmd = SOI + wCmd + CalCheckSum(wCmd) + EOI;
              int rLen = 1 + 1 + 2 + 1 + 5 + 1 + 1;
              if (!com.send(wCmd, rLen, ref rData, ref er))
                  return false;
              if (!ToCheckSum(rData, ref rVal))
              {
                  er = "检验和错误:" + rData;
                  return false;
              }
              rPara.wQCType = System.Convert.ToInt32(rVal.Substring(0, 2), 16);
              for (int i = 0; i < 4; i++)              
                  rPara.wQCVolt[i] = System.Convert.ToInt32(rVal.Substring(2 + i * 2, 2), 16);
              return true;
          }
          catch (Exception e)
          {
              er = e.ToString();
              return false;
          }
      }
      
      #endregion
    
      #region 控制板协议
      private const string SOI = "EE";
      private const string EOI = "EF";
      private const string ROI = "ED";
      /// <summary>
      /// 计算检验和
      /// </summary>
      /// <param name="wCmd"></param>
      /// <returns></returns>
      private string CalCheckSum(string wCmd)
      {
         int sum = 0;
         for (int i = 0; i < wCmd.Length / 2; i++)
         {
            sum += System.Convert.ToInt16(wCmd.Substring(i * 2, 2), 16);
         }
         sum = sum % 0x100;
         string chkSum = sum.ToString("X2");
         if (chkSum == SOI || chkSum == EOI)
            chkSum = ROI;
         return chkSum;
      }
      /// <summary>
      /// 检查检验和
      /// </summary>
      /// <param name="wCmd"></param>
      /// <param name="rVal">有效数据</param>
      /// <returns></returns>
      private bool ToCheckSum(string wCmd, ref string rVal)
      {
         int s1 = wCmd.IndexOf(SOI);
         int s2 = wCmd.LastIndexOf(EOI);
         if (s2 == 0 || s1 >= s2 || s2 - s1 < 12)
            return false;
         wCmd = wCmd.Substring(s1, s2 - s1 + 2);
         int sum = 0;
         for (int i = 1; i <= (wCmd.Length - 6) / 2; i++)
         {
            sum += System.Convert.ToInt16(wCmd.Substring(i * 2, 2), 16);
         }
         sum = sum % 256;
         string calSum = sum.ToString("X2");
         if (calSum == SOI || calSum == EOI)
            calSum = ROI;
         string getSum = wCmd.Substring(wCmd.Length - 4, 2);
         if (calSum != getSum)
            return false;
         rVal = wCmd.Substring(10, wCmd.Length - 14);
         return true;
      }
      /// <summary>
      /// 检查数据是否为桢头和桢尾
      /// </summary>
      /// <param name="wCmd"></param>
      /// <returns></returns>
      private string chkSOIEOI(string wCmd)
      {
         string rCmd = string.Empty;
         int len = wCmd.Length / 2;
         for (int i = 0; i < len; i++)
         {
            string temp = wCmd.Substring(i * 2, 2);
            if (temp == SOI || temp == EOI)
               rCmd += ROI;
            else
               rCmd += temp;
         }
         return rCmd;
      }
      #endregion
   }
}
