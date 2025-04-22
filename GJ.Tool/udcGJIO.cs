using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Dev.IO; 
namespace GJ.Tool
{
    public partial class udcGJIO : UserControl
    {
        #region 构造函数
        public udcGJIO()
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
            picX = new PictureBox[] { 
                                     picX1, picX2, picX3, picX4, picX5, picX6, picX7, picX8
                                     };
            picK = new PictureBox[]{ 
                                    picK1,picK2,picK3,picK4,picK5,picK6,picK7,picK8,
                                    picK9,picK10,picK11,picK12,picK13,picK14
                                    }; 
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

        #region 字段
        private CIOCom comMon = null;
        #endregion

        #region 面板控件
        private PictureBox[] picX = null;
        private PictureBox[] picK = null;
        #endregion

        #region 面板回调函数
        private void udcGJIO_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < picX.Length; i++)
                picX[i].Image = ImageList1.Images["OFF"];
            for (int i = 0; i < picK.Length; i++)
            {
                picK[i].Image = ImageList1.Images["OFF"];
                picK[i].Tag = "OFF";
                picK[i].DoubleClick += new EventHandler(picKDoubleClick); 
            }
                
            string[] com = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < com.Length; i++)
                cmbCOM.Items.Add(com[i]);
            if (com.Length > 0)
                cmbCOM.Text = com[0];
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (cmbCOM.Text == "")
            {
                labStatus.Text = "请输入串口编号";
                labStatus.ForeColor = Color.Red;
                return;
            }
            string er = string.Empty;
            if (comMon == null)
            {
                comMon = new CIOCom();
                if (!comMon.open(cmbCOM.Text, ref er))
                {
                    labStatus.Text = er;
                    labStatus.ForeColor = Color.Red;
                    comMon = null;
                    return;
                }
                btnOpen.Text = "关闭";
                labStatus.Text = "成功打开串口.";
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                comMon.close();
                comMon = null;
                btnOpen.Text = "打开";
                labStatus.Text = "关闭串口.";
                labStatus.ForeColor = Color.Blue;
            }
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (comMon == null)
            {
                labStatus.Text = "请确定串口是否打开?";
                labStatus.ForeColor = Color.Red;
                return;
            }
            if (txtAddr.Text == "")
            {
                labStatus.Text = "请输入模块地址";
                labStatus.ForeColor = Color.Red;
                return;
            }
            string er = string.Empty;

            int wAddr = System.Convert.ToInt32(txtAddr.Text);

            List<EX> X = new List<EX>();

            if (!comMon.ReadInSingal(wAddr, ref X, ref er))
            {
                labStatus.Text = "读取IO板X1-X8信号失败:" + er;
                labStatus.ForeColor = Color.Red;
                return;
            }
            for (int i = 0; i < X.Count; i++)
            {
                if(X[i]==EX.XON)
                    picX[i].Image=ImageList1.Images["ON"];
                else
                    picX[i].Image = ImageList1.Images["OFF"];
            }
            labStatus.Text = "读取IO板X1-X8信号成功";
            labStatus.ForeColor = Color.Blue;
        }
        private void btnOff_Click(object sender, EventArgs e)
        {
            if (comMon == null)
            {
                labStatus.Text = "请确定串口是否打开?";
                labStatus.ForeColor = Color.Red;
                return;
            }
            if (txtAddr.Text == "")
            {
                labStatus.Text = "请输入模块地址";
                labStatus.ForeColor = Color.Red;
                return;
            }
            string er = string.Empty;

            int wAddr = System.Convert.ToInt32(txtAddr.Text);

            if (!comMon.CtrlRelayByCmd1(wAddr, 0, ref er))
            {
                labStatus.Text = "设置IO板K1-K14信号OFF失败:" + er;
                labStatus.ForeColor = Color.Red;
                return;
            }
            for (int i = 0; i < picK.Length; i++)
            {
                picK[i].Image = ImageList1.Images["OFF"];
                picK[i].Tag = "OFF";
            }
            labStatus.Text = "设置IO板K1-K14信号OFF成功.";
            labStatus.ForeColor = Color.Blue;

        }
        private void picKDoubleClick(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            int idNo = System.Convert.ToInt16(pic.Name.Substring(4, pic.Name.Length - 4));

            if (comMon == null)
            {
                labStatus.Text = "请确定串口是否打开?";
                labStatus.ForeColor = Color.Red;
                return;
            }
            if (txtAddr.Text == "")
            {
                labStatus.Text = "请输入模块地址";
                labStatus.ForeColor = Color.Red;
                return;
            }
            string er = string.Empty;

            int wAddr = System.Convert.ToInt32(txtAddr.Text);

            EY onoff=EY.YOFF; 

            if (pic.Tag.ToString() == "OFF")
               onoff=EY.YON;

            if (!comMon.CtrlRelayByCmd2(wAddr, idNo, onoff, ref er))
            {
                labStatus.Text = "设置IO板K1-K14信号失败:" + er;
                labStatus.ForeColor = Color.Red;
                return;
            }
            if (onoff == EY.YON)
            {
                pic.Tag = "ON";
                pic.Image = ImageList1.Images["ON"]; 
            }
            else
            {
                pic.Tag = "OFF";
                pic.Image = ImageList1.Images["OFF"];
            }
            labStatus.Text = "设置IO板K1-K14信号成功.";
            labStatus.ForeColor = Color.Blue;
        }
        #endregion




      
    }
}
