using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace GJ.Para.BURNIN
{
    public partial class udcUUTInfo : Form
    {
        #region 构造函数

        public udcUUTInfo(udcRun.CUUT runUUT)
        {
            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

            this.runUUT = runUUT;

            this.idNo = runUUT.wBase.uutNo - 1;

        }
        #endregion

        #region 字段
        private int C_SLOT_NUM = 8;
        private int idNo = 0;
        private udcRun.CUUT runUUT = null;
        private int runNowTime = 0;
        #endregion

        #region 初始化
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            for (int i = 0; i < C_SLOT_NUM; i++)
		 {
             Label labId = new Label();
             labId.Name = "labId" + i.ToString();  
             labId.Dock = DockStyle.Fill;
             labId.Margin = new Padding(3);   
             labId.Font = new Font("宋体", 12); 
             labId.TextAlign = ContentAlignment.MiddleCenter;
             labId.Text = "槽位产品" + (i + 1).ToString("D2")+":";
             labIdList.Add(labId);

             Label labSn = new Label();
             labSn.Name = "labSn" + i.ToString();
             labSn.Dock = DockStyle.Fill;
             labSn.Margin = new Padding(3);
             labSn.Font = new Font("宋体", 12);
             labSn.TextAlign = ContentAlignment.MiddleCenter;
             labSn.Text = "";
             labSnList.Add(labSn);

             Label labResult= new Label();
             labResult.Name = "labResult" + i.ToString();
             labResult.Dock = DockStyle.Fill;
             labResult.Margin = new Padding(3);
             labResult.Font = new Font("宋体", 12);
             labResult.TextAlign = ContentAlignment.MiddleCenter;
             labResult.Text = "";
             labResultList.Add(labResult);

             Label labVolt = new Label();
             labVolt.Name = "labVolt" + i.ToString();
             labVolt.Dock = DockStyle.Fill;
             labVolt.Margin = new Padding(3);
             labVolt.Font = new Font("宋体", 12);
             labVolt.TextAlign = ContentAlignment.MiddleCenter;
             labVolt.Text = "";
             labVoltList.Add(labVolt);

             Label labCur = new Label();
             labCur.Name = "labCur" + i.ToString();
             labCur.Dock = DockStyle.Fill;
             labCur.Margin = new Padding(3);
             labCur.Font = new Font("宋体", 12);
             labCur.TextAlign = ContentAlignment.MiddleCenter;
             labCur.Text = "";
             labCurList.Add(labCur);

             panel2.Controls.Add(labIdList[i], 0, i + 1);
             panel2.Controls.Add(labSnList[i], 1, i + 1);
             panel2.Controls.Add(labResultList[i], 2, i + 1);
             panel2.Controls.Add(labVoltList[i], 3, i + 1);
             panel2.Controls.Add(labCurList[i], 4, i + 1);  
		 }
          udcOnOff.Dock = DockStyle.Fill;   
          panel4.Controls.Add(udcOnOff, 0, 0);    
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

        }
        #endregion

        #region 面板控件
        private List<Label> labIdList = new List<Label>();
        private List<Label> labSnList = new List<Label>();
        private List<Label> labResultList = new List<Label>();
        private List<Label> labVoltList=new List<Label>();
        private List<Label> labCurList=new List<Label>();
        private Udc.BURNIN.udcOnOffWave udcOnOff = new Udc.BURNIN.udcOnOffWave();  
        #endregion

        #region 面板回调函数
        private void udcUUTInfo_Load(object sender, EventArgs e)
        {
            lablocalName.Text = runUUT.wBase.localName;    
            labModel.Text = runUUT.wPara.modelName;
            labStartTime.Text = runUUT.wPara.startTime;
            labIdCard.Text = runUUT.wPara.idCard;
            labBITime.Text = (((double)runUUT.wPara.burnTime) / 3600).ToString("0.0")+"H"; 
            DateTime endTime;
            if (runUUT.wPara.startTime == "")
                 labEndTime.Text = "";
            else
            {
                endTime = (System.Convert.ToDateTime(runUUT.wPara.startTime)).AddSeconds(runUUT.wPara.burnTime);
                labEndTime.Text = endTime.ToString("yyyy/MM/dd HH:mm:ss");
            }
            runNowTime = runUUT.wPara.runTime;
            TimeSpan ts = new TimeSpan(0, 0, runNowTime);
            TimeSpan tl = new TimeSpan(0, 0, runUUT.wPara.burnTime - runNowTime);
            string runTime = ts.Days.ToString("D2") + ":" + ts.Hours.ToString("D2") + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");
            string leftTime = tl.Days.ToString("D2") + ":" + tl.Hours.ToString("D2") + ":" + tl.Minutes.ToString("D2") + ":" + tl.Seconds.ToString("D2");
            labRunTime.Text = runTime;
            labLeftTime.Text = leftTime; 
            for (int i = 0; i < runUUT.wLed.Count; i++)
            {
                if (runUUT.wLed[i].serialNo == "")
                {
                    labSnList[i].Text = "----";
                    labSnList[i].ForeColor = Color.Black;  
                    labResultList[i].Text = "----";
                    labResultList[i].ForeColor = Color.Black;  
                    labVoltList[i].Text = "----";
                    labVoltList[i].ForeColor = Color.Black;  
                    labCurList[i].Text = "----";
                    labCurList[i].ForeColor = Color.Black;  
                }
                else
                {
                    labSnList[i].Text = runUUT.wLed[i].serialNo;
                    labSnList[i].ForeColor = Color.Black;
                    labVoltList[i].Text = runUUT.wLed[i].unitV.ToString("0.000");
                    labCurList[i].Text = runUUT.wLed[i].unitA.ToString("0.00");
                    int stepNo = 0;
                    for (int CH = 0; CH < runUUT.wLed[0].vName.Count; CH++)
                    {
                        if (runUUT.wPara.runQCStepNo == runUUT.wLed[0].QCV[CH])
                        {
                            stepNo = CH;
                            break;
                        }
                    }
                    if (runUUT.wLed[i].unitV >= runUUT.wLed[i].vMin[stepNo] &&
                       runUUT.wLed[i].unitV <= runUUT.wLed[i].vMax[stepNo])                    
                        labVoltList[i].ForeColor = Color.Blue;
                    else
                        labVoltList[i].ForeColor = Color.Red;
                    if (runUUT.wLed[i].unitA >= runUUT.wLed[i].iMin[stepNo] &&
                       runUUT.wLed[i].unitA <= runUUT.wLed[i].iMax[stepNo])
                        labCurList[i].ForeColor = Color.Blue;
                    else
                        labCurList[i].ForeColor = Color.Red;
                    if (runUUT.wLed[i].passResult == 0)
                    {
                        labResultList[i].Text = "PASS";
                        labResultList[i].ForeColor = Color.Blue;  
                    }
                    else
                    {
                        labResultList[i].Text = "FAIL";
                        labResultList[i].ForeColor = Color.Red;  
                    }
                }
            }
            InitialONOFF();
            if(udcRun.C_RUNNING) 
               timer1.Enabled = true;
        }
        private void udcUUTInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            udcOnOff.stopRun();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            refreshRunTimeUI();
            refreshVoltCur();
            UIRefresh.OnEvented(new CUIRefreshArgs(idNo,ref runUUT)); 
        }
        private void refreshRunTimeUI()
        {
            runNowTime = udcOnOff.mCurRunTime;
            TimeSpan ts = new TimeSpan(0, 0, runNowTime);
            TimeSpan tl = new TimeSpan(0, 0, runUUT.wPara.burnTime - runNowTime);
            string runTime = ts.Days.ToString("D2") + ":" + ts.Hours.ToString("D2") + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");
            string leftTime = tl.Days.ToString("D2") + ":" + tl.Hours.ToString("D2") + ":" + tl.Minutes.ToString("D2") + ":" + tl.Seconds.ToString("D2");
            labRunTime.Text = runTime;
            labLeftTime.Text = leftTime; 
        }
        private void refreshVoltCur()
        {
            for (int i = 0; i < runUUT.wLed.Count; i++)
            {
                if (runUUT.wLed[i].serialNo == "")
                {
                    labSnList[i].Text = "----";
                    labSnList[i].ForeColor = Color.Black;
                    labResultList[i].Text = "----";
                    labResultList[i].ForeColor = Color.Black;
                    labVoltList[i].Text = "----";
                    labVoltList[i].ForeColor = Color.Black;
                    labCurList[i].Text = "----";
                    labCurList[i].ForeColor = Color.Black;
                }
                else
                {                    
                    labSnList[i].Text = runUUT.wLed[i].serialNo;
                    labSnList[i].ForeColor = Color.Black;
                    labVoltList[i].Text = runUUT.wLed[i].unitV.ToString("0.000");
                    labVoltList[i].ForeColor = Color.Black;  
                    labCurList[i].Text = runUUT.wLed[i].unitA.ToString("0.00");
                    labCurList[i].ForeColor = Color.Black; 
                    int stepNo = 0;
                    for (int CH = 0; CH < runUUT.wLed[0].vName.Count; CH++)
                    {
                        if (runUUT.wPara.runQCStepNo == runUUT.wLed[0].QCV[CH])
                        {
                            stepNo = CH;
                            break;
                        }
                    }
                    if (runUUT.wLed[i].unitV >= runUUT.wLed[i].vMin[stepNo] &&
                       runUUT.wLed[i].unitV <= runUUT.wLed[i].vMax[stepNo])
                        labVoltList[i].ForeColor = Color.Blue;
                    else
                        labVoltList[i].ForeColor = Color.Red;
                    if (runUUT.wLed[i].unitA >= runUUT.wLed[i].iMin[stepNo] &&
                       runUUT.wLed[i].unitA <= runUUT.wLed[i].iMax[stepNo])
                        labCurList[i].ForeColor = Color.Blue;
                    else
                        labCurList[i].ForeColor = Color.Red;
                    if (runUUT.wLed[i].passResult == 0)
                    {
                        labResultList[i].Text = "PASS";
                        labResultList[i].ForeColor = Color.Blue;
                    }
                    else
                    {
                        labResultList[i].Text = "FAIL";
                        labResultList[i].ForeColor = Color.Red;
                    }
                }
            }
        }
        private void InitialONOFF()
        {
            udcOnOff.mMaxACVolt = 220;
            udcOnOff.OnOffTime.mBITime = ((double)runUUT.wPara.burnTime)/3600;
            udcOnOff.OnOffTime.mOnOffNum =runUUT.wOnOff.C_ONOFF_MAX;
            for (int i = 0; i < udcOnOff.OnOffTime.mOnOffNum; i++)
            {
                udcOnOff.OnOffTime.listACVolt.Add(runUUT.wOnOff.ACV[i]);
                int onoffTimes = (runUUT.wOnOff.OnTime[i] + runUUT.wOnOff.OffTime[i]) * runUUT.wOnOff.OnOffTimes[i];
                udcOnOff.OnOffTime.listOnOffTime.Add(onoffTimes);
                udcOnOff.OnOffTime.listOnTime.Add(runUUT.wOnOff.OnTime[i]);
                udcOnOff.OnOffTime.listOffTime.Add(runUUT.wOnOff.OffTime[i]);
            }
            udcOnOff.initalUI();
            udcOnOff.startRun(runUUT.wPara.runTime);    
        }
        #endregion

        #region 事件定义
        public class CUIRefreshArgs : EventArgs
        {
            public readonly int idNo;
            public udcRun.CUUT runUUT; 
            public CUIRefreshArgs(int idNo,ref udcRun.CUUT runUUT)
            {
                this.idNo = idNo;
                this.runUUT = runUUT;
            }
        }
        public COnEvent<CUIRefreshArgs> UIRefresh = new COnEvent<CUIRefreshArgs>();
        #endregion


    }
}
