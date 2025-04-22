using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ;
using GJ.Para.LOADUP;

namespace GJ.Para.Udc.LOADUP
{
    public partial class udcSnBand : UserControl
    {
        #region 构造函数
        public udcSnBand()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();
        }
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
                                          .SetValue(panel2, true, null);
            panel3.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel3, true, null);
        }
        #endregion

        #region 面板回调函数
        private void btnClr_Click(object sender, EventArgs e)
        {
            OnBtnClick.OnEvented(new COnBtnClick(EBtnNo.清除数量));    
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OnBtnClick.OnEvented(new COnBtnClick(EBtnNo.选机种));    
        }
        #endregion

        #region 委托
        private delegate void ShowModelHandler(CModelPara modelPara);
        private delegate void ShowUUTNumHandler(int ttNum, int failNum);
        private delegate void ShowFixtureHandler(string status, bool bFail);
        #endregion

        #region 事件
        public enum EBtnNo
        {
          选机种,
          清除数量
        }
        public class COnBtnClick:EventArgs 
        {
            public readonly EBtnNo btnNo;
            public COnBtnClick(EBtnNo btnNo)
            {
                this.btnNo = btnNo;  
            }
        }
        public COnEvent<COnBtnClick> OnBtnClick = new COnEvent<COnBtnClick>(); 
        #endregion

        #region 属性
        /// <summary>
        /// 是否为空治具
        /// </summary>
        public int mFixIsNull
        {
            get {
                if (chkFixNull.Checked)
                    return 1;
                else
                    return 0;
                }
        }
        /// <summary>
        /// 点检治具
        /// </summary>
        public bool mSpotCheckFix
        {
            get
            {
                return chkSpotCheckFix.Checked;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 显示机种信息
        /// </summary>
        /// <param name="modelPara"></param>
        public void ShowModel(CModelPara modelPara)
        {
            if (this.InvokeRequired)
                this.Invoke(new ShowModelHandler(ShowModel), modelPara);
            else
            {
                labModel.Text = modelPara.model;
                labCustom.Text = modelPara.custom;
                labVersion.Text = modelPara.version;
                if (modelPara.acv == 0)
                    labACSpec.Text = "90ACV";
                else if (modelPara.acv == 1)
                    labACSpec.Text = "220ACV";
                else
                    labACSpec.Text = "264ACV";
                labVSpec.Text = "[" + modelPara.vName + "]:" + modelPara.vMin.ToString() +
                                "V~" + modelPara.vMax.ToString() + "V";
                if (modelPara.loadMode == 0)
                    labISpec.Text = "[CC模式-"+  modelPara.loadSet.ToString()+ "A]:" + modelPara.loadMin.ToString() +
                                  "A~" + modelPara.loadMax.ToString() + "A";                   
                else if (modelPara.loadMode == 1)
                    labISpec.Text = "[CV模式-" + modelPara.loadSet.ToString() + "V]:" + modelPara.loadMin.ToString() +
                                  "A~" + modelPara.loadMax.ToString() + "A";  
                else
                    labISpec.Text = "[LED模式-" + modelPara.loadSet.ToString() + "V]:" + modelPara.loadMin.ToString() +
                                  "A~" + modelPara.loadMax.ToString() + "A";  
            }
        }
        /// <summary>
        /// 显示数量
        /// </summary>
        /// <param name="ttNum"></param>
        /// <param name="failNum"></param>
        public void ShowUUTNum(int ttNum, int failNum)
        {
            if (this.InvokeRequired)
                this.Invoke(new ShowUUTNumHandler(ShowUUTNum), ttNum, failNum);
            else
            {
                labTTNum.Text = ttNum.ToString();
                labFailNum.Text = failNum.ToString();
            }
        }
        /// <summary>
        /// 显示治具信息
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ttNum"></param>
        /// <param name="failNum"></param>
        /// <param name="bFail"></param>
        public void ShowFixture(string status,bool bFail=false)
        {
            if (this.InvokeRequired)
                this.Invoke(new ShowFixtureHandler(ShowFixture), status, bFail);
            else
            {               
                labStatus.Text = status;
                if (!bFail)
                    labStatus.ForeColor = Color.Blue;
                else
                    labStatus.ForeColor = Color.Red;   
            }        
        }

        private delegate void setUIEnableHandler(bool enabled);
        public void setUIEnable(bool enabled)
        {
            if (this.InvokeRequired)
                this.Invoke(new setUIEnableHandler(setUIEnable), enabled);
            else
            {
                if (enabled)
                {
                    chkFixNull.Enabled = true;
                    btnClr.Enabled = true;
                }
                else
                {
                    chkFixNull.Enabled = false;
                    btnClr.Enabled = false;
                }
            }
        }
        #endregion

    }
}
