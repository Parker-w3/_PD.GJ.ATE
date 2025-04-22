using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Dev.RemIO;
using GJ.Para; 

namespace GJ.Tool
{
    public partial class udcIO : UserControl
    {
        #region 构造函数
        public udcIO(string IODB = "PlcLog\\PLC.accdb")
        {
            this.IODB = IODB;

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
        public  string IODB = "PlcLog\\PLC_HIPOT.accdb";
        private CIOCom io = null;
        private CIOPara ioMotor= null;
        private bool InitFlag = false;
        private Dictionary<string, PictureBox> readObj = null;
        private Dictionary<string, PictureBox> writeObj = null;
        private Dictionary<string, Button> opObj = null;
        private Dictionary<string, bool> refresObj = null;
        #endregion

        #region 面板回调函数
        private void udcIO_Load(object sender, EventArgs e)
        {
            cmbCom.Items.Clear();  
            cmbDevType.Items.Clear();
            cmbDevType.Items.Add(ECoilType.X);
            cmbDevType.Items.Add(ECoilType.Y);
            cmbDevType.Items.Add(ECoilType.D);
            cmbDevType.Text = "X";
            string[] com = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < com.Length; i++)
                cmbCom.Items.Add(com[i]);
            if (com.Length > 0)
                cmbCom.Text = com[0];
            if (!InitFlag)
            {
                if (IODB == string.Empty)
                    ioMotor = new CIOPara();
                else
                    ioMotor = new CIOPara(IODB);
                InitialREG();
            }
            InitFlag = true;
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (cmbCom.Text == "")
            {
                labStatus.Text = "请输入串口编号";
                labStatus.ForeColor = Color.Red;
                return;
            }
            string er=string.Empty; 
            if (btnOpen.Text == "打开")
            {
                io = new CIOCom(EIOType.IO_24_16);
                if(!io.open(cmbCom.Text,ref er,txtBaud.Text))
                {
                    io = null;
                    labStatus.Text = "打开串口失败:"+er;
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                btnOpen.Text = "关闭";
                labStatus.Text = "成功打开串口";
                labStatus.ForeColor = Color.Blue;
                ioMotor.spinUp(io);
                tmrIO.Enabled = true;  
            }
            else
            {
                tmrIO.Enabled = false;
                ioMotor.spinDown();  
                if (io != null)
                {
                    io.close();
                    io = null;
                }
                btnOpen.Text = "打开";
                labStatus.Text = "关闭串口";
                labStatus.ForeColor = Color.Black;  
            }
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (io == null)
                {
                    labStatus.Text = "请确定已打开串口?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                if (txtAddr.Text == "")
                {
                    labStatus.Text = "请输入地址.";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                cmbVal.Items.Clear();
                string rData = string.Empty;
                string er = string.Empty;
                int devAddr = System.Convert.ToInt32(txtAddr.Text);
                ECoilType devType = (ECoilType)cmbDevType.SelectedIndex;
                int startAddr = System.Convert.ToInt32(txtStartAddr.Text);
                int N = System.Convert.ToInt32(txtLen.Text);
                int[] rVal = new int[N];
                if (!io.read(devAddr,devType, startAddr, ref rVal, ref er))
                {
                    labStatus.Text = "读取IO失败:" + er;
                    labStatus.ForeColor = Color.Red;
                    return;
                }               
                labRtn.Text = rData;
                for (int i = 0; i < rVal.Length; i++)
                {
                    cmbVal.Items.Add(i.ToString() + ":" + rVal[i].ToString());
                }
                if (rVal.Length > 0)
                    cmbVal.Text = "0:" + rVal[0].ToString();
                labStatus.Text = "读取IO成功.";
                labStatus.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }        
        }
        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                if (io == null)
                {
                    labStatus.Text = "请确定已打开串口?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                if (txtAddr.Text == "")
                {
                    labStatus.Text = "请输入地址.";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                string rData = string.Empty;
                string er = string.Empty;
                int devAddr = System.Convert.ToInt32(txtAddr.Text);
                ECoilType devType = (ECoilType)cmbDevType.SelectedIndex;
                int startAddr = System.Convert.ToInt32(txtStartAddr.Text);
                int N = System.Convert.ToInt32(txtLen.Text);
                if (N < 2)
                {
                    int val = System.Convert.ToInt32(txtData.Text);
                    if (!io.write(devAddr,devType, startAddr, val, ref er))
                    {
                        labStatus.Text = "写入IO失败:" + er;
                        labStatus.ForeColor = Color.Red;
                        return;
                    }
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
                    if (!io.write(devAddr,devType, startAddr, val, ref er))
                    {
                        labStatus.Text = "写入IO失败:" + er;
                        labStatus.ForeColor = Color.Red;
                        return;
                    }
                }
                labStatus.Text = "写入IO成功.";
                labStatus.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnSetAddr_Click(object sender, EventArgs e)
        {
            try
            {
                 if (io == null)
                {
                    labStatus.Text = "请确定已打开串口?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                if (txtSetAddr.Text == "")
                {
                    labStatus.Text = "请输入地址.";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                string er = string.Empty;
                if (io.setAddr(System.Convert.ToInt32(txtSetAddr.Text), ref er))
                {
                    labStatus.Text = "成功设置当前地址";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "设置当前地址失败:" + er;
                    labStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnReadAddr_Click(object sender, EventArgs e)
        {
            try
            {
                if (io == null)
                {
                    labStatus.Text = "请确定已打开串口?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                if (txtSetAddr.Text == "")
                {
                    labStatus.Text = "请输入地址.";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                int rData = 0;
                string er=string.Empty;
                if (io.readAddr(ref rData, ref er))
                {
                    txtSetAddr.Text = rData.ToString();
                    labStatus.Text = "成功读取当前地址";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "读取当前地址失败:"+er;
                    labStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnSetBaud_Click(object sender, EventArgs e)
        {
            try
            {
                if (io == null)
                {
                    labStatus.Text = "请确定已打开串口?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                if (txtSetBaud.Text == "")
                {
                    labStatus.Text = "请输入波特率.";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                string er = string.Empty;
                if (io.setBaud(System.Convert.ToInt32(txtAddr.Text),System.Convert.ToInt32(txtSetBaud.Text), ref er))
                {
                    labStatus.Text = "成功设置当前波特率";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "设置当前波特率失败:" + er;
                    labStatus.ForeColor = Color.Red;
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnReadBaud_Click(object sender, EventArgs e)
        {
            try
            {
                if (io == null)
                {
                    labStatus.Text = "请确定已打开串口?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                if (txtSetBaud.Text == "")
                {
                    labStatus.Text = "请输入波特率.";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                int rData = 0;
                string er = string.Empty;
                if (io.readBaud(System.Convert.ToInt32(txtAddr.Text),ref rData, ref er))
                {
                    if (rData > 57600)
                        rData = 115200;
                    txtSetBaud.Text = rData.ToString();
                    labStatus.Text = "成功读取当前波特率";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "读取当前波特率失败:" + er;
                    labStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnReadVer_Click(object sender, EventArgs e)
        {
            try
            {
                if (io == null)
                {
                    labStatus.Text = "请确定已打开串口?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                int rData = 0;
                string er = string.Empty;
                if (io.readVersion(System.Convert.ToInt32(txtAddr.Text),ref rData, ref er))
                {
                    txtSoftVer.Text = rData.ToString();
                    labStatus.Text = "成功读取当前版本";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "读取当前版本:" + er;
                    labStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnReadErr_Click(object sender, EventArgs e)
        {
            try
            {
                if (io == null)
                {
                    labStatus.Text = "请确定已打开串口?";
                    labStatus.ForeColor = Color.Red;
                    return;
                }
                int rData = 0;
                string er = string.Empty;
                if (io.readErrCode(System.Convert.ToInt32(txtAddr.Text),ref rData, ref er))
                {
                    txtErrCode.Text = rData.ToString();
                    labStatus.Text = "成功读取当前错误代码";
                    labStatus.ForeColor = Color.Blue;
                }
                else
                {
                    labStatus.Text = "读取当前错误代码:" + er;
                    labStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 方法
        private void InitialREG()
        {
            int n = 0;
            int row_set = 20;
            int col_set = 4;
            readObj = new Dictionary<string, PictureBox>();
            //读寄存器
            foreach (string keyVal in ioMotor.rIOBit.Keys)
            {
                if (ioMotor.rIOBit[keyVal].visabled == 1 && n < row_set*col_set)
                {
                    Label labReg = new Label();
                    labReg.Dock = DockStyle.Fill;
                    labReg.TextAlign = ContentAlignment.MiddleLeft;
                    labReg.Text = keyVal+"-["+ioMotor.rIOBit[keyVal].name +"]";

                    PictureBox picReg = new PictureBox();
                    picReg.Dock = DockStyle.Fill;
                    picReg.Margin = new Padding(0);
                    picReg.Image = imageList1.Images["L"];
                    picReg.SizeMode = PictureBoxSizeMode.CenterImage;
                    if (!readObj.ContainsKey(keyVal))
                        readObj.Add(keyVal, picReg);                   
                    int col = (n + row_set) / row_set - 1;
                    int row = n +1 - col * row_set;
                    panelrREG.Controls.Add(labReg, col * 2, row);
                    panelrREG.Controls.Add(readObj[keyVal], 1 + col * 2, row);
                    n++;
                }
            }
            //写寄存器
            row_set = 10;
            col_set = 3;
            writeObj = new Dictionary<string,PictureBox>();
            opObj = new Dictionary<string, Button>();
            refresObj = new Dictionary<string, bool>();
            n = 0;
            foreach (string keyVal in ioMotor.wIOBit.Keys)
            {
                if (ioMotor.wIOBit[keyVal].visabled == 1 && n < row_set * col_set)
                {
                    Label labReg = new Label();
                    labReg.Dock = DockStyle.Fill;
                    labReg.TextAlign = ContentAlignment.MiddleLeft;
                    labReg.Text = keyVal + "-[" + ioMotor.wIOBit[keyVal].name + "]";

                    Button btnReg = new Button();
                    btnReg.Dock = DockStyle.Fill;
                    btnReg.Margin = new Padding(0);
                    btnReg.TextAlign = ContentAlignment.MiddleCenter;

                    PictureBox picReg = new PictureBox();
                    picReg.Dock = DockStyle.Fill;
                    picReg.Margin = new Padding(0);
                    picReg.Image = imageList1.Images["L"];
                    picReg.SizeMode = PictureBoxSizeMode.CenterImage;
                    if (!writeObj.ContainsKey(keyVal))
                        writeObj.Add(keyVal, picReg);
                    btnReg.Text = "ON";                    
                  
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

        #region IO监控
        private void tmrIO_Tick(object sender, EventArgs e)
        {

            //读寄存器
            foreach (string keyVal in readObj.Keys)
            {
                PictureBox picImg = (PictureBox)readObj[keyVal];
                if (!ioMotor.rIOVal.ContainsKey(keyVal))
                {
                    picImg.Image = imageList1.Images["F"];
                }
                else
                {
                    if (ioMotor.rIOVal[keyVal] == -1)
                        picImg.Image = imageList1.Images["F"];
                    else if (ioMotor.rIOVal[keyVal] == CIOCom.XOFF)
                        picImg.Image = imageList1.Images["L"];
                    else
                        picImg.Image = imageList1.Images["H"];
                }
            }
            foreach (string keyVal in writeObj.Keys)
            {             
                PictureBox picImg = (PictureBox)writeObj[keyVal];
                if (ioMotor.wIOVal[keyVal] == -1)
                {
                    picImg.Image = imageList1.Images["F"];
                    opObj[keyVal].Text = "--";
                }
                else if (ioMotor.wIOVal[keyVal] == CIOCom.YOFF)
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
        private void OnSetWrite(object sender, EventArgs e)
        {
            if (io == null)
            {
                labStatus.Text = "请确定连接上IO?";
                labStatus.ForeColor = Color.Red;
                return;
            }
            Button btn = (Button)sender;
            string keyVal = btn.Tag.ToString();
            if (btn.Text == "ON")
            {
                if (ioMotor.addIoWrite(keyVal, CIOCom.YON))
                {
                    writeObj[keyVal].Image = imageList1.Images["H"];
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
                if (ioMotor.addIoWrite(keyVal, CIOCom.YOFF))
                {
                    writeObj[keyVal].Image = imageList1.Images["L"];
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
        }
        #endregion
    }
}
