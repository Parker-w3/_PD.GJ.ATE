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

namespace GJ.Para.ATE
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
            string[] comNames = SerialPort.GetPortNames();              
            for (int i = 0; i < comNames.Length; i++)   
            {
                cmbIOCom.Items.Add(comNames[i]);     
                cmbACCOM .Items.Add (comNames[i]);     
            }
            txtPlcIP.Text = CSysPara.mVal.plcIp;
            txtPlcPort.Text = CSysPara.mVal.plcPort.ToString();
            txtSerTcpIP.Text = CSysPara.mVal.serIP;
            txtSerTcpPort.Text = CSysPara.mVal.serPort.ToString();
            cmbIOCom.Text = CSysPara.mVal.ioCom;
            chkIO.Checked = CSysPara.mVal.chkIo;
            if (CSysPara.mVal.chkIo)
                cmbIOCom.Enabled = true;
            else
                cmbIOCom.Enabled = false;
            cmbATEMode.SelectedIndex = CSysPara.mVal.statATEMode;

            cmbATELanguage.SelectedIndex = CSysPara.mVal.ate_Languge;   
            txtATEPrg.Text=CSysPara.mVal.ate_title_name;
            txtATEResultPath.Text=CSysPara.mVal.ate_result_folder;
            chkBarForm.Checked=CSysPara.mVal.ate_scanSn_Enable;
            txtBarFormName.Text=CSysPara.mVal.ate_scanSn_name;
            txtATEDelay.Text=CSysPara.mVal.ate_result_delay.ToString();
            txtATERepeats.Text=CSysPara.mVal.ate_result_repeats.ToString();
            txtATEFailTimes.Text = CSysPara.mVal.ATEFailTimes.ToString();

            txtModelPath.Text = CSysPara.mVal.modelPath;
            chkSaveReport.Checked = CSysPara.mVal.saveReport;
            txtSaveTimes.Text = CSysPara.mVal.saveReportTimes.ToString();
            txtReportPath.Text = CSysPara.mVal.reportPath;

            chkSaveTcpLog.Checked = CSysPara.mVal.chkSaveTcpLog;
            txtMonDelayMs.Text = CSysPara.mVal.monDelayMs.ToString();
            chkHPower.Checked = CSysPara.mVal.ATEHPowerModel;

            ChkRelayTest.Checked = CSysPara.mVal.ChkRelay;
            cmbACCOM.Text = CSysPara.mVal.acCOM;
            txtStartVmax.Text = CSysPara.mVal.ACStartVmax.ToString();
            txtDCVmin.Text = CSysPara.mVal.DCVmin.ToString();
            txtDCVmax.Text = CSysPara.mVal.DCVmax.ToString();
            txtACVmin.Text = CSysPara.mVal.ACVmin.ToString();
            txtACVmax.Text = CSysPara.mVal.ACVmax.ToString();
            txtRelayDelayMs.Text = CSysPara.mVal.RelayDelayMs.ToString();
            ChkMes.Checked = CSysPara.mVal.conMes;
            ChkMesFail.Checked = CSysPara.mVal.ChkMesFail;
               
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
                CSysPara.mVal.plcIp = txtPlcIP.Text;
                CSysPara.mVal.plcPort = System.Convert.ToInt32(txtPlcPort.Text);
                CSysPara.mVal.serIP = txtSerTcpIP.Text;
                CSysPara.mVal.serPort = System.Convert.ToInt32(txtSerTcpPort.Text); 
                CSysPara.mVal.ioCom = cmbIOCom.Text;
                CSysPara.mVal.chkIo = chkIO.Checked;
                CSysPara.mVal.statATEMode = cmbATEMode.SelectedIndex;

                CSysPara.mVal.ate_Languge = cmbATELanguage.SelectedIndex; 
                CSysPara.mVal.ate_title_name = txtATEPrg.Text;
                CSysPara.mVal.ate_result_folder = txtATEResultPath.Text;
                CSysPara.mVal.ate_scanSn_Enable = chkBarForm.Checked;
                CSysPara.mVal.ate_scanSn_name = txtBarFormName.Text;
                CSysPara.mVal.ate_result_delay = System.Convert.ToInt32(txtATEDelay.Text);
                CSysPara.mVal.ate_result_repeats = System.Convert.ToInt32(txtATERepeats.Text);
                CSysPara.mVal.ATEFailTimes = System.Convert.ToInt32(txtATEFailTimes.Text);

                CSysPara.mVal.modelPath = txtModelPath.Text;
                CSysPara.mVal.saveReport = chkSaveReport.Checked;
                CSysPara.mVal.saveReportTimes = System.Convert.ToInt32(txtSaveTimes.Text);
                CSysPara.mVal.reportPath = txtReportPath.Text;

                CSysPara.mVal.chkSaveTcpLog = chkSaveTcpLog.Checked;
                CSysPara.mVal.monDelayMs = System.Convert.ToInt32(txtMonDelayMs.Text);
                CSysPara.mVal.ATEHPowerModel = chkHPower.Checked;

                CSysPara.mVal.ChkRelay=  ChkRelayTest.Checked ;
                CSysPara.mVal.acCOM= cmbACCOM.Text ;
                CSysPara.mVal.ACStartVmax=double .Parse ( txtStartVmax.Text) ;
                 CSysPara.mVal.DCVmin=double .Parse (txtDCVmin.Text) ;
                 CSysPara.mVal.DCVmax=double .Parse (txtDCVmax.Text );
                CSysPara.mVal.ACVmin= double .Parse (txtACVmin.Text) ;
                CSysPara.mVal.ACVmax=double .Parse (txtACVmax.Text) ;
                CSysPara.mVal.RelayDelayMs = int.Parse(txtRelayDelayMs.Text);
                CSysPara.mVal.ChkMesFail = ChkMesFail.Checked;
                CSysPara.mVal.conMes = ChkMes.Checked;
                CSysSet<CSysPara>.save(CSysPara.mVal);
                OnSysSave.OnEvented(new CSysSaveArgs(true));
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            OnSysCancel.OnEvented(new CSysCancelArgs(true));  
        }
        #endregion

        private void chkIO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIO.Checked)
                cmbIOCom.Enabled = true;
            else
                cmbIOCom.Enabled = false;   
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPlcIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPlcPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

    }
}
