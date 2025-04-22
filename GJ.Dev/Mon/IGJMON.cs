using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.Mon
{
   public interface IGJMON
   {
      #region 属性
      string mName
      { set; get; }
      int mIdNo
      { set; get; }
      COM.ICom mCom
      { set; }
      #endregion

      #region 方法
      /// <summary>
      /// 打开串口
      /// </summary>
      /// <param name="comName"></param>
      /// <param name="setting"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool open(string comName, ref string er, string setting);
      /// <summary>
      /// 关闭串口
      /// </summary>
      /// <returns></returns>
      void close();
      /// <summary>
      /// 设置波特率
      /// </summary>
      /// <param name="setting"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool setBaud(ref string er, string setting);
      /// <summary>
      /// 设置地址
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool SetNewAddr(int wAddr, ref string er);
      /// <summary>
      /// 读控制板版本
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="version"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool ReadVersion(int wAddr, ref string version, ref string er);
      /// <summary>
      /// 设定工作模式
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wMode"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool SetWorkMode(int wAddr, int wMode, ref string er);
      /// <summary>
      /// 设置ON/OFF参数
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool SetOnOffPara(int wAddr, COnOffPara wPara, ref string er);
      /// <summary>
      /// 启动老化测试
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool SetRunStart(int wAddr, CWriteRunPara wPara, ref string er);
      /// <summary>
      /// 强制结束
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool ForceFinish(int wAddr, ref string er);
      /// <summary>
      /// 启动AC ON/AC OFF
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wOnOff"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool RemoteACOnOff(int wAddr, EOnOff wOnOff, ref string er);
      /// <summary>
      /// 控制通道Relay ON
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="wRelayNo"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool SetRelayOn(int wAddr, int wRelayNo, ref string er);
      /// <summary>
      /// 回读ON/OFF参数
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="rPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool ReadOnOffPara(int wAddr, ref COnOffPara rPara, ref string er);
      /// <summary>
      /// 从监控板读取电压及各个状态数据，电压数据基于同步AC ON/OFF信号
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="rVolt"></param>
      /// <param name="er"></param>
      /// <param name="synNo"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      bool ReadVolt(int wAddr, ref CVoltVal rVolt, ref string er, ESynON synNo = ESynON.同步, ERunMode mode = ERunMode.主控ACONOFF的工作模式);
      /// <summary>
      /// 读取控制板测试信号数据
      /// </summary>
      /// <param name="wAddr"></param>
      /// <param name="rPara"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool ReadRunData(int wAddr, ref CReadRunPara rPara, ref string er, ERunMode mode = ERunMode.主控ACONOFF及可控制快充QC2_0模式);
      /// <summary>
      /// 发扫描命令
      /// </summary>
      void SetScanAll();

      bool SetGJM_RunQC_Para(int wAddr, COnOffPara wPara, ref string er);
      
      bool ReadGJM_RunQC_Para(int wAddr, ref COnOffPara rPara, ref string er);

      bool ControlPauseOrContinue(int wAddr, int wContinue, ref string er);

      #endregion
   }
}
