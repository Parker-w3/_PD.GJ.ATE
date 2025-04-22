using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ
{
    /// <summary>
    /// 定义触发事件类
    /// </summary>
    /// <typeparam name="T">泛类事件类</typeparam>
    public class COnEvent<T> where T : EventArgs
    {
        /// <summary>
        /// 定义事件委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void OnEventHandler(object sender, T e);
        /// <summary>
        /// 定义事件
        /// </summary>
        public event OnEventHandler OnEvent;
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="e"></param>
        public void OnEvented(T e)
        {
            if (OnEvent != null)
                OnEvent(this, e);
        }
    }

    #region 事件定义

    #region 面板事件消息
    public enum EFormStatus
    {
        Idel,
        Auto,
        Stop
    }
    /// <summary>
    /// 运行指示
    /// </summary>
    public class CFormRunArgs : EventArgs
    {
        public readonly EFormStatus status;
        public CFormRunArgs(EFormStatus status)
        {
            this.status = status;
        }
    }
    /// <summary>
    /// 按钮触发事件
    /// </summary>
    public class CFormBtnArgs : EventArgs
    {
        public enum EBtnName
        {
            启动,
            暂停,
            退出,
            F9,
            F10
        }
        public readonly EBtnName btnName;
        public readonly int runStatus;
        public CFormBtnArgs(EBtnName btnName, int runStatus)
        {
            this.btnName = btnName;
            this.runStatus = runStatus;
        }
    }
    #endregion

    #region 系统参数保存消息
    public class CSysSaveArgs : EventArgs
    {
        public readonly bool saved;
        public CSysSaveArgs(bool saved)
        {
            this.saved = saved;
        }
    }
    public class CSysCancelArgs : EventArgs
    {
        public readonly bool cancel;
        public CSysCancelArgs(bool cancel)
        {
            this.cancel = cancel;
        }
    }
    #endregion

    #region 机种参数消息
    /// <summary>
    /// 机种参数按钮消息
    /// </summary>
    public class CModelBtnArgs : EventArgs
    {
        public enum EBtnName
        {
            新建,
            打开,
            保存,
            退出
        }
        public readonly EBtnName BtnNo;
        public CModelBtnArgs(EBtnName BtnNo)
        {
            this.BtnNo = BtnNo; 
        }
    }
    /// <summary>
    /// 机种界面响应消息
    /// </summary>
    public class CModelReposeArgs : EventArgs
    {
        public enum ERepose
        { 
           新建,
           打开,
           保存,           
           退出
        }
        public readonly ERepose ReposeNo;
        public CModelReposeArgs(ERepose ReposeNo)
        {
            this.ReposeNo = ReposeNo;  
        }
    }
    #endregion

    #endregion
}

