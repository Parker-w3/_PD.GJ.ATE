using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.Para.Udc.HIPOT
{
    public partial class udcHPChan : UserControl
    {
        #region 构造函数
        public udcHPChan()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            labSlot = new Label[]{
                                  labSlot1,labSlot2,labSlot3,labSlot4,labSlot5,labSlot6,labSlot7,labSlot8,
                                  labSlot9,labSlot10,labSlot11,labSlot12,labSlot13,labSlot14,labSlot15,labSlot16
                                  };
            cmbHpChan = new ComboBox[]{
                                      cmbHpChan1,cmbHpChan2,cmbHpChan3,cmbHpChan4,cmbHpChan5,cmbHpChan6,cmbHpChan7,cmbHpChan8,
                                      cmbHpChan9,cmbHpChan10,cmbHpChan11,cmbHpChan12,cmbHpChan13,cmbHpChan14,cmbHpChan15,cmbHpChan16
                                     };
            cmbIoChan = new ComboBox[]{
                                      cmbIOChan1,cmbIOChan2,cmbIOChan3,cmbIOChan4,cmbIOChan5,cmbIOChan6,cmbIOChan7,cmbIOChan8,
                                      cmbIOChan9,cmbIOChan10,cmbIOChan11,cmbIOChan12,cmbIOChan13,cmbIOChan14,cmbIOChan15,cmbIOChan16
                                      };
            for (int i = 0; i < 16; i++)
            {
                cmbHpChan[i].SelectedIndexChanged += new EventHandler(OnHpChanTextChange);
                cmbIoChan[i].SelectedIndexChanged += new EventHandler(OnIoChanTextChange); 
            }
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
        }
        #endregion

        #region 面板控件
        private Label[] labSlot = null;
        private ComboBox[] cmbHpChan = null;
        private ComboBox[] cmbIoChan = null;
        #endregion

        #region 字段
        private List<int> uutHpChan = new List<int>();
        private  List<int> uutIoChan = new List<int>();        
        #endregion

        #region 属性
        public List<int> mHpChan
        {
            set
            {
                uutHpChan = value;
                for (int i = 0; i < uutHpChan.Count; i++)
                    cmbHpChan[i].Text = uutHpChan[i].ToString();
                if (uutHpChan.Count != 16)
                    cmbUUTNum.SelectedIndex = 1;
                else
                    cmbUUTNum.SelectedIndex = 0;
            }
            get
            {
                return uutHpChan;
            }
        }
        public List<int> mIoChan
        {
            set {  
                  uutIoChan = value;
                  for (int i = 0; i < uutIoChan.Count; i++)                  
                      cmbIoChan[i].Text = uutIoChan[i].ToString();                  
                  if (uutIoChan.Count != 16)
                      cmbUUTNum.SelectedIndex = 1;
                  else
                      cmbUUTNum.SelectedIndex = 0; 
                }
            get {
                 return uutIoChan; 
                 }
        }
        #endregion

        #region 面板回调函数
        private void udcHPChan_Load(object sender, EventArgs e)
        {
            try
            {
                cmbUUTNum.Items.Clear();
                cmbUUTNum.Items.Add(16);
                cmbUUTNum.Items.Add(8);
                cmbUUTNum.SelectedIndex = 0;

                for (int i = 0; i < 16; i++)
                {
                    cmbHpChan[i].Items.Clear();
                    for (int j = 0; j < 10; j++)
                        cmbHpChan[i].Items.Add(j + 1);
                    if (i < 8)
                        cmbHpChan[i].Text = (i + 1).ToString();
                    else
                        cmbHpChan[i].Text = (i - 7).ToString();
                    cmbHpChan[i].DropDownStyle = ComboBoxStyle.DropDownList;    
                }
                for (int i = 0; i < 16; i++)
                {
                    cmbIoChan[i].Items.Clear();
                    for (int j = 0; j < 8; j++)
                        cmbIoChan[i].Items.Add(j + 1);
                    if (i < 8)
                        cmbIoChan[i].Text = (i + 1).ToString();
                    else
                        cmbIoChan[i].Text = (i - 7).ToString();
                    cmbIoChan[i].DropDownStyle = ComboBoxStyle.DropDownList; 
                }
            }
            catch (Exception)
            {                
                throw;
            }
          
        }
        private void cmbUUTNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            int uutNum = System.Convert.ToInt16(cmbUUTNum.Text);
            for (int i = uutIoChan.Count; i < uutNum; i++)
            {
                uutHpChan.Add(0);
                uutIoChan.Add(0);
            }
            for (int i = uutNum; i < uutIoChan.Count; )
            {
                uutHpChan.RemoveAt(uutNum);
                uutIoChan.RemoveAt(uutNum); 
            }                
            for (int i = 0; i < uutNum; i++)
            {
                labSlot[i].Visible = true;
                cmbHpChan[i].Visible = true;
                cmbHpChan[i].Text = uutHpChan[i].ToString(); 
                cmbIoChan[i].Visible = true;
                cmbIoChan[i].Text = uutIoChan[i].ToString(); 
            }
            for (int i = uutNum; i < labSlot.Length; i++)
            {
                labSlot[i].Visible = false;
                cmbHpChan[i].Visible = false;
                cmbIoChan[i].Visible = false;
            }
            if(uutNum==16)
            {
                labNo3.Visible = true;
                labNo4.Visible = true;
                labId3.Visible = true;
                labId4.Visible = true;
                labHpId3.Visible = true;
                labHpId4.Visible = true; 
             }
             else
            {
                labNo3.Visible = false;
                labNo4.Visible = false;
                labId3.Visible = false;
                labId4.Visible = false;
                labHpId3.Visible = false;
                labHpId4.Visible = false; 
            }               
        }
        private void OnHpChanTextChange(object sender, EventArgs e)
        {
            ComboBox txtBox = (ComboBox)sender;
            int idNo = System.Convert.ToInt16(txtBox.Name.Substring(9, txtBox.Name.Length - 9));
            uutHpChan[idNo - 1] = System.Convert.ToInt16(txtBox.Text);
        }
        private void OnIoChanTextChange(object sender, EventArgs e)
        {
            ComboBox txtBox = (ComboBox)sender;
            int idNo =System.Convert.ToInt16(txtBox.Name.Substring(9, txtBox.Name.Length - 9));
            uutIoChan[idNo - 1] = System.Convert.ToInt16(txtBox.Text);   
        }
        #endregion

        #region 方法
        

        #endregion

    }
}
