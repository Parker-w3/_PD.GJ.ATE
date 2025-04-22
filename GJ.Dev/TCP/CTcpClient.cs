using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GJ;

namespace GJ.Dev.TCP
{
    public class CClient
    {
        public CClient(int idNo=0,string name="")
        {
            this.idNo = idNo;
            this.name = name; 
        }

        #region 字段 
        private int idNo = 0;
        private string name = "";
        private Socket socketClient = null;
        private Thread threadClient = null;
        private int clientReady = 0;
        public string recvData = string.Empty;
        #endregion

        #region 属性
        public int mReady
        {
            get { return clientReady; }
        }
        public int mIdNo
        {
            set { idNo = value; }
            get { return idNo; }
        }
        public string mName
        {
            set { name = value; }
            get { return name; }
        }
        #endregion

        #region 事件
        public COnEvent<CTcpConArgs> OnConEvent = new COnEvent<CTcpConArgs>();
        public COnEvent<CTcpRecvArgs> OnRecvAgrs = new COnEvent<CTcpRecvArgs>();
        #endregion

        #region 方法
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool connect(string ipAddress,int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "与服务器[" + ipAddress + ":" + port.ToString() + "]连接中…"));
                clientReady = 0;
                socketClient.Connect(endPoint);
            }
            catch (SocketException se)
            {
                socketClient = null;
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "无法连接服务器[" + ipAddress + ":" + port.ToString() + "]:" + se.ToString(), true));
                clientReady = 2;
                return false;
            }
            catch (Exception e)
            {
                socketClient = null;
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "无法连接服务器[" + ipAddress + ":" + port.ToString() + "]:" + e.ToString(), true));
                clientReady = 2;
                return false;
            }            
            OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "与服务器[" + ipAddress + ":" + port.ToString() + "]取得连接.", false, ipAddress + ":" + port.ToString(), 1));
            threadClient = new Thread(RecMsg);
            threadClient.IsBackground = true;
            threadClient.Start();
            clientReady = 1;
            return true;
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        public bool close()
        {
            try
            {
                if (threadClient != null)
                {
                    threadClient.Abort();
                    threadClient = null;
                }
                if (socketClient != null)
                {
                    socketClient.Dispose();
                    socketClient = null;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                clientReady = 0;
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "客户端断开连接.", true)); 
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        private void RecMsg()
        {
            try
            {
                while (true)
                {
                    // 定义一个2M的缓存区；  
                    byte[] arrMsgRec = new byte[1024 * 1024 * 2];
                    // 将接受到的数据存入到输入  arrMsgRec中；  
                    int length = -1;
                    try
                    {
                        length = socketClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度；  
                    }
                    catch (SocketException)
                    {
                        socketClient = null;
                        OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "与服务器断开连接1.", true)); 
                        return;
                    }
                    catch (Exception)
                    {
                        socketClient = null;
                        OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "与服务器断开连接2.", true)); 
                        return;
                    }
                    if (length == 0)
                    {
                        socketClient = null;
                        OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "与服务器断开连接3.", true)); 
                        return;
                    }                       
                    string strMsg = System.Text.Encoding.UTF8.GetString(arrMsgRec, 0, length);// 将接受到的字节数据转化成字符串；

                    OnRecvAgrs.OnEvented(new CTcpRecvArgs(idNo, name, socketClient.RemoteEndPoint.ToString(), strMsg));   
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                
            }
        }
        /// <summary>
       /// 发送数据
       /// </summary>
       /// <param name="strMsg"></param>
       /// <returns></returns>
        public bool SendMsg(string strMsg)
       {
           try
           {
               if (socketClient == null)
                   return false;
               if (clientReady!=1)
                   return false; 
               byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg);
               socketClient.Send(arrMsg); // 发送消息； 
               return true; 
           }
           catch (Exception)
           {
               return false;
           }
              
       }
       #endregion

    }
}
