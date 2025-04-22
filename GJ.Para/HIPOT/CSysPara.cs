using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;
using GJ.Dev.HIPOT;  
namespace GJ.Para.HIPOT
{
    public class CSysPara:CSysBase
    {

        #region 字段

        public string ioCom = "COM1";
        public EHipotType hipotDev=EHipotType.Chroma_PLCCOM;
        public string hipotCom1;
        public string hipotCom2;

        public string plcIp = "192.168.3.100";
        public int plcPort = 502;
        public string serIP = "127.0.0.1";
        public int serPort = 8000;

        public int ioDelayMs = 20;

        public int monDelayMs = 200;

        public int MesDelayMs = 200;
        public int Hp2DelayMs = 2000;
        public int FailTestCount = 3;
        //比对高压值
        public bool chkHpVolt = true;
        /// <summary>
        /// 手动确认不良
        /// </summary>
        public bool chkhandleFail = false;
        /// <summary>
        /// 高压位分测模式
        /// </summary>
        public bool chkSpHP = false;
        public bool chkImpPrg = false;

        public bool chkAutoModel = false;

        public bool chkSaveTcpLog = false;

        public int C_HIPOT_MAX = 1;

        public int C_SLOT_MAX = 8;

        public int StatHipotMode = 0;

        public bool conMes = false;

        public bool ChkMesFail = false;

        public double[] offsetDC = new double[8];

        public double[] offsetAC = new double[8];

        public bool chksaveHipotData = true;
        #endregion

        public static CSysPara mVal = new CSysPara();

    }
}
