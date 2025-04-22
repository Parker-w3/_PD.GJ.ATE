using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.TCP
{
    #region TCP/IP事件消息
    /// <summary>
    /// Tcp连接状态
    /// </summary>
    public class CTcpConArgs : EventArgs
    {
        public int idNo;

        public string name;

        public readonly string conStatus;

        public readonly bool bErr;

        public readonly string remoteIP;

        public readonly int remoteStatus;

        public CTcpConArgs(int idNo,string name, string conStatus,
                           bool bErr = false,string remoteIP = "", int remoteStatus = 0)
        {
            this.idNo = idNo;
            this.name = name;
            this.conStatus = conStatus;            
            this.bErr = bErr;
            this.remoteIP = remoteIP;
            this.remoteStatus = remoteStatus; 
        }
    }
    /// <summary>
    /// Tcp数据接收类
    /// </summary>
    public class CTcpRecvArgs : EventArgs
    {
        public int idNo;

        public string name;

        public readonly string remoteEndPoint;

        public readonly string recvData;

        public CTcpRecvArgs(int idNo,string name,string remoteEndPoint, string recvData)
        {
            this.idNo = idNo;
            this.name = name;
            this.remoteEndPoint = remoteEndPoint;
            this.recvData = recvData;
        }
    }
    #endregion
   
}
