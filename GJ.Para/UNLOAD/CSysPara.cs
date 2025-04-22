using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;  
namespace GJ.Para.UNLOAD
{
    public class CSysPara:CSysBase
    {
        #region 字段

        public string plcIp1 = "127.0.0.1";
        public int plcPort1 = 502;
        public string plcIp2 = "127.0.0.1";
        public int plcPort2 = 502;
        public string plcUnloadIP = "";
        public int plcUnloadPort = 502;

        public string idCom = "COM1";
        public string LCRCom = "COM2";
        public string IoCom = "COM3";
        /// <summary>
        /// 使用DCN
        /// </summary>
        public bool ChkDCN = false;
        /// <summary>
        /// DCN串口
        /// </summary>
        public string DCNCom = "COM4";
        /// <summary>
        /// DCN串口波特率
        /// </summary>
        public string DCNBand = "9600,N,8,1";
        public string mySqlIp = "127.0.0.1";
        public int testPort = 8000;
        public bool AtuoUnload = false;

        public int FailTestCount = 0;
        public int IoDelayMs = 20;
        public int monDelayMs = 1000;
        public int ATEFailTimes = 3;
        public bool chkBIFail = false;
        public bool chkHPFail = false;
        public bool chkATEFail = false;
        public bool chkUnLoadFail = false;
        public bool chkHandUp = false;
        public bool[] chkForceTURNON = new bool[] { false, false};
        public bool[] chkForceRRCC = new bool[] { false, false };
        public bool[] chkForceHP =new bool[]{false,false,false,false};
        public bool[] chkForceATE = new bool[] { false, false ,false ,false ,false ,false ,false ,false };

        public bool chkTURNONIn = false;
        public bool chkRRCCIn = false;
        public bool chkHPIn = false;
        public bool chkATEIn = false;


        public bool[] chkNoHP = new bool[] { false, false };
        public bool[] chkNoATE = new bool[] { false, false, false, false };

        public bool chkNoRRCC = false;
        public bool chkNoTurnOn = false;

        public string chkstationHP = "HIPOT";
        public string[] chkstationATE = new string[] { "ATE1", "ATE2", "ATE3", "ATE4" };

        public bool conMES = false;
        public bool conRRCC = false;
        public int MesDelayTime = 100;
        public string FailPassword = "123456";
        public bool ChkRRCCPass = false;

        #endregion

        public static CSysPara mVal = new CSysPara();
    }
}
