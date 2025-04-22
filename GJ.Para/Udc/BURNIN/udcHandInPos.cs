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
    public partial class udcHandInPos : Form
    {
        public static int C_UUT_POS = 0;

        public udcHandInPos()
        {
            InitializeComponent();
        }

        #region 面板回调函数
        private void btnOK_Click(object sender, EventArgs e)
        {
            int row = System.Convert.ToInt32(txtRow.Text);
            int col = System.Convert.ToInt32(txtCol.Text);
            if (row < 1 || row > 10)
            {
                MessageBox.Show("输入层数范围为1-10");
                return;
            }
            if (col < 1 || col > 16)
            {
                MessageBox.Show("输入层数范围为1-16");
                return;
            }
            if (col % 2 == 0)
                col--;
            C_UUT_POS = (row - 1) * 16 + col; 
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void txtRow_KeyPress(object sender, KeyPressEventArgs e)
        {
            //char-8为退格键
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }
        private void txtCol_KeyPress(object sender, KeyPressEventArgs e)
        {
            //char-8为退格键
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }
        #endregion

    }
}
