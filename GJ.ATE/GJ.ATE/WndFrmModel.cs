using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ;
using GJ.Para;
using GJ.Para.Base; 

namespace GJ.ATE
{
    public partial class WndFrmModel : Form
    {
        #region 构造函数
        public WndFrmModel()
        {
            InitializeComponent();
        }
        #endregion

        #region 字段
        private udcModelBase udcModel = null; 
        #endregion

        #region 初始化
        private void load()
        {
            if (CGlobal.CFlow.idNo > 0)
            {
                if (udcModel != null)
                {
                    OnModelBtnClick.OnEvent -= new COnEvent<CModelBtnArgs>.OnEventHandler(udcModel.OnModelBtnClick);
                    udcModel.OnModelRepose.OnEvent -= new COnEvent<CModelReposeArgs>.OnEventHandler(OnModelRepose);
                    udcModel.Dispose();
                    udcModel = null;
                }
                switch (CGlobal.CFlow.flowGUID)
                {
                    case "LOADUP":
                        udcModel = new GJ.Para.LOADUP.udcModelPara();  
                        break;
                    case "BURNIN":
                        udcModel = new GJ.Para.BURNIN.udcModelPara();    
                        break;
                    case "HIPOT":
                        udcModel = new GJ.Para.HIPOT.udcModelPara(); 
                        break;
                    case "TURNON":
                         udcModel = new GJ.Para.TURNON .udcModelPara(); 
                        break;

                    case "ATE":
                        this.Close();
                        break;
                    case "UNLOAD":
                        udcModel = new GJ.Para.UNLOAD .udcModelPara(); 
                       // this.Close();
                        break;
                    default:
                        break;
                }
                if (udcModel != null)
                {
                    OnModelBtnClick.OnEvent += new COnEvent<CModelBtnArgs>.OnEventHandler(udcModel.OnModelBtnClick);
                    udcModel.OnModelRepose.OnEvent += new COnEvent<CModelReposeArgs>.OnEventHandler(OnModelRepose);
                    udcModel.Dock = DockStyle.Fill;
                    this.panelMain.Controls.Add(udcModel);                      
                }                 
            }             
        }
        private void WndFrmModel_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0; 
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            load();
            
        }
        #endregion

        #region 事件定义
        public COnEvent<CModelBtnArgs> OnModelBtnClick = new COnEvent<CModelBtnArgs>(); 
        #endregion

        #region 事件触发
        private void btnNew_Click(object sender, EventArgs e)
        {
            OnModelBtnClick.OnEvented(new CModelBtnArgs(CModelBtnArgs.EBtnName.新建));  
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OnModelBtnClick.OnEvented(new CModelBtnArgs(CModelBtnArgs.EBtnName.打开));  
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            OnModelBtnClick.OnEvented(new CModelBtnArgs(CModelBtnArgs.EBtnName.保存));  
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            OnModelBtnClick.OnEvented(new CModelBtnArgs(CModelBtnArgs.EBtnName.退出));  
        }
        #endregion

        #region 消息响应
        private void OnModelRepose(object sender, CModelReposeArgs e)
        {
            switch (e.ReposeNo)
            {
                case CModelReposeArgs.ERepose.新建:
                    break;
                case CModelReposeArgs.ERepose.打开:
                    break;
                case CModelReposeArgs.ERepose.保存:                    
                    break;                
                case CModelReposeArgs.ERepose.退出:
                    this.Close(); 
                    break;
                default:
                    break;
            }
        }
        #endregion


    }
}
