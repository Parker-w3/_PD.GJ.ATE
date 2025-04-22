using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.Para.Udc.UNLOAD
{
    public partial class udcUnload : UserControl
    {

        #region 参数常量
        private const int C_SLOT_MAX = 8;
        #endregion

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

        #region 面板控件
        private Label[] labUUT = null;
        private Label[] labNo = null;
        private Label[] labSn = null;
        private Label[] labResult0 = null; 
        private Label[] labResult1 = null;
        private Label[] labResult2 = null;
        private Label[] labResult3 = null;
        private Label[] labResult4 = null;
        private Label[] labResult5 = null;
        private Label[] labResult6 = null;
        private Label[] labResult7 = null;
        private Label[] labResult8 = null;
        private Label[] labResult9 = null;
        private Label[] labUUTttNum = null;
        private Label[] labUUTfailNum = null;
        private Label[] labIdttNum = null;
        private Label[] labIdFailNum = null;
        #endregion

        #region 字段
        private string iniFile=Application.StartupPath + "\\" + CGlobal.CFlow.flowGUID + ".ini";  
        private bool clrFailWait = false;
        private int[] ttNum = new int[10];
        private int[] failNum = new int[10]; 
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
        public udcUnload()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

            load();
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            labUUT = new Label[]{
                                 labUUT1,labUUT2,labUUT3,labUUT4,labUUT5,labUUT6,labUUT7,labUUT8
                                 };
            labUUTttNum = new Label[] {
                                        labLinePTTT , labLineBITT,labLineTURNONTT, labLineHPTT1,labLineHPTT2, 
                                        labLineAteTT1, labLineAteTT2,labLineAteTT3,labLineAteTT4,labLineRRCCTT
                                       };
            labUUTfailNum = new Label[] {labLinePTFail , labLineBIFail,labLineTURNONFail, labLineHPFail1,labLineHPFail2,
                                         labLineAteFail1,labLineAteFail2,labLineAteFail3,labLineAteFail4,labLineRRCCFail };
            labIdttNum = new Label[] {labPTTT,labBITT,labHPTT1,labHPTT2,labAteTT1,labAteTT2,labAteTT3 ,labAteTT4 ,labRRCCTT};
            labIdFailNum = new Label[] {labPTFail,labBIFail,labHPFail1,labHPFail2,labAteFail1,labAteFail2,labAteFail3,labAteFail4,labRRCCFail}; 

            labNo = new Label[C_SLOT_MAX];
            labSn = new Label[C_SLOT_MAX];
            labResult0 = new Label[C_SLOT_MAX];
            labResult1 = new Label[C_SLOT_MAX];
            labResult2 = new Label[C_SLOT_MAX];
            labResult3 = new Label[C_SLOT_MAX];
            labResult4 = new Label[C_SLOT_MAX];
            labResult5 = new Label[C_SLOT_MAX];
            labResult6 = new Label[C_SLOT_MAX];
            labResult7 = new Label[C_SLOT_MAX];
            labResult8 = new Label[C_SLOT_MAX];
            labResult9 = new Label[C_SLOT_MAX];
      
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

                labResult0[i] = new Label();
                labResult0[i].Name = "labResult0" + i.ToString();
                labResult0[i].Dock = DockStyle.Fill;
                labResult0[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult0[i].Margin = new Padding(1);
                labResult0[i].Text = "";

                labResult1[i] = new Label();
                labResult1[i].Name = "labResult1" + i.ToString();
                labResult1[i].Dock = DockStyle.Fill;
                labResult1[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult1[i].Margin = new Padding(1);
                labResult1[i].Text = "";

                labResult2[i] = new Label();
                labResult2[i].Name = "labResult2" + i.ToString();
                labResult2[i].Dock = DockStyle.Fill;
                labResult2[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult2[i].Margin = new Padding(1);
                labResult2[i].Text = "";

                labResult3[i] = new Label();
                labResult3[i].Name = "labResult3" + i.ToString();
                labResult3[i].Dock = DockStyle.Fill;
                labResult3[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult3[i].Margin = new Padding(1);
                labResult3[i].Text = "";


                labResult4[i] = new Label();
                labResult4[i].Name = "labResult4" + i.ToString();
                labResult4[i].Dock = DockStyle.Fill;
                labResult4[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult4[i].Margin = new Padding(1);
                labResult4[i].Text = "";

                labResult5[i] = new Label();
                labResult5[i].Name = "labResult5" + i.ToString();
                labResult5[i].Dock = DockStyle.Fill;
                labResult5[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult5[i].Margin = new Padding(1);
                labResult5[i].Text = "";

                labResult6[i] = new Label();
                labResult6[i].Name = "labResult6" + i.ToString();
                labResult6[i].Dock = DockStyle.Fill;
                labResult6[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult6[i].Margin = new Padding(1);
                labResult6[i].Text = "";

                labResult7[i] = new Label();
                labResult7[i].Name = "labResult7" + i.ToString();
                labResult7[i].Dock = DockStyle.Fill;
                labResult7[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult7[i].Margin = new Padding(1);
                labResult7[i].Text = "";

                labResult8[i] = new Label();
                labResult8[i].Name = "labResult8" + i.ToString();
                labResult8[i].Dock = DockStyle.Fill;
                labResult8[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult8[i].Margin = new Padding(1);
                labResult8[i].Text = "";

                labResult9[i] = new Label();
                labResult9[i].Name = "labResult9" + i.ToString();
                labResult9[i].Dock = DockStyle.Fill;
                labResult9[i].TextAlign = ContentAlignment.MiddleCenter;
                labResult9[i].Margin = new Padding(1);
                labResult9[i].Text = "";


                panelSn.Controls.Add(labNo[i], 0, i+1);
                panelSn.Controls.Add(labSn[i], 1, i + 1);
                panelSn.Controls.Add(labResult0[i], 2, i + 1);  
                panelSn.Controls.Add(labResult1[i], 3, i + 1);
                panelSn.Controls.Add(labResult2[i], 4, i + 1);
                panelSn.Controls.Add(labResult3[i], 5, i + 1);
                panelSn.Controls.Add(labResult4[i], 6, i + 1);
                panelSn.Controls.Add(labResult5[i], 7, i + 1);
                panelSn.Controls.Add(labResult6[i], 8, i + 1);
                panelSn.Controls.Add(labResult7[i], 9, i + 1);
                panelSn.Controls.Add(labResult8[i], 10, i + 1);
                panelSn.Controls.Add(labResult9[i], 11, i + 1);  
   
   
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
            //panel6.GetType().GetProperty("DoubleBuffered",
            //                              System.Reflection.BindingFlags.Instance |
            //                              System.Reflection.BindingFlags.NonPublic)
            //                              .SetValue(panel6, true, null);
            panel7.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel7, true, null);
            panel8.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel8, true, null);
            panelUUT.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panelUUT, true, null);
            panelSn.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panelSn, true, null);
            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(tableLayoutPanel1, true, null);
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void load()
        {

            string strVal = string.Empty;

            strVal = CIniFile.ReadFromIni("Parameter", "PTTTNum", iniFile);
            if (strVal == "")
                ttNum[0] = 0;
            else
                ttNum[0] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "BITTNum", iniFile);
            if (strVal == "")
                ttNum[1] = 0;
            else
                ttNum[1] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "TURNONTTNum", iniFile);
            if (strVal == "")
                ttNum[2] = 0;
            else
                ttNum[2] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "HPTTNum1", iniFile);
            if (strVal == "")
                ttNum[3] = 0;
            else
                ttNum[3] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "HPTTNum2", iniFile);
            if (strVal == "")
                ttNum[4] = 0;
            else
                ttNum[4] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "ATETTNum1", iniFile);
            if (strVal == "")
                ttNum[5] = 0;
            else
                ttNum[5] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "ATETTNum2", iniFile);
            if (strVal == "")
                ttNum[6] = 0;
            else
                ttNum[6] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "ATETTNum3", iniFile);
            if (strVal == "")
                ttNum[7] = 0;
            else
                ttNum[7] = System.Convert.ToInt32(strVal);


            strVal = CIniFile.ReadFromIni("Parameter", "ATETTNum4", iniFile);
            if (strVal == "")
                ttNum[8] = 0;
            else
                ttNum[8] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "RRCCTTNum", iniFile);
            if (strVal == "")
                ttNum[9] = 0;
            else
                ttNum[9] = System.Convert.ToInt32(strVal);

    
       

            strVal = CIniFile.ReadFromIni("Parameter", "PTFailNum", iniFile);
            if (strVal == "")
                failNum[0] = 0;
            else
                failNum[0] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "BIFailNum", iniFile);
            if (strVal == "")
                failNum[1] = 0;
            else
                failNum[1] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "TURNONFailNum", iniFile);
            if (strVal == "")
                failNum[2] = 0;
            else
                failNum[2] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "HPFailNum1", iniFile);
            if (strVal == "")
                failNum[3] = 0;
            else
                failNum[3] = System.Convert.ToInt32(strVal);


            strVal = CIniFile.ReadFromIni("Parameter", "HPFailNum2", iniFile);
            if (strVal == "")
                failNum[4] = 0;
            else
                failNum[4] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "ATEFailNum1", iniFile);
            if (strVal == "")
                failNum[5] = 0;
            else
                failNum[5] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "ATEFailNum2", iniFile);
            if (strVal == "")
                failNum[6] = 0;
            else
                failNum[6] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "ATEFailNum3", iniFile);
            if (strVal == "")
                failNum[7] = 0;
            else
                failNum[7] = System.Convert.ToInt32(strVal);


            strVal = CIniFile.ReadFromIni("Parameter", "ATEFailNum4", iniFile);
            if (strVal == "")
                failNum[8] = 0;
            else
                failNum[8] = System.Convert.ToInt32(strVal);

            strVal = CIniFile.ReadFromIni("Parameter", "RRCCFailNum", iniFile);
            if (strVal == "")
                failNum[9] = 0;
            else
                failNum[9] = System.Convert.ToInt32(strVal);

      
        }
        #endregion       

        #region 面板回调函数
        private void udcUnload_Load(object sender, EventArgs e)
        {
            clrFailWait = false;
            SetFailWait(false);
            for (int i = 0; i < labUUTttNum.Length; i++)
            {
                labUUTttNum[i].Text = ttNum[i].ToString();
                labUUTfailNum[i].Text = failNum[i].ToString();
            }        
        }
        private void labClrUUT_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清除产能统计?", "产能统计", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                clrUUTNum();
            }
        }

        private void label10_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清除产能统计?", "产能统计", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                       == DialogResult.Yes)
            {
                clrUUTNum();
            }
        }

        private void labClrFix_DoubleClick(object sender, EventArgs e)
        {
            if (labIdCard.Text == "")
                return;
            string idCard = labIdCard.Text; 
            if (MessageBox.Show("确定要清除治具统计?", "治具统计", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                string er = string.Empty;

                if (GJ.Mes.CGJMES.clearStatTTNum(idCard, ref er))
                {
                    for (int i = 0; i < labIdttNum.Length; i++)
                    {
                        labIdttNum[i].Text = "0";
                        labIdFailNum[i].Text = "0";
                    }
                }                
            }
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
        private delegate void SetRunHandler(ESTATUS status, string idCard, List<int> statId, List<string> serialNo, List<int> result,int LastID, string alarmInfo);
        private delegate void SetToolTipHandler(List<string> uutInfo);
        private delegate void SetClrHandler();
        private delegate void SetStatusHandler(string status, bool bNG);
        private delegate void SetFailWaitHandler(bool clrFail);
        private delegate void SetIdNumHandler(List<int> ttNum, List<int> failNum);
        private delegate void clrUUTNumHandler();
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
        public void SetRun(ESTATUS status,string idCard,List<int> statId,List<string> serialNo, List<int> result,int LastID =10,string alarmInfo="")
        {
            if (this.InvokeRequired)
                this.Invoke(new SetRunHandler(SetRun), status, idCard, statId,serialNo, result, LastID,alarmInfo);
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
                            labResult0[i].Text = "";
                            labResult1[i].Text = "";
                            labResult2[i].Text = "";
                            labResult3[i].Text = "";
                            labResult4[i].Text = "";
                            labResult5[i].Text = "";
                            labResult6[i].Text = "";
                            labResult7[i].Text = "";
                            labResult8[i].Text = "";
                            labResult9[i].Text = "";
                         
                            labResult0[i].BackColor = Button.DefaultBackColor;
                            labResult1[i].BackColor = Button.DefaultBackColor;
                            labResult2[i].BackColor = Button.DefaultBackColor;
                            labResult3[i].BackColor = Button.DefaultBackColor;
                            labResult4[i].BackColor = Button.DefaultBackColor;
                            labResult5[i].BackColor = Button.DefaultBackColor;
                            labResult6[i].BackColor = Button.DefaultBackColor;
                            labResult7[i].BackColor = Button.DefaultBackColor;
                            labResult8[i].BackColor = Button.DefaultBackColor;
                            labResult9[i].BackColor = Button.DefaultBackColor;
                         
                        }
                        for (int i = 0; i < labIdttNum.Length; i++)
                        {
                            labIdttNum[i].Text = "0";
                            labIdFailNum[i].Text = "0";
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
                            labResult0[i].Text = "";
                            labResult1[i].Text = "";
                            labResult2[i].Text = "";
                            labResult3[i].Text = "";
                            labResult4[i].Text = "";
                            labResult5[i].Text = "";
                            labResult6[i].Text = "";
                            labResult7[i].Text = "";
                            labResult8[i].Text = "";
                            labResult9[i].Text = "";

                            labResult0[i].BackColor = Button.DefaultBackColor;
                            labResult1[i].BackColor = Button.DefaultBackColor;
                            labResult2[i].BackColor = Button.DefaultBackColor;
                            labResult3[i].BackColor = Button.DefaultBackColor;
                            labResult4[i].BackColor = Button.DefaultBackColor;
                            labResult5[i].BackColor = Button.DefaultBackColor;
                            labResult6[i].BackColor = Button.DefaultBackColor;
                            labResult7[i].BackColor = Button.DefaultBackColor;
                            labResult8[i].BackColor = Button.DefaultBackColor;
                            labResult9[i].BackColor = Button.DefaultBackColor;
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
                            labResult0[i].Text = "";
                            labResult1[i].Text = "";
                            labResult2[i].Text = "";
                            labResult3[i].Text = "";
                            labResult4[i].Text = "";
                            labResult5[i].Text = "";
                            labResult6[i].Text = "";
                            labResult7[i].Text = "";
                            labResult8[i].Text = "";
                            labResult9[i].Text = "";

                            labResult0[i].BackColor = Button.DefaultBackColor;
                            labResult1[i].BackColor = Button.DefaultBackColor;
                            labResult2[i].BackColor = Button.DefaultBackColor;
                            labResult3[i].BackColor = Button.DefaultBackColor;
                            labResult4[i].BackColor = Button.DefaultBackColor;
                            labResult5[i].BackColor = Button.DefaultBackColor;
                            labResult6[i].BackColor = Button.DefaultBackColor;
                            labResult7[i].BackColor = Button.DefaultBackColor;
                            labResult8[i].BackColor = Button.DefaultBackColor;
                            labResult9[i].BackColor = Button.DefaultBackColor;
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
                                labUUT[i].ImageKey = "TEST";
                            else
                                labUUT[i].ImageKey = null;
                            labResult0[i].Text = "";
                            labResult1[i].Text = "";
                            labResult2[i].Text = "";
                            labResult3[i].Text = "";
                            labResult4[i].Text = "";
                            labResult5[i].Text = "";
                            labResult6[i].Text = "";
                            labResult7[i].Text = "";
                            labResult8[i].Text = "";
                            labResult9[i].Text = "";

                            labResult0[i].BackColor = Button.DefaultBackColor;
                            labResult1[i].BackColor = Button.DefaultBackColor;
                            labResult2[i].BackColor = Button.DefaultBackColor;
                            labResult3[i].BackColor = Button.DefaultBackColor;
                            labResult4[i].BackColor = Button.DefaultBackColor;
                            labResult5[i].BackColor = Button.DefaultBackColor;
                            labResult6[i].BackColor = Button.DefaultBackColor;
                            labResult7[i].BackColor = Button.DefaultBackColor;
                            labResult8[i].BackColor = Button.DefaultBackColor;
                            labResult9[i].BackColor = Button.DefaultBackColor;
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
                            labSn[i].ForeColor = Color.Blue;  
                            if (serialNo[i] != "")
                            {
                                if (result[i] == 0)
                                {
                                    if (statId[i] == LastID)
                                    {
                                        labUUT[i].ImageKey = "PASS";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        if (LastID >= 3)
                                        SetLabPassFail(labResult2[i], "PASS");
                                        if (LastID >= 4)
                                        SetLabPassFail(labResult3[i], "PASS");
                                        if (LastID >= 5)
                                        SetLabPassFail(labResult4[i], "PASS");
                                        if (LastID >= 6)
                                        SetLabPassFail(labResult5[i], "PASS");
                                        if (LastID >= 7)
                                        SetLabPassFail(labResult6[i], "PASS");
                                        if (LastID >= 8)
                                        SetLabPassFail(labResult7[i], "PASS");
                                        if (LastID >=9)
                                        SetLabPassFail(labResult8[i], "PASS");
                                        if (LastID ==10)
                                           SetLabPassFail(labResult9[i], "PASS");
                                        for (int z = 0; z < LastID; z++)
                                        {
                                            ttNum[z]++;           
                                        }
                                      
                                    }
                                    else if (statId[i] == 9)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i], "PASS");
                                        SetLabPassFail(labResult6[i], "PASS");
                                        SetLabPassFail(labResult7[i], "PASS");
                                        SetLabPassFail(labResult8[i], "PASS");
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 9; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        uutPass = false; 
                                    }
                                    else if (statId[i] == 8)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i], "PASS");
                                        SetLabPassFail(labResult6[i], "PASS");
                                        SetLabPassFail(labResult7[i], "PASS");
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 8; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        uutPass = false; 
                                    }
                                    else if (statId[i] == 7)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i], "PASS");
                                        SetLabPassFail(labResult6[i], "PASS");
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 7; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        uutPass = false; 
                                    }
                                    else if (statId[i] == 6)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i], "PASS");
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 6; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        uutPass = false; 
                                    }

                                    else if (statId[i] == 5)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 5; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        uutPass = false;
                                    }

                                    else if (statId[i] == 4)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 4; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        uutPass = false;
                                    }

                                    else if (statId[i] == 3)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i]);
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 3; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        uutPass = false;
                                    }

                                    else if (statId[i] == 2)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 2; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        uutPass = false;
                                    }

                                    else if (statId[i] == 1)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i]);
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 1; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        uutPass = false;
                                    }
                                    else 
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i]);
                                        SetLabPassFail(labResult1[i]);
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                   
                                        uutPass = false;
                                    }
                                }
                                else
                                {
                                    labUUT[i].ImageKey = "FAIL";
                                    labSn[i].ForeColor = Color.Red;
                                    if (result[i] == 10) //RRCC
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i], "PASS");
                                        SetLabPassFail(labResult6[i], "PASS");
                                        SetLabPassFail(labResult7[i], "PASS");
                                        SetLabPassFail(labResult8[i], "PASS");
                                        SetLabPassFail(labResult9[i], "FAIL");
                                        for (int z = 0; z < 10; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[9]++;
                                    }
                                    else if (result[i] == 9) //ATE4
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i], "PASS");
                                        SetLabPassFail(labResult6[i], "PASS");
                                        SetLabPassFail(labResult7[i], "PASS");
                                        SetLabPassFail(labResult8[i], "FAIL");
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 9; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[8]++;
                                    }
                                    else if (result[i] == 8) //ATE3
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i], "PASS");
                                        SetLabPassFail(labResult6[i], "PASS");
                                        SetLabPassFail(labResult7[i], "FAIL");
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 8; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[7]++;
                                    }
                                    else if (result[i] == 7) //ATE2
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i], "PASS");
                                        SetLabPassFail(labResult6[i], "FAIL");
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 7; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[6]++;
                                    }
                                    else if (result[i] ==6) //ATE1
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "PASS");
                                        SetLabPassFail(labResult5[i], "FAIL");
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z <6; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[5]++;
                                    }
                                    else if (result[i] == 5) //HP2
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        SetLabPassFail(labResult4[i], "FAIL");
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 5; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[4]++;
                                    }
                                    else if (result[i] == 4) //HP1
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "FAIL");
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 4; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[3]++;
                                    }
                                    else if (result[i] == 3) //TURNON
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "FAIL");
                                        SetLabPassFail(labResult3[i]);
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 3; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[2]++;
                                    }
                                    else if (result[i] == 2) //BI
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "FAIL");
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 2; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[1]++;
                                    }
                                    else if (result[i] == 1) //PT
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i]);
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                        for (int z = 0; z < 1; z++)
                                        {
                                            ttNum[z]++;
                                        }
                                        failNum[0]++;
                                    }
                                    else 
                                    {
                                        SetLabPassFail(labResult0[i]);
                                        SetLabPassFail(labResult1[i]);
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        SetLabPassFail(labResult4[i]);
                                        SetLabPassFail(labResult5[i]);
                                        SetLabPassFail(labResult6[i]);
                                        SetLabPassFail(labResult7[i]);
                                        SetLabPassFail(labResult8[i]);
                                        SetLabPassFail(labResult9[i]);
                                      
                                    }
                                    uutPass = false;
                                }                                
                            }
                            else
                            {
                                labUUT[i].ImageKey = null;
                                labSn[i].ForeColor = Color.Black;
                                labResult0[i].Text = "";
                                labResult1[i].Text = "";
                                labResult2[i].Text = "";
                                labResult3[i].Text = "";
                                labResult4[i].Text = "";
                                labResult5[i].Text = "";
                                labResult6[i].Text = "";
                                labResult7[i].Text = "";
                                labResult8[i].Text = "";
                                labResult9[i].Text = "";

                                labResult0[i].BackColor = Button.DefaultBackColor;
                                labResult1[i].BackColor = Button.DefaultBackColor;
                                labResult2[i].BackColor = Button.DefaultBackColor;
                                labResult3[i].BackColor = Button.DefaultBackColor;
                                labResult4[i].BackColor = Button.DefaultBackColor;
                                labResult5[i].BackColor = Button.DefaultBackColor;
                                labResult6[i].BackColor = Button.DefaultBackColor;
                                labResult7[i].BackColor = Button.DefaultBackColor;
                                labResult8[i].BackColor = Button.DefaultBackColor;
                                labResult9[i].BackColor = Button.DefaultBackColor;
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
                        saveUUTNum();
                        panelUUT.Visible = true;
                        break;
                    case ESTATUS.异常报警:
                        labIdCard.Text = idCard;
                        labIdCard.ForeColor = Color.Red;
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            labSn[i].Text = serialNo[i];
                            labSn[i].ForeColor = Color.Blue;
                            if (serialNo[i] != "")
                            {
                                if (result[i] == 0)
                                {
                                    if (statId[i] == 4)
                                    {
                                        labUUT[i].ImageKey = "PASS";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "PASS");
                                        ttNum[0]++;
                                        ttNum[1]++;
                                        ttNum[2]++;
                                        ttNum[3]++;
                                    }
                                    else if (statId[i] == 3)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i]);
                                        ttNum[0]++;
                                        ttNum[1]++;
                                        ttNum[2]++;
                                        uutPass = false;
                                    }
                                    else if (statId[i] == 2)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        ttNum[0]++;
                                        ttNum[1]++;
                                        uutPass = false;
                                    }
                                    else if (statId[i] == 1)
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i]);
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        ttNum[0]++;
                                        uutPass = false;
                                    }
                                    else
                                    {
                                        labUUT[i].ImageKey = "FAIL";
                                        labSn[i].ForeColor = Color.Blue;
                                        SetLabPassFail(labResult0[i]);
                                        SetLabPassFail(labResult1[i]);
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        uutPass = false;
                                    }
                                }
                                else
                                {
                                    labUUT[i].ImageKey = "FAIL";
                                    labSn[i].ForeColor = Color.Red;
                                    if (result[i] == 4) //ATE
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "PASS");
                                        SetLabPassFail(labResult3[i], "FAIL");
                                        ttNum[0]++;
                                        ttNum[1]++;
                                        ttNum[2]++;
                                        ttNum[3]++;
                                        failNum[3]++;
                                    }
                                    else if (result[i] == 3) //HIPOT
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "PASS");
                                        SetLabPassFail(labResult2[i], "FAIL");
                                        SetLabPassFail(labResult3[i]);
                                        ttNum[0]++;
                                        ttNum[1]++;
                                        ttNum[2]++;
                                        failNum[2]++;
                                    }
                                    else if (result[i] == 2) //BI
                                    {
                                        SetLabPassFail(labResult0[i], "PASS");
                                        SetLabPassFail(labResult1[i], "FAIL");
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        ttNum[0]++;
                                        ttNum[1]++;
                                        failNum[1]++;
                                    }
                                    else if (result[i] == 1) //BI
                                    {
                                        SetLabPassFail(labResult0[i], "FAIL");
                                        SetLabPassFail(labResult1[i]);
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                        ttNum[0]++;
                                        failNum[0]++;
                                    }
                                    else
                                    {
                                        SetLabPassFail(labResult0[i]);
                                        SetLabPassFail(labResult1[i]);
                                        SetLabPassFail(labResult2[i]);
                                        SetLabPassFail(labResult3[i]);
                                    }
                                    uutPass = false;
                                }
                            }
                            else
                            {
                                labUUT[i].ImageKey = null;
                                labSn[i].ForeColor = Color.Black;
                                labResult0[i].Text = "";
                                labResult1[i].Text = "";
                                labResult2[i].Text = "";
                                labResult3[i].Text = "";
                                labResult4[i].Text = "";
                                labResult5[i].Text = "";
                                labResult6[i].Text = "";
                                labResult7[i].Text = "";
                                labResult8[i].Text = "";
                                labResult9[i].Text = "";

                                labResult0[i].BackColor = Button.DefaultBackColor;
                                labResult1[i].BackColor = Button.DefaultBackColor;
                                labResult2[i].BackColor = Button.DefaultBackColor;
                                labResult3[i].BackColor = Button.DefaultBackColor;
                                labResult4[i].BackColor = Button.DefaultBackColor;
                                labResult5[i].BackColor = Button.DefaultBackColor;
                                labResult6[i].BackColor = Button.DefaultBackColor;
                                labResult7[i].BackColor = Button.DefaultBackColor;
                                labResult8[i].BackColor = Button.DefaultBackColor;
                                labResult9[i].BackColor = Button.DefaultBackColor;
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

        private void SetLabPassFail(Label lab, string result="")
        {
            if (result=="PASS")
            {
                lab.Text = "PASS";
                lab.ForeColor = Color.Blue;
            }
            else if (result == "FAIL")
            {
                lab.Text = "FAIL";
                lab.ForeColor = Color.Red;
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
                    labResult0[i].Text = "";                     
                    labResult1[i].Text = "";
                    labResult2[i].Text = "";
                    labResult3[i].Text = "";
                    labResult4[i].Text = "";
                    labResult5[i].Text = "";
                    labResult6[i].Text = "";
                    labResult7[i].Text = "";
                    labResult8[i].Text = "";
                    labResult9[i].Text = "";
                    labResult0[i].BackColor = Button.DefaultBackColor;
                    labResult1[i].BackColor = Button.DefaultBackColor;
                    labResult2[i].BackColor = Button.DefaultBackColor;
                    labResult3[i].BackColor = Button.DefaultBackColor; 
                }
                for (int i = 0; i < labIdttNum.Length; i++)
                {
                    labIdttNum[i].Text = "0";
                    labIdFailNum[i].Text = "0";
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
        /// <summary>
        /// 设置当前IDCard数量
        /// </summary>
        /// <param name="ttNum"></param>
        /// <param name="failNum"></param>
        public void SetIdNum(List<int> ttNum,List<int> failNum)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetIdNumHandler(SetIdNum), ttNum, failNum);
            else
            {
                for (int i = 0; i < labIdttNum.Length; i++)
                {
                    labIdttNum[i].Text = ttNum[i].ToString();
                    labIdFailNum[i].Text = failNum[i].ToString();
                }
            }
        }
        /// <summary>
        /// 清除产能统计
        /// </summary>
        public void clrUUTNum()
        {
            if (this.InvokeRequired)
                this.Invoke(new clrUUTNumHandler(clrUUTNum));
            else
            {
                for (int i = 0; i < ttNum.Length; i++)
                {
                    ttNum[i] = 0;
                    failNum[i] = 0;
                }
                saveUUTNum();
            }
        }
        /// <summary>
        /// 保存产能设置
        /// </summary>
        private void saveUUTNum()
        {
            for (int i = 0; i < labUUTttNum.Length; i++)
			{
                labUUTttNum[i].Text = ttNum[i].ToString();
                labUUTfailNum[i].Text = failNum[i].ToString();  
			}

            CIniFile.WriteToIni("Parameter", "PTTTNum", ttNum[0].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "BITTNum", ttNum[1].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "TURNONTTNum", ttNum[2].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "HPTTNum1", ttNum[3].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "HPTTNum2", ttNum[4].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "ATETTNum1", ttNum[5].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "ATETTNum2", ttNum[6].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "ATETTNum3", ttNum[7].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "ATETTNum4", ttNum[8].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "RRCCTTNum", ttNum[9].ToString(), iniFile);
       

            CIniFile.WriteToIni("Parameter", "PTFailNum", failNum[0].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "BIFailNum", failNum[1].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "TURNONFailNum", failNum[2].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "HPFailNum1", failNum[3].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "HPFailNum2", failNum[4].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "ATEFailNum1", failNum[5].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "ATEFailNum2", failNum[6].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "ATEFailNum3", failNum[7].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "ATEFailNum4", failNum[8].ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "RRCCFailNum", failNum[9].ToString(), iniFile);
           
        }        
        #endregion

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            if (tabControl1.Visible == false)
            {
                tabControl1.Visible = true;
            }
            else
            {
                tabControl1.Visible = false ;
            }
        }

     

     
      

    }
}
