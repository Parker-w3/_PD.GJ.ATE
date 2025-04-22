using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;
namespace GJ.Para.BURNIN
{
    /// <summary>
    /// 快充模式
    /// </summary>
    public enum EQCV
    { 
      QC2_0,
      QC3_0,
      MTK,
      海思
    }
    [Serializable]
    public class CDCVPara
    { 
      public string Vname;
      public double Vmin;
      public double Vmax;
      public double ISet;
      public double Imin;
      public double Imax;
      public int QC_VOLT;
    }

    [Serializable]
    public class COnOffSetting
    {
        /// <summary>
        /// 时间单位
        /// </summary>
        public bool chkSec;
        /// <summary>
        /// OnOff次数
        /// </summary>
        public int OnOffTime;
        /// <summary>
        /// On时间(S)
        /// </summary>
        public int OnTime;
        /// <summary>
        /// Off时间(S)
        /// </summary>
        public int OffTime;
        /// <summary>
        /// 快充电压
        /// </summary>
        public int QC_VOLT;       
    }

    [Serializable]
    public class CModelPara:CModelBase 
    {
        #region 字段

        public int ACV = 0;
        public double BITime = 0;        
       
        public double TSet = 0;
        public double TLP = 0;
        public double THP = 0;
        public double THAlarm = 0;
        public double TOPEN = 0;
        public double TCLOSE = 0;
        /// <summary>
        /// 输出组数
        /// </summary>
        public int DCVNum = 0;

        /// <summary>
        /// 输出集合
        /// </summary>
        public List<CDCVPara> DCVList = new List<CDCVPara>();  

        /// <summary>
        /// ON/OFF时序段数
        /// </summary>
        public int OnOffSeqNum = 0;

        /// <summary>
        /// OnOff集合
        /// </summary>
        public List<COnOffSetting> OnOffList = new List<COnOffSetting>();

        /// <summary>
        /// 快充模式
        /// </summary>
        public EQCV QC_TYPE = EQCV.QC2_0; 

        #endregion

    }
}
