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
    public partial class udcSysBase : UserControl
    {
        #region 构造函数
        public udcSysBase()
        {
            InitializeComponent();
        }
        #endregion

        #region 定义事件
        public COnEvent<CSysSaveArgs> OnSysSave= new COnEvent<CSysSaveArgs>();
        public COnEvent<CSysCancelArgs> OnSysCancel = new COnEvent<CSysCancelArgs>(); 
        #endregion

    }
}
