using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using GJ;

namespace GJ.Para.Udc.BURNIN
{
    public partial class udcOnOffWave : UserControl
    {
        #region 构造函数
        public udcOnOffWave()
        {
            InitializeComponent();
            this.Paint += new System.Windows.Forms.PaintEventHandler(refreshUI);
        }
        #endregion

        #region 面板回调函数
        private void udcOnOffWave_Load(object sender, EventArgs e)
        {
            startTime = CIniFile.ReadFromIni("OnOff Para", "startTime", Application.StartupPath + "\\onoff.tmr");
            string runTemp = CIniFile.ReadFromIni("OnOff Para", "runingTime", Application.StartupPath + "\\onoff.tmr");
            if (runTemp != "")
                runningTime = System.Convert.ToInt32(runTemp);
            runTemp = CIniFile.ReadFromIni("OnOff Para", "runStatus", Application.StartupPath + "\\onoff.tmr");
            if (runTemp != "")            
                runStatus = System.Convert.ToInt32(runTemp);
            if (runStatus == 0)
                runningTime = 0;
            //运行中
            if (runStatus == 1)
            {
                resetRun = true;
                if (!runWorker.IsBusy)
                    runWorker.RunWorkerAsync();  
            }
        }
        private void udcOnOffWave_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }
        #endregion

        #region 参数类
        public class COnOffTime
        {
            /// <summary>
            /// 老化时间(H)
            /// </summary>
            public double mBITime = 0;
            /// <summary>
            /// OnOff段数
            /// </summary>
            public int mOnOffNum = 0;
            /// <summary>
            /// 输入ACV
            /// </summary>
            public List<int> listACVolt = new List<int>();
            /// <summary>
            /// ONOFF时间(S)
            /// </summary>
            public List<int> listOnOffTime =new List<int>();
            /// <summary>
            /// ON时间(S)
            /// </summary>
            public List<int> listOnTime = new List<int>();
            /// <summary>
            /// OFF时间(S)
            /// </summary>
            public List<int> listOffTime = new List<int>();
        }
        public COnOffTime OnOffTime = new COnOffTime();
        #endregion

        #region 属性
        /// <summary>
        /// 最大输入电压
        /// </summary>
        public int mMaxACVolt
        {
            set { maxACVolt = value; }
        }
        /// <summary>
        /// 当前输入AC电压
        /// </summary>
        public int mCurACVolt
        {
            get { return runCurACVolt; }
        }
        /// <summary>
        /// 当前运行时间
        /// </summary>
        public int mCurRunTime
        {
            set { runningTime = value;}
            get { return runningTime; }
        }
        /// <summary>
        /// 运行标志-0:停止;1:运行;2:暂停 
        /// </summary>
        public int mCurRunStatus
        {
            get { return runStatus; }
        }
        /// <summary>
        /// 清除界面刷新
        /// </summary>
        public bool mClrUI
        {
            set { resetRun = value; }
        }
        #endregion

        #region 字段

        private int maxACVolt = 220;
        private int maxOnOffTime = 3600;

        private const int Xv = 20;
        private const int Yv = 12;
        
        private float curYon = 0;
        private float curYoff = 0;
        private float unitTv = 0;

        private bool runStart = false; 
        private bool runOnOffFlag = true;
        private bool raiseOnOff = true;
        private int raiseACVolt = 0;
        private float curY = 0;
        private float curX = 0;

        private bool runStarting = false;
        private bool runOnOffFlaging = true;
        private bool raiseOnOffing = true;
        private int raiseACVolting = 0;
        private float curXing = 0;
        private float curYing = 0;

        private bool resetRun = false;
        private string startTime = string.Empty;
        private int runACVolting = 0;
        private bool runEnd = false;
        /// <summary>
        /// 运行时间
        /// </summary>
        private int runningTime = 0;
        /// <summary>
        /// 当前AC电压
        /// </summary>
        private int runCurACVolt = 0;
        /// <summary>
        /// 运行标志-0:停止;1:运行;2:暂停 
        /// </summary>
        private int runStatus = 0;

        #endregion

        #region 方法
        public void refreshUI(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.Clear(Color.White);
                //画X轴坐标
                e.Graphics.DrawLine(Pens.DarkGray, new Point(0, this.Height - Yv), new Point(this.Width, this.Height - Yv));
                //画Y轴坐标
                e.Graphics.DrawLine(Pens.DarkGray, new Point(Xv, this.Height), new Point(Xv, 0));
                //写X轴单位
                for (int i = 0; i < 11; i++)
                {
                    float x = Xv + (this.Width - Xv) * i / 10;
                    float y = this.Height - Yv;
                    e.Graphics.DrawLine(Pens.DarkGray, x, y, x, 0);

                    if (OnOffTime.mBITime != 0)                    
                        maxOnOffTime =(int)(OnOffTime.mBITime * 3600);

                    int unitX = maxOnOffTime * i / 600;

                    SizeF size = e.Graphics.MeasureString(unitX.ToString(), new Font("宋体", 10));
                    if(i==0)
                        e.Graphics.DrawString(unitX.ToString(), new Font("宋体", 10), Brushes.Black,
                                              x, this.Height - size.Height + 3);
                    else if(i==10)
                        e.Graphics.DrawString(unitX.ToString(), new Font("宋体", 10), Brushes.Black,
                                              x - size.Width, this.Height - size.Height + 3);
                    else 
                        e.Graphics.DrawString(unitX.ToString(), new Font("宋体", 10), Brushes.Black,
                                              x - size.Width/2, this.Height - size.Height + 3);
                }
                //写Y轴单位
                for (int i = 1; i < 4; i++)
                {
                    float x = Xv;
                    float y = (this.Height - Yv) * i / 4;
                    e.Graphics.DrawLine(Pens.DarkGray, x, y, this.Width, y);
                    int unitY = 0;
                    if (i == 1)
                        unitY = maxACVolt;
                    else if(i==2)
                        unitY=maxACVolt/2;
                    SizeF size = e.Graphics.MeasureString(unitY.ToString(), new Font("宋体", 10));
                    if(i!=3)
                        e.Graphics.DrawString(unitY.ToString(), new Font("宋体", 10), Brushes.Black, -2, y - size.Height / 2);
                    else
                        e.Graphics.DrawString("  0", new Font("宋体", 10), Brushes.Black, -2, y - size.Height / 2);
                }                
                initalUI();
                updateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());  
            }          
        }
        public void initalUI()
        {
            try
            {
                if (OnOffTime.mBITime == 0 || OnOffTime.mOnOffNum == 0)
                    return;
                //防止没设置时间段造成死循环
                int calOnOffTime=0;
                for (int i = 0; i < OnOffTime.mOnOffNum; i++)                
                    calOnOffTime += OnOffTime.listOnOffTime[i];
                if (calOnOffTime == 0)
                    return;
                calOnOffTime = 0;
                for (int i = 0; i < OnOffTime.mOnOffNum; i++)
                {
                    calOnOffTime += OnOffTime.listOnTime[i];
                    calOnOffTime += OnOffTime.listOffTime[i];
                }
                if (calOnOffTime == 0)
                    return;
     
                int totalTime = (int)(OnOffTime.mBITime * 3600);
                float waveWith = this.Width - Xv;
                float waveHeight = this.Height - Yv;
                curX = Xv;
                curYon = (this.Height - Yv) / 4;
                curYoff = (this.Height - Yv) * 3 / 4;
                unitTv = waveWith / totalTime;
                int runTime = totalTime;
                runOnOffFlag = true;
                raiseOnOff = true;
                runStart = false;
                do
                {
                    for (int i = 0; i < OnOffTime.mOnOffNum; i++)
                    {
                        if (runTime >= OnOffTime.listOnOffTime[i])
                        {
                            calOnOffWave(OnOffTime.listACVolt[i],OnOffTime.listOnOffTime[i], OnOffTime.listOnTime[i], OnOffTime.listOffTime[i]);
                            runTime -= OnOffTime.listOnOffTime[i];
                        }
                        else
                        {
                            calOnOffWave(OnOffTime.listACVolt[i],runTime, OnOffTime.listOnTime[i], OnOffTime.listOffTime[i]);
                            runTime = 0;
                        }
                        if (runTime < 0)
                            runTime = 0;
                    }

                } while (runTime > 0);
            }
            catch (Exception)
            {
                
                throw;
            }            
        }
        private void calOnOffWave(int inputAC,int runTime, int runOnTime, int runOffTime)
        {
            try
            {
                int runInputAC = 0;
                int runWaveTime = 0;
                while (runTime > 0)
                {
                    if (runOnTime == 0 && runOffTime == 0) //该段无效
                        return;
                    else if (runOnTime == 0)   //全Off时间
                    {
                        runOnOffFlag = false;
                        runWaveTime = runTime;
                        runInputAC = 0;
                    }
                    else if (runOffTime == 0) //全On时间
                    {
                        runOnOffFlag = true;
                        runWaveTime = runTime;
                        runInputAC = inputAC;
                    }
                    else
                    {
                        if (runOnOffFlag)  //ON
                        {
                            if (runTime > runOnTime)
                                runWaveTime = runOnTime;
                            else
                                runWaveTime = runTime;
                            runInputAC = inputAC;
                        }
                        else                //OFF
                        {
                            if (runTime > runOffTime)
                                runWaveTime = runOffTime;
                            else
                                runWaveTime = runTime;
                            runInputAC = 0;
                        }                        
                    }
                    runTime -= runWaveTime;
                    if (runTime < 0)
                        runTime = 0;
                    drawOnOff(runOnOffFlag, runInputAC,runWaveTime);
                    runOnOffFlag = !runOnOffFlag;
                }
            }
            catch (Exception)
            {                
                throw;
            }           
        }
        private void drawOnOff(bool wOnOff,int inputAC, float waveTime)
        {
            try
            {
                Graphics g = Graphics.FromHwnd(this.Handle);

                if (runStart && raiseOnOff != wOnOff)
                {
                    if (inputAC == 0)
                    {
                        g.DrawLine(Pens.Red, curX, curY, curX, curYoff);
                    }
                    else
                    {
                        if (inputAC == raiseACVolt)
                            g.DrawLine(Pens.Red, curX, curY, curX, curY);
                        else
                        {
                            float y_v = curYoff-inputAC * (curYoff - curYon) / maxACVolt;
                            g.DrawLine(Pens.Red, curX, curY, curX, y_v);
                        }                           
                    }                    
                }                    
                float x_T = unitTv * waveTime;
                if (wOnOff)
                {
                    float y_v = (maxACVolt - inputAC) * (curYoff - curYon) / maxACVolt + curYon;
                    g.DrawLine(Pens.Red, curX, y_v, curX + x_T, y_v);
                    raiseACVolt = inputAC;
                    curY = y_v;
                }
                else
                {
                    g.DrawLine(Pens.Red, curX, curYoff, curX + x_T, curYoff);
                    raiseACVolt = 0;
                    curY = curYoff;
                }                   
                curX += x_T;
                raiseOnOff = wOnOff;
                runStart = true;
            }
            catch (Exception)
            {
                throw;
            }           
        }        
        public void updateUI()
        {
            try
            {
                writeLock.AcquireWriterLock(-1);
                if (runningTime == 0 || OnOffTime.mBITime == 0 || OnOffTime.mOnOffNum == 0)
                    return;
                //防止没设置时间段造成死循环
                int calOnOffTime = 0;
                for (int i = 0; i < OnOffTime.mOnOffNum; i++)
                    calOnOffTime += OnOffTime.listOnOffTime[i];
                if (calOnOffTime == 0)
                    return;
                calOnOffTime = 0;
                for (int i = 0; i < OnOffTime.mOnOffNum; i++)
                {
                    calOnOffTime += OnOffTime.listOnTime[i];
                    calOnOffTime += OnOffTime.listOffTime[i];
                }
                if (calOnOffTime == 0)
                    return;

                if (runningTime > (OnOffTime.mBITime * 3600))
                    runningTime = (int)(OnOffTime.mBITime * 3600);
                int runTime = runningTime;
                curXing = Xv;
                runOnOffFlaging = true;
                raiseOnOffing = true;
                runStarting = false;
                do
                {
                    for (int i = 0; i < OnOffTime.mOnOffNum; i++)
                    {
                        if (runTime >= OnOffTime.listOnOffTime[i])
                        {
                            calOnOffWaveing(OnOffTime.listACVolt[i], OnOffTime.listOnOffTime[i], OnOffTime.listOnTime[i], OnOffTime.listOffTime[i]);
                            runTime -= OnOffTime.listOnOffTime[i];
                        }
                        else
                        {
                            calOnOffWaveing(OnOffTime.listACVolt[i], runTime, OnOffTime.listOnTime[i], OnOffTime.listOffTime[i]);
                            runTime = 0;
                        }
                        if (runTime < 0)
                            runTime = 0;
                    }

                } while (runTime > 0);

                /****************检测ON/OFF状态***************/
                if (resetRun || runCurACVolt != runACVolting)
                {
                    resetRun = false;
                    if (runningTime >= (int)(OnOffTime.mBITime * 3600))
                    {
                        runEnd = true;
                        OnChangeACVolted(new COnOffArgs(runACVolting, runningTime, runEnd));
                    }
                    else
                    {
                        if(runWorker.IsBusy)    //运行中
                           OnChangeACVolted(new COnOffArgs(runACVolting, runningTime));
                    }
                       
                }
                else
                {
                    if (!runEnd && (runningTime >= (int)(OnOffTime.mBITime * 3600)))
                    {
                        runEnd = true;
                        OnChangeACVolted(new COnOffArgs(runACVolting, runningTime, runEnd)); 
                    }
                }
                runCurACVolt = runACVolting;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                writeLock.ReleaseWriterLock();
            }
   
        }
        private void calOnOffWaveing(int inputAC,int runTime, int runOnTime, int runOffTime)
        {
            try
            {
                int runWaveTime = 0;
                int runInputAC = 0;
                while (runTime > 0)
                {
                    if (runOnTime == 0 && runOffTime == 0) //该段无效
                        return;
                    else if (runOnTime == 0)   //全Off时间
                    {
                        runOnOffFlaging = false;
                        runWaveTime = runTime;
                        runInputAC = 0;
                    }
                    else if (runOffTime == 0) //全On时间
                    {
                        runOnOffFlaging = true;
                        runWaveTime = runTime;
                        runInputAC = inputAC;
                    }
                    else
                    {
                        if (runOnOffFlaging)  //ON
                        {
                            if (runTime > runOnTime)
                                runWaveTime = runOnTime;
                            else
                                runWaveTime = runTime;
                            runInputAC = inputAC;
                            runACVolting = inputAC;
                        }
                        else                //OFF
                        {
                            if (runTime > runOffTime)
                                runWaveTime = runOffTime;
                            else
                                runWaveTime = runTime;
                            runInputAC = 0;
                            runACVolting = 0;
                        }
                    }
                    runTime -= runWaveTime;
                    if (runTime < 0)
                        runTime = 0;
                    drawOnOffing(runOnOffFlaging, runInputAC ,runWaveTime);
                    runOnOffFlaging = !runOnOffFlaging;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void drawOnOffing(bool wOnOff,int inputAC, float waveTime)
        {
            try
            {
              
                Graphics g = Graphics.FromHwnd(this.Handle);

                if (runStarting && raiseOnOffing != wOnOff)
                {
                    if (inputAC == 0)
                    {
                        g.DrawLine(Pens.LightGreen, curXing, curYing, curXing, curYoff);
                    }
                    else
                    {
                        if (inputAC == raiseACVolting)
                            g.DrawLine(Pens.LightGreen, curXing, curYing, curXing, curYing);
                        else
                        {
                            float y_v = curYoff - inputAC * (curYoff - curYon) / maxACVolt;
                            g.DrawLine(Pens.LightGreen, curXing, curYing, curXing, y_v);
                        }
                    }
                }
                float x_T = unitTv * waveTime;
                if (wOnOff)
                {
                    float y_v = (maxACVolt - inputAC) * (curYoff - curYon) / maxACVolt + curYon;
                    g.DrawLine(Pens.Green, curXing, y_v, curXing + x_T, y_v);
                    raiseACVolting = inputAC;
                    curYing = y_v;
                }
                else
                {
                    g.DrawLine(Pens.Green, curXing, curYoff, curXing + x_T, curYoff);
                    raiseACVolting = 0;
                    curYing = curYoff;
                }
                curXing += x_T;
                raiseOnOffing = wOnOff;
                runStarting = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 启动计时
        /// </summary>
        /// <param name="runTime"></param>
        /// <param name="continueRun"></param>
        public void startRun(int runTime=0)
        {
            try
            {
                writeLock.AcquireWriterLock(-1);                
                runningTime = runTime;
                resetRun = true;
                runEnd = false;
                runStatus = 1;
                startTime = DateTime.Now.ToString();
                if (!runWorker.IsBusy)
                    runWorker.RunWorkerAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                writeLock.ReleaseWriterLock();
            }           
        }
        /// <summary>
        /// 继续计时
        /// </summary>
        public void continuRun()
        {
            try
            {
                startTime = DateTime.Now.ToString();
                runEnd = false;
                resetRun = true;
                runStatus = 1;
                if (!runWorker.IsBusy)
                    runWorker.RunWorkerAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            { 
            
            }
        }
        /// <summary>
        /// 停止计时
        /// </summary>
        public void stopRun()
        {
            try
            {
                writeLock.AcquireWriterLock(-1);
                runStatus = 2;
                if (runWorker.IsBusy)
                    runWorker.CancelAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                writeLock.ReleaseWriterLock();
            }
        }
        #endregion

        #region 事件
        public class COnOffArgs:EventArgs
        {
          public COnOffArgs(int curACVolt,int curRunTime,bool endBI=false)
          {
            this.curACVolt=curACVolt;
            this.curRunTime=curRunTime;
            this.endBI = endBI;
          }
          public readonly int curACVolt;
          public readonly int curRunTime;
          public readonly bool endBI;
        }
        public delegate void OnChangeACVoltHandler(object sender, COnOffArgs e);
        public event OnChangeACVoltHandler OnChangeACVolt;
        private void OnChangeACVolted(COnOffArgs e)
        {
            if (OnChangeACVolt != null)
                OnChangeACVolt(this, e); 
        }
        #endregion

        #region 委托
        private delegate void refrehUIHandler(bool reset);
        private void OnRefreshUI(bool reset = false)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new refrehUIHandler(OnRefreshUI), reset);
                else
                    if (!reset)
                        updateUI();
                    else
                        this.Refresh(); 
            }
            catch (Exception)
            {
                throw;
            }
         }
        #endregion

        #region 线程
        private ReaderWriterLock writeLock = new ReaderWriterLock();     
        private void runWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    if (runWorker.CancellationPending)
                        return;            
                    if (CMath.DifTime(startTime, DateTime.Now.ToString()) >= 1)
                    {
                        writeLock.AcquireWriterLock(-1);
                        runningTime += CMath.DifTime(startTime, DateTime.Now.ToString());
                        if (runningTime >= (int)(OnOffTime.mBITime * 3600))
                            runningTime = (int)(OnOffTime.mBITime * 3600);
                        startTime = DateTime.Now.ToString();
                        writeLock.ReleaseWriterLock();
                        OnRefreshUI(resetRun);
                        if (runningTime == (int)(OnOffTime.mBITime * 3600))
                        {
                            runStatus = 0;
                            break;
                        }                           
                    }           
                    Thread.Sleep(100);
                }
            }
            catch (Exception)
            {
                
            }
            finally
            {
            }
        }
        #endregion
    }
}
