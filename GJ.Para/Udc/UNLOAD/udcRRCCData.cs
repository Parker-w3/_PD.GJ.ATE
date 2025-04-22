using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ;
using GJ.Para.TURNON;
//using GJ.Para.Udc.TURNON;
namespace GJ.Para.Udc.RRCC
{
    public partial class udcRRCCData : UserControl
    {
        # region 面板回调函数
        public udcRRCCData()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            labV = new Label[]{
                               labV1,labV2,labV3,labV4,labV5,labV6,labV7,labV8,
                               };
                                  
            labA = new Label[] {
                               LabA1,LabA2,LabA3,LabA4,LabA5,LabA6,LabA7,LabA8 
                               };
          
          
        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            Panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(Panel1, true, null);
        }

        # endregion

        #region 字段

        private double CCmin = 0;
        private double CCmax = 0;
        private double RRmin = 0;
        private double RRmax = 0;

        private double Pmax = 0;
        private double Pmin = 0;

        private Label[] labV = null;
        private Label[] labA = null;
       

        #endregion

        #region 属性

        public double mCCmin
        {
            set { CCmin = value; }
        }
        public double mCCmax
        {
            set { CCmax = value; }
        }
        public double mRRmin
        {
            set { RRmin = value; }
        }
        public double mRRmax
        {
            set { RRmax = value; }
        }
        public double mPmax
        {
            set { Pmax = value; }
        }
        public double mPmin
        {
            set { Pmin = value; }
        }
        #endregion

        #region 委托

        private delegate void SetClrHandler();
        private delegate void ShowDataHandler(List<string> serialNo, List<double> CC, List<double> RR);

        #endregion

        #region 方法

        /// <summary>
        /// 清除状态
        /// </summary>
        public void SetClr()
        {
            if (this.InvokeRequired)
                this.Invoke(new SetClrHandler(SetClr));
            else
            {
                for (int i = 0; i < labV.Length; i++)
                {
                
                    labA[i].Text = "";
                 
                    labV[i].Text = "";

                    labV[i].ForeColor = Color.Black;
                    labA[i].ForeColor = Color.Black;
                   
                }
             
            }
        }


        /// <summary>
        /// 刷新界面
        /// </summary>
        /// <param name="serialNo"></param>
        /// <param name="V"></param>
        /// <param name="I"></param>
        /// <param name="result"></param>
        /// <param name="testTimes"></param>
        public void ShowData(List<string> serialNo, List<double> CC, List<double> RR)
        {
            if (this.InvokeRequired)
                this.Invoke(new ShowDataHandler(ShowData), serialNo, CC, RR);
            else
            {
                
                for (int i = 0; i < serialNo.Count; i++)
                {
                    if (serialNo[i] == "")
                    {
                        labV[i].Text = "---";
                        labA[i].Text = "---";
                     
                    
                        labV[i].ForeColor = Color.Black;
                        labA[i].ForeColor = Color.Black;
                      
  
                    }
                    else
                    {
                        labV[i].Text = (CC[i] ).ToString();
                        labA[i].Text = RR[i].ToString();
                    
                   
                        if (CC[i] < CCmin || CC[i] > CCmax)
                        {
                            labV[i].ForeColor = Color.Red;
                        }
                        else
                            labV[i].ForeColor = Color.Blue;

                        if (RR[i] < RRmin || RR[i] > RRmax)
                        {
                            labA[i].ForeColor = Color.Red;
                        }
                        else
                            labA[i].ForeColor = Color.Blue;

                     
 
                    }
                }
                this.Refresh();
            }
        }
        #endregion
    }
}
