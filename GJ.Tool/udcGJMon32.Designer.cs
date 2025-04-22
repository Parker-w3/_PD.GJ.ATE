namespace GJ.Tool
{
   partial class udcGJMon32
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
          this.splitContainer1 = new System.Windows.Forms.SplitContainer();
          this.panel1 = new System.Windows.Forms.TableLayoutPanel();
          this.label1 = new System.Windows.Forms.Label();
          this.cmbCOM = new System.Windows.Forms.ComboBox();
          this.btnOpen = new System.Windows.Forms.Button();
          this.label3 = new System.Windows.Forms.Label();
          this.txtAddr = new System.Windows.Forms.TextBox();
          this.btnSet = new System.Windows.Forms.Button();
          this.label2 = new System.Windows.Forms.Label();
          this.labStatus = new System.Windows.Forms.Label();
          this.label4 = new System.Windows.Forms.Label();
          this.labVersion = new System.Windows.Forms.Label();
          this.panel2 = new System.Windows.Forms.TableLayoutPanel();
          this.panel3 = new System.Windows.Forms.TableLayoutPanel();
          this.labV12 = new System.Windows.Forms.Label();
          this.label61 = new System.Windows.Forms.Label();
          this.labV16 = new System.Windows.Forms.Label();
          this.label54 = new System.Windows.Forms.Label();
          this.labV4 = new System.Windows.Forms.Label();
          this.label52 = new System.Windows.Forms.Label();
          this.labV8 = new System.Windows.Forms.Label();
          this.label50 = new System.Windows.Forms.Label();
          this.labV28 = new System.Windows.Forms.Label();
          this.label48 = new System.Windows.Forms.Label();
          this.labV32 = new System.Windows.Forms.Label();
          this.label46 = new System.Windows.Forms.Label();
          this.labV20 = new System.Windows.Forms.Label();
          this.label44 = new System.Windows.Forms.Label();
          this.labV24 = new System.Windows.Forms.Label();
          this.label42 = new System.Windows.Forms.Label();
          this.label33 = new System.Windows.Forms.Label();
          this.labV15 = new System.Windows.Forms.Label();
          this.label31 = new System.Windows.Forms.Label();
          this.labV19 = new System.Windows.Forms.Label();
          this.labV23 = new System.Windows.Forms.Label();
          this.label28 = new System.Windows.Forms.Label();
          this.label27 = new System.Windows.Forms.Label();
          this.labV3 = new System.Windows.Forms.Label();
          this.labV11 = new System.Windows.Forms.Label();
          this.label24 = new System.Windows.Forms.Label();
          this.label23 = new System.Windows.Forms.Label();
          this.labV7 = new System.Windows.Forms.Label();
          this.labV6 = new System.Windows.Forms.Label();
          this.label20 = new System.Windows.Forms.Label();
          this.labV2 = new System.Windows.Forms.Label();
          this.label18 = new System.Windows.Forms.Label();
          this.labV21 = new System.Windows.Forms.Label();
          this.label16 = new System.Windows.Forms.Label();
          this.labV17 = new System.Windows.Forms.Label();
          this.label14 = new System.Windows.Forms.Label();
          this.labV13 = new System.Windows.Forms.Label();
          this.label12 = new System.Windows.Forms.Label();
          this.labV9 = new System.Windows.Forms.Label();
          this.label10 = new System.Windows.Forms.Label();
          this.labV5 = new System.Windows.Forms.Label();
          this.label8 = new System.Windows.Forms.Label();
          this.labV1 = new System.Windows.Forms.Label();
          this.label6 = new System.Windows.Forms.Label();
          this.label34 = new System.Windows.Forms.Label();
          this.labV10 = new System.Windows.Forms.Label();
          this.label36 = new System.Windows.Forms.Label();
          this.labV14 = new System.Windows.Forms.Label();
          this.label38 = new System.Windows.Forms.Label();
          this.labV18 = new System.Windows.Forms.Label();
          this.label40 = new System.Windows.Forms.Label();
          this.labV22 = new System.Windows.Forms.Label();
          this.label5 = new System.Windows.Forms.Label();
          this.label56 = new System.Windows.Forms.Label();
          this.label57 = new System.Windows.Forms.Label();
          this.labV25 = new System.Windows.Forms.Label();
          this.labV26 = new System.Windows.Forms.Label();
          this.labV27 = new System.Windows.Forms.Label();
          this.label63 = new System.Windows.Forms.Label();
          this.label64 = new System.Windows.Forms.Label();
          this.label65 = new System.Windows.Forms.Label();
          this.labV29 = new System.Windows.Forms.Label();
          this.labV30 = new System.Windows.Forms.Label();
          this.labV31 = new System.Windows.Forms.Label();
          this.panel4 = new System.Windows.Forms.TableLayoutPanel();
          this.label69 = new System.Windows.Forms.Label();
          this.btnVer = new System.Windows.Forms.Button();
          this.btnVolt = new System.Windows.Forms.Button();
          this.chkSync = new System.Windows.Forms.CheckBox();
          this.label70 = new System.Windows.Forms.Label();
          this.labOnOff = new System.Windows.Forms.Label();
          this.chkMode = new System.Windows.Forms.CheckBox();
          this.btnScan = new System.Windows.Forms.Button();
          this.label62 = new System.Windows.Forms.Label();
          this.txtEndAddr = new System.Windows.Forms.TextBox();
          this.panel10 = new System.Windows.Forms.TableLayoutPanel();
          this.gridMon = new System.Windows.Forms.DataGridView();
          this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.panel11 = new System.Windows.Forms.TableLayoutPanel();
          this.panel9 = new System.Windows.Forms.TableLayoutPanel();
          this.label59 = new System.Windows.Forms.Label();
          this.labErrCode = new System.Windows.Forms.Label();
          this.label60 = new System.Windows.Forms.Label();
          this.txtRelayNo = new System.Windows.Forms.TextBox();
          this.btnRlyON = new System.Windows.Forms.Button();
          this.btnRlyOff = new System.Windows.Forms.Button();
          this.panel7 = new System.Windows.Forms.TableLayoutPanel();
          this.label17 = new System.Windows.Forms.Label();
          this.btnStart = new System.Windows.Forms.Button();
          this.btnReadSgn = new System.Windows.Forms.Button();
          this.btnStop = new System.Windows.Forms.Button();
          this.label25 = new System.Windows.Forms.Label();
          this.label32 = new System.Windows.Forms.Label();
          this.label41 = new System.Windows.Forms.Label();
          this.labX1 = new System.Windows.Forms.Label();
          this.labX2 = new System.Windows.Forms.Label();
          this.labX3 = new System.Windows.Forms.Label();
          this.labX4 = new System.Windows.Forms.Label();
          this.labX5 = new System.Windows.Forms.Label();
          this.labX6 = new System.Windows.Forms.Label();
          this.labX7 = new System.Windows.Forms.Label();
          this.labX8 = new System.Windows.Forms.Label();
          this.labS1 = new System.Windows.Forms.Label();
          this.labS2 = new System.Windows.Forms.Label();
          this.labX9 = new System.Windows.Forms.Label();
          this.panel5 = new System.Windows.Forms.TableLayoutPanel();
          this.label7 = new System.Windows.Forms.Label();
          this.btnSetPara = new System.Windows.Forms.Button();
          this.btnReadPara = new System.Windows.Forms.Button();
          this.panel8 = new System.Windows.Forms.TableLayoutPanel();
          this.label43 = new System.Windows.Forms.Label();
          this.label45 = new System.Windows.Forms.Label();
          this.label47 = new System.Windows.Forms.Label();
          this.label49 = new System.Windows.Forms.Label();
          this.label51 = new System.Windows.Forms.Label();
          this.label53 = new System.Windows.Forms.Label();
          this.label55 = new System.Windows.Forms.Label();
          this.label58 = new System.Windows.Forms.Label();
          this.labRunMin = new System.Windows.Forms.Label();
          this.labRunOnOff = new System.Windows.Forms.Label();
          this.labRunSec = new System.Windows.Forms.Label();
          this.labOnOffTime = new System.Windows.Forms.Label();
          this.labStartFlag = new System.Windows.Forms.Label();
          this.labOnOffCycle = new System.Windows.Forms.Label();
          this.labFinishFlag = new System.Windows.Forms.Label();
          this.labRunFlag = new System.Windows.Forms.Label();
          this.label73 = new System.Windows.Forms.Label();
          this.labAcOn = new System.Windows.Forms.Label();
          this.label75 = new System.Windows.Forms.Label();
          this.label76 = new System.Windows.Forms.Label();
          this.labRelayON = new System.Windows.Forms.Label();
          this.panel6 = new System.Windows.Forms.TableLayoutPanel();
          this.label9 = new System.Windows.Forms.Label();
          this.txtBIToTime = new System.Windows.Forms.TextBox();
          this.label11 = new System.Windows.Forms.Label();
          this.label13 = new System.Windows.Forms.Label();
          this.label15 = new System.Windows.Forms.Label();
          this.label19 = new System.Windows.Forms.Label();
          this.label21 = new System.Windows.Forms.Label();
          this.label22 = new System.Windows.Forms.Label();
          this.label26 = new System.Windows.Forms.Label();
          this.label29 = new System.Windows.Forms.Label();
          this.label30 = new System.Windows.Forms.Label();
          this.label35 = new System.Windows.Forms.Label();
          this.label37 = new System.Windows.Forms.Label();
          this.label39 = new System.Windows.Forms.Label();
          this.txtOnOff1 = new System.Windows.Forms.TextBox();
          this.txtOn1 = new System.Windows.Forms.TextBox();
          this.txtOff1 = new System.Windows.Forms.TextBox();
          this.txtOnOff2 = new System.Windows.Forms.TextBox();
          this.txtOn2 = new System.Windows.Forms.TextBox();
          this.txtOff2 = new System.Windows.Forms.TextBox();
          this.txtOnOff3 = new System.Windows.Forms.TextBox();
          this.txtOn3 = new System.Windows.Forms.TextBox();
          this.txtOff3 = new System.Windows.Forms.TextBox();
          this.txtOnOff4 = new System.Windows.Forms.TextBox();
          this.txtOn4 = new System.Windows.Forms.TextBox();
          this.txtOff4 = new System.Windows.Forms.TextBox();
          ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
          this.splitContainer1.Panel1.SuspendLayout();
          this.splitContainer1.Panel2.SuspendLayout();
          this.splitContainer1.SuspendLayout();
          this.panel1.SuspendLayout();
          this.panel2.SuspendLayout();
          this.panel3.SuspendLayout();
          this.panel4.SuspendLayout();
          this.panel10.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.gridMon)).BeginInit();
          this.panel11.SuspendLayout();
          this.panel9.SuspendLayout();
          this.panel7.SuspendLayout();
          this.panel5.SuspendLayout();
          this.panel8.SuspendLayout();
          this.panel6.SuspendLayout();
          this.SuspendLayout();
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
          this.splitContainer1.Size = new System.Drawing.Size(1379, 845);
          this.splitContainer1.SplitterDistance = 62;
          this.splitContainer1.SplitterWidth = 5;
          this.splitContainer1.TabIndex = 0;
          // 
          // panel1
          // 
          this.panel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel1.ColumnCount = 5;
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1013F));
          this.panel1.Controls.Add(this.label1, 0, 0);
          this.panel1.Controls.Add(this.cmbCOM, 1, 0);
          this.panel1.Controls.Add(this.btnOpen, 2, 0);
          this.panel1.Controls.Add(this.label3, 0, 1);
          this.panel1.Controls.Add(this.txtAddr, 1, 1);
          this.panel1.Controls.Add(this.btnSet, 2, 1);
          this.panel1.Controls.Add(this.label2, 3, 0);
          this.panel1.Controls.Add(this.labStatus, 4, 0);
          this.panel1.Controls.Add(this.label4, 3, 1);
          this.panel1.Controls.Add(this.labVersion, 4, 1);
          this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel1.Location = new System.Drawing.Point(0, 0);
          this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel1.Name = "panel1";
          this.panel1.RowCount = 2;
          this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
          this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
          this.panel1.Size = new System.Drawing.Size(1379, 62);
          this.panel1.TabIndex = 0;
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
          this.cmbCOM.Size = new System.Drawing.Size(112, 25);
          this.cmbCOM.TabIndex = 1;
          // 
          // btnOpen
          // 
          this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnOpen.Location = new System.Drawing.Point(230, 1);
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
          this.label3.Size = new System.Drawing.Size(99, 30);
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
          this.txtAddr.Size = new System.Drawing.Size(112, 27);
          this.txtAddr.TabIndex = 6;
          this.txtAddr.Text = "1";
          this.txtAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // btnSet
          // 
          this.btnSet.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnSet.Location = new System.Drawing.Point(230, 31);
          this.btnSet.Margin = new System.Windows.Forms.Padding(0);
          this.btnSet.Name = "btnSet";
          this.btnSet.Size = new System.Drawing.Size(107, 30);
          this.btnSet.TabIndex = 7;
          this.btnSet.Text = "设地址";
          this.btnSet.UseVisualStyleBackColor = true;
          this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label2.Location = new System.Drawing.Point(342, 1);
          this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(81, 29);
          this.label2.TabIndex = 8;
          this.label2.Text = "状态指示:";
          this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labStatus
          // 
          this.labStatus.AutoSize = true;
          this.labStatus.Dock = System.Windows.Forms.DockStyle.Left;
          this.labStatus.Location = new System.Drawing.Point(432, 1);
          this.labStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labStatus.Name = "labStatus";
          this.labStatus.Size = new System.Drawing.Size(0, 29);
          this.labStatus.TabIndex = 9;
          this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label4
          // 
          this.label4.AutoSize = true;
          this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label4.Location = new System.Drawing.Point(342, 31);
          this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(81, 30);
          this.label4.TabIndex = 10;
          this.label4.Text = "模块版本:";
          this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labVersion
          // 
          this.labVersion.AutoSize = true;
          this.labVersion.Dock = System.Windows.Forms.DockStyle.Left;
          this.labVersion.Location = new System.Drawing.Point(432, 31);
          this.labVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labVersion.Name = "labVersion";
          this.labVersion.Size = new System.Drawing.Size(0, 30);
          this.labVersion.TabIndex = 11;
          this.labVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // panel2
          // 
          this.panel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel2.ColumnCount = 1;
          this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel2.Controls.Add(this.panel3, 0, 1);
          this.panel2.Controls.Add(this.panel4, 0, 0);
          this.panel2.Controls.Add(this.panel10, 0, 2);
          this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel2.Location = new System.Drawing.Point(0, 0);
          this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel2.Name = "panel2";
          this.panel2.RowCount = 4;
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 568F));
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
          this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
          this.panel2.Size = new System.Drawing.Size(1379, 778);
          this.panel2.TabIndex = 0;
          // 
          // panel3
          // 
          this.panel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel3.ColumnCount = 16;
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.247052F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.247052F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250422F));
          this.panel3.Controls.Add(this.labV12, 0, 3);
          this.panel3.Controls.Add(this.label61, 0, 3);
          this.panel3.Controls.Add(this.labV16, 0, 3);
          this.panel3.Controls.Add(this.label54, 0, 3);
          this.panel3.Controls.Add(this.labV4, 0, 3);
          this.panel3.Controls.Add(this.label52, 0, 3);
          this.panel3.Controls.Add(this.labV8, 0, 3);
          this.panel3.Controls.Add(this.label50, 0, 3);
          this.panel3.Controls.Add(this.labV28, 0, 3);
          this.panel3.Controls.Add(this.label48, 0, 3);
          this.panel3.Controls.Add(this.labV32, 0, 3);
          this.panel3.Controls.Add(this.label46, 0, 3);
          this.panel3.Controls.Add(this.labV20, 0, 3);
          this.panel3.Controls.Add(this.label44, 0, 3);
          this.panel3.Controls.Add(this.labV24, 0, 3);
          this.panel3.Controls.Add(this.label42, 0, 3);
          this.panel3.Controls.Add(this.label33, 0, 2);
          this.panel3.Controls.Add(this.labV15, 0, 2);
          this.panel3.Controls.Add(this.label31, 0, 2);
          this.panel3.Controls.Add(this.labV19, 0, 2);
          this.panel3.Controls.Add(this.labV23, 0, 2);
          this.panel3.Controls.Add(this.label28, 0, 2);
          this.panel3.Controls.Add(this.label27, 0, 2);
          this.panel3.Controls.Add(this.labV3, 0, 2);
          this.panel3.Controls.Add(this.labV11, 0, 2);
          this.panel3.Controls.Add(this.label24, 0, 2);
          this.panel3.Controls.Add(this.label23, 0, 2);
          this.panel3.Controls.Add(this.labV7, 0, 2);
          this.panel3.Controls.Add(this.labV6, 3, 1);
          this.panel3.Controls.Add(this.label20, 2, 1);
          this.panel3.Controls.Add(this.labV2, 1, 1);
          this.panel3.Controls.Add(this.label18, 0, 1);
          this.panel3.Controls.Add(this.labV21, 11, 0);
          this.panel3.Controls.Add(this.label16, 10, 0);
          this.panel3.Controls.Add(this.labV17, 9, 0);
          this.panel3.Controls.Add(this.label14, 8, 0);
          this.panel3.Controls.Add(this.labV13, 7, 0);
          this.panel3.Controls.Add(this.label12, 6, 0);
          this.panel3.Controls.Add(this.labV9, 5, 0);
          this.panel3.Controls.Add(this.label10, 4, 0);
          this.panel3.Controls.Add(this.labV5, 3, 0);
          this.panel3.Controls.Add(this.label8, 2, 0);
          this.panel3.Controls.Add(this.labV1, 1, 0);
          this.panel3.Controls.Add(this.label6, 0, 0);
          this.panel3.Controls.Add(this.label34, 4, 1);
          this.panel3.Controls.Add(this.labV10, 5, 1);
          this.panel3.Controls.Add(this.label36, 6, 1);
          this.panel3.Controls.Add(this.labV14, 7, 1);
          this.panel3.Controls.Add(this.label38, 8, 1);
          this.panel3.Controls.Add(this.labV18, 9, 1);
          this.panel3.Controls.Add(this.label40, 10, 1);
          this.panel3.Controls.Add(this.labV22, 11, 1);
          this.panel3.Controls.Add(this.label5, 12, 0);
          this.panel3.Controls.Add(this.label56, 12, 1);
          this.panel3.Controls.Add(this.label57, 12, 2);
          this.panel3.Controls.Add(this.labV25, 13, 0);
          this.panel3.Controls.Add(this.labV26, 13, 1);
          this.panel3.Controls.Add(this.labV27, 13, 2);
          this.panel3.Controls.Add(this.label63, 14, 0);
          this.panel3.Controls.Add(this.label64, 14, 1);
          this.panel3.Controls.Add(this.label65, 14, 2);
          this.panel3.Controls.Add(this.labV29, 15, 0);
          this.panel3.Controls.Add(this.labV30, 15, 1);
          this.panel3.Controls.Add(this.labV31, 15, 2);
          this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel3.Location = new System.Drawing.Point(5, 50);
          this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel3.Name = "panel3";
          this.panel3.RowCount = 4;
          this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
          this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
          this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
          this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
          this.panel3.Size = new System.Drawing.Size(1369, 117);
          this.panel3.TabIndex = 1;
          // 
          // labV12
          // 
          this.labV12.AutoSize = true;
          this.labV12.BackColor = System.Drawing.Color.White;
          this.labV12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV12.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV12.Location = new System.Drawing.Point(430, 88);
          this.labV12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV12.Name = "labV12";
          this.labV12.Size = new System.Drawing.Size(76, 28);
          this.labV12.TabIndex = 57;
          this.labV12.Text = "0.000";
          this.labV12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label61
          // 
          this.label61.AutoSize = true;
          this.label61.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label61.Location = new System.Drawing.Point(345, 88);
          this.label61.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label61.Name = "label61";
          this.label61.Size = new System.Drawing.Size(76, 28);
          this.label61.TabIndex = 56;
          this.label61.Text = "CH12:";
          this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV16
          // 
          this.labV16.AutoSize = true;
          this.labV16.BackColor = System.Drawing.Color.White;
          this.labV16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV16.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV16.Location = new System.Drawing.Point(600, 88);
          this.labV16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV16.Name = "labV16";
          this.labV16.Size = new System.Drawing.Size(76, 28);
          this.labV16.TabIndex = 49;
          this.labV16.Text = "0.000";
          this.labV16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label54
          // 
          this.label54.AutoSize = true;
          this.label54.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label54.Location = new System.Drawing.Point(515, 88);
          this.label54.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label54.Name = "label54";
          this.label54.Size = new System.Drawing.Size(76, 28);
          this.label54.TabIndex = 48;
          this.label54.Text = "CH16:";
          this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV4
          // 
          this.labV4.AutoSize = true;
          this.labV4.BackColor = System.Drawing.Color.White;
          this.labV4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV4.Location = new System.Drawing.Point(90, 88);
          this.labV4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV4.Name = "labV4";
          this.labV4.Size = new System.Drawing.Size(76, 28);
          this.labV4.TabIndex = 47;
          this.labV4.Text = "0.000";
          this.labV4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label52
          // 
          this.label52.AutoSize = true;
          this.label52.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label52.Location = new System.Drawing.Point(5, 88);
          this.label52.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label52.Name = "label52";
          this.label52.Size = new System.Drawing.Size(76, 28);
          this.label52.TabIndex = 46;
          this.label52.Text = "CH04:";
          this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV8
          // 
          this.labV8.AutoSize = true;
          this.labV8.BackColor = System.Drawing.Color.White;
          this.labV8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV8.Location = new System.Drawing.Point(260, 88);
          this.labV8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV8.Name = "labV8";
          this.labV8.Size = new System.Drawing.Size(76, 28);
          this.labV8.TabIndex = 45;
          this.labV8.Text = "0.000";
          this.labV8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label50
          // 
          this.label50.AutoSize = true;
          this.label50.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label50.Location = new System.Drawing.Point(175, 88);
          this.label50.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label50.Name = "label50";
          this.label50.Size = new System.Drawing.Size(76, 28);
          this.label50.TabIndex = 44;
          this.label50.Text = "CH08:";
          this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV28
          // 
          this.labV28.AutoSize = true;
          this.labV28.BackColor = System.Drawing.Color.White;
          this.labV28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV28.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV28.Location = new System.Drawing.Point(1110, 88);
          this.labV28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV28.Name = "labV28";
          this.labV28.Size = new System.Drawing.Size(76, 28);
          this.labV28.TabIndex = 43;
          this.labV28.Text = "0.000";
          this.labV28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label48
          // 
          this.label48.AutoSize = true;
          this.label48.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label48.Location = new System.Drawing.Point(1025, 88);
          this.label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label48.Name = "label48";
          this.label48.Size = new System.Drawing.Size(76, 28);
          this.label48.TabIndex = 42;
          this.label48.Text = "CH28:";
          this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV32
          // 
          this.labV32.AutoSize = true;
          this.labV32.BackColor = System.Drawing.Color.White;
          this.labV32.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV32.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV32.Location = new System.Drawing.Point(1280, 88);
          this.labV32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV32.Name = "labV32";
          this.labV32.Size = new System.Drawing.Size(84, 28);
          this.labV32.TabIndex = 41;
          this.labV32.Text = "0.000";
          this.labV32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label46
          // 
          this.label46.AutoSize = true;
          this.label46.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label46.Location = new System.Drawing.Point(1195, 88);
          this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label46.Name = "label46";
          this.label46.Size = new System.Drawing.Size(76, 28);
          this.label46.TabIndex = 40;
          this.label46.Text = "CH32:";
          this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV20
          // 
          this.labV20.AutoSize = true;
          this.labV20.BackColor = System.Drawing.Color.White;
          this.labV20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV20.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV20.Location = new System.Drawing.Point(770, 88);
          this.labV20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV20.Name = "labV20";
          this.labV20.Size = new System.Drawing.Size(76, 28);
          this.labV20.TabIndex = 39;
          this.labV20.Text = "0.000";
          this.labV20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label44
          // 
          this.label44.AutoSize = true;
          this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label44.Location = new System.Drawing.Point(685, 88);
          this.label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label44.Name = "label44";
          this.label44.Size = new System.Drawing.Size(76, 28);
          this.label44.TabIndex = 38;
          this.label44.Text = "CH20:";
          this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV24
          // 
          this.labV24.AutoSize = true;
          this.labV24.BackColor = System.Drawing.Color.White;
          this.labV24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV24.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV24.Location = new System.Drawing.Point(940, 88);
          this.labV24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV24.Name = "labV24";
          this.labV24.Size = new System.Drawing.Size(76, 28);
          this.labV24.TabIndex = 37;
          this.labV24.Text = "0.000";
          this.labV24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label42
          // 
          this.label42.AutoSize = true;
          this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label42.Location = new System.Drawing.Point(855, 88);
          this.label42.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label42.Name = "label42";
          this.label42.Size = new System.Drawing.Size(76, 28);
          this.label42.TabIndex = 36;
          this.label42.Text = "CH24:";
          this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label33
          // 
          this.label33.AutoSize = true;
          this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label33.Location = new System.Drawing.Point(5, 59);
          this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label33.Name = "label33";
          this.label33.Size = new System.Drawing.Size(76, 28);
          this.label33.TabIndex = 27;
          this.label33.Text = "CH03:";
          this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV15
          // 
          this.labV15.AutoSize = true;
          this.labV15.BackColor = System.Drawing.Color.White;
          this.labV15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV15.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV15.Location = new System.Drawing.Point(600, 59);
          this.labV15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV15.Name = "labV15";
          this.labV15.Size = new System.Drawing.Size(76, 28);
          this.labV15.TabIndex = 26;
          this.labV15.Text = "0.000";
          this.labV15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label31
          // 
          this.label31.AutoSize = true;
          this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label31.Location = new System.Drawing.Point(685, 59);
          this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label31.Name = "label31";
          this.label31.Size = new System.Drawing.Size(76, 28);
          this.label31.TabIndex = 25;
          this.label31.Text = "CH19:";
          this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV19
          // 
          this.labV19.AutoSize = true;
          this.labV19.BackColor = System.Drawing.Color.White;
          this.labV19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV19.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV19.Location = new System.Drawing.Point(770, 59);
          this.labV19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV19.Name = "labV19";
          this.labV19.Size = new System.Drawing.Size(76, 28);
          this.labV19.TabIndex = 24;
          this.labV19.Text = "0.000";
          this.labV19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV23
          // 
          this.labV23.AutoSize = true;
          this.labV23.BackColor = System.Drawing.Color.White;
          this.labV23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV23.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV23.Location = new System.Drawing.Point(940, 59);
          this.labV23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV23.Name = "labV23";
          this.labV23.Size = new System.Drawing.Size(76, 28);
          this.labV23.TabIndex = 23;
          this.labV23.Text = "0.000";
          this.labV23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label28
          // 
          this.label28.AutoSize = true;
          this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label28.Location = new System.Drawing.Point(855, 59);
          this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label28.Name = "label28";
          this.label28.Size = new System.Drawing.Size(76, 28);
          this.label28.TabIndex = 22;
          this.label28.Text = "CH23:";
          this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label27
          // 
          this.label27.AutoSize = true;
          this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label27.Location = new System.Drawing.Point(175, 59);
          this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label27.Name = "label27";
          this.label27.Size = new System.Drawing.Size(76, 28);
          this.label27.TabIndex = 21;
          this.label27.Text = "CH07:";
          this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV3
          // 
          this.labV3.AutoSize = true;
          this.labV3.BackColor = System.Drawing.Color.White;
          this.labV3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV3.Location = new System.Drawing.Point(90, 59);
          this.labV3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV3.Name = "labV3";
          this.labV3.Size = new System.Drawing.Size(76, 28);
          this.labV3.TabIndex = 20;
          this.labV3.Text = "0.000";
          this.labV3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV11
          // 
          this.labV11.AutoSize = true;
          this.labV11.BackColor = System.Drawing.Color.White;
          this.labV11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV11.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV11.Location = new System.Drawing.Point(430, 59);
          this.labV11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV11.Name = "labV11";
          this.labV11.Size = new System.Drawing.Size(76, 28);
          this.labV11.TabIndex = 19;
          this.labV11.Text = "0.000";
          this.labV11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label24
          // 
          this.label24.AutoSize = true;
          this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label24.Location = new System.Drawing.Point(515, 59);
          this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label24.Name = "label24";
          this.label24.Size = new System.Drawing.Size(76, 28);
          this.label24.TabIndex = 18;
          this.label24.Text = "CH15:";
          this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label23
          // 
          this.label23.AutoSize = true;
          this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label23.Location = new System.Drawing.Point(345, 59);
          this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label23.Name = "label23";
          this.label23.Size = new System.Drawing.Size(76, 28);
          this.label23.TabIndex = 17;
          this.label23.Text = "CH11:";
          this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV7
          // 
          this.labV7.AutoSize = true;
          this.labV7.BackColor = System.Drawing.Color.White;
          this.labV7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV7.Location = new System.Drawing.Point(260, 59);
          this.labV7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV7.Name = "labV7";
          this.labV7.Size = new System.Drawing.Size(76, 28);
          this.labV7.TabIndex = 16;
          this.labV7.Text = "0.000";
          this.labV7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV6
          // 
          this.labV6.AutoSize = true;
          this.labV6.BackColor = System.Drawing.Color.White;
          this.labV6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV6.Location = new System.Drawing.Point(260, 30);
          this.labV6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV6.Name = "labV6";
          this.labV6.Size = new System.Drawing.Size(76, 28);
          this.labV6.TabIndex = 15;
          this.labV6.Text = "0.000";
          this.labV6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label20
          // 
          this.label20.AutoSize = true;
          this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label20.Location = new System.Drawing.Point(175, 30);
          this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label20.Name = "label20";
          this.label20.Size = new System.Drawing.Size(76, 28);
          this.label20.TabIndex = 14;
          this.label20.Text = "CH06:";
          this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV2
          // 
          this.labV2.AutoSize = true;
          this.labV2.BackColor = System.Drawing.Color.White;
          this.labV2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV2.Location = new System.Drawing.Point(90, 30);
          this.labV2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV2.Name = "labV2";
          this.labV2.Size = new System.Drawing.Size(76, 28);
          this.labV2.TabIndex = 13;
          this.labV2.Text = "0.000";
          this.labV2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label18
          // 
          this.label18.AutoSize = true;
          this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label18.Location = new System.Drawing.Point(5, 30);
          this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label18.Name = "label18";
          this.label18.Size = new System.Drawing.Size(76, 28);
          this.label18.TabIndex = 12;
          this.label18.Text = "CH02:";
          this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV21
          // 
          this.labV21.AutoSize = true;
          this.labV21.BackColor = System.Drawing.Color.White;
          this.labV21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV21.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV21.Location = new System.Drawing.Point(940, 1);
          this.labV21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV21.Name = "labV21";
          this.labV21.Size = new System.Drawing.Size(76, 28);
          this.labV21.TabIndex = 11;
          this.labV21.Text = "0.000";
          this.labV21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label16
          // 
          this.label16.AutoSize = true;
          this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label16.Location = new System.Drawing.Point(855, 1);
          this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label16.Name = "label16";
          this.label16.Size = new System.Drawing.Size(76, 28);
          this.label16.TabIndex = 10;
          this.label16.Text = "CH21:";
          this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV17
          // 
          this.labV17.AutoSize = true;
          this.labV17.BackColor = System.Drawing.Color.White;
          this.labV17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV17.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV17.Location = new System.Drawing.Point(770, 1);
          this.labV17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV17.Name = "labV17";
          this.labV17.Size = new System.Drawing.Size(76, 28);
          this.labV17.TabIndex = 9;
          this.labV17.Text = "0.000";
          this.labV17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label14
          // 
          this.label14.AutoSize = true;
          this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label14.Location = new System.Drawing.Point(685, 1);
          this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label14.Name = "label14";
          this.label14.Size = new System.Drawing.Size(76, 28);
          this.label14.TabIndex = 8;
          this.label14.Text = "CH17:";
          this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV13
          // 
          this.labV13.AutoSize = true;
          this.labV13.BackColor = System.Drawing.Color.White;
          this.labV13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV13.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV13.Location = new System.Drawing.Point(600, 1);
          this.labV13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV13.Name = "labV13";
          this.labV13.Size = new System.Drawing.Size(76, 28);
          this.labV13.TabIndex = 7;
          this.labV13.Text = "0.000";
          this.labV13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label12
          // 
          this.label12.AutoSize = true;
          this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label12.Location = new System.Drawing.Point(515, 1);
          this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label12.Name = "label12";
          this.label12.Size = new System.Drawing.Size(76, 28);
          this.label12.TabIndex = 6;
          this.label12.Text = "CH13:";
          this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV9
          // 
          this.labV9.AutoSize = true;
          this.labV9.BackColor = System.Drawing.Color.White;
          this.labV9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV9.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV9.Location = new System.Drawing.Point(430, 1);
          this.labV9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV9.Name = "labV9";
          this.labV9.Size = new System.Drawing.Size(76, 28);
          this.labV9.TabIndex = 5;
          this.labV9.Text = "0.000";
          this.labV9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label10
          // 
          this.label10.AutoSize = true;
          this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label10.Location = new System.Drawing.Point(345, 1);
          this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label10.Name = "label10";
          this.label10.Size = new System.Drawing.Size(76, 28);
          this.label10.TabIndex = 4;
          this.label10.Text = "CH09:";
          this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV5
          // 
          this.labV5.AutoSize = true;
          this.labV5.BackColor = System.Drawing.Color.White;
          this.labV5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV5.Location = new System.Drawing.Point(260, 1);
          this.labV5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV5.Name = "labV5";
          this.labV5.Size = new System.Drawing.Size(76, 28);
          this.labV5.TabIndex = 3;
          this.labV5.Text = "0.000";
          this.labV5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label8
          // 
          this.label8.AutoSize = true;
          this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label8.Location = new System.Drawing.Point(175, 1);
          this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label8.Name = "label8";
          this.label8.Size = new System.Drawing.Size(76, 28);
          this.label8.TabIndex = 2;
          this.label8.Text = "CH05:";
          this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV1
          // 
          this.labV1.AutoSize = true;
          this.labV1.BackColor = System.Drawing.Color.White;
          this.labV1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV1.Location = new System.Drawing.Point(90, 1);
          this.labV1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV1.Name = "labV1";
          this.labV1.Size = new System.Drawing.Size(76, 28);
          this.labV1.TabIndex = 1;
          this.labV1.Text = "0.000";
          this.labV1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label6
          // 
          this.label6.AutoSize = true;
          this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label6.Location = new System.Drawing.Point(5, 1);
          this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(76, 28);
          this.label6.TabIndex = 0;
          this.label6.Text = "CH01:";
          this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label34
          // 
          this.label34.AutoSize = true;
          this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label34.Location = new System.Drawing.Point(345, 30);
          this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label34.Name = "label34";
          this.label34.Size = new System.Drawing.Size(76, 28);
          this.label34.TabIndex = 28;
          this.label34.Text = "CH10:";
          this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV10
          // 
          this.labV10.AutoSize = true;
          this.labV10.BackColor = System.Drawing.Color.White;
          this.labV10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV10.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV10.Location = new System.Drawing.Point(430, 30);
          this.labV10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV10.Name = "labV10";
          this.labV10.Size = new System.Drawing.Size(76, 28);
          this.labV10.TabIndex = 29;
          this.labV10.Text = "0.000";
          this.labV10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label36
          // 
          this.label36.AutoSize = true;
          this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label36.Location = new System.Drawing.Point(515, 30);
          this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label36.Name = "label36";
          this.label36.Size = new System.Drawing.Size(76, 28);
          this.label36.TabIndex = 30;
          this.label36.Text = "CH14:";
          this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV14
          // 
          this.labV14.AutoSize = true;
          this.labV14.BackColor = System.Drawing.Color.White;
          this.labV14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV14.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV14.Location = new System.Drawing.Point(600, 30);
          this.labV14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV14.Name = "labV14";
          this.labV14.Size = new System.Drawing.Size(76, 28);
          this.labV14.TabIndex = 31;
          this.labV14.Text = "0.000";
          this.labV14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label38
          // 
          this.label38.AutoSize = true;
          this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label38.Location = new System.Drawing.Point(685, 30);
          this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label38.Name = "label38";
          this.label38.Size = new System.Drawing.Size(76, 28);
          this.label38.TabIndex = 32;
          this.label38.Text = "CH18:";
          this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV18
          // 
          this.labV18.AutoSize = true;
          this.labV18.BackColor = System.Drawing.Color.White;
          this.labV18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV18.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV18.Location = new System.Drawing.Point(770, 30);
          this.labV18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV18.Name = "labV18";
          this.labV18.Size = new System.Drawing.Size(76, 28);
          this.labV18.TabIndex = 33;
          this.labV18.Text = "0.000";
          this.labV18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label40
          // 
          this.label40.AutoSize = true;
          this.label40.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label40.Location = new System.Drawing.Point(855, 30);
          this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label40.Name = "label40";
          this.label40.Size = new System.Drawing.Size(76, 28);
          this.label40.TabIndex = 34;
          this.label40.Text = "CH22:";
          this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV22
          // 
          this.labV22.AutoSize = true;
          this.labV22.BackColor = System.Drawing.Color.White;
          this.labV22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV22.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV22.Location = new System.Drawing.Point(940, 30);
          this.labV22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV22.Name = "labV22";
          this.labV22.Size = new System.Drawing.Size(76, 28);
          this.labV22.TabIndex = 35;
          this.labV22.Text = "0.000";
          this.labV22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label5
          // 
          this.label5.AutoSize = true;
          this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label5.Location = new System.Drawing.Point(1025, 1);
          this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(76, 28);
          this.label5.TabIndex = 50;
          this.label5.Text = "CH25:";
          this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label56
          // 
          this.label56.AutoSize = true;
          this.label56.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label56.Location = new System.Drawing.Point(1025, 30);
          this.label56.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label56.Name = "label56";
          this.label56.Size = new System.Drawing.Size(76, 28);
          this.label56.TabIndex = 51;
          this.label56.Text = "CH26:";
          this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label57
          // 
          this.label57.AutoSize = true;
          this.label57.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label57.Location = new System.Drawing.Point(1025, 59);
          this.label57.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label57.Name = "label57";
          this.label57.Size = new System.Drawing.Size(76, 28);
          this.label57.TabIndex = 52;
          this.label57.Text = "CH27:";
          this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV25
          // 
          this.labV25.AutoSize = true;
          this.labV25.BackColor = System.Drawing.Color.White;
          this.labV25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV25.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV25.Location = new System.Drawing.Point(1110, 1);
          this.labV25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV25.Name = "labV25";
          this.labV25.Size = new System.Drawing.Size(76, 28);
          this.labV25.TabIndex = 53;
          this.labV25.Text = "0.000";
          this.labV25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV26
          // 
          this.labV26.AutoSize = true;
          this.labV26.BackColor = System.Drawing.Color.White;
          this.labV26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV26.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV26.Location = new System.Drawing.Point(1110, 30);
          this.labV26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV26.Name = "labV26";
          this.labV26.Size = new System.Drawing.Size(76, 28);
          this.labV26.TabIndex = 54;
          this.labV26.Text = "0.000";
          this.labV26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV27
          // 
          this.labV27.AutoSize = true;
          this.labV27.BackColor = System.Drawing.Color.White;
          this.labV27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV27.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV27.Location = new System.Drawing.Point(1110, 59);
          this.labV27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV27.Name = "labV27";
          this.labV27.Size = new System.Drawing.Size(76, 28);
          this.labV27.TabIndex = 55;
          this.labV27.Text = "0.000";
          this.labV27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label63
          // 
          this.label63.AutoSize = true;
          this.label63.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label63.Location = new System.Drawing.Point(1195, 1);
          this.label63.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label63.Name = "label63";
          this.label63.Size = new System.Drawing.Size(76, 28);
          this.label63.TabIndex = 58;
          this.label63.Text = "CH29:";
          this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label64
          // 
          this.label64.AutoSize = true;
          this.label64.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label64.Location = new System.Drawing.Point(1195, 30);
          this.label64.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label64.Name = "label64";
          this.label64.Size = new System.Drawing.Size(76, 28);
          this.label64.TabIndex = 59;
          this.label64.Text = "CH30:";
          this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label65
          // 
          this.label65.AutoSize = true;
          this.label65.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label65.Location = new System.Drawing.Point(1195, 59);
          this.label65.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label65.Name = "label65";
          this.label65.Size = new System.Drawing.Size(76, 28);
          this.label65.TabIndex = 60;
          this.label65.Text = "CH31:";
          this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV29
          // 
          this.labV29.AutoSize = true;
          this.labV29.BackColor = System.Drawing.Color.White;
          this.labV29.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV29.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV29.Location = new System.Drawing.Point(1280, 1);
          this.labV29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV29.Name = "labV29";
          this.labV29.Size = new System.Drawing.Size(84, 28);
          this.labV29.TabIndex = 61;
          this.labV29.Text = "0.000";
          this.labV29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV30
          // 
          this.labV30.AutoSize = true;
          this.labV30.BackColor = System.Drawing.Color.White;
          this.labV30.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV30.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV30.Location = new System.Drawing.Point(1280, 30);
          this.labV30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV30.Name = "labV30";
          this.labV30.Size = new System.Drawing.Size(84, 28);
          this.labV30.TabIndex = 62;
          this.labV30.Text = "0.000";
          this.labV30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV31
          // 
          this.labV31.AutoSize = true;
          this.labV31.BackColor = System.Drawing.Color.White;
          this.labV31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV31.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV31.Location = new System.Drawing.Point(1280, 59);
          this.labV31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labV31.Name = "labV31";
          this.labV31.Size = new System.Drawing.Size(84, 28);
          this.labV31.TabIndex = 63;
          this.labV31.Text = "0.000";
          this.labV31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // panel4
          // 
          this.panel4.ColumnCount = 10;
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel4.Controls.Add(this.label69, 0, 0);
          this.panel4.Controls.Add(this.btnVer, 7, 0);
          this.panel4.Controls.Add(this.btnVolt, 8, 0);
          this.panel4.Controls.Add(this.chkSync, 6, 0);
          this.panel4.Controls.Add(this.label70, 4, 0);
          this.panel4.Controls.Add(this.labOnOff, 5, 0);
          this.panel4.Controls.Add(this.chkMode, 3, 0);
          this.panel4.Controls.Add(this.btnScan, 9, 0);
          this.panel4.Controls.Add(this.label62, 1, 0);
          this.panel4.Controls.Add(this.txtEndAddr, 2, 0);
          this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel4.Location = new System.Drawing.Point(5, 5);
          this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel4.Name = "panel4";
          this.panel4.RowCount = 1;
          this.panel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel4.Size = new System.Drawing.Size(1369, 36);
          this.panel4.TabIndex = 2;
          // 
          // label69
          // 
          this.label69.AutoSize = true;
          this.label69.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label69.Location = new System.Drawing.Point(4, 0);
          this.label69.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label69.Name = "label69";
          this.label69.Size = new System.Drawing.Size(345, 36);
          this.label69.TabIndex = 0;
          this.label69.Text = "功能1:32通道电压检测";
          this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // btnVer
          // 
          this.btnVer.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnVer.Location = new System.Drawing.Point(1048, 0);
          this.btnVer.Margin = new System.Windows.Forms.Padding(0);
          this.btnVer.Name = "btnVer";
          this.btnVer.Size = new System.Drawing.Size(107, 36);
          this.btnVer.TabIndex = 1;
          this.btnVer.Text = "读版本";
          this.btnVer.UseVisualStyleBackColor = true;
          this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
          // 
          // btnVolt
          // 
          this.btnVolt.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnVolt.Location = new System.Drawing.Point(1155, 0);
          this.btnVolt.Margin = new System.Windows.Forms.Padding(0);
          this.btnVolt.Name = "btnVolt";
          this.btnVolt.Size = new System.Drawing.Size(107, 36);
          this.btnVolt.TabIndex = 2;
          this.btnVolt.Text = "读电压";
          this.btnVolt.UseVisualStyleBackColor = true;
          this.btnVolt.Click += new System.EventHandler(this.btnVolt_Click);
          // 
          // chkSync
          // 
          this.chkSync.AutoSize = true;
          this.chkSync.Dock = System.Windows.Forms.DockStyle.Fill;
          this.chkSync.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.chkSync.Location = new System.Drawing.Point(892, 4);
          this.chkSync.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.chkSync.Name = "chkSync";
          this.chkSync.Size = new System.Drawing.Size(152, 28);
          this.chkSync.TabIndex = 3;
          this.chkSync.Text = "同步读电压";
          this.chkSync.UseVisualStyleBackColor = true;
          // 
          // label70
          // 
          this.label70.AutoSize = true;
          this.label70.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label70.Location = new System.Drawing.Point(678, 0);
          this.label70.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label70.Name = "label70";
          this.label70.Size = new System.Drawing.Size(99, 36);
          this.label70.TabIndex = 4;
          this.label70.Text = "AC ON/OFF:";
          this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // labOnOff
          // 
          this.labOnOff.AutoSize = true;
          this.labOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labOnOff.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labOnOff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
          this.labOnOff.Location = new System.Drawing.Point(785, 0);
          this.labOnOff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labOnOff.Name = "labOnOff";
          this.labOnOff.Size = new System.Drawing.Size(99, 36);
          this.labOnOff.TabIndex = 5;
          this.labOnOff.Text = "---";
          this.labOnOff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // chkMode
          // 
          this.chkMode.AutoSize = true;
          this.chkMode.Dock = System.Windows.Forms.DockStyle.Fill;
          this.chkMode.Location = new System.Drawing.Point(571, 4);
          this.chkMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.chkMode.Name = "chkMode";
          this.chkMode.Size = new System.Drawing.Size(99, 28);
          this.chkMode.TabIndex = 6;
          this.chkMode.Text = "老化房";
          this.chkMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          this.chkMode.UseVisualStyleBackColor = true;
          // 
          // btnScan
          // 
          this.btnScan.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnScan.Location = new System.Drawing.Point(1262, 0);
          this.btnScan.Margin = new System.Windows.Forms.Padding(0);
          this.btnScan.Name = "btnScan";
          this.btnScan.Size = new System.Drawing.Size(107, 36);
          this.btnScan.TabIndex = 7;
          this.btnScan.Text = "扫描监控";
          this.btnScan.UseVisualStyleBackColor = true;
          this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
          // 
          // label62
          // 
          this.label62.AutoSize = true;
          this.label62.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label62.Location = new System.Drawing.Point(357, 4);
          this.label62.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.label62.Name = "label62";
          this.label62.Size = new System.Drawing.Size(99, 28);
          this.label62.TabIndex = 8;
          this.label62.Text = "末尾地址:";
          this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // txtEndAddr
          // 
          this.txtEndAddr.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtEndAddr.Location = new System.Drawing.Point(464, 4);
          this.txtEndAddr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtEndAddr.Name = "txtEndAddr";
          this.txtEndAddr.Size = new System.Drawing.Size(99, 25);
          this.txtEndAddr.TabIndex = 9;
          this.txtEndAddr.Text = "1";
          this.txtEndAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // panel10
          // 
          this.panel10.ColumnCount = 2;
          this.panel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
          this.panel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
          this.panel10.Controls.Add(this.gridMon, 0, 0);
          this.panel10.Controls.Add(this.panel11, 0, 0);
          this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel10.Location = new System.Drawing.Point(5, 176);
          this.panel10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel10.Name = "panel10";
          this.panel10.RowCount = 1;
          this.panel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel10.Size = new System.Drawing.Size(1369, 560);
          this.panel10.TabIndex = 8;
          // 
          // gridMon
          // 
          this.gridMon.BackgroundColor = System.Drawing.Color.White;
          this.gridMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.gridMon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column5,
            this.dataGridViewTextBoxColumn4});
          this.gridMon.Dock = System.Windows.Forms.DockStyle.Fill;
          this.gridMon.Location = new System.Drawing.Point(688, 4);
          this.gridMon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.gridMon.Name = "gridMon";
          this.gridMon.RowHeadersVisible = false;
          this.gridMon.RowHeadersWidth = 20;
          this.gridMon.RowTemplate.Height = 23;
          this.gridMon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
          this.gridMon.Size = new System.Drawing.Size(677, 552);
          this.gridMon.TabIndex = 1;
          // 
          // dataGridViewTextBoxColumn1
          // 
          this.dataGridViewTextBoxColumn1.HeaderText = "地址";
          this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
          this.dataGridViewTextBoxColumn1.Width = 56;
          // 
          // dataGridViewTextBoxColumn2
          // 
          this.dataGridViewTextBoxColumn2.HeaderText = "通信";
          this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
          this.dataGridViewTextBoxColumn2.Width = 56;
          // 
          // dataGridViewTextBoxColumn3
          // 
          this.dataGridViewTextBoxColumn3.HeaderText = "版本";
          this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
          this.dataGridViewTextBoxColumn3.Width = 55;
          // 
          // Column5
          // 
          this.Column5.HeaderText = "ON/OFF";
          this.Column5.Name = "Column5";
          this.Column5.Width = 55;
          // 
          // dataGridViewTextBoxColumn4
          // 
          this.dataGridViewTextBoxColumn4.HeaderText = "读值";
          this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
          this.dataGridViewTextBoxColumn4.Width = 700;
          // 
          // panel11
          // 
          this.panel11.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel11.ColumnCount = 1;
          this.panel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel11.Controls.Add(this.panel9, 0, 4);
          this.panel11.Controls.Add(this.panel7, 0, 3);
          this.panel11.Controls.Add(this.panel5, 0, 2);
          this.panel11.Controls.Add(this.panel8, 0, 1);
          this.panel11.Controls.Add(this.panel6, 0, 0);
          this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel11.Location = new System.Drawing.Point(0, 0);
          this.panel11.Margin = new System.Windows.Forms.Padding(0);
          this.panel11.Name = "panel11";
          this.panel11.RowCount = 6;
          this.panel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
          this.panel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
          this.panel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
          this.panel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
          this.panel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
          this.panel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel11.Size = new System.Drawing.Size(684, 560);
          this.panel11.TabIndex = 0;
          // 
          // panel9
          // 
          this.panel9.ColumnCount = 7;
          this.panel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
          this.panel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
          this.panel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel9.Controls.Add(this.label59, 0, 0);
          this.panel9.Controls.Add(this.labErrCode, 1, 0);
          this.panel9.Controls.Add(this.label60, 2, 0);
          this.panel9.Controls.Add(this.txtRelayNo, 3, 0);
          this.panel9.Controls.Add(this.btnRlyON, 4, 0);
          this.panel9.Controls.Add(this.btnRlyOff, 5, 0);
          this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel9.Location = new System.Drawing.Point(5, 445);
          this.panel9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel9.Name = "panel9";
          this.panel9.RowCount = 1;
          this.panel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel9.Size = new System.Drawing.Size(674, 32);
          this.panel9.TabIndex = 10;
          // 
          // label59
          // 
          this.label59.AutoSize = true;
          this.label59.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label59.Location = new System.Drawing.Point(4, 0);
          this.label59.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label59.Name = "label59";
          this.label59.Size = new System.Drawing.Size(99, 32);
          this.label59.TabIndex = 0;
          this.label59.Text = "错误代码:";
          this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labErrCode
          // 
          this.labErrCode.AutoSize = true;
          this.labErrCode.BackColor = System.Drawing.Color.White;
          this.labErrCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labErrCode.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labErrCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labErrCode.ForeColor = System.Drawing.Color.Black;
          this.labErrCode.Location = new System.Drawing.Point(111, 0);
          this.labErrCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labErrCode.Name = "labErrCode";
          this.labErrCode.Size = new System.Drawing.Size(392, 32);
          this.labErrCode.TabIndex = 1;
          this.labErrCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label60
          // 
          this.label60.AutoSize = true;
          this.label60.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label60.Location = new System.Drawing.Point(511, 0);
          this.label60.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label60.Name = "label60";
          this.label60.Size = new System.Drawing.Size(152, 32);
          this.label60.TabIndex = 2;
          this.label60.Text = "IO控制-RelayNo:";
          this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // txtRelayNo
          // 
          this.txtRelayNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.txtRelayNo.Location = new System.Drawing.Point(671, 4);
          this.txtRelayNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtRelayNo.Name = "txtRelayNo";
          this.txtRelayNo.Size = new System.Drawing.Size(97, 30);
          this.txtRelayNo.TabIndex = 3;
          this.txtRelayNo.Text = "1";
          this.txtRelayNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // btnRlyON
          // 
          this.btnRlyON.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnRlyON.Location = new System.Drawing.Point(774, 0);
          this.btnRlyON.Margin = new System.Windows.Forms.Padding(0);
          this.btnRlyON.Name = "btnRlyON";
          this.btnRlyON.Size = new System.Drawing.Size(107, 32);
          this.btnRlyON.TabIndex = 4;
          this.btnRlyON.Text = "Relay ON";
          this.btnRlyON.UseVisualStyleBackColor = true;
          // 
          // btnRlyOff
          // 
          this.btnRlyOff.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnRlyOff.Location = new System.Drawing.Point(881, 0);
          this.btnRlyOff.Margin = new System.Windows.Forms.Padding(0);
          this.btnRlyOff.Name = "btnRlyOff";
          this.btnRlyOff.Size = new System.Drawing.Size(107, 32);
          this.btnRlyOff.TabIndex = 5;
          this.btnRlyOff.Text = "Relay OFF";
          this.btnRlyOff.UseVisualStyleBackColor = true;
          // 
          // panel7
          // 
          this.panel7.ColumnCount = 18;
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel7.Controls.Add(this.label17, 0, 0);
          this.panel7.Controls.Add(this.btnStart, 16, 0);
          this.panel7.Controls.Add(this.btnReadSgn, 15, 0);
          this.panel7.Controls.Add(this.btnStop, 17, 0);
          this.panel7.Controls.Add(this.label25, 5, 0);
          this.panel7.Controls.Add(this.label32, 3, 0);
          this.panel7.Controls.Add(this.label41, 1, 0);
          this.panel7.Controls.Add(this.labX1, 6, 0);
          this.panel7.Controls.Add(this.labX2, 7, 0);
          this.panel7.Controls.Add(this.labX3, 8, 0);
          this.panel7.Controls.Add(this.labX4, 9, 0);
          this.panel7.Controls.Add(this.labX5, 10, 0);
          this.panel7.Controls.Add(this.labX6, 11, 0);
          this.panel7.Controls.Add(this.labX7, 12, 0);
          this.panel7.Controls.Add(this.labX8, 13, 0);
          this.panel7.Controls.Add(this.labS1, 2, 0);
          this.panel7.Controls.Add(this.labS2, 4, 0);
          this.panel7.Controls.Add(this.labX9, 14, 0);
          this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel7.Location = new System.Drawing.Point(5, 400);
          this.panel7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel7.Name = "panel7";
          this.panel7.RowCount = 1;
          this.panel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel7.Size = new System.Drawing.Size(674, 36);
          this.panel7.TabIndex = 9;
          // 
          // label17
          // 
          this.label17.AutoSize = true;
          this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label17.Location = new System.Drawing.Point(4, 0);
          this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label17.Name = "label17";
          this.label17.Size = new System.Drawing.Size(1, 36);
          this.label17.TabIndex = 0;
          this.label17.Text = "功能3:控制信号设置";
          this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // btnStart
          // 
          this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnStart.Location = new System.Drawing.Point(460, 0);
          this.btnStart.Margin = new System.Windows.Forms.Padding(0);
          this.btnStart.Name = "btnStart";
          this.btnStart.Size = new System.Drawing.Size(107, 36);
          this.btnStart.TabIndex = 1;
          this.btnStart.Text = "启动";
          this.btnStart.UseVisualStyleBackColor = true;
          this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
          // 
          // btnReadSgn
          // 
          this.btnReadSgn.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnReadSgn.Location = new System.Drawing.Point(353, 0);
          this.btnReadSgn.Margin = new System.Windows.Forms.Padding(0);
          this.btnReadSgn.Name = "btnReadSgn";
          this.btnReadSgn.Size = new System.Drawing.Size(107, 36);
          this.btnReadSgn.TabIndex = 2;
          this.btnReadSgn.Text = "读信号";
          this.btnReadSgn.UseVisualStyleBackColor = true;
          this.btnReadSgn.Click += new System.EventHandler(this.btnReadSgn_Click);
          // 
          // btnStop
          // 
          this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnStop.Location = new System.Drawing.Point(567, 0);
          this.btnStop.Margin = new System.Windows.Forms.Padding(0);
          this.btnStop.Name = "btnStop";
          this.btnStop.Size = new System.Drawing.Size(107, 36);
          this.btnStop.TabIndex = 3;
          this.btnStop.Text = "停止";
          this.btnStop.UseVisualStyleBackColor = true;
          this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
          // 
          // label25
          // 
          this.label25.AutoSize = true;
          this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label25.Location = new System.Drawing.Point(47, 0);
          this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label25.Name = "label25";
          this.label25.Size = new System.Drawing.Size(59, 36);
          this.label25.TabIndex = 4;
          this.label25.Text = "IO_X:";
          this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // label32
          // 
          this.label32.AutoSize = true;
          this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label32.Location = new System.Drawing.Point(-47, 0);
          this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label32.Name = "label32";
          this.label32.Size = new System.Drawing.Size(59, 36);
          this.label32.TabIndex = 5;
          this.label32.Text = "S2:";
          this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // label41
          // 
          this.label41.AutoSize = true;
          this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label41.Location = new System.Drawing.Point(-141, 0);
          this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label41.Name = "label41";
          this.label41.Size = new System.Drawing.Size(59, 36);
          this.label41.TabIndex = 6;
          this.label41.Text = "S1:";
          this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // labX1
          // 
          this.labX1.AutoSize = true;
          this.labX1.BackColor = System.Drawing.SystemColors.Control;
          this.labX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labX1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labX1.Location = new System.Drawing.Point(114, 4);
          this.labX1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labX1.Name = "labX1";
          this.labX1.Size = new System.Drawing.Size(19, 28);
          this.labX1.TabIndex = 7;
          // 
          // labX2
          // 
          this.labX2.AutoSize = true;
          this.labX2.BackColor = System.Drawing.SystemColors.Control;
          this.labX2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labX2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labX2.Location = new System.Drawing.Point(141, 4);
          this.labX2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labX2.Name = "labX2";
          this.labX2.Size = new System.Drawing.Size(19, 28);
          this.labX2.TabIndex = 8;
          // 
          // labX3
          // 
          this.labX3.AutoSize = true;
          this.labX3.BackColor = System.Drawing.SystemColors.Control;
          this.labX3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labX3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labX3.Location = new System.Drawing.Point(168, 4);
          this.labX3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labX3.Name = "labX3";
          this.labX3.Size = new System.Drawing.Size(19, 28);
          this.labX3.TabIndex = 9;
          // 
          // labX4
          // 
          this.labX4.AutoSize = true;
          this.labX4.BackColor = System.Drawing.SystemColors.Control;
          this.labX4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labX4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labX4.Location = new System.Drawing.Point(195, 4);
          this.labX4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labX4.Name = "labX4";
          this.labX4.Size = new System.Drawing.Size(19, 28);
          this.labX4.TabIndex = 10;
          // 
          // labX5
          // 
          this.labX5.AutoSize = true;
          this.labX5.BackColor = System.Drawing.SystemColors.Control;
          this.labX5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labX5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labX5.Location = new System.Drawing.Point(222, 4);
          this.labX5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labX5.Name = "labX5";
          this.labX5.Size = new System.Drawing.Size(19, 28);
          this.labX5.TabIndex = 11;
          // 
          // labX6
          // 
          this.labX6.AutoSize = true;
          this.labX6.BackColor = System.Drawing.SystemColors.Control;
          this.labX6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labX6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labX6.Location = new System.Drawing.Point(249, 4);
          this.labX6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labX6.Name = "labX6";
          this.labX6.Size = new System.Drawing.Size(19, 28);
          this.labX6.TabIndex = 12;
          // 
          // labX7
          // 
          this.labX7.AutoSize = true;
          this.labX7.BackColor = System.Drawing.SystemColors.Control;
          this.labX7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labX7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labX7.Location = new System.Drawing.Point(276, 4);
          this.labX7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labX7.Name = "labX7";
          this.labX7.Size = new System.Drawing.Size(19, 28);
          this.labX7.TabIndex = 13;
          // 
          // labX8
          // 
          this.labX8.AutoSize = true;
          this.labX8.BackColor = System.Drawing.SystemColors.Control;
          this.labX8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labX8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labX8.Location = new System.Drawing.Point(303, 4);
          this.labX8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labX8.Name = "labX8";
          this.labX8.Size = new System.Drawing.Size(19, 28);
          this.labX8.TabIndex = 14;
          // 
          // labS1
          // 
          this.labS1.AutoSize = true;
          this.labS1.BackColor = System.Drawing.SystemColors.Control;
          this.labS1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labS1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labS1.Location = new System.Drawing.Point(-74, 4);
          this.labS1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labS1.Name = "labS1";
          this.labS1.Size = new System.Drawing.Size(19, 28);
          this.labS1.TabIndex = 15;
          // 
          // labS2
          // 
          this.labS2.AutoSize = true;
          this.labS2.BackColor = System.Drawing.SystemColors.Control;
          this.labS2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labS2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labS2.Location = new System.Drawing.Point(20, 4);
          this.labS2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labS2.Name = "labS2";
          this.labS2.Size = new System.Drawing.Size(19, 28);
          this.labS2.TabIndex = 16;
          // 
          // labX9
          // 
          this.labX9.AutoSize = true;
          this.labX9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labX9.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labX9.Location = new System.Drawing.Point(330, 4);
          this.labX9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labX9.Name = "labX9";
          this.labX9.Size = new System.Drawing.Size(19, 28);
          this.labX9.TabIndex = 17;
          // 
          // panel5
          // 
          this.panel5.ColumnCount = 3;
          this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel5.Controls.Add(this.label7, 0, 0);
          this.panel5.Controls.Add(this.btnSetPara, 1, 0);
          this.panel5.Controls.Add(this.btnReadPara, 2, 0);
          this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel5.Location = new System.Drawing.Point(5, 357);
          this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel5.Name = "panel5";
          this.panel5.RowCount = 1;
          this.panel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel5.Size = new System.Drawing.Size(674, 34);
          this.panel5.TabIndex = 8;
          // 
          // label7
          // 
          this.label7.AutoSize = true;
          this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label7.Location = new System.Drawing.Point(4, 0);
          this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(452, 34);
          this.label7.TabIndex = 0;
          this.label7.Text = "功能2:ONOFF参数设置";
          this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // btnSetPara
          // 
          this.btnSetPara.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnSetPara.Location = new System.Drawing.Point(460, 0);
          this.btnSetPara.Margin = new System.Windows.Forms.Padding(0);
          this.btnSetPara.Name = "btnSetPara";
          this.btnSetPara.Size = new System.Drawing.Size(107, 34);
          this.btnSetPara.TabIndex = 1;
          this.btnSetPara.Text = "设置";
          this.btnSetPara.UseVisualStyleBackColor = true;
          this.btnSetPara.Click += new System.EventHandler(this.btnSetPara_Click);
          // 
          // btnReadPara
          // 
          this.btnReadPara.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnReadPara.Location = new System.Drawing.Point(567, 0);
          this.btnReadPara.Margin = new System.Windows.Forms.Padding(0);
          this.btnReadPara.Name = "btnReadPara";
          this.btnReadPara.Size = new System.Drawing.Size(107, 34);
          this.btnReadPara.TabIndex = 2;
          this.btnReadPara.Text = "读取";
          this.btnReadPara.UseVisualStyleBackColor = true;
          this.btnReadPara.Click += new System.EventHandler(this.btnReadPara_Click);
          // 
          // panel8
          // 
          this.panel8.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel8.ColumnCount = 8;
          this.panel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel8.Controls.Add(this.label43, 0, 0);
          this.panel8.Controls.Add(this.label45, 2, 0);
          this.panel8.Controls.Add(this.label47, 4, 0);
          this.panel8.Controls.Add(this.label49, 0, 1);
          this.panel8.Controls.Add(this.label51, 2, 1);
          this.panel8.Controls.Add(this.label53, 4, 1);
          this.panel8.Controls.Add(this.label55, 6, 0);
          this.panel8.Controls.Add(this.label58, 6, 1);
          this.panel8.Controls.Add(this.labRunMin, 1, 0);
          this.panel8.Controls.Add(this.labRunOnOff, 1, 1);
          this.panel8.Controls.Add(this.labRunSec, 3, 0);
          this.panel8.Controls.Add(this.labOnOffTime, 3, 1);
          this.panel8.Controls.Add(this.labStartFlag, 5, 0);
          this.panel8.Controls.Add(this.labOnOffCycle, 5, 1);
          this.panel8.Controls.Add(this.labFinishFlag, 7, 0);
          this.panel8.Controls.Add(this.labRunFlag, 7, 1);
          this.panel8.Controls.Add(this.label73, 0, 2);
          this.panel8.Controls.Add(this.labAcOn, 1, 2);
          this.panel8.Controls.Add(this.label75, 2, 2);
          this.panel8.Controls.Add(this.label76, 4, 2);
          this.panel8.Controls.Add(this.labRelayON, 3, 2);
          this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel8.Location = new System.Drawing.Point(5, 181);
          this.panel8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel8.Name = "panel8";
          this.panel8.RowCount = 3;
          this.panel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
          this.panel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
          this.panel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
          this.panel8.Size = new System.Drawing.Size(674, 167);
          this.panel8.TabIndex = 7;
          // 
          // label43
          // 
          this.label43.AutoSize = true;
          this.label43.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label43.Location = new System.Drawing.Point(5, 1);
          this.label43.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label43.Name = "label43";
          this.label43.Size = new System.Drawing.Size(75, 54);
          this.label43.TabIndex = 0;
          this.label43.Text = "运行时间:";
          this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label45
          // 
          this.label45.AutoSize = true;
          this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label45.Location = new System.Drawing.Point(173, 1);
          this.label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label45.Name = "label45";
          this.label45.Size = new System.Drawing.Size(75, 54);
          this.label45.TabIndex = 1;
          this.label45.Text = "分钟";
          this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label47
          // 
          this.label47.AutoSize = true;
          this.label47.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label47.Location = new System.Drawing.Point(341, 1);
          this.label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label47.Name = "label47";
          this.label47.Size = new System.Drawing.Size(75, 54);
          this.label47.TabIndex = 2;
          this.label47.Text = "启动标志:";
          this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label49
          // 
          this.label49.AutoSize = true;
          this.label49.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label49.Location = new System.Drawing.Point(5, 56);
          this.label49.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label49.Name = "label49";
          this.label49.Size = new System.Drawing.Size(75, 54);
          this.label49.TabIndex = 3;
          this.label49.Text = "运行ONOFF段:";
          this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label51
          // 
          this.label51.AutoSize = true;
          this.label51.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label51.Location = new System.Drawing.Point(173, 56);
          this.label51.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label51.Name = "label51";
          this.label51.Size = new System.Drawing.Size(75, 54);
          this.label51.TabIndex = 4;
          this.label51.Text = "运行ONOFF时间:";
          this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label53
          // 
          this.label53.AutoSize = true;
          this.label53.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label53.Location = new System.Drawing.Point(341, 56);
          this.label53.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label53.Name = "label53";
          this.label53.Size = new System.Drawing.Size(75, 54);
          this.label53.TabIndex = 5;
          this.label53.Text = "已循环次数:";
          this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label55
          // 
          this.label55.AutoSize = true;
          this.label55.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label55.Location = new System.Drawing.Point(509, 1);
          this.label55.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label55.Name = "label55";
          this.label55.Size = new System.Drawing.Size(75, 54);
          this.label55.TabIndex = 6;
          this.label55.Text = "完成标志:";
          this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label58
          // 
          this.label58.AutoSize = true;
          this.label58.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label58.Location = new System.Drawing.Point(509, 56);
          this.label58.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label58.Name = "label58";
          this.label58.Size = new System.Drawing.Size(75, 54);
          this.label58.TabIndex = 7;
          this.label58.Text = "运行标志:";
          this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labRunMin
          // 
          this.labRunMin.AutoSize = true;
          this.labRunMin.BackColor = System.Drawing.Color.White;
          this.labRunMin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labRunMin.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labRunMin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labRunMin.ForeColor = System.Drawing.Color.Black;
          this.labRunMin.Location = new System.Drawing.Point(89, 1);
          this.labRunMin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labRunMin.Name = "labRunMin";
          this.labRunMin.Size = new System.Drawing.Size(75, 54);
          this.labRunMin.TabIndex = 8;
          this.labRunMin.Text = "0";
          this.labRunMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labRunOnOff
          // 
          this.labRunOnOff.AutoSize = true;
          this.labRunOnOff.BackColor = System.Drawing.Color.White;
          this.labRunOnOff.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labRunOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labRunOnOff.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labRunOnOff.ForeColor = System.Drawing.Color.Black;
          this.labRunOnOff.Location = new System.Drawing.Point(89, 56);
          this.labRunOnOff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labRunOnOff.Name = "labRunOnOff";
          this.labRunOnOff.Size = new System.Drawing.Size(75, 54);
          this.labRunOnOff.TabIndex = 9;
          this.labRunOnOff.Text = "0";
          this.labRunOnOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labRunSec
          // 
          this.labRunSec.AutoSize = true;
          this.labRunSec.BackColor = System.Drawing.Color.White;
          this.labRunSec.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labRunSec.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labRunSec.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labRunSec.ForeColor = System.Drawing.Color.Black;
          this.labRunSec.Location = new System.Drawing.Point(257, 1);
          this.labRunSec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labRunSec.Name = "labRunSec";
          this.labRunSec.Size = new System.Drawing.Size(75, 54);
          this.labRunSec.TabIndex = 10;
          this.labRunSec.Text = "0";
          this.labRunSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labOnOffTime
          // 
          this.labOnOffTime.AutoSize = true;
          this.labOnOffTime.BackColor = System.Drawing.Color.White;
          this.labOnOffTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labOnOffTime.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labOnOffTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labOnOffTime.ForeColor = System.Drawing.Color.Black;
          this.labOnOffTime.Location = new System.Drawing.Point(257, 56);
          this.labOnOffTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labOnOffTime.Name = "labOnOffTime";
          this.labOnOffTime.Size = new System.Drawing.Size(75, 54);
          this.labOnOffTime.TabIndex = 11;
          this.labOnOffTime.Text = "0";
          this.labOnOffTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labStartFlag
          // 
          this.labStartFlag.AutoSize = true;
          this.labStartFlag.BackColor = System.Drawing.Color.White;
          this.labStartFlag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labStartFlag.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labStartFlag.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labStartFlag.ForeColor = System.Drawing.Color.Black;
          this.labStartFlag.Location = new System.Drawing.Point(425, 1);
          this.labStartFlag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labStartFlag.Name = "labStartFlag";
          this.labStartFlag.Size = new System.Drawing.Size(75, 54);
          this.labStartFlag.TabIndex = 12;
          this.labStartFlag.Text = "0";
          this.labStartFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labOnOffCycle
          // 
          this.labOnOffCycle.AutoSize = true;
          this.labOnOffCycle.BackColor = System.Drawing.Color.White;
          this.labOnOffCycle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labOnOffCycle.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labOnOffCycle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labOnOffCycle.ForeColor = System.Drawing.Color.Black;
          this.labOnOffCycle.Location = new System.Drawing.Point(425, 56);
          this.labOnOffCycle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labOnOffCycle.Name = "labOnOffCycle";
          this.labOnOffCycle.Size = new System.Drawing.Size(75, 54);
          this.labOnOffCycle.TabIndex = 13;
          this.labOnOffCycle.Text = "0";
          this.labOnOffCycle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labFinishFlag
          // 
          this.labFinishFlag.AutoSize = true;
          this.labFinishFlag.BackColor = System.Drawing.Color.White;
          this.labFinishFlag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labFinishFlag.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labFinishFlag.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labFinishFlag.ForeColor = System.Drawing.Color.Black;
          this.labFinishFlag.Location = new System.Drawing.Point(593, 1);
          this.labFinishFlag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labFinishFlag.Name = "labFinishFlag";
          this.labFinishFlag.Size = new System.Drawing.Size(76, 54);
          this.labFinishFlag.TabIndex = 14;
          this.labFinishFlag.Text = "0";
          this.labFinishFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labRunFlag
          // 
          this.labRunFlag.AutoSize = true;
          this.labRunFlag.BackColor = System.Drawing.Color.White;
          this.labRunFlag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labRunFlag.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labRunFlag.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labRunFlag.ForeColor = System.Drawing.Color.Black;
          this.labRunFlag.Location = new System.Drawing.Point(593, 56);
          this.labRunFlag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labRunFlag.Name = "labRunFlag";
          this.labRunFlag.Size = new System.Drawing.Size(76, 54);
          this.labRunFlag.TabIndex = 15;
          this.labRunFlag.Text = "0";
          this.labRunFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label73
          // 
          this.label73.AutoSize = true;
          this.label73.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label73.Location = new System.Drawing.Point(5, 111);
          this.label73.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label73.Name = "label73";
          this.label73.Size = new System.Drawing.Size(75, 55);
          this.label73.TabIndex = 16;
          this.label73.Text = "ON/OFF:";
          this.label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labAcOn
          // 
          this.labAcOn.AutoSize = true;
          this.labAcOn.BackColor = System.Drawing.Color.White;
          this.labAcOn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labAcOn.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labAcOn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labAcOn.ForeColor = System.Drawing.Color.Black;
          this.labAcOn.Location = new System.Drawing.Point(89, 111);
          this.labAcOn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labAcOn.Name = "labAcOn";
          this.labAcOn.Size = new System.Drawing.Size(75, 55);
          this.labAcOn.TabIndex = 17;
          this.labAcOn.Text = "OFF";
          this.labAcOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label75
          // 
          this.label75.AutoSize = true;
          this.label75.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label75.Location = new System.Drawing.Point(173, 111);
          this.label75.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label75.Name = "label75";
          this.label75.Size = new System.Drawing.Size(75, 55);
          this.label75.TabIndex = 18;
          this.label75.Text = "触点同步信号:";
          this.label75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label76
          // 
          this.label76.AutoSize = true;
          this.label76.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label76.Location = new System.Drawing.Point(341, 111);
          this.label76.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label76.Name = "label76";
          this.label76.Size = new System.Drawing.Size(75, 55);
          this.label76.TabIndex = 19;
          this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // labRelayON
          // 
          this.labRelayON.AutoSize = true;
          this.labRelayON.BackColor = System.Drawing.Color.White;
          this.labRelayON.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labRelayON.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labRelayON.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labRelayON.ForeColor = System.Drawing.Color.Black;
          this.labRelayON.Location = new System.Drawing.Point(257, 111);
          this.labRelayON.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labRelayON.Name = "labRelayON";
          this.labRelayON.Size = new System.Drawing.Size(75, 55);
          this.labRelayON.TabIndex = 20;
          this.labRelayON.Text = "OFF";
          this.labRelayON.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // panel6
          // 
          this.panel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel6.ColumnCount = 8;
          this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.00527F));
          this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.77504F));
          this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
          this.panel6.Controls.Add(this.label9, 0, 0);
          this.panel6.Controls.Add(this.txtBIToTime, 1, 0);
          this.panel6.Controls.Add(this.label11, 0, 1);
          this.panel6.Controls.Add(this.label13, 0, 2);
          this.panel6.Controls.Add(this.label15, 0, 3);
          this.panel6.Controls.Add(this.label19, 2, 1);
          this.panel6.Controls.Add(this.label21, 2, 2);
          this.panel6.Controls.Add(this.label22, 2, 3);
          this.panel6.Controls.Add(this.label26, 4, 1);
          this.panel6.Controls.Add(this.label29, 4, 2);
          this.panel6.Controls.Add(this.label30, 4, 3);
          this.panel6.Controls.Add(this.label35, 6, 1);
          this.panel6.Controls.Add(this.label37, 6, 2);
          this.panel6.Controls.Add(this.label39, 6, 3);
          this.panel6.Controls.Add(this.txtOnOff1, 1, 1);
          this.panel6.Controls.Add(this.txtOn1, 1, 2);
          this.panel6.Controls.Add(this.txtOff1, 1, 3);
          this.panel6.Controls.Add(this.txtOnOff2, 3, 1);
          this.panel6.Controls.Add(this.txtOn2, 3, 2);
          this.panel6.Controls.Add(this.txtOff2, 3, 3);
          this.panel6.Controls.Add(this.txtOnOff3, 5, 1);
          this.panel6.Controls.Add(this.txtOn3, 5, 2);
          this.panel6.Controls.Add(this.txtOff3, 5, 3);
          this.panel6.Controls.Add(this.txtOnOff4, 7, 1);
          this.panel6.Controls.Add(this.txtOn4, 7, 2);
          this.panel6.Controls.Add(this.txtOff4, 7, 3);
          this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel6.Location = new System.Drawing.Point(5, 5);
          this.panel6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.panel6.Name = "panel6";
          this.panel6.RowCount = 4;
          this.panel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
          this.panel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
          this.panel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
          this.panel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
          this.panel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
          this.panel6.Size = new System.Drawing.Size(674, 167);
          this.panel6.TabIndex = 5;
          // 
          // label9
          // 
          this.label9.AutoSize = true;
          this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label9.Location = new System.Drawing.Point(5, 1);
          this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label9.Name = "label9";
          this.label9.Size = new System.Drawing.Size(78, 40);
          this.label9.TabIndex = 0;
          this.label9.Text = "总时间(M):";
          this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // txtBIToTime
          // 
          this.txtBIToTime.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtBIToTime.Location = new System.Drawing.Point(92, 5);
          this.txtBIToTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtBIToTime.Name = "txtBIToTime";
          this.txtBIToTime.Size = new System.Drawing.Size(70, 25);
          this.txtBIToTime.TabIndex = 1;
          this.txtBIToTime.Text = "10";
          this.txtBIToTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // label11
          // 
          this.label11.AutoSize = true;
          this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label11.Location = new System.Drawing.Point(5, 42);
          this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label11.Name = "label11";
          this.label11.Size = new System.Drawing.Size(78, 40);
          this.label11.TabIndex = 2;
          this.label11.Text = "ONOFF1:";
          this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label13
          // 
          this.label13.AutoSize = true;
          this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label13.Location = new System.Drawing.Point(5, 83);
          this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label13.Name = "label13";
          this.label13.Size = new System.Drawing.Size(78, 40);
          this.label13.TabIndex = 3;
          this.label13.Text = "ON1(秒):";
          this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label15
          // 
          this.label15.AutoSize = true;
          this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label15.Location = new System.Drawing.Point(5, 124);
          this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label15.Name = "label15";
          this.label15.Size = new System.Drawing.Size(78, 42);
          this.label15.TabIndex = 4;
          this.label15.Text = "OFF1(秒):";
          this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label19
          // 
          this.label19.AutoSize = true;
          this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label19.Location = new System.Drawing.Point(171, 42);
          this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label19.Name = "label19";
          this.label19.Size = new System.Drawing.Size(75, 40);
          this.label19.TabIndex = 6;
          this.label19.Text = "ONOFF2:";
          this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label21
          // 
          this.label21.AutoSize = true;
          this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label21.Location = new System.Drawing.Point(171, 83);
          this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label21.Name = "label21";
          this.label21.Size = new System.Drawing.Size(75, 40);
          this.label21.TabIndex = 7;
          this.label21.Text = "ON1(秒):";
          this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label22
          // 
          this.label22.AutoSize = true;
          this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label22.Location = new System.Drawing.Point(171, 124);
          this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label22.Name = "label22";
          this.label22.Size = new System.Drawing.Size(75, 42);
          this.label22.TabIndex = 8;
          this.label22.Text = "OFF2(秒):";
          this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label26
          // 
          this.label26.AutoSize = true;
          this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label26.Location = new System.Drawing.Point(339, 42);
          this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label26.Name = "label26";
          this.label26.Size = new System.Drawing.Size(75, 40);
          this.label26.TabIndex = 10;
          this.label26.Text = "ONOFF3:";
          this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label29
          // 
          this.label29.AutoSize = true;
          this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label29.Location = new System.Drawing.Point(339, 83);
          this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label29.Name = "label29";
          this.label29.Size = new System.Drawing.Size(75, 40);
          this.label29.TabIndex = 11;
          this.label29.Text = "ON3(秒):";
          this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label30
          // 
          this.label30.AutoSize = true;
          this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label30.Location = new System.Drawing.Point(339, 124);
          this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label30.Name = "label30";
          this.label30.Size = new System.Drawing.Size(75, 42);
          this.label30.TabIndex = 12;
          this.label30.Text = "OFF3(秒):";
          this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label35
          // 
          this.label35.AutoSize = true;
          this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label35.Location = new System.Drawing.Point(507, 42);
          this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label35.Name = "label35";
          this.label35.Size = new System.Drawing.Size(75, 40);
          this.label35.TabIndex = 14;
          this.label35.Text = "ONOFF4:";
          this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label37
          // 
          this.label37.AutoSize = true;
          this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label37.Location = new System.Drawing.Point(507, 83);
          this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label37.Name = "label37";
          this.label37.Size = new System.Drawing.Size(75, 40);
          this.label37.TabIndex = 15;
          this.label37.Text = "ON4(秒):";
          this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // label39
          // 
          this.label39.AutoSize = true;
          this.label39.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label39.Location = new System.Drawing.Point(507, 124);
          this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label39.Name = "label39";
          this.label39.Size = new System.Drawing.Size(75, 42);
          this.label39.TabIndex = 16;
          this.label39.Text = "OFF4(秒):";
          this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // txtOnOff1
          // 
          this.txtOnOff1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOnOff1.Location = new System.Drawing.Point(92, 46);
          this.txtOnOff1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOnOff1.Name = "txtOnOff1";
          this.txtOnOff1.Size = new System.Drawing.Size(70, 25);
          this.txtOnOff1.TabIndex = 17;
          this.txtOnOff1.Text = "1";
          this.txtOnOff1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOn1
          // 
          this.txtOn1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOn1.Location = new System.Drawing.Point(92, 87);
          this.txtOn1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOn1.Name = "txtOn1";
          this.txtOn1.Size = new System.Drawing.Size(70, 25);
          this.txtOn1.TabIndex = 18;
          this.txtOn1.Text = "60";
          this.txtOn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOff1
          // 
          this.txtOff1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOff1.Location = new System.Drawing.Point(92, 128);
          this.txtOff1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOff1.Name = "txtOff1";
          this.txtOff1.Size = new System.Drawing.Size(70, 25);
          this.txtOff1.TabIndex = 19;
          this.txtOff1.Text = "0";
          this.txtOff1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOnOff2
          // 
          this.txtOnOff2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOnOff2.Location = new System.Drawing.Point(255, 46);
          this.txtOnOff2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOnOff2.Name = "txtOnOff2";
          this.txtOnOff2.Size = new System.Drawing.Size(75, 25);
          this.txtOnOff2.TabIndex = 20;
          this.txtOnOff2.Text = "0";
          this.txtOnOff2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOn2
          // 
          this.txtOn2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOn2.Location = new System.Drawing.Point(255, 87);
          this.txtOn2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOn2.Name = "txtOn2";
          this.txtOn2.Size = new System.Drawing.Size(75, 25);
          this.txtOn2.TabIndex = 21;
          this.txtOn2.Text = "0";
          this.txtOn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOff2
          // 
          this.txtOff2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOff2.Location = new System.Drawing.Point(255, 128);
          this.txtOff2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOff2.Name = "txtOff2";
          this.txtOff2.Size = new System.Drawing.Size(75, 25);
          this.txtOff2.TabIndex = 22;
          this.txtOff2.Text = "0";
          this.txtOff2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOnOff3
          // 
          this.txtOnOff3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOnOff3.Location = new System.Drawing.Point(423, 46);
          this.txtOnOff3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOnOff3.Name = "txtOnOff3";
          this.txtOnOff3.Size = new System.Drawing.Size(75, 25);
          this.txtOnOff3.TabIndex = 23;
          this.txtOnOff3.Text = "0";
          this.txtOnOff3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOn3
          // 
          this.txtOn3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOn3.Location = new System.Drawing.Point(423, 87);
          this.txtOn3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOn3.Name = "txtOn3";
          this.txtOn3.Size = new System.Drawing.Size(75, 25);
          this.txtOn3.TabIndex = 24;
          this.txtOn3.Text = "0";
          this.txtOn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOff3
          // 
          this.txtOff3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOff3.Location = new System.Drawing.Point(423, 128);
          this.txtOff3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOff3.Name = "txtOff3";
          this.txtOff3.Size = new System.Drawing.Size(75, 25);
          this.txtOff3.TabIndex = 25;
          this.txtOff3.Text = "0";
          this.txtOff3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOnOff4
          // 
          this.txtOnOff4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOnOff4.Location = new System.Drawing.Point(591, 46);
          this.txtOnOff4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOnOff4.Name = "txtOnOff4";
          this.txtOnOff4.Size = new System.Drawing.Size(78, 25);
          this.txtOnOff4.TabIndex = 26;
          this.txtOnOff4.Text = "0";
          this.txtOnOff4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOn4
          // 
          this.txtOn4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOn4.Location = new System.Drawing.Point(591, 87);
          this.txtOn4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOn4.Name = "txtOn4";
          this.txtOn4.Size = new System.Drawing.Size(78, 25);
          this.txtOn4.TabIndex = 27;
          this.txtOn4.Text = "0";
          this.txtOn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtOff4
          // 
          this.txtOff4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtOff4.Location = new System.Drawing.Point(591, 128);
          this.txtOff4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtOff4.Name = "txtOff4";
          this.txtOff4.Size = new System.Drawing.Size(78, 25);
          this.txtOff4.TabIndex = 28;
          this.txtOff4.Text = "0";
          this.txtOff4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // udcGJMon32
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.splitContainer1);
          this.DoubleBuffered = true;
          this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.Name = "udcGJMon32";
          this.Size = new System.Drawing.Size(1379, 845);
          this.Load += new System.EventHandler(this.udcGJMon32_Load);
          this.splitContainer1.Panel1.ResumeLayout(false);
          this.splitContainer1.Panel2.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
          this.splitContainer1.ResumeLayout(false);
          this.panel1.ResumeLayout(false);
          this.panel1.PerformLayout();
          this.panel2.ResumeLayout(false);
          this.panel3.ResumeLayout(false);
          this.panel3.PerformLayout();
          this.panel4.ResumeLayout(false);
          this.panel4.PerformLayout();
          this.panel10.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.gridMon)).EndInit();
          this.panel11.ResumeLayout(false);
          this.panel9.ResumeLayout(false);
          this.panel9.PerformLayout();
          this.panel7.ResumeLayout(false);
          this.panel7.PerformLayout();
          this.panel5.ResumeLayout(false);
          this.panel5.PerformLayout();
          this.panel8.ResumeLayout(false);
          this.panel8.PerformLayout();
          this.panel6.ResumeLayout(false);
          this.panel6.PerformLayout();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.TableLayoutPanel panel1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ComboBox cmbCOM;
      private System.Windows.Forms.Button btnOpen;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox txtAddr;
      private System.Windows.Forms.Button btnSet;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label labStatus;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label labVersion;
      private System.Windows.Forms.TableLayoutPanel panel2;
      private System.Windows.Forms.TableLayoutPanel panel3;
      private System.Windows.Forms.Label labV12;
      private System.Windows.Forms.Label label61;
      private System.Windows.Forms.Label labV16;
      private System.Windows.Forms.Label label54;
      private System.Windows.Forms.Label labV4;
      private System.Windows.Forms.Label label52;
      private System.Windows.Forms.Label labV8;
      private System.Windows.Forms.Label label50;
      private System.Windows.Forms.Label labV28;
      private System.Windows.Forms.Label label48;
      private System.Windows.Forms.Label labV32;
      private System.Windows.Forms.Label label46;
      private System.Windows.Forms.Label labV20;
      private System.Windows.Forms.Label label44;
      private System.Windows.Forms.Label labV24;
      private System.Windows.Forms.Label label42;
      private System.Windows.Forms.Label label33;
      private System.Windows.Forms.Label labV15;
      private System.Windows.Forms.Label label31;
      private System.Windows.Forms.Label labV19;
      private System.Windows.Forms.Label labV23;
      private System.Windows.Forms.Label label28;
      private System.Windows.Forms.Label label27;
      private System.Windows.Forms.Label labV3;
      private System.Windows.Forms.Label labV11;
      private System.Windows.Forms.Label label24;
      private System.Windows.Forms.Label label23;
      private System.Windows.Forms.Label labV7;
      private System.Windows.Forms.Label labV6;
      private System.Windows.Forms.Label label20;
      private System.Windows.Forms.Label labV2;
      private System.Windows.Forms.Label label18;
      private System.Windows.Forms.Label labV21;
      private System.Windows.Forms.Label label16;
      private System.Windows.Forms.Label labV17;
      private System.Windows.Forms.Label label14;
      private System.Windows.Forms.Label labV13;
      private System.Windows.Forms.Label label12;
      private System.Windows.Forms.Label labV9;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Label labV5;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label labV1;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label34;
      private System.Windows.Forms.Label labV10;
      private System.Windows.Forms.Label label36;
      private System.Windows.Forms.Label labV14;
      private System.Windows.Forms.Label label38;
      private System.Windows.Forms.Label labV18;
      private System.Windows.Forms.Label label40;
      private System.Windows.Forms.Label labV22;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label56;
      private System.Windows.Forms.Label label57;
      private System.Windows.Forms.Label labV25;
      private System.Windows.Forms.Label labV26;
      private System.Windows.Forms.Label labV27;
      private System.Windows.Forms.Label label63;
      private System.Windows.Forms.Label label64;
      private System.Windows.Forms.Label label65;
      private System.Windows.Forms.Label labV29;
      private System.Windows.Forms.Label labV30;
      private System.Windows.Forms.Label labV31;
      private System.Windows.Forms.TableLayoutPanel panel4;
      private System.Windows.Forms.Label label69;
      private System.Windows.Forms.Button btnVer;
      private System.Windows.Forms.Button btnVolt;
      private System.Windows.Forms.CheckBox chkSync;
      private System.Windows.Forms.Label label70;
      private System.Windows.Forms.Label labOnOff;
      private System.Windows.Forms.CheckBox chkMode;
      private System.Windows.Forms.TableLayoutPanel panel10;
      private System.Windows.Forms.TableLayoutPanel panel11;
      private System.Windows.Forms.TableLayoutPanel panel9;
      private System.Windows.Forms.Label label59;
      private System.Windows.Forms.Label labErrCode;
      private System.Windows.Forms.Label label60;
      private System.Windows.Forms.TextBox txtRelayNo;
      private System.Windows.Forms.Button btnRlyON;
      private System.Windows.Forms.Button btnRlyOff;
      private System.Windows.Forms.TableLayoutPanel panel7;
      private System.Windows.Forms.Label label17;
      private System.Windows.Forms.Button btnStart;
      private System.Windows.Forms.Button btnReadSgn;
      private System.Windows.Forms.Button btnStop;
      private System.Windows.Forms.Label label25;
      private System.Windows.Forms.Label label32;
      private System.Windows.Forms.Label label41;
      private System.Windows.Forms.Label labX1;
      private System.Windows.Forms.Label labX2;
      private System.Windows.Forms.Label labX3;
      private System.Windows.Forms.Label labX4;
      private System.Windows.Forms.Label labX5;
      private System.Windows.Forms.Label labX6;
      private System.Windows.Forms.Label labX7;
      private System.Windows.Forms.Label labX8;
      private System.Windows.Forms.Label labS1;
      private System.Windows.Forms.Label labS2;
      private System.Windows.Forms.Label labX9;
      private System.Windows.Forms.TableLayoutPanel panel5;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Button btnSetPara;
      private System.Windows.Forms.Button btnReadPara;
      private System.Windows.Forms.TableLayoutPanel panel8;
      private System.Windows.Forms.Label label43;
      private System.Windows.Forms.Label label45;
      private System.Windows.Forms.Label label47;
      private System.Windows.Forms.Label label49;
      private System.Windows.Forms.Label label51;
      private System.Windows.Forms.Label label53;
      private System.Windows.Forms.Label label55;
      private System.Windows.Forms.Label label58;
      private System.Windows.Forms.Label labRunMin;
      private System.Windows.Forms.Label labRunOnOff;
      private System.Windows.Forms.Label labRunSec;
      private System.Windows.Forms.Label labOnOffTime;
      private System.Windows.Forms.Label labStartFlag;
      private System.Windows.Forms.Label labOnOffCycle;
      private System.Windows.Forms.Label labFinishFlag;
      private System.Windows.Forms.Label labRunFlag;
      private System.Windows.Forms.Label label73;
      private System.Windows.Forms.Label labAcOn;
      private System.Windows.Forms.Label label75;
      private System.Windows.Forms.Label label76;
      private System.Windows.Forms.Label labRelayON;
      private System.Windows.Forms.TableLayoutPanel panel6;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.TextBox txtBIToTime;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.Label label13;
      private System.Windows.Forms.Label label15;
      private System.Windows.Forms.Label label19;
      private System.Windows.Forms.Label label21;
      private System.Windows.Forms.Label label22;
      private System.Windows.Forms.Label label26;
      private System.Windows.Forms.Label label29;
      private System.Windows.Forms.Label label30;
      private System.Windows.Forms.Label label35;
      private System.Windows.Forms.Label label37;
      private System.Windows.Forms.Label label39;
      private System.Windows.Forms.TextBox txtOnOff1;
      private System.Windows.Forms.TextBox txtOn1;
      private System.Windows.Forms.TextBox txtOff1;
      private System.Windows.Forms.TextBox txtOnOff2;
      private System.Windows.Forms.TextBox txtOn2;
      private System.Windows.Forms.TextBox txtOff2;
      private System.Windows.Forms.TextBox txtOnOff3;
      private System.Windows.Forms.TextBox txtOn3;
      private System.Windows.Forms.TextBox txtOff3;
      private System.Windows.Forms.TextBox txtOnOff4;
      private System.Windows.Forms.TextBox txtOn4;
      private System.Windows.Forms.TextBox txtOff4;
      private System.Windows.Forms.Button btnScan;
      private System.Windows.Forms.Label label62;
      private System.Windows.Forms.TextBox txtEndAddr;
      private System.Windows.Forms.DataGridView gridMon;
      private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
      private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
      private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
      private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
   }
}
