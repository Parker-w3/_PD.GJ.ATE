using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; 
using System.Net;
using System.Net.Sockets;
using GJ;

namespace GJ.Dev.TCP
{
    public class CServer
    {

        #region 字段

        private int idNo = 0;

        private string name = "server";

        private bool bWacth=false; 
        /// <summary>
        /// 监听套接字
        /// </summary>
        Socket socketWatch = null;  
        /// <summary>
        /// 监听线程
        /// </summary>
        Thread threadWatch = null;

        Dictionary<string, Socket> dict = new Dictionary<string, Socket>();
        Dictionary<string, Thread> dictThread = new Dictionary<string, Thread>(); 

        #endregion

        #region 事件
        public COnEvent<CTcpConArgs> OnConEvent = new COnEvent<CTcpConArgs>();
        public COnEvent<CTcpRecvArgs> OnRecvAgrs = new COnEvent<CTcpRecvArgs>();
        #endregion

        #region 方法
        public void Listen(int tcpPort=8000)
        {
            try
            {
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, tcpPort);
                // 将负责监听的套接字绑定到唯一的ip和端口上
                socketWatch.Bind(endPoint); 
            }
            catch (Exception e)
            {
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, e.ToString(), true)); 
            }
            // 设置监听队列的长度；  
            socketWatch.Listen(20);
            // 创建负责监听的线程；  
            threadWatch = new Thread(WatchConnecting);
            threadWatch.IsBackground =true;
            threadWatch.Start();
            dict.Clear();
            dictThread.Clear();  
            bWacth = true;
            OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "服务器[端口:" + tcpPort.ToString() + "]启动监听."));   
        }
        /// <summary>
        /// 监控线程
        /// </summary>
        private void WatchConnecting()
        {
            try
            {
                while (true)  // 持续不断的监听客户端的连接请求；  
                {
                    // 开始监听客户端连接请求，Accept方法会阻断当前的线程；  
                    Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；  

                    OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "与客户端[" + sokConnection.RemoteEndPoint.ToString() + "]连接.", false,
                                         sokConnection.RemoteEndPoint.ToString(),1)); 

                    // 将与客户端连接的 套接字 对象添加到集合中；  
                    dict.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);
                    Thread thr = new Thread(RecMsg);
                    thr.IsBackground = true;
                    thr.Start(sokConnection);
                    dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程添加到线程的集合中去。  
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "服务端断开连接.", true)); 
            }

        }
        /// <summary>
        /// 与客户端连接套接字通信
        /// </summary>
        /// <param name="sokConnectionparn"></param>
        private void RecMsg(object sokConnectionparn)
        {
            try
            {
                Socket sokClient = sokConnectionparn as Socket;

                while (true)
                {
                    // 定义一个2M的缓存区；  
                    byte[] arrMsgRec = new byte[1024 * 1024 * 2];
                    // 将接受到的数据存入到输入  arrMsgRec中；  
                    int length = -1;
                    try
                    {
                        length = sokClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度；  
                    }
                    catch (SocketException)
                    {
                        if (bWacth)
                        {
                            // 从 通信套接字 集合中删除被中断连接的通信套接字；  
                            dict.Remove(sokClient.RemoteEndPoint.ToString());
                            // 从通信线程集合中删除被中断连接的通信线程对象；  
                            dictThread.Remove(sokClient.RemoteEndPoint.ToString());

                            OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "客户端[" + sokClient.RemoteEndPoint.ToString() +
                                                                   "]断开连接.", true, sokClient.RemoteEndPoint.ToString(),2)); 
                        }                        
                        break;
                    }
                    catch (Exception)
                    {
                        if (bWacth)
                        {
                            // 从 通信套接字 集合中删除被中断连接的通信套接字；  
                            dict.Remove(sokClient.RemoteEndPoint.ToString());
                            // 从通信线程集合中删除被中断连接的通信线程对象；  
                            dictThread.Remove(sokClient.RemoteEndPoint.ToString());

                            OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "客户端[" + sokClient.RemoteEndPoint.ToString() +
                                                                 "]断开连接.", true, sokClient.RemoteEndPoint.ToString(),2)); 
                        }                       
                        break;
                    }
                    try
                    {
                        // 表示接收到的是数据；  
                        if (length == 0)
                        {
                            if (bWacth)
                            {
                                // 从 通信套接字 集合中删除被中断连接的通信套接字；  
                                dict.Remove(sokClient.RemoteEndPoint.ToString());
                                // 从通信线程集合中删除被中断连接的通信线程对象；  
                                dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                            }
                            OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "客户端[" + sokClient.RemoteEndPoint.ToString() +
                                                                      "]断开连接.", true, sokClient.RemoteEndPoint.ToString(),2));
                            break;
                        }
                        if (bWacth)
                        {
                            string strMsg = System.Text.Encoding.UTF8.GetString(arrMsgRec, 0, length);// 将接受到的字节数据转化成字符串

                            OnRecvAgrs.OnEvented(new CTcpRecvArgs(idNo, name, sokClient.RemoteEndPoint.ToString(), strMsg));
                        }
                    }
                    catch (Exception)
                    {
                        
                    }                  
                }
            }
            finally
            {
              
            }           
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strMsg"></param>
        public bool SendMsg(string strKey, string strMsg)
        {
            try
            {
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg); // 将要发送的字符串转换成Utf-8字节数组；  
                dict[strKey].Send(arrMsg);// 解决了 sokConnection是局部变量，不能再本函数中引用的问题；  
                return true;
            }
            catch (Exception)
            {
                return false; 
            }
        }
        /// <summary>
        /// 关闭监听
        /// </summary>
        /// <returns></returns>
        public bool close()
        {
            try
            {
                bWacth = false;
                for (int i = 0; i < dict.Count; i++)
                {
                    string strKey = dict.ElementAt(i).Key;
                    dict[strKey].Shutdown(SocketShutdown.Both);  
                    dict[strKey].Close();
                    dict[strKey].Dispose(); 
                }
                if (threadWatch != null)
                {
                    socketWatch.Close();                 
                    threadWatch.Abort(); 
                    threadWatch = null;
                }
                socketWatch.Close();
                socketWatch = null;
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
