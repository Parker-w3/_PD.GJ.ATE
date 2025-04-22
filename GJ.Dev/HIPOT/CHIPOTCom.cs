using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.HIPOT
{
    #region 枚举

    public enum EHipotType
    { 
     Chroma19020,
     Chroma1907x,
     Chroma19020_4,
     HYESA140,   
     Chroma_PLCCOM
    }

    public enum EHPStatus
    { 
     RUNNING,
     STOPPED,
     ABORT
    }
    #endregion

    public class CHIPOTCom
    {
        #region 字段
        private IHIPOT hipot = null;
        #endregion

        #region 构造函数
        public CHIPOTCom(EHipotType hipotType = EHipotType.Chroma19020)
        {
            switch (hipotType )
            {
                case EHipotType.Chroma19020:
                    hipot = new CChroma19020(); 
                    hipot.mCom= new COM.CSerialPort(); 
                    break;
                case EHipotType.Chroma1907x:
                    break;

                case EHipotType .HYESA140:
                     hipot = new HYESA140(); 
                     hipot.mCom= new COM.CSerialPort(); 
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comName"></param>
        /// <param name="er"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool open(string comName, ref string er, string setting = "9600,n,8,1")
        {
            return hipot.open(comName, ref er, setting);
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void close()
        {
            hipot.close();
        }
        /// <summary>
        /// 初始化设备
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool init(ref string er)
        {
            return hipot.init(ref er);
        }
        /// <summary>
        /// 导入仪器程序
        /// </summary>
        /// <param name="proName">程序名称(不分大小写)</param>
        /// <returns></returns>
        public bool importProgram(string proName,ref string er)
        {
            return hipot.importProgram(proName, ref er);
        }
        /// <summary>
        /// 读测试步骤
        /// </summary>
        /// <param name="stepName"></param>
        /// <param name="er"></param>
        /// <param name="chan"></param>
        /// <returns></returns>
        public bool readStepName(ref List<CHPSetting.EStepName> stepName, ref string er, int chan = 1)
        {
            return hipot.readStepName(ref stepName, ref er, chan); 
        }
        /// <summary>
        /// 读取步骤设置值
        /// </summary>
        /// <param name="stepNo"></param>
        /// <param name="rStepVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readStepSetting(int stepNo, ref CHPSetting.EStepName stepName, ref List<double> stepVal, ref string er)
        {
            return hipot.readStepSetting(stepNo, ref stepName,ref stepVal, ref er);
        }
        /// <summary>
        /// 设置高压通道
        /// </summary>
        /// <param name="chanList"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setChanEnable(List<int> chanList,ref string er)
        {
          return hipot.setChanEnable(chanList,ref er); 
        }
        public bool setTestPara(List<CHPSetting.CStep> step, ref string er, string proName, bool saveToDev, ref string sendreaddata)
        {
            return hipot.setTestPara(step, ref er,proName,saveToDev, ref  sendreaddata); 
        }
        /// <summary>
        /// 启动
        /// </summary>
        public bool start(ref string er, ref string sendreaddata)
        {
           return hipot.start(ref er, ref  sendreaddata); 
        }
        /// <summary>
        /// 停止
        /// </summary>
        public bool stop(ref string er, ref string sendreaddata)
        {
            return hipot.stop(ref er, ref  sendreaddata); 
        }
        /// <summary>
        /// 读状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readStatus(ref EHPStatus status, ref string er, ref string sendreaddata)
        {
            return hipot.readStatus(ref status,ref er, ref  sendreaddata);  
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="chan"></param>
        /// <param name="chanResult"></param>
        /// <param name="stepResult"></param>
        /// <param name="stepCode"></param>
        /// <param name="stepMode"></param>
        /// <param name="stepVal"></param>
        /// <param name="stepUnit"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readResult(int chan, ref int chanResult,
                               ref List<int> stepResult, ref List<string> stepCode,
                               ref List<CHPSetting.EStepName> stepMode, ref List<string> stepVal,
                                ref List<string> stepUnit, ref string er, ref List<string> HpVolt, ref string sendreaddata)
        {
            return hipot.readResult(chan, ref chanResult,
                                    ref stepResult, ref stepCode,
                                    ref stepMode, ref stepVal,
                                    ref stepUnit,ref er,ref  HpVolt, ref  sendreaddata);    
        }
        #endregion

    }
}
