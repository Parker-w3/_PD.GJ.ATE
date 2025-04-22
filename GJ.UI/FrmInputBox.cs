using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.UI
{
    public partial class FrmInputBox : Form
    {
        public FrmInputBox()
        {
            InitializeComponent();
        }

        private void FrmInputBox_Load(object sender, EventArgs e)
        {
            txtPassword.Text = "";
        }
 
        private string mPassword = "";
        private bool mFailWait = false;
        public string wPassword
        {
            set
            {
                mPassword = value;
            }
        }

        public bool rFailWait
        {
            set
            {
                mFailWait = value;
            }
            get
            {
                return mFailWait;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != mPassword)
            {
                MessageBox.Show("输入密码错误!", "输入提示", MessageBoxButtons.OK);
            }
            else
            {
                mFailWait = true;
                this.Close();
            }
        }

    
   
    }
}
