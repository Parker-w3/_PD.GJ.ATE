using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;  
namespace GJ.Para.BURNIN
{
    public class CSysPara:CSysBase
    {

        #region 字段

        public string plcIp = "127.0.0.1";
        public int plcPort = 502;
        public string plcTempIp = "127.0.0.1";
        public int plcTempPort = 502;

        public string idCom = "COM1";
        public string[] monCom = new string [] {"COM2","COM3","COM4"};
        public string[] ersCom = new string [] {"COM5","COM6"};
        public string mySqlIp = "127.0.0.1";
        public bool conMes = false;

        /// <summary>
        /// 不判断电流
        /// </summary>
        public bool chkNoJugdeCur = false;
        /// <summary>
        /// 不锁住当机
        /// </summary>
        public bool chkNoLockFail = false;
        public double VLP = 0;
        public double VHP = 0;
        public double ILP = 0;
        public double IHP = 0;
        public double PwrLimit = 45;
        public double IOFFSET = 0.15;
        /// <summary>
        /// 不良次数
        /// </summary>
        public int failTimes = 0;
        /// <summary>
        /// 针盘使用寿命
        /// </summary>
        public int fixTimes = 0;
        public int comFailTimes = 0;

        /// <summary>
        /// 手动进机位置
        /// </summary>
        public bool chkHandIn = false;
        /// <summary>
        /// 检测MTK电压
        /// </summary>
        public bool chkgetMTK = false;
        /// <summary>
        /// MTK升降次数
        /// </summary>
        public int getMTKCoutn = 10;
        /// <summary>
        /// 通用加载模式
        /// </summary>
        public bool comLoad = false;
        /// <summary>
        /// QC重启复位
        /// </summary>
        public int C_RESET_QCV_TIMES = 1;
        #endregion

        public static CSysPara mVal = new CSysPara();
    }
}
