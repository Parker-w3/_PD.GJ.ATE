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

namespace GJ.Para.TURNON
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
            cmbMode1.SelectedIndex = 0;
            cmbMode2.SelectedIndex = 0;
            cmbMode3.SelectedIndex = 0;
            cmbTypeC.Items.Add("一组CC信号");
            cmbTypeC.Items.Add("二组CC信号");
        }
        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMode1.SelectedIndex == 0)
            {
                labLoadSet1.Text = "负载设置(A):";
                //labLoadMin.Text = "负载下限(A):";
                //labLoadMax.Text = "负载上限(A):";
            }
            else if (cmbMode1.SelectedIndex == 1)
            {
                labLoadSet1.Text = "负载设置(V):";
                //labLoadMin.Text = "负载下限(A):";
                //labLoadMax.Text = "负载上限(A):";
            }
            else
            {
                labLoadSet1.Text = "负载设置(W):";
                //labLoadMin.Text = "负载下限(W):";
                //labLoadMax.Text = "负载上限(W):";
            }

        }

        private void cmbMode2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMode2.SelectedIndex == 0)
            {
                labLoadSet2.Text = "负载设置(A):";
                //labLoadMin.Text = "负载下限(A):";
                //labLoadMax.Text = "负载上限(A):";
            }
            else if (cmbMode2.SelectedIndex == 1)
            {
                labLoadSet2.Text = "负载设置(V):";
                //labLoadMin.Text = "负载下限(A):";
                //labLoadMax.Text = "负载上限(A):";
            }
            else
            {
                labLoadSet2.Text = "负载设置(W):";
                //labLoadMin.Text = "负载下限(W):";
                //labLoadMax.Text = "负载上限(W):";
            }
        }

        private void cmbMode3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbMode3.SelectedIndex == 0)
            {
                labLoadSet3.Text = "负载设置(A):";
                //labLoadMin.Text = "负载下限(A):";
                //labLoadMax.Text = "负载上限(A):";
            }
            else if (cmbMode3.SelectedIndex == 1)
            {
                labLoadSet3.Text = "负载设置(V):";
                //labLoadMin.Text = "负载下限(A):";
                //labLoadMax.Text = "负载上限(A):";
            }
            else
            {
                labLoadSet3.Text = "负载设置(W):";
                //labLoadMin.Text = "负载下限(W):";
                //labLoadMax.Text = "负载上限(W):";
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
            txtACV.Text = "220";

            txtVname.Text = "+5V";
            txtVmin.Text = "0";
            txtVmax.Text = "0";

            cmbMode1.SelectedIndex = 0;            
            txtVon.Text = "0";

            //labLoadSet.Text = "负载设置(A):";
            //labLoadMin.Text = "负载下限(A):";
            //labLoadMax.Text = "负载上限(A):";  

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
                dlg.Filter = "spec files (*.TurnOn)|*.TurnOn";
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
                txtACV.Text = modelPara.acv.ToString();

                txtVname.Text = modelPara.vName;
                cmbMode1.SelectedIndex = modelPara.loadMode[0];
                cmbMode2.SelectedIndex = modelPara.loadMode[1];
                cmbMode3.SelectedIndex = modelPara.loadMode[2];
                ChkID.Checked = modelPara.ChkID;
                ChkTypeC.Checked = modelPara.ChkTypeC;
                chkHightPower.Checked = modelPara.ChkHightPower;
                chkTwoLoad.Checked = modelPara.ChkTwoLoad;
                cmbTypeC.SelectedIndex = modelPara.TypeCSum;

                txtVmin.Text = modelPara.vMin[0].ToString();
                txtVmax.Text = modelPara.vMax[0].ToString();
                txtVon.Text = modelPara.loadVon[0].ToString();
                txtLoadSet.Text = modelPara.loadSet[0].ToString();
                txtLoadmin.Text = modelPara.loadMin[0].ToString();
                txtLoadmax.Text = modelPara.loadMax[0].ToString();
                TxtIDmin.Text = modelPara.IDmin[0].ToString();
                TxtIDmax.Text = modelPara.IDmax[0].ToString();


                txtVmin1.Text = modelPara.vMin[1].ToString();
                txtVmax1.Text = modelPara.vMax[1].ToString();
                txtVon1.Text = modelPara.loadVon[1].ToString();
                txtLoadSet1.Text = modelPara.loadSet[1].ToString();
                txtLoadmin1.Text = modelPara.loadMin[1].ToString();
                txtLoadmax1.Text = modelPara.loadMax[1].ToString();
                TxtIDmin1.Text = modelPara.IDmin[1].ToString();
                TxtIDmax1.Text = modelPara.IDmax[1].ToString();

                txtVmin2.Text = modelPara.vMin[2].ToString();
                txtVmax2.Text = modelPara.vMax[2].ToString();
                txtVon2.Text = modelPara.loadVon[2].ToString();
                txtLoadSet2.Text = modelPara.loadSet[2].ToString();
                txtLoadmin2.Text = modelPara.loadMin[2].ToString();
                txtLoadmax2.Text = modelPara.loadMax[2].ToString();
                TxtIDmin2.Text = modelPara.IDmin[2].ToString();
                TxtIDmax2.Text = modelPara.IDmax[2].ToString();

                txtPmin1.Text = modelPara.Pmin[0].ToString();
                txtPmin2.Text = modelPara.Pmin[1].ToString();
                txtPmin3.Text = modelPara.Pmin[2].ToString();

                txtPmax1.Text = modelPara.Pmax[0].ToString();
                txtPmax2.Text = modelPara.Pmax[1].ToString();
                txtPmax3.Text = modelPara.Pmax[2].ToString();

                ChkModel1.Checked = modelPara.ChkModel[1];
                ChkModel2.Checked = modelPara.ChkModel[2];
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
                dlg.Filter = "spec files (*.TurnOn)|*.TurnOn";
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
                modelPara.acv = System.Convert.ToInt32(txtACV.Text);

                modelPara.vName = txtVname.Text;
                modelPara.ChkID = ChkID.Checked;
                modelPara.ChkTypeC = ChkTypeC.Checked;
                modelPara.TypeCSum = cmbTypeC.SelectedIndex;
                modelPara.ChkHightPower = chkHightPower.Checked;
                modelPara.ChkTwoLoad = chkTwoLoad.Checked;

                modelPara.loadMode[0] = cmbMode1.SelectedIndex;
                modelPara.loadMode[1] = cmbMode2.SelectedIndex;
                modelPara.loadMode[2] = cmbMode3.SelectedIndex;

                modelPara.vMin[0] = System.Convert.ToDouble(txtVmin.Text);
                modelPara.vMax[0] = System.Convert.ToDouble(txtVmax.Text);
                modelPara.loadVon[0] = System.Convert.ToDouble(txtVon.Text);
                modelPara.loadSet[0] = System.Convert.ToDouble(txtLoadSet.Text);
                modelPara.loadMin[0] = System.Convert.ToDouble(txtLoadmin.Text);
                modelPara.loadMax[0] = System.Convert.ToDouble(txtLoadmax.Text);
                modelPara.IDmin[0] = System.Convert.ToDouble(TxtIDmin.Text);
                modelPara.IDmax[0] = System.Convert.ToDouble(TxtIDmax.Text);

                modelPara.vMin[1] = System.Convert.ToDouble(txtVmin1.Text);
                modelPara.vMax[1] = System.Convert.ToDouble(txtVmax1.Text);
                modelPara.loadVon[1] = System.Convert.ToDouble(txtVon1.Text);
                modelPara.loadSet[1] = System.Convert.ToDouble(txtLoadSet1.Text);
                modelPara.loadMin[1] = System.Convert.ToDouble(txtLoadmin1.Text);
                modelPara.loadMax[1] = System.Convert.ToDouble(txtLoadmax1.Text);
                modelPara.IDmin[1] = System.Convert.ToDouble(TxtIDmin1.Text);
                modelPara.IDmax[1] = System.Convert.ToDouble(TxtIDmax1.Text);

                modelPara.vMin[2] = System.Convert.ToDouble(txtVmin2.Text);
                modelPara.vMax[2] = System.Convert.ToDouble(txtVmax2.Text);
                modelPara.loadVon[2] = System.Convert.ToDouble(txtVon2.Text);
                modelPara.loadSet[2] = System.Convert.ToDouble(txtLoadSet2.Text);
                modelPara.loadMin[2] = System.Convert.ToDouble(txtLoadmin2.Text);
                modelPara.loadMax[2] = System.Convert.ToDouble(txtLoadmax2.Text);
                modelPara.IDmin[2] = System.Convert.ToDouble(TxtIDmin2.Text);
                modelPara.IDmax[2] = System.Convert.ToDouble(TxtIDmax2.Text);

                modelPara.Pmin[0] = System.Convert.ToDouble(txtPmin1.Text);
                modelPara.Pmin[1] = System.Convert.ToDouble(txtPmin2.Text);
                modelPara.Pmin[2] = System.Convert.ToDouble(txtPmin3.Text);

                modelPara.Pmax[0] = System.Convert.ToDouble(txtPmax1.Text);
                modelPara.Pmax[1] = System.Convert.ToDouble(txtPmax2.Text);
                modelPara.Pmax[2] = System.Convert.ToDouble(txtPmax3.Text);

                modelPara.ChkModel[0] = true;
                modelPara.ChkModel[1] = ChkModel1.Checked;
                modelPara.ChkModel[2] = ChkModel2.Checked;
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
