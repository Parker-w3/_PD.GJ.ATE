using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.Para.Udc.BURNIN
{
    public partial class udcClrDealTime : Form
    {
        public udcClrDealTime()
        {
            InitializeComponent();
        }

        #region 面板回调函数
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPassWord.Text.ToUpper() != "BURNIN")
            {
                MessageBox.Show("输入解除密码错误,请重新输入", "解除治具", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        
    }
}
