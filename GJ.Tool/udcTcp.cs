using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ;
using GJ.Dev.TCP;  

namespace GJ.Tool
{
    public partial class udcTcp : UserControl
    {
        #region 构造函数
        public udcTcp()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {

        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            panel1.GetType().GetProperty("DoubleBuffered",
                                           System.Reflection.BindingFlags.Instance |
                                           System.Reflection.BindingFlags.NonPublic)
                                           .SetValue(panel1, true, null);
            panel2.GetType().GetProperty("DoubleBuffered",
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic)
                                            .SetValue(panel1, true, null);
        }
        #endregion

        #region TCP服务器

        #region 字段
        private CServer host = null;
        private COnEvent<CTcpConArgs> OnSerConArgs = new COnEvent<CTcpConArgs>();
        private COnEvent<CTcpRecvArgs> OnSerRecvArgs = new COnEvent<CTcpRecvArgs>(); 
        #endregion

        #region 面板回调函数
        private void btnListen_Click(object sender, EventArgs e)
        {
            if (btnListen.Text == "监听")
            {
                host = new CServer();
                host.OnConEvent.OnEvent += new COnEvent<CTcpConArgs>.OnEventHandler(OnSerCon);
                host.OnRecvAgrs.OnEvent += new COnEvent<CTcpRecvArgs>.OnEventHandler(OnSerRecv);   
                host.Listen(System.Convert.ToInt32(txtserPort.Text));
                btnListen.Text = "停止";
                cmbClients.Items.Clear();   
            }
            else
            {
                if (host != null)
                {
                    host.close();
                    host = null;
                }
                btnListen.Text = "监听";   
            }
        }
        private void btnserSend_Click(object sender, EventArgs e)
        {
            if (host != null && cmbClients.Text!="")
            {
                host.SendMsg(cmbClients.Text, txtSerCmd.Text);
                serRunLog.Log("发送[" + txtSerCmd.Text + "]->" + cmbClients.Text, UI.udcRunLog.ELog.Action);     
            }
        }
        private void OnSerCon(object sender, CTcpConArgs e)
        {
            OnSerConed(e);
        }
        private void OnSerRecv(object sender, CTcpRecvArgs e)
        {
            serRunLog.Log("["+e.remoteEndPoint + "]:" + e.recvData,UI.udcRunLog.ELog.OK);    
        }
        private delegate void OnSerConHandler(CTcpConArgs e);
        private void OnSerConed(CTcpConArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new OnSerConHandler(OnSerConed), e);
            else
            {
                if (e.remoteStatus == 1)
                {
                    cmbClients.Items.Add(e.remoteIP);  
                }
                else if (e.remoteStatus == 2)
                {
                    cmbClients.Items.Remove(e.remoteIP);  
                }
                serRunLog.Log(e.conStatus, UI.udcRunLog.ELog.Content);   
            }
        }
        #endregion
      
        #endregion

        #region TCP客户端

        #region 字段
        private string remoteHost = string.Empty;
        private CClient client = null;
        private COnEvent<CTcpConArgs> clientConArgs = new COnEvent<CTcpConArgs>();
        private COnEvent<CTcpRecvArgs> clientRecvArgs = new COnEvent<CTcpRecvArgs>(); 
        #endregion

        #region 面板回调函数

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "连接")
            {
                if(client!=null)
                    client=null; 
                client = new CClient();
                client.OnConEvent.OnEvent += new COnEvent<CTcpConArgs>.OnEventHandler(OnClientCon);
                client.OnRecvAgrs.OnEvent += new COnEvent<CTcpRecvArgs>.OnEventHandler(OnClientRevc);   
                if(!client.connect(txtClientIP.Text,System.Convert.ToInt32(txtClientPort.Text)))
                {
                    client = null;
                    return;
                }
                btnConnect.Text = "断开";  
            }
            else
            {
                if (client != null)
                {
                    client.close();
                    client = null;
                    btnConnect.Text = "连接";  
                }
            }
        }
        private void btnClientSend_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.SendMsg(txtClientCmd.Text);
                clientRunLog.Log("发送[" + txtClientCmd.Text + "]-->" + remoteHost, UI.udcRunLog.ELog.Action); 
            }
        }
        private void OnClientCon(object sender, CTcpConArgs e)
        {
            if (e.remoteStatus == 1)
                remoteHost = e.remoteIP; 
            clientRunLog.Log(e.conStatus, UI.udcRunLog.ELog.Content);     
        }
        private void OnClientRevc(object sender, CTcpRecvArgs e)
        {
            clientRunLog.Log("[" + e.remoteEndPoint + "]:" + e.recvData, UI.udcRunLog.ELog.OK);    
        }
        #endregion

        #endregion
    }
}
