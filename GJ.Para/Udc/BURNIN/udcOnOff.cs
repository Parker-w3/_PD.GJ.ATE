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
    public partial class udcOnOff : UserControl
    {

        #region 构造函数
        public udcOnOff(int idNo,bool chkSecUnit=false,int onoffTime=0,int onTime=0,int offTime=0)
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

            this.idNo = idNo;
            chkUnit.Checked = chkSecUnit;
            txtOnOff.Text = onoffTime.ToString();
            if (chkSecUnit)
            {

                txtOn.Text = onTime.ToString();
                txtOff.Text = offTime.ToString();
                labOnUint.Text = "秒";
                labOffUint.Text = "秒";
            }
            else
            {
                txtOn.Text = (onTime / 60).ToString();
                txtOff.Text = (offTime / 60).ToString();
                labOnUint.Text = "分钟";
                labOffUint.Text = "分钟";
            }
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {

        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel1, true, null);
            panel2.GetType().GetProperty("DoubleBuffered",
                                         System.Reflection.BindingFlags.Instance |
                                         System.Reflection.BindingFlags.NonPublic)
                                         .SetValue(panel2, true, null);
        }
        #endregion

        #region 类声明
        
        #endregion

        #region 字段
        private int idNo = 0;
        #endregion

        #region 属性
        public bool m_chkSecUnit
        {
           set {
                chkUnit.Checked = value;
                if (value)
                {
                    labOnUint.Text = "秒";
                    labOffUint.Text = "秒";
                }
                else
                {
                    labOnUint.Text = "分钟";
                    labOffUint.Text = "分钟";
                }
                }
           get { return chkUnit.Checked; }
        }
        public int m_onoffTime
        {
            set { txtOnOff.Text = value.ToString(); }
            get {return System.Convert.ToInt32(txtOnOff.Text);}
        }
        public int m_onTime
        {
            set {
                if (chkUnit.Checked)
                    txtOn.Text = value.ToString();
                else
                    txtOn.Text = (value / 60).ToString();   
                }
            get {
                if (chkUnit.Checked)
                    return System.Convert.ToInt32(txtOn.Text);
                else
                    return System.Convert.ToInt32(txtOn.Text)*60;
                 }
        }
        public int m_offTime
        {
            set
            {
                if (chkUnit.Checked)
                    txtOff.Text = value.ToString();
                else
                    txtOff.Text = (value / 60).ToString();
            }
            get
            {
                if (chkUnit.Checked)
                    return System.Convert.ToInt32(txtOff.Text);
                else
                    return System.Convert.ToInt32(txtOff.Text) * 60;
            }
        }
        public int m_QCVolt
        {
            set {
                if (value == 0)
                    cmbVType.Text = "0:+5V";
                else if (value == 1)
                    cmbVType.Text = "1:+7V";
                else if (value == 2)
                    cmbVType.Text = "2:+9V";
                else if (value == 3)
                    cmbVType.Text = "3:+12V";
                else if (value == 4)
                    cmbVType.Text = "4:+20V";
                }
            get { return cmbVType.SelectedIndex; }
        }
        #endregion

        #region 面板回调函数
        private void udcOnOff_Load(object sender, EventArgs e)
        {

            cmbVType.Items.Clear();
            cmbVType.Items.Add("0:+5V");
            cmbVType.Items.Add("1:+7V");
            cmbVType.Items.Add("2:+9V");
            cmbVType.Items.Add("3:+12V");
            cmbVType.Items.Add("4:+20V");
            cmbVType.SelectedIndex = 0; 
 
            chkUnit.Text = "ONOFF"+ (idNo+1).ToString()+"----单位:秒";
            labOnOff.Text = "ONOFF" + (idNo + 1).ToString() + "循环:";  
            labOn.Text = "ON" + (idNo + 1).ToString() + "时间:";
            labOff.Text = "OFF" + (idNo + 1).ToString() + "时间:";

            txtOnOff.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtOn.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
            txtOff.KeyPress += new KeyPressEventHandler(OnTextKeyPressIsNumber);
  
        }
        private void chkUnit_Click(object sender, EventArgs e)
        {
            if (chkUnit.Checked)
            {
                labOnUint.Text = "秒";
                labOffUint.Text = "秒";                
            }
            else
            {
                labOnUint.Text = "分钟";
                labOffUint.Text = "分钟"; 
            }  
        }
        private void OnTextKeyPressIsNumber(object sender, KeyPressEventArgs e)
        {
            //char-8为退格键
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)'.')
                e.Handled = true;
        }
        #endregion

    }
}
