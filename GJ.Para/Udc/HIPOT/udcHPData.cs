using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Dev.HIPOT; 
using GJ.Para.HIPOT;

namespace GJ.Para.Udc.HIPOT
{
    public partial class udcHPData : UserControl
    {
        #region 构造函数
        public udcHPData(int slotMax=8)
        {

            this.slotMax = slotMax;

            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

           
        }

        #region 字段
        private int slotMax = 8;
        #endregion
       
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {          
            for (int i = 0; i < labValList.Length; i++)
                labValList[i] = new List<Label>();              
        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {

            splitContainer1.Panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(splitContainer1.Panel1, true, null);
            splitContainer1.Panel2.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(splitContainer1.Panel2, true, null);
        }
 
        #endregion

        #region 面板控件
        private TableLayoutPanel[] panelUUT = new TableLayoutPanel[2];
        private List<Label>[] labValList = new List<Label>[16]; 
        private List<Label> labResultList = new List<Label>();        
        #endregion

        #region 面板回调函数
        private void udcHPData_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region 私有方法
        
        #endregion

        #region 委托
        private delegate void clrDataHandler();
        private delegate void setTestValHandler(List<string> serialNo,List<CHPSetting.CStepVal> stepValList,int idNo, int HipotMax);
        #endregion

        #region 共享方法
        /// <summary>
        /// 刷新界面
        /// </summary>
        /// <param name="step"></param>
        public void refreshUI(List<CHPSetting.CStep> step)
        {

            int chanNum = 8;

            for (int i = 0; i < labValList.Length; i++)            
                labValList[i].Clear();
            labResultList.Clear();

            for (int i = 0; i < panelUUT.Length; i++)
            {
                if(panelUUT[i]!=null)
                {
                    panelUUT[i].Dispose();
                    panelUUT[i] = null;
                }
                panelUUT[i] = new TableLayoutPanel();
                panelUUT[i].Dock = DockStyle.Fill;
                panelUUT[i].CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
                panelUUT[i].GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panelUUT[i], true, null);
            }

            if (slotMax == 8)
                panelUUT[1].Visible = false;

            int stepNo = step.Count;

            for (int index = 0; index < 2; index++)           
            {
                panelUUT[index].RowCount = stepNo + 3;
                for (int i = 0; i < stepNo+2; i++)
                     panelUUT[index].RowStyles.Add(new RowStyle(SizeType.Absolute,24));
                panelUUT[index].RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                panelUUT[index].ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,80));
                for (int i = 0; i < chanNum; i++)
                    panelUUT[index].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

                Label labid = new Label();
                labid.Dock = DockStyle.Fill;
                labid.Margin = new Padding(0); 
                labid.BackColor = Color.Turquoise;
                labid.TextAlign = ContentAlignment.MiddleCenter;
                labid.Font = new Font("宋体", 12);
                labid.Text = "StepNo";
                panelUUT[index].Controls.Add(labid, 0, 0);

                for (int i = 0; i < chanNum; i++)
                {
                    Label labCH = new Label();
                    labCH.Dock = DockStyle.Fill;
                    labCH.Margin = new Padding(0); 
                    labCH.BackColor = Color.Turquoise;
                    labCH.TextAlign = ContentAlignment.MiddleCenter;
                    labCH.Font = new Font("宋体", 12);
                    labCH.Text = "CH" + (i + index * chanNum + 1).ToString("D2");
                    panelUUT[index].Controls.Add(labCH, i+1, 0); 
                }
            }

            for (int index = 0; index < 2; index++)
            {
                for (int i = 0; i < stepNo; i++)
                {
                    Label labStep = new Label();
                    labStep.Dock = DockStyle.Fill;
                    labStep.TextAlign = ContentAlignment.MiddleCenter;
                    labStep.Margin = new Padding(0);
                    labStep.Font = new Font("宋体", 12);
                    labStep.Text = step[i].name.ToString();
                    panelUUT[index].Controls.Add(labStep, 0, i + 1);

                    for (int CH = 0; CH < chanNum; CH++)
                    {
                        Label labVal = new Label();
                        labVal.Dock = DockStyle.Fill;
                        labVal.TextAlign = ContentAlignment.MiddleCenter;
                        labVal.Margin = new Padding(0);
                        labVal.Font = new Font("宋体", 12);
                        labVal.BackColor = Color.White;
                        labVal.Text ="";
                        labValList[CH + index * chanNum].Add(labVal);
                        panelUUT[index].Controls.Add(labValList[CH + index * chanNum][i], CH + 1, i + 1);
                    }
                }

                if (stepNo > 0)
                {
                    Label labResultId = new Label();
                    labResultId.Dock = DockStyle.Fill;
                    labResultId.TextAlign = ContentAlignment.MiddleCenter;
                    labResultId.Margin = new Padding(0);
                    labResultId.Font = new Font("宋体", 12);
                    labResultId.Text = "Result";
                    panelUUT[index].Controls.Add(labResultId, 0, stepNo + 1);

                    for (int CH = 0; CH < chanNum; CH++)
                    {
                        Label labResult = new Label();
                        labResult.Dock = DockStyle.Fill;
                        labResult.TextAlign = ContentAlignment.MiddleCenter;
                        labResult.Margin = new Padding(0);
                        labResult.Font = new Font("宋体", 12);
                        labResult.BackColor = Color.White;
                        labResult.Text = "";
                        labResultList.Add(labResult);
                        panelUUT[index].Controls.Add(labResultList[CH + index * chanNum], CH + 1, stepNo + 1);
                    }
                }              
            }

            splitContainer1.Panel1.Controls.Add(panelUUT[0]);
            splitContainer1.Panel2.Controls.Add(panelUUT[1]);  
        }
        /// <summary>
        /// 初始化UI
        /// </summary>
        public void clrData()
        {
            if (this.InvokeRequired)
                this.Invoke(new clrDataHandler(clrData));
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < labValList[i].Count; j++)                    
                        labValList[i][j].Text = "";                    
                }
                for (int i = 0; i < labResultList.Count; i++)
                    labResultList[i].Text = "";
            }
        }
        /// <summary>
        /// 设置测试数据
        /// </summary>
        /// <param name="stepValList"></param>
        public void setTestVal(List<string> serialNo,List<CHPSetting.CStepVal> stepValList,int idNo,int HipotMax)
        {
            if (this.InvokeRequired)
                this.Invoke(new setTestValHandler(setTestVal), serialNo, stepValList, idNo, HipotMax);
            else
            {
                //int mFixCount = 0;
                //if (HipotMax == 1)
                //    mFixCount = 1;
                //for (int uutNo = 0 + idNo * 4; uutNo < idNo * 4 + 4 + mFixCount * 4; uutNo++)
          
                //{
                    int uutNo = idNo;
                    //if (stepValList[uutNo].mVal.Count != labValList[uutNo].Count)
                    //    continue;

                    string failCode = string.Empty;
 
                    //for (int stepNo = 0; stepNo < labValList[uutNo].Count; stepNo++)
                    for (int stepNo = 0; stepNo < stepValList[uutNo].mVal.Count; stepNo++)
                    {                        
                        CHPSetting.EStepName stepName = stepValList[uutNo].mVal[stepNo].name; 
                        string stepVal = stepValList[uutNo].mVal[stepNo].value;
                        string stepUnit = stepValList[uutNo].mVal[stepNo].unit;
                        string stepShow = stepVal + stepUnit;                       
                        if (stepName == CHPSetting.EStepName.IR)
                        {
                            //if (stepVal >= 1000)
                            //    stepShow = "UUUUUU";
                           // stepShow = stepShow;
                        }
                        if (serialNo[uutNo] != string.Empty)
                        {
                            labValList[uutNo][stepNo].Text = stepShow;
                            if (stepValList[uutNo].mVal[stepNo].result == 0)
                                labValList[uutNo][stepNo].ForeColor = Color.Blue;
                            else
                            {
                                labValList[uutNo][stepNo].ForeColor = Color.Red;
                                failCode = stepValList[uutNo].mVal[stepNo].code;
                            }
                        }
                        else
                        {
                            labValList[uutNo][stepNo].Text = "";
                            labValList[uutNo][stepNo].ForeColor = Color.Black;
                        }
                    }
                    if (serialNo[uutNo] != string.Empty)
                    {
                        if (stepValList[uutNo].result == 0)
                        {
                            labResultList[uutNo].Text = "PASS";
                            labResultList[uutNo].ForeColor = Color.Blue;
                        }
                        else
                        {
                            labResultList[uutNo].Text = failCode;
                            labResultList[uutNo].ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        labResultList[uutNo].Text = "";
                        labResultList[uutNo].ForeColor = Color.Black;
                    }
               // }
            }
        }
        #endregion
    }
}
