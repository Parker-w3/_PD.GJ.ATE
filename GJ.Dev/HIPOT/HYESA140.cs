using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.HIPOT
{
   public  class HYESA140 :IHIPOT 
    {

        public HYESA140()
        {
            c_HPCode = new Dictionary<int, string>();
            //c_HPCode.Add(112, "STOP");
            //c_HPCode.Add(115, "TESTING");
            //c_HPCode.Add(116, "PASS");
            //c_HPCode.Add(33, "HIGH FAIL");
            //c_HPCode.Add(49, "HIGH FAIL");
            //c_HPCode.Add(65, "HIGH FAIL");
            //c_HPCode.Add(34, "LOW FAIL");
            //c_HPCode.Add(50, "LOW FAIL");
            //c_HPCode.Add(66, "LOW FAIL");
            //c_HPCode.Add(35, "ARC FAIL");
            //c_HPCode.Add(51, "ARC FAIL");
            //c_HPCode.Add(36, "OCP");
            //c_HPCode.Add(52, "OCP");
            //c_HPCode.Add(68, "OCP");
            //c_HPCode.Add(100, "OCP");
            //c_HPCode.Add(97, "SHORT FAIL");
            //c_HPCode.Add(98, "OPEN FAIL");
            com = new COM.CSerialPort();
        }

        #region 字段
        private string name = "ESA-140";
        private int idNo = 0;
        private int chanNum = 1;
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
                //if (devName != name)
                //{
                //    er = "高压仪器[" + name + "]型号错误:" + devName;
                //    return false;
                //}
             
                if (!remote(ref er))
                    return false;
                string rData = string.Empty;
                if (!SendCmdToHP("RESET",ref rData,ref er))  //清除错误
                    return false;
                //if (rData != "RESET")
                //    return false;
                rData =string .Empty ;
                if (!SendCmdToHP ("SF 1",ref rData ,ref er)) //不良停止
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

                //string cmd = "MEM:STAT:DEF? \"" + proName + "\"";

                //string rData = string.Empty;

                //if (!SendCmdToHP(cmd, ref rData, ref er))
                //{
                //    er = "程序[" + proName + "]不存在.";
                //    return false;
                //}

                //int proNo = System.Convert.ToInt16(rData);

                //if (!writeCmd("*RCL " + proNo, ref er))
                //    return false;

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
        public bool readStepName(ref List<CHPSetting.EStepName> stepName, ref string er, int chan = 1)
        {
            try
            {
                ////获取步骤模式
                //string cmd = "SAF:CHAN" + chan.ToString("D3") + ":RES:ALL:MODE?";

                //string rData = string.Empty;

                //if (!SendCmdToHP(cmd, ref rData, ref er))
                //    return false;

                //string[] stepModeList = rData.Split(',');

                //for (int i = 0; i < stepModeList.Length; i++)
                //{
                //    switch (stepModeList[i])
                //    {
                //        case "AC":
                //            stepName.Add(CHPSetting.EStepName.AC);
                //            break;
                //        case "DC":
                //            stepName.Add(CHPSetting.EStepName.DC);
                //            break;
                //        case "IR":
                //            stepName.Add(CHPSetting.EStepName.IR);
                //            break;
                //        case "OSC":
                //            stepName.Add(CHPSetting.EStepName.OSC);
                //            break;
                //        default:
                //            stepName.Add(CHPSetting.EStepName.PA);
                //            break;
                //    }
                //}
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
        public bool readStepSetting(int stepNo, ref CHPSetting.EStepName stepName, ref List<double> stepVal, ref string er)
        {
            try
            {
                //string cmd = "SAF:STEP" + stepNo.ToString() + ":SET?";

                //string rData = string.Empty;

                //if (!SendCmdToHP(cmd, ref rData, ref er))
                //    return false;

                //string[] stepList = rData.Split(',');

                //if (stepList.Length < 4)
                //{
                //    er = "获取步骤数据错误:" + rData;
                //    return false;
                //}
                //switch (stepList[2])
                //{
                //    case "AC":
                //        stepName = CHPSetting.EStepName.AC;
                //        for (int i = 0; i < stepList.Length - 5; i++)
                //            stepVal.Add(System.Convert.ToDouble(stepList[i + 3]));
                //        if (stepVal.Count != CHPSetting.c_ACItem.Length)
                //        {
                //            er = "获取步骤数据错误:" + rData;
                //            return false;
                //        }
                //        stepVal[0] = stepVal[0] / 1000;//VOLTAGE
                //        stepVal[1] = stepVal[1] * 1000;//HIGHT LIMIT
                //        stepVal[2] = stepVal[2] * 1000;//LOW LIMIT
                //        stepVal[3] = stepVal[3] * 1000;//ARC LIMIT
                //        stepVal[4] = stepVal[4];//RAMP TIME 
                //        stepVal[5] = stepVal[5];//TEST TIME                        
                //        stepVal[6] = stepVal[6];//FALL TIME 
                //        break;
                //    case "DC":
                //        stepName = CHPSetting.EStepName.DC;
                //        for (int i = 0; i < stepList.Length - 5; i++)
                //            stepVal.Add(System.Convert.ToDouble(stepList[i + 3]));
                //        if (stepVal.Count != CHPSetting.c_DCItem.Length)
                //        {
                //            er = "获取步骤数据错误:" + rData;
                //            return false;
                //        }
                //        stepVal[0] = stepVal[0] / 1000;//VOLTAGE
                //        stepVal[1] = stepVal[1] * 1000;//HIGHT LIMIT
                //        stepVal[2] = stepVal[2] * 1000;//LOW LIMIT
                //        stepVal[3] = stepVal[3] * 1000;//ARC LIMIT
                //        stepVal[4] = stepVal[4];//RAMP TIME 
                //        stepVal[5] = stepVal[5];//DWELL TIME                        
                //        stepVal[6] = stepVal[6];//TEST TIME 
                //        stepVal[7] = stepVal[7]; //FALL TIME
                //        break;
                //    case "IR":
                //        stepName = CHPSetting.EStepName.IR;
                //        for (int i = 0; i < stepList.Length - 7; i++)
                //            stepVal.Add(System.Convert.ToDouble(stepList[i + 3]));
                //        if (stepVal.Count != CHPSetting.c_IRItem.Length)
                //        {
                //            er = "获取步骤数据错误:" + rData;
                //            return false;
                //        }
                //        stepVal[0] = stepVal[0] / 1000;//VOLTAGE
                //        stepVal[1] = stepVal[1] / 1000000;//LOW LIMIT
                //        stepVal[2] = stepVal[2] / 1000000;//HIGHT LIMIT
                //        stepVal[3] = stepVal[3];//RAMP TIME                    
                //        stepVal[4] = stepVal[4];//TEST TIME 
                //        stepVal[5] = stepVal[5]; //FALL TIME
                //        break;
                //    case "OSC":
                //        stepName = CHPSetting.EStepName.OSC;
                //        for (int i = 0; i < stepList.Length - 5; i++)
                //            stepVal.Add(System.Convert.ToDouble(stepList[i + 3]));
                //        if (stepVal.Count != CHPSetting.c_OSCItem.Length)
                //        {
                //            er = "获取步骤数据错误:" + rData;
                //            return false;
                //        }
                //        stepVal[0] = stepVal[0] * 100;     //OPEN
                //        stepVal[1] = stepVal[1] * 100;     //SHORT
                //        break;
                //    default:
                //        stepName = CHPSetting.EStepName.PA;
                //        break;
                //}
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
        public bool setChanEnable(List<int> chanList, ref string er)
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
                    if (i < chanList.Count - 1)
                        chanStr += chanList[i].ToString("D3") + ",";
                    else
                        chanStr += chanList[i].ToString("D3");
                }

                string cmd = "SYST:TCON:CHAN (@" + chanStr + ")";

                return writeCmd(cmd, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
 /// <summary>
 /// 设置测试步骤
 /// </summary>
 /// <param name="step"></param>
 /// <param name="er"></param>
 /// <param name="proName"></param>
 /// <param name="saveToDev"></param>
 /// <param name="sendreaddata"></param>
 /// <returns></returns>
        public bool setTestPara(List<CHPSetting.CStep> step, ref string er,string proName,bool saveToDev, ref string sendreaddata)
        {
            try
            {
                int waitTime = 20;
                sendreaddata = "";
                //删除原有参数设置
                string rData = string.Empty;
                string cmd = "FD";
                  
                if (!SendCmdToHP(cmd, ref rData, ref er))  //清除错误
                    return false;
                CMath.WaitMs(waitTime);
                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";   
                rData = string.Empty;
                if (!SendCmdToHP("RESET",ref rData , ref er))  //清除错误
                    return false;
                CMath.WaitMs(waitTime);
                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + "RESET" + "回复：" + rData + "\r\n";
                cmd = "FN 1,TEST";
                rData = string.Empty;
                if (!SendCmdToHP(cmd, ref rData, ref er))  //清除错误
                    return false;
                CMath.WaitMs(waitTime);
                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                //int stepNum = System.Convert.ToInt32(rData);

                //for (int i = 1; i < stepNum+1; i++)                
                //    if(!writeCmd("SAF:STEP1:DEL",ref er))
                //        return false;

                //加载新的参数设置

              // stepNum = 0; 

                for (int i =step.Count-1; i >-1; i--)
                {
                    string stepNo = (step[i].stepNo + 1).ToString();
  
                    switch (step[i].name)
                    {
                        case CHPSetting.EStepName.AC:
                            cmd = "SAA"; //新建ACW测试项目
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            //VOLTAGE
                            cmd = "EV " + ((int)(step[i].para[0].setVal * 1000)).ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            //HIGHT LIMIT
                            cmd = "EHT "+((step[i].para[1].setVal)).ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            //LOW LIMIT
                            cmd = "ELT  "+((step[i].para[2].setVal)).ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            //ARC LIMIT
                            if (step[i].para[3].setVal != 0)
                            {
                                cmd = "EAD 1";
                                if (!SendCmdToHP(cmd, ref rData, ref er))
                                    return false;
                                CMath.WaitMs(waitTime);
                                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                                cmd = "EA " + ((step[i].para[3].setVal)).ToString();
                                if (!SendCmdToHP(cmd, ref rData, ref er))
                                    return false;
                                CMath.WaitMs(waitTime);
                                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            }
                            else
                            {
                                cmd = "EAD 0";
                                if (!SendCmdToHP(cmd, ref rData, ref er))
                                    return false;
                                CMath.WaitMs(waitTime);
                                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            }
                            //RAMP TIME 
                            cmd = "ERU " + step[i].para[4].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                           //TEST TIME           
                            cmd = "EDW " + step[i].para[5].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            cmd = "EO " + step[i].para[6].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            cmd = "EHR " + step[i].para[7].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            cmd = "ELR " + step[i].para[8].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            break;
                           //FALL TIME 
                            //cmd = "SAF:STEP" + stepNo + ":AC:TIME:FALL " + step[i].para[6].setVal.ToString();
                            //if (!SendCmdToHP(cmd, ref rData, ref er))
                            //    return false;
                            //break;
                        case CHPSetting.EStepName.DC:
                           //VOLTAGE
                            cmd = "SAD";
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            cmd = "EV "+((int)(step[i].para[0].setVal*1000)).ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                           //HIGHT LIMIT
                            cmd = "EH "+((step[i].para[1].setVal)).ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                           //LOW LIMIT
                            cmd = "EL "+((step[i].para[2].setVal)).ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                           //ARC LIMIT
                            if (step[i].para[3].setVal != 0)
                            {
                                cmd = "EAD 1";
                                if (!SendCmdToHP(cmd, ref rData, ref er))
                                    return false;
                                CMath.WaitMs(waitTime);
                                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                                cmd = "EA " + ((step[i].para[3].setVal)).ToString();
                                if (!SendCmdToHP(cmd, ref rData, ref er))
                                    return false;
                                CMath.WaitMs(waitTime);
                                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            }
                            else
                            {
                                cmd = "EAD 0";
                                if (!SendCmdToHP(cmd, ref rData, ref er))
                                    return false;
                                CMath.WaitMs(waitTime);
                                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            }
                           //RAMP TIME 
                            cmd = "ERU " + step[i].para[4].setVal.ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            ////DWELL TIME     
                            //cmd = "SAF:STEP" + stepNo + ":DC:TIME:DWEL " + step[i].para[5].setVal.ToString();
                            //if (!SendCmdToHP(cmd, ref rData, ref er))
                            //    return false;
                           //TEST TIME 
                            cmd = "EDW " + step[i].para[5].setVal.ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  

                            cmd = "EO " + step[i].para[6].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            //Charge Low
                            cmd = "ECG " + step[i].para[7].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            break;
                           ////FALL TIME
                           // cmd = "SAF:STEP" + stepNo + ":DC:TIME:FALL " + step[i].para[7].setVal.ToString();
                           // if(!SendCmdToHP(cmd, ref rData, ref er))
                           //     return false;
                           // break;
                        case CHPSetting.EStepName.IR:
                            //新建IR
                            cmd = "SAI";
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";  
                            //VOLTAGE
                            cmd = "EV "+((int)(step[i].para[0].setVal*1000)).ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //HIGHT LIMIT
                            cmd = "EH " + ((int)(step[i].para[1].setVal )).ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //LOW LIMIT
                            cmd = "EL "+((int)(step[i].para[2].setVal)).ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //RAMP TIME   
                            cmd = "ERU " + step[i].para[3].setVal.ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //TEST TIME 
                            cmd = "EDW " + step[i].para[4].setVal.ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //TEST TIME 
                            cmd = "EDE " + step[i].para[5].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            cmd = "EO " + step[i].para[6].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //Charge Low
                            cmd = "ECG " + step[i].para[7].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                     
                            break;
                        case CHPSetting.EStepName.OSC:
                            //OPEN
                            //cmd = "SAF:STEP" + stepNo + ":OSC:LIM:OPEN "+(step[i].para[0].setVal/100).ToString();
                            //if(!SendCmdToHP(cmd, ref rData, ref er))
                            //    return false;
                            ////SHORT
                            //cmd = "SAF:STEP" + stepNo + ":OSC:LIM:SHOR "+(step[i].para[1].setVal/100).ToString();
                            //if(!SendCmdToHP(cmd, ref rData, ref er))
                            //    return false;
                            break;

                        case CHPSetting.EStepName.GC:

                            cmd = "SAG";
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //VOLTAGE
                            cmd = "EC "+((int)(step[i].para[0].setVal)).ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //LOW LIMIT
                            cmd = "EV " + ((int)(step[i].para[1].setVal )).ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //HIGHT LIMIT
                            cmd = "EH "+((int)(step[i].para[2].setVal)).ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //RAMP TIME   
                            cmd = "EL " + step[i].para[3].setVal.ToString();
                            if(!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            //TEST TIME 
                            cmd = "EDW " + step[i].para[4].setVal.ToString();
                            if (!SendCmdToHP(cmd, ref rData, ref er))
                                return false;
                            CMath.WaitMs(waitTime);
                            sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                            break;
                        case CHPSetting.EStepName.PA:
                            break;
                        default:
                            break;
                    }
                }
                cmd = "FS";
                rData = string.Empty;
                if (!SendCmdToHP(cmd, ref rData, ref er))  //清除错误
                    return false;
                sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n"; 
                if (saveToDev)
                {
                    //if (!saveProgram(proName, ref er))
                    //    return false;
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
        public bool start(ref string er,ref string sendreaddata)
        {
            string rData = string.Empty;
            sendreaddata = string.Empty;
            if (!SendCmdToHP("RESET", ref rData, ref er))
            {
                if (!SendCmdToHP("RESET", ref rData, ref er))
                {
                    return false;
                }
            }
            CMath.WaitMs(20);
            string cmd = "TEST";
            if (! SendCmdToHP(cmd, ref rData, ref er))
            {
                return false;
            }
            sendreaddata = DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";
            return true;
        }
        /// <summary>
        /// 停止测试
        /// </summary>
        public bool stop(ref string er,ref string sendreaddata)
        {
            sendreaddata = string.Empty;
            string rData = string.Empty;
            string cmd = "RESET";
            if (!SendCmdToHP(cmd, ref rData, ref er))
            {
                return false;
            }
            sendreaddata = DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";
            return true;
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
                sendreaddata = string.Empty;
                string cmd = "*STB?";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;
                if (rData == "8") //0:FAIL,1:PASS,2:ABORT;8:TESTING
                    status = EHPStatus.RUNNING ;
                else if (rData == "2")
                    status = EHPStatus.ABORT;  
                else 
                    status = EHPStatus.STOPPED ;

                sendreaddata = DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";
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
                sendreaddata = string.Empty;
                //获取测试结果
                string rData = string.Empty;
                chanResult = 0;
                //获取步骤模式
                for (int i = 0; i < chan; i++)
                {

                    string cmd = "RD " + (i+1).ToString ()+ "?";

                    rData = string.Empty;

                    if (!SendCmdToHP(cmd, ref rData, ref er))
                        return false;

                    sendreaddata += DateTime.Now.ToString("HH:mm:ss") + "发送:" + cmd + "回复：" + rData + "\r\n";
                    string[] valList = rData.Split(',');
                    if (valList.Length < 5)
                    {
                        continue;
                    }
                    if (valList[2] == "PASS")
                    {
                        stepResult.Add(0);

                    }
                    else
                    {
                        stepResult.Add(i + 1);
                        chanResult = i + 1;

                    }
                    stepCode.Add(valList[2]);
                    string stepModeList = valList[1];
                    //高压测试电压
                    HpVolt.Add(valList[3]);
                  //  string stepValList = valList[4];
   
                    switch (stepModeList)
                        {
                            case "ACW":
                                stepMode.Add(CHPSetting.EStepName.AC);
                                break;
                            case "DCW":
                                stepMode.Add(CHPSetting.EStepName.DC);
                                break;
                            case "IR":
                                stepMode.Add(CHPSetting.EStepName.IR);
                                break;
                            case "OSC":
                                stepMode.Add(CHPSetting.EStepName.OSC);
                                break;
                            case "GND":
                                stepMode.Add(CHPSetting.EStepName.GC);
                                break;
                            default:
                                stepMode.Add(CHPSetting.EStepName.PA);
                                break;
                        }
                  

                    //获取测试数据
              
                    //double stepData = System.Convert.ToDouble(stepValList[i]);
                   // double stepData = 0;
                    switch (stepMode[i])
                        {
                            case CHPSetting.EStepName.AC:
                                valList[4] = valList[4].Replace(">","");
                                valList[4] = valList[4].Replace("<", "");
                               
                               // stepData = System.Convert.ToDouble(valList[4]);

                                stepVal.Add(valList[4]);
                                stepUnit.Add("mA");
                                break;
                            case CHPSetting.EStepName.DC:
                                valList[4] = valList[4].Replace(">","");
                                valList[4] = valList[4].Replace("<", "");
                              //  stepData = System.Convert.ToDouble(valList[4]);
                                stepVal.Add(valList[4]);
                                stepUnit.Add("uA");
                                break;
                            case CHPSetting.EStepName.IR:
                                valList[4] = valList[4].Replace(">","");
                                valList[4] = valList[4].Replace("<", "");
                                //stepData = System.Convert.ToDouble(valList[4]);
                                //double R = stepData;  // / System.Math.Pow(10, 6);
                                stepVal.Add(valList[4]);
                                stepUnit.Add("MΩ");
                                //if (R < 1000)
                                //{
                                //   // stepVal.Add(R);
                                //    stepUnit.Add("MΩ");
                                //}
                                //else
                                //{
                                //   // stepVal.Add(R / 1000);
                                //    stepUnit.Add("GΩ");
                                //}
                                break;
                            case CHPSetting.EStepName.OSC:

                               // stepVal.Add(stepData * System.Math.Pow(10, 9));
                                stepUnit.Add("nF");
                                break;
                            case CHPSetting.EStepName.GC:
                                valList[4] = valList[4].Replace(">","");
                                valList[4] = valList[4].Replace("<", "");
                                //stepData = System.Convert.ToDouble(valList[4]);
                                //double gndR = stepData;  // / System.Math.Pow(10, 6)
                                stepVal.Add(valList[4]);
                                stepUnit.Add("MΩ");
                                //if (gndR < 1000)
                                //{
                                //    stepVal.Add(gndR);
                                //    stepUnit.Add("MΩ");
                                //}
                                //else
                                //{
                                //    stepVal.Add(gndR / 1000);
                                //    stepUnit.Add("GΩ");
                                //}
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
                return false ;
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

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;
                string[] valList = rData.Split(',');
                if (valList.Length < 2)
                    return false;
                devName = valList[2];
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
                string cmd = "*ESR?";

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
                string cmd = "SPR 1?";

                string rData = string.Empty;

                if (!SendCmdToHP(cmd, ref rData, ref er))
                    return false;
                //if (rData != "")
                //    return false;
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
        private bool SendCmdToHP(string wData, ref string rData, ref string er,string rEOI="\n",int wTimeOut = 1000)
        {
            rData = string.Empty;
            string recvData = string.Empty;
            wData += "\n"; 
            if (!com.send(wData, rEOI, ref rData, ref er, wTimeOut))
                return false;
            rData = rData.Replace("\n", "");
            //发送成功060A 发送失败15H0A
            return true;
            //if (rData=="060A")
            //    return true;
            //else
            //    return false;
           // return true;
        }
        #endregion
    }
}
