namespace GJ.Para.Udc.BURNIN
{
    partial class udcOnOff
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
            this.panel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labOnOff = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOnOff = new System.Windows.Forms.TextBox();
            this.labOn = new System.Windows.Forms.Label();
            this.labOff = new System.Windows.Forms.Label();
            this.txtOn = new System.Windows.Forms.TextBox();
            this.txtOff = new System.Windows.Forms.TextBox();
            this.labOnUint = new System.Windows.Forms.Label();
            this.labOffUint = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbVType = new System.Windows.Forms.ComboBox();
            this.chkUnit = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.ColumnCount = 1;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Controls.Add(this.panel2, 0, 1);
            this.panel1.Controls.Add(this.chkUnit, 0, 0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 2;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Size = new System.Drawing.Size(223, 160);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel2.ColumnCount = 3;
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.panel2.Controls.Add(this.labOnOff, 0, 0);
            this.panel2.Controls.Add(this.label2, 2, 0);
            this.panel2.Controls.Add(this.txtOnOff, 1, 0);
            this.panel2.Controls.Add(this.labOn, 0, 1);
            this.panel2.Controls.Add(this.labOff, 0, 2);
            this.panel2.Controls.Add(this.txtOn, 1, 1);
            this.panel2.Controls.Add(this.txtOff, 1, 2);
            this.panel2.Controls.Add(this.labOnUint, 2, 1);
            this.panel2.Controls.Add(this.labOffUint, 2, 2);
            this.panel2.Controls.Add(this.label1, 0, 3);
            this.panel2.Controls.Add(this.cmbVType, 1, 3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 31);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.panel2.RowCount = 4;
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panel2.Size = new System.Drawing.Size(217, 126);
            this.panel2.TabIndex = 4;
            // 
            // labOnOff
            // 
            this.labOnOff.AutoSize = true;
            this.labOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labOnOff.Location = new System.Drawing.Point(5, 7);
            this.labOnOff.Margin = new System.Windows.Forms.Padding(3);
            this.labOnOff.Name = "labOnOff";
            this.labOnOff.Size = new System.Drawing.Size(72, 22);
            this.labOnOff.TabIndex = 0;
            this.labOnOff.Text = "ONOFF1循环:";
            this.labOnOff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(169, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "次数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOnOff
            // 
            this.txtOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOnOff.Location = new System.Drawing.Point(84, 7);
            this.txtOnOff.Name = "txtOnOff";
            this.txtOnOff.Size = new System.Drawing.Size(78, 21);
            this.txtOnOff.TabIndex = 2;
            this.txtOnOff.Text = "0";
            this.txtOnOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labOn
            // 
            this.labOn.AutoSize = true;
            this.labOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labOn.Location = new System.Drawing.Point(5, 36);
            this.labOn.Margin = new System.Windows.Forms.Padding(3);
            this.labOn.Name = "labOn";
            this.labOn.Size = new System.Drawing.Size(72, 22);
            this.labOn.TabIndex = 3;
            this.labOn.Text = "ON1时间:";
            this.labOn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labOff
            // 
            this.labOff.AutoSize = true;
            this.labOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labOff.Location = new System.Drawing.Point(5, 65);
            this.labOff.Margin = new System.Windows.Forms.Padding(3);
            this.labOff.Name = "labOff";
            this.labOff.Size = new System.Drawing.Size(72, 22);
            this.labOff.TabIndex = 4;
            this.labOff.Text = "OFF1时间:";
            this.labOff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOn
            // 
            this.txtOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOn.Location = new System.Drawing.Point(84, 36);
            this.txtOn.Name = "txtOn";
            this.txtOn.Size = new System.Drawing.Size(78, 21);
            this.txtOn.TabIndex = 5;
            this.txtOn.Text = "0";
            this.txtOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOff
            // 
            this.txtOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOff.Location = new System.Drawing.Point(84, 65);
            this.txtOff.Name = "txtOff";
            this.txtOff.Size = new System.Drawing.Size(78, 21);
            this.txtOff.TabIndex = 6;
            this.txtOff.Text = "0";
            this.txtOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labOnUint
            // 
            this.labOnUint.AutoSize = true;
            this.labOnUint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labOnUint.Location = new System.Drawing.Point(169, 33);
            this.labOnUint.Name = "labOnUint";
            this.labOnUint.Size = new System.Drawing.Size(43, 28);
            this.labOnUint.TabIndex = 7;
            this.labOnUint.Text = "分钟";
            this.labOnUint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labOffUint
            // 
            this.labOffUint.AutoSize = true;
            this.labOffUint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labOffUint.Location = new System.Drawing.Point(169, 62);
            this.labOffUint.Name = "labOffUint";
            this.labOffUint.Size = new System.Drawing.Size(43, 28);
            this.labOffUint.TabIndex = 8;
            this.labOffUint.Text = "分钟";
            this.labOffUint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(5, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "快充电压:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbVType
            // 
            this.cmbVType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbVType.FormattingEnabled = true;
            this.cmbVType.Location = new System.Drawing.Point(84, 94);
            this.cmbVType.Name = "cmbVType";
            this.cmbVType.Size = new System.Drawing.Size(78, 22);
            this.cmbVType.TabIndex = 10;
            // 
            // chkUnit
            // 
            this.chkUnit.AutoSize = true;
            this.chkUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkUnit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkUnit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkUnit.Location = new System.Drawing.Point(40, 3);
            this.chkUnit.Margin = new System.Windows.Forms.Padding(40, 3, 3, 3);
            this.chkUnit.Name = "chkUnit";
            this.chkUnit.Padding = new System.Windows.Forms.Padding(50, 1, 1, 1);
            this.chkUnit.Size = new System.Drawing.Size(180, 22);
            this.chkUnit.TabIndex = 0;
            this.chkUnit.Text = "ONOFF1----单位:秒";
            this.chkUnit.UseVisualStyleBackColor = true;
            this.chkUnit.Click += new System.EventHandler(this.chkUnit_Click);
            // 
            // udcOnOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "udcOnOff";
            this.Size = new System.Drawing.Size(223, 160);
            this.Load += new System.EventHandler(this.udcOnOff_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.TableLayoutPanel panel2;
        private System.Windows.Forms.Label labOnOff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOnOff;
        private System.Windows.Forms.Label labOn;
        private System.Windows.Forms.Label labOff;
        private System.Windows.Forms.TextBox txtOn;
        private System.Windows.Forms.TextBox txtOff;
        private System.Windows.Forms.Label labOnUint;
        private System.Windows.Forms.Label labOffUint;
        private System.Windows.Forms.CheckBox chkUnit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbVType;

    }
}
