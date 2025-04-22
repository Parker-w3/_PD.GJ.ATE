using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.HIPOT
{
    public interface IHIPOT
    {   

     #region 属性
     /// <summary>
     /// 仪器名称
     /// </summary>
     string mName
     { set; get; }
     /// <summary>
     ///仪器Id 
     /// </summary>
     int mIdNo
     { set; get; }
     /// <summary>
     /// 仪器通道
     /// </summary>
     int mCH
     { set; get; }
     /// <summary>
     /// 通信接口
     /// </summary>
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
     /// 初始化设备
     /// </summary>
     /// <returns></returns>
     bool init(ref string er);
     /// <summary>
     /// 设置测试步骤
     /// </summary>
     /// <param name="step"></param>
     /// <param name="er"></param>
     /// <returns></returns>
     bool setTestPara(List<CHPSetting.CStep> step, ref string er, string proName, bool saveToDev, ref string sendreaddata);
     /// <summary>
     /// 启动测试
     /// </summary>
     bool start(ref string er, ref string sendreaddata);
     /// <summary>
     /// 停止测试
     /// </summary>
     bool stop(ref string er, ref string sendreaddata);
     /// <summary>
     /// 导入高压编辑程序
     /// </summary>
     /// <param name="proName"></param>
     /// <returns></returns>
     bool importProgram(string proName, ref string er);
    /// <summary>
    /// 读测试步骤
    /// </summary>
    /// <param name="stepName"></param>
    /// <param name="er"></param>
    /// <param name="chan"></param>
    /// <returns></returns>
     bool readStepName(ref List<CHPSetting.EStepName> stepName, ref string er, int chan = 1);
    /// <summary>
    /// 设置高压通道
    /// </summary>
    /// <param name="chanList"></param>
    /// <param name="er"></param>
    /// <returns></returns>
     bool  setChanEnable(List<int> chanList,ref string er);
     /// <summary>
     /// 读设置值
     /// </summary>
     /// <param name="stepNo"></param>
     /// <param name="er"></param>
     /// <returns></returns>
     bool readStepSetting(int stepNo, ref CHPSetting.EStepName stepName, ref List<double> stepVal, ref string er);
     /// <summary>
     /// 读取状态
     /// </summary>
     /// <returns></returns>
     bool readStatus(ref EHPStatus status, ref string er, ref string sendreaddata);
     /// <summary>
     /// 获取测试结果
     /// </summary>
     /// <param name="result"></param>
     /// <param name="stepVal"></param>
     /// <returns></returns>
     bool readResult(int chan, ref int chanResult,
                     ref List<int> stepResult, ref List<string> stepCode,
                     ref List<CHPSetting.EStepName> stepMode, ref List<string> stepVal,
                     ref List<string> stepUnit, ref string er, ref List<string> HpVolt, ref string sendreaddata);

     #endregion

    }
}
