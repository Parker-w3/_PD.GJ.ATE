﻿namespace GJ.Para.Udc.BURNIN
{
    partial class udcOnOffWave
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
            CIniFile.WriteToIni("OnOff Para", "startTime", startTime, System.Windows.Forms.Application.StartupPath + "\\onoff.tmr");
            CIniFile.WriteToIni("OnOff Para", "runStatus", runStatus.ToString(), System.Windows.Forms.Application.StartupPath + "\\onoff.tmr");
            CIniFile.WriteToIni("OnOff Para", "runingTime", runningTime.ToString(), System.Windows.Forms.Application.StartupPath + "\\onoff.tmr");

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
            this.runWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // runWorker
            // 
            this.runWorker.WorkerSupportsCancellation = true;
            this.runWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.runWorker_DoWork);
            // 
            // udcOnOffWave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0, 1, 0, 2);
            this.Name = "udcOnOffWave";
            this.Size = new System.Drawing.Size(978, 127);
            this.Load += new System.EventHandler(this.udcOnOffWave_Load);
            this.Resize += new System.EventHandler(this.udcOnOffWave_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker runWorker;
    }
}
