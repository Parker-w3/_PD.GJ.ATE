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
    public partial class udcModelBase : UserControl
    {
        public udcModelBase()
        {
            InitializeComponent();
        }

        #region 面板状态消息
        public COnEvent<CModelReposeArgs> OnModelRepose = new COnEvent<CModelReposeArgs>();
        #endregion

        #region 面板按钮事件
        public virtual void OnModelBtnClick(object sender, CModelBtnArgs e)
        {
            switch (e.BtnNo)
            {
                case CModelBtnArgs.EBtnName.新建:
                    break;
                case CModelBtnArgs.EBtnName.打开:
                    break;
                case CModelBtnArgs.EBtnName.保存:
                    break;
                case CModelBtnArgs.EBtnName.退出:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
