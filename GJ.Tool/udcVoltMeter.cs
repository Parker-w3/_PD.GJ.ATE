using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.Dev.VoltMeter ;  

namespace GJ.Tool
{
   public partial class udcVoltMeter : UserControl
   {
      #region 构造函数
       public udcVoltMeter()
      {
         InitializeComponent();

         IntialControl();

         SetDoubleBuffered();

         
      }
      #endregion

      #region 字段
      private ZH40063   comMon = null;
   
      private Label[] labVs;
  
      #endregion

      #region 初始化
      /// <summary>
      /// 绑定控件
      /// </summary>
      private void IntialControl()
      {
    
         labVs = new Label[]{labVs1, labVs2,labVs3,labVs4 };
   
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
             comMon = new ZH40063();
             
            if (!comMon.open (cmbCOM.Text, ref er, txtBaud.Text))
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
      private void btnRead_Click(object sender, EventArgs e)
      {
          try
          {
              string er=string.Empty ;
              double[] ACVolt =new double[4];
              if (!comMon.readACV(ref ACVolt, ref er))
              {
                  labStatus.Text = "读取电压失败：" + er;

              }
              else
              {
                  for (int i = 0; i < 4; i++)
                  {

                      labVs[i].Text = ACVolt[i].ToString();
                  }

              }

          }

          catch (Exception ex)
          {

              throw ex;
          }
      }
      #endregion

    

   }
}
