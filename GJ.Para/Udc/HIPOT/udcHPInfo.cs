using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GJ.Para.Base; 
using GJ.Para.HIPOT;
namespace GJ.Para.Udc.HIPOT
{
    public partial class udcHPInfo : UserControl
    {
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
        #region 构造函数
        public udcHPInfo()
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
            panel4.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel4, true, null);
            panel5.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel5, true, null);

        }
        #endregion

        #region 字段
        private string iniFile = Application.StartupPath + "\\" + CGlobal.CFlow.flowGUID + ".ini";
        private CModelPara runModel = new CModelPara();
        private int ttNum = 0;
        private int passNum = 0;
        private int connectorTimes = 0;

        #endregion

        #region 属性
        /// <summary>
        /// 机种参数
        /// </summary>
        public CModelPara mRunModel
        {
            get { return runModel; }
        }
        public int mConnectorTimes
        {
            get { return connectorTimes; }
        }
        #endregion

        #region 面板回调函数
        private void udcHPInfo_Load(object sender, EventArgs e)
        {
            InitialUI();
        }
        private void btnSelectModel_Click(object sender, EventArgs e)
        {
            string fileDirectry = string.Empty;
            fileDirectry = CSysPara.mVal.modelPath;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = fileDirectry;
            dlg.Filter = "hp files (*.hp)|*.hp";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            CModelSet<CModelPara>.load(dlg.FileName, ref runModel);

            labModel.Text = runModel.model;

            labCustom.Text = runModel.custom;

            labVersion.Text = runModel.version;

            labItemNum.Text = runModel.step.Count.ToString();

            CIniFile.WriteToIni("Parameter", "Model", dlg.FileName, iniFile); 

            dlg = null;

            OnBtnArgs.OnEvented(new COnBtnClickArgs(2,0)); 
        }
        private void btnClrNum_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("确定要清除测试数量统计?","测试数量统计",MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes)
           {
               ttNum = 0;
               passNum = 0;
               refreshNum();
           }
        }
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
        private void btnRgt_Click(object sender, EventArgs e)
        {
            if (btnRgt.Text == "调试(&R)")
            {                
                btnRgt.Text = "停止(&R)";
                OnBtnArgs.OnEvented(new COnBtnClickArgs(1, 1));
            }
            else
            {                
                btnRgt.Text = "调试(&R)";
                OnBtnArgs.OnEvented(new COnBtnClickArgs(1, 0));
            }
        }
        #endregion

        #region 私有方法
        private void InitialUI()
        {
            try
            {
                string strTemp = string.Empty;

                strTemp = CIniFile.ReadFromIni("Parameter", "ConnectorTimes", iniFile);
                if (strTemp == "")
                    connectorTimes = 0;
                else
                    connectorTimes = System.Convert.ToInt32(strTemp);

                strTemp = CIniFile.ReadFromIni("Parameter", "TotalNum", iniFile);
                if (strTemp == "")
                    ttNum = 0;
                else
                    ttNum = System.Convert.ToInt32(strTemp);
                strTemp = CIniFile.ReadFromIni("Parameter", "PassNum", iniFile);
                if (strTemp == "")
                    passNum = 0;
                else
                    passNum = System.Convert.ToInt32(strTemp);
                labTTNum.Text = ttNum.ToString();
                labPassNum.Text = passNum.ToString();
                labFailNum.Text = (ttNum - passNum).ToString();
                if (ttNum == 0)
                    labFailRate.Text = "0.0";
                else
                    labFailRate.Text = ((double)(ttNum - passNum) * 100 / (double)ttNum).ToString("0.0");
                string modelFile = CIniFile.ReadFromIni("Parameter", "Model", iniFile);
                if (modelFile == "" || !File.Exists(modelFile))
                    return;
                CModelSet<CModelPara>.load(modelFile, ref runModel);

                labModel.Text = runModel.model;

                labCustom.Text = runModel.custom;

                labVersion.Text = runModel.version;

                labItemNum.Text = runModel.step.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());  
            }
        }
        private void refreshNum()
        {
            labTTNum.Text = ttNum.ToString();
            labPassNum.Text = passNum.ToString();
            labFailNum.Text = (ttNum - passNum).ToString();
            if (ttNum == 0)
                labFailRate.Text = "0.0";
            else
                labFailRate.Text = ((double)(ttNum - passNum) * 100 / (double)ttNum).ToString("0.0");
            CIniFile.WriteToIni("Parameter", "TotalNum", ttNum.ToString(), iniFile);
            CIniFile.WriteToIni("Parameter", "PassNum", passNum.ToString(), iniFile);
        }
        #endregion

        #region 事件
        public class COnBtnClickArgs : EventArgs
        {
            public readonly int idNo;
            public readonly int run;
            public COnBtnClickArgs(int idNo,int run)
            {
                this.idNo = idNo;
                this.run = run;
            }
        }
        public COnEvent<COnBtnClickArgs> OnBtnArgs = new COnEvent<COnBtnClickArgs>(); 
        #endregion

        #region 委托
        private delegate void AddConnectorTimesHandler();
        private delegate void SetNumHandler(int ttNum, int passNum);
        private delegate void SetStatusHandler(ERun runStatus);
        private delegate void SetTimesHandler(int timeMs);
        private delegate void SetNewModelHandler(CModelPara modelPara, string modelPath);
        private delegate void SetDebugBtnHandler(int idNo, int run);
        #endregion

        #region 共享方法
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
        /// 设置数量统计
        /// </summary>
        /// <param name="ttNum"></param>
        /// <param name="passNum"></param>
        public void SetNum(int ttNum, int passNum)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetNumHandler(SetNum), ttNum, passNum);
            else
            {
                this.ttNum += ttNum;
                this.passNum += passNum;
                refreshNum();
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
                btnSelectModel.Enabled = false;
                btnLft.Enabled = false;
                btnRgt.Enabled = false;  
                switch (runStatus)
                {
                    case ERun.Idle:
                        labStatus.ForeColor = Color.Blue;
                        labStatus.Font = new Font("宋体", 72f, FontStyle.Bold);  
                        btnSelectModel.Enabled = true;
                        btnLft.Enabled = true;
                        btnRgt.Enabled = true;  
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
                        btnRgt.Enabled = true; 
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
        /// 设置测试时间
        /// </summary>
        /// <param name="timeMs"></param>
        public void SetTimes(int timeMs)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetTimesHandler(SetTimes), timeMs);
            else
            {
                double testTimes = (double)timeMs / 1000;
                labTestTimes.Text = testTimes.ToString("0.0") + "s";    
            }
        }
        /// <summary>
        /// 设置新机种
        /// </summary>
        /// <param name="modelPara"></param>
        /// <param name="modelPath"></param>
        public void SetNewModel(CModelPara modelPara, string modelPath)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetNewModelHandler(SetNewModel), modelPara, modelPath);
            else
            {
                runModel = modelPara;
 
                labModel.Text = runModel.model;

                labCustom.Text = runModel.custom;

                labVersion.Text = runModel.version;

                labItemNum.Text = runModel.step.Count.ToString();

                CIniFile.WriteToIni("Parameter", "Model", modelPath, iniFile); 

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
               if(idNo==0)
               {
                   if (run==1)
                       btnLft.Text = "停止(&L)";                   
                   else
                       btnLft.Text = "调试(&L)";
               }
               else
               {
                   if (run == 1)
                       btnRgt.Text = "停止(&R)";
                   else
                       btnRgt.Text = "调试(&R)"; 
               }
            }
        }
        #endregion

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

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (labTTNum.Visible)
            {
                labTTNum.Visible = false;
                labPassNum.Visible = false;
                labFailNum.Visible = false;
                labFailRate.Visible = false;
                labConnectTimes.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
             

            }
            else
            {
                labTTNum.Visible = true;
                labPassNum.Visible = true ;
                labFailNum.Visible = true;
                labFailRate.Visible = true;
                labConnectTimes.Visible = true;
                label6.Visible = true ;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
            }
        }

    

    }
}
