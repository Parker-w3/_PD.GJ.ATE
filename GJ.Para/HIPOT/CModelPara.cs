using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;
using GJ.Dev.HIPOT;
namespace GJ.Para.HIPOT
{
    [Serializable]
    public class CModelPara:CModelBase 
    {
        #region 字段
        public List<int> uutHpCH = new List<int>();
        public List<int> uutIoCH = new List<int>();
        public List<CHPSetting.CStep> step = new List<CHPSetting.CStep>();  
        #endregion

    }
}
