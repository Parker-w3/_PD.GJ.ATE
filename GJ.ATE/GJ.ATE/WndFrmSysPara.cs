using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Para;
using GJ.Para.Base;
using System.IO;
using System.Reflection;
 
namespace GJ.ATE
{
    public partial class WndFrmSysPara : Form
    {
        #region 构造函数
        public WndFrmSysPara()
        {
            InitializeComponent();
        }
        #endregion

        #region 加载测试工位
        private void load()
        {
            if (CGlobal.CFlow.idNo > 0)
            {
                if (udcSys != null)
                {
                    udcSys.OnSysSave.OnEvent -= new COnEvent<CSysSaveArgs>.OnEventHandler(OnSave);
                    udcSys.OnSysCancel.OnEvent -= new COnEvent<CSysCancelArgs>.OnEventHandler(OnCancel);
                    udcSys.Dispose();
                    udcSys = null;
                }

                //加载动态库
                string dllFile = Application.StartupPath + "\\GJ.Para.dll";

                if (!File.Exists(dllFile))
                {
                    MessageBox.Show("找不到加载动态库[" + dllFile + "]");
                    return;
                }
                //得到当前动态库的程序集
                Assembly asb = Assembly.LoadFile(dllFile);

                //创建实例
                string ctrlName = "GJ.Para." + CGlobal.CFlow.flowGUID + ".udcSysPara";
                object obj = asb.CreateInstance(ctrlName, true);

                if (obj != null)
                {
                    udcSys = (GJ.Para.Base.udcSysBase)obj;
                    udcSys.OnSysSave.OnEvent += new COnEvent<CSysSaveArgs>.OnEventHandler(OnSave);
                    udcSys.OnSysCancel.OnEvent += new COnEvent<CSysCancelArgs>.OnEventHandler(OnCancel);
                    udcSys.Dock = DockStyle.Fill;
                    this.Controls.Add(udcSys); 
                
                }
            }             
        }
        #endregion

        #region 字段
        private udcSysBase udcSys= null;
        #endregion

        #region 面板回调函数
        private void WndFrmSysPara_Load(object sender, EventArgs e)
        {
            load();
        }
        private void OnSave(object sender, CSysSaveArgs e)
        {
            MessageBox.Show("系统参数保存成功", "系统参数", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        private void OnCancel(object sender, CSysCancelArgs e)
        {
            this.Close();
        }
        #endregion
 
    }
}
