using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ;

namespace GJ.Dev.HIPOT
{
    [Author("kp.lin", "V1.0.0", "2016/03/12")]
    public class CChroma19020 : IHIPOT
    {
        public CChroma19020()
        {
            c_HPCode = new Dictionary<int, string>();
            c_HPCode.Add(112, "STOP");
            c_HPCode.Add(115, "TESTING");
            c_HPCode.Add(116, "PASS");
            c_HPCode.Add(33, "HIGH FAIL");
            c_HPCode.Add(49, "HIGH FAIL");
            c_HPCode.Add(65, "HIGH FAIL");
            c_HPCode.Add(34, "LOW FAIL");
            c_HPCode.Add(50, "LOW FAIL");
            c_HPCode.Add(66, "LOW FAIL");
            c_HPCode.Add(35, "ARC FAIL");
            c_HPCode.Add(51, "ARC FAIL");
            c_HPCode.Add(36, "OCP");
            c_HPCode.Add(52, "OCP");
            c_HPCode.Add(68, "OCP");
            c_HPCode.Add(100, "OCP");
            c_HPCode.Add(97, "SHORT FAIL");
            c_HPCode.Add(98, "OPEN FAIL");
            com = new COM.CSerialPort();
        }

        #region 字段
        private string name = "Chroma19020-4";
        private int idNo = 0;
        private int chanNum = 4;
        private COM.ICom com;
        private Dictionary<int, string> c_HPCode = null;
        #endregion

        #region 属性
        public string mName
        {
            get{return name;}
            set{name = value;}
        }
        public int mIdNo
        {
            get{return idNo;}
            set{idNo = value;}
        }
        public int mCH
        {
            get { return chanNum; }
            set { chanNum = value; }
        }
        public COM.ICom mCom
        {
          set{ com = value; }
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
        public bool open(string comName, ref string er, string setting="9600,n,8,1")
        {
            if (com == null)
                return false;
            com.mComDataType = 1;
            return com.open(comName, ref er, setting);
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void close()
        {
            com.close();
        }
        /// <summary>
        /// 初始化设备
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool init(ref string er)
        {
            try
            {
                string devName = string.Empty;

                if (!readIDN(ref devName, ref er))
                    return false;
                if (devName != name)
                {
                    er = "高压仪器[" + name + "]型号错误:" + devName;
                    return false;
                }
             
                if (!remote(ref er))
                    return false;

                if (!writeCmd("*CLS",ref er))  //清除错误
                    return false;

                if (!writeCmd("SYST:TCON:FAIL:OPER CONT", ref er)) //不良继续测试
                    return false;  
 
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true; 
            }
        }
        /// <summary>
        /// 导入程序
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool importProgram(string proName, ref string er)
        {
            try
            {

                string cmd = "MEM:STAT:DEF? \"" + proName + "\"";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                {
                    er = "程序[" + proName + "]不存在.";
                    return false;
                }

                int proNo = System.Convert.ToInt16(rData);

                if(!writeCmd("*RCL "+proNo,ref er))
                    return false;

                return true; 
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true; 
            }
        }
        /// <summary>
        /// 获取测试步骤
        /// </summary>
        /// <param name="stepName"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readStepName(ref List<CHPSetting.EStepName> stepName, ref string er,int chan=1)
        {
            try
            {
                  //获取步骤模式
                string cmd = "SAF:CHAN" + chan.ToString("D3") + ":RES:ALL:MODE?";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;

                string[] stepModeList = rData.Split(',');

                for (int i = 0; i < stepModeList.Length; i++)
                {
                    switch (stepModeList[i])
                    {
                        case "AC":
                            stepName.Add(CHPSetting.EStepName.AC);   
                            break;
                        case "DC":
                            stepName.Add(CHPSetting.EStepName.DC);  
                            break;
                        case "IR":
                            stepName.Add(CHPSetting.EStepName.IR);
                            break;
                        case "OSC":
                            stepName.Add(CHPSetting.EStepName.OSC);
                            break;
                        default:
                            stepName.Add(CHPSetting.EStepName.PA);
                            break;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true;
            }
        }
        /// <summary>
        /// 读取测试步骤设置值
        /// </summary>
        /// <param name="stepNo"></param>
        /// <param name="rStepVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readStepSetting(int stepNo, ref CHPSetting.EStepName stepName, ref List<double>stepVal, ref string er)
        {
            try
            {
                string cmd = "SAF:STEP" + stepNo.ToString() + ":SET?";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;

                string[] stepList = rData.Split(',');

                if (stepList.Length <4)
                {
                    er = "获取步骤数据错误:"+rData;
                    return false;
                }               
                switch (stepList[2])
                {
                    case "AC":                        
                        stepName=CHPSetting.EStepName.AC;
                        for (int i = 0; i < stepList.Length - 5; i++)
                            stepVal.Add(System.Convert.ToDouble(stepList[i + 3]));
                        if (stepVal.Count != CHPSetting.c_ACItem.Length)
                        {
                            er = "获取步骤数据错误:" + rData;
                            return false;
                        }
                        stepVal[0] = stepVal[0] / 1000;//VOLTAGE
                        stepVal[1] = stepVal[1]*1000;//HIGHT LIMIT
                        stepVal[2] = stepVal[2]*1000;//LOW LIMIT
                        stepVal[3] = stepVal[3]*1000;//ARC LIMIT
                        stepVal[4] = stepVal[4];//RAMP TIME 
                        stepVal[5] = stepVal[5];//TEST TIME                        
                        stepVal[6] = stepVal[6];//FALL TIME 
                        break;
                    case "DC":
                       stepName= CHPSetting.EStepName.DC;
                       for (int i = 0; i < stepList.Length - 5; i++)
                           stepVal.Add(System.Convert.ToDouble(stepList[i + 3]));
                       if (stepVal.Count != CHPSetting.c_DCItem.Length)
                       {
                           er = "获取步骤数据错误:" + rData;
                           return false;
                       }
                        stepVal[0] = stepVal[0] / 1000;//VOLTAGE
                        stepVal[1] = stepVal[1]*1000;//HIGHT LIMIT
                        stepVal[2] = stepVal[2]*1000;//LOW LIMIT
                        stepVal[3] = stepVal[3]*1000;//ARC LIMIT
                        stepVal[4] = stepVal[4];//RAMP TIME 
                        stepVal[5] = stepVal[5];//DWELL TIME                        
                        stepVal[6] = stepVal[6];//TEST TIME 
                        stepVal[7] = stepVal[7]; //FALL TIME
                       break;
                    case "IR":
                       stepName=CHPSetting.EStepName.IR;
                       for (int i = 0; i < stepList.Length - 7; i++)
                           stepVal.Add(System.Convert.ToDouble(stepList[i + 3]));
                       if (stepVal.Count != CHPSetting.c_IRItem.Length)
                       {
                           er = "获取步骤数据错误:" + rData;
                           return false;
                       }
                       stepVal[0] = stepVal[0] / 1000;//VOLTAGE
                       stepVal[1] = stepVal[1]/1000000;//LOW LIMIT
                       stepVal[2] = stepVal[2]/1000000;//HIGHT LIMIT
                       stepVal[3] = stepVal[3];//RAMP TIME                    
                       stepVal[4] = stepVal[4];//TEST TIME 
                       stepVal[5] = stepVal[5]; //FALL TIME
                       break;
                    case "OSC":
                      stepName=CHPSetting.EStepName.OSC;
                      for (int i = 0; i < stepList.Length - 5; i++)
                          stepVal.Add(System.Convert.ToDouble(stepList[i + 3]));
                      if (stepVal.Count !=CHPSetting.c_OSCItem.Length)
                      {
                          er = "获取步骤数据错误:" + rData;
                          return false;
                      }
                      stepVal[0] = stepVal[0] * 100;     //OPEN
                      stepVal[1] = stepVal[1] * 100;     //SHORT
                      break;
                    default:
                      stepName=CHPSetting.EStepName.PA;
                      break;
                }               
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true;
            }
        }
        /// <summary>
        /// 设置高压通道
        /// </summary>
        /// <param name="chanList"></param>
        /// <returns></returns>
        public bool  setChanEnable(List<int> chanList,ref string er)
        {
            try
            {
                if (chanList.Count == 0)
                {
                    er = "无可设置通道";
                    return false;
                }                    

                string chanStr = string.Empty;

                for (int i = 0; i < chanList.Count; i++)
                {
                    if(i<chanList.Count-1)
                       chanStr += chanList[i].ToString("D3")+","; 
                    else
                        chanStr += chanList[i].ToString("D3"); 
                }

                string cmd = "SYST:TCON:CHAN (@" + chanStr+")";

               return writeCmd(cmd,ref er); 
            }
            catch (Exception ex)
            {
                er=ex.ToString(); 
                return false;
            }
        }
        /// <summary>
        /// 设置测试步骤
        /// </summary>
        /// <param name="step"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setTestPara(List<CHPSetting.CStep> step, ref string er, string proName, bool saveToDev, ref string sendreaddata)
        {
            try
            {
                //删除原有参数设置

                string cmd = "SAF:SNUMB?";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;

                if (!writeCmd("*CLS", ref er))  //清除错误
                    return false; 

                int stepNum = System.Convert.ToInt32(rData);

                for (int i = 1; i < stepNum+1; i++)                
                    if(!writeCmd("SAF:STEP1:DEL",ref er))
                        return false;

                //加载新的参数设置

                stepNum = 0; 

                for (int i = 0; i < step.Count; i++)
                {
                    string stepNo = (step[i].stepNo + 1).ToString();
  
                    switch (step[i].name)
                    {
                        case CHPSetting.EStepName.AC:
                             //VOLTAGE
                            cmd = "SAF:STEP" + stepNo + ":AC " + ((int)(step[i].para[0].setVal * 1000)).ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            //HIGHT LIMIT
                            cmd = "SAF:STEP" + stepNo + ":AC:LIM:HIGH "+((step[i].para[1].setVal/1000)).ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                            //LOW LIMIT
                            cmd = "SAF:STEP" + stepNo + ":AC:LIM:LOW "+((step[i].para[2].setVal/1000)).ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                            //ARC LIMIT
                            cmd = "SAF:STEP" + stepNo + ":AC:LIM:ARC " + ((step[i].para[3].setVal / 1000)).ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                            //RAMP TIME 
                            cmd = "SAF:STEP" + stepNo + ":AC:TIME:RAMP " + step[i].para[4].setVal.ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                           //TEST TIME           
                            cmd = "SAF:STEP" + stepNo + ":AC:TIME " + step[i].para[5].setVal.ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                           //FALL TIME 
                            cmd = "SAF:STEP" + stepNo + ":AC:TIME:FALL " + step[i].para[6].setVal.ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                            break;
                        case CHPSetting.EStepName.DC:
                           //VOLTAGE
                            cmd = "SAF:STEP" + stepNo + ":DC "+((int)(step[i].para[0].setVal*1000)).ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                           //HIGHT LIMIT
                            cmd = "SAF:STEP" + stepNo + ":DC:LIM:HIGH "+((step[i].para[1].setVal/1000)).ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                           //LOW LIMIT
                            cmd = "SAF:STEP" + stepNo + ":DC:LIM:LOW "+((step[i].para[2].setVal/1000)).ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                           //ARC LIMIT
                            cmd = "SAF:STEP" + stepNo + ":DC:LIM:ARC " + ((step[i].para[3].setVal / 1000)).ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                           //RAMP TIME 
                            cmd = "SAF:STEP" + stepNo + ":DC:TIME:RAMP " + step[i].para[4].setVal.ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                           //DWELL TIME     
                            cmd = "SAF:STEP" + stepNo + ":DC:TIME:DWEL " + step[i].para[5].setVal.ToString();
                            if (!writeCmd(cmd, ref er))
                                return false;
                           //TEST TIME 
                            cmd = "SAF:STEP" + stepNo + ":DC:TIME " + step[i].para[6].setVal.ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                           //FALL TIME
                            cmd = "SAF:STEP" + stepNo + ":DC:TIME:FALL " + step[i].para[7].setVal.ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            break;
                        case CHPSetting.EStepName.IR:
                            //VOLTAGE
                            cmd = "SAF:STEP" + stepNo + ":IR "+((int)(step[i].para[0].setVal*1000)).ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            //LOW LIMIT
                            cmd = "SAF:STEP" + stepNo + ":IR:LIM:LOW " + ((int)(step[i].para[1].setVal * 1000000)).ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            //HIGHT LIMIT
                            cmd = "SAF:STEP" + stepNo + ":IR:LIM:HIGH "+((int)(step[i].para[2].setVal*1000000)).ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            //RAMP TIME   
                            cmd = "SAF:STEP" + stepNo + ":IR:TIME:RAMP " + step[i].para[3].setVal.ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            //TEST TIME 
                            cmd = "SAF:STEP" + stepNo + ":IR:TIME " + step[i].para[4].setVal.ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            //FALL TIME
                            cmd = "SAF:STEP" + stepNo + ":IR:TIME:FALL " + step[i].para[5].setVal.ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            break;
                        case CHPSetting.EStepName.OSC:
                            //OPEN
                            cmd = "SAF:STEP" + stepNo + ":OSC:LIM:OPEN "+(step[i].para[0].setVal/100).ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            //SHORT
                            cmd = "SAF:STEP" + stepNo + ":OSC:LIM:SHOR "+(step[i].para[1].setVal/100).ToString();
                            if(!writeCmd(cmd,ref er))
                                return false;
                            break;
                        case CHPSetting.EStepName.GC:
                            break;
                        case CHPSetting.EStepName.PA:
                            break;
                        default:
                            break;
                    }
                }
                if (saveToDev)
                { 
                   if(!saveProgram(proName,ref er))
                       return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true;
            }
        }
        /// <summary>
        /// 启动测试
        /// </summary>
        public bool start(ref string er, ref string sendreaddata)
        {
            return writeCmd("SAF:STAR", ref er);
        }
        /// <summary>
        /// 停止测试
        /// </summary>
        public bool stop(ref string er, ref string sendreaddata)
        {
           return writeCmd("SAF:STOP",ref er);
        }
        /// <summary>
        /// 读取测试状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readStatus(ref EHPStatus status, ref string er, ref string sendreaddata)
        {
            try
            {
                string cmd = "SAF:STAT?";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;
                if (rData == EHPStatus.RUNNING.ToString())
                    status = EHPStatus.RUNNING;
                else
                    status = EHPStatus.STOPPED;  
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true; 
            }
        }
        /// <summary>
        /// 读取测试结果
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
                               ref List<CHPSetting.EStepName>stepMode,ref List<string> stepVal,
                               ref List<string> stepUnit, ref string er, ref List<string> HpVolt, ref string sendreaddata)
        {

            try
            {
                //获取测试结果

                string cmd = "SAF:CHAN"+chan.ToString("D3")+":RES:ALL?";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;

                string[] stepResultList = rData.Split(',');

                int stepNo = stepResultList.Length;

                chanResult = 0;

                for (int i = 0; i < stepNo; i++)
                {
                    int resultCode = System.Convert.ToInt32(stepResultList[i]);

                    if (c_HPCode.ContainsKey(resultCode))
                    {
                        if (c_HPCode[resultCode] == "PASS")
                            stepResult.Add(0);
                        else
                        {
                            chanResult = resultCode;
                            stepResult.Add(resultCode);
                        }                           
                        stepCode.Add(c_HPCode[resultCode]);
                    }
                    else
                    {
                        chanResult = 1;
                        stepResult.Add(-1);
                        stepCode.Add("CODE ERROR"); 
                    }
                }

                //获取步骤模式
                cmd = "SAF:CHAN" + chan.ToString("D3") + ":RES:ALL:MODE?";

                rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;

                string[] stepModeList = rData.Split(',');

                for (int i = 0; i < stepModeList.Length; i++)
                {
                    switch (stepModeList[i])
                    {
                        case "AC":
                            stepMode.Add(CHPSetting.EStepName.AC);   
                            break;
                        case "DC":
                            stepMode.Add(CHPSetting.EStepName.DC);  
                            break;
                        case "IR":
                            stepMode.Add(CHPSetting.EStepName.IR);
                            break;
                        case "OSC":
                            stepMode.Add(CHPSetting.EStepName.OSC);
                            break;
                        default:
                            stepMode.Add(CHPSetting.EStepName.PA);
                            break;
                    }
                }

                //获取测试数据

                cmd = "SAF:CHAN" + chan.ToString("D3") + ":RES:ALL:MMET?";

                rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;

                string[] stepValList = rData.Split(',');

                for (int i = 0; i < stepValList.Length; i++)
                {
                    double stepData = System.Convert.ToDouble(stepValList[i]);  

                    switch (stepMode[i])
                    {
                        case CHPSetting.EStepName.AC:
                            stepVal.Add((stepData*System.Math.Pow(10,3)).ToString() );
                            stepUnit.Add("mA");  
                            break;
                        case CHPSetting.EStepName.DC:
                            stepVal.Add((stepData * System.Math.Pow(10, 3)).ToString ());
                            stepUnit.Add("mA"); 
                            break;
                        case CHPSetting.EStepName.IR:
                            double R= stepData / System.Math.Pow(10, 6);
                            if (R < 1000)
                            {
                                stepVal.Add(R.ToString ());
                                stepUnit.Add("MΩ");
                            }
                            else
                            {
                                stepVal.Add((R/1000).ToString ());
                                stepUnit.Add("GΩ");
                            }
                            break;
                        case CHPSetting.EStepName.OSC:
                            stepVal.Add((stepData*System.Math.Pow(10,9)).ToString ());
                            stepUnit.Add("nF"); 
                            break;
                        case CHPSetting.EStepName.GC:
                            break;
                        case CHPSetting.EStepName.PA:
                            break;
                        default:
                            break;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true;
            }
        }
        #endregion

        #region 仪器通信
        /// <summary>
        /// 读取仪器设备
        /// </summary>
        /// <param name="devName"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool readIDN(ref string devName,ref string er)
        {
            try
            {
                string cmd = "*IDN?";

                string rData = string.Empty;

                if(!SendCmdToHP(cmd,ref rData,ref er))
                   return false;
                string[] valList = rData.Split(',');
                if (valList.Length < 2)
                    return false;
                devName = valList[0] + valList[1];
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;  
            }
        }
        /// <summary>
        /// 读错误信息
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool readError(ref string errCode,ref string er)
        {
            try
            {
                string cmd = "SYST:ERR?";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;

                string[] codeList = rData.Split(',');

                int code = System.Convert.ToInt32(codeList[0]);

                if (code == 0)    
                    errCode = "OK";                    
                else
                    errCode = "错误信息:" + codeList[1].Replace("\"", "") + "-" + code.ToString();
                
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        
        }
        /// <summary>
        /// 远程控制
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool remote(ref string er)
        {
            try
            {
                string cmd = "SYST:LOCK:REQ?";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;
                if (rData != "1")
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;  
            }
        }
        /// <summary>
        /// 保存机种文件到高压机种
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool saveProgram(string proName, ref string er)
        {
            try
            {
                er = string.Empty;

                string cmd = "MEM:STAT:DEF? \"" + proName + "\"";

                string rData = string.Empty;

                int proNo = 0;

                if (SendCmdToHP(cmd, ref rData, ref er)) //程序名存在
                {
                    proNo = System.Convert.ToInt16(rData);

                    if (!writeCmd("*SAV " + proNo, ref er))
                        return false;

                    if (!writeCmd("MEM:STAT:DEF \"" + proName + "\"," + proNo, ref er))
                        return false;
                   
                }
                else
                {
                    //查询存储空间
                    cmd = "MEM:NST?";
                    if (!SendCmdToHP(cmd, ref rData, ref er))
                        return false;
                    int maxProNo = System.Convert.ToInt16(rData);

                    //查询可用位置
                    cmd = "MEM:FREE:STAT?";
                    if (!SendCmdToHP(cmd, ref rData, ref er))
                        return false;
                    int proIndex = System.Convert.ToInt16(rData);

                    proNo = maxProNo - proIndex;
         
                    if (proNo == 0)
                    {
                        er = "高压机无可用存储程序,请删除多余程序";
                        return false;
                    }

                    writeCmd("*SAV " + proNo, ref er);

                    writeCmd("MEM:STAT:DEF \"" + proName + "\"," + proNo, ref er);
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 写命令
        /// </summary>
        /// <param name="wCmd"></param>
        /// <param name="delayMs"></param>
        private bool writeCmd(string wCmd,ref string er,int delayMs=5,int timeOutMs=500)
        {
            string rData = string.Empty;

            er = string.Empty; 

            SendCmdToHP(wCmd, ref rData, ref er,string.Empty);

            System.Threading.Thread.Sleep(delayMs);

            int doReady = 0;

            int waitTimes = System.Environment.TickCount;

            do
            {
                if (!SendCmdToHP("*OPC?", ref rData, ref er))
                    continue;
                doReady = System.Convert.ToInt32(rData);
            }
            while (doReady == 0 || System.Environment.TickCount - waitTimes < timeOutMs);

            if (doReady == 0)
            {
                er = "写命令("+wCmd + ")超时";
                return false;
            }

            string errcode = string.Empty;

            if (!readError(ref errcode, ref er))
                return false;

            if (errcode != "OK")
            {
                er = "写命令(" + wCmd + ")" + errcode;
                return false; 
            }

            return true;
        }
        /// <summary>
        /// 发送和接收数据
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="wData"></param>
        /// <param name="rLen"></param>
        /// <param name="rData"></param>
        /// <param name="er"></param>
        /// <param name="wTimeOut"></param>
        /// <returns></returns>
        private bool SendCmdToHP(string wData, ref string rData, ref string er,string rEOI="\r\n",int wTimeOut = 1000)
        {
            string recvData = string.Empty;
            wData += rEOI; 

            if (!com.send(wData, rEOI, ref rData, ref er, wTimeOut))
                return false;
            rData = rData.Replace("\r\n", ""); 
            return true;
        }
        #endregion
    }
}
