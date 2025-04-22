using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ;

namespace GJ.Para.Udc.LOADUP
{
    public partial class udcPreTest : UserControl
    {
        #region 构造函数
        public udcPreTest()
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

            load();
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            labV = new Label[]{
                               labV1,labV2,labV3,labV4,labV5,labV6,labV7,labV8,
                               labV9,labV10,labV11,labV12,labV13,labV14,labV15,labV16
                               };
            labCur = new Label[]{
                               labCur1,labCur2 ,labCur3,labCur4,labCur5,labCur6,labCur7,labCur8,
                               labCur9,labCur10 ,labCur11,labCur12,labCur13,labCur14,labCur15,labCur16
                               };
            labResult = new Label[]{
                               labResult1,labResult2,labResult3,labResult4,labResult5,labResult6,labResult7,labResult8,
                               labResult9,labResult10,labResult11,labResult12,labResult13,labResult14,labResult15,labResult16
                               };
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
        /// <summary>
        /// 加载数据
        /// </summary>
        private void load()
        {

            string strVal = string.Empty;

            strVal = CIniFile.ReadFromIni("Parameter", "ConnectorTimes", iniFile);
            if (strVal == "")
                connectorTimes = 0;
            else
                connectorTimes = System.Convert.ToInt32(strVal);
            strVal = CIniFile.ReadFromIni("Parameter", "TTNum", iniFile);
            if (strVal == "")
                ttNum = 0;
            else
                ttNum = System.Convert.ToInt32(strVal);
            strVal = CIniFile.ReadFromIni("Parameter", "FailNum", iniFile);
            if (strVal == "")
                failNum = 0;
            else
                failNum = System.Convert.ToInt32(strVal);

            labConnectTimes.Text = connectorTimes.ToString();

            labTTNum.Text = ttNum.ToString();

            labFailNum.Text = failNum.ToString();   

            if (ttNum == 0)
                labFailRate.Text = "0.0%";
            else
                labFailRate.Text = ((double)failNum * 100 / (double)ttNum).ToString("0.0")+"%";  
        }
        #endregion

        #region 面板回调函数
        private void btnClrNum_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清除产品统计数量?", "清除数量", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                ttNum = 0;
                failNum = 0;                
                CIniFile.WriteToIni("Parameter", "TTNum", ttNum.ToString(), iniFile);
                CIniFile.WriteToIni("Parameter", "FailNum", failNum.ToString(), iniFile);
                labTTNum.Text = ttNum.ToString();
                labFailNum.Text = failNum.ToString();
                if (ttNum == 0)
                    labFailRate.Text = "0.0%";
                else
                    labFailRate.Text = ((double)failNum * 100 / (double)ttNum).ToString("0.0")+"%";
            }
        }
        private void btnClrConnector_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要解除连接器次数?", "解除次数", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
               DialogResult.Yes)
            {
                connectorTimes = 0;
                CIniFile.WriteToIni("Parameter", "ConnectorTimes", connectorTimes.ToString(), iniFile);
                labConnectTimes.Text = connectorTimes.ToString();
            }
        }
        #endregion

        #region 字段

        private string iniFile = "sysLog\\" + CGlobal.CFlow.flowGUID + ".ini";

        private Label[] labV = null;
        private Label[] labResult = null;
        private Label[] labCur = null;

        private int ttNum = 0;
        private int failNum = 0;
        private int connectorTimes = 0;

        private int ReTestTimes = 0;
        private double Vmin = 0;
        private double Vmax = 0;
        private double Imin = 0;
        private double Imax = 0;

        #endregion

        #region 属性
        public int mTTNum
        {
            get { return ttNum; }
        }
        public int mFailNum
        {
            get { return failNum; }
        }
        public int mConnectorTimes
        {
            get { return connectorTimes; }
        }
        public int mReTestTimes
        {
            set { ReTestTimes = value; }
        }
        public double mVmin
        {
            set { Vmin = value; }
        }        
        public double mVmax
        {
            set { Vmax = value; }
        }
        public double mImin
        {
            set { Imin = value; }
        }
        public double mImax
        {
            set { Imax = value; }
        }
        #endregion

        #region 委托
        private delegate void AddConnectorTimesHandler();
        private delegate void AddNumHandler(List<string> serialNo, List<int>result);
        private delegate void ShowDataHandler(List<string> serialNo, List<double> V,List<double>Cur, int testTimes,int msTimes);
        private delegate void SetClrHandler();
        private delegate void ShowTestTimesHandler(int testTimes);
        #endregion

        #region 方法
        /// <summary>
        /// 连接器使用次数
        /// </summary>
        public void AddConnectorTimes()        
        {
            if(this.InvokeRequired)
                this.Invoke(new AddConnectorTimesHandler(AddConnectorTimes));
            else
            {
                connectorTimes++;
                CIniFile.WriteToIni("Parameter", "ConnectorTimes", connectorTimes.ToString(), iniFile);
                labConnectTimes.Text = connectorTimes.ToString();
            }            
        }
        /// <summary>
        /// 测试产品数量
        /// </summary>
        /// <param name="bFail"></param>
        public void AddNum(List<string> serialNo, List<int> result)
        {
            if (this.InvokeRequired)
                this.Invoke(new AddNumHandler(AddNum), serialNo, result);
            else
            {
                for (int i = 0; i < serialNo.Count; i++)
                {
                    if (serialNo[i] != "")
                    {
                        ttNum++;
                        if (result[i] != 0)
                            failNum++;
                    }
                }
                CIniFile.WriteToIni("Parameter", "TTNum", ttNum.ToString(), iniFile);
                CIniFile.WriteToIni("Parameter", "FailNum", failNum.ToString(), iniFile);
                labTTNum.Text = ttNum.ToString();
                labFailNum.Text = failNum.ToString();
                if (ttNum == 0)
                    labFailRate.Text = "0.0%";
                else
                    labFailRate.Text = ((double)failNum * 100 / (double)ttNum).ToString("0.0")+"%";
            }            
        }
        /// <summary>
        /// 清除状态
        /// </summary>
        public void SetClr()
        {
            if (this.InvokeRequired)
                this.Invoke(new SetClrHandler(SetClr));
            else
            {
                for (int i = 0; i < labV.Length; i++)
                {
                    labV[i].Text = "";
                    labCur[i].Text = "";
                    labResult[i].Text = "";
                }
                labTestTimes.Text = "0";
            }
        }
        /// <summary>
        /// 刷新界面
        /// </summary>
        /// <param name="serialNo"></param>
        /// <param name="V"></param>
        /// <param name="I"></param>
        /// <param name="result"></param>
        /// <param name="testTimes"></param>
        public void ShowData(List<string> serialNo, List<double> V, List<double> Cur, int testTimes, int failTimes)
        {
            if (this.InvokeRequired)
                this.Invoke(new ShowDataHandler(ShowData), serialNo, V, Cur, testTimes, failTimes);
            else
            {
                bool uutPass = true;
                int[] result = new int[serialNo.Count];
                for (int i = 0; i < serialNo.Count; i++)
                {
                    result[i] = 0;
                    if (serialNo[i] == "")
                    {
                        labV[i].Text = "---";
                        labV[i].ForeColor = Color.Black;
                        labCur[i].Text = "---";
                        labCur[i].ForeColor = Color.Black;
                        labResult[i].Text = "---";
                        labResult[i].ForeColor = Color.Black;
                    }
                    else
                    {
                        labV[i].Text = V[i].ToString();
                        labCur[i].Text = Cur[i].ToString();
                        if (V[i] < Vmin || V[i] > Vmax)
                        {
                            labV[i].ForeColor = Color.Red;
                            result[i] = 1;
                        }
                        else
                            labV[i].ForeColor = Color.Blue;
                        if (Cur[i] < Imin || Cur[i] > Imax)
                        {
                            labCur[i].ForeColor = Color.Red;
                            result[i] = 1;
                        }
                        else
                            labCur[i].ForeColor = Color.Blue;
                        if (result[i] != 0)
                            uutPass = false;
                    }
                }
                if (uutPass)
                {
                    for (int i = 0; i < serialNo.Count; i++)
                    {
                        if (serialNo[i] != "")
                        {
                            labResult[i].Text = "PASS";
                            labResult[i].ForeColor = Color.Blue;
                        }
                    }
                }
                else
                {
                    if (failTimes < ReTestTimes)
                    {
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            if (serialNo[i] != "")
                            {
                                labResult[i].Text = "";
                                labResult[i].ForeColor = Color.Black;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < serialNo.Count; i++)
                        {
                            if (serialNo[i] != "")
                            {
                                if (result[i] == 0)
                                {
                                    labResult[i].Text = "PASS";
                                    labResult[i].ForeColor = Color.Blue;
                                }
                                else
                                {
                                    labResult[i].Text = "FAIL";
                                    labResult[i].ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                }
                labTestTimes.Text = (((double)testTimes) / 1000).ToString("0.0");
                this.Refresh();
            }
        }
        /// <summary>
        /// 显示测试时间
        /// </summary>
        /// <param name="testTimes"></param>
        public void ShowTestTimes(int testTimes)
        {
            if (this.InvokeRequired)
                this.Invoke(new ShowTestTimesHandler(ShowTestTimes), testTimes);
            else
            {
                labTestTimes.Text = (((double)testTimes) / 1000).ToString("0.0");
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
                    btnClrNum.Enabled = true;
                    btnClrConnector.Enabled = true;
                }
                else
                {
                    btnClrNum.Enabled = false;
                    btnClrConnector.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 输入AC电压
        /// </summary>
        /// <param name="acv"></param>
        /// <param name="flag"></param>
        public void setACV(double acv, int flag = 1)
        {
            if (this.InvokeRequired)
               this.Invoke(new Action<double,int>(setACV),acv,flag);
            else
            {
                labACV.Text = acv.ToString()+"V";
                if (flag==1)
                    labACV.ForeColor = Color.Blue;
                else if (flag==2)
                    labACV.ForeColor = Color.Red; 
                else
                    labACV.ForeColor = Color.Black; 
            }
        }
        #endregion
    }
}
