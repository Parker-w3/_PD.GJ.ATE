using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GJ.Para.Base;
using GJ.Para.Udc.BURNIN;   
  
namespace GJ.Para.BURNIN
{
    public partial class udcModelPara : udcModelBase
    {

        #region 构造函数
        public udcModelPara()
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
        }
        #endregion

        #region 面板回调函数
        private void udcModelPara_Load(object sender, EventArgs e)
        {
            cmbACV.Items.Clear();
            cmbACV.Items.Add(220);
            cmbACV.Items.Add(110);
            cmbACV.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbACV.SelectedIndex = 0; 

            cmbDCVNum.Items.Clear();
            cmbDCVNum.Items.Add(1);
            cmbDCVNum.Items.Add(2);
            cmbDCVNum.Items.Add(3);
            cmbDCVNum.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDCVNum.SelectedIndex = 0; 

            cmbSeqNo.Items.Clear();
            cmbSeqNo.Items.Add(1);
            cmbSeqNo.Items.Add(2);
            cmbSeqNo.Items.Add(3);
            cmbSeqNo.Items.Add(4);
            cmbSeqNo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSeqNo.SelectedIndex = 0;

            cmbQCTYPE.Items.Clear();
            cmbQCTYPE.Items.Add("QC2.0");
            cmbQCTYPE.Items.Add("QC3.0");
            cmbQCTYPE.Items.Add("MTK");
            cmbQCTYPE.Items.Add("海思");
            cmbQCTYPE.SelectedIndex = 3;

            txtBITime.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtTSet.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtTLP.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtTHP.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtHAlarm.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtTOpen.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtTClose.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);

            setOnOffWave();
        }
        private void cmbDCVNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDCVUI(System.Convert.ToInt32(cmbDCVNum.Text));   
        }
        private void cmbSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            setOnOffUI(System.Convert.ToInt32(cmbSeqNo.Text));
        }
        private void OnTextKeyPressIsNumber(object sender, KeyPressEventArgs e)
        {
            //char-8为退格键
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)'.')
                e.Handled = true; 
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            setOnOffWave(); 
        }
        #endregion

        #region 面板按钮事件
        public override void OnModelBtnClick(object sender, CModelBtnArgs e)
        {
            switch (e.BtnNo)
            {
                case CModelBtnArgs.EBtnName.新建:
                    clr();
                    OnModelRepose.OnEvented(new CModelReposeArgs(CModelReposeArgs.ERepose.新建)); 
                    break;
                case CModelBtnArgs.EBtnName.打开:
                    open();
                    break;
                case CModelBtnArgs.EBtnName.保存:
                    save();
                    break;
                case CModelBtnArgs.EBtnName.退出:
                    OnModelRepose.OnEvented(new CModelReposeArgs(CModelReposeArgs.ERepose.退出)); 
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 面板控件
        private TableLayoutPanel panelV = null;
        private List<TextBox> udclistVname =new List<TextBox>();
        private List<TextBox> udclistVmin = new List<TextBox>();
        private List<TextBox> udclistVmax = new List<TextBox>();
        private List<TextBox> udclistISet = new List<TextBox>();
        private List<TextBox> udclistImin = new List<TextBox>();
        private List<TextBox> udclistImax = new List<TextBox>();
        private List<ComboBox> udclistQCVolt = new List<ComboBox>(); 

        private TableLayoutPanel panelOnOff = null;
        private List<udcOnOff> udcListOnOff = new List<udcOnOff>();

        private udcOnOffWave udcWaveOnOff = new udcOnOffWave(); 
        #endregion

        #region 字段
        private CModelPara modelPara = new CModelPara();
        #endregion

        #region 方法
        /// <summary>
        /// 设置DCV输出控件
        /// </summary>
        /// <param name="dcvNum"></param>
        private void setDCVUI(int dcvNum)
        {
            try
            {
                //清除            
                udclistVname.Clear();
                udclistVmin.Clear();
                udclistVmax.Clear();
                udclistISet.Clear();
                udclistImin.Clear();
                udclistImax.Clear();
                udclistQCVolt.Clear(); 
                if (panelV != null)
                {
                    foreach (Control item in panelV.Controls)
                        item.Dispose();
                    panelV.Dispose();
                    panelV = null;
                }

                string[] dcvName = new string[] { "+5V", "+7V","+9V", "+12V" };
                string[] dcvTitle = new string[] { "输出通道","输出名称","电压下限(V)","电压上限(V)",
                                               "负载设置(A)","电流下限(A)","电流上限(A)","对应快充电压"};

                panelV = new TableLayoutPanel();
                panelV.GetType().GetProperty("DoubleBuffered",
                                        System.Reflection.BindingFlags.Instance |
                                        System.Reflection.BindingFlags.NonPublic)
                                        .SetValue(panelV, true, null);
                panelV.Dock = DockStyle.Fill;
                panelV.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                panelV.RowCount = dcvNum + 2;
                panelV.ColumnCount = dcvTitle.Length + 1;
                for (int i = 0; i < dcvNum + 1; i++)
                    panelV.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
                panelV.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                for (int i = 0; i < dcvTitle.Length; i++)
                    panelV.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
                panelV.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

                ////标题               
                for (int i = 0; i < dcvTitle.Length; i++)
                {
                    Label labTitle = new Label();
                    labTitle = new Label();
                    labTitle.Dock = DockStyle.Fill;
                    labTitle.Margin = new Padding(0);
                    labTitle.TextAlign = ContentAlignment.MiddleCenter;
                    labTitle.Text = dcvTitle[i];
                    panelV.Controls.Add(labTitle, i, 0);
                }

                for (int iRow = 0; iRow < dcvNum; iRow++)
                {

                    ////输出通道
                    Label labCH = new Label();
                    labCH.Dock = DockStyle.Fill;
                    labCH.Margin = new Padding(0);
                    labCH.TextAlign = ContentAlignment.MiddleCenter;
                    labCH.Text = "CH" + (iRow + 1).ToString();
                    panelV.Controls.Add(labCH, 0, iRow + 1);
                    ////输出名称   
                    TextBox txtName = new TextBox();
                    txtName.Dock = DockStyle.Fill;
                    txtName.TextAlign = HorizontalAlignment.Center;
                    txtName.Text = dcvName[iRow];
                    udclistVname.Add(txtName);
                    panelV.Controls.Add(udclistVname[iRow], 1, iRow + 1);
                    ////电压下限
                    TextBox txtVmin = new TextBox();
                    txtVmin.Dock = DockStyle.Fill;
                    txtVmin.TextAlign = HorizontalAlignment.Center;
                    txtVmin.Text = "0";
                    txtVmin.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    udclistVmin.Add(txtVmin);
                    panelV.Controls.Add(udclistVmin[iRow], 2, iRow + 1);
                    ////电压上限
                    TextBox txtVmax = new TextBox();
                    txtVmax.Dock = DockStyle.Fill;
                    txtVmax.TextAlign = HorizontalAlignment.Center;
                    txtVmax.Text = "0";
                    txtVmax.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    udclistVmax.Add(txtVmax);
                    panelV.Controls.Add(udclistVmax[iRow], 3, iRow + 1);
                    ////负载设置
                    TextBox txtISet = new TextBox();
                    txtISet.Dock = DockStyle.Fill;
                    txtISet.TextAlign = HorizontalAlignment.Center;
                    txtISet.Text = "0";
                    txtISet.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    udclistISet.Add(txtISet);
                    panelV.Controls.Add(udclistISet[iRow], 4, iRow + 1);
                    ////电流下限
                    TextBox txtImin = new TextBox();
                    txtImin.Dock = DockStyle.Fill;
                    txtImin.TextAlign = HorizontalAlignment.Center;
                    txtImin.Text = "0";
                    txtImin.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    udclistImin.Add(txtImin);
                    panelV.Controls.Add(udclistImin[iRow], 5, iRow + 1);
                    ////电流上限
                    TextBox txtImax = new TextBox();
                    txtImax.Dock = DockStyle.Fill;
                    txtImax.TextAlign = HorizontalAlignment.Center;
                    txtImax.Text = "0";
                    txtImax.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
                    udclistImax.Add(txtImax);
                    panelV.Controls.Add(udclistImax[iRow], 6, iRow + 1);
                    ////快充电压
                    ComboBox cmbQCVolt = new ComboBox();
                    cmbQCVolt.DropDownStyle = ComboBoxStyle.DropDownList;   
                    cmbQCVolt.Dock = DockStyle.Fill;
                    cmbQCVolt.Items.Clear();
                    cmbQCVolt.Items.Add("0:+5V");
                    cmbQCVolt.Items.Add("1:+7V");
                    cmbQCVolt.Items.Add("2:+9V");
                    cmbQCVolt.Items.Add("3:+12V");
                    cmbQCVolt.Items.Add("4:+20V");
                    cmbQCVolt.SelectedIndex = 0; 
                    udclistQCVolt.Add(cmbQCVolt);
                    panelV.Controls.Add(udclistQCVolt[iRow], 7, iRow + 1);                    
                }
                panel1.Controls.Add(panelV, 0, 3);   
            }
            catch (Exception)
            {
                
                throw;
            }            

        }
        /// <summary>
        /// 设置DCV输出数据
        /// </summary>
        private void setDCVVal(int dcvNum)
        {
            try
            {
                if (modelPara == null || modelPara.DCVList.Count!= dcvNum)
                    return;
                for (int i = 0; i < dcvNum; i++)
                {
                    udclistVname[i].Text = modelPara.DCVList[i].Vname;
                    udclistVmin[i].Text = modelPara.DCVList[i].Vmin.ToString();
                    udclistVmax[i].Text = modelPara.DCVList[i].Vmax.ToString();
                    udclistISet[i].Text = modelPara.DCVList[i].ISet.ToString();
                    udclistImin[i].Text = modelPara.DCVList[i].Imin.ToString();
                    udclistImax[i].Text = modelPara.DCVList[i].Imax.ToString();
                    if (modelPara.DCVList[i].QC_VOLT == 0)
                        udclistQCVolt[i].Text = "0:+5V";
                    else if (modelPara.DCVList[i].QC_VOLT == 1)
                        udclistQCVolt[i].Text = "1:+7V";
                    else if (modelPara.DCVList[i].QC_VOLT == 2)
                        udclistQCVolt[i].Text = "2:+9V";
                    else if (modelPara.DCVList[i].QC_VOLT == 3)
                        udclistQCVolt[i].Text = "3:+12V";
                    else if (modelPara.DCVList[i].QC_VOLT == 4)
                        udclistQCVolt[i].Text = "4:+20V";
 
                }
            }
            catch (Exception)
            {
                
                throw;
            }           
        }
        /// <summary>
        /// 获取DCV输出数据
        /// </summary>
        /// <param name="dcvNum"></param>
        private void getDCVVal(int dcvNum)
        {
            if (modelPara == null)
                return;
            modelPara.DCVList.Clear();
 
            for (int i = 0; i < dcvNum; i++)
            {

                CDCVPara dcvPara = new CDCVPara();

                dcvPara.Vname = udclistVname[i].Text;
                dcvPara.Vmin = System.Convert.ToDouble(udclistVmin[i].Text);
                dcvPara.Vmax = System.Convert.ToDouble(udclistVmax[i].Text);
                dcvPara.ISet = System.Convert.ToDouble(udclistISet[i].Text);
                dcvPara.Imin = System.Convert.ToDouble(udclistImin[i].Text);
                dcvPara.Imax = System.Convert.ToDouble(udclistImax[i].Text);
                dcvPara.QC_VOLT = udclistQCVolt[i].SelectedIndex;   
                modelPara.DCVList.Add(dcvPara);
            }
        }

        /// <summary>
        /// 设置ONOFF控件
        /// </summary>
        /// <param name="OnOffSeqNo"></param>
        private void setOnOffUI(int OnOffSeqNo)
        {
            try
            {
                udcListOnOff.Clear();

                if (panelOnOff != null)
                {
                    foreach (Control item in panelOnOff.Controls)
                        item.Dispose();
                    panelOnOff.Dispose();
                    panelOnOff = null;
                }
                panelOnOff = new TableLayoutPanel();
                panelOnOff.GetType().GetProperty("DoubleBuffered",
                                       System.Reflection.BindingFlags.Instance |
                                       System.Reflection.BindingFlags.NonPublic)
                                       .SetValue(panelOnOff, true, null);
                panelOnOff.Dock = DockStyle.Fill;
                panelOnOff.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                panelOnOff.RowCount = 1;
                panelOnOff.ColumnCount =4;
                for (int i = 0; i < 4; i++)
                    panelOnOff.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                for (int i = 0; i < OnOffSeqNo; i++)
			   {
			      udcListOnOff.Add(new udcOnOff(i));
                  udcListOnOff[i].Dock=DockStyle.Fill;
                  panelOnOff.Controls.Add(udcListOnOff[i], i, 0);  
			   }
                panel1.Controls.Add(panelOnOff, 0, 5);  
            }
            catch (Exception)
            {                
                throw;
            }
        }
        /// <summary>
        /// 设置控件数据
        /// </summary>
        /// <param name="OnOffSeqNo"></param>
        private void setOnOffVal(int OnOffSeqNo)
        {
            try
            {
                if (modelPara == null || modelPara.OnOffList.Count != OnOffSeqNo)
                    return;
                for (int i = 0; i < OnOffSeqNo; i++)
                {
                    udcListOnOff[i].m_chkSecUnit = modelPara.OnOffList[i].chkSec;
                    udcListOnOff[i].m_onoffTime = modelPara.OnOffList[i].OnOffTime;
                    udcListOnOff[i].m_onTime = modelPara.OnOffList[i].OnTime;
                    udcListOnOff[i].m_offTime = modelPara.OnOffList[i].OffTime;
                    udcListOnOff[i].m_QCVolt = modelPara.OnOffList[i].QC_VOLT;   
                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// 读取控件数据
        /// </summary>
        /// <param name="OnOffSeqNo"></param>
        private void getOnOffVal(int OnOffSeqNo)
        {
            if (modelPara == null)
                return;
            modelPara.OnOffList.Clear();

            for (int i = 0; i < OnOffSeqNo; i++)
            {

                COnOffSetting onoffPara = new COnOffSetting();

                onoffPara.chkSec = udcListOnOff[i].m_chkSecUnit;

                onoffPara.OnOffTime = udcListOnOff[i].m_onoffTime;

                onoffPara.OnTime = udcListOnOff[i].m_onTime;

                onoffPara.OffTime = udcListOnOff[i].m_offTime;

                onoffPara.QC_VOLT = udcListOnOff[i].m_QCVolt;   

                modelPara.OnOffList.Add(onoffPara); 
            }
        }

        /// <summary>
        /// OnOff曲线
        /// </summary>
        private void setOnOffWave()
        {
            udcWaveOnOff.Dock = DockStyle.Fill;
            panel1.Controls.Add(udcWaveOnOff, 0, 6);   
            udcWaveOnOff.OnOffTime.listACVolt.Clear();
            udcWaveOnOff.OnOffTime.listOnOffTime.Clear();
            udcWaveOnOff.OnOffTime.listOnTime.Clear();
            udcWaveOnOff.OnOffTime.listOffTime.Clear();

            if (udcListOnOff == null || udcListOnOff.Count==0)
            {                
                udcWaveOnOff.OnOffTime.mBITime = 0;
                udcWaveOnOff.OnOffTime.mOnOffNum = 0;
            }
            else
            {
                udcWaveOnOff.mCurRunTime = 0; 
                udcWaveOnOff.OnOffTime.mBITime = System.Convert.ToDouble(txtBITime.Text);
                udcWaveOnOff.OnOffTime.mOnOffNum = System.Convert.ToInt32(cmbSeqNo.Text);
                for (int i = 0; i < udcWaveOnOff.OnOffTime.mOnOffNum; i++)
                {
                    udcWaveOnOff.OnOffTime.listACVolt.Add(System.Convert.ToInt16(cmbACV.Text));
                    int onoffTimes = (udcListOnOff[i].m_onTime + udcListOnOff[i].m_offTime) * udcListOnOff[i].m_onoffTime;
                    udcWaveOnOff.OnOffTime.listOnOffTime.Add(onoffTimes);
                    udcWaveOnOff.OnOffTime.listOnTime.Add(udcListOnOff[i].m_onTime);
                    udcWaveOnOff.OnOffTime.listOffTime.Add(udcListOnOff[i].m_offTime);
                }            
            }
            this.Refresh(); 
        }
        #endregion

        #region 文件方法
        /// <summary>
        /// 新建
        /// </summary>
        private void clr()
        {
            txtModel.Text = "";
            txtCustom.Text = "";
            txtVersion.Text = "";

            txtReleaseName.Text = "";
            pickerDate.Value = DateTime.Now;

        }
        /// <summary>
        /// 打开
        /// </summary>
        private void open()
        {
            try
            {
                string modelPath = string.Empty;
                string fileDirectry = string.Empty;
                fileDirectry = CSysPara.mVal.modelPath;  
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = fileDirectry;
                dlg.Filter = "BI files (*.bi)|*.bi";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;                
                modelPath = dlg.FileName;     
                CModelSet<CModelPara>.load(modelPath, ref modelPara);

                txtModel.Text = modelPara.model;
                txtCustom.Text = modelPara.custom;
                txtVersion.Text = modelPara.version;
                txtReleaseName.Text = modelPara.releaseName;
                pickerDate.Value = System.Convert.ToDateTime(modelPara.releaseDate);

                txtBITime.Text = modelPara.BITime.ToString();
                cmbACV.Text = modelPara.ACV.ToString();
                cmbDCVNum.Text = modelPara.DCVNum.ToString();
                cmbSeqNo.Text = modelPara.OnOffSeqNum.ToString();

                txtTSet.Text = modelPara.TSet.ToString();
                txtTLP.Text = modelPara.TLP.ToString();
                txtTHP.Text = modelPara.THP.ToString();
                txtHAlarm.Text = modelPara.THAlarm.ToString();
                txtTOpen.Text = modelPara.TOPEN.ToString();
                txtTClose.Text = modelPara.TCLOSE.ToString();

                if (modelPara.QC_TYPE==EQCV.QC2_0)
                    cmbQCTYPE.Text = "QC2.0";
                else if (modelPara.QC_TYPE == EQCV.QC3_0)
                    cmbQCTYPE.Text = "QC3.0";
                else if (modelPara.QC_TYPE == EQCV.MTK)
                    cmbQCTYPE.Text = "MTK"; 
                setDCVUI(modelPara.DCVNum);
                setDCVVal(modelPara.DCVNum);

                setOnOffUI(modelPara.OnOffSeqNum);
                setOnOffVal(modelPara.OnOffSeqNum);

                setOnOffWave();
  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
         
        }
        /// <summary>
        ///保存
        /// </summary>
        private void save()
        {
            try
            {
                string modelPath = string.Empty; 
                string fileDirectry = string.Empty;
                if (CSysPara.mVal.modelPath == "")
                {
                  fileDirectry = Application.StartupPath + "\\Model";
                  if (!Directory.Exists(fileDirectry))
                  Directory.CreateDirectory(fileDirectry);
                }                    
                else
                    fileDirectry = CSysPara.mVal.modelPath;                
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = fileDirectry;
                dlg.Filter = "BI files (*.bi)|*.bi";
                dlg.FileName = txtModel.Text;
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                modelPath = dlg.FileName;

                modelPara.model = txtModel.Text;
                modelPara.custom = txtCustom.Text;
                modelPara.version = txtVersion.Text;
                modelPara.releaseName = txtReleaseName.Text;
                modelPara.releaseDate = pickerDate.Value.Date.ToString();

                modelPara.BITime = System.Convert.ToDouble(txtBITime.Text);
                modelPara.ACV = System.Convert.ToInt32(cmbACV.Text);
                modelPara.DCVNum = System.Convert.ToInt32(cmbDCVNum.Text);
                modelPara.OnOffSeqNum = System.Convert.ToInt32(cmbSeqNo.Text);

                modelPara.TSet = System.Convert.ToDouble(txtTSet.Text);
                modelPara.TLP = System.Convert.ToDouble(txtTLP.Text);
                modelPara.THP = System.Convert.ToDouble(txtTHP.Text);
                modelPara.THAlarm = System.Convert.ToDouble(txtHAlarm.Text);
                modelPara.TOPEN = System.Convert.ToDouble(txtTOpen.Text);
                modelPara.TCLOSE = System.Convert.ToDouble(txtTClose.Text);

                modelPara.QC_TYPE = (EQCV)cmbQCTYPE.SelectedIndex;   

                getDCVVal(modelPara.DCVNum);

                getOnOffVal(modelPara.OnOffSeqNum);

                setOnOffWave();

                CModelSet<CModelPara>.save(modelPath, modelPara);
                MessageBox.Show("机种参数保存成功.", "机种保存", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        
        }
        #endregion

    }
}
