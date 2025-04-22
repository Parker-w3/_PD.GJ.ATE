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
namespace GJ.Para.Udc.TURNON
{
    public partial class udcTurnOnData : UserControl
    {
        # region 面板回调函数
        public udcTurnOnData()
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
            labP = new Label[] {
                               LabP1,LabP2,LabP3,LabP4,LabP5,LabP6,LabP7,LabP8 
                               };
            labID = new Label[] {
                               LabID1,LabID2,LabID3,LabID4,LabID5,LabID6,LabID7,LabID8 
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

        private double[] Vmin = new double[3];
        private double[] Vmax = new double[3];
        private double[] Imin = new double[3];
        private double[] Imax = new double[3];

        private double[] IDmax = new double[3];
        private double[] IDmin = new double[3];

        private bool ChkA = false;
        private Label[] labV = null;
        private Label[] labA = null;
        private Label[] labID = null;
        private Label[] labP = null;
        private bool ChkID = false;
        #endregion




        #region 属性

        public double[] mVmin
        {
            set { Vmin = value; }
        }
        public double[] mVmax
        {
            set { Vmax = value; }
        }
        public double[] mImin
        {
            set { Imin = value; }
        }
        public double[] mImax
        {
            set { Imax = value; }
        }
        public double[] mIDmax
        {
            set { IDmax = value; }
        }
        public double[] mIDmin
        {
            set { IDmin = value; }
        }
        public bool mChkA
        {
            set { ChkA = value; }
        }

        public bool mChkID
        {
            set { ChkID = value; }
        }
        #endregion

        #region 委托

        private delegate void SetClrHandler();
        private delegate void ShowDataHandler(List<string> serialNo, List<double> V, List<double> A, List<double> ID, int step,int Fixstep);
        private delegate void ShowDataTypeCHandler(List<string> serialNo, List<double> V, List<double> A, int step, int Fixstep, bool TwoLoad);
        private delegate void ShowDataTwoLoadHandler(List<string> serialNo, List<double> V, List<double> A, int step, int Fixstep);
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
                    labID[i].Text = "";
                    labV[i].Text = "";
                    labP[i].Text = "";
                    labV[i].ForeColor = Color.Black;
                    labA[i].ForeColor = Color.Black;
                    labID[i].ForeColor = Color.Black;
                    labP[i].ForeColor = Color.Black;
                }
                labNameID.Text = "";
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
        public void ShowData(List<string> serialNo, List<double> V, List<double> A, List<double> ID,int step,int Fixstep)
        {
            if (this.InvokeRequired)
                this.Invoke(new ShowDataHandler(ShowData), serialNo, V, A, ID,step,Fixstep);
            else
            {
                
              //  for (int i = 0; i < serialNo.Count; i++)   
                for (int i = Fixstep * 4; i < (Fixstep+1) * 4; i++)   
                {
                    if (serialNo[i] == "")
                    {
                        labV[i].Text = "---";
                        labA[i].Text = "---";
                        labID[i].Text = "---";
                        labP[i].Text = "___";
                        labV[i].ForeColor = Color.Black;
                        labA[i].ForeColor = Color.Black;
                        labID[i].ForeColor = Color.Black;
                        labP[i].ForeColor = Color.Black;
  
                    }
                    else
                    {
                        double mP = 0;
                        V[i] = System.Convert.ToDouble(V[i].ToString("0.00"));
                        A[i] = System.Convert.ToDouble(A[i].ToString("0.00"));
                        ID[i] = System.Convert.ToDouble(ID[i].ToString("0.00"));
                 
                        mP = System.Convert.ToDouble ((V[i] * A[i]).ToString("0.00"));
                        labV[i].Text = labV[i].Text + "第" + (step + 1).ToString() + "步电压: " + V[i].ToString("00.00")  + "\r";
                        labA[i].Text = labA[i].Text + "第" + (step + 1).ToString() + "步电流: " + A[i].ToString("00.00")  + "\r";
                        labP[i].Text = labP[i].Text + "第" + (step + 1).ToString() + "步功率：" + mP.ToString("00.00") + "\r";
                        if (ChkID)
                        {
                            labID[i].Text = labID[i].Text + "第" + (step + 1).ToString() + "步电压: " + ID[i].ToString("00.00") + "\r";
                            labNameID.Text = "ID电压(V)";
                        }

                        labP[i].ForeColor = Color.Blue;
                        if (V[i] < Vmin[step] || V[i] > Vmax[step])
                        {
                            labV[i].ForeColor = Color.Red;
                        }
                        else
                        {
                            if (labV[i].ForeColor == Color.Black)
                            labV[i].ForeColor = Color.Blue;
                        }
                        if (ChkA)
                        {
                            if (A[i] < Imin[step] || A[i] > Imax[step])
                            {
                                labA[i].ForeColor = Color.Red;
                            }
                            else
                            {
                                if (labA[i].ForeColor == Color.Black)
                                    labA[i].ForeColor = Color.Blue;
                            }
                        }
                        else
                            labA[i].ForeColor = Color.Blue;

                        if (ChkID)
                        {
                            if (ID[i] < IDmin[step] || ID[i] > IDmax[step])
                            {
                                labID[i].ForeColor = Color.Red;
                            }
                            else
                            {
                                if (labID[i].ForeColor == Color.Black)
                                    labID[i].ForeColor = Color.Blue;
                            }
                        }
 
                    }
                }
                this.Refresh();
            }
        }


        /// <summary>
        /// 刷新TypeC测试结果
        /// </summary>
        /// <param name="serialNo"></param>
        /// <param name="V"></param>
        /// <param name="A"></param>
        /// <param name="step"></param>
        /// <param name="Fixstep"></param>
        /// <param name="TwoLoad"></param>
        public void ShowDataTypeC(List<string> serialNo, List<double> V, List<double> A,  int step, int Fixstep,bool TwoLoad)
        {
            if (this.InvokeRequired)
                this.Invoke(new ShowDataTypeCHandler(ShowDataTypeC), serialNo, V, A, step, Fixstep, TwoLoad);
            else
            { 
                for (int i = Fixstep * 4; i < (Fixstep + 1) * 4; i++)
                {
                    if (serialNo[i] == "")
                    {
                        labV[i].Text = "---";
                        labA[i].Text = "---";
                        labID[i].Text = "---";
                        labP[i].Text = "___";
                        labV[i].ForeColor = Color.Black;
                        labA[i].ForeColor = Color.Black;
                        labID[i].ForeColor = Color.Black;
                        labP[i].ForeColor = Color.Black;

                    }
                    else
                    {
                        double mP = 0;
                        V[i] = System.Convert.ToDouble(V[i].ToString("0.00"));
                        A[i] = System.Convert.ToDouble(A[i].ToString("0.00"));
                        mP = System.Convert.ToDouble((V[i] * A[i]).ToString("0.00"));
                        labV[i].Text = labV[i].Text + "第" + (step + 1).ToString() + "步电压: " + V[i].ToString("00.00") + "\r";
                        labA[i].Text = labA[i].Text + "第" + (step + 1).ToString() + "步电流: " + A[i].ToString("00.00") + "\r";
                        labP[i].Text = labP[i].Text + "第" + (step + 1).ToString() + "步功率：" + mP.ToString("00.00") + "\r";
          
                        labP[i].ForeColor = Color.Blue;
                        if (!TwoLoad)
                        {
                            if (V[i] < Vmin[step] || V[i] > Vmax[step])
                            {
                                labV[i].ForeColor = Color.Red;
                            }
                            else
                            {
                                if (labV[i].ForeColor == Color.Black)
                                    labV[i].ForeColor = Color.Blue;
                            }
                            if (ChkA)
                            {
                                if (A[i] < Imin[step] || A[i] > Imax[step])
                                {
                                    labA[i].ForeColor = Color.Red;
                                }
                                else
                                {
                                    if (labA[i].ForeColor == Color.Black)
                                        labA[i].ForeColor = Color.Blue;
                                }
                            }
                            else
                                labA[i].ForeColor = Color.Blue;
                        }
                        else
                        {
                            if (V[i] < Vmin[i % 2] || V[i] > Vmax[i % 2])
                            {
                                labV[i].ForeColor = Color.Red;
                            }
                            else
                            {
                                if (labV[i].ForeColor == Color.Black)
                                    labV[i].ForeColor = Color.Blue;
                            }
                            if (ChkA)
                            {
                                if (A[i] < Imin[i % 2] || A[i] > Imax[i % 2])
                                {
                                    labA[i].ForeColor = Color.Red;
                                }
                                else
                                {
                                    if (labA[i].ForeColor == Color.Black)
                                        labA[i].ForeColor = Color.Blue;
                                }
                            }
                            else
                                labA[i].ForeColor = Color.Blue;

                        }


                    }
                }
                this.Refresh();
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
        public void ShowDataTwoLoad(List<string> serialNo, List<double> V, List<double> A, int step, int Fixstep)
        {
            if (this.InvokeRequired)
                this.Invoke(new ShowDataTwoLoadHandler(ShowDataTwoLoad), serialNo, V, A, step, Fixstep);
            else
            { 
                for (int i = Fixstep * 4; i < (Fixstep + 1) * 4; i++)
                {
                    if (serialNo[i] == "")
                    {
                        labV[i].Text = "---";
                        labA[i].Text = "---";
                        labID[i].Text = "---";
                        labP[i].Text = "___";
                        labV[i].ForeColor = Color.Black;
                        labA[i].ForeColor = Color.Black;
                        labID[i].ForeColor = Color.Black;
                        labP[i].ForeColor = Color.Black;

                    }
                    else
                    {
                        double mP = 0;
                        V[i] = System.Convert.ToDouble(V[i].ToString("0.00"));
                        A[i] = System.Convert.ToDouble(A[i].ToString("0.00"));
                      

                        mP = System.Convert.ToDouble((V[i] * A[i]).ToString("0.00"));
                        labV[i].Text = labV[i].Text + "电压: " + V[i].ToString("00.00") + "\r";
                        labA[i].Text = labA[i].Text + "电流: " + A[i].ToString("00.00") + "\r";
                        labP[i].Text = labP[i].Text + "功率：" + mP.ToString("00.00") + "\r";
   
                        labP[i].ForeColor = Color.Blue;
                        if (V[i] < Vmin[i % 2] || V[i] > Vmax[i % 2])
                        {
                            labV[i].ForeColor = Color.Red;
                        }
                        else
                        {
                            if (labV[i].ForeColor == Color.Black)
                                labV[i].ForeColor = Color.Blue;
                        }
                        if (ChkA)
                        {
                            if (A[i] < Imin[i % 2] || A[i] > Imax[i % 2])
                            {
                                labA[i].ForeColor = Color.Red;
                            }
                            else
                            {
                                if (labA[i].ForeColor == Color.Black)
                                    labA[i].ForeColor = Color.Blue;
                            }
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
