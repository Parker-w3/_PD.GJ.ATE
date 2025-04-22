namespace GJ.ATE
{
   partial class WndFrmRunLog
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WndFrmRunLog));
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.panel1 = new System.Windows.Forms.TableLayoutPanel();
         this.treeFiles = new System.Windows.Forms.TreeView();
         this.panel2 = new System.Windows.Forms.TableLayoutPanel();
         this.progressBar1 = new System.Windows.Forms.ProgressBar();
         this.labStatus = new System.Windows.Forms.Label();
         this.rtbRunLog = new System.Windows.Forms.RichTextBox();
         this.imageList1 = new System.Windows.Forms.ImageList(this.components);
         this.timer1 = new System.Windows.Forms.Timer(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // splitContainer1
         // 
         this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
         this.splitContainer1.Location = new System.Drawing.Point(0, 0);
         this.splitContainer1.Name = "splitContainer1";
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.panel1);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.rtbRunLog);
         this.splitContainer1.Size = new System.Drawing.Size(878, 768);
         this.splitContainer1.SplitterDistance = 293;
         this.splitContainer1.TabIndex = 0;
         // 
         // panel1
         // 
         this.panel1.ColumnCount = 1;
         this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.panel1.Controls.Add(this.treeFiles, 0, 0);
         this.panel1.Controls.Add(this.panel2, 0, 1);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.RowCount = 2;
         this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
         this.panel1.Size = new System.Drawing.Size(291, 766);
         this.panel1.TabIndex = 0;
         // 
         // treeFiles
         // 
         this.treeFiles.Dock = System.Windows.Forms.DockStyle.Fill;
         this.treeFiles.Location = new System.Drawing.Point(3, 3);
         this.treeFiles.Name = "treeFiles";
         this.treeFiles.Size = new System.Drawing.Size(285, 728);
         this.treeFiles.TabIndex = 1;
         this.treeFiles.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFiles_NodeMouseClick);
         // 
         // panel2
         // 
         this.panel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
         this.panel2.ColumnCount = 2;
         this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.49296F));
         this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.50704F));
         this.panel2.Controls.Add(this.progressBar1, 0, 0);
         this.panel2.Controls.Add(this.labStatus, 1, 0);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(3, 737);
         this.panel2.Name = "panel2";
         this.panel2.RowCount = 1;
         this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.panel2.Size = new System.Drawing.Size(285, 26);
         this.panel2.TabIndex = 2;
         // 
         // progressBar1
         // 
         this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.progressBar1.Location = new System.Drawing.Point(4, 4);
         this.progressBar1.Name = "progressBar1";
         this.progressBar1.Size = new System.Drawing.Size(178, 18);
         this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
         this.progressBar1.TabIndex = 0;
         // 
         // labStatus
         // 
         this.labStatus.AutoSize = true;
         this.labStatus.BackColor = System.Drawing.Color.Lime;
         this.labStatus.Dock = System.Windows.Forms.DockStyle.Fill;
         this.labStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
         this.labStatus.Location = new System.Drawing.Point(189, 1);
         this.labStatus.Name = "labStatus";
         this.labStatus.Size = new System.Drawing.Size(92, 24);
         this.labStatus.TabIndex = 1;
         this.labStatus.Text = "加载完毕..";
         this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // rtbRunLog
         // 
         this.rtbRunLog.Dock = System.Windows.Forms.DockStyle.Fill;
         this.rtbRunLog.Location = new System.Drawing.Point(0, 0);
         this.rtbRunLog.Name = "rtbRunLog";
         this.rtbRunLog.Size = new System.Drawing.Size(579, 766);
         this.rtbRunLog.TabIndex = 0;
         this.rtbRunLog.Text = "";
         // 
         // imageList1
         // 
         this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
         this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
         this.imageList1.Images.SetKeyName(0, "Tree View.ico");
         this.imageList1.Images.SetKeyName(1, "Folder List.ico");
         this.imageList1.Images.SetKeyName(2, "folder.ico");
         this.imageList1.Images.SetKeyName(3, "Folder-Closed.ico");
         this.imageList1.Images.SetKeyName(4, "View-One Page.ico");
         this.imageList1.Images.SetKeyName(5, "4577.ico");
         // 
         // timer1
         // 
         this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
         // 
         // WndFrmRunLog
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(878, 768);
         this.Controls.Add(this.splitContainer1);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "WndFrmRunLog";
         this.Text = "运行日志查询";
         this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
         this.Load += new System.EventHandler(this.WndFrmRunLog_Load);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.panel2.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.ImageList imageList1;
      private System.Windows.Forms.TableLayoutPanel panel1;
      private System.Windows.Forms.TreeView treeFiles;
      private System.Windows.Forms.TableLayoutPanel panel2;
      private System.Windows.Forms.ProgressBar progressBar1;
      private System.Windows.Forms.Label labStatus;
      private System.Windows.Forms.RichTextBox rtbRunLog;
      private System.Windows.Forms.Timer timer1;
   }
}