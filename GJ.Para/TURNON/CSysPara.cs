using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;
//using GJ.Dev.HIPOT;  
namespace GJ.Para.TURNON
{
    public class CSysPara:CSysBase
    {

        #region 字段

        public string ioCom = "COM1";
      //  public EHipotType hipotDev=EHipotType.Chroma_PLCCOM;
        public string ChromaEloadCom;
        public string IDCom;
        public string serIP = "127.0.0.1";
        public int serPort = 8000;
        public bool ChkID = false;
        public string MonCom = "COM3";

        public int disChargerTime = 2000;

        public int ioDelayMs = 20;

        public int monDelayMs = 200;

        public int StepDelayTime = 3000;

        public bool chkImpPrg = false;

        public bool chkAutoModel = false;

        public bool chkSaveTcpLog = false;

        public int C_Chroma_MAX =1;

        public int C_SLOT_MAX = 8;

        public int StatHipotMode = 0;

        public bool conMes = false;

        public bool ChkCur = false;
        #endregion

        public static CSysPara mVal = new CSysPara();

    }
}
