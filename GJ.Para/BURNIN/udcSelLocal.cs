using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.Para.BURNIN
{
    public partial class udcSelLocal : Form
    {
        public udcSelLocal()
        {
            InitializeComponent();
        }

        public static int row = 0;
        public static int col = 0;

        private void btnOK_Click(object sender, EventArgs e)
        {
            row = System.Convert.ToInt16(txtRow.Text);
            col = System.Convert.ToInt16(txtCol.Text);
            this.DialogResult = DialogResult.OK;
            this.Close(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
