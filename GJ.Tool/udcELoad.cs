using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Dev.ELoad;  

namespace GJ.Tool
{
   public partial class udcELoad : UserControl
   {
      #region 构造函数
      public udcELoad(ELoadType loadType)
      {
         InitializeComponent();

         IntialControl();

         SetDoubleBuffered();

         this.loadType = loadType;

         switch (loadType)
         {
            case ELoadType.GJEL_20_16:  
               break;
            //case ELoadType.GJEL_40_8L:
            //   for (int i = 4; i < 8; i++)
            //   {
            //      labCH[i].Visible = false;
            //      cmbMode[i].Visible = false;   
            //      labVs[i].Visible = false;
            //      labV[i].Visible = false;
            //      labCur[i].Visible = false;
            //      txtLoad[i].Visible = false;
            //      txtVon[i].Visible = false;  
            //   }  
            //   break;
            case ELoadType.GJEL_100_4L:
               for (int i = 2; i < 8; i++)
               {
                  labCH[i].Visible = false;
                  cmbMode[i].Visible = false;
                  labVs[i].Visible = false;
                  labV[i].Visible = false;
                  labCur[i].Visible = false;
                  txtLoad[i].Visible = false;
                  txtVon[i].Visible = false; 
               }  
               break;
            default:
               break;
         }
         
      }
      #endregion

      #region 字段
      private CELCom comMon = null;
      private ELoadType loadType;
      private ComboBox[] cmbMode;
      private TextBox[] txtVon;
      private TextBox[] txtLoad;
      private Label[] labCH;
      private Label[] labVs;
      private Label[] labV;
      private Label[] labCur;
      #endregion

      #region 初始化
      /// <summary>
      /// 绑定控件
      /// </summary>
      private void IntialControl()
      {
         cmbMode = new ComboBox[]{
                                 cmbMode1,cmbMode2,cmbMode3,cmbMode4,
                                 cmbMode5,cmbMode6,cmbMode7,cmbMode8
                                };
         txtVon = new TextBox[]{
                              txtVon1,txtVon2,txtVon3,txtVon4,
                              txtVon5,txtVon6,txtVon7,txtVon8
                             };
         txtLoad = new TextBox[]{
                               txtLoad1,txtLoad2,txtLoad3,txtLoad4,
                               txtLoad5,txtLoad6,txtLoad7,txtLoad8
                              };
         labCH = new Label[]{
                             labCH1,labCH2,labCH3,labCH4,
                             labCH5,labCH6,labCH7,labCH8
                            }; 
         labVs = new Label[]{
                           labVs1, labVs2,labVs3,labVs4,
                           labVs5,labVs6,labVs7,labVs8
                          };
         labV = new Label[]{
                           labV1,labV2,labV3,labV4,
                           labV5,labV6,labV7,labV8
                           };
         labCur = new Label[]{
                           labCur1,labCur2,labCur3,labCur4,
                           labCur5,labCur6,labCur7,labCur8
                           };  
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

      #region 面板回调函数
      private void udcELoad_Load(object sender, EventArgs e)
      {
         string[] com = System.IO.Ports.SerialPort.GetPortNames();
         for (int i = 0; i < com.Length; i++)
            cmbCOM.Items.Add(com[i]);
         if (com.Length > 0)
            cmbCOM.Text = com[0];
         for (int i = 0; i < cmbMode.Length ; i++)
         {
            cmbMode[i].Items.Clear(); 
            cmbMode[i].Items.Add(EMode.CC);
            cmbMode[i].Items.Add(EMode.CV);
            cmbMode[i].Items.Add(EMode.LED);
            cmbMode[i].SelectedIndex=0;
         }
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
            comMon = new CELCom(loadType);
            if (!comMon.open(cmbCOM.Text, ref er, txtBaud.Text))
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
      private void btnSetAddr_Click(object sender, EventArgs e)
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
         if (!comMon.SetNewAddr(wAddr, ref er))
         {
            labStatus.Text = "设置地址失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labStatus.Text = "设置新地址OK.";
         labStatus.ForeColor = Color.Blue;
      }
      private void btnSetLoad_Click(object sender, EventArgs e)
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
         CEL_SetPara wPara=new CEL_SetPara();
         for (int i = 0; i < comMon.mELCH ; i++)
			{
          wPara.Run_Mode[i]=(EMode)cmbMode[i].SelectedIndex;  
			 wPara.Run_Val[i]=System.Convert.ToDouble(txtLoad[i].Text);    
          wPara.Run_Von[i]=System.Convert.ToDouble(txtVon[i].Text);     
			}
         if (!comMon.SetELData(wAddr,wPara,ref er))
         {
            labStatus.Text = "设置模块负载参数失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labStatus.Text = "设置模块负载参数成功.";
         labStatus.ForeColor = Color.Blue;
      }
      private void btnReadSet_Click(object sender, EventArgs e)
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
         CEL_ReadSetPara rPara = new CEL_ReadSetPara();
         if(!comMon.ReadELLoadSet(wAddr,rPara,ref er))
         {
            labStatus.Text = "读取模块负载参数失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labStatus.Text = "读取模块负载参数成功.";
         labStatus.ForeColor = Color.Blue;
         for (int i = 0; i < comMon.mELCH; i++)
         {
            cmbMode[i].SelectedIndex = (int)rPara.LoadMode[i];
            txtLoad[i].Text = rPara.LoadVal[i].ToString("0.0");
            txtVon[i].Text = rPara.Von[i].ToString("0.0");     
         }
      }

      private void btnReadData_Click(object sender, EventArgs e)
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
         CEL_ReadData rData = new CEL_ReadData();
         if (!comMon.ReadELData(wAddr, rData, ref er))
         {
            labStatus.Text = "读取模块数据失败:" + er;
            labStatus.ForeColor = Color.Red;
            return;
         }
         labStatus.Text = "读取模块数据成功.";
         labStatus.ForeColor = Color.Blue;
         for (int i = 0; i < comMon.mELCH; i++)
         {
            labV[i].Text = rData.Volt[i].ToString("0.00");
            labVs[i].Text = rData.Vs[i].ToString("0.00");
            labCur[i].Text = rData.Load[i].ToString("0.00");    
         }
         labSatus.Text = rData.Status;
         labOnOff.Text = rData.ONOFF.ToString();
         labT0.Text = rData.NTC_0.ToString();
         labT1.Text = rData.NTC_1.ToString();
         labOCP.Text = rData.OCP.ToString();
         labOPP.Text = rData.OPP.ToString();
         labOTP.Text = rData.OTP.ToString();
         labOVP.Text = rData.OVP.ToString();   
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

              gridMon.Rows[i].Cells[0].Value = wAddr.ToString();
              gridMon.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

              CEL_ReadData rData = new CEL_ReadData();
              if (comMon.ReadELData(wAddr, rData, ref er))
              {
                  gridMon.Rows[i].Cells[1].Value = "PASS";
                  gridMon.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
                  gridMon.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                  string temp = string.Empty;

                  temp += rData.ONOFF.ToString()+"|";
                  if (rData.Status != "OK")
                  {
                      temp += rData.Status;
                      gridMon.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                  }
                  else
                      gridMon.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
                  gridMon.Rows[i].Cells[2].Value = temp;

                  temp = string.Empty;
                  for (int j = 0; j < comMon.mELCH; j++)
                  {
                      temp += rData.Vs[j].ToString("0.00") + "Vs";
                      temp += rData.Volt[j].ToString("0.00") + "V;";
                      temp += rData.Load[j].ToString("0.00") + "A|"; 
                  }
                  gridMon.Rows[i].Cells[3].Value = temp;
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
