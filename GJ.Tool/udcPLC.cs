using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Dev.PLC;
using GJ.Para;
namespace GJ.Tool
{
    public partial class udcPLC : UserControl
    {

        #region 构造函数
        public udcPLC(string plcDB = "PlcLog\\PLC.accdb")
        {
            this.plcDB = plcDB; 

            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

        }
        #endregion

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
                                            .SetValue(panel2, true, null);
            panel3.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel3, true, null);
            panel4.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel4, true, null);
            panelrREG.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panelrREG, true, null);
            panelwREG.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panelwREG, true, null);
        }
        #endregion

        #region 字段
        public string plcDB = "PlcLog\\PLC.accdb";
        private CPLCCom comMon = null;
        private CPLCPara plcMotor = null;
        private Dictionary<string, Control> readObj = null;
        private Dictionary<string, Control> writeObj = null;        
        private Dictionary<string, Button> opObj = null;
        private Dictionary<string, bool> refresObj = null;
        private bool InitFlag = false;
        #endregion

        #region 面板回调函数
        private void udcInovancePLC_Load(object sender, EventArgs e)
        {
            cmbDevType.Items.Clear();
            cmbDevType.Items.Add(EDevType.M);
            cmbDevType.Items.Add(EDevType.W);
            cmbDevType.Items.Add(EDevType.D);
            cmbDevType.Items.Add(EDevType.X);
            cmbDevType.Items.Add(EDevType.Y);
            cmbDevType.Text = "M";

            cmbMode.Items.Clear();
            cmbMode.Items.Add("串口通信"); 
            cmbMode.Items.Add("TCP/IP通信");                     
            cmbMode.SelectedIndex = 1;

            string[] com = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < com.Length; i++)
                cmbCom.Items.Add(com[i]);
            if (com.Length > 0)
                cmbCom.Text = com[0];

            if (!InitFlag)
            {
                if(plcDB==string.Empty)  
                   plcMotor = new CPLCPara();
                else
                    plcMotor = new CPLCPara(plcDB);
                InitialREG();
            }                
            InitFlag = true;

        }
        private void cmbMode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbMode.SelectedIndex == 0)
            {
                labAddr.Text = "PLC地址:";
                labPort.Text = "波特率:";
                txtAddr.Text = "01";
                txtPort.Text = "115200,n,8,2";  
            }
            else
            {
                labAddr.Text = "网络地址:";
                labPort.Text = "端口:";
                txtAddr.Text = "192.168.3.230";
                txtPort.Text = "502"; 
            }             
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {

            if (txtAddr.Text == "")
            {
                labStatus.Text = "请输入PLC网络地址";
                labStatus.ForeColor = Color.Red;
                return;
            }
            string er = string.Empty;
            if (comMon == null)
            {
                if (cmbMode.SelectedIndex == 0)
                {
                    comMon = new CPLCCom(EPLCType.InovanceCOM);
                    comMon.mPlcNo = System.Convert.ToInt32(txtAddr.Text);
                    if (!comMon.open(cmbCom.Text, ref er, txtPort.Text))
                    {
                        labStatus.Text = er;
                        labStatus.ForeColor = Color.Red;
                        comMon = null;
                        return;
                    }
                }                    
                else
                {
                    comMon = new CPLCCom(EPLCType.InovanceTCP);
                    if (!comMon.open(txtAddr.Text, ref er, txtPort.Text))
                    {
                        labStatus.Text = er;
                        labStatus.ForeColor = Color.Red;
                        comMon = null;
                        return;
                    }
                }
                plcMotor.spinUp(comMon);                
                btnOpen.Text = "断开";
                labStatus.Text ="连接PLC正常.";
                labStatus.ForeColor = Color.Blue;
                this.tmrPLC.Enabled = true; 
            }
            else
            {
                this.tmrPLC.Enabled = false; 
                plcMotor.spinDown();                    
                comMon.close();
                comMon = null;                
                btnOpen.Text = "连接";
                labStatus.Text = "断开连接.";
                labStatus.ForeColor = Color.Black;
            }
        }
        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                if (comMon == null)
                {
                    labStatus.Text = "请确定连接上PLC?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                if (txtAddr.Text == "")
                {
                    labStatus.Text = "请输入设置IP地址.";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                int waitTime = 0;
                string rData = string.Empty;
                string er = string.Empty;
                EDevType devType = (EDevType)cmbDevType.SelectedIndex;
                int startAddr = System.Convert.ToInt32(txtStartAddr.Text);
                int N = System.Convert.ToInt32(txtLen.Text);
                if (N < 2)
                {
                    int val = System.Convert.ToInt32(txtData.Text);
                    waitTime = Environment.TickCount;
                    if (!comMon.write(devType, startAddr, val, ref er))
                    {
                        labStatus.Text = "写入PLC失败:" + er;
                        labStatus.ForeColor = Color.Red;
                        return;
                    }
                    labVersion.Text = (Environment.TickCount - waitTime).ToString() + "ms";
                }
                else
                {
                    string temp = txtData.Text;
                    if (temp.LastIndexOf(';') == (temp.Length - 1))
                        temp = temp.Substring(0, temp.Length - 1);
                    string[] valArray = temp.Split(';');
                    int[] val = new int[valArray.Length];
                    for (int i = 0; i < val.Length; i++)
                    {
                        if (valArray[i] != "")
                            val[i] = System.Convert.ToInt32(valArray[i]);
                    }
                    waitTime = Environment.TickCount;
                    if (!comMon.write(devType, startAddr, val, ref er))
                    {
                        labStatus.Text = "写入PLC失败:" + er;
                        labStatus.ForeColor = Color.Red;
                        return;
                    }
                    labVersion.Text = (Environment.TickCount - waitTime).ToString() + "ms";
                }
                labStatus.Text = "写入PLC成功.";
                labStatus.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
       
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (comMon == null)
                {
                    labStatus.Text = "请确定连接上PLC?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                if (txtAddr.Text == "")
                {
                    labStatus.Text = "请输入设置IP地址.";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                cmbVal.Items.Clear();
                string rData = string.Empty;
                string er = string.Empty;
                EDevType devType = (EDevType)cmbDevType.SelectedIndex;
                int startAddr = System.Convert.ToInt32(txtStartAddr.Text);
                int N = System.Convert.ToInt32(txtLen.Text);
                if (devType == EDevType.W)
                    N *= 16;
                int[] rVal = new int[N];
                int waitTime = Environment.TickCount;
                if (!comMon.read(devType, startAddr, ref rVal, ref er))
                {
                    labStatus.Text = "读取PLC失败:" + er;
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                labVersion.Text = (Environment.TickCount - waitTime).ToString() + "ms";
                labRtn.Text = rData;
                for (int i = 0; i < rVal.Length; i++)
                {
                    cmbVal.Items.Add(i.ToString() + ":" + rVal[i].ToString());
                }
                if (rVal.Length > 0)
                    cmbVal.Text = "0:" + rVal[0].ToString();
                labStatus.Text = "读取PLC成功.";
                labStatus.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }        
        }
        private void tmrPLC_Tick(object sender, EventArgs e)
        {
            //读寄存器
            foreach (string keyVal in readObj.Keys)
            {
                if (readObj[keyVal].GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                  TextBox txtBox=(TextBox)readObj[keyVal];
                  if (!plcMotor.rREG.ContainsKey(keyVal))
                     txtBox.Text = "-1";
                  else                     
                     txtBox.Text = plcMotor.rREGVal[keyVal].ToString();
                }
                else
                {
                    PictureBox picImg = (PictureBox)readObj[keyVal];
                    if (!plcMotor.rREG.ContainsKey(keyVal))
                    {
                        picImg.Image = imageList1.Images["F"];
                    }
                    else
                    {
                        if (plcMotor.rREGVal[keyVal] == -1)
                            picImg.Image = imageList1.Images["F"];
                        else if (plcMotor.rREGVal[keyVal] == 0)
                            picImg.Image = imageList1.Images["L"];
                        else
                            picImg.Image = imageList1.Images["H"];
                    }
                    
                }
            }
            foreach (string keyVal in writeObj.Keys)
            {
                if (writeObj[keyVal].GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    if (plcMotor.wREGVal[keyVal] == -1)
                    {
                        refresObj[keyVal] = true;
                        TextBox txtBox = (TextBox)writeObj[keyVal];
                        txtBox.Text = plcMotor.wREGVal[keyVal].ToString();
                    }
                    else
                    {
                        if (refresObj[keyVal])
                        {
                            refresObj[keyVal] = false;
                            TextBox txtBox = (TextBox)writeObj[keyVal];
                            txtBox.Text = plcMotor.wREGVal[keyVal].ToString();
                        }
                    }                    
                }
                else
                {
                    PictureBox picImg = (PictureBox)writeObj[keyVal];
                    if (plcMotor.wREGVal[keyVal] == -1)
                    {
                        picImg.Image = imageList1.Images["F"];
                        opObj[keyVal].Text = "--"; 
                    }                        
                    else if (plcMotor.wREGVal[keyVal] == 0)
                    {
                        picImg.Image = imageList1.Images["L"];
                        opObj[keyVal].Text = "ON"; 
                    }
                    else
                    {
                        picImg.Image = imageList1.Images["H"];
                        opObj[keyVal].Text = "OFF"; 
                    }
                       
                }
            }
        }
        private void OnSetWrite(object sender, EventArgs e)
        {
            if (comMon == null)
            {
                labStatus.Text = "请确定连接上PLC?";
                labStatus.ForeColor = Color.Red;
                return;
            }
            Button btn = (Button)sender;
            string keyVal = btn.Tag.ToString();
            if (btn.Text == "ON")
            {
                if (plcMotor.addREGWrite(keyVal, 1))
                {
                    ((PictureBox)writeObj[keyVal]).Image = imageList1.Images["H"];
                    btn.Text = "OFF";
                    labStatus.Text = "写入寄存器OK";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "写入寄存器错误";
                    labStatus.ForeColor = Color.Red;
                }
            }
            else if (btn.Text == "OFF")
            {
                if (plcMotor.addREGWrite(keyVal, 0))
                {
                    ((PictureBox)writeObj[keyVal]).Image = imageList1.Images["L"];
                    btn.Text = "ON";
                    labStatus.Text = "写入寄存器OK";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "写入寄存器错误";
                    labStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                string strVal = ((TextBox)writeObj[keyVal]).Text;
                if (plcMotor.addREGWrite(keyVal, System.Convert.ToInt32(strVal)))
                {
                    refresObj[keyVal] = true; 
                    labStatus.Text = "写入寄存器OK";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "写入寄存器错误";
                    labStatus.ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region 方法
        private void InitialREG()
        {

            int n = 0;
            int row_set = 20;
            int col_set = 4;

            readObj = new Dictionary<string, Control>();
             //读寄存器
            foreach (string keyVal in plcMotor.rREG.Keys)
            {
                if (plcMotor.rREG[keyVal].regVisable == 1 && n < row_set*col_set)
                {
                    Label labReg = new Label();
                    labReg.Dock = DockStyle.Fill;
                    labReg.TextAlign = ContentAlignment.MiddleLeft;
                    labReg.Text = keyVal+"-["+plcMotor.rREG[keyVal].regName+"]";
                    if (plcMotor.rREG[keyVal].regType != EDevType.D)
                    {
                        PictureBox picReg = new PictureBox();
                        picReg.Dock = DockStyle.Fill;
                        picReg.Margin = new Padding(0);
                        picReg.Image = imageList1.Images["L"];
                        picReg.SizeMode = PictureBoxSizeMode.CenterImage;
                        if (!readObj.ContainsKey(keyVal))
                            readObj.Add(keyVal, picReg);
                    }
                    else
                    {
                        TextBox txtReg = new TextBox();
                        txtReg.Dock = DockStyle.Fill;
                        txtReg.Margin = new Padding(1);
                        txtReg.TextAlign = HorizontalAlignment.Center;
                        txtReg.Text = "0";
                        if (!readObj.ContainsKey(keyVal))
                            readObj.Add(keyVal, txtReg);
                    }
                    int col = (n + row_set) / row_set - 1;
                    int row = n + 1 - col * row_set;
                    panelrREG.Controls.Add(labReg, col * 2, row);
                    panelrREG.Controls.Add(readObj[keyVal], 1 + col * 2, row);
                    n++;
                }               
            }        
            //写寄存器
            row_set = 20;
            col_set =3;

            writeObj = new Dictionary<string, Control>();
            opObj = new Dictionary<string, Button>();
            refresObj = new Dictionary<string, bool>();
            n = 0;
            foreach (string keyVal in plcMotor.wREG.Keys)
           {
               if (plcMotor.wREG[keyVal].regVisable == 1 && n < row_set * col_set)
               {
                   Label labReg = new Label();
                   labReg.Dock = DockStyle.Fill;
                   labReg.TextAlign = ContentAlignment.MiddleLeft;
                   labReg.Text = keyVal + "-[" + plcMotor.wREG[keyVal].regName + "]"; ;

                   Button btnReg = new Button();
                   btnReg.Dock = DockStyle.Fill;
                   btnReg.Margin = new Padding(0); 
                   btnReg.TextAlign = ContentAlignment.MiddleCenter;

                   if (plcMotor.wREG[keyVal].regType != EDevType.D)
                   {
                       PictureBox picReg = new PictureBox();
                       picReg.Dock = DockStyle.Fill;
                       picReg.Margin = new Padding(0);
                       picReg.Image = imageList1.Images["L"];
                       picReg.SizeMode = PictureBoxSizeMode.CenterImage;
                       if (!writeObj.ContainsKey(keyVal))
                           writeObj.Add(keyVal, picReg);
                       btnReg.Text = "ON";                       
                   }
                   else
                   {
                       TextBox txtReg = new TextBox();
                       txtReg.Dock = DockStyle.Fill;
                       txtReg.Margin = new Padding(1);
                       txtReg.TextAlign = HorizontalAlignment.Center;
                       txtReg.Text = "0";
                       if (!writeObj.ContainsKey(keyVal))
                           writeObj.Add(keyVal, txtReg);
                       btnReg.Text = "设置"; 
                   }
                   btnReg.Tag = keyVal; 
                   if (!opObj.ContainsKey(keyVal))
                       opObj.Add(keyVal, btnReg);
                   if (!refresObj.ContainsKey(keyVal))
                       refresObj.Add(keyVal, true);  
                   opObj[keyVal].Click += new EventHandler(OnSetWrite);
                   int col = (n + row_set) / row_set - 1;
                   int row = n + 1 - col * row_set;
                   panelwREG.Controls.Add(labReg, col * 3, row);
                   panelwREG.Controls.Add(writeObj[keyVal], 1 + col * 3, row);
                   panelwREG.Controls.Add(opObj[keyVal], 2 + col * 3, row);
                   n++;               
               }
           }
        }
        #endregion

    }
}
