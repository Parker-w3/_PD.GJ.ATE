using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports; 
using GJ.Para.Base;
using GJ.Dev.HIPOT;

namespace GJ.Para.HIPOT
{
    public partial class udcSysPara:udcSysBase
    {
        #region 构造函数
        public udcSysPara()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();
        }
        #endregion
        
        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {

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

        #region 面板回调函数
        private void udcSysPara_Load(object sender, EventArgs e)
        {
            cmbIOCom.Items.Clear();
            cmbHPCom1.Items.Clear();
            cmbHPCom2.Items.Clear();
            string[] comNames = SerialPort.GetPortNames();              
            for (int i = 0; i < comNames.Length; i++)
            {
                cmbIOCom.Items.Add(comNames[i]);
                cmbHPCom1.Items.Add(comNames[i]);
                cmbHPCom2.Items.Add(comNames[i]);
            }
            txtPlcIP.Text = CSysPara.mVal.plcIp;
            txtPlcPort.Text = CSysPara.mVal.plcPort.ToString();
            txtSerTcpIP.Text = CSysPara.mVal.serIP;
            txtSerTcpPort.Text = CSysPara.mVal.serPort.ToString();
            cmbIOCom.Text = CSysPara.mVal.ioCom;
            cmbHPDev.SelectedIndex = (int)CSysPara.mVal.hipotDev;
            cmbHPCom1.Text = CSysPara.mVal.hipotCom1;
            cmbHPCom2.Text = CSysPara.mVal.hipotCom2;
            cmbHipotMode.SelectedIndex = CSysPara.mVal.StatHipotMode;
           
            txtOffsetDC1.Text = CSysPara.mVal.offsetDC[0].ToString();
            txtOffsetDC2.Text = CSysPara.mVal.offsetDC[1].ToString();
            txtOffsetDC3.Text = CSysPara.mVal.offsetDC[2].ToString();
            txtOffsetDC4.Text = CSysPara.mVal.offsetDC[3].ToString();
            txtOffsetDC5.Text = CSysPara.mVal.offsetDC[4].ToString();
            txtOffsetDC6.Text = CSysPara.mVal.offsetDC[5].ToString();
            txtOffsetDC7.Text = CSysPara.mVal.offsetDC[6].ToString();
            txtOffsetDC8.Text = CSysPara.mVal.offsetDC[7].ToString();

            txtOffsetAC1.Text = CSysPara.mVal.offsetAC[0].ToString();
            txtOffsetAC2.Text = CSysPara.mVal.offsetAC[1].ToString();
            txtOffsetAC3.Text = CSysPara.mVal.offsetAC[2].ToString();
            txtOffsetAC4.Text = CSysPara.mVal.offsetAC[3].ToString();
            txtOffsetAC5.Text = CSysPara.mVal.offsetAC[4].ToString();
            txtOffsetAC6.Text = CSysPara.mVal.offsetAC[5].ToString();
            txtOffsetAC7.Text = CSysPara.mVal.offsetAC[6].ToString();
            txtOffsetAC8.Text = CSysPara.mVal.offsetAC[7].ToString();

            txtIoDelayMs.Text = CSysPara.mVal.ioDelayMs.ToString();
            txtMonDelayMs.Text = CSysPara.mVal.monDelayMs.ToString();
            txtMesDelayTime.Text = CSysPara.mVal.MesDelayMs.ToString();
            txtHp2delaytime.Text = CSysPara.mVal.Hp2DelayMs.ToString();
            txtFailTestTimes.Text = CSysPara.mVal.FailTestCount.ToString();
            chkHpVolt.Checked = CSysPara.mVal.chkHpVolt;
            chkhandleFail.Checked = CSysPara.mVal.chkhandleFail;
            chkSpHP.Checked = CSysPara.mVal.chkSpHP;

            txtModelPath.Text = CSysPara.mVal.modelPath;
            chkSaveReport.Checked = CSysPara.mVal.saveReport;
            txtSaveTimes.Text = CSysPara.mVal.saveReportTimes.ToString();
            txtReportPath.Text = CSysPara.mVal.reportPath;

            chkImpPrg.Checked = CSysPara.mVal.chkImpPrg;
            chkAutoModel.Checked = CSysPara.mVal.chkAutoModel;
            chkSaveTcpLog.Checked = CSysPara.mVal.chkSaveTcpLog;

            cmbHipotMax.Text=CSysPara.mVal.C_HIPOT_MAX.ToString();
            cmbSlotMax.Text=CSysPara.mVal.C_SLOT_MAX.ToString();

            ChkMes.Checked  = CSysPara.mVal.conMes;
            ChkMesFail.Checked = CSysPara.mVal.ChkMesFail;

            chkSaveHipotData.Checked = CSysPara.mVal.chksaveHipotData;

        }
        private void btnModel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                txtModelPath.Text = dlg.SelectedPath;  
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                txtReportPath.Text = dlg.SelectedPath;  
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要保存系统参数设置?", "系统参数设置", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
               DialogResult.Yes)
            {
                CSysPara.mVal = new CSysPara();
                CSysPara.mVal.plcIp = txtPlcIP.Text;
                CSysPara.mVal.plcPort = System.Convert.ToInt32(txtPlcPort.Text);
                CSysPara.mVal.serIP = txtSerTcpIP.Text;
                CSysPara.mVal.serPort = System.Convert.ToInt32(txtSerTcpPort.Text); 
                CSysPara.mVal.ioCom = cmbIOCom.Text;
                CSysPara.mVal.hipotDev = (EHipotType)cmbHPDev.SelectedIndex; 
                CSysPara.mVal.hipotCom1 = cmbHPCom1.Text;
                CSysPara.mVal.hipotCom2 = cmbHPCom2.Text;
                CSysPara.mVal.StatHipotMode = cmbHipotMode.SelectedIndex;
                CSysPara.mVal.ChkMesFail = ChkMesFail.Checked;
                CSysPara.mVal.conMes = ChkMes.Checked;

                CSysPara.mVal.offsetDC[0] = System.Convert.ToDouble(txtOffsetDC1.Text);
                CSysPara.mVal.offsetDC[1] = System.Convert.ToDouble(txtOffsetDC2.Text);
                CSysPara.mVal.offsetDC[2] = System.Convert.ToDouble(txtOffsetDC3.Text);
                CSysPara.mVal.offsetDC[3] = System.Convert.ToDouble(txtOffsetDC4.Text);
                CSysPara.mVal.offsetDC[4] = System.Convert.ToDouble(txtOffsetDC5.Text);
                CSysPara.mVal.offsetDC[5] = System.Convert.ToDouble(txtOffsetDC6.Text);
                CSysPara.mVal.offsetDC[6] = System.Convert.ToDouble(txtOffsetDC7.Text);
                CSysPara.mVal.offsetDC[7] = System.Convert.ToDouble(txtOffsetDC8.Text);

                CSysPara.mVal.offsetAC[0] = System.Convert.ToDouble(txtOffsetAC1.Text);
                CSysPara.mVal.offsetAC[1] = System.Convert.ToDouble(txtOffsetAC2.Text);
                CSysPara.mVal.offsetAC[2] = System.Convert.ToDouble(txtOffsetAC3.Text);
                CSysPara.mVal.offsetAC[3] = System.Convert.ToDouble(txtOffsetAC4.Text);
                CSysPara.mVal.offsetAC[4] = System.Convert.ToDouble(txtOffsetAC5.Text);
                CSysPara.mVal.offsetAC[5] = System.Convert.ToDouble(txtOffsetAC6.Text);
                CSysPara.mVal.offsetAC[6] = System.Convert.ToDouble(txtOffsetAC7.Text);
                CSysPara.mVal.offsetAC[7] = System.Convert.ToDouble(txtOffsetAC8.Text);

                CSysPara.mVal.ioDelayMs = System.Convert.ToInt32(txtIoDelayMs.Text);
                CSysPara.mVal.monDelayMs = System.Convert.ToInt32(txtMonDelayMs.Text);
                CSysPara.mVal.MesDelayMs = System.Convert.ToInt32(txtMesDelayTime.Text);
                CSysPara.mVal.Hp2DelayMs = System.Convert.ToInt32(txtHp2delaytime .Text );
                CSysPara.mVal.FailTestCount = System.Convert.ToInt32(txtFailTestTimes .Text );
                CSysPara.mVal.chkHpVolt = chkHpVolt.Checked;
                CSysPara.mVal.chkhandleFail = chkhandleFail.Checked;
                CSysPara.mVal.chkSpHP = chkSpHP.Checked;

                CSysPara.mVal.modelPath = txtModelPath.Text;
                CSysPara.mVal.saveReport = chkSaveReport.Checked;
                CSysPara.mVal.saveReportTimes = System.Convert.ToInt32(txtSaveTimes.Text);
                CSysPara.mVal.reportPath = txtReportPath.Text;

                CSysPara.mVal.chkImpPrg = chkImpPrg.Checked;
                CSysPara.mVal.chkAutoModel = chkAutoModel.Checked;
                CSysPara.mVal.chkSaveTcpLog = chkSaveTcpLog.Checked;

                CSysPara.mVal.C_HIPOT_MAX = System.Convert.ToInt16(cmbHipotMax.Text);
                CSysPara.mVal.C_SLOT_MAX = System.Convert.ToInt16(cmbSlotMax.Text);

                CSysPara.mVal.chksaveHipotData = chkSaveHipotData.Checked;
         

                CSysSet<CSysPara>.save(CSysPara.mVal);
                OnSysSave.OnEvented(new CSysSaveArgs(true));
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            OnSysCancel.OnEvented(new CSysCancelArgs(true));  
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChkMesFail.Visible = !ChkMesFail.Visible;
        }
      
    }
}
