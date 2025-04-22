using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.Para.Udc
{
    public partial class udcTcpRecv : UserControl
    {
        #region 构造函数
        public udcTcpRecv()
        {
            InitializeComponent();
        }
        #endregion

        #region 字段
        private string recv = string.Empty; 
        #endregion

        #region 方法
        private delegate void setStatusHandler(string status);

        public void setStatus(string status)
        {
            if (this.InvokeRequired)
                this.Invoke(new setStatusHandler(setStatus), status);
            else
            {
                if (recv != status)
                {
                    recv = status;

                    string[] recvList = recv.Split('|');

                    int ready = System.Convert.ToInt32(recvList[1]);

                    rtbSnList.Clear();

                    if (ready == 0)
                    {
                        labReady.Text = "治具未就绪";
                        labReady.ForeColor = Color.Red;
                        labModel.Text = "----";
                        labIdCard.Text = "----";
                    }
                    else
                    {
                        if (ready==1)
                          labReady.Text = "测试前工位";
                        else
                          labReady.Text = "测试后工位";
                        labReady.ForeColor = Color.Blue;

                        labIdCard.Text = recvList[2];

                        labModel.Text = recvList[4];

                        string[] serialNos = recvList[3].Split(';');

                        for (int i = 0; i < serialNos.Length; i++)
                        {
                            string sn="条码【"+ (i+1).ToString() + "】:"+serialNos[i]+"\r\n";

                            rtbSnList.AppendText(sn); 

                        }                        
                    }               
                
                }            
            }
        }
        #endregion

    }
}
