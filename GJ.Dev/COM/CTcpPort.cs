using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using GJ;
using GJ.Dev.TCP;
namespace GJ.Dev.COM
{
    public class CTcpPort:ICom 
    {
        #region 字段
        private int idNo = 0;
        private string name = "TcpPort";
        private int comDataType = 0;
        private Socket socketClient = null;
        private Thread threadClient = null;
        private bool conStatus = false;
        private volatile int recvLen= 0; 
        private volatile string recvData = string.Empty;
        private volatile byte[] recvBytes = new byte[1024*1024]; 
        #endregion

        #region 事件
        public COnEvent<CTcpConArgs> OnConEvent = new COnEvent<CTcpConArgs>();
        public COnEvent<CTcpRecvArgs> OnRecvAgrs = new COnEvent<CTcpRecvArgs>();
        #endregion

        #region 属性
        public int mIdNo
        {
            get { return idNo; }
            set { idNo = value; }
        }
        public string mName
        {
            get { return name; }
            set { name = value; }
        }
        public int mComDataType
        {
            set { comDataType = value; }
        }
        public bool mConStatus
        {
            get { return conStatus; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 客户端
        /// </summary>
        /// <param name="comName">IP Address</param>
        /// <param name="setting">Port</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool open(string comName, ref string er, string setting)
        {
            IPAddress ip = IPAddress.Parse(comName);
            IPEndPoint endPoint = new IPEndPoint(ip, System.Convert.ToInt32(setting));
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                OnConEvent.OnEvented(new CTcpConArgs(idNo,name,"与服务器[" + comName + ":" + setting + "]连接中…"));
                er = "与服务器[" + comName + ":" + setting + "]连接中…";
                conStatus = false;
                socketClient.Connect(endPoint);
            }
            catch (SocketException se)
            {
                socketClient = null;
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "无法连接服务器[" + comName + ":" + setting + "]:" + se.ToString(), true));
                er = "无法连接服务器[" + comName + ":" + setting + "]:" + se.ToString();
                return false;
            }
            catch (Exception e)
            {
                socketClient = null;
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "无法连接服务器[" + comName + ":" + setting + "]:" + e.ToString(), true));
                er = "无法连接服务器[" + comName + ":" + setting + "]:" + e.ToString();
                return false;
            }
            conStatus = true;
            er = "与服务器[" + comName + ":" + setting + "]取得连接.";
            OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "与服务器[" + comName + ":" + setting + "]取得连接.")); 
            threadClient = new Thread(RecMsg);
            threadClient.IsBackground = true;
            threadClient.Start();
            return true;
        }
        /// <summary>
        /// 关闭串口
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
                conStatus = false;
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "客户端断开连接.", true));
            }
        }
        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setBaud(ref string er, string setting)
        {
            return false;
        }

        public bool SetDtrRts(bool Dtr, bool Rts, ref string er)
        {
            return false;
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
                    // 定义一个1M的缓存区；  
                    byte[] arrMsgRec = new byte[1024*1024];
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

                    for (int i = 0; i < length; i++)
                        recvBytes[i] = arrMsgRec[i];  

                    recvLen  = length;

                    recvData = strMsg;

                    OnRecvAgrs.OnEvented(new CTcpRecvArgs(idNo, name, socketClient.RemoteEndPoint.ToString(), strMsg));
                }
            }
            catch (Exception)
            {
                OnConEvent.OnEvented(new CTcpConArgs(idNo, name, "与服务器断开连接4.", true));
            }
            finally
            {
                conStatus = false;
            }
        }
        /// <summary>
        /// 发送数据及接收数据
        /// </summary>
        /// <param name="wData"></param>
        /// <param name="rLen"></param>
        /// <param name="rData"></param>
        /// <param name="er"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public bool send(string wData, int rLen, ref string rData, ref string er, int timeOut = 1000)
        {
            try
            {
                if (socketClient == null)
                    return false;
                if (!conStatus)
                    return false;                               
                byte[] arrMsg = new byte[wData.Length / 2];
                for (int i = 0; i < wData.Length / 2; i++)
                {
                    arrMsg[i] = System.Convert.ToByte(wData.Substring(i * 2, 2), 16); 
                }                
                recvData = string.Empty;
                recvLen = 0; 
                socketClient.Send(arrMsg); // 发送消息；                
                int waitTime = Environment.TickCount;
                do
                {
                    System.Threading.Thread.Sleep(2);
                } while ((recvLen < rLen) && (Environment.TickCount - waitTime) < timeOut);
                if (recvLen == 0)
                {
                    er = "接收数据超时";
                    return false;
                }
                rData = string.Empty;            
                for (int i = 0; i < recvLen; i++)
                {
                    rData+=recvBytes[i].ToString("X2");   
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool send(string wData, string rEOI, ref string rData, ref string er, int timeOut = 200)
        {
            return false; 
        }
        #endregion
    }
}
