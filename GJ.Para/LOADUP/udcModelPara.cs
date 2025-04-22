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

namespace GJ.Para.LOADUP
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
        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMode.SelectedIndex == 0)
            {
                labLoadSet.Text = "负载设置(A):";
                labLoadMin.Text = "负载下限(A):";
                labLoadMax.Text = "负载上限(A):";
            }
            else
            {
                labLoadSet.Text = "负载设置(V):";
                labLoadMin.Text = "负载下限(A):";
                labLoadMax.Text = "负载上限(A):";
            }
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
            cmbACV.SelectedIndex = 0;
            txtACVL.Text = "0";
            txtACVH.Text = "0";  

            txtVname.Text = "+5V";
            txtVmin.Text = "0";
            txtVmax.Text = "0";

            cmbMode.SelectedIndex = 0;            
            txtVon.Text = "0";

            labLoadSet.Text = "负载设置(A):";
            labLoadMin.Text = "负载下限(A):";
            labLoadMax.Text = "负载上限(A):";  

            txtLoadSet.Text = "0";
            txtLoadmin.Text = "0";
            txtLoadmax.Text = "0"; 


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
                cmbACV.SelectedIndex = modelPara.acv;
                txtACVL.Text = modelPara.acvMin.ToString();
                txtACVH.Text = modelPara.acvMax.ToString();    

                txtVname.Text = modelPara.vName;
                txtVmin.Text = modelPara.vMin.ToString();
                txtVmax.Text = modelPara.vMax.ToString();

                cmbMode.SelectedIndex=modelPara.loadMode;
                txtVon.Text = modelPara.loadVon.ToString();

                txtLoadSet.Text = modelPara.loadSet.ToString();
                txtLoadmin.Text = modelPara.loadMin.ToString();
                txtLoadmax.Text = modelPara.loadMax.ToString();

              
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
                modelPara.acv = cmbACV.SelectedIndex;
                modelPara.acvMin = System.Convert.ToDouble(txtACVL.Text);
                modelPara.acvMax = System.Convert.ToDouble(txtACVH.Text);   

                modelPara.vName = txtVname.Text;
                modelPara.vMin = System.Convert.ToDouble(txtVmin.Text);
                modelPara.vMax = System.Convert.ToDouble(txtVmax.Text);
                    
                modelPara.loadMode = cmbMode.SelectedIndex;
                modelPara.loadVon = System.Convert.ToDouble(txtVon.Text);

                modelPara.loadSet = System.Convert.ToDouble(txtLoadSet.Text);
                modelPara.loadMin = System.Convert.ToDouble(txtLoadmin.Text);
                modelPara.loadMax = System.Convert.ToDouble(txtLoadmax.Text);

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
