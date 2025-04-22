using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;  
namespace GJ.Para.ATE
{
    public class CSysPara:CSysBase
    {

        #region 字段
        public string plcIp = "192.168.3.100";
        public int plcPort = 502;
        public string serIP = "127.0.0.1";
        public int serPort = 8000;
        public bool chkIo = false;
        public string ioCom = "COM1";

        public int ate_Languge = 0;
        public string ate_title_name = "Chroma 8020 IDE - [Execution Control]";
        public string ate_result_folder = @"C:\Program Files\Chroma\Chroma 8020\Log";
        public string ate_scanSn_name = "BarCode Reader";

        public bool ate_scanSn_Enable = false;
        public int ate_result_delay = 100;
        public int ate_result_repeats = 3;
        /// <summary>
        /// ATE不良报警次数
        /// </summary>
        public int ATEFailTimes = 0;

        public bool chkSaveTcpLog = false;
        public int monDelayMs = 200;

        //0:ATE控制2个工位;1:ATE1控制工位1;2:ATE2控制工位2;
        public int statATEMode = 0;

        /// <summary>
        /// 大功率模式，需要把2通道并联
        /// </summary>
        public bool ATEHPowerModel = false;

        //Relay 测试
        public bool ChkRelay = false;
        public string acCOM = "COM2";
        public double ACStartVmax = 0;
        public double DCVmin = 0;
        public double DCVmax = 0;
        public double ACVmin = 0;
        public double ACVmax = 0;
        public int RelayDelayMs = 1000;

        public bool conMes = false;
        public bool ChkMesFail = false;
        #endregion

        public static CSysPara mVal = new CSysPara();

    }
}
