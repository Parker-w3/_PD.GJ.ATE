using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.Para.Udc
{
    public partial class udcFixture : UserControl
    {
        #region 枚举
        public enum ESTATUS
        { 
         空闲,
         就绪,
         测试中,
         测试结束,
         异常报警,
         空治具
        }
        #endregion

        #region 字段
        private const int C_SLOT_NUM = 8;
        private Label[] labUUT = null;
        private Label[] labNo = null; 
        private Label[] labSn = null;
        private Label[] labResult = null;
        private bool clrFailWait = false;
        #endregion

        #region 属性
        public bool mClrFailWait
        {
            set { 
                 clrFailWait = value;
                 SetFailWait(clrFailWait);
                 }
            get { return clrFailWait; }
        }
        #endregion

        #region 构造函数
        public udcFixture()
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
            labUUT = new Label[] { labUUT1, labUUT2, labUUT3, labUUT4, labUUT5, labUUT6, labUUT7, labUUT8 };
            labNo = new Label[C_SLOT_NUM];
            labSn = new Label[C_SLOT_NUM];
            labResult = new Label[C_SLOT_NUM];
            for (int i = 0; i < labNo.Length; i++)
            {
                labNo[i] = new Label();
                labNo[i].Name = "labNo" + i.ToString();                 
                labNo[i].Dock = DockStyle.Fill;
                labNo[i].TextAlign = ContentAlignment.MiddleCenter;
                labNo[i].Margin = new Padding(0);
                labNo[i].Text = (i + 1).ToString("D2");

                labSn[i] = new Label();
                labSn[i].Name = "labSn" + i.ToString();
                labSn[i].Dock = DockStyle.Fill;
                labSn[i].TextAlign = ContentAlignment.MiddleCenter;
                labSn[i].Margin = new Padding(0);
                labSn[i].Text = "";

                labResult[i] = new Label();
                labResult[i].Name = "labResult" + i.ToString();
                labResult[i].Dock = DockStyle.Fill;
                labResult[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult[i].Margin = new Padding(0);
                labResult[i].Text = "";

                panelSn.Controls.Add(labNo[i], 0, i+1);
                panelSn.Controls.Add(labSn[i], 1, i + 1);
                panelSn.Controls.Add(labResult[i], 2, i + 1);  
   
            }

            //是否显示提示信息
            tlTip.Active =true;
            //是否显示提示信息，当窗体没有获得焦点时
            tlTip.ShowAlways = true;
            //工具提示”窗口显示之前，鼠标指针必须在控件内保持静止的时间（以毫秒计）
            tlTip.InitialDelay = 200;
            // 提示信息刷新时间 
            tlTip.ReshowDelay = 300;
            //提示信息延迟时间
            tlTip.AutomaticDelay = 200;
            // 提示信息弹出时间
            tlTip.AutoPopDelay = 10000;
            // 提示信息
            tlTip.ToolTipTitle = "产品信息";
        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel1, true, null);
            panel2.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel2, true, null);
            panel3.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel3, true, null);
            panel4.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel4, true, null);
            panel5.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel5, true, null);
            panelUUT.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panelUUT, true, null);
            panelSn.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panelSn, true, null);
        }
        #endregion       

        #region 面板回调函数
        private void udcFixture_Load(object sender, EventArgs e)
        {
            clrFailWait = false;
            SetFailWait(false);
        }
         private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否已确定不良,需继续下一步测试?", "确定不良", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                clrFailWait = false;
                SetFailWait(false);
            }
        }
        #endregion

        #region 委托
        private delegate void SetRunHandler(ESTATUS status,string idCard, List<string> serialNo, List<int> result,string alarmInfo);
        private delegate void SetToolTipHandler(List<string> uutInfo);
        private delegate void SetClrHandler();
        private delegate void SetStatusHandler(string status, bool bNG);
        private delegate void SetFailWaitHandler(bool clrFail);
        #endregion

        #region 方法
        /// <summary>
        /// 设置治具状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="idCard"></param>
        /// <param name="serialNo"></param>
        /// <param name="result"></param>
        /// <param name="alarmInfo"></param>
        public void SetRun(ESTATUS status,string idCard, List<string> serialNo, List<int> result,string alarmInfo="")
        {
            if (this.InvokeRequired)
                this.Invoke(new SetRunHandler(SetRun), status, idCard,serialNo, result,alarmInfo);
            else
            {
                switch (status)
                {
                    case ESTATUS.空闲:
                        panelUUT.Visible = false;
                        labIdCard.Text = "";
                        labStatus.Text = "等待治具到位..";
                        labStatus.ForeColor = Color.Blue;                         
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            labSn[i].Text = "";
                            labResult[i].Text = "";
                        }                                                   
                        break;
                    case ESTATUS.空治具:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Blue; 
                        labStatus.Text = "空治具准备过站..";
                        labStatus.ForeColor = Color.Blue;                         
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            labSn[i].Text = "";
                            labResult[i].Text = "";
                            labUUT[i].ImageKey = null;
                        }
                        panelUUT.Visible = true;
                        break;
                    case ESTATUS.就绪:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Blue; 
                        labStatus.Text = "治具到位就绪,等待测试..";
                        labStatus.ForeColor = Color.Blue;                        
                        for (int i = 0; i < serialNo.Count; i++)
                        {                            
                            if (serialNo[i] == "")
                                labUUT[i].ImageKey = null;
                            else
                                labUUT[i].ImageKey = "READY";
                            labSn[i].Text = serialNo[i];
                            labSn[i].ForeColor = Color.Black; 
                            labResult[i].Text = "";
                        }
                        panelUUT.Visible = true;
                        break;
                    case ESTATUS.测试中:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Blue; 
                        labStatus.Text = "治具到位就绪,产品测试中..";
                        labStatus.ForeColor = Color.Blue;                        
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            if (serialNo[i] != "")
                                labUUT[i].ImageKey = "READY";
                            else
                                labUUT[i].ImageKey = null;
                            labResult[i].Text = "";
                        }
                        panelUUT.Visible = true;
                        break;
                    case ESTATUS.测试结束:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Blue;                        
                        bool uutPass = true;
                        for (int i = 0; i <  serialNo.Count; i++)
                        {
                            
                            labSn[i].Text = serialNo[i];
                            if (serialNo[i] != "")
                            {
                                if (result[i] == 0)
                                {
                                    labUUT[i].ImageKey = "PASS";
                                    labSn[i].ForeColor = Color.Blue;
                                    labResult[i].Text = "PASS";
                                    labResult[i].ForeColor = Color.Blue;
                                }
                                else
                                {
                                    labUUT[i].ImageKey = "FAIL";
                                    labSn[i].ForeColor = Color.Red;
                                    labResult[i].Text = "FAIL";
                                    labResult[i].ForeColor = Color.Red;
                                    uutPass = false;
                                }
                            }
                            else
                            {
                                labUUT[i].ImageKey = null;
                                labSn[i].ForeColor = Color.Black;
                                labResult[i].Text = "";                            
                            }
                        }
                        if (uutPass)
                        {
                            labStatus.Text = "测试结果:PASS,准备过站..";
                            labStatus.ForeColor = Color.Blue;
                        }
                        else
                        {
                            labStatus.Text = "测试结果:FAIL,请检查..";
                            labStatus.ForeColor = Color.Red;
                        }
                        panelUUT.Visible = true;
                        break;
                    case ESTATUS.异常报警:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Red;                        
                        for (int i = 0; i <  serialNo.Count; i++)
                        {
                            labSn[i].Text = serialNo[i];
                            if (serialNo[i] == "")
                            {
                                labUUT[i].ImageKey = null;
                                labSn[i].ForeColor = Color.Black;
                                labResult[i].Text = "";
                            }                                
                            else
                            {
                                if (result[i] == 0)
                                {
                                    labUUT[i].ImageKey = "PASS";
                                    labSn[i].ForeColor = Color.Blue;
                                    labResult[i].Text = "PASS";
                                    labResult[i].ForeColor = Color.Blue; 
                                }                                    
                                else
                                {
                                    labUUT[i].ImageKey = "FAIL";
                                    labSn[i].ForeColor = Color.Red;
                                    labResult[i].Text = "FAIL";
                                    labResult[i].ForeColor = Color.Red;
                                }                                    
                            }                            
                        }
                        labStatus.Text = alarmInfo;
                        labStatus.ForeColor = Color.Red;
                        panelUUT.Visible = true;
                        break;
                    default:
                        break;
                }
                this.Refresh();
            }
        }
        /// <summary>
        /// 设置提示信息
        /// </summary>
        /// <param name="uutInfo"></param>
        public void SetToolTip(List<string> uutInfo)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetToolTipHandler(SetToolTip), uutInfo);
            else
            {
                tlTip.RemoveAll();
                for (int i = 0; i < uutInfo.Count; i++)
                {
                    if (uutInfo[i] != "")
                        tlTip.SetToolTip(labUUT[i], uutInfo[i]);
                }                    
            }           
        }
        /// <summary>
        /// 置空闲状态
        /// </summary>
        public void SetClr()
        {
            if (this.InvokeRequired)
                this.Invoke(new SetClrHandler(SetClr));
            else
            {
                labIdCard.Text = "";
                labStatus.Text = "等待治具到位..";
                labStatus.ForeColor = Color.Blue;
                panelUUT.Visible = false;
                for (int i = 0; i <labSn.Length; i++)
                {
                    labSn[i].Text = "";
                    labResult[i].Text = "";
                }
                tlTip.RemoveAll();
            }         
        }
        /// <summary>
        /// 设置测试状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="bNG"></param>
        public void SetStatus(string status, bool bNG = false)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetStatusHandler(SetStatus), status, bNG);
            else
            {
                labStatus.Text = status;
                if(!bNG)
                  labStatus.ForeColor = Color.Blue; 
                else
                  labStatus.ForeColor = Color.Red; 
            }
        }
        /// <summary>
        /// 不良确定
        /// </summary>
        /// <param name="clrFail"></param>
        public void SetFailWait(bool clrFail)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetFailWaitHandler(SetFailWait), clrFail);
            else
            {
                if (!clrFail)
                {
                    panel3.ColumnStyles[3].SizeType = SizeType.Absolute;
                    panel3.ColumnStyles[3].Width = 2;
                    btnOK.Visible = false;  
                }
                else
                {
                    panel3.ColumnStyles[3].SizeType = SizeType.Absolute;
                    panel3.ColumnStyles[3].Width = 60;
                    btnOK.Visible = true;  
                }
            }          
        }
        #endregion
    }
}
