﻿namespace GJ.Tool
{
   partial class udcELoad
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
          this.panel3 = new System.Windows.Forms.TableLayoutPanel();
          this.label4 = new System.Windows.Forms.Label();
          this.btnReadData = new System.Windows.Forms.Button();
          this.btnReadSet = new System.Windows.Forms.Button();
          this.btnSetLoad = new System.Windows.Forms.Button();
          this.btnScan = new System.Windows.Forms.Button();
          this.txtEndAddr = new System.Windows.Forms.TextBox();
          this.label14 = new System.Windows.Forms.Label();
          this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
          this.gridMon = new System.Windows.Forms.DataGridView();
          this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.panel4 = new System.Windows.Forms.TableLayoutPanel();
          this.label5 = new System.Windows.Forms.Label();
          this.label6 = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.label8 = new System.Windows.Forms.Label();
          this.label9 = new System.Windows.Forms.Label();
          this.label10 = new System.Windows.Forms.Label();
          this.label13 = new System.Windows.Forms.Label();
          this.labCH1 = new System.Windows.Forms.Label();
          this.labCH2 = new System.Windows.Forms.Label();
          this.labCH3 = new System.Windows.Forms.Label();
          this.labCH4 = new System.Windows.Forms.Label();
          this.labCH5 = new System.Windows.Forms.Label();
          this.labCH6 = new System.Windows.Forms.Label();
          this.labCH7 = new System.Windows.Forms.Label();
          this.labCH8 = new System.Windows.Forms.Label();
          this.cmbMode1 = new System.Windows.Forms.ComboBox();
          this.cmbMode2 = new System.Windows.Forms.ComboBox();
          this.cmbMode3 = new System.Windows.Forms.ComboBox();
          this.cmbMode4 = new System.Windows.Forms.ComboBox();
          this.cmbMode5 = new System.Windows.Forms.ComboBox();
          this.cmbMode6 = new System.Windows.Forms.ComboBox();
          this.cmbMode7 = new System.Windows.Forms.ComboBox();
          this.cmbMode8 = new System.Windows.Forms.ComboBox();
          this.txtVon1 = new System.Windows.Forms.TextBox();
          this.txtVon2 = new System.Windows.Forms.TextBox();
          this.txtVon3 = new System.Windows.Forms.TextBox();
          this.txtVon4 = new System.Windows.Forms.TextBox();
          this.txtVon5 = new System.Windows.Forms.TextBox();
          this.txtVon6 = new System.Windows.Forms.TextBox();
          this.txtVon7 = new System.Windows.Forms.TextBox();
          this.txtVon8 = new System.Windows.Forms.TextBox();
          this.txtLoad1 = new System.Windows.Forms.TextBox();
          this.txtLoad2 = new System.Windows.Forms.TextBox();
          this.txtLoad3 = new System.Windows.Forms.TextBox();
          this.txtLoad4 = new System.Windows.Forms.TextBox();
          this.txtLoad5 = new System.Windows.Forms.TextBox();
          this.txtLoad6 = new System.Windows.Forms.TextBox();
          this.txtLoad7 = new System.Windows.Forms.TextBox();
          this.txtLoad8 = new System.Windows.Forms.TextBox();
          this.labVs1 = new System.Windows.Forms.Label();
          this.labVs2 = new System.Windows.Forms.Label();
          this.labVs3 = new System.Windows.Forms.Label();
          this.labVs4 = new System.Windows.Forms.Label();
          this.labVs5 = new System.Windows.Forms.Label();
          this.labVs6 = new System.Windows.Forms.Label();
          this.labVs7 = new System.Windows.Forms.Label();
          this.labVs8 = new System.Windows.Forms.Label();
          this.labV1 = new System.Windows.Forms.Label();
          this.labV2 = new System.Windows.Forms.Label();
          this.labV3 = new System.Windows.Forms.Label();
          this.labV4 = new System.Windows.Forms.Label();
          this.labV5 = new System.Windows.Forms.Label();
          this.labV6 = new System.Windows.Forms.Label();
          this.labV7 = new System.Windows.Forms.Label();
          this.labV8 = new System.Windows.Forms.Label();
          this.labCur1 = new System.Windows.Forms.Label();
          this.labCur2 = new System.Windows.Forms.Label();
          this.labCur3 = new System.Windows.Forms.Label();
          this.labCur4 = new System.Windows.Forms.Label();
          this.labCur5 = new System.Windows.Forms.Label();
          this.labCur6 = new System.Windows.Forms.Label();
          this.labCur7 = new System.Windows.Forms.Label();
          this.labCur8 = new System.Windows.Forms.Label();
          this.label46 = new System.Windows.Forms.Label();
          this.labSatus = new System.Windows.Forms.Label();
          this.label48 = new System.Windows.Forms.Label();
          this.labT0 = new System.Windows.Forms.Label();
          this.label50 = new System.Windows.Forms.Label();
          this.labT1 = new System.Windows.Forms.Label();
          this.label52 = new System.Windows.Forms.Label();
          this.label53 = new System.Windows.Forms.Label();
          this.labOnOff = new System.Windows.Forms.Label();
          this.labOCP = new System.Windows.Forms.Label();
          this.label56 = new System.Windows.Forms.Label();
          this.labOVP = new System.Windows.Forms.Label();
          this.label58 = new System.Windows.Forms.Label();
          this.labOPP = new System.Windows.Forms.Label();
          this.label60 = new System.Windows.Forms.Label();
          this.labOTP = new System.Windows.Forms.Label();
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
          this.panel3.SuspendLayout();
          this.tableLayoutPanel1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.gridMon)).BeginInit();
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
          this.panel2.Controls.Add(this.panel3, 0, 0);
          this.panel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
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
          // panel3
          // 
          this.panel3.ColumnCount = 7;
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
          this.panel3.Controls.Add(this.label4, 0, 0);
          this.panel3.Controls.Add(this.btnReadData, 5, 0);
          this.panel3.Controls.Add(this.btnReadSet, 4, 0);
          this.panel3.Controls.Add(this.btnSetLoad, 3, 0);
          this.panel3.Controls.Add(this.btnScan, 6, 0);
          this.panel3.Controls.Add(this.txtEndAddr, 2, 0);
          this.panel3.Controls.Add(this.label14, 1, 0);
          this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.panel3.Location = new System.Drawing.Point(1, 1);
          this.panel3.Margin = new System.Windows.Forms.Padding(0);
          this.panel3.Name = "panel3";
          this.panel3.RowCount = 1;
          this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel3.Size = new System.Drawing.Size(1210, 44);
          this.panel3.TabIndex = 3;
          // 
          // label4
          // 
          this.label4.AutoSize = true;
          this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label4.Location = new System.Drawing.Point(4, 4);
          this.label4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(560, 36);
          this.label4.TabIndex = 1;
          this.label4.Text = "功能:负载设置和读取";
          this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // btnReadData
          // 
          this.btnReadData.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnReadData.Location = new System.Drawing.Point(996, 0);
          this.btnReadData.Margin = new System.Windows.Forms.Padding(0);
          this.btnReadData.Name = "btnReadData";
          this.btnReadData.Size = new System.Drawing.Size(107, 44);
          this.btnReadData.TabIndex = 2;
          this.btnReadData.Text = "读取数据";
          this.btnReadData.UseVisualStyleBackColor = true;
          this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
          // 
          // btnReadSet
          // 
          this.btnReadSet.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnReadSet.Location = new System.Drawing.Point(889, 0);
          this.btnReadSet.Margin = new System.Windows.Forms.Padding(0);
          this.btnReadSet.Name = "btnReadSet";
          this.btnReadSet.Size = new System.Drawing.Size(107, 44);
          this.btnReadSet.TabIndex = 3;
          this.btnReadSet.Text = "读取设置";
          this.btnReadSet.UseVisualStyleBackColor = true;
          this.btnReadSet.Click += new System.EventHandler(this.btnReadSet_Click);
          // 
          // btnSetLoad
          // 
          this.btnSetLoad.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnSetLoad.Location = new System.Drawing.Point(782, 0);
          this.btnSetLoad.Margin = new System.Windows.Forms.Padding(0);
          this.btnSetLoad.Name = "btnSetLoad";
          this.btnSetLoad.Size = new System.Drawing.Size(107, 44);
          this.btnSetLoad.TabIndex = 4;
          this.btnSetLoad.Text = "设置负载";
          this.btnSetLoad.UseVisualStyleBackColor = true;
          this.btnSetLoad.Click += new System.EventHandler(this.btnSetLoad_Click);
          // 
          // btnScan
          // 
          this.btnScan.Dock = System.Windows.Forms.DockStyle.Fill;
          this.btnScan.Location = new System.Drawing.Point(1103, 0);
          this.btnScan.Margin = new System.Windows.Forms.Padding(0);
          this.btnScan.Name = "btnScan";
          this.btnScan.Size = new System.Drawing.Size(107, 44);
          this.btnScan.TabIndex = 5;
          this.btnScan.Text = "扫描监控";
          this.btnScan.UseVisualStyleBackColor = true;
          this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
          // 
          // txtEndAddr
          // 
          this.txtEndAddr.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtEndAddr.Location = new System.Drawing.Point(679, 8);
          this.txtEndAddr.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
          this.txtEndAddr.Name = "txtEndAddr";
          this.txtEndAddr.Size = new System.Drawing.Size(99, 25);
          this.txtEndAddr.TabIndex = 6;
          this.txtEndAddr.Text = "1";
          this.txtEndAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // label14
          // 
          this.label14.AutoSize = true;
          this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label14.Location = new System.Drawing.Point(572, 4);
          this.label14.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.label14.Name = "label14";
          this.label14.Size = new System.Drawing.Size(99, 36);
          this.label14.TabIndex = 7;
          this.label14.Text = "结束地址:";
          this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // tableLayoutPanel1
          // 
          this.tableLayoutPanel1.ColumnCount = 2;
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.9434F));
          this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.0566F));
          this.tableLayoutPanel1.Controls.Add(this.gridMon, 0, 0);
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
          // gridMon
          // 
          this.gridMon.BackgroundColor = System.Drawing.Color.White;
          this.gridMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.gridMon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
          this.gridMon.Dock = System.Windows.Forms.DockStyle.Fill;
          this.gridMon.Location = new System.Drawing.Point(616, 4);
          this.gridMon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.gridMon.Name = "gridMon";
          this.gridMon.RowHeadersVisible = false;
          this.gridMon.RowHeadersWidth = 20;
          this.gridMon.RowTemplate.Height = 23;
          this.gridMon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
          this.gridMon.Size = new System.Drawing.Size(582, 523);
          this.gridMon.TabIndex = 31;
          // 
          // Column1
          // 
          this.Column1.HeaderText = "地址";
          this.Column1.Name = "Column1";
          this.Column1.Width = 56;
          // 
          // Column2
          // 
          this.Column2.HeaderText = "通信";
          this.Column2.Name = "Column2";
          this.Column2.Width = 56;
          // 
          // Column3
          // 
          this.Column3.HeaderText = "状态";
          this.Column3.Name = "Column3";
          this.Column3.Width = 90;
          // 
          // Column4
          // 
          this.Column4.HeaderText = "读值";
          this.Column4.Name = "Column4";
          this.Column4.Width = 700;
          // 
          // panel4
          // 
          this.panel4.BackColor = System.Drawing.SystemColors.Control;
          this.panel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
          this.panel4.ColumnCount = 7;
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
          this.panel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
          this.panel4.Controls.Add(this.label5, 0, 0);
          this.panel4.Controls.Add(this.label6, 1, 0);
          this.panel4.Controls.Add(this.label7, 2, 0);
          this.panel4.Controls.Add(this.label8, 3, 0);
          this.panel4.Controls.Add(this.label9, 4, 0);
          this.panel4.Controls.Add(this.label10, 5, 0);
          this.panel4.Controls.Add(this.label13, 6, 0);
          this.panel4.Controls.Add(this.labCH1, 0, 1);
          this.panel4.Controls.Add(this.labCH2, 0, 2);
          this.panel4.Controls.Add(this.labCH3, 0, 3);
          this.panel4.Controls.Add(this.labCH4, 0, 4);
          this.panel4.Controls.Add(this.labCH5, 0, 5);
          this.panel4.Controls.Add(this.labCH6, 0, 6);
          this.panel4.Controls.Add(this.labCH7, 0, 7);
          this.panel4.Controls.Add(this.labCH8, 0, 8);
          this.panel4.Controls.Add(this.cmbMode1, 1, 1);
          this.panel4.Controls.Add(this.cmbMode2, 1, 2);
          this.panel4.Controls.Add(this.cmbMode3, 1, 3);
          this.panel4.Controls.Add(this.cmbMode4, 1, 4);
          this.panel4.Controls.Add(this.cmbMode5, 1, 5);
          this.panel4.Controls.Add(this.cmbMode6, 1, 6);
          this.panel4.Controls.Add(this.cmbMode7, 1, 7);
          this.panel4.Controls.Add(this.cmbMode8, 1, 8);
          this.panel4.Controls.Add(this.txtVon1, 2, 1);
          this.panel4.Controls.Add(this.txtVon2, 2, 2);
          this.panel4.Controls.Add(this.txtVon3, 2, 3);
          this.panel4.Controls.Add(this.txtVon4, 2, 4);
          this.panel4.Controls.Add(this.txtVon5, 2, 5);
          this.panel4.Controls.Add(this.txtVon6, 2, 6);
          this.panel4.Controls.Add(this.txtVon7, 2, 7);
          this.panel4.Controls.Add(this.txtVon8, 2, 8);
          this.panel4.Controls.Add(this.txtLoad1, 3, 1);
          this.panel4.Controls.Add(this.txtLoad2, 3, 2);
          this.panel4.Controls.Add(this.txtLoad3, 3, 3);
          this.panel4.Controls.Add(this.txtLoad4, 3, 4);
          this.panel4.Controls.Add(this.txtLoad5, 3, 5);
          this.panel4.Controls.Add(this.txtLoad6, 3, 6);
          this.panel4.Controls.Add(this.txtLoad7, 3, 7);
          this.panel4.Controls.Add(this.txtLoad8, 3, 8);
          this.panel4.Controls.Add(this.labVs1, 4, 1);
          this.panel4.Controls.Add(this.labVs2, 4, 2);
          this.panel4.Controls.Add(this.labVs3, 4, 3);
          this.panel4.Controls.Add(this.labVs4, 4, 4);
          this.panel4.Controls.Add(this.labVs5, 4, 5);
          this.panel4.Controls.Add(this.labVs6, 4, 6);
          this.panel4.Controls.Add(this.labVs7, 4, 7);
          this.panel4.Controls.Add(this.labVs8, 4, 8);
          this.panel4.Controls.Add(this.labV1, 5, 1);
          this.panel4.Controls.Add(this.labV2, 5, 2);
          this.panel4.Controls.Add(this.labV3, 5, 3);
          this.panel4.Controls.Add(this.labV4, 5, 4);
          this.panel4.Controls.Add(this.labV5, 5, 5);
          this.panel4.Controls.Add(this.labV6, 5, 6);
          this.panel4.Controls.Add(this.labV7, 5, 7);
          this.panel4.Controls.Add(this.labV8, 5, 8);
          this.panel4.Controls.Add(this.labCur1, 6, 1);
          this.panel4.Controls.Add(this.labCur2, 6, 2);
          this.panel4.Controls.Add(this.labCur3, 6, 3);
          this.panel4.Controls.Add(this.labCur4, 6, 4);
          this.panel4.Controls.Add(this.labCur5, 6, 5);
          this.panel4.Controls.Add(this.labCur6, 6, 6);
          this.panel4.Controls.Add(this.labCur7, 6, 7);
          this.panel4.Controls.Add(this.labCur8, 6, 8);
          this.panel4.Controls.Add(this.label46, 0, 9);
          this.panel4.Controls.Add(this.labSatus, 1, 9);
          this.panel4.Controls.Add(this.label48, 2, 9);
          this.panel4.Controls.Add(this.labT0, 3, 9);
          this.panel4.Controls.Add(this.label50, 4, 9);
          this.panel4.Controls.Add(this.labT1, 5, 9);
          this.panel4.Controls.Add(this.label52, 2, 10);
          this.panel4.Controls.Add(this.label53, 0, 10);
          this.panel4.Controls.Add(this.labOnOff, 1, 10);
          this.panel4.Controls.Add(this.labOCP, 3, 10);
          this.panel4.Controls.Add(this.label56, 4, 10);
          this.panel4.Controls.Add(this.labOVP, 5, 10);
          this.panel4.Controls.Add(this.label58, 2, 11);
          this.panel4.Controls.Add(this.labOPP, 3, 11);
          this.panel4.Controls.Add(this.label60, 4, 11);
          this.panel4.Controls.Add(this.labOTP, 5, 11);
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
          // label6
          // 
          this.label6.AutoSize = true;
          this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label6.Location = new System.Drawing.Point(83, 1);
          this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(79, 35);
          this.label6.TabIndex = 1;
          this.label6.Text = "负载模式";
          this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label7
          // 
          this.label7.AutoSize = true;
          this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label7.Location = new System.Drawing.Point(171, 1);
          this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(79, 35);
          this.label7.TabIndex = 2;
          this.label7.Text = "Von设置";
          this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label8
          // 
          this.label8.AutoSize = true;
          this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label8.Location = new System.Drawing.Point(259, 1);
          this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label8.Name = "label8";
          this.label8.Size = new System.Drawing.Size(79, 35);
          this.label8.TabIndex = 3;
          this.label8.Text = "负载设置";
          this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label9
          // 
          this.label9.AutoSize = true;
          this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label9.Location = new System.Drawing.Point(347, 1);
          this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label9.Name = "label9";
          this.label9.Size = new System.Drawing.Size(76, 35);
          this.label9.TabIndex = 4;
          this.label9.Text = "Vs电压(V)";
          this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label10
          // 
          this.label10.AutoSize = true;
          this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label10.Location = new System.Drawing.Point(432, 1);
          this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label10.Name = "label10";
          this.label10.Size = new System.Drawing.Size(81, 35);
          this.label10.TabIndex = 5;
          this.label10.Text = "Load电压(V)";
          this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label13
          // 
          this.label13.AutoSize = true;
          this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label13.Location = new System.Drawing.Point(522, 1);
          this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label13.Name = "label13";
          this.label13.Size = new System.Drawing.Size(77, 35);
          this.label13.TabIndex = 6;
          this.label13.Text = "负载读值";
          this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
          // labCH5
          // 
          this.labCH5.AutoSize = true;
          this.labCH5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCH5.Location = new System.Drawing.Point(5, 182);
          this.labCH5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labCH5.Name = "labCH5";
          this.labCH5.Size = new System.Drawing.Size(69, 34);
          this.labCH5.TabIndex = 11;
          this.labCH5.Text = "CH05:";
          this.labCH5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCH6
          // 
          this.labCH6.AutoSize = true;
          this.labCH6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCH6.Location = new System.Drawing.Point(5, 217);
          this.labCH6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labCH6.Name = "labCH6";
          this.labCH6.Size = new System.Drawing.Size(69, 35);
          this.labCH6.TabIndex = 12;
          this.labCH6.Text = "CH06:";
          this.labCH6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCH7
          // 
          this.labCH7.AutoSize = true;
          this.labCH7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCH7.Location = new System.Drawing.Point(5, 253);
          this.labCH7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labCH7.Name = "labCH7";
          this.labCH7.Size = new System.Drawing.Size(69, 35);
          this.labCH7.TabIndex = 13;
          this.labCH7.Text = "CH07:";
          this.labCH7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCH8
          // 
          this.labCH8.AutoSize = true;
          this.labCH8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCH8.Location = new System.Drawing.Point(5, 289);
          this.labCH8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.labCH8.Name = "labCH8";
          this.labCH8.Size = new System.Drawing.Size(69, 35);
          this.labCH8.TabIndex = 14;
          this.labCH8.Text = "CH08:";
          this.labCH8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // cmbMode1
          // 
          this.cmbMode1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cmbMode1.FormattingEnabled = true;
          this.cmbMode1.Location = new System.Drawing.Point(83, 41);
          this.cmbMode1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.cmbMode1.Name = "cmbMode1";
          this.cmbMode1.Size = new System.Drawing.Size(79, 23);
          this.cmbMode1.TabIndex = 15;
          // 
          // cmbMode2
          // 
          this.cmbMode2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cmbMode2.FormattingEnabled = true;
          this.cmbMode2.Location = new System.Drawing.Point(83, 77);
          this.cmbMode2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.cmbMode2.Name = "cmbMode2";
          this.cmbMode2.Size = new System.Drawing.Size(79, 23);
          this.cmbMode2.TabIndex = 16;
          // 
          // cmbMode3
          // 
          this.cmbMode3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cmbMode3.FormattingEnabled = true;
          this.cmbMode3.Location = new System.Drawing.Point(83, 113);
          this.cmbMode3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.cmbMode3.Name = "cmbMode3";
          this.cmbMode3.Size = new System.Drawing.Size(79, 23);
          this.cmbMode3.TabIndex = 17;
          // 
          // cmbMode4
          // 
          this.cmbMode4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cmbMode4.FormattingEnabled = true;
          this.cmbMode4.Location = new System.Drawing.Point(83, 149);
          this.cmbMode4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.cmbMode4.Name = "cmbMode4";
          this.cmbMode4.Size = new System.Drawing.Size(79, 23);
          this.cmbMode4.TabIndex = 18;
          // 
          // cmbMode5
          // 
          this.cmbMode5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cmbMode5.FormattingEnabled = true;
          this.cmbMode5.Location = new System.Drawing.Point(83, 186);
          this.cmbMode5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.cmbMode5.Name = "cmbMode5";
          this.cmbMode5.Size = new System.Drawing.Size(79, 23);
          this.cmbMode5.TabIndex = 19;
          // 
          // cmbMode6
          // 
          this.cmbMode6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cmbMode6.FormattingEnabled = true;
          this.cmbMode6.Location = new System.Drawing.Point(83, 221);
          this.cmbMode6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.cmbMode6.Name = "cmbMode6";
          this.cmbMode6.Size = new System.Drawing.Size(79, 23);
          this.cmbMode6.TabIndex = 20;
          // 
          // cmbMode7
          // 
          this.cmbMode7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cmbMode7.FormattingEnabled = true;
          this.cmbMode7.Location = new System.Drawing.Point(83, 257);
          this.cmbMode7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.cmbMode7.Name = "cmbMode7";
          this.cmbMode7.Size = new System.Drawing.Size(79, 23);
          this.cmbMode7.TabIndex = 21;
          // 
          // cmbMode8
          // 
          this.cmbMode8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.cmbMode8.FormattingEnabled = true;
          this.cmbMode8.Location = new System.Drawing.Point(83, 293);
          this.cmbMode8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.cmbMode8.Name = "cmbMode8";
          this.cmbMode8.Size = new System.Drawing.Size(79, 23);
          this.cmbMode8.TabIndex = 22;
          // 
          // txtVon1
          // 
          this.txtVon1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtVon1.Location = new System.Drawing.Point(171, 41);
          this.txtVon1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtVon1.Name = "txtVon1";
          this.txtVon1.Size = new System.Drawing.Size(79, 25);
          this.txtVon1.TabIndex = 23;
          this.txtVon1.Text = "3.0";
          this.txtVon1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtVon2
          // 
          this.txtVon2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtVon2.Location = new System.Drawing.Point(171, 77);
          this.txtVon2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtVon2.Name = "txtVon2";
          this.txtVon2.Size = new System.Drawing.Size(79, 25);
          this.txtVon2.TabIndex = 24;
          this.txtVon2.Text = "3.0";
          this.txtVon2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtVon3
          // 
          this.txtVon3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtVon3.Location = new System.Drawing.Point(171, 113);
          this.txtVon3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtVon3.Name = "txtVon3";
          this.txtVon3.Size = new System.Drawing.Size(79, 25);
          this.txtVon3.TabIndex = 25;
          this.txtVon3.Text = "3.0";
          this.txtVon3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtVon4
          // 
          this.txtVon4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtVon4.Location = new System.Drawing.Point(171, 149);
          this.txtVon4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtVon4.Name = "txtVon4";
          this.txtVon4.Size = new System.Drawing.Size(79, 25);
          this.txtVon4.TabIndex = 26;
          this.txtVon4.Text = "3.0";
          this.txtVon4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtVon5
          // 
          this.txtVon5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtVon5.Location = new System.Drawing.Point(171, 186);
          this.txtVon5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtVon5.Name = "txtVon5";
          this.txtVon5.Size = new System.Drawing.Size(79, 25);
          this.txtVon5.TabIndex = 27;
          this.txtVon5.Text = "3.0";
          this.txtVon5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtVon6
          // 
          this.txtVon6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtVon6.Location = new System.Drawing.Point(171, 221);
          this.txtVon6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtVon6.Name = "txtVon6";
          this.txtVon6.Size = new System.Drawing.Size(79, 25);
          this.txtVon6.TabIndex = 28;
          this.txtVon6.Text = "3.0";
          this.txtVon6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtVon7
          // 
          this.txtVon7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtVon7.Location = new System.Drawing.Point(171, 257);
          this.txtVon7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtVon7.Name = "txtVon7";
          this.txtVon7.Size = new System.Drawing.Size(79, 25);
          this.txtVon7.TabIndex = 29;
          this.txtVon7.Text = "3.0";
          this.txtVon7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtVon8
          // 
          this.txtVon8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtVon8.Location = new System.Drawing.Point(171, 293);
          this.txtVon8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtVon8.Name = "txtVon8";
          this.txtVon8.Size = new System.Drawing.Size(79, 25);
          this.txtVon8.TabIndex = 30;
          this.txtVon8.Text = "3.0";
          this.txtVon8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtLoad1
          // 
          this.txtLoad1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtLoad1.Location = new System.Drawing.Point(259, 41);
          this.txtLoad1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtLoad1.Name = "txtLoad1";
          this.txtLoad1.Size = new System.Drawing.Size(79, 25);
          this.txtLoad1.TabIndex = 31;
          this.txtLoad1.Text = "0.5";
          this.txtLoad1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtLoad2
          // 
          this.txtLoad2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtLoad2.Location = new System.Drawing.Point(259, 77);
          this.txtLoad2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtLoad2.Name = "txtLoad2";
          this.txtLoad2.Size = new System.Drawing.Size(79, 25);
          this.txtLoad2.TabIndex = 32;
          this.txtLoad2.Text = "0.5";
          this.txtLoad2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtLoad3
          // 
          this.txtLoad3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtLoad3.Location = new System.Drawing.Point(259, 113);
          this.txtLoad3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtLoad3.Name = "txtLoad3";
          this.txtLoad3.Size = new System.Drawing.Size(79, 25);
          this.txtLoad3.TabIndex = 33;
          this.txtLoad3.Text = "0.5";
          this.txtLoad3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtLoad4
          // 
          this.txtLoad4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtLoad4.Location = new System.Drawing.Point(259, 149);
          this.txtLoad4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtLoad4.Name = "txtLoad4";
          this.txtLoad4.Size = new System.Drawing.Size(79, 25);
          this.txtLoad4.TabIndex = 34;
          this.txtLoad4.Text = "0.5";
          this.txtLoad4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtLoad5
          // 
          this.txtLoad5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtLoad5.Location = new System.Drawing.Point(259, 186);
          this.txtLoad5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtLoad5.Name = "txtLoad5";
          this.txtLoad5.Size = new System.Drawing.Size(79, 25);
          this.txtLoad5.TabIndex = 35;
          this.txtLoad5.Text = "0.5";
          this.txtLoad5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtLoad6
          // 
          this.txtLoad6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtLoad6.Location = new System.Drawing.Point(259, 221);
          this.txtLoad6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtLoad6.Name = "txtLoad6";
          this.txtLoad6.Size = new System.Drawing.Size(79, 25);
          this.txtLoad6.TabIndex = 36;
          this.txtLoad6.Text = "0.5";
          this.txtLoad6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtLoad7
          // 
          this.txtLoad7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtLoad7.Location = new System.Drawing.Point(259, 257);
          this.txtLoad7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtLoad7.Name = "txtLoad7";
          this.txtLoad7.Size = new System.Drawing.Size(79, 25);
          this.txtLoad7.TabIndex = 37;
          this.txtLoad7.Text = "0.5";
          this.txtLoad7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // txtLoad8
          // 
          this.txtLoad8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.txtLoad8.Location = new System.Drawing.Point(259, 293);
          this.txtLoad8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.txtLoad8.Name = "txtLoad8";
          this.txtLoad8.Size = new System.Drawing.Size(79, 25);
          this.txtLoad8.TabIndex = 38;
          this.txtLoad8.Text = "0.5";
          this.txtLoad8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // labVs1
          // 
          this.labVs1.AutoSize = true;
          this.labVs1.BackColor = System.Drawing.Color.White;
          this.labVs1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labVs1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labVs1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labVs1.ForeColor = System.Drawing.Color.Black;
          this.labVs1.Location = new System.Drawing.Point(347, 41);
          this.labVs1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs1.Name = "labVs1";
          this.labVs1.Size = new System.Drawing.Size(76, 27);
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
          this.labVs2.Location = new System.Drawing.Point(347, 77);
          this.labVs2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs2.Name = "labVs2";
          this.labVs2.Size = new System.Drawing.Size(76, 27);
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
          this.labVs3.Location = new System.Drawing.Point(347, 113);
          this.labVs3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs3.Name = "labVs3";
          this.labVs3.Size = new System.Drawing.Size(76, 27);
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
          this.labVs4.Location = new System.Drawing.Point(347, 149);
          this.labVs4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs4.Name = "labVs4";
          this.labVs4.Size = new System.Drawing.Size(76, 28);
          this.labVs4.TabIndex = 42;
          this.labVs4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labVs5
          // 
          this.labVs5.AutoSize = true;
          this.labVs5.BackColor = System.Drawing.Color.White;
          this.labVs5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labVs5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labVs5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labVs5.ForeColor = System.Drawing.Color.Black;
          this.labVs5.Location = new System.Drawing.Point(347, 186);
          this.labVs5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs5.Name = "labVs5";
          this.labVs5.Size = new System.Drawing.Size(76, 26);
          this.labVs5.TabIndex = 43;
          this.labVs5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labVs6
          // 
          this.labVs6.AutoSize = true;
          this.labVs6.BackColor = System.Drawing.Color.White;
          this.labVs6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labVs6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labVs6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labVs6.ForeColor = System.Drawing.Color.Black;
          this.labVs6.Location = new System.Drawing.Point(347, 221);
          this.labVs6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs6.Name = "labVs6";
          this.labVs6.Size = new System.Drawing.Size(76, 27);
          this.labVs6.TabIndex = 44;
          this.labVs6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labVs7
          // 
          this.labVs7.AutoSize = true;
          this.labVs7.BackColor = System.Drawing.Color.White;
          this.labVs7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labVs7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labVs7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labVs7.ForeColor = System.Drawing.Color.Black;
          this.labVs7.Location = new System.Drawing.Point(347, 257);
          this.labVs7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs7.Name = "labVs7";
          this.labVs7.Size = new System.Drawing.Size(76, 27);
          this.labVs7.TabIndex = 45;
          this.labVs7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labVs8
          // 
          this.labVs8.AutoSize = true;
          this.labVs8.BackColor = System.Drawing.Color.White;
          this.labVs8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labVs8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labVs8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labVs8.ForeColor = System.Drawing.Color.Black;
          this.labVs8.Location = new System.Drawing.Point(347, 293);
          this.labVs8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labVs8.Name = "labVs8";
          this.labVs8.Size = new System.Drawing.Size(76, 27);
          this.labVs8.TabIndex = 46;
          this.labVs8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV1
          // 
          this.labV1.AutoSize = true;
          this.labV1.BackColor = System.Drawing.Color.White;
          this.labV1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labV1.ForeColor = System.Drawing.Color.Black;
          this.labV1.Location = new System.Drawing.Point(432, 41);
          this.labV1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labV1.Name = "labV1";
          this.labV1.Size = new System.Drawing.Size(81, 27);
          this.labV1.TabIndex = 47;
          this.labV1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV2
          // 
          this.labV2.AutoSize = true;
          this.labV2.BackColor = System.Drawing.Color.White;
          this.labV2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labV2.ForeColor = System.Drawing.Color.Black;
          this.labV2.Location = new System.Drawing.Point(432, 77);
          this.labV2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labV2.Name = "labV2";
          this.labV2.Size = new System.Drawing.Size(81, 27);
          this.labV2.TabIndex = 48;
          this.labV2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV3
          // 
          this.labV3.AutoSize = true;
          this.labV3.BackColor = System.Drawing.Color.White;
          this.labV3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labV3.ForeColor = System.Drawing.Color.Black;
          this.labV3.Location = new System.Drawing.Point(432, 113);
          this.labV3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labV3.Name = "labV3";
          this.labV3.Size = new System.Drawing.Size(81, 27);
          this.labV3.TabIndex = 49;
          this.labV3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV4
          // 
          this.labV4.AutoSize = true;
          this.labV4.BackColor = System.Drawing.Color.White;
          this.labV4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labV4.ForeColor = System.Drawing.Color.Black;
          this.labV4.Location = new System.Drawing.Point(432, 149);
          this.labV4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labV4.Name = "labV4";
          this.labV4.Size = new System.Drawing.Size(81, 28);
          this.labV4.TabIndex = 50;
          this.labV4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV5
          // 
          this.labV5.AutoSize = true;
          this.labV5.BackColor = System.Drawing.Color.White;
          this.labV5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labV5.ForeColor = System.Drawing.Color.Black;
          this.labV5.Location = new System.Drawing.Point(432, 186);
          this.labV5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labV5.Name = "labV5";
          this.labV5.Size = new System.Drawing.Size(81, 26);
          this.labV5.TabIndex = 51;
          this.labV5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV6
          // 
          this.labV6.AutoSize = true;
          this.labV6.BackColor = System.Drawing.Color.White;
          this.labV6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labV6.ForeColor = System.Drawing.Color.Black;
          this.labV6.Location = new System.Drawing.Point(432, 221);
          this.labV6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labV6.Name = "labV6";
          this.labV6.Size = new System.Drawing.Size(81, 27);
          this.labV6.TabIndex = 52;
          this.labV6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV7
          // 
          this.labV7.AutoSize = true;
          this.labV7.BackColor = System.Drawing.Color.White;
          this.labV7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labV7.ForeColor = System.Drawing.Color.Black;
          this.labV7.Location = new System.Drawing.Point(432, 257);
          this.labV7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labV7.Name = "labV7";
          this.labV7.Size = new System.Drawing.Size(81, 27);
          this.labV7.TabIndex = 53;
          this.labV7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labV8
          // 
          this.labV8.AutoSize = true;
          this.labV8.BackColor = System.Drawing.Color.White;
          this.labV8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labV8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labV8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labV8.ForeColor = System.Drawing.Color.Black;
          this.labV8.Location = new System.Drawing.Point(432, 293);
          this.labV8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labV8.Name = "labV8";
          this.labV8.Size = new System.Drawing.Size(81, 27);
          this.labV8.TabIndex = 54;
          this.labV8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCur1
          // 
          this.labCur1.AutoSize = true;
          this.labCur1.BackColor = System.Drawing.Color.White;
          this.labCur1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labCur1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCur1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labCur1.ForeColor = System.Drawing.Color.Black;
          this.labCur1.Location = new System.Drawing.Point(522, 41);
          this.labCur1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labCur1.Name = "labCur1";
          this.labCur1.Size = new System.Drawing.Size(77, 27);
          this.labCur1.TabIndex = 55;
          this.labCur1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCur2
          // 
          this.labCur2.AutoSize = true;
          this.labCur2.BackColor = System.Drawing.Color.White;
          this.labCur2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labCur2.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCur2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labCur2.ForeColor = System.Drawing.Color.Black;
          this.labCur2.Location = new System.Drawing.Point(522, 77);
          this.labCur2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labCur2.Name = "labCur2";
          this.labCur2.Size = new System.Drawing.Size(77, 27);
          this.labCur2.TabIndex = 56;
          this.labCur2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCur3
          // 
          this.labCur3.AutoSize = true;
          this.labCur3.BackColor = System.Drawing.Color.White;
          this.labCur3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labCur3.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCur3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labCur3.ForeColor = System.Drawing.Color.Black;
          this.labCur3.Location = new System.Drawing.Point(522, 113);
          this.labCur3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labCur3.Name = "labCur3";
          this.labCur3.Size = new System.Drawing.Size(77, 27);
          this.labCur3.TabIndex = 57;
          this.labCur3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCur4
          // 
          this.labCur4.AutoSize = true;
          this.labCur4.BackColor = System.Drawing.Color.White;
          this.labCur4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labCur4.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCur4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labCur4.ForeColor = System.Drawing.Color.Black;
          this.labCur4.Location = new System.Drawing.Point(522, 149);
          this.labCur4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labCur4.Name = "labCur4";
          this.labCur4.Size = new System.Drawing.Size(77, 28);
          this.labCur4.TabIndex = 58;
          this.labCur4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCur5
          // 
          this.labCur5.AutoSize = true;
          this.labCur5.BackColor = System.Drawing.Color.White;
          this.labCur5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labCur5.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCur5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labCur5.ForeColor = System.Drawing.Color.Black;
          this.labCur5.Location = new System.Drawing.Point(522, 186);
          this.labCur5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labCur5.Name = "labCur5";
          this.labCur5.Size = new System.Drawing.Size(77, 26);
          this.labCur5.TabIndex = 59;
          this.labCur5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCur6
          // 
          this.labCur6.AutoSize = true;
          this.labCur6.BackColor = System.Drawing.Color.White;
          this.labCur6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labCur6.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCur6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labCur6.ForeColor = System.Drawing.Color.Black;
          this.labCur6.Location = new System.Drawing.Point(522, 221);
          this.labCur6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labCur6.Name = "labCur6";
          this.labCur6.Size = new System.Drawing.Size(77, 27);
          this.labCur6.TabIndex = 60;
          this.labCur6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCur7
          // 
          this.labCur7.AutoSize = true;
          this.labCur7.BackColor = System.Drawing.Color.White;
          this.labCur7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labCur7.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCur7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labCur7.ForeColor = System.Drawing.Color.Black;
          this.labCur7.Location = new System.Drawing.Point(522, 257);
          this.labCur7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labCur7.Name = "labCur7";
          this.labCur7.Size = new System.Drawing.Size(77, 27);
          this.labCur7.TabIndex = 61;
          this.labCur7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labCur8
          // 
          this.labCur8.AutoSize = true;
          this.labCur8.BackColor = System.Drawing.Color.White;
          this.labCur8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labCur8.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labCur8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labCur8.ForeColor = System.Drawing.Color.Black;
          this.labCur8.Location = new System.Drawing.Point(522, 293);
          this.labCur8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labCur8.Name = "labCur8";
          this.labCur8.Size = new System.Drawing.Size(77, 27);
          this.labCur8.TabIndex = 62;
          this.labCur8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label46
          // 
          this.label46.AutoSize = true;
          this.label46.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label46.Location = new System.Drawing.Point(5, 325);
          this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label46.Name = "label46";
          this.label46.Size = new System.Drawing.Size(69, 35);
          this.label46.TabIndex = 63;
          this.label46.Text = "状态:";
          this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labSatus
          // 
          this.labSatus.AutoSize = true;
          this.labSatus.BackColor = System.Drawing.Color.White;
          this.labSatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labSatus.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labSatus.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labSatus.ForeColor = System.Drawing.Color.Black;
          this.labSatus.Location = new System.Drawing.Point(83, 329);
          this.labSatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labSatus.Name = "labSatus";
          this.labSatus.Size = new System.Drawing.Size(79, 27);
          this.labSatus.TabIndex = 64;
          this.labSatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label48
          // 
          this.label48.AutoSize = true;
          this.label48.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label48.Location = new System.Drawing.Point(171, 325);
          this.label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label48.Name = "label48";
          this.label48.Size = new System.Drawing.Size(79, 35);
          this.label48.TabIndex = 65;
          this.label48.Text = "NTC_0:";
          this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labT0
          // 
          this.labT0.AutoSize = true;
          this.labT0.BackColor = System.Drawing.Color.White;
          this.labT0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labT0.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labT0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labT0.ForeColor = System.Drawing.Color.Black;
          this.labT0.Location = new System.Drawing.Point(259, 329);
          this.labT0.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labT0.Name = "labT0";
          this.labT0.Size = new System.Drawing.Size(79, 27);
          this.labT0.TabIndex = 66;
          this.labT0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label50
          // 
          this.label50.AutoSize = true;
          this.label50.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label50.Location = new System.Drawing.Point(347, 325);
          this.label50.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label50.Name = "label50";
          this.label50.Size = new System.Drawing.Size(76, 35);
          this.label50.TabIndex = 67;
          this.label50.Text = "NTC_1:";
          this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labT1
          // 
          this.labT1.AutoSize = true;
          this.labT1.BackColor = System.Drawing.Color.White;
          this.labT1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labT1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labT1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labT1.ForeColor = System.Drawing.Color.Black;
          this.labT1.Location = new System.Drawing.Point(432, 329);
          this.labT1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labT1.Name = "labT1";
          this.labT1.Size = new System.Drawing.Size(81, 27);
          this.labT1.TabIndex = 68;
          this.labT1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label52
          // 
          this.label52.AutoSize = true;
          this.label52.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label52.Location = new System.Drawing.Point(171, 361);
          this.label52.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label52.Name = "label52";
          this.label52.Size = new System.Drawing.Size(79, 35);
          this.label52.TabIndex = 69;
          this.label52.Text = "OCP:";
          this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label53
          // 
          this.label53.AutoSize = true;
          this.label53.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label53.Location = new System.Drawing.Point(5, 361);
          this.label53.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label53.Name = "label53";
          this.label53.Size = new System.Drawing.Size(69, 35);
          this.label53.TabIndex = 70;
          this.label53.Text = "ON/OFF:";
          this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labOnOff
          // 
          this.labOnOff.AutoSize = true;
          this.labOnOff.BackColor = System.Drawing.Color.White;
          this.labOnOff.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labOnOff.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labOnOff.ForeColor = System.Drawing.Color.Black;
          this.labOnOff.Location = new System.Drawing.Point(83, 365);
          this.labOnOff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labOnOff.Name = "labOnOff";
          this.labOnOff.Size = new System.Drawing.Size(79, 27);
          this.labOnOff.TabIndex = 71;
          this.labOnOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labOCP
          // 
          this.labOCP.AutoSize = true;
          this.labOCP.BackColor = System.Drawing.Color.White;
          this.labOCP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labOCP.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labOCP.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labOCP.ForeColor = System.Drawing.Color.Black;
          this.labOCP.Location = new System.Drawing.Point(259, 365);
          this.labOCP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labOCP.Name = "labOCP";
          this.labOCP.Size = new System.Drawing.Size(79, 27);
          this.labOCP.TabIndex = 72;
          this.labOCP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label56
          // 
          this.label56.AutoSize = true;
          this.label56.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label56.Location = new System.Drawing.Point(347, 361);
          this.label56.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label56.Name = "label56";
          this.label56.Size = new System.Drawing.Size(76, 35);
          this.label56.TabIndex = 73;
          this.label56.Text = "OVP:";
          this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labOVP
          // 
          this.labOVP.AutoSize = true;
          this.labOVP.BackColor = System.Drawing.Color.White;
          this.labOVP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labOVP.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labOVP.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labOVP.ForeColor = System.Drawing.Color.Black;
          this.labOVP.Location = new System.Drawing.Point(432, 365);
          this.labOVP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labOVP.Name = "labOVP";
          this.labOVP.Size = new System.Drawing.Size(81, 27);
          this.labOVP.TabIndex = 74;
          this.labOVP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label58
          // 
          this.label58.AutoSize = true;
          this.label58.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label58.Location = new System.Drawing.Point(171, 397);
          this.label58.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label58.Name = "label58";
          this.label58.Size = new System.Drawing.Size(79, 35);
          this.label58.TabIndex = 75;
          this.label58.Text = "OPP:";
          this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labOPP
          // 
          this.labOPP.AutoSize = true;
          this.labOPP.BackColor = System.Drawing.Color.White;
          this.labOPP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labOPP.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labOPP.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labOPP.ForeColor = System.Drawing.Color.Black;
          this.labOPP.Location = new System.Drawing.Point(259, 401);
          this.labOPP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labOPP.Name = "labOPP";
          this.labOPP.Size = new System.Drawing.Size(79, 27);
          this.labOPP.TabIndex = 76;
          this.labOPP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // label60
          // 
          this.label60.AutoSize = true;
          this.label60.Dock = System.Windows.Forms.DockStyle.Fill;
          this.label60.Location = new System.Drawing.Point(347, 397);
          this.label60.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
          this.label60.Name = "label60";
          this.label60.Size = new System.Drawing.Size(76, 35);
          this.label60.TabIndex = 77;
          this.label60.Text = "OTP:";
          this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // labOTP
          // 
          this.labOTP.AutoSize = true;
          this.labOTP.BackColor = System.Drawing.Color.White;
          this.labOTP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.labOTP.Dock = System.Windows.Forms.DockStyle.Fill;
          this.labOTP.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
          this.labOTP.ForeColor = System.Drawing.Color.Black;
          this.labOTP.Location = new System.Drawing.Point(432, 401);
          this.labOTP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.labOTP.Name = "labOTP";
          this.labOTP.Size = new System.Drawing.Size(81, 27);
          this.labOTP.TabIndex = 78;
          this.labOTP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
          this.txtBaud.Text = "57600,N,8,1";
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
          this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 840F));
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
          this.btnSetAddr.Click += new System.EventHandler(this.btnSetAddr_Click);
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
          // udcELoad
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.splitContainer1);
          this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
          this.Name = "udcELoad";
          this.Size = new System.Drawing.Size(1212, 776);
          this.Load += new System.EventHandler(this.udcELoad_Load);
          this.panel2.ResumeLayout(false);
          this.panel3.ResumeLayout(false);
          this.panel3.PerformLayout();
          this.tableLayoutPanel1.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.gridMon)).EndInit();
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
      private System.Windows.Forms.TableLayoutPanel panel3;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Button btnReadData;
      private System.Windows.Forms.Button btnReadSet;
      private System.Windows.Forms.Button btnSetLoad;
      private System.Windows.Forms.Button btnSetAddr;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private System.Windows.Forms.TableLayoutPanel panel4;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Label label13;
      private System.Windows.Forms.Label labCH1;
      private System.Windows.Forms.Label labCH2;
      private System.Windows.Forms.Label labCH3;
      private System.Windows.Forms.Label labCH4;
      private System.Windows.Forms.Label labCH5;
      private System.Windows.Forms.Label labCH6;
      private System.Windows.Forms.Label labCH7;
      private System.Windows.Forms.Label labCH8;
      private System.Windows.Forms.ComboBox cmbMode1;
      private System.Windows.Forms.ComboBox cmbMode2;
      private System.Windows.Forms.ComboBox cmbMode3;
      private System.Windows.Forms.ComboBox cmbMode4;
      private System.Windows.Forms.ComboBox cmbMode5;
      private System.Windows.Forms.ComboBox cmbMode6;
      private System.Windows.Forms.ComboBox cmbMode7;
      private System.Windows.Forms.ComboBox cmbMode8;
      private System.Windows.Forms.TextBox txtVon1;
      private System.Windows.Forms.TextBox txtVon2;
      private System.Windows.Forms.TextBox txtVon3;
      private System.Windows.Forms.TextBox txtVon4;
      private System.Windows.Forms.TextBox txtVon5;
      private System.Windows.Forms.TextBox txtVon6;
      private System.Windows.Forms.TextBox txtVon7;
      private System.Windows.Forms.TextBox txtVon8;
      private System.Windows.Forms.TextBox txtLoad1;
      private System.Windows.Forms.TextBox txtLoad2;
      private System.Windows.Forms.TextBox txtLoad3;
      private System.Windows.Forms.TextBox txtLoad4;
      private System.Windows.Forms.TextBox txtLoad5;
      private System.Windows.Forms.TextBox txtLoad6;
      private System.Windows.Forms.TextBox txtLoad7;
      private System.Windows.Forms.TextBox txtLoad8;
      private System.Windows.Forms.Label labVs1;
      private System.Windows.Forms.Label labVs2;
      private System.Windows.Forms.Label labVs3;
      private System.Windows.Forms.Label labVs4;
      private System.Windows.Forms.Label labVs5;
      private System.Windows.Forms.Label labVs6;
      private System.Windows.Forms.Label labVs7;
      private System.Windows.Forms.Label labVs8;
      private System.Windows.Forms.Label labV1;
      private System.Windows.Forms.Label labV2;
      private System.Windows.Forms.Label labV3;
      private System.Windows.Forms.Label labV4;
      private System.Windows.Forms.Label labV5;
      private System.Windows.Forms.Label labV6;
      private System.Windows.Forms.Label labV7;
      private System.Windows.Forms.Label labV8;
      private System.Windows.Forms.Label labCur1;
      private System.Windows.Forms.Label labCur2;
      private System.Windows.Forms.Label labCur3;
      private System.Windows.Forms.Label labCur4;
      private System.Windows.Forms.Label labCur5;
      private System.Windows.Forms.Label labCur6;
      private System.Windows.Forms.Label labCur7;
      private System.Windows.Forms.Label labCur8;
      private System.Windows.Forms.Label label46;
      private System.Windows.Forms.Label labSatus;
      private System.Windows.Forms.Label label48;
      private System.Windows.Forms.Label labT0;
      private System.Windows.Forms.Label label50;
      private System.Windows.Forms.Label labT1;
      private System.Windows.Forms.Label label52;
      private System.Windows.Forms.Label label53;
      private System.Windows.Forms.Label labOnOff;
      private System.Windows.Forms.Label labOCP;
      private System.Windows.Forms.Label label56;
      private System.Windows.Forms.Label labOVP;
      private System.Windows.Forms.Label label58;
      private System.Windows.Forms.Label labOPP;
      private System.Windows.Forms.Label label60;
      private System.Windows.Forms.Label labOTP;
      private System.Windows.Forms.DataGridView gridMon;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
      private System.Windows.Forms.Button btnScan;
      private System.Windows.Forms.TextBox txtEndAddr;
      private System.Windows.Forms.Label label14;
   }
}
