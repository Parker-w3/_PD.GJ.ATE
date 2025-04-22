using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;
namespace GJ.Para.LOADUP
{
    [Serializable]
    public class CModelPara:CModelBase 
    {
        #region 字段

        public int acv = 0;
        public double acvMin = 0;
        public double acvMax = 0;

        public string vName;
        public double vMin;
        public double vMax;

        public int loadMode;
        public double loadVon;

        public double loadSet;
        public double loadMin;
        public double loadMax;

        #endregion

    }
}
