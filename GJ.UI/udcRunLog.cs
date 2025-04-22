using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading; 
namespace GJ.UI
{
   [ToolboxBitmap(typeof(udcRunLog), "RunLog.bmp")]
   public partial class udcRunLog : UserControl
   {
      #region 构造函数
      public udcRunLog()
      {
         InitializeComponent();
      }
      #endregion

      #region 枚举
      public enum ELog
      {
         /// <summary>
         /// 内容(黑色)
         /// </summary>
         Content, 
         /// <summary>
         /// 注意(蓝色)
         /// </summary>
         Action,
         /// <summary>
         /// 正常(绿色)
         /// </summary>
         OK,
         /// <summary>
         /// 异常(红色)
         /// </summary>
         NG,
         /// <summary>
         /// 错误(黄色)
         /// </summary>
         Err
      }
      #endregion

      #region 字段
      private int maxLine = 1000;
      private double maxMB =1;
      private Color[] colorArray = new Color[] { Color.Black, Color.Blue, Color.Green, Color.Red, Color.DarkOrange };
      private bool saveEnable = true;
      private string saveName = "RunLog";
      private bool mVisible = false;
      #endregion

      #region 属性
      [Localizable(false)]
      [Bindable(false)]
      [Browsable(true)] 
      [Category("自定义")]
      [Description("标题")]    
      public string mTitle
      {
         set { labTitle.Text = value; }
         get { return labTitle.Text; }
      }
      [Localizable(false)]
      [Bindable(false)]
      [Browsable(true)]
      [Category("自定义")]
      [Description("标题可见")] 
      public bool mTitleEnable
      {
          set {
              if (value)
              {
                  panelMain.RowStyles[0].Height = 28;
                  labTitle.Visible = true;
              }
              else
              {
                  panelMain.RowStyles[0].Height = 0;
                  labTitle.Visible = false; 
              }
                   
            }
      }
      [Localizable(false)]
      [Bindable(false)]
      [Browsable(true)]
      [Category("自定义")]
      [Description("字体")]  
      public Font mFont
      {
          set { rtbLog.Font = value; }
          get { return rtbLog.Font; }
      }
      [Localizable(false)]
      [Bindable(false)]
      [Browsable(true)]
      [Category("自定义")]
      [Description("日志最大行数")] 
      public int mMaxLine
      {
          set { maxLine = value; }
          get { return maxLine; }
      }
      [Localizable(false)]
      [Bindable(false)]
      [Browsable(true)]
      [Category("自定义")]
      [Description("日志文件大小")] 
      public double mMaxMB
      {
          set { maxMB = value; }
          get { return maxMB; }
      }
      [Localizable(false)]
      [Bindable(false)]
      [Browsable(true)]
      [Category("自定义")]
      [Description("是否保存日志")] 
      public bool mSaveEnable
      {
          set { saveEnable = value; }
          get { return saveEnable; }
      }
      [Localizable(false)]
      [Bindable(false)]
      [Browsable(true)]
      [Category("自定义")]
      [Description("设置保存日志名称")]
      public string mSaveName
      {
          set { saveName = value; }
          get { return saveName; }
      }
      #endregion

      #region 同步锁
      private AutoResetEvent mHEvent = new AutoResetEvent(true);
      #endregion

      #region 委托
      private delegate void LogHandler(string wMessage, ELog wLog);
      #endregion

      #region 方法
      /// <summary>
      /// 保存数据
      /// </summary>
      /// <param name="wMessage"></param>
      /// <param name="wLog"></param>
      public void Log(string wMessage, ELog wLog)
      {
         if (this.InvokeRequired)    //跨线程调用
            this.Invoke(new LogHandler(Log), wMessage, wLog);
         else
         {
            try
            {
               mHEvent.WaitOne();                   //防止对同一文件写入数据
               if (rtbLog.Lines.Length > maxLine)   //清空数据
                  rtbLog.Clear();
               if (wMessage == string.Empty)
                  return;               
               string insertNow = DateTime.Now.ToString("HH:mm:ss.") + DateTime.Now.Millisecond.ToString("D3") +" | ";
               rtbLog.AppendText(insertNow);
               int lines = rtbLog.Text.Length;
               int lens = wMessage.Length;
               rtbLog.AppendText(wMessage + "\r\n");
               rtbLog.Select(lines, lens);
               rtbLog.SelectionColor = colorArray[(int)wLog];
               rtbLog.ScrollToCaret();
               rtbLog.Refresh();
               string saveInfo=insertNow+wMessage;
               if (saveEnable)               
                  SaveToTxt(saveInfo);               
            }
            catch (Exception)
            {
               
            }
            finally
            {
               mHEvent.Set(); 
            }
         }
      }
      /// <summary>
      /// 保存日志
      /// </summary>
      /// <param name="insertNow"></param>
      /// <param name="wMessage"></param>
      /// <param name="wLog"></param>
      private void SaveToTxt(string wMessage)
      {
          try
          {
              //获取保存文件名称
              string fileName = string.Empty;
              string path = saveName + "\\" + DateTime.Now.ToString(@"yyyy\\MM\\dd")+"\\";
              if (!Directory.Exists(path))
              {
                  Directory.CreateDirectory(path);
                  fileName = path + saveName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
              }
              else
              {
                  string[] dirFileName = Directory.GetFiles(path);
                  if (dirFileName.Length == 0)
                      fileName = path + saveName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
                  else
                  {
                      DateTime rDate = DateTime.Now;
                      for (int i = 0; i < dirFileName.Length; i++)
                      {
                          if (i == 0)
                          {
                              rDate = File.GetLastWriteTime(dirFileName[i]);
                              fileName = dirFileName[i];
                              continue;
                          }
                          if (rDate < File.GetLastWriteTime(dirFileName[i]))
                          {
                              rDate = File.GetLastWriteTime(dirFileName[i]);
                              fileName = dirFileName[i];
                          }
                      }
                  }                 
              }
              //判断文件是否过大？
              if (File.Exists(fileName))
              {
                  double rSize = new FileInfo(fileName).Length / 1024 / 1024;  //取文件大小为 KB--MB
                  if (rSize > maxMB)
                      fileName = path + saveName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
              }
              StreamWriter sw = new StreamWriter(fileName, true, Encoding.Default);
              sw.WriteLine(wMessage);
              sw.Flush();
              sw.Close();
              sw.Dispose();
          }
          catch (Exception e)
          {
              MessageBox.Show(e.ToString());  
          }
      }
      #endregion

      private void labTitle_Click(object sender, EventArgs e)
      {
          if (!mVisible)
          {
              rtbLog.Visible = false;
              mVisible = true;
          }
          else
          {
              rtbLog.Visible = true;
              mVisible = false;
          }
      }
   }
}
