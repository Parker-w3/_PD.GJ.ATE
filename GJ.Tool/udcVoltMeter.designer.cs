namespace GJ.Tool
{
    partial class udcVoltMeter
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
         if (comMon != null)
         {
            comMon.close();
            comMon = null;
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
          this.label11 = new System.Windows.Forms.Label();
          this.panel2 = new System.Windows.Forms.TableLayoutPanel();
          this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
          this.panel4 = new System.Windows.Forms.TableLayoutPanel();
          this.label5 = new System.Windows.Forms.Label();
          this.label9 = new System.Windows.Forms.Label();
          this.labCH1 = new System.Windows.Forms.Label();
          this.labCH2 = new System.Windows.Forms.Label();
          this.labCH3 = new System.Windows.Forms.Label();
          this.labCH4 = new System.Windows.Forms.Label();
          this.labVs1 = new System.Windows.Forms.Label();
          this.labVs2 = new System.Windows.Forms.Label();
          this.labVs3 = new System.Windows.Forms.Label();
          this.labVs4 = new System.Windows.Forms.Label();
          this.btnRead = new System.Windows.Forms.Button();
          this.label12 = new System.Windows.Forms.Label();
          this.txtBaud = new System.Windows.Forms.TextBox();
          this.labVersion = new System.Windows.Forms.Label();
          this.labStatus = new System.Windows.Forms.Label();
          this.panel1 = new System.Windows.Forms.TableLayoutPanel();
          this.label1 = new System.Windows.Forms.Label();
          this.cmbCOM = new System.Windows.Forms.ComboBox();
          this.btnOpen = new System.Windows.Forms.Button();
          this.label3 = new System.Windows.Forms.Label();
          this.txtAddr = new System.Windows.Forms.TextBox();
          this.label2 = new System.Windows.Forms.Label();
          this.btnSetAddr = new System.Windows.Forms.Button();
          this.splitContainer1 = new System.Windows.Forms.SplitContainer();
          this.panel2.SuspendLayout();
          this.tableLayoutPanel1.SuspendLayout();
          this.panel4.SuspendLayout();
          this.panel1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
          this.splitContainer1.Panel1.SuspendLayout();
          this.splitContainer1.Panel2.SuspendLayout();
          this.splitContainer1.SuspendLayout();
          this.SuspendLayout();
          // 
          // label11
          // 
          this.label11.AutoSize = true;
          this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label11.Location = new System.Drawing.Point(382, 31);
          this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label11.Name = "label11";
          this.label11.Size = new System.Drawing.Size(81, 29);
          this.label11.TabIndex = 12;
          this.label11.Text = "通信时间:";
          this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // panel2
          // 
          this.panel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel2.ColumnCount = 1;
          this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
          this.panel2.Controls.Add(this.btnRead, 0, 0);
          this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel2.Location = new System.Drawing.Point(0, 0);
          this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel2.Name = "panel2";
          this.panel2.RowCount = 3;
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 539F));
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
          this.panel2.Size = new System.Drawing.Size(1212, 680);
          this.panel2.TabIndex = 0;
          // 
          // tableLayoutPanel1
          // 
          this.tableLayoutPanel1.ColumnCount = 2;
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.9434F));
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.0566F));
          this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
          this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 50);
          this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.tableLayoutPanel1.Name = "tableLayoutPanel1";
          this.tableLayoutPanel1.RowCount = 1;
          this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
          this.tableLayoutPanel1.Size = new System.Drawing.Size(1202, 531);
          this.tableLayoutPanel1.TabIndex = 4;
          // 
          // panel4
          // 
          this.panel4.BackColor = System.Drawing.SystemColors.Control;
          this.panel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel4.ColumnCount = 2;
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
          this.panel4.Controls.Add(this.label5, 0, 0);
          this.panel4.Controls.Add(this.label9, 1, 0);
          this.panel4.Controls.Add(this.labCH1, 0, 1);
          this.panel4.Controls.Add(this.labCH2, 0, 2);
          this.panel4.Controls.Add(this.labCH3, 0, 3);
          this.panel4.Controls.Add(this.labCH4, 0, 4);
          this.panel4.Controls.Add(this.labVs1, 1, 1);
          this.panel4.Controls.Add(this.labVs2, 1, 2);
          this.panel4.Controls.Add(this.labVs3, 1, 3);
          this.panel4.Controls.Add(this.labVs4, 1, 4);
          this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel4.Location = new System.Drawing.Point(4, 4);
          this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel4.Name = "panel4";
          this.panel4.RowCount = 13;
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel4.Size = new System.Drawing.Size(604, 523);
          this.panel4.TabIndex = 5;
          // 
          // label5
          // 
          this.label5.AutoSize = true;
          this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label5.Location = new System.Drawing.Point(5, 1);
          this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(69, 35);
          this.label5.TabIndex = 0;
          this.label5.Text = "通道";
          this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label9
          // 
          this.label9.AutoSize = true;
          this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label9.Location = new System.Drawing.Point(83, 1);
          this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label9.Name = "label9";
          this.label9.Size = new System.Drawing.Size(516, 35);
          this.label9.TabIndex = 4;
          this.label9.Text = "电压(V)";
          this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCH1
          // 
          this.labCH1.AutoSize = true;
          this.labCH1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCH1.Location = new System.Drawing.Point(5, 37);
          this.labCH1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labCH1.Name = "labCH1";
          this.labCH1.Size = new System.Drawing.Size(69, 35);
          this.labCH1.TabIndex = 7;
          this.labCH1.Text = "CH01:";
          this.labCH1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCH2
          // 
          this.labCH2.AutoSize = true;
          this.labCH2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCH2.Location = new System.Drawing.Point(5, 73);
          this.labCH2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labCH2.Name = "labCH2";
          this.labCH2.Size = new System.Drawing.Size(69, 35);
          this.labCH2.TabIndex = 8;
          this.labCH2.Text = "CH02:";
          this.labCH2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCH3
          // 
          this.labCH3.AutoSize = true;
          this.labCH3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCH3.Location = new System.Drawing.Point(5, 109);
          this.labCH3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labCH3.Name = "labCH3";
          this.labCH3.Size = new System.Drawing.Size(69, 35);
          this.labCH3.TabIndex = 9;
          this.labCH3.Text = "CH03:";
          this.labCH3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCH4
          // 
          this.labCH4.AutoSize = true;
          this.labCH4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCH4.Location = new System.Drawing.Point(5, 145);
          this.labCH4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labCH4.Name = "labCH4";
          this.labCH4.Size = new System.Drawing.Size(69, 36);
          this.labCH4.TabIndex = 10;
          this.labCH4.Text = "CH04:";
          this.labCH4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labVs1
          // 
          this.labVs1.AutoSize = true;
          this.labVs1.BackColor = System.Drawing.Color.White;
          this.labVs1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labVs1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labVs1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labVs1.ForeColor = System.Drawing.Color.Black;
          this.labVs1.Location = new System.Drawing.Point(83, 41);
          this.labVs1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs1.Name = "labVs1";
          this.labVs1.Size = new System.Drawing.Size(516, 27);
          this.labVs1.TabIndex = 39;
          this.labVs1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labVs2
          // 
          this.labVs2.AutoSize = true;
          this.labVs2.BackColor = System.Drawing.Color.White;
          this.labVs2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labVs2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labVs2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labVs2.ForeColor = System.Drawing.Color.Black;
          this.labVs2.Location = new System.Drawing.Point(83, 77);
          this.labVs2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs2.Name = "labVs2";
          this.labVs2.Size = new System.Drawing.Size(516, 27);
          this.labVs2.TabIndex = 40;
          this.labVs2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labVs3
          // 
          this.labVs3.AutoSize = true;
          this.labVs3.BackColor = System.Drawing.Color.White;
          this.labVs3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labVs3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labVs3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labVs3.ForeColor = System.Drawing.Color.Black;
          this.labVs3.Location = new System.Drawing.Point(83, 113);
          this.labVs3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs3.Name = "labVs3";
          this.labVs3.Size = new System.Drawing.Size(516, 27);
          this.labVs3.TabIndex = 41;
          this.labVs3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labVs4
          // 
          this.labVs4.AutoSize = true;
          this.labVs4.BackColor = System.Drawing.Color.White;
          this.labVs4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labVs4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labVs4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labVs4.ForeColor = System.Drawing.Color.Black;
          this.labVs4.Location = new System.Drawing.Point(83, 149);
          this.labVs4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs4.Name = "labVs4";
          this.labVs4.Size = new System.Drawing.Size(516, 28);
          this.labVs4.TabIndex = 42;
          this.labVs4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // btnRead
          // 
          this.btnRead.Location = new System.Drawing.Point(4, 3);
          this.btnRead.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
          this.btnRead.Name = "btnRead";
          this.btnRead.Size = new System.Drawing.Size(221, 38);
          this.btnRead.TabIndex = 16;
          this.btnRead.Text = "读电压";
          this.btnRead.UseVisualStyleBackColor = true;
          this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
          // 
          // label12
          // 
          this.label12.AutoSize = true;
          this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label12.Location = new System.Drawing.Point(5, 61);
          this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label12.Name = "label12";
          this.label12.Size = new System.Drawing.Size(99, 29);
          this.label12.TabIndex = 13;
          this.label12.Text = "波特率:";
          this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // txtBaud
          // 
          this.txtBaud.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtBaud.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.txtBaud.Location = new System.Drawing.Point(113, 65);
          this.txtBaud.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtBaud.Name = "txtBaud";
          this.txtBaud.Size = new System.Drawing.Size(152, 27);
          this.txtBaud.TabIndex = 14;
          this.txtBaud.Text = "9600,N,8,1";
          // 
          // labVersion
          // 
          this.labVersion.AutoSize = true;
          this.labVersion.Dock = System.Windows.Forms.DockStyle.Left;
          this.labVersion.Location = new System.Drawing.Point(472, 31);
          this.labVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labVersion.Name = "labVersion";
          this.labVersion.Size = new System.Drawing.Size(0, 29);
          this.labVersion.TabIndex = 11;
          this.labVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labStatus
          // 
          this.labStatus.AutoSize = true;
          this.labStatus.Dock = System.Windows.Forms.DockStyle.Left;
          this.labStatus.Location = new System.Drawing.Point(472, 1);
          this.labStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labStatus.Name = "labStatus";
          this.labStatus.Size = new System.Drawing.Size(0, 29);
          this.labStatus.TabIndex = 9;
          this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // panel1
          // 
          this.panel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel1.ColumnCount = 5;
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 843F));
          this.panel1.Controls.Add(this.label1, 0, 0);
          this.panel1.Controls.Add(this.cmbCOM, 1, 0);
          this.panel1.Controls.Add(this.btnOpen, 2, 0);
          this.panel1.Controls.Add(this.label3, 0, 1);
          this.panel1.Controls.Add(this.txtAddr, 1, 1);
          this.panel1.Controls.Add(this.label2, 3, 0);
          this.panel1.Controls.Add(this.labStatus, 4, 0);
          this.panel1.Controls.Add(this.labVersion, 4, 1);
          this.panel1.Controls.Add(this.label11, 3, 1);
          this.panel1.Controls.Add(this.label12, 0, 2);
          this.panel1.Controls.Add(this.txtBaud, 1, 2);
          this.panel1.Controls.Add(this.btnSetAddr, 2, 1);
          this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel1.Location = new System.Drawing.Point(0, 0);
          this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel1.Name = "panel1";
          this.panel1.RowCount = 3;
          this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
          this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
          this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
          this.panel1.Size = new System.Drawing.Size(1212, 91);
          this.panel1.TabIndex = 1;
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label1.Location = new System.Drawing.Point(5, 1);
          this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(99, 29);
          this.label1.TabIndex = 0;
          this.label1.Text = "串口编号:";
          this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // cmbCOM
          // 
          this.cmbCOM.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cmbCOM.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.cmbCOM.FormattingEnabled = true;
          this.cmbCOM.Location = new System.Drawing.Point(113, 5);
          this.cmbCOM.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.cmbCOM.Name = "cmbCOM";
          this.cmbCOM.Size = new System.Drawing.Size(152, 25);
          this.cmbCOM.TabIndex = 1;
          // 
          // btnOpen
          // 
          this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnOpen.Location = new System.Drawing.Point(270, 1);
          this.btnOpen.Margin = new System.Windows.Forms.Padding(0);
          this.btnOpen.Name = "btnOpen";
          this.btnOpen.Size = new System.Drawing.Size(107, 29);
          this.btnOpen.TabIndex = 2;
          this.btnOpen.Text = "打开";
          this.btnOpen.UseVisualStyleBackColor = true;
          this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label3.Location = new System.Drawing.Point(5, 31);
          this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(99, 29);
          this.label3.TabIndex = 5;
          this.label3.Text = "模块地址:";
          this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // txtAddr
          // 
          this.txtAddr.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtAddr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.txtAddr.Location = new System.Drawing.Point(113, 35);
          this.txtAddr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtAddr.Name = "txtAddr";
          this.txtAddr.Size = new System.Drawing.Size(152, 27);
          this.txtAddr.TabIndex = 6;
          this.txtAddr.Text = "1";
          this.txtAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label2.Location = new System.Drawing.Point(382, 1);
          this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(81, 29);
          this.label2.TabIndex = 8;
          this.label2.Text = "状态指示:";
          this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // btnSetAddr
          // 
          this.btnSetAddr.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnSetAddr.Location = new System.Drawing.Point(270, 31);
          this.btnSetAddr.Margin = new System.Windows.Forms.Padding(0);
          this.btnSetAddr.Name = "btnSetAddr";
          this.btnSetAddr.Size = new System.Drawing.Size(107, 29);
          this.btnSetAddr.TabIndex = 15;
          this.btnSetAddr.Text = "设地址";
          this.btnSetAddr.UseVisualStyleBackColor = true;
          // 
          // splitContainer1
          // 
          this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
          this.splitContainer1.Location = new System.Drawing.Point(0, 0);
          this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.splitContainer1.Name = "splitContainer1";
          this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
          // 
          // splitContainer1.Panel1
          // 
          this.splitContainer1.Panel1.Controls.Add(this.panel1);
          // 
          // splitContainer1.Panel2
          // 
          this.splitContainer1.Panel2.Controls.Add(this.panel2);
          this.splitContainer1.Size = new System.Drawing.Size(1212, 776);
          this.splitContainer1.SplitterDistance = 91;
          this.splitContainer1.SplitterWidth = 5;
          this.splitContainer1.TabIndex = 1;
          // 
          // udcVoltMeter
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.splitContainer1);
          this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.Name = "udcVoltMeter";
          this.Size = new System.Drawing.Size(1212, 776);
          this.Load += new System.EventHandler(this.udcELoad_Load);
          this.panel2.ResumeLayout(false);
          this.tableLayoutPanel1.ResumeLayout(false);
          this.panel4.ResumeLayout(false);
          this.panel4.PerformLayout();
          this.panel1.ResumeLayout(false);
          this.panel1.PerformLayout();
          this.splitContainer1.Panel1.ResumeLayout(false);
          this.splitContainer1.Panel2.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
          this.splitContainer1.ResumeLayout(false);
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.TableLayoutPanel panel2;
      private System.Windows.Forms.Label label12;
      private System.Windows.Forms.TextBox txtBaud;
      private System.Windows.Forms.Label labVersion;
      private System.Windows.Forms.Label labStatus;
      private System.Windows.Forms.TableLayoutPanel panel1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ComboBox cmbCOM;
      private System.Windows.Forms.Button btnOpen;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox txtAddr;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.Button btnSetAddr;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private System.Windows.Forms.TableLayoutPanel panel4;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label labCH1;
      private System.Windows.Forms.Label labCH2;
      private System.Windows.Forms.Label labCH3;
      private System.Windows.Forms.Label labCH4;
      private System.Windows.Forms.Label labVs1;
      private System.Windows.Forms.Label labVs2;
      private System.Windows.Forms.Label labVs3;
      private System.Windows.Forms.Label labVs4;
      private System.Windows.Forms.Button btnRead;
   }
}
