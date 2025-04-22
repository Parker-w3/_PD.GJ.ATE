using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Para.Base;
namespace GJ.Para.TURNON
{
    [Serializable]
    public class CModelPara:CModelBase 
    {
        #region 字段

        public int acv;

        public string vName;
        public bool ChkID =false ; //测试ID功能
        public bool ChkTypeC=false ; //是否测试TypeC功能
        public bool ChkHightPower = false; //测试大功率（需双通道并联)
        public bool ChkTwoLoad = false; //测试双组输出产品
        public int TypeCSum = 0;//TypeC CC信号组数

        public double[] vMin =new double[3];
        public double[] vMax=new double[3];

        public int[] loadMode =new int[3];
        public double[] loadVon = new double[3];

        public double[] loadSet = new double[3];
        public double[] loadMin = new double[3];
        public double[] loadMax = new double[3];

        public double[] IDmin = new double[3];
        public double[] IDmax = new double[3];

        public double[] Pmin = new double[3];
        public double[] Pmax = new double[3];

        public bool[] ChkModel = new bool[3];
        #endregion

    }
}
