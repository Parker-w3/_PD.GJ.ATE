using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ;
using GJ.Para.TURNON;
using GJ.Para.Base;

namespace GJ.Para.Udc.TURNON
{
    public partial class udcTURNON : UserControl
    {
   
        #region 构造函数
        public udcTURNON()
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
            //labV = new Label[]{
            //                   labV1,labV2,labV3,labV4,labV5,labV6,labV7,labV8,
            //                   };
            //labResult = new Label[]{
            //                   labResult1,labResult2,labResult3,labResult4,labResult5,labResult6,labResult7,labResult8,
            //                   };
            //labA = new Label[] {
            //                   LabA1,LabA2,LabA3,LabA4,LabA5,LabA6,LabA7,LabA8 
            //                   };
            //labP = new Label[] {
            //                   LabP1,LabP2,LabP3,LabP4,LabP5,LabP6,LabP7,LabP8 
            //                   };
            //labSN = new Label[] {
            //                   labSN1,labSN2,labSN3,labSN4,labSN5,labSN6,labSN7,labSN8
            //                   };
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

        }
     
        #endregion

        #region 字段
        public enum ERun
        {
            Idle,
            Initialize,
            Ready,
            Testing,
            Debuging,
            Pass,
            Fail
        }

        private string iniFile = Application.StartupPath + "\\" +"sysLog\\" + CGlobal.CFlow.flowGUID + ".ini";
        private CModelPara runModel = new CModelPara();
        //private Label[] labV = null;
        //private Label[] labResult = null;
        //private Label[] labA = null;
        //private Label[] labP = null;
        //private Label[] labSN = null;

        private int ttNum = 0;
        private int failNum = 0;
        private int connectorTimes = 0;

        private int ReTestTimes = 0;
        private double Vmin = 0;
        private double Vmax = 0;
        private double Imin = 0;
        private double Imax = 0;

        private double Pmax = 0;
        private double Pmin = 0;
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
        public double mIDmax
        {
            set { Pmax = value; }
        }
        public double mIDmin
        {
            set { Pmin = value; }
        }


       
        /// <summary>
        /// 机种参数
        /// </summary>
        public CModelPara mRunModel
        {
            get { return runModel; }
        }
      
        #endregion

        #region 委托
        private delegate void AddConnectorTimesHandler();
        private delegate void AddNumHandler(List<string> serialNo, List<int>result);
        private delegate void ShowDataHandler(List<string> serialNo, List<double> V, List<double>A,List<double>P,int testTimes,int msTimes);
        private delegate void SetClrHandler();
        private delegate void ShowTestTimesHandler(int testTimes);

        private delegate void ShowModelHandler(CModelPara modelPara);
        private delegate void ShowUUTNumHandler(int ttNum, int failNum);

        private delegate void SetStatusHandler(ERun runStatus);
        private delegate void SetDebugBtnHandler(int idNo, int run);
        #endregion

        #region 方法
        /// <summary>
        /// 连接器使用次数
        /// </summary>
        public void AddConnectorTimes()
        {
            if (this.InvokeRequired)
                this.Invoke(new AddConnectorTimesHandler(AddConnectorTimes));
            else
            {
                connectorTimes++;
                CIniFile.WriteToIni("Parameter", "ConnectorTimes", connectorTimes.ToString(), iniFile);
                labConnectTimes.Text = connectorTimes.ToString();
            }
        }
        /// <summary>
        /// 设置运行状态
        /// </summary>
        /// <param name="runStatus"></param>
        public void SetStatus(ERun runStatus)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetStatusHandler(SetStatus), runStatus);
            else
            {
                labStatus.Text = runStatus.ToString();
                btnSelect.Enabled = false;
                btnLft.Enabled = false;
             
                switch (runStatus)
                {
                    case ERun.Idle:
                        labStatus.ForeColor = Color.Blue;
                        labStatus.Font = new Font("宋体", 72f, FontStyle.Bold);
                        btnSelect.Enabled = true;
                        btnLft.Enabled = true;
                     
                        break;
                    case ERun.Initialize:
                        labStatus.ForeColor = Color.Red;
                        labStatus.Font = new Font("宋体", 48f, FontStyle.Bold);
                        break;
                    case ERun.Ready:
                        labStatus.ForeColor = Color.Blue;
                        labStatus.Font = new Font("宋体", 72f, FontStyle.Bold);
                        break;
                    case ERun.Testing:
                        labStatus.ForeColor = Color.Blue;
                        labStatus.Font = new Font("宋体", 72f, FontStyle.Bold);
                        break;
                    case ERun.Debuging:
                        labStatus.ForeColor = Color.Red;
                        labStatus.Font = new Font("宋体", 72f, FontStyle.Bold);
                        btnLft.Enabled = true;
                   
                        break;
                    case ERun.Pass:
                        labStatus.ForeColor = Color.Green;
                        labStatus.Font = new Font("宋体", 72f, FontStyle.Bold);
                        break;
                    case ERun.Fail:
                        labStatus.ForeColor = Color.Red;
                        labStatus.Font = new Font("宋体", 72f, FontStyle.Bold);
                        break;
                    default:
                        break;
                }
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
        ///// <summary>
        ///// 清除状态
        ///// </summary>
        //public void SetClr()
        //{
        //    if (this.InvokeRequired)
        //        this.Invoke(new SetClrHandler(SetClr));
        //    else
        //    {
        //        for (int i = 0; i < labV.Length; i++)
        //        {
        //            labSN[i].Text = "";
        //            labA[i].Text = "";
        //            labP[i].Text = "";
        //            labV[i].Text = "";
        //            labResult[i].Text = ""; 
        //        }
        //        labTestTimes.Text = "0";
        //    }            
        //}
        ///// <summary>
        ///// 刷新界面
        ///// </summary>
        ///// <param name="serialNo"></param>
        ///// <param name="V"></param>
        ///// <param name="I"></param>
        ///// <param name="result"></param>
        ///// <param name="testTimes"></param>
        //public void ShowData(List<string> serialNo, List<double> V,List<double>A,List<double>P,int testTimes, int failTimes)
        //{
        //    if (this.InvokeRequired)
        //        this.Invoke(new ShowDataHandler(ShowData), serialNo, V,A,P,testTimes, failTimes);
        //    else
        //    {               
        //        bool uutPass = true;
        //        int[] result = new int[serialNo.Count];
        //        for (int i = 0; i < serialNo.Count; i++)
        //        {
        //            result[i] = 0;
        //            if (serialNo[i] == "")
        //            {
        //                labV[i].Text = "---";
        //                labA[i].Text = "---";
        //                labP[i].Text = "---";
        //                labSN[i].Text = "";
        //                labV[i].ForeColor = Color.Black;
        //                labA[i].ForeColor = Color.Black;
        //                labP[i].ForeColor = Color.Black;
        //                labResult[i].Text = "---";
        //                labResult[i].ForeColor = Color.Black;
        //            }
        //            else
        //            {
        //                labV[i].Text = V[i].ToString();
        //                labA[i].Text = A[i].ToString();
        //                labP[i].Text = P[i].ToString();
        //                labSN[i].Text = serialNo[i];
        //                if (V[i] < Vmin || V[i] > Vmax)
        //                {
        //                    labV[i].ForeColor = Color.Red;
        //                    result[i] = 1;
        //                }
        //                else
        //                    labV[i].ForeColor = Color.Blue;

        //                if (A[i] < Imin || A[i] > Imax)
        //                {
        //                    labA[i].ForeColor = Color.Red;
        //                    result[i] = 1;
        //                }
        //                else
        //                    labA[i].ForeColor = Color.Blue;

        //                if (P[i] < Pmin || P[i] > Pmax)
        //                {
        //                    labP[i].ForeColor = Color.Red;
        //                    result[i] = 1;
        //                }
        //                else
        //                    labP[i].ForeColor = Color.Blue;

        //                if (result[i] != 0)
        //                    uutPass = false;  
        //            }
        //        }
        //        if (uutPass)
        //        {
        //            for (int i = 0; i < serialNo.Count; i++)
        //            {
        //                if (serialNo[i] != "")
        //                {
        //                    labResult[i].Text = "PASS";
        //                    labResult[i].ForeColor = Color.Blue;   
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (failTimes < ReTestTimes)
        //            {
        //                for (int i = 0; i < serialNo.Count; i++)
        //                {
        //                    if (serialNo[i] != "")
        //                    {
        //                        labResult[i].Text = "";
        //                        labResult[i].ForeColor = Color.Black;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                for (int i = 0; i < serialNo.Count; i++)
        //                {
        //                    if (serialNo[i] != "")
        //                    {
        //                        if (result[i] == 0)
        //                        {
        //                            labResult[i].Text = "PASS";
        //                            labResult[i].ForeColor = Color.Blue;
        //                        }
        //                        else
        //                        {
        //                            labResult[i].Text = "FAIL";
        //                            labResult[i].ForeColor = Color.Red;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        labTestTimes.Text = (((double)testTimes) / 1000).ToString("0.0");
        //        this.Refresh(); 
        //    }
        //}
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
        //public void setUIEnable(bool enabled)
        //{
        //    if (this.InvokeRequired)
        //        this.Invoke(new setUIEnableHandler(setUIEnable), enabled);
        //    else
        //    {
        //        if (enabled)
        //        {
        //            btnClrNum.Enabled = true;
        //           btnClrConnector.Enabled = true;
        //        }
        //        else
        //        {
        //            btnClrNum.Enabled = false;
        //            btnClrConnector.Enabled = false;
        //        }
        //    }
        //}
        ///// <summary>
        ///// 输入AC电压
        ///// </summary>
        ///// <param name="acv"></param>
        ///// <param name="flag"></param>
        //public void setACV(double acv, int flag = 1)
        //{
        //    if (this.InvokeRequired)
        //       this.Invoke(new Action<double,int>(setACV),acv,flag);
        //    else
        //    {
        //        labConnectTimes.Text = acv.ToString()+"V";
        //        if (flag==1)
        //            labConnectTimes.ForeColor = Color.Blue;
        //        else if (flag==2)
        //            labConnectTimes.ForeColor = Color.Red; 
        //        else
        //            labConnectTimes.ForeColor = Color.Black; 
        //    }
        //}

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
                labVSpec.Text = "";
                labIDSpec.Text = "";
                labISpec.Text = "";
                for (int i = 0; i < 3; i++)
                {
                    if (modelPara.ChkModel[i])
                    {
                        labVSpec.Text = labVSpec.Text + "   " + modelPara.vMin[i].ToString() +
                                    "V~" + modelPara.vMax[i].ToString() + "V";
                        if (modelPara.ChkID)
                        {
                            labNameID.Visible = true;
                            labIDSpec.Text = labIDSpec.Text + "   " + modelPara.IDmin[i].ToString() +
                                             "V~" + modelPara.IDmax[i].ToString() + "V";
                        }
                        else
                        {
                            labNameID.Visible = false ;
                            labIDSpec.Text = "";
                        }
                        if (modelPara.loadMode[i] == 0)
                            labISpec.Text = labISpec.Text + "  [CC模式-" + modelPara.loadSet[i].ToString() + "A]:" + modelPara.loadMin[i].ToString() +
                                          "A~" + modelPara.loadMax[i].ToString() + "A";
                        else if (modelPara.loadMode[i] == 1)
                            labISpec.Text = labISpec.Text + "  [CV模式-" + modelPara.loadSet[i].ToString() + "V]:" + modelPara.loadMin[i].ToString() +
                                          "A~" + modelPara.loadMax[i].ToString() + "A";
                        else
                            labISpec.Text = labISpec.Text + "  [CP模式-" + modelPara.loadSet[i].ToString() + "W]:" + modelPara.loadMin[i].ToString() +
                                          "A~" + modelPara.loadMax[i].ToString() + "A";
                    }

                }

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
        /// 设置调式按钮状态
        /// </summary>
        /// <param name="idNo"></param>
        /// <param name="run"></param>
        public void SetDebugBtn(int idNo, int run)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetDebugBtnHandler(SetDebugBtn), idNo, run);
            else
            {
                if (idNo == 0)
                {
                    if (run == 1)
                        btnLft.Text = "停止(&L)";
                    else
                        btnLft.Text = "调试(&L)";
                }
             
            }
        }
        #endregion
        /// <summary>
        /// 清除数量统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClrNum_Click_1(object sender, EventArgs e)
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
                    labFailRate.Text = ((double)failNum * 100 / (double)ttNum).ToString("0.0") + "%";
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string fileDirectry = string.Empty;
            fileDirectry = CSysPara.mVal.modelPath;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = fileDirectry;
            dlg.Filter = "spec files (*.TurnOn)|*.TurnOn";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            CModelSet<CModelPara>.load(dlg.FileName, ref runModel);

            labModel.Text = runModel.model;

            labCustom.Text = runModel.custom;

            labVersion.Text = runModel.version;
            labVSpec.Text = "";
            labIDSpec.Text = "";
            labISpec.Text = "";
   
            for (int i = 0; i < 3; i++)
            {
                if (runModel .ChkModel[i])
                {
                    labVSpec.Text =labVSpec.Text + "  " + runModel.vMin[i].ToString() +
                                "V~" + runModel.vMax[i].ToString() + "V";
                    if (runModel.ChkID)
                    {
                        labNameID.Visible = true;
                        labIDSpec.Text = labIDSpec.Text + "   " + runModel.IDmin[i].ToString() +
                                         "V~" + runModel.IDmax[i].ToString() + "V";
                    }
                    else
                    {
                        labNameID.Visible = false;
                        labIDSpec.Text = "";
                    }

                    if (runModel.loadMode[i] == 0)
                        labISpec.Text = labISpec.Text + " [CC模式-" + runModel.loadSet[i].ToString() + "A]:" + runModel.loadMin[i].ToString() +
                                      "A~" + runModel.loadMax[i].ToString() + "A";
                    else if (runModel.loadMode[i] == 1)
                        labISpec.Text = labISpec.Text + " [CV模式-" + runModel.loadSet[i].ToString() + "V]:" + runModel.loadMin[i].ToString() +
                                      "A~" + runModel.loadMax[i].ToString() + "A";
                    else
                        labISpec.Text = labISpec.Text + " [CP模式-" + runModel.loadSet[i].ToString() + "W]:" + runModel.loadMin[i].ToString() +
                                      "A~" + runModel.loadMax[i].ToString() + "A";
                }

            }

            CIniFile.WriteToIni("Parameter", "Model", dlg.FileName, iniFile);

            dlg = null;

          
            OnBtnClick.OnEvented(new COnBtnClick(EBtnNo.选机种));    
        }

        #region 事件
        public enum EBtnNo
        {
            选机种,
            清除数量
        }
        public class COnBtnClick : EventArgs
        {
            public readonly EBtnNo btnNo;
            public COnBtnClick(EBtnNo btnNo)
            {
                this.btnNo = btnNo;
            }
        }
        public COnEvent<COnBtnClick> OnBtnClick = new COnEvent<COnBtnClick>();
        #endregion

        private void btnLft_Click(object sender, EventArgs e)
        {
            if (btnLft.Text == "调试(&L)")
            {
                btnLft.Text = "停止(&L)";
                OnBtnArgs.OnEvented(new COnBtnClickArgs(0, 1));
            }
            else
            {
                btnLft.Text = "调试(&L)";
                OnBtnArgs.OnEvented(new COnBtnClickArgs(0, 0));
            }
        }

        public class COnBtnClickArgs : EventArgs
        {
            public readonly int idNo;
            public readonly int run;
            public COnBtnClickArgs(int idNo, int run)
            {
                this.idNo = idNo;
                this.run = run;
            }
        }
        public COnEvent<COnBtnClickArgs> OnBtnArgs = new COnEvent<COnBtnClickArgs>();



        /// <summary>
        /// 清除连接器使用次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labConnectTimes_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要解除连接器次数?", "解除次数", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                connectorTimes = 0;
                CIniFile.WriteToIni("Parameter", "ConnectorTimes", connectorTimes.ToString(), iniFile);
                labConnectTimes.Text = connectorTimes.ToString();
            }
        } 

    }
}
