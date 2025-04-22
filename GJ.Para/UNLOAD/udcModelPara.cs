using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Para.Base;
using System.IO; 

namespace GJ.Para.UNLOAD
{
    public partial class udcModelPara : udcModelBase 
    {

        public udcModelPara()
        {
            InitializeComponent();
        }

        #region 面板回调函数
        private void udcModelPara_Load(object sender, EventArgs e)
        {
            clr();
        }
        //private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbHZ.SelectedIndex == 0)
        //    {
        //        labCCMin.Text = "负载设置(A):";
        //        labCCMax.Text = "负载下限(A):";
        //        labRRMin.Text = "负载上限(A):";
        //    }
        //    else
        //    {
        //        labCCMin.Text = "负载设置(V):";
        //        labCCMax.Text = "负载下限(A):";
        //        labRRMin.Text = "负载上限(A):";
        //    }
        //}
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

        #region 方法
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
            CmbLcrModel.SelectedIndex = 0;
            cmbLCRTestModel.Text = "CS,RS";
            cmbHZ.SelectedIndex = 0;
            CmbLcrVolt.SelectedIndex = 0;
            cmbR.SelectedIndex = 0;
            cmbSpeed.SelectedIndex = 0;

            labCCMin.Text = "C下限(nF):";
            labCCMax.Text = "C上限(nF):";
            labRRMin.Text = "R下限(Ω）:";
            labRRMax.Text = "R上限(Ω):";

            txtCCMin.Text = "0";
            TxtCCMax.Text = "0";
            txtRRMin.Text = "0"; 


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
                if (CSysPara.mVal.modelPath == "")
                {
                    fileDirectry = Application.StartupPath + "\\Model";
                    if (!Directory.Exists(fileDirectry))
                        Directory.CreateDirectory(fileDirectry);
                }
                else
                    fileDirectry = CSysPara.mVal.modelPath;  
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = fileDirectry;
                dlg.Filter = "spec files (*.spec)|*.spec";
                dlg.FileName = txtModel.Text;   
                if (dlg.ShowDialog() != DialogResult.OK)
                   return;
                modelPath = dlg.FileName;

                CModelPara modelPara = new CModelPara();

                CModelSet<CModelPara>.load(modelPath, ref modelPara);

                txtModel.Text = modelPara.model;
                txtCustom.Text = modelPara.custom;
                txtVersion.Text = modelPara.version;

                txtReleaseName.Text=modelPara.releaseName;
                pickerDate.Value = System.Convert.ToDateTime(modelPara.releaseDate);
                cmbHZ.Text = modelPara.LCRHZ.ToString();
                CmbLcrModel.Text = modelPara.LCRMode;
                CmbLcrVolt.Text = modelPara.LCRVolt.ToString();
                cmbLCRTestModel.Text  = modelPara.LCRTestMode;
                cmbR.Text = modelPara.LCRRmax;
                cmbSpeed.Text = modelPara.LCRSpeed;

                txtCCMin.Text = modelPara.CCMin.ToString();
                TxtCCMax.Text = modelPara.CCMax.ToString();
                txtRRMin.Text = modelPara.RRMin.ToString();
                TxtRRMax.Text = modelPara.RRMax.ToString();
              
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
                if (txtModel.Text == "")
                {
                    MessageBox.Show("请输入机种保存名称");
                    return;
                }              
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
                dlg.Filter = "spec files (*.spec)|*.spec";
                dlg.FileName = txtModel.Text;
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                modelPath = dlg.FileName;
    
                CModelPara modelPara = new CModelPara();
                modelPara.model = txtModel.Text;
                modelPara.custom = txtCustom.Text;
                modelPara.version = txtVersion.Text;

                modelPara.releaseName = txtReleaseName.Text;
                modelPara.releaseDate = pickerDate.Value.Date.ToString();
                modelPara.LCRHZ= System .Convert .ToInt32( cmbHZ.Text) ;
                modelPara.LCRMode=CmbLcrModel.Text ;
                modelPara.LCRVolt=System .Convert .ToDouble (  CmbLcrVolt.Text) ;
                modelPara.LCRTestMode= cmbLCRTestModel.Text ;
                modelPara.LCRRmax = cmbR.Text;
                modelPara.LCRSpeed = cmbSpeed.Text;

                modelPara.CCMin= System .Convert .ToDouble (txtCCMin.Text) ;
                modelPara.CCMax=System .Convert .ToDouble (TxtCCMax.Text) ;
                modelPara.RRMin = System.Convert.ToDouble(txtRRMin.Text);
                modelPara.RRMax = System.Convert.ToDouble(TxtRRMax.Text);

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
