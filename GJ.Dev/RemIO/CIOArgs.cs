using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.RemIO
{
    public class CIOConArgs : EventArgs
    {
        public readonly string conStatus;
        public readonly bool bErr;
        public CIOConArgs(string conStatus, bool bErr = false)
        {
            this.conStatus = conStatus;
            this.bErr = bErr;
        }
    }
    public class CIODataArgs : EventArgs
    {
        public readonly string rData;
        public readonly bool bErr;
        public readonly bool bComplete;
        public CIODataArgs(string rData, bool bComplete = true, bool bErr = false)
        {
            this.rData = rData;
            this.bComplete = bComplete;
            this.bErr = bErr;
        }
    }
}
