using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.Para.Base
{
    public partial class udcMainBase : UserControl
    {
        #region 构造函数
        public udcMainBase()
        {
            InitializeComponent();
        }
        #endregion
        
        #region 面板状态事件
        public COnEvent<CFormRunArgs> OnFrmMainStatus = new COnEvent<CFormRunArgs>();
        #endregion

        #region 面板按钮事件
        public virtual void OnFrmMainBtnClick(object sender, CFormBtnArgs e)
        {
            switch (e.btnName)
            {
                case CFormBtnArgs.EBtnName.启动:
                    break;
                case CFormBtnArgs.EBtnName.暂停:
                    break;
                case CFormBtnArgs.EBtnName.退出:
                    Application.Exit();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
