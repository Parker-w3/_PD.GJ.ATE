namespace GJ.ATE
{
    partial class WndFrmUsers
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
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WndFrmUsers));
           this.label1 = new System.Windows.Forms.Label();
           this.gridUsers = new System.Windows.Forms.DataGridView();
           this.label2 = new System.Windows.Forms.Label();
           this.groupBox1 = new System.Windows.Forms.GroupBox();
           this.chkLevel8 = new System.Windows.Forms.CheckBox();
           this.chkLevel7 = new System.Windows.Forms.CheckBox();
           this.chkLevel6 = new System.Windows.Forms.CheckBox();
           this.chkLevel5 = new System.Windows.Forms.CheckBox();
           this.chkLevel4 = new System.Windows.Forms.CheckBox();
           this.chkLevel3 = new System.Windows.Forms.CheckBox();
           this.chkLevel2 = new System.Windows.Forms.CheckBox();
           this.chkLevel1 = new System.Windows.Forms.CheckBox();
           this.groupBox2 = new System.Windows.Forms.GroupBox();
           this.chkLook = new System.Windows.Forms.CheckBox();
           this.txtPassWord = new System.Windows.Forms.TextBox();
           this.txtUserName = new System.Windows.Forms.TextBox();
           this.label4 = new System.Windows.Forms.Label();
           this.label3 = new System.Windows.Forms.Label();
           this.btnAdd = new System.Windows.Forms.Button();
           this.btnDelete = new System.Windows.Forms.Button();
           this.btnSave = new System.Windows.Forms.Button();
           this.btnExit = new System.Windows.Forms.Button();
           ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).BeginInit();
           this.groupBox1.SuspendLayout();
           this.groupBox2.SuspendLayout();
           this.SuspendLayout();
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.label1.Location = new System.Drawing.Point(82, 18);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(104, 16);
           this.label1.TabIndex = 0;
           this.label1.Text = "用户信息明细";
           // 
           // gridUsers
           // 
           this.gridUsers.AllowUserToAddRows = false;
           this.gridUsers.AllowUserToDeleteRows = false;
           this.gridUsers.AllowUserToResizeColumns = false;
           this.gridUsers.AllowUserToResizeRows = false;
           this.gridUsers.BackgroundColor = System.Drawing.SystemColors.Window;
           this.gridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
           this.gridUsers.Location = new System.Drawing.Point(12, 42);
           this.gridUsers.Name = "gridUsers";
           this.gridUsers.RowHeadersWidth = 24;
           this.gridUsers.RowTemplate.Height = 23;
           this.gridUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
           this.gridUsers.Size = new System.Drawing.Size(245, 262);
           this.gridUsers.TabIndex = 1;
           this.gridUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridUsers_CellClick);
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.label2.Location = new System.Drawing.Point(307, 18);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(72, 16);
           this.label2.TabIndex = 2;
           this.label2.Text = "权限管理";
           // 
           // groupBox1
           // 
           this.groupBox1.Controls.Add(this.chkLevel8);
           this.groupBox1.Controls.Add(this.chkLevel7);
           this.groupBox1.Controls.Add(this.chkLevel6);
           this.groupBox1.Controls.Add(this.chkLevel5);
           this.groupBox1.Controls.Add(this.chkLevel4);
           this.groupBox1.Controls.Add(this.chkLevel3);
           this.groupBox1.Controls.Add(this.chkLevel2);
           this.groupBox1.Controls.Add(this.chkLevel1);
           this.groupBox1.Location = new System.Drawing.Point(263, 37);
           this.groupBox1.Name = "groupBox1";
           this.groupBox1.Size = new System.Drawing.Size(174, 267);
           this.groupBox1.TabIndex = 3;
           this.groupBox1.TabStop = false;
           // 
           // chkLevel8
           // 
           this.chkLevel8.AutoSize = true;
           this.chkLevel8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.chkLevel8.Location = new System.Drawing.Point(22, 237);
           this.chkLevel8.Name = "chkLevel8";
           this.chkLevel8.Size = new System.Drawing.Size(120, 16);
           this.chkLevel8.TabIndex = 7;
           this.chkLevel8.Text = "系统帮助说明功能";
           this.chkLevel8.UseVisualStyleBackColor = true;
           // 
           // chkLevel7
           // 
           this.chkLevel7.AutoSize = true;
           this.chkLevel7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.chkLevel7.Location = new System.Drawing.Point(22, 206);
           this.chkLevel7.Name = "chkLevel7";
           this.chkLevel7.Size = new System.Drawing.Size(120, 16);
           this.chkLevel7.TabIndex = 6;
           this.chkLevel7.Text = "软件版本查询功能";
           this.chkLevel7.UseVisualStyleBackColor = true;
           // 
           // chkLevel6
           // 
           this.chkLevel6.AutoSize = true;
           this.chkLevel6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.chkLevel6.Location = new System.Drawing.Point(22, 175);
           this.chkLevel6.Name = "chkLevel6";
           this.chkLevel6.Size = new System.Drawing.Size(120, 16);
           this.chkLevel6.TabIndex = 5;
           this.chkLevel6.Text = "系统调式工具功能";
           this.chkLevel6.UseVisualStyleBackColor = true;
           // 
           // chkLevel5
           // 
           this.chkLevel5.AutoSize = true;
           this.chkLevel5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.chkLevel5.Location = new System.Drawing.Point(22, 144);
           this.chkLevel5.Name = "chkLevel5";
           this.chkLevel5.Size = new System.Drawing.Size(120, 16);
           this.chkLevel5.TabIndex = 4;
           this.chkLevel5.Text = "运行日志查询功能";
           this.chkLevel5.UseVisualStyleBackColor = true;
           // 
           // chkLevel4
           // 
           this.chkLevel4.AutoSize = true;
           this.chkLevel4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.chkLevel4.Location = new System.Drawing.Point(22, 113);
           this.chkLevel4.Name = "chkLevel4";
           this.chkLevel4.Size = new System.Drawing.Size(120, 16);
           this.chkLevel4.TabIndex = 3;
           this.chkLevel4.Text = "测试报表查询功能";
           this.chkLevel4.UseVisualStyleBackColor = true;
           // 
           // chkLevel3
           // 
           this.chkLevel3.AutoSize = true;
           this.chkLevel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.chkLevel3.Location = new System.Drawing.Point(22, 82);
           this.chkLevel3.Name = "chkLevel3";
           this.chkLevel3.Size = new System.Drawing.Size(120, 16);
           this.chkLevel3.TabIndex = 2;
           this.chkLevel3.Text = "用户权限设置功能";
           this.chkLevel3.UseVisualStyleBackColor = true;
           // 
           // chkLevel2
           // 
           this.chkLevel2.AutoSize = true;
           this.chkLevel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.chkLevel2.Location = new System.Drawing.Point(22, 51);
           this.chkLevel2.Name = "chkLevel2";
           this.chkLevel2.Size = new System.Drawing.Size(120, 16);
           this.chkLevel2.TabIndex = 1;
           this.chkLevel2.Text = "机种参数设置功能";
           this.chkLevel2.UseVisualStyleBackColor = true;
           // 
           // chkLevel1
           // 
           this.chkLevel1.AutoSize = true;
           this.chkLevel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.chkLevel1.Location = new System.Drawing.Point(22, 20);
           this.chkLevel1.Name = "chkLevel1";
           this.chkLevel1.Size = new System.Drawing.Size(120, 16);
           this.chkLevel1.TabIndex = 0;
           this.chkLevel1.Text = "系统参数设置功能";
           this.chkLevel1.UseVisualStyleBackColor = true;
           // 
           // groupBox2
           // 
           this.groupBox2.Controls.Add(this.chkLook);
           this.groupBox2.Controls.Add(this.txtPassWord);
           this.groupBox2.Controls.Add(this.txtUserName);
           this.groupBox2.Controls.Add(this.label4);
           this.groupBox2.Controls.Add(this.label3);
           this.groupBox2.Location = new System.Drawing.Point(12, 315);
           this.groupBox2.Name = "groupBox2";
           this.groupBox2.Size = new System.Drawing.Size(425, 86);
           this.groupBox2.TabIndex = 4;
           this.groupBox2.TabStop = false;
           this.groupBox2.Text = "用户信息";
           // 
           // chkLook
           // 
           this.chkLook.AutoSize = true;
           this.chkLook.Location = new System.Drawing.Point(251, 60);
           this.chkLook.Name = "chkLook";
           this.chkLook.Size = new System.Drawing.Size(72, 16);
           this.chkLook.TabIndex = 4;
           this.chkLook.Text = "查看密码";
           this.chkLook.UseVisualStyleBackColor = true;
           this.chkLook.CheckedChanged += new System.EventHandler(this.chkLook_CheckedChanged);
           // 
           // txtPassWord
           // 
           this.txtPassWord.Location = new System.Drawing.Point(88, 55);
           this.txtPassWord.Name = "txtPassWord";
           this.txtPassWord.PasswordChar = '*';
           this.txtPassWord.Size = new System.Drawing.Size(143, 21);
           this.txtPassWord.TabIndex = 3;
           // 
           // txtUserName
           // 
           this.txtUserName.Location = new System.Drawing.Point(88, 24);
           this.txtUserName.Name = "txtUserName";
           this.txtUserName.Size = new System.Drawing.Size(143, 21);
           this.txtUserName.TabIndex = 2;
           // 
           // label4
           // 
           this.label4.AutoSize = true;
           this.label4.Location = new System.Drawing.Point(35, 58);
           this.label4.Name = "label4";
           this.label4.Size = new System.Drawing.Size(35, 12);
           this.label4.TabIndex = 1;
           this.label4.Text = "密码:";
           // 
           // label3
           // 
           this.label3.AutoSize = true;
           this.label3.Location = new System.Drawing.Point(35, 27);
           this.label3.Name = "label3";
           this.label3.Size = new System.Drawing.Size(47, 12);
           this.label3.TabIndex = 0;
           this.label3.Text = "用户名:";
           // 
           // btnAdd
           // 
           this.btnAdd.Location = new System.Drawing.Point(51, 412);
           this.btnAdd.Name = "btnAdd";
           this.btnAdd.Size = new System.Drawing.Size(72, 32);
           this.btnAdd.TabIndex = 5;
           this.btnAdd.Text = "新增(&N)";
           this.btnAdd.UseVisualStyleBackColor = true;
           this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
           // 
           // btnDelete
           // 
           this.btnDelete.Location = new System.Drawing.Point(230, 412);
           this.btnDelete.Name = "btnDelete";
           this.btnDelete.Size = new System.Drawing.Size(72, 32);
           this.btnDelete.TabIndex = 6;
           this.btnDelete.Text = "删除(&D)";
           this.btnDelete.UseVisualStyleBackColor = true;
           this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
           // 
           // btnSave
           // 
           this.btnSave.Location = new System.Drawing.Point(145, 412);
           this.btnSave.Name = "btnSave";
           this.btnSave.Size = new System.Drawing.Size(72, 32);
           this.btnSave.TabIndex = 7;
           this.btnSave.Text = "保存(&S)";
           this.btnSave.UseVisualStyleBackColor = true;
           this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
           // 
           // btnExit
           // 
           this.btnExit.Location = new System.Drawing.Point(323, 412);
           this.btnExit.Name = "btnExit";
           this.btnExit.Size = new System.Drawing.Size(72, 32);
           this.btnExit.TabIndex = 8;
           this.btnExit.Text = "退出(&E)";
           this.btnExit.UseVisualStyleBackColor = true;
           this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
           // 
           // WndFrmUsers
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.ClientSize = new System.Drawing.Size(450, 456);
           this.Controls.Add(this.btnExit);
           this.Controls.Add(this.btnSave);
           this.Controls.Add(this.btnDelete);
           this.Controls.Add(this.btnAdd);
           this.Controls.Add(this.groupBox2);
           this.Controls.Add(this.groupBox1);
           this.Controls.Add(this.label2);
           this.Controls.Add(this.gridUsers);
           this.Controls.Add(this.label1);
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
           this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.Name = "WndFrmUsers";
           this.Text = "用户权限";
           this.Load += new System.EventHandler(this.WndFrmUsers_Load);
           ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).EndInit();
           this.groupBox1.ResumeLayout(false);
           this.groupBox1.PerformLayout();
           this.groupBox2.ResumeLayout(false);
           this.groupBox2.PerformLayout();
           this.ResumeLayout(false);
           this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkLevel8;
        private System.Windows.Forms.CheckBox chkLevel7;
        private System.Windows.Forms.CheckBox chkLevel6;
        private System.Windows.Forms.CheckBox chkLevel5;
        private System.Windows.Forms.CheckBox chkLevel4;
        private System.Windows.Forms.CheckBox chkLevel3;
        private System.Windows.Forms.CheckBox chkLevel2;
        private System.Windows.Forms.CheckBox chkLevel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox chkLook;
    }
}