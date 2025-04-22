using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.Para.Udc.BURNIN
{
    public partial class udcIdSlot : UserControl
    {
        #region 构造函数
        public udcIdSlot(int idNo)
        {
            InitializeComponent();

            panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel1, true, null);
            labId1.Text = idNo.ToString("D2");
            labId2.Text = (idNo+1).ToString("D2");
        }
        #endregion

    }
}
