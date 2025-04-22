using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.Mon
{
   #region 枚举
   /// <summary>
   /// ON/OFF状态
   /// </summary>
   public enum EOnOff
   {
      ON,
      OFF
   }
   /// <summary>
   /// AC同步模式
   /// </summary>
   public enum ESynON
   {
      同步,
      异步
   }
   /// <summary>
   /// 错误代码
   /// </summary>
   public enum EErrCode
   {
      正常,
      治具到位信号异常,
      气缸升到位信号异常,
      AC同步信号异常,
      治具1AC不通,
      治具2AC不通,
      治具位12AC都不通,
      快充电压与Y输出异常,
      有负载回路不良      
   }
   /// <summary>
   /// 运行模式
   /// </summary>
   public enum ERunMode
   {
       主控ACONOFF的工作模式,
       从控ACONOFF的工作模式,
       ACStatus仅同步In信号的工作模式,
       主控ACONOFF及可控制快充QC2_0模式
   }
   #endregion

   #region 内部类
   /// <summary>
   /// 设置控制板运行参数
   /// </summary>
   public class CWriteRunPara
   {
      /// <summary>
      /// 老化总时间计时(Unit:Min)
      /// </summary>
      public int runTolTime;
      /// <summary>
      /// 60Sec/Min的计时
      /// </summary>
      public int secMinCnt;
      /// <summary>
      /// 1->只有治具1进行老化，2->只有治具2进行老化，3->治具1,2同时进行老化 4->空治具老化
      /// </summary>
      public int runTypeFlag;
      /// <summary>
      /// 0->无启动老化请求，1->PC请求启动老化,2->自检 3-〉老化中 4-〉老化结束
      /// </summary>
      public int startFlag;
      /// <summary>
      /// ON/OFF运行段的计时 (Unit:Sec)
      /// </summary>
      public int onoff_RunTime;
      /// <summary>
      /// OnOff的运行段号
      /// </summary>
      public int onoff_YXDH;
      /// <summary>
      /// OnOff的运行次数
      /// </summary>
      public int onoff_Cnt;
      /// <summary>
      /// OnOff_Flag=ON,OnOff_Flag=Off
      /// </summary>
      public int onoff_Flag;
   }
   /// <summary>
   /// 回读控制板运行参数
   /// </summary>
   public class CReadRunPara
   {
      public int runTolTime;     //老化总时间计时(Unit:Min)
      public int secMinCnt;      //60Sec/Min的计时
      public int runTypeFlag;    //1->只有治具1进行老化，2->只有治具2进行老化，3->治具1,2同时进行老化 4->空治具老化 
      public int startFlag;      //0->无启动老化请求，1->PC请求启动老化,2->自检 3-〉老化中 4-〉老化结束
      public int biFinishFlag;   //0->完成老化，1->老化未完成  [掉电保存]
      public int onoff_RunTime;  //ON/OFF运行段的计时 (Unit:Sec)
      public int onoff_YXDH;    //OnOff的运行段号
      public int onoff_Cnt;     //OnOff的运行次数
      public int onoff_Flag;    //OnOff_Flag=ON,OnOff_Flag=Off
      public int s1;           //设定地址的指拨开关信号
      public int s2;           //切换手动/自动运行模式的指拨开关信号
      public int ac_Sync;      //主接触器辅助触点过来的同步信号
      public int ac_on;        //AC 输入ON
      public int[] x = new int[9];  //X0 - X8 输入信号
      public EErrCode errCode;      //0->无故障;1->治具到位信号异常;2->气缸升到位信号异常;3->AC同步信号异常
                                    // 4->治具1AC不通;5->治具2AC不通; 6->2治具位AC都不通;
                                    //7->气缸降到位信号异常;8->有负载回路不良
      /// <summary>
      /// 快充模式
      /// </summary>
      public int QC_TYPE = 0;
      /// <summary>
      /// 快充电压
      /// </summary>
      public int QC_VOLT = 0;
      /// <summary>
      /// 快充QC2.0控制D+/D-
      /// </summary>
      public int QC_Y_VOLT = 0;
      /// <summary>
      /// 快充控制模式
      /// </summary>
      public int[] Y = new int[9];  //Y0-Y8;
   }
   /// <summary>
   /// 设置控制板ON/OFF参数
   /// </summary>
   public class COnOffPara
   {
      /// <summary>
      /// Unit Min
      /// </summary>
      public int BIToTime;
      /// <summary>
      /// ON1,ON2,ON3,ON4设定值  (Unit:Sec)
      /// </summary> 
      public int[] wON = new int[4];
      /// <summary>
      /// OFF1,OFF2,OFF3,OFF4设定值  (Unit:Sec)
      /// </summary>
      public int[] wOFF = new int[4];
      /// <summary>
      /// OnOff1_Cycle,OnOff2_Cycle,OnOff3_Cycle,OnOff4_Cycle的设定值
      /// </summary>
      public int[] wOnOff = new int[4];

      /// <summary>
      /// 快充类型，0->QC2.0;1-QC3.0;2->MTK  Ver5.1后才有此功能 3->海思
      /// </summary>
      public int wQCType = 0;
      /// <summary>
      /// 4个ONOFF阶段对应的快充电压设定:0->5V;1->9V;2->12V;3->20V  ,Ver5.1后才有此功能
      /// </summary>
      public int[] wQCVolt = new int[4]; 

   }
   /// <summary>
   ///回读控制板32通道电压值
   /// </summary>
   public class CVoltVal
   {
      public double[] volt = new double[32];
      public int onOffFlag;
   }
   #endregion

   public class CGJMonCom
   {
      #region 字段
      private IGJMON com = null;
      #endregion

      #region 构造函数
      public CGJMonCom()
      {
         com = new GJMon32();
         com.mCom = new COM.CSerialPort(); 
      }
      #endregion

      /// <summary>
      /// 打开串口
      /// </summary>
      /// <param name="comName"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool open(string comName, ref string er, string setting = "57600,n,8,1")
      {
         return com.open(comName, ref er,setting);
 
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
      /// <param name="er"></param>
      /// <param name="setting"></param>
      /// <returns></returns>
      public bool setBaud(ref string er, string setting = "57600,n,8,1")
      {
          return com.setBaud(ref er, setting); 
      }
      /// <summary>
      /// 设置新地址
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetNewAddr(int wAddr, ref string er)
      {
         return com.SetNewAddr(wAddr, ref er);  
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
        return com.ReadVersion(wAddr, ref version, ref er); 
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
      /// 04: 主控快充模式-兼容海思模式
      /// </param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool SetWorkMode(int wAddr, int wMode, ref string er)
      {
         return com.SetWorkMode(wAddr, wMode, ref er); 
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
         return com.SetOnOffPara(wAddr, wPara, ref er);
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
         return com.SetRunStart(wAddr, wPara, ref er); 
      }
      /// <summary>
      /// 强制结束
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool ForceFinish(int wAddr, ref string er)
      {
         return com.ForceFinish(wAddr, ref er); 
      }
      /// <summary>
      /// 启动AC ON/AC OFF
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool RemoteACOnOff(int wAddr, EOnOff wOnOff, ref string er)
      {
         return com.RemoteACOnOff(wAddr, wOnOff, ref er); 
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
         return com.SetRelayOn(wAddr, wRelayNo, ref er); 
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
         return com.ReadOnOffPara(wAddr, ref rPara, ref er); 
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
         return com.ReadVolt(wAddr, ref rVolt, ref er, synNo, mode); 
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
         return com.ReadRunData(wAddr, ref rPara, ref er,mode); 
      }
      /// <summary>
      /// 发扫描命令
      /// </summary>
      public void SetScanAll()
      {
         com.SetScanAll(); 
      }
      public bool SetGJM_RunQC_Para(int wAddr, COnOffPara wPara, ref string er)
      {
          return com.SetGJM_RunQC_Para(wAddr, wPara, ref er); 
      }
      public bool ReadGJM_RunQC_Para(int wAddr, ref COnOffPara rPara, ref string er)
      {
          return com.ReadGJM_RunQC_Para(wAddr, ref rPara, ref er); 
      }
      public bool ControlPauseOrContinue(int wAddr, int wContinue, ref string er)
      {
          return com.ControlPauseOrContinue(wAddr, wContinue,ref er); 
      }
   }
}
