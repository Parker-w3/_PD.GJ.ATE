using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;
namespace GJ.Para.UNLOAD
{
    [Serializable]
    public class CModelPara:CModelBase 
    {
        #region 字段

     
        public double LCRVolt;
        public int LCRHZ;

        public string LCRTestMode;//测试模式
        public string LCRMode;//触发模式

        public double CCMin;
        public double CCMax;
        public double RRMin;
        public double RRMax;
        /// <summary>
        /// LCR电阻挡位
        /// </summary>
        public string LCRRmax="100000";
        /// <summary>
        /// LCR测试速度
        /// </summary>
        public string LCRSpeed="FAST";

        #endregion

    }
}
