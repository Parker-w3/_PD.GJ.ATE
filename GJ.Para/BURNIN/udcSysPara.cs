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
using GJ.PDB; 

namespace GJ.Para.BURNIN
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
        }
        #endregion

        #region 面板回调函数
        private void udcSysPara_Load(object sender, EventArgs e)
        {
            cmbIdCom.Items.Clear();
            cmbMonCom1.Items.Clear();
            cmbMonCom2.Items.Clear();
            cmbMonCom3.Items.Clear();
            cmbERSCom1.Items.Clear();
            cmbERSCom2.Items.Clear();
            string[] comNames = SerialPort.GetPortNames();              
            for (int i = 0; i < comNames.Length; i++)
            {
                cmbIdCom.Items.Add(comNames[i]);
                cmbMonCom1.Items.Add(comNames[i]);
                cmbMonCom2.Items.Add(comNames[i]);
                cmbMonCom3.Items.Add(comNames[i]);
                cmbERSCom1.Items.Add(comNames[i]);
                cmbERSCom2.Items.Add(comNames[i]);
            }           

            txtModelPath.Text = CSysPara.mVal.modelPath;
            chkSaveReport.Checked = CSysPara.mVal.saveReport;
            txtSaveTimes.Text = CSysPara.mVal.saveReportTimes.ToString();
            txtReportPath.Text = CSysPara.mVal.reportPath;

            txtPlcTcpIP.Text = CSysPara.mVal.plcIp;
            txtPlcTcpPort.Text = CSysPara.mVal.plcPort.ToString();
            txtTempPlcTcpIP.Text = CSysPara.mVal.plcTempIp;
            txtTempPlcTcpPort.Text = CSysPara.mVal.plcTempPort.ToString();
            chkMES.Checked = CSysPara.mVal.conMes ;

            cmbIdCom.Text = CSysPara.mVal.idCom;
            cmbMonCom1.Text = CSysPara.mVal.monCom[0];
            cmbMonCom2.Text = CSysPara.mVal.monCom[1];
            cmbMonCom3.Text = CSysPara.mVal.monCom[2];
            cmbERSCom1.Text = CSysPara.mVal.ersCom[0];
            cmbERSCom2.Text = CSysPara.mVal.ersCom[1];
            txtMySQLIP.Text = CSysPara.mVal.mySqlIp;

            chkNoJugdeCur.Checked = CSysPara.mVal.chkNoJugdeCur;
            chkNoLockFail.Checked=CSysPara.mVal.chkNoLockFail;
            txtVLP.Text=CSysPara.mVal.VLP.ToString();
            txtVHP.Text = CSysPara.mVal.VHP.ToString();
            txtILP.Text = CSysPara.mVal.ILP.ToString();
            txtIHP.Text = CSysPara.mVal.IHP.ToString();

            txtFailTimes.Text=CSysPara.mVal.failTimes.ToString();
            txtFixTimes.Text=CSysPara.mVal.fixTimes.ToString();
            txtComFails.Text=CSysPara.mVal.comFailTimes.ToString();
            chkHandIn.Checked = CSysPara.mVal.chkHandIn;

            txtPwrLimit.Text = CSysPara.mVal.PwrLimit.ToString();   
            txtIOFFSET.Text = CSysPara.mVal.IOFFSET.ToString();

            chkgetMTK.Checked = CSysPara.mVal.chkgetMTK;
            txtMTKCount.Text = CSysPara.mVal.getMTKCoutn.ToString();

            chkComLoad.Checked = CSysPara.mVal.comLoad;

            txtResetQCV.Text = CSysPara.mVal.C_RESET_QCV_TIMES.ToString();     
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
                CSysPara.mVal.modelPath = txtModelPath.Text;
                CSysPara.mVal.saveReport = chkSaveReport.Checked;
                CSysPara.mVal.saveReportTimes = System.Convert.ToInt32(txtSaveTimes.Text);
                CSysPara.mVal.reportPath = txtReportPath.Text;  

                CSysPara.mVal.plcIp = txtPlcTcpIP.Text;
                CSysPara.mVal.plcPort = System.Convert.ToInt32(txtPlcTcpPort.Text);
                CSysPara.mVal.plcTempIp = txtTempPlcTcpIP.Text;
                CSysPara.mVal.plcTempPort = System.Convert.ToInt32(txtTempPlcTcpPort.Text );
                CSysPara.mVal.idCom = cmbIdCom.Text;
                CSysPara.mVal.monCom[0]  = cmbMonCom1.Text;
                CSysPara.mVal.monCom[1]  = cmbMonCom2.Text;
                CSysPara.mVal.monCom[2] = cmbMonCom3.Text;
                CSysPara.mVal.ersCom[0] = cmbERSCom1.Text;
                CSysPara.mVal.ersCom[1] = cmbERSCom2.Text;
                CSysPara.mVal.mySqlIp = txtMySQLIP.Text;
                CSysPara.mVal.conMes = chkMES.Checked;

                CSysPara.mVal.chkNoJugdeCur = chkNoJugdeCur.Checked;
                CSysPara.mVal.chkNoLockFail = chkNoLockFail.Checked;
                CSysPara.mVal.VLP = System.Convert.ToDouble(txtVLP.Text);
                CSysPara.mVal.VHP = System.Convert.ToDouble(txtVHP.Text);
                CSysPara.mVal.ILP = System.Convert.ToDouble(txtILP.Text);
                CSysPara.mVal.IHP = System.Convert.ToDouble(txtIHP.Text);

                CSysPara.mVal.failTimes = System.Convert.ToInt32(txtFailTimes.Text);                  
                CSysPara.mVal.fixTimes = System.Convert.ToInt32(txtFixTimes.Text);
                CSysPara.mVal.comFailTimes = System.Convert.ToInt32(txtComFails.Text);
                CSysPara.mVal.chkHandIn = chkHandIn.Checked;

                CSysPara.mVal.PwrLimit = System.Convert.ToDouble(txtPwrLimit.Text);
                CSysPara.mVal.IOFFSET = System.Convert.ToDouble(txtIOFFSET.Text);

                CSysPara.mVal.chkgetMTK = chkgetMTK.Checked;
                CSysPara.mVal.getMTKCoutn = System.Convert.ToInt16(txtMTKCount.Text);

                CSysPara.mVal.comLoad = chkComLoad.Checked;

                CSysPara.mVal.C_RESET_QCV_TIMES = System.Convert.ToInt16(txtResetQCV.Text);       

                CSysSet<CSysPara>.save(CSysPara.mVal);
                OnSysSave.OnEvented(new CSysSaveArgs(true));
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            OnSysCancel.OnEvented(new CSysCancelArgs(true));  
        }
        #endregion


        private void btnClrFixs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清除所有母治具针盘使用次数?", "使用次数", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string er = string.Empty;
                string sysDB = "DBLog\\" + CGlobal.CFlow.flowGUID + ".accdb";
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);
                string sqlCmd = "update RUN_PARA Set UsedNum=0";
                db.excuteSQL(sqlCmd,ref er); 
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      
    }
}
