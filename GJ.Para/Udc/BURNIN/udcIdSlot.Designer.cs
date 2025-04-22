namespace GJ.Para.Udc.BURNIN
{
    partial class udcIdSlot
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
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labId1 = new System.Windows.Forms.Label();
            this.labId2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.ColumnCount = 2;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel1.Controls.Add(this.labId1, 0, 0);
            this.panel1.Controls.Add(this.labId2, 1, 0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 1;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Size = new System.Drawing.Size(160, 28);
            this.panel1.TabIndex = 0;
            // 
            // labId1
            // 
            this.labId1.AutoSize = true;
            this.labId1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labId1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labId1.Location = new System.Drawing.Point(0, 0);
            this.labId1.Margin = new System.Windows.Forms.Padding(0);
            this.labId1.Name = "labId1";
            this.labId1.Size = new System.Drawing.Size(80, 28);
            this.labId1.TabIndex = 0;
            this.labId1.Text = "01";
            this.labId1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labId2
            // 
            this.labId2.AutoSize = true;
            this.labId2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labId2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labId2.Location = new System.Drawing.Point(80, 0);
            this.labId2.Margin = new System.Windows.Forms.Padding(0);
            this.labId2.Name = "labId2";
            this.labId2.Size = new System.Drawing.Size(80, 28);
            this.labId2.TabIndex = 1;
            this.labId2.Text = "01";
            this.labId2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // udcIdSlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "udcIdSlot";
            this.Size = new System.Drawing.Size(160, 28);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.Label labId1;
        private System.Windows.Forms.Label labId2;
    }
}
