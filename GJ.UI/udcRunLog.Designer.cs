namespace GJ.UI
{
   partial class udcRunLog
   {
      /// <summary> 
      /// 必需的设计器变量。
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// 清理所有正在使用的资源。
      /// </summary>
      /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region 组件设计器生成的代码

      /// <summary> 
      /// 设计器支持所需的方法 - 不要
      /// 使用代码编辑器修改此方法的内容。
      /// </summary>
      private void InitializeComponent()
      {
          this.panelMain = new System.Windows.Forms.TableLayoutPanel();
          this.labTitle = new System.Windows.Forms.Label();
          this.rtbLog = new System.Windows.Forms.RichTextBox();
          this.SaveLogWorker = new System.ComponentModel.BackgroundWorker();
          this.panelMain.SuspendLayout();
          this.SuspendLayout();
          // 
          // panelMain
          // 
          this.panelMain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panelMain.ColumnCount = 1;
          this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panelMain.Controls.Add(this.labTitle, 0, 0);
          this.panelMain.Controls.Add(this.rtbLog, 0, 1);
          this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panelMain.Location = new System.Drawing.Point(0, 0);
          this.panelMain.Name = "panelMain";
          this.panelMain.RowCount = 2;
          this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
          this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panelMain.Size = new System.Drawing.Size(276, 380);
          this.panelMain.TabIndex = 0;
          // 
          // labTitle
          // 
          this.labTitle.AutoSize = true;
          this.labTitle.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labTitle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labTitle.ForeColor = System.Drawing.Color.Black;
          this.labTitle.Location = new System.Drawing.Point(4, 1);
          this.labTitle.Name = "labTitle";
          this.labTitle.Size = new System.Drawing.Size(268, 28);
          this.labTitle.TabIndex = 0;
          this.labTitle.Text = "运行日志";
          this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          this.labTitle.DoubleClick  += new System.EventHandler(this.labTitle_Click);
          // 
          // rtbLog
          // 
          this.rtbLog.BackColor = System.Drawing.Color.White;
          this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
          this.rtbLog.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.rtbLog.Location = new System.Drawing.Point(4, 33);
          this.rtbLog.Name = "rtbLog";
          this.rtbLog.ReadOnly = true;
          this.rtbLog.Size = new System.Drawing.Size(268, 343);
          this.rtbLog.TabIndex = 1;
          this.rtbLog.Text = "";
          // 
          // udcRunLog
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.panelMain);
          this.DoubleBuffered = true;
          this.Name = "udcRunLog";
          this.Size = new System.Drawing.Size(276, 380);
          this.panelMain.ResumeLayout(false);
          this.panelMain.PerformLayout();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel panelMain;
      private System.Windows.Forms.Label labTitle;
      private System.Windows.Forms.RichTextBox rtbLog;
      private System.ComponentModel.BackgroundWorker SaveLogWorker;
   }
}
