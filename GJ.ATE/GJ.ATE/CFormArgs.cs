using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing; 
namespace GJ.ATE
{
    #region 用户登录事件消息
    /// <summary>
    /// 用户登录
    /// </summary>
    public class CLogInArgs : EventArgs
    {
        public readonly string userName;
        public readonly List<int> pwrLevel;
        public CLogInArgs(string userName, List<int> pwrLevel)
        {
            this.userName = userName;
            this.pwrLevel = pwrLevel;
        }
    }
    #endregion

}
