using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;  
namespace GJ.Para.LOADUP
{
    public class CSysPara:CSysBase
    {
        #region 字段

        public string plcIp = "127.0.0.1";
        public int plcPort = 502;
        public string idCom = "COM1";
        public string eloadCom = "COM2";
        public string ACRCom = "COM3";
        public string mySqlIp = "127.0.0.1";

        public int acDelayTimes;
        public int testTimes;
        public int MesDelatTime = 200;
        public bool chkAcOn;
        public bool chkFailGo;
        public bool chkFailWait;
        public bool chkAcMeter;

        public string[] SpotCheckSn = new string[] { "FIX01", "FIX02", "FIX03", "FIX04", "FIX05", "FIX06", "FIX07", "FIX08" };
        public int connectorTimes;
        public int fixtureTimes;
        public int failTimes;

        public int monAddr = 77;

        public bool handBandSn = false;
        public int snLen = 0;
        public string snSpec = string.Empty;

        public bool conMes = false;
        public bool PTconMes = false;
        public string MesUserName = "";
        public string MesPassWord = "";
        public string MesStation = "";

        public int dayTimeOut = 7;
        public double OffSetV;
        public double OffSetCur;



        #endregion

        public static CSysPara mVal = new CSysPara();
    }
}
