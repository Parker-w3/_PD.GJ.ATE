using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Para.BURNIN; 
namespace GJ.Para.Udc.BURNIN
{
    /// <summary>
    /// UI状态
    /// </summary>
    public enum EUI
    { 
      空闲,
      状态,
      老化
    }
    public partial class udcUUTSlot : UserControl
    {
        #region 构造函数
        public udcUUTSlot(int idNo)
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

            this.idNo = idNo;
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            imgUUT = new PictureBox[]{
                                     img1,img2,img3,img4,img5,img6,img7,img8                                 
                                     };
            labStatus = new Label();
            labStatus.Dock = DockStyle.Fill;
            labStatus.Font = new Font("宋体", 8.5f);
            labStatus.TextAlign = ContentAlignment.MiddleCenter;            
            labStatus.Text = "TEST";

            //是否显示提示信息
            tlTip.Active = true;
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

        }
        #endregion

        #region 面板控件
        private PictureBox[] imgUUT;
        private Label labStatus = null;
	    #endregion

        #region 面板回调函数
        private void udcUUTSlot_Load(object sender, EventArgs e)
        {

        }
        private void tlDisplay_Click(object sender, EventArgs e)
        {
            menuClick.OnEvented(new CSetMenuArgs(idNo, ESetMenu.显示信息));
        }
        private void tlFree_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要设置该位置为空闲状态?", "状态设置", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuClick.OnEvented(new CSetMenuArgs(idNo, ESetMenu.位置空闲));
            }
        }
        private void tlForbit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要设置该位置为禁用状态?", "状态设置", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuClick.OnEvented(new CSetMenuArgs(idNo, ESetMenu.禁用位置));
            }
        }
        private void tlStartBI_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要强制该位置启动老化?", "状态设置", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuClick.OnEvented(new CSetMenuArgs(idNo, ESetMenu.启动老化));
            }
        }
        private void tlEndBI_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要强制该位置结束老化?", "状态设置", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuClick.OnEvented(new CSetMenuArgs(idNo, ESetMenu.停止老化));
            }
        }
        private void tlClrAlarm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要解除该位置异常报警?", "状态设置", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuClick.OnEvented(new CSetMenuArgs(idNo, ESetMenu.解除报警));
            }
        }
        private void tlClrFail_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清除该位置产品不良?", "状态设置", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuClick.OnEvented(new CSetMenuArgs(idNo, ESetMenu.清除不良));
            }
        }
        private void tlResetBI_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要复位该位置老化?", "状态设置", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuClick.OnEvented(new CSetMenuArgs(idNo, ESetMenu.复位老化));
            }
        }
        private void tlInPos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要指定位置老化?", "状态设置", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuClick.OnEvented(new CSetMenuArgs(idNo, ESetMenu.指定位置老化));
            }
        }
        #endregion

        #region 字段
        private int idNo = 0;
        private EUI IsUI = EUI.空闲;
        #endregion

        #region 属性
    
        #endregion

        #region 事件
        public enum ESetMenu
        {
            显示信息,
            位置空闲,
            禁用位置,
            启动老化,
            停止老化,
            解除报警,
            清除不良,
            复位老化,
            指定位置老化
        }
        public class CSetMenuArgs : EventArgs
        {
            public readonly int idNo;
            public readonly ESetMenu menuInfo;
            public CSetMenuArgs(int idNo, ESetMenu menuInfo)
            {
                this.idNo = idNo;
                this.menuInfo = menuInfo;
            }
        }
        public COnEvent<CSetMenuArgs> menuClick = new COnEvent<CSetMenuArgs>();
        #endregion

        #region 委托
        private delegate void SetRunStatusHandler(udcRun.CUUT runUUT);
        #endregion

        #region 方法
        public void SetRunStatus(udcRun.CUUT runUUT)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetRunStatusHandler(SetRunStatus), runUUT);
            else
            {
                EUI NowIsUI = EUI.空闲;
                if (runUUT.wPara.alarmErr == udcRun.EAlarmCode.正常 && runUUT.wPara.alarmInfo == string.Empty)
                {
                    if (runUUT.wPara.doRun == udcRun.EDoRun.正在老化 || runUUT.wPara.doRun == udcRun.EDoRun.老化结束)
                        NowIsUI = EUI.老化;
                    else
                        NowIsUI = EUI.状态;
                }
                else
                {
                    NowIsUI = EUI.状态;
                }
                if (IsUI != NowIsUI)
                {
                    foreach (Control item in this.Controls)
                        this.Controls.Remove(item);
                    if (NowIsUI == EUI.老化)                    
                        this.Controls.Add(panel1);
                    else          
                        this.Controls.Add(labStatus); 
                    IsUI = NowIsUI;
                }

                if (IsUI == EUI.状态)
                    setFixInStatus(runUUT);
                else
                    setFixInBI(runUUT);
            }
        }
        /// <summary>
        /// 设置治具状态
        /// </summary>
        /// <param name="runUUT"></param>
        private void setFixInStatus(udcRun.CUUT runUUT)
        {
            string uutInfo = "位置编号:" + runUUT.wBase.localName + "\r\n";
            string testInfo = string.Empty;
            string devInfo = string.Empty;
            devInfo = "控制板地址:" + CSysPara.mVal.monCom[runUUT.wBase.ctrlCom] + "_" + runUUT.wBase.ctrlAddr.ToString("D2") + ";";
            devInfo += "ERS地址:" + CSysPara.mVal.ersCom[runUUT.wBase.ersCom] + "_" + runUUT.wBase.ersAddr.ToString("D2") + "_" + runUUT.wBase.ersCH.ToString("D2") + ";\r\n";
            devInfo += "针盘使用次数=" + runUUT.wPara.usedNum.ToString() + ";\r\n";
            if (runUUT.wPara.doRun!=udcRun.EDoRun.位置禁用 &&  runUUT.wPara.alarmErr != udcRun.EAlarmCode.正常)
            {
                if (runUUT.wPara.alarmErr == udcRun.EAlarmCode.针盘使用寿命已到)
                {
                    labStatus.Text = "针盘维修";
                    labStatus.ForeColor = Color.Red;
                    labStatus.BackColor = Color.Yellow;
                    testInfo = "异常报警:" + runUUT.wPara.alarmErr.ToString();
                }
                else
                {
                    labStatus.Text = "异常报警";
                    labStatus.ForeColor = Color.Red;
                    labStatus.BackColor = Color.Yellow;
                    testInfo = "异常报警:" + runUUT.wPara.alarmErr.ToString();
                }                
            }
            else if (runUUT.wPara.alarmInfo != "")
            {
                labStatus.Text = runUUT.wPara.alarmInfo;
                labStatus.ForeColor = Color.Red;
                labStatus.BackColor = Color.Yellow;
                testInfo = "异常报警:" + runUUT.wPara.alarmInfo;
            }
            else
            {
                switch (runUUT.wPara.doRun)
                {
                    case udcRun.EDoRun.异常报警:
                        labStatus.Text = runUUT.wPara.alarmInfo;
                        labStatus.ForeColor = Color.Red;
                        labStatus.BackColor = Color.Yellow;
                        testInfo = "异常报警:" + runUUT.wPara.alarmInfo;
                        break;
                    case udcRun.EDoRun.位置禁用:
                        labStatus.Text = "";
                        labStatus.ForeColor = Color.Black;
                        labStatus.BackColor = Control.DefaultBackColor;
                        testInfo = "位置状态:该位置已禁用;";
                        break;
                    case udcRun.EDoRun.位置空闲:
                        labStatus.Text = "";
                        labStatus.ForeColor = Color.Black;
                        labStatus.BackColor = Color.White;
                        testInfo = "位置状态:空闲,等待进机.";
                        break;
                    case udcRun.EDoRun.正在进机:
                        labStatus.Text = runUUT.wPara.doRun.ToString();
                        labStatus.ForeColor = Color.Black;
                        labStatus.BackColor = Color.Turquoise;
                        testInfo = "位置状态:治具正在进机中.";
                        break;
                    case udcRun.EDoRun.进机完毕:
                        labStatus.Text = runUUT.wPara.doRun.ToString();
                        labStatus.ForeColor = Color.Black;
                        labStatus.BackColor = Color.Turquoise;
                        testInfo = "位置状态:治具进机完毕.";
                        break;
                    case udcRun.EDoRun.启动老化:
                        labStatus.Text = runUUT.wPara.doRun.ToString();
                        labStatus.ForeColor = Color.Black;
                        labStatus.BackColor = Color.Turquoise;
                        testInfo = "位置状态:治具启动老化.";
                        break;
                    case udcRun.EDoRun.老化自检:
                        labStatus.Text = runUUT.wPara.doRun.ToString();
                        labStatus.ForeColor = Color.Black;
                        labStatus.BackColor = Color.Turquoise;
                        testInfo = "位置状态:老化自检.";
                        break;
                    case udcRun.EDoRun.正在老化:
                        break;
                    case udcRun.EDoRun.老化结束:
                        break;
                    case udcRun.EDoRun.正在出机:
                        labStatus.Text = runUUT.wPara.doRun.ToString();
                        labStatus.ForeColor = Color.Black;
                        labStatus.BackColor = Color.Turquoise;
                        testInfo = "位置状态:正在出机.";
                        break;
                    case udcRun.EDoRun.空治具到位:
                        labStatus.Text = "";
                        labStatus.ForeColor = Color.Black;
                        labStatus.BackColor = Color.PaleTurquoise;
                        testInfo = "位置状态:为空治具.";
                        break;
                    default:
                        break;
                }
            }
            testInfo += "\r\n";
            uutInfo += testInfo;
            uutInfo += devInfo;
            tlTip.RemoveAll();
            tlTip.SetToolTip(labStatus, uutInfo);
        }
        /// <summary>
        /// 设置治具老化中
        /// </summary>
        /// <param name="runUUT"></param>
        private void setFixInBI(udcRun.CUUT runUUT)
        {
           try 
           {
                string uutInfo = "位置编号:" + runUUT.wBase.localName + "\r\n";
                string testInfo = string.Empty;
                string devInfo = string.Empty;
                devInfo = "控制板地址:" + CSysPara.mVal.monCom[runUUT.wBase.ctrlCom] + "_" + runUUT.wBase.ctrlAddr.ToString("D2") + ";";
                devInfo += "ERS地址:" + CSysPara.mVal.ersCom[runUUT.wBase.ersCom] + "_" + runUUT.wBase.ersAddr.ToString("D2") + "_" + runUUT.wBase.ersCH.ToString("D2") + ";\r\n";
                devInfo += "针盘使用次数=" + runUUT.wPara.usedNum.ToString() + ";\r\n";
                bool uutPass = true;
                TimeSpan ts = new TimeSpan(0, 0, runUUT.wPara.runTime);
                string runTime = ts.Days.ToString("D2") + ":" + ts.Hours.ToString("D2") + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");
                testInfo += "机种名称:" + runUUT.wPara.modelName + ";老化时间=" + (((double)runUUT.wPara.burnTime) / 3600).ToString("0.0") + "小时;\r\n";
                testInfo += "开始时间:" + runUUT.wPara.startTime + ";运行时间=" + runTime + ";\r\n";
                for (int i = 0; i < runUUT.wLed.Count; i++)
                {
                    if (runUUT.wLed[i].serialNo != "")
                    {
                        if (runUUT.wLed[i].failResult == 0)
                        {
                            testInfo += (i + 1).ToString("D2") + ")产品条码:" + runUUT.wLed[i].serialNo + ";";
                            testInfo += "电压=" + runUUT.wLed[i].unitV.ToString("0.000") + "V;电流=" + runUUT.wLed[i].unitA.ToString("0.00") + "A;";
                            testInfo += "->PASS;\r\n";
                        }
                        else
                        {
                            uutPass = false;
                            testInfo += (i + 1).ToString("D2") + ")产品条码:" + runUUT.wLed[i].serialNo + ";";
                            testInfo += "电压=" + runUUT.wLed[i].unitV.ToString("0.000") + "V;电流=" + runUUT.wLed[i].unitA.ToString("0.00") + "A;";
                            testInfo += "->FAIL;";
                            testInfo += "不良时间:" + runUUT.wLed[i].failTime + ";不良信息:" + runUUT.wLed[i].failInfo + ";\r\n";
                        }
                    }
                    else
                    {
                        testInfo += (i + 1).ToString("D2") + ")无产品条码;\r\n";
                    }
                }
                if (uutPass)
                    testInfo = "治具ID[" + runUUT.wPara.idCard + "]老化结果=PASS;\r\n" + testInfo;
                else
                    testInfo = "治具ID[" + runUUT.wPara.idCard + "]老化结果=FAIL;\r\n" + testInfo;

                uutInfo += testInfo;
                uutInfo += devInfo;

                tlTip.RemoveAll();
                tlTip.SetToolTip(panel1, uutInfo);

                if (runUUT.wPara.doRun == udcRun.EDoRun.正在老化)
                {
                    for (int i = 0; i < runUUT.wLed.Count; i++)
                    {
                        if (runUUT.wLed[i].serialNo == "")
                            imgUUT[i].Image = ImageList1.Images["FREE"];
                        else
                        {
                            if (runUUT.wLed[i].failResult == 0)
                                imgUUT[i].Image = ImageList1.Images["PASS1"];
                            else
                                imgUUT[i].Image = ImageList1.Images["FAIL1"];
                        }
                        tlTip.SetToolTip(imgUUT[i], uutInfo);
                    }
                }
                else
                {
                    for (int i = 0; i < runUUT.wLed.Count; i++)
                    {
                        if (runUUT.wLed[i].serialNo == "")
                            imgUUT[i].Image = ImageList1.Images["FREE"];
                        else
                        {
                            if (runUUT.wLed[i].failResult == 0)
                                imgUUT[i].Image = ImageList1.Images["PASS2"];
                            else
                                imgUUT[i].Image = ImageList1.Images["FAIL2"];
                        }
                        tlTip.SetToolTip(imgUUT[i], uutInfo);
                    }
                }
           }
           catch (Exception ex)
           {
              string er = ex.ToString();
           }
        }
        #endregion

      

    }
}
