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

namespace GJ.Para.UNLOAD
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
        private TextBox[] txtChkStationATE = null;
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            txtChkStationATE = new TextBox[] { txtchkStationATE1, txtchkStationATE2, txtchkStationATE3, txtchkStationATE4 };
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
            string[] comNames = SerialPort.GetPortNames();
            cmbIdCom.Items.Clear();
            cmbLCRCom.Items.Clear();
            cmbIoCom.Items.Clear();
            cmbDCNCom.Items.Clear();
            for (int i = 0; i < comNames.Length; i++)   
            {
                cmbIdCom.Items.Add(comNames[i]);  
                cmbLCRCom.Items.Add(comNames[i]);
                cmbIoCom.Items.Add(comNames[i]);
                cmbDCNCom.Items.Add(comNames[i]);
            }

            txtPlcIP1.Text = CSysPara.mVal.plcIp1;
            txtPlcPort1.Text = CSysPara.mVal.plcPort1.ToString();
            txtPlcIP2.Text = CSysPara.mVal.plcIp2;
            txtPlcPort2.Text = CSysPara.mVal.plcPort2.ToString();

            txtUnLoadPlcIp.Text = CSysPara.mVal.plcUnloadIP;
            TxtUnLoadPlcPort.Text = CSysPara.mVal.plcUnloadPort.ToString();
            cmbIdCom.Text = CSysPara.mVal.idCom;
            cmbLCRCom.Text = CSysPara.mVal.LCRCom;
            cmbIoCom.Text = CSysPara.mVal.IoCom;
            chkDCN.Checked = CSysPara.mVal.ChkDCN;
            cmbDCNCom.Text = CSysPara.mVal.DCNCom;
            txtDCNband.Text = CSysPara.mVal.DCNBand;
            txtMySql.Text = CSysPara.mVal.mySqlIp;
            txtTestPort.Text = CSysPara.mVal.testPort.ToString();
            ChkAtuoUnload.Checked = CSysPara.mVal.AtuoUnload;

            txtFailTestCount.Text = CSysPara.mVal.FailTestCount.ToString();
            txtIoDelayMs.Text = CSysPara.mVal.IoDelayMs.ToString();
            txtmonDelayMs.Text = CSysPara.mVal.monDelayMs.ToString();
            txtATEFailTimes.Text = CSysPara.mVal.ATEFailTimes.ToString();
            chkBIFail.Checked = CSysPara.mVal.chkBIFail;
            chkHPFail.Checked = CSysPara.mVal.chkHPFail;
            chkATEFail.Checked = CSysPara.mVal.chkATEFail;
            chkUnloadFail.Checked = CSysPara.mVal.chkUnLoadFail;
            chkHandUp.Checked = CSysPara.mVal.chkHandUp;

            txtModelPath.Text = CSysPara.mVal.modelPath;
            chkSaveReport.Checked = CSysPara.mVal.saveReport;
            txtSaveTimes.Text = CSysPara.mVal.saveReportTimes.ToString();
            txtReportPath.Text = CSysPara.mVal.reportPath;

            chkHPTest1.Checked=CSysPara.mVal.chkForceHP[0];
            chkHPTest2.Checked=CSysPara.mVal.chkForceHP[1];
            chkATETest1.Checked=CSysPara.mVal.chkForceATE[0];
            chkATETest2.Checked=CSysPara.mVal.chkForceATE[1];
            chkHPIn.Checked = CSysPara.mVal.chkHPIn;
            chkATEIn.Checked = CSysPara.mVal.chkATEIn;

            chkNoHipot1.Checked = CSysPara.mVal.chkNoHP[0];
            chkNoHipot2.Checked = CSysPara.mVal.chkNoHP[1];
            chkNoATE1.Checked = CSysPara.mVal.chkNoATE[0];
            chkNoATE2.Checked = CSysPara.mVal.chkNoATE[1];
            chkNoATE3.Checked = CSysPara.mVal.chkNoATE[2];
            chkNoATE4.Checked = CSysPara.mVal.chkNoATE[3];

            ChkNoRRCC.Checked = CSysPara.mVal.chkNoRRCC;
            ChkNoTurnON.Checked = CSysPara.mVal.chkNoTurnOn;

            txtchkStationHP.Text = CSysPara.mVal.chkstationHP;
            for (int i = 0; i < txtChkStationATE.Length; i++)
            {
                txtChkStationATE[i].Text = CSysPara.mVal.chkstationATE[i];
            }

            ChkMes.Checked = CSysPara.mVal.conMES;
            ChkRRCC.Checked = CSysPara.mVal.conRRCC;
            txtMesDelayTime.Text = CSysPara.mVal.MesDelayTime.ToString();
            txtPassword.Text = CSysPara.mVal.FailPassword;
            ChkRRCCPass.Checked = CSysPara.mVal.ChkRRCCPass;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要保存系统参数设置?", "系统参数设置", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
               DialogResult.Yes)
            {
                CSysPara.mVal = new CSysPara();
                CSysPara.mVal.plcIp1 = txtPlcIP1.Text;
                CSysPara.mVal.plcPort1 = System.Convert.ToInt32(txtPlcPort1.Text);
                CSysPara.mVal.plcIp2 = txtPlcIP2.Text;
                CSysPara.mVal.plcPort2 = System.Convert.ToInt32(txtPlcPort2.Text);
                CSysPara.mVal.plcUnloadIP = txtUnLoadPlcIp.Text;
                CSysPara.mVal.plcUnloadPort = System.Convert.ToInt32(TxtUnLoadPlcPort.Text);
                CSysPara.mVal.idCom = cmbIdCom.Text;
                CSysPara.mVal.LCRCom = cmbLCRCom.Text;
                CSysPara.mVal.IoCom = cmbIoCom.Text;
                CSysPara.mVal.ChkDCN = chkDCN .Checked ;
                CSysPara.mVal.DCNCom = cmbDCNCom.Text;
                CSysPara.mVal.DCNBand  = txtDCNband.Text;
                CSysPara.mVal.mySqlIp = txtMySql.Text;
                CSysPara.mVal.testPort = System.Convert.ToInt32(txtTestPort.Text);
                CSysPara.mVal.AtuoUnload = ChkAtuoUnload.Checked;

                CSysPara.mVal.FailTestCount = System.Convert.ToInt32(txtFailTestCount .Text );
                CSysPara.mVal.IoDelayMs = System.Convert.ToInt32(txtIoDelayMs.Text);
                CSysPara.mVal.monDelayMs = System.Convert.ToInt32(txtmonDelayMs.Text);
                CSysPara.mVal.ATEFailTimes = System.Convert.ToInt32(txtATEFailTimes.Text);
                CSysPara.mVal.chkBIFail = chkBIFail.Checked;
                CSysPara.mVal.chkHPFail = chkHPFail.Checked;
                CSysPara.mVal.chkATEFail = chkATEFail.Checked;
                CSysPara.mVal.chkUnLoadFail = chkUnloadFail.Checked;
                CSysPara.mVal.chkHandUp = chkHandUp.Checked;

                CSysPara.mVal.modelPath = txtModelPath.Text;
                CSysPara.mVal.saveReport = chkSaveReport.Checked;
                CSysPara.mVal.saveReportTimes = System.Convert.ToInt32(txtSaveTimes.Text);
                CSysPara.mVal.reportPath = txtReportPath.Text;

                CSysPara.mVal.chkForceHP[0] = chkHPTest1.Checked;
                CSysPara.mVal.chkForceHP[1] = chkHPTest2.Checked;
                CSysPara.mVal.chkForceATE[0] = chkATETest1.Checked;
                CSysPara.mVal.chkForceATE[1] = chkATETest2.Checked;
                CSysPara.mVal.chkHPIn = chkHPIn.Checked;
                CSysPara.mVal.chkATEIn = chkATEIn.Checked;
                CSysPara.mVal.chkNoHP[0] = chkNoHipot1.Checked;
                CSysPara.mVal.chkNoHP[1] = chkNoHipot2.Checked;
                CSysPara.mVal.chkNoATE[0] = chkNoATE1.Checked;
                CSysPara.mVal.chkNoATE[1] = chkNoATE2.Checked;
                CSysPara.mVal.chkNoATE[2] = chkNoATE3.Checked;
                CSysPara.mVal.chkNoATE[3] = chkNoATE4.Checked;
                CSysPara.mVal.chkNoRRCC = ChkNoRRCC.Checked;
                CSysPara.mVal.chkNoTurnOn = ChkNoTurnON.Checked;

                CSysPara.mVal.chkstationHP = txtchkStationHP.Text;
                for (int i = 0; i < txtChkStationATE.Length; i++)
                {
                    CSysPara.mVal.chkstationATE[i] = txtChkStationATE[i].Text;
                }

                CSysPara.mVal.conMES = ChkMes.Checked;
                CSysPara.mVal.conRRCC = ChkRRCC.Checked;
                CSysPara.mVal.MesDelayTime =  System.Convert.ToInt32(txtMesDelayTime.Text);
                CSysPara.mVal.FailPassword = txtPassword.Text;
                CSysPara.mVal.ChkRRCCPass = ChkRRCCPass.Checked;
                CSysSet<CSysPara>.save(CSysPara.mVal);
                OnSysSave.OnEvented(new CSysSaveArgs(true));
            }
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            OnSysCancel.OnEvented(new CSysCancelArgs(true));  
        }
        #endregion

      
    }
}
