using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Dev;
using GJ.Dev.Mon;
namespace GJ.Tool
{
   public partial class udcGJMon32 : UserControl
   {
      public udcGJMon32()
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
        
         labV = new Label[] { 
                             labV1, labV2, labV3, labV4, labV5, labV6, labV7, labV8, 
                             labV9, labV10, labV11, labV12, labV13, labV14, labV15, labV16, 
                             labV17, labV18, labV19, labV20, labV21, labV22, labV23, labV24, 
                             labV25, labV26, labV27, labV28, labV29, labV30, labV31, labV32
                             };
         txtOnOff = new TextBox[] { txtOnOff1, txtOnOff2, txtOnOff3, txtOnOff4 };
         txtOn = new TextBox[] { txtOn1, txtOn2, txtOn3, txtOn4 };
         txtOff = new TextBox[] { txtOff1, txtOff2, txtOff3, txtOff4 };
         LabX = new Label[] { labX1, labX2, labX3, labX4, labX5, labX6, labX7, labX8, labX9 };              
      }
      /// <summary>
      /// 设置双缓冲,防止界面闪烁
      /// </summary>
      private void SetDoubleBuffered()
      {
         splitContainer1.Panel1.GetType().GetProperty("DoubleBuffered",
                                        System.Reflection.BindingFlags.Instance |
                                        System.Reflection.BindingFlags.NonPublic)
                                        .SetValue(splitContainer1.Panel1, true, null);
         splitContainer1.Panel2.GetType().GetProperty("DoubleBuffered",
                                        System.Reflection.BindingFlags.Instance |
                                        System.Reflection.BindingFlags.NonPublic)
                                        .SetValue(splitContainer1.Panel1, true, null);
         panel1.GetType().GetProperty("DoubleBuffered",
                                        System.Reflection.BindingFlags.Instance |
                                        System.Reflection.BindingFlags.NonPublic)
                                        .SetValue(panel1, true, null);
         panel2.GetType().GetProperty("DoubleBuffered",
                                         System.Reflection.BindingFlags.Instance |
                                         System.Reflection.BindingFlags.NonPublic)
                                         .SetValue(panel1, true, null);
         foreach (Control panelItem in panel2.Controls)
         {
            if (panelItem.GetType().ToString() == "System.Windows.Forms.TableLayoutPanel")
            {
               panelItem.GetType().GetProperty("DoubleBuffered",
                                         System.Reflection.BindingFlags.Instance |
                                         System.Reflection.BindingFlags.NonPublic)
                                         .SetValue(panelItem, true, null);
            }
         }
      }
      #endregion

      #region 字段
      private CGJMonCom comMon= null;
      private Label[] labV;
      private TextBox[] txtOnOff;
      private TextBox[] txtOn;
      private TextBox[] txtOff;
      private Label[] LabX;
      #endregion

      #region 面板回调函数
      private void udcGJMon32_Load(object sender, EventArgs e)
      {
         string[] com = System.IO.Ports.SerialPort.GetPortNames();
         for (int i = 0; i < com.Length; i++)         
            cmbCOM.Items.Add(com[i]);
         if (com.Length > 0)
            cmbCOM.Text = com[0];
      }
      private void btnOpen_Click(object sender, EventArgs e)
      {
         if (cmbCOM.Text =="")
         {
            labStatus.Text = "请输入串口编号";
            labStatus.ForeColor = Color.Red;  
            return;
         }
         string er=string.Empty;
         if (comMon == null)
         {
            comMon = new CGJMonCom() ;
            if (!comMon.open(cmbCOM.Text, ref er, "57600,n,8,1"))
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
            labStatus.ForeColor = Color.Black; 
         }
      }

      private void btnSet_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red; 
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red; 
            return;
         }
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         string er=string.Empty;
         if (!comMon.SetNewAddr(wAddr, ref er))
         {
            labStatus.Text = "设置地址失败:" + er;
            labStatus.ForeColor = Color.Red; 
            return;
         }
         labStatus.Text = "设置新地址OK.";
         labStatus.ForeColor = Color.Blue; 
      }

      private void btnVer_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         string ver = string.Empty;
         string er = string.Empty;
         if (!comMon.ReadVersion(wAddr, ref ver, ref er))
         {
            labStatus.Text = "读取模块版本失败:"+er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labVersion.Text = ver;
         labStatus.Text = "成功读取模块版本.";
         labStatus.ForeColor = Color.Blue;
      }

      private void btnVolt_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         string er = string.Empty;
         CVoltVal monV = new CVoltVal();
         ESynON  synOn=ESynON.同步;
         if(!chkSync.Checked)
            synOn=ESynON.异步;
         if (chkMode.Checked)
         {
             if (!comMon.ReadVolt(wAddr, ref monV, ref er, synOn,ERunMode.从控ACONOFF的工作模式))
             {
                 labStatus.Text = "读取模块电压失败:" + er;
                 labStatus.ForeColor = Color.Red;
                 return;
             }
         }
         else
         {
             if (!comMon.ReadVolt(wAddr, ref monV, ref er, synOn))
             {
                 labStatus.Text = "读取模块电压失败:" + er;
                 labStatus.ForeColor = Color.Red;
                 return;
             }
         }

         
         for (int i = 0; i < 32; i++)
            labV[i].Text = monV.volt[i].ToString("0.000");
         labOnOff.Text = (monV.onOffFlag == 1) ? "ON" : "OFF";   
         labStatus.Text = "成功读取模块电压值.";
         labStatus.ForeColor = Color.Blue;

      }
      private void btnSetPara_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         string er = string.Empty;
         COnOffPara  wPara = new COnOffPara();
         wPara.BIToTime = System.Convert.ToInt32(txtBIToTime.Text);
         for (int i = 0; i < 4; i++)
         {
            wPara.wOnOff[i]=System.Convert.ToInt32(txtOnOff[i].Text);
            wPara.wON [i] = System.Convert.ToInt32(txtOn[i].Text);
            wPara.wOFF[i] = System.Convert.ToInt32(txtOff[i].Text);
         }
         if (!comMon.SetOnOffPara(wAddr, wPara, ref er))
         {
            labStatus.Text = "设置模块ONOFF参数失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labStatus.Text = "成功设置模块ONOFF参数.";
         labStatus.ForeColor = Color.Blue;
      }

      private void btnReadPara_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         string er = string.Empty;
         COnOffPara rPara = new COnOffPara();
         if (!comMon.ReadOnOffPara(wAddr, ref rPara, ref er))
         {
            labStatus.Text = "读取模块ONOFF参数失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         txtBIToTime.Text = rPara.BIToTime.ToString();
         for (int i = 0; i < 4; i++)
         {
            txtOnOff[i].Text = rPara.wOnOff[i].ToString();
            txtOn[i].Text = rPara.wON[i].ToString();
            txtOff[i].Text = rPara.wOFF[i].ToString();    
         }
         labStatus.Text = "成功读取模块ONOFF参数.";
         labStatus.ForeColor = Color.Blue;
      }
      private void btnReadSgn_Click(object sender, EventArgs e)
      {

         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         string er = string.Empty;
         CReadRunPara rPara = new CReadRunPara();
         if (!comMon.ReadRunData(wAddr, ref rPara, ref er))
         {
            labStatus.Text = "读取模块控制信号失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labRunMin.Text  = rPara.runTolTime.ToString();
         labRunSec.Text  = rPara.secMinCnt.ToString();
         labStartFlag.Text = rPara.startFlag.ToString();
         labFinishFlag.Text = rPara.biFinishFlag.ToString();
         if (rPara.onoff_Flag == 1)
         {
            labAcOn.Text = "ON";  
            labRunOnOff.Text = "ON" + rPara.onoff_YXDH.ToString();            
         }
         else
         {
            labAcOn.Text = "OFF";  
            labRunOnOff.Text = "OFF" + rPara.onoff_YXDH.ToString();
         }
         labOnOffTime.Text = rPara.onoff_RunTime.ToString();
         labOnOffCycle.Text = rPara.onoff_Cnt.ToString();
         labRunFlag.Text = rPara.runTypeFlag.ToString();
         if (rPara.ac_Sync == 1)
            labRelayON.Text = "ON"; 
         else
            labRelayON.Text = "OFF";
         if (rPara.s1 != 1)
            labS1.BackColor =Color.Black;
         else
            labS1.BackColor = Color.Red;
         if (rPara.s2 == 1)
            labS2.BackColor = Color.Red;
         else
            labS2.BackColor = Color.Black;
         for (int i = 0; i < 9; i++)
         {
            if (rPara.x[i] != 1)
               LabX[i].BackColor = Color.Black;
            else
               LabX[i].BackColor = Color.Red;    
         }         
         labErrCode.Text = rPara.errCode.ToString();
         if (rPara.errCode==EErrCode.正常)
            labErrCode.ForeColor = Color.LightGreen;
         else
            labErrCode.ForeColor = Color.Red;
         labStatus.Text = "成功读取模块控制信号.";
         labStatus.ForeColor = Color.Blue;         
      }

      private void btnStart_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }         
         CWriteRunPara wPara = new CWriteRunPara();
         wPara.runTolTime = 0;
         wPara.secMinCnt = 0;
         wPara.onoff_RunTime = 0;  
         wPara.onoff_Cnt = 0;
         wPara.onoff_Flag = 1;
         wPara.onoff_YXDH = 1;
         wPara.runTypeFlag = 3;
         wPara.startFlag = 1;
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         string er = string.Empty;
         if (!comMon.SetRunStart(wAddr, wPara, ref er))
         {
            labStatus.Text = "启动模块运行失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labStatus.Text = "成功启动模块运行.";
         labStatus.ForeColor = Color.Blue;
      }

      private void btnStop_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         string er = string.Empty;
         if (!comMon.ForceFinish(wAddr, ref er))
         {
            labStatus.Text = "停止模块运行失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labStatus.Text = "成功停止模块运行.";
         labStatus.ForeColor = Color.Blue;
      }

      private void btnRlyON_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         int relayNo = System.Convert.ToInt16(txtRelayNo.Text);   
         string er = string.Empty;
         if (!comMon.SetRelayOn(wAddr, relayNo, ref er))
         {
            labStatus.Text = "设置模块Relay ON失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labStatus.Text = "成功设置模块Relay ON.";
         labStatus.ForeColor = Color.Blue;
      }

      private void btnRlyOff_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int wAddr = System.Convert.ToInt16(txtAddr.Text);
         int relayNo = 101;
         string er = string.Empty;
         if (!comMon.SetRelayOn(wAddr, relayNo, ref er))
         {
            labStatus.Text = "设置模块Relay OFF失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labStatus.Text = "成功设置模块Relay OFF.";
         labStatus.ForeColor = Color.Blue;
      }
      private void btnScan_Click(object sender, EventArgs e)
      {
         if (comMon == null)
         {
            labStatus.Text = "请确定串口是否打开?";
            labStatus.ForeColor = Color.Red;
            return;
         }
         if (txtAddr.Text == "")
         {
            labStatus.Text = "请输入设置地址号.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int wStartAddr = System.Convert.ToInt16(txtAddr.Text);
         int wEndAddr = System.Convert.ToInt16(txtEndAddr.Text);
         if (wStartAddr > wEndAddr)
         {
            labStatus.Text = "末尾地址需大于等于设置地址.";
            labStatus.ForeColor = Color.Red;
            return;
         }
         int rowNum = wEndAddr - wStartAddr + 1;
         gridMon.Rows.Clear();
         for (int i = 0; i < rowNum; i++)
         {
            gridMon.RowCount++;
            int wAddr = wStartAddr + i;
            string er = string.Empty;
            string ver = string.Empty;
            gridMon.Rows[i].Cells[0].Value = wAddr.ToString();
            gridMon.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (comMon.ReadVersion(wAddr, ref ver, ref er))
            {
               gridMon.Rows[i].Cells[1].Value = "PASS";
               gridMon.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
               gridMon.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
               gridMon.Rows[i].Cells[2].Value = ver;
               CVoltVal volt=new CVoltVal();
               string temp = string.Empty; 
               if(comMon.ReadVolt(wAddr,ref volt,ref er))
               {
                  for (int j= 0; j < 32; j++)                  
                     temp += (j + 1).ToString("D2") + ":" + volt.volt[i].ToString("0.00");                 
               }
               if(volt.onOffFlag==1)
                  gridMon.Rows[i].Cells[3].Value = "ON";
               else
                  gridMon.Rows[i].Cells[3].Value = "OFF";
               gridMon.Rows[i].Cells[4].Value = temp;
            }
            else
            {
               gridMon.Rows[i].Cells[1].Value = "FAIL";
               gridMon.Rows[i].Cells[1].Style.ForeColor = Color.Red;
               gridMon.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            gridMon.CurrentCell = gridMon.Rows[i].Cells[0];
            Application.DoEvents();
         }
      }
      #endregion


   }
}
