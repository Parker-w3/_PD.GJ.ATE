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
//using GJ.Dev.HIPOT;

namespace GJ.Para.TURNON
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
            cmbChromaEloadCom.Items.Clear();
         
            string[] comNames = SerialPort.GetPortNames();              
            for (int i = 0; i < comNames.Length; i++)
            {
                cmbIOCom.Items.Add(comNames[i]);
                cmbChromaEloadCom.Items.Add(comNames[i]);
                cmbIDCom.Items.Add(comNames[i]);
                cmbMonCom.Items.Add(comNames[i]);
            }
            txtSerTcpIP.Text = CSysPara.mVal.serIP;
            txtSerTcpPort.Text = CSysPara.mVal.serPort.ToString();
           
            cmbMonCom.Text = CSysPara.mVal.MonCom;

            cmbIOCom.Text = CSysPara.mVal.ioCom;
        
            cmbChromaEloadCom.Text = CSysPara.mVal.ChromaEloadCom ;
            cmbIDCom.Text = CSysPara.mVal.IDCom;
            chkID.Checked = CSysPara.mVal.ChkID;

            txtIoDelayMs.Text = CSysPara.mVal.ioDelayMs.ToString();
            txtMonDelayMs.Text = CSysPara.mVal.monDelayMs.ToString();
            txtDisChargerTime.Text = CSysPara.mVal.disChargerTime.ToString();
            txtStepDelayTime.Text = CSysPara.mVal.StepDelayTime.ToString();

            txtModelPath.Text = CSysPara.mVal.modelPath;
            chkSaveReport.Checked = CSysPara.mVal.saveReport;
            txtSaveTimes.Text = CSysPara.mVal.saveReportTimes.ToString();
            txtReportPath.Text = CSysPara.mVal.reportPath;

            chkImpPrg.Checked = CSysPara.mVal.chkImpPrg;
            chkAutoModel.Checked = CSysPara.mVal.chkAutoModel;
            chkSaveTcpLog.Checked = CSysPara.mVal.chkSaveTcpLog;

            ChkMes.Checked = CSysPara.mVal.conMes;
            cmbSlotMax.Text=CSysPara.mVal.C_SLOT_MAX.ToString();

            ChkA.Checked = CSysPara.mVal.ChkCur;
               
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

                CSysPara.mVal.serIP = txtSerTcpIP.Text;
                CSysPara.mVal.serPort = System.Convert.ToInt32(txtSerTcpPort.Text);
                CSysPara.mVal.MonCom = cmbMonCom.Text;
               

                CSysPara.mVal.ioCom = cmbIOCom.Text;
              
                CSysPara.mVal.ChromaEloadCom  = cmbChromaEloadCom.Text;
                CSysPara.mVal.IDCom = cmbIDCom.Text;
                CSysPara.mVal.ChkID = chkID.Checked;

                CSysPara.mVal.ioDelayMs = System.Convert.ToInt32(txtIoDelayMs.Text);
                CSysPara.mVal.monDelayMs = System.Convert.ToInt32(txtMonDelayMs.Text);
                CSysPara.mVal.disChargerTime = System.Convert.ToInt32(txtDisChargerTime .Text );
                CSysPara.mVal.StepDelayTime = System.Convert.ToInt32(txtStepDelayTime .Text ); 

                CSysPara.mVal.modelPath = txtModelPath.Text;
                CSysPara.mVal.saveReport = chkSaveReport.Checked;
                CSysPara.mVal.saveReportTimes = System.Convert.ToInt32(txtSaveTimes.Text);
                CSysPara.mVal.reportPath = txtReportPath.Text;

                CSysPara.mVal.chkImpPrg = chkImpPrg.Checked;
                CSysPara.mVal.chkAutoModel = chkAutoModel.Checked;
                CSysPara.mVal.chkSaveTcpLog = chkSaveTcpLog.Checked;

              
                CSysPara.mVal.C_SLOT_MAX = System.Convert.ToInt16(cmbSlotMax.Text);
                CSysPara.mVal.conMes = ChkMes.Checked;

                CSysPara.mVal.ChkCur = ChkA.Checked;
                CSysSet<CSysPara>.save(CSysPara.mVal);
                OnSysSave.OnEvented(new CSysSaveArgs(true));
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            OnSysCancel.OnEvented(new CSysCancelArgs(true));  
        }
        #endregion

  
      
    }
}
