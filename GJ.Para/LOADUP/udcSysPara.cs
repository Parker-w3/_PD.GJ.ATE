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

namespace GJ.Para.LOADUP
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
            txtSpotCheckSn = new TextBox[]
            {
                txtSpotCheckSn1, txtSpotCheckSn2, txtSpotCheckSn3, txtSpotCheckSn4,
                txtSpotCheckSn5, txtSpotCheckSn6, txtSpotCheckSn7, txtSpotCheckSn8
            };
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

        private TextBox[] txtSpotCheckSn = null;

        #region 面板回调函数
        private void udcSysPara_Load(object sender, EventArgs e)
        {
            string[] comNames = SerialPort.GetPortNames();
            cmbIdCom.Items.Clear();
            cmbEloadCom.Items.Clear();
            for (int i = 0; i < comNames.Length; i++)
            {
                cmbIdCom.Items.Add(comNames[i]);
                cmbEloadCom.Items.Add(comNames[i]);
                cmbACRCom.Items.Add(comNames[i]);
            }
            txtPlcIP.Text = CSysPara.mVal.plcIp;
            txtPlcPort.Text = CSysPara.mVal.plcPort.ToString();
            cmbIdCom.Text = CSysPara.mVal.idCom;
            cmbEloadCom.Text = CSysPara.mVal.eloadCom;
            cmbACRCom.Text = CSysPara.mVal.ACRCom;
            txtMySql.Text = CSysPara.mVal.mySqlIp;
            TxtoffsetV.Text = CSysPara.mVal.OffSetV.ToString();
            TxtoffsetCur.Text = CSysPara.mVal.OffSetCur.ToString();

            for (int i = 0; i < txtSpotCheckSn.Length; i++)
            {
                txtSpotCheckSn[i].Text = CSysPara.mVal.SpotCheckSn[i];
            }

            txtACDelay.Text=CSysPara.mVal.acDelayTimes.ToString();
            txtTestTimes.Text=CSysPara.mVal.testTimes.ToString();
            txtMesDelayTime.Text = CSysPara.mVal.MesDelatTime.ToString();
            chkACOn.Checked=CSysPara.mVal.chkAcOn;
            chkFailGo.Checked=CSysPara.mVal.chkFailGo;
            chkFailWait.Checked = CSysPara.mVal.chkFailWait;
            chkAcMeter.Checked = CSysPara.mVal.chkAcMeter;   

            txtConnectTimes.Text=CSysPara.mVal.connectorTimes.ToString();
            txtFixTimes.Text=CSysPara.mVal.fixtureTimes.ToString();
            txtFailTimes.Text=CSysPara.mVal.failTimes.ToString();  

            txtModelPath.Text = CSysPara.mVal.modelPath;
            chkSaveReport.Checked = CSysPara.mVal.saveReport;
            txtSaveTimes.Text = CSysPara.mVal.saveReportTimes.ToString();
            txtReportPath.Text = CSysPara.mVal.reportPath;

            chkScanBar.Checked = CSysPara.mVal.handBandSn;
            txtSnLen.Text = CSysPara.mVal.snLen.ToString();
            txtSnSpec.Text = CSysPara.mVal.snSpec;
            txtDayOut.Text = CSysPara.mVal.dayTimeOut.ToString();

            chkMes.Checked = CSysPara.mVal.conMes;
            ChkPTMes.Checked = CSysPara.mVal.PTconMes;
            txtMesUserName.Text = CSysPara.mVal.MesUserName;
            txtMesPassWord.Text = CSysPara.mVal.MesPassWord;
            txtMesStation.Text = CSysPara.mVal.MesStation;
               
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要保存系统参数设置?", "系统参数设置", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
               DialogResult.Yes)
            {
                CSysPara.mVal = new CSysPara();
                CSysPara.mVal.plcIp = txtPlcIP.Text;
                CSysPara.mVal.plcPort = System.Convert.ToInt32(txtPlcPort.Text);
                CSysPara.mVal.idCom = cmbIdCom.Text;
                CSysPara.mVal.eloadCom = cmbEloadCom.Text;
                CSysPara.mVal.ACRCom = cmbACRCom.Text;
                CSysPara.mVal.mySqlIp = txtMySql.Text;
                CSysPara.mVal.OffSetV = System.Convert.ToDouble(TxtoffsetV.Text);
                CSysPara.mVal.OffSetCur = System.Convert.ToDouble(TxtoffsetCur.Text);

                CSysPara.mVal.acDelayTimes = System.Convert.ToInt32(txtACDelay.Text);
                CSysPara.mVal.testTimes = System.Convert.ToInt32(txtTestTimes.Text);
                CSysPara.mVal.MesDelatTime = System.Convert.ToInt32(txtMesDelayTime.Text);
                CSysPara.mVal.chkAcOn = chkACOn.Checked;
                CSysPara.mVal.chkFailGo = chkFailGo.Checked;
                CSysPara.mVal.chkFailWait = chkFailWait.Checked;
                CSysPara.mVal.chkAcMeter = chkAcMeter.Checked;

                for (int i = 0; i < txtSpotCheckSn.Length; i++)
                {
                    CSysPara.mVal.SpotCheckSn[i] = txtSpotCheckSn[i].Text;

                }
                CSysPara.mVal.connectorTimes = System.Convert.ToInt32(txtConnectTimes.Text);
                CSysPara.mVal.fixtureTimes = System.Convert.ToInt32(txtFixTimes.Text);
                CSysPara.mVal.failTimes = System.Convert.ToInt32(txtFailTimes.Text);

                CSysPara.mVal.modelPath = txtModelPath.Text;
                CSysPara.mVal.saveReport = chkSaveReport.Checked;
                CSysPara.mVal.saveReportTimes = System.Convert.ToInt32(txtSaveTimes.Text);
                CSysPara.mVal.reportPath = txtReportPath.Text;

                CSysPara.mVal.handBandSn = chkScanBar.Checked;
                CSysPara.mVal.snLen = System.Convert.ToInt16(txtSnLen.Text);
                CSysPara.mVal.snSpec = txtSnSpec.Text;

                CSysPara.mVal.conMes = chkMes.Checked;
                CSysPara.mVal.PTconMes = ChkPTMes.Checked;
                CSysPara.mVal.dayTimeOut = System.Convert.ToInt16(txtDayOut.Text);
                CSysPara.mVal.MesUserName = txtMesUserName.Text;
                CSysPara.mVal.MesPassWord = txtMesPassWord.Text;
                CSysPara.mVal.MesStation = txtMesStation.Text;

                CSysSet<CSysPara>.save(CSysPara.mVal);
                OnSysSave.OnEvented(new CSysSaveArgs(true));
            }
        }
        private void btnClrConnector_Click(object sender, EventArgs e)
        {

        }

        private void btnClrFix_Click(object sender, EventArgs e)
        {

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
