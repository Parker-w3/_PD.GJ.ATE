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
using GJ.Para.Udc.HIPOT;
using GJ.Dev.HIPOT;
  
namespace GJ.Para.HIPOT
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
            hpChan = new udcHPChan();
            hpChan.Dock = DockStyle.Fill;
            panel1.Controls.Add(hpChan, 0, 3);

            hpPara = new udcHPPara();
            hpPara.Dock = DockStyle.Fill;
            panel3.Controls.Add(hpPara, 0, 1);
            hpPara.OnStepChange.OnEvent += new COnEvent<udcHPPara.CStepChangeArgs>.OnEventHandler(OnStepValChange); 
            hpPara.setItem(CHPSetting.iniStep(CHPSetting.EStepName.AC,0));

            c_HPStepName = new string[] {"交流电压耐压(AC)测试", "直流电压耐压(DC)测试", 
                                         "绝缘阻抗(IR)测试", "开短路侦测(OS)测试", "接地阻抗(GC)测试"};

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
        }
        #endregion

        #region 面板回调函数
        private void udcModelPara_Load(object sender, EventArgs e)
        {
            listSource.Items.Clear();

            for (int i = 0; i < c_HPStepName.Length; i++)
                listSource.Items.Add(c_HPStepName[i]);  

            clr();
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
                    InitListTarget();
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
        private udcHPChan hpChan = null;
        private udcHPPara hpPara = null;
        #endregion

        #region 面板拖拉
        private bool sourceDrag = false;
        /// <summary>
        /// 调用拖和放使用DoDragDrop方法-->在MouseDown事件中实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listSource_MouseDown(object sender, MouseEventArgs e)
        {
            if (listSource.SelectedIndex < 0)
                return;
            sourceDrag = true;
            if (e.Clicks == 2) //双击鼠标添加测试项目
            {
                step.Add(CHPSetting.iniStep((CHPSetting.EStepName)(listSource.SelectedIndex), listTarget.Items.Count));
                listTarget.Items.Add(listSource.Items[listSource.SelectedIndex]);   
            }
            else
            {
                if (e.Button == MouseButtons.Left) //鼠标按下左键
                {                    
                    DragDropEffects dragDropResult = listSource.DoDragDrop(listSource.Items[listSource.SelectedIndex],
                                                     DragDropEffects.Move | DragDropEffects.Copy);   
                }
            }
        }
        /// <summary>
        /// 当鼠标移动到接收容器的上方会触发DragEnter消息,DragEventArg包含Effect和KeyStatus属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">>以表示鼠标移动或复制;KeyStatus:监测Ctrl,Alt,Shift,按Ctrl表示复制</param>
        private void listTarget_DragEnter(object sender, DragEventArgs e)
        {
            if (e.KeyState == 9) //Ctrl键
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;          
        }
        /// <summary>
        /// 当鼠标松开时触发DrapDrop消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listTarget_DragDrop(object sender, DragEventArgs e)
        {
            if (sourceDrag)
            {
                if (listTarget.SelectedIndex < 0)
                {
                    step.Add(CHPSetting.iniStep((CHPSetting.EStepName)(listSource.SelectedIndex), listTarget.Items.Count));
                    listTarget.Items.Add(e.Data.GetData(DataFormats.Text));
                }
                else
                {
                    step.Insert(listTarget.SelectedIndex, CHPSetting.iniStep((CHPSetting.EStepName)(listSource.SelectedIndex),
                                listTarget.SelectedIndex));
                    listTarget.Items.Insert(listTarget.SelectedIndex, e.Data.GetData(DataFormats.Text));
                }
            }            
        }
        private void listTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTarget.SelectedIndex >= 0 && listTarget.SelectedIndex < step.Count)
            {
                step[listTarget.SelectedIndex].stepNo = listTarget.SelectedIndex;
                hpPara.setItem(step[listTarget.SelectedIndex]);
            }                  
        }
        /// <summary>
        /// 调用拖和放使用DoDragDrop方法-->在MouseDown事件中实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listTarget_MouseDown(object sender, MouseEventArgs e)
        {
            if (listTarget.SelectedIndex < 0)
                return;
            sourceDrag = false; 
            if (e.Button == MouseButtons.Left) //鼠标按下左键
            {
                if (listTarget.SelectedIndex >= 0 && listTarget.SelectedIndex < step.Count)
                {
                    step[listTarget.SelectedIndex].stepNo = listTarget.SelectedIndex;
                    hpPara.setItem(step[listTarget.SelectedIndex]);
                }            
                DragDropEffects dragDropResult = listTarget.DoDragDrop(listTarget.Items[listTarget.SelectedIndex],
                                                    DragDropEffects.Move | DragDropEffects.Copy);
            }          
        }
        /// <summary>
        /// 当鼠标移动到接收容器的上方会触发DragEnter消息,DragEventArg包含Effect和KeyStatus属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listSource_DragEnter(object sender, DragEventArgs e)
        {
            if (e.KeyState == 9) //Ctrl键
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;            
        }
        /// <summary>
        /// 当鼠标松开时触发DrapDrop消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listSource_DragDrop(object sender, DragEventArgs e)
        {
            if (!sourceDrag)
            {
                if (listTarget.SelectedIndex < 0)
                    return;
                step.RemoveAt(listTarget.SelectedIndex);
                listTarget.Items.RemoveAt(listTarget.SelectedIndex);
                for (int i = 0; i < step.Count; i++)
                    step[i].stepNo = i;         
            }               
        }
        #endregion

        #region 面板事件响应
        private void OnStepValChange(object sender, udcHPPara.CStepChangeArgs e)
        {
            if (e.stepNo < step.Count)
                step[e.stepNo].para[e.itemNo].setVal = e.itemVal;  
        }
        #endregion

        #region 字段
        private string[] c_HPStepName = null;
        private List<CHPSetting.CStep> step = new List<CHPSetting.CStep>(); 
        #endregion

        #region 方法
        private void InitListTarget()
        {
            listTarget.Items.Clear();
            for (int i = 0; i < step.Count; i++)
                listTarget.Items.Add(step[i].des);
        }
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
                dlg.Filter = "hp files (*.hp)|*.hp";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;                
                modelPath = dlg.FileName;

                dlg = null;

                CModelPara modelPara = new CModelPara();

                CModelSet<CModelPara>.load(modelPath, ref modelPara);

                txtModel.Text = modelPara.model;
                txtCustom.Text = modelPara.custom;
                txtVersion.Text = modelPara.version;

                txtReleaseName.Text = modelPara.releaseName;
                pickerDate.Value = System.Convert.ToDateTime(modelPara.releaseDate);

                hpChan.mHpChan = modelPara.uutHpCH;
                hpChan.mIoChan = modelPara.uutIoCH;

                step = modelPara.step;

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
                dlg.Filter = "hp files (*.hp)|*.hp";
                dlg.FileName = txtModel.Text;
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                modelPath = dlg.FileName;

                dlg = null;

                CModelPara modelPara = new CModelPara();

                modelPara.model = txtModel.Text;
                modelPara.custom = txtCustom.Text;
                modelPara.version = txtVersion.Text;

                modelPara.releaseName = txtReleaseName.Text;
                modelPara.releaseDate = pickerDate.Value.Date.ToString();

                modelPara.uutHpCH = hpChan.mHpChan;
                modelPara.uutIoCH = hpChan.mIoChan;

                modelPara.step = step;
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
