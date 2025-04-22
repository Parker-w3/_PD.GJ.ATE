using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Dev.Card;     

namespace GJ.Tool
{
    public partial class udcIDCard : UserControl
    {
        public udcIDCard()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();
        }

        #region 初始化
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
                                            .SetValue(panel1, true, null);
        }
        #endregion

        #region 字段
        private CCardCom comMon = null;
        #endregion

        #region 面板回调函数
        private void udcIDCard_Load(object sender, EventArgs e)
        {
            string[] com = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < com.Length; i++)
                cmbCOM.Items.Add(com[i]);
            if (com.Length > 0)
                cmbCOM.Text = com[0];
            cmbVer.Items.Clear();
            cmbVer.Items.Add("旧版本A");
            cmbVer.Items.Add("新版本B");
            cmbVer.SelectedIndex = 1; 
            cmdMode.Items.Clear();
            cmdMode.Items.Add("模式A");
            cmdMode.Items.Add("模式B");
            cmdMode.Items.Add("模式C");
            cmdMode.SelectedIndex = 0; 
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
                comMon = new CCardCom();
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
        private void cmbVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comMon == null)
            {
                labStatus.Text = "请确定串口是否打开?";
                labStatus.ForeColor = Color.Red;
                return;
            }
            if (cmbVer.SelectedIndex == 0)
                comMon.mVersion = "A";
            else
                comMon.mVersion = "B";  
        }
        private void btnRAddr_Click(object sender, EventArgs e)
        {
            if (comMon == null)
            {
                labStatus.Text = "请确定串口是否打开?";
                labStatus.ForeColor = Color.Red;
                return;
            }
            if(txtSn.Text=="")
            {
              labStatus.Text="请输入模块序列号";
              labStatus.ForeColor = Color.Red;
              return;
            }

            string er = string.Empty;

            int idAddr=0;

            if(comMon.GetRecorderID(txtSn.Text,ref idAddr,ref er))
            {
                txtAddr.Text = idAddr.ToString();    
                labStatus.Text = "读取模块地址成功";
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                txtAddr.Text = "0";
                labStatus.Text = "读取模块地址失败:"+er;
                labStatus.ForeColor = Color.Red;
            }
        }

        private void btnSAddr_Click(object sender, EventArgs e)
        {
            if (comMon == null)
            {
                labStatus.Text = "请确定串口是否打开?";
                labStatus.ForeColor = Color.Red;
                return;
            }
            if (txtSn.Text == "")
            {
                labStatus.Text = "请输入模块序列号";
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

            int idAddr = System.Convert.ToInt32(txtAddr.Text);

            if (comMon.SetRecorderID(txtSn.Text, idAddr, ref er))
            {
                labStatus.Text = "设置模块地址成功";
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                txtAddr.Text = "0";
                labStatus.Text = "设置模块地址失败:" + er;
                labStatus.ForeColor = Color.Red;
            }
        }

        private void btnRSn_Click(object sender, EventArgs e)
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

            int idAddr = System.Convert.ToInt32(txtAddr.Text);

            string Sn=string.Empty;

            if (comMon.GetRecorderSn(idAddr, ref Sn, ref er))
            {
                txtSn.Text = Sn; 
                labStatus.Text = "读取模块序列号成功";
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                labStatus.Text = "读取模块序列号失败:" + er;
                labStatus.ForeColor = Color.Red;
            }
        }

        private void btnRMode_Click(object sender, EventArgs e)
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

            int idAddr = System.Convert.ToInt32(txtAddr.Text);

            EMode mode=EMode.A;

            if (comMon.GetRecordMode(idAddr, ref mode, ref er))
            {
                cmdMode.SelectedIndex = (int)mode;   
                labStatus.Text = "读取模块工作模式成功";
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                labStatus.Text = "读取模块工作模式失败:" + er;
                labStatus.ForeColor = Color.Red;
            }
        }

        private void btnSMode_Click(object sender, EventArgs e)
        {
            if (comMon == null)
            {
                labStatus.Text = "请确定串口是否打开?";
                labStatus.ForeColor = Color.Red;
                return;
            }
            if (!chkBoard.Checked && txtAddr.Text == "")
            {
                labStatus.Text = "请输入模块地址";
                labStatus.ForeColor = Color.Red;
                return;
            }
            string er = string.Empty;

            int idAddr = System.Convert.ToInt32(txtAddr.Text);

            EMode mode=(EMode)cmdMode.SelectedIndex;  

            if (chkBoard.Checked)
            {
                if (comMon.SetRecorderWorkMode(mode, ref er))
                {
                    labStatus.Text = "广播设置模块工作模式成功";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "广播设置模块工作模式失败:" + er;
                    labStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                if (comMon.SetRecorderWorkMode(idAddr,mode, ref er))
                {
                    labStatus.Text = "设置模块工作模式成功";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "设置模块工作模式失败:" + er;
                    labStatus.ForeColor = Color.Red;
                }
            }
        }

        private void btnRIdCard_Click(object sender, EventArgs e)
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

            int idAddr = System.Convert.ToInt32(txtAddr.Text);

            string idCard=string.Empty;

            if (comMon.GetRecord(idAddr, ref idCard, ref er))
            {
                txtIdCard.Text = idCard;  
                labStatus.Text = "读取模块卡片资料成功";
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                labStatus.Text = "读取模块卡片资料失败:" + er;
                labStatus.ForeColor = Color.Red;
            }
        }

        private void btnRAIdCard_Click(object sender, EventArgs e)
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

            int idAddr = System.Convert.ToInt32(txtAddr.Text);

            string idCard = string.Empty;

            if (comMon.GetRecordAgain(idAddr, ref idCard, ref er))
            {
                txtIdCard.Text = idCard;
                labStatus.Text = "重读模块卡片资料成功";
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                labStatus.Text = "重读模块卡片资料失败:" + er;
                labStatus.ForeColor = Color.Red;
            }
        }

        private void btnRIdTrigger_Click(object sender, EventArgs e)
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

            int idAddr = System.Convert.ToInt32(txtAddr.Text);

            string idCard = string.Empty;

            bool rTrigger = false; 

            if (comMon.GetRecordTriggerSignal(idAddr, ref idCard,ref rTrigger,ref er))
            {
                txtIdCard.Text = idCard;
                if (rTrigger)
                    labTrigger.ImageKey = "H";
                else
                    labTrigger.ImageKey = "L";  
                labStatus.Text = "读取模块卡片资料成功";
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                labStatus.Text = "读取模块卡片资料失败:" + er;
                labStatus.ForeColor = Color.Red;
            }
        }

        private void btnRTrigger_Click(object sender, EventArgs e)
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

            int idAddr = System.Convert.ToInt32(txtAddr.Text);

            bool rTrigger = false;

            if (comMon.GetTriggerSignal(idAddr, ref rTrigger, ref er))
            {
                if (rTrigger)
                    labTrigger.ImageKey = "H";
                else
                    labTrigger.ImageKey = "L";
                labStatus.Text = "读取模块触发信号成功";
                labStatus.ForeColor = Color.Blue;
            }
            else
            {
                labStatus.Text = "读取模块触发信号失败:" + er;
                labStatus.ForeColor = Color.Red;
            }
        }
        #endregion

    }
}
