namespace GJ.Para.Udc.TURNON
{
    partial class udcTURNON
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
            this.panel5 = new System.Windows.Forms.TableLayoutPanel();
            this.labStatus = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLft = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labTTNum = new System.Windows.Forms.Label();
            this.labFailNum = new System.Windows.Forms.Label();
            this.labTestTimes = new System.Windows.Forms.Label();
            this.labFailRate = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labConnectTimes = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.labNameID = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labVersion = new System.Windows.Forms.Label();
            this.labCustom = new System.Windows.Forms.Label();
            this.labModel = new System.Windows.Forms.Label();
            this.labVSpec = new System.Windows.Forms.Label();
            this.labISpec = new System.Windows.Forms.Label();
            this.labIDSpec = new System.Windows.Forms.Label();
            this.btnClrNum = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.ColumnCount = 1;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Controls.Add(this.panel5, 0, 1);
            this.panel1.Controls.Add(this.panel3, 0, 0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 2;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.panel1.Size = new System.Drawing.Size(817, 668);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel5.ColumnCount = 2;
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 204F));
            this.panel5.Controls.Add(this.labStatus, 0, 0);
            this.panel5.Controls.Add(this.panel2, 1, 0);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 404);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.RowCount = 1;
            this.panel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel5.Size = new System.Drawing.Size(809, 260);
            this.panel5.TabIndex = 2;
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.BackColor = System.Drawing.Color.White;
            this.labStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labStatus.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labStatus.ForeColor = System.Drawing.Color.Blue;
            this.labStatus.Location = new System.Drawing.Point(5, 5);
            this.labStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(594, 250);
            this.labStatus.TabIndex = 2;
            this.labStatus.Text = "Idle";
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnLft);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(608, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(196, 250);
            this.panel2.TabIndex = 3;
            // 
            // btnLft
            // 
            this.btnLft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLft.ImageKey = "Debug";
            this.btnLft.Location = new System.Drawing.Point(1, 4);
            this.btnLft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLft.Name = "btnLft";
            this.btnLft.Size = new System.Drawing.Size(194, 66);
            this.btnLft.TabIndex = 0;
            this.btnLft.Text = "调试(&L)";
            this.btnLft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLft.UseVisualStyleBackColor = true;
            this.btnLft.Click += new System.EventHandler(this.btnLft_Click);
            // 
            // panel3
            // 
            this.panel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel3.ColumnCount = 4;
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel3.Controls.Add(this.label9, 0, 0);
            this.panel3.Controls.Add(this.label10, 0, 1);
            this.panel3.Controls.Add(this.label12, 2, 0);
            this.panel3.Controls.Add(this.labTTNum, 1, 0);
            this.panel3.Controls.Add(this.labFailNum, 1, 1);
            this.panel3.Controls.Add(this.labTestTimes, 3, 0);
            this.panel3.Controls.Add(this.labFailRate, 3, 1);
            this.panel3.Controls.Add(this.label11, 2, 1);
            this.panel3.Controls.Add(this.label3, 0, 2);
            this.panel3.Controls.Add(this.labConnectTimes, 1, 2);
            this.panel3.Controls.Add(this.btnSelect, 3, 2);
            this.panel3.Controls.Add(this.label16, 2, 3);
            this.panel3.Controls.Add(this.label15, 2, 4);
            this.panel3.Controls.Add(this.labNameID, 2, 5);
            this.panel3.Controls.Add(this.label7, 0, 3);
            this.panel3.Controls.Add(this.label6, 0, 5);
            this.panel3.Controls.Add(this.label13, 0, 4);
            this.panel3.Controls.Add(this.labVersion, 1, 5);
            this.panel3.Controls.Add(this.labCustom, 1, 4);
            this.panel3.Controls.Add(this.labModel, 1, 3);
            this.panel3.Controls.Add(this.labVSpec, 3, 4);
            this.panel3.Controls.Add(this.labISpec, 3, 3);
            this.panel3.Controls.Add(this.labIDSpec, 3, 5);
            this.panel3.Controls.Add(this.btnClrNum, 2, 2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.RowCount = 6;
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.panel3.Size = new System.Drawing.Size(809, 392);
            this.panel3.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(5, 1);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 64);
            this.label9.TabIndex = 0;
            this.label9.Text = "产品测试总数:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(5, 66);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 64);
            this.label10.TabIndex = 1;
            this.label10.Text = "产品测试不良数:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(422, 1);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 64);
            this.label12.TabIndex = 3;
            this.label12.Text = "测试时间(S):";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTTNum
            // 
            this.labTTNum.AutoSize = true;
            this.labTTNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTTNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTTNum.Location = new System.Drawing.Point(146, 1);
            this.labTTNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTTNum.Name = "labTTNum";
            this.labTTNum.Size = new System.Drawing.Size(267, 64);
            this.labTTNum.TabIndex = 5;
            this.labTTNum.Text = "0";
            this.labTTNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labFailNum
            // 
            this.labFailNum.AutoSize = true;
            this.labFailNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labFailNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFailNum.ForeColor = System.Drawing.Color.Red;
            this.labFailNum.Location = new System.Drawing.Point(146, 66);
            this.labFailNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFailNum.Name = "labFailNum";
            this.labFailNum.Size = new System.Drawing.Size(267, 64);
            this.labFailNum.TabIndex = 6;
            this.labFailNum.Text = "0";
            this.labFailNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labTestTimes
            // 
            this.labTestTimes.AutoSize = true;
            this.labTestTimes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTestTimes.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTestTimes.Location = new System.Drawing.Point(536, 1);
            this.labTestTimes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTestTimes.Name = "labTestTimes";
            this.labTestTimes.Size = new System.Drawing.Size(268, 64);
            this.labTestTimes.TabIndex = 8;
            this.labTestTimes.Text = "0";
            this.labTestTimes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labFailRate
            // 
            this.labFailRate.AutoSize = true;
            this.labFailRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labFailRate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFailRate.ForeColor = System.Drawing.Color.Red;
            this.labFailRate.Location = new System.Drawing.Point(536, 66);
            this.labFailRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFailRate.Name = "labFailRate";
            this.labFailRate.Size = new System.Drawing.Size(268, 64);
            this.labFailRate.TabIndex = 7;
            this.labFailRate.Text = "0.0%";
            this.labFailRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(422, 66);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 64);
            this.label11.TabIndex = 2;
            this.label11.Text = "产品不良率:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(5, 131);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 64);
            this.label3.TabIndex = 12;
            this.label3.Text = "连接器使用次数:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labConnectTimes
            // 
            this.labConnectTimes.AutoSize = true;
            this.labConnectTimes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labConnectTimes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labConnectTimes.Location = new System.Drawing.Point(146, 131);
            this.labConnectTimes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labConnectTimes.Name = "labConnectTimes";
            this.labConnectTimes.Size = new System.Drawing.Size(267, 64);
            this.labConnectTimes.TabIndex = 13;
            this.labConnectTimes.Text = "0";
            this.labConnectTimes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labConnectTimes.DoubleClick += new System.EventHandler(this.labConnectTimes_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(536, 135);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(240, 32);
            this.btnSelect.TabIndex = 14;
            this.btnSelect.Text = "选机种";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(422, 196);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 64);
            this.label16.TabIndex = 19;
            this.label16.Text = "负载设置:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(422, 261);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 64);
            this.label15.TabIndex = 18;
            this.label15.Text = "输出规格:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labNameID
            // 
            this.labNameID.AutoSize = true;
            this.labNameID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labNameID.Location = new System.Drawing.Point(422, 326);
            this.labNameID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labNameID.Name = "labNameID";
            this.labNameID.Size = new System.Drawing.Size(105, 65);
            this.labNameID.TabIndex = 20;
            this.labNameID.Text = "ID规格:";
            this.labNameID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(5, 196);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 64);
            this.label7.TabIndex = 15;
            this.label7.Text = "机种名称:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(5, 326);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 65);
            this.label6.TabIndex = 17;
            this.label6.Text = "机种版本:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(5, 261);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(132, 64);
            this.label13.TabIndex = 16;
            this.label13.Text = "机种客户:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labVersion
            // 
            this.labVersion.AutoSize = true;
            this.labVersion.BackColor = System.Drawing.Color.White;
            this.labVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labVersion.Location = new System.Drawing.Point(146, 330);
            this.labVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(267, 57);
            this.labVersion.TabIndex = 23;
            this.labVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labCustom
            // 
            this.labCustom.AutoSize = true;
            this.labCustom.BackColor = System.Drawing.Color.White;
            this.labCustom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labCustom.Location = new System.Drawing.Point(146, 265);
            this.labCustom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labCustom.Name = "labCustom";
            this.labCustom.Size = new System.Drawing.Size(267, 56);
            this.labCustom.TabIndex = 22;
            this.labCustom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labModel
            // 
            this.labModel.AutoSize = true;
            this.labModel.BackColor = System.Drawing.Color.White;
            this.labModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labModel.Location = new System.Drawing.Point(146, 200);
            this.labModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labModel.Name = "labModel";
            this.labModel.Size = new System.Drawing.Size(267, 56);
            this.labModel.TabIndex = 21;
            this.labModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labVSpec
            // 
            this.labVSpec.AutoSize = true;
            this.labVSpec.BackColor = System.Drawing.Color.White;
            this.labVSpec.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labVSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labVSpec.Location = new System.Drawing.Point(536, 265);
            this.labVSpec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labVSpec.Name = "labVSpec";
            this.labVSpec.Size = new System.Drawing.Size(268, 56);
            this.labVSpec.TabIndex = 25;
            this.labVSpec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labISpec
            // 
            this.labISpec.AutoSize = true;
            this.labISpec.BackColor = System.Drawing.Color.White;
            this.labISpec.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labISpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labISpec.Location = new System.Drawing.Point(536, 200);
            this.labISpec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labISpec.Name = "labISpec";
            this.labISpec.Size = new System.Drawing.Size(268, 56);
            this.labISpec.TabIndex = 26;
            this.labISpec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labIDSpec
            // 
            this.labIDSpec.AutoSize = true;
            this.labIDSpec.BackColor = System.Drawing.Color.White;
            this.labIDSpec.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labIDSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labIDSpec.Location = new System.Drawing.Point(536, 330);
            this.labIDSpec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labIDSpec.Name = "labIDSpec";
            this.labIDSpec.Size = new System.Drawing.Size(268, 57);
            this.labIDSpec.TabIndex = 24;
            this.labIDSpec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClrNum
            // 
            this.btnClrNum.Location = new System.Drawing.Point(422, 135);
            this.btnClrNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClrNum.Name = "btnClrNum";
            this.btnClrNum.Size = new System.Drawing.Size(105, 32);
            this.btnClrNum.TabIndex = 27;
            this.btnClrNum.Text = "清除统计";
            this.btnClrNum.UseVisualStyleBackColor = true;
            this.btnClrNum.Click += new System.EventHandler(this.btnClrNum_Click_1);
            // 
            // udcTURNON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "udcTURNON";
            this.Size = new System.Drawing.Size(817, 668);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.TableLayoutPanel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labTTNum;
        private System.Windows.Forms.Label labFailNum;
        private System.Windows.Forms.Label labFailRate;
        private System.Windows.Forms.Label labTestTimes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labConnectTimes;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labNameID;
        private System.Windows.Forms.Label labVersion;
        private System.Windows.Forms.Label labCustom;
        private System.Windows.Forms.Label labModel;
        private System.Windows.Forms.Label labVSpec;
        private System.Windows.Forms.Label labIDSpec;
        private System.Windows.Forms.Label labISpec;
        private System.Windows.Forms.Button btnClrNum;
        private System.Windows.Forms.TableLayoutPanel panel5;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLft;
    }
}
