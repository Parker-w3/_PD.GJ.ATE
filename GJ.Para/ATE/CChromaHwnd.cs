using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;
using GJ.PDB;
using System.Diagnostics;
using System.Windows.Forms;
using HalconDotNet;
using System.Drawing;
namespace GJ.Para.ATE
{
    public class CChromaHwnd
    {
        #region 导入动态库

        private const int GW_CHILD = 5;
        private const int GW_HWNDNEXT = 2;
        private const int GW_HWNDFIRST = 0;
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        private const int MEM_COMMIT = 0x1000;
        private const int MEM_DECOMMIT = 0x4000;
        private const int PAGE_READWRITE = 0x4;
        private const int WM_USER = 0x400;
        private const int TB_GETBUTTON = WM_USER + 23;
        private const int WM_SETTEXT = 0xC;
        private const int WM_COMMAND = 0x111;
        private const int BM_CLICK = 0xF5;
        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public const int HFILE_ERROR = -1;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_F2 = 0x71;
        public const int WM_F10 = 0x79;

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetWindow")]
        private extern static int GetWindow(int hWnd, int wCmd);
        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        private static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize);
        [DllImport("user32.dll", EntryPoint = "GetWindowTextLength", CharSet = CharSet.Auto)]
        private extern static int GetWindowTextLength(int hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(int hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowThreadProcessId(int hwnd, out int ID);

        [DllImport("kernel32.dll")]
        private static extern int OpenProcess(int dwDesiredAccess, int bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(int handle);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]//为指定的进程分配内存地址:成功则返回分配内存的首地址
        private static extern int VirtualAllocEx(int hProcess, int lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]//在其它进程中释放申请的虚拟内存空间
        private static extern bool VirtualFreeEx(
                                    int hProcess,//目标进程的句柄,该句柄必须拥有PROCESS_VM_OPERATION的权限
                                    int lpAddress,//指向要释放的虚拟内存空间首地址的指针
                                    uint dwSize,
                                    uint dwFreeType//释放类型
        );
        [DllImport("kernel32.dll")]
        public static extern int _lopen(string lpPathName, int iReadWrite);
        #endregion

        #region 构造函数
        public CChromaHwnd(CBase wBase, int language = 0)
        {
            this.language = language;
            ateRun.wBase = wBase;
        }
        #endregion

        #region 参数类
        public class CBase
        {
            public int C_MAX_CHAN;
            public bool preSnInput;
            public string ateProgTitle;
            public string ateResultFolder;
            public string ateExtSnBar;
            public CBase(bool preSnInput = true,
                          string ateProgTitle = "Chroma SMPS ATS IDE - [Execution Control]",
                          string ateResultFolder = @"C:\Program Files\Chroma\SMPS ATS\Log",
                          string ateExtSnBar = "BarCode Reader", int C_MAX_CHAN = 8)
            {
                this.C_MAX_CHAN = C_MAX_CHAN;
                this.preSnInput = preSnInput;
                this.ateProgTitle = ateProgTitle;
                this.ateResultFolder = ateResultFolder;
                this.ateExtSnBar = ateExtSnBar;
            }
        }
        public class CPara
        {
            public string programName = string.Empty;
            public string modelName = string.Empty;
            public string elapsedTime = string.Empty;
            /// <summary>
            /// 程序运行状态
            /// </summary>
            public int doRun = 0;
            /// <summary>
            /// 程序停止状态
            /// </summary>
            public int doFinish = 0;
        }
        public class CHwnd
        {
            /// <summary>
            /// 主程序
            /// </summary>
            public int mainProg;

            

            /// <summary>
            /// 外部条码输入框
            /// </summary>
            public int extSnBar;
            public List<int> extSnText = new List<int>();
            public int extSnOK = 0;

            /// <summary>
            /// 预扫描条码框
            /// </summary>
            public bool bSnTextBegin = false;
            public bool bSnTextEnd = false;
            public List<int> snText = new List<int>();

            /// <summary>
            /// 条码确认按钮
            /// </summary>
            public bool bSnBtnOK = false;
            public int snBtnOK = 0;

            /// <summary>
            /// 主工具栏
            /// </summary>
            public bool bMainToolBar = false;
            public int mainToolBar = 0;

            /// <summary>
            /// 测试信息
            /// </summary>
            public bool bInfoStep = false;
            public int InfoStep = 0;

            /// <summary>
            /// 测试时间
            /// </summary>
            public bool bTestTimes = false;
            public int testTimesStep = 0;

            public bool bDoFinishStart = false;
            public bool bDoFinish = false;

            /// <summary>
            /// 结果图片框
            /// </summary>
            public bool bImgResult = false;
            public int imgResult = 0;
        }
        public class CResult
        {
            public List<int> snTestTimes = new List<int>();
            public List<string> serialNos = new List<string>();
            public List<int> result = new List<int>();
        }
        public class CRun
        {
            public CBase wBase = new CBase();
            public CPara wPara = new CPara();
            public CHwnd wHwnd = new CHwnd();
            public CResult wResult = new CResult();
        }

        #endregion

        #region 字段
        public CRun ateRun = new CRun();
        private int language = 0;
        #endregion

        #region 语言类型定义
        private string[] c_ExpandArray = new string[] { "Expand Array-type variables", "展开数组变量" };
        private string[] c_Btn_OK = new string[] { "Ok", "确定" };
        private string[] c_repeatTimes = new string[] { "Iterations", "重复次数" };
        private string[] c_ModelName = new string[] { "Model Name", "型号名称" };
        private string[] c_TestTimes = new string[] { "Elapsed Time", "测试时间" };
        #endregion

        #region 属性
        public int mLanguage
        {
            set { language = value; }
            get { return language; }
        }

        public string[] m_Btn_OK
        {
            set 
            {
                c_Btn_OK = value;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 获取Chroma ATE程序句柄
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetATEHanlder(ref string er)
        {
            try
            {
                er = string.Empty;

                //初始化句柄参数

                ateRun.wHwnd.bSnTextBegin = false;
                ateRun.wHwnd.bSnTextEnd = false;
                ateRun.wHwnd.snText.Clear();

                ateRun.wHwnd.bSnBtnOK = false;
                ateRun.wHwnd.snBtnOK = 0;

                ateRun.wHwnd.bMainToolBar = false;
                ateRun.wHwnd.mainToolBar = 0;

                ateRun.wHwnd.bInfoStep = false;
                ateRun.wHwnd.InfoStep = 0;

                ateRun.wHwnd.bTestTimes = false;
                ateRun.wHwnd.testTimesStep = 0;

                ateRun.wHwnd.bDoFinishStart = false;
                ateRun.wHwnd.bDoFinish = false;

                ateRun.wHwnd.mainProg = FindWindow(null, ateRun.wBase.ateProgTitle);

                if (ateRun.wHwnd.mainProg == 0)
                {
                    er = "检测不到程序标题名称:" + ateRun.wBase.ateProgTitle;
                    return false;
                }

                FindChild(ateRun.wHwnd.mainProg);

              //  ateRun.wPara.doRun = ReadTBButtionEnable(ateRun.wHwnd.mainToolBar, 0);

                if (!ateRun.wBase.preSnInput)
                {
                    GetExtSnBar(ref er);
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return true;
            }
        }
        /// <summary>
        /// 找父窗体所有子窗体句柄
        /// </summary>
        /// <param name="ptrhParent"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private void FindChild(int ptrhParent)
        {
            try
            {
                int hWndChild;

                StringBuilder szClass = new StringBuilder(255);

                StringBuilder szCaption = new StringBuilder(255);

                int szLen = 0;

                hWndChild = GetWindow(ptrhParent, GW_CHILD);

                if (hWndChild == 0)
                    return;

                hWndChild = GetWindow(hWndChild, GW_HWNDFIRST);

                if (hWndChild == 0)
                    return;

                while (hWndChild != 0)
                {
                    //控件类型及名称
                    GetClassName(hWndChild, szClass, szClass.Capacity - 1);
                    szLen = GetWindowTextLength(hWndChild);
                    GetWindowText(hWndChild, szCaption, szCaption.Capacity - 1);

                    System.Console.WriteLine(hWndChild.ToString() + "-->" + szClass.ToString() + ":" + szCaption.ToString());

                    FindSnTextHwnd(hWndChild, szClass.ToString(), szCaption.ToString());

                    FindSnBtnHwnd(hWndChild, szClass.ToString(), szCaption.ToString());

                    FindResultImgHwnd(hWndChild, szClass.ToString(), szCaption.ToString());

                    FindMainToolBar(hWndChild, szClass.ToString(), szCaption.ToString());

                    getDoFinish(szClass.ToString(), szCaption.ToString());

                    getModelInfo(szClass.ToString(), szCaption.ToString());

                    getTestTimes(szClass.ToString(), szCaption.ToString());

                    FindChild(hWndChild);

                    hWndChild = GetWindow(hWndChild, GW_HWNDNEXT);

                }
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 找预扫描文本条码框
        /// </summary>
        private void FindSnTextHwnd(int hWndChild, string szClass, string szCaption)
        {
            if (!ateRun.wHwnd.bSnTextBegin && szClass == "Button" && szCaption == c_ExpandArray[language])
            {
                ateRun.wHwnd.bSnTextBegin = true;
                return;
            }
            if (!ateRun.wHwnd.bSnTextBegin)
                return;
            if (ateRun.wHwnd.snText.Count < ateRun.wBase.C_MAX_CHAN)
            {
                if (szClass == "Edit")
                    ateRun.wHwnd.snText.Add(hWndChild);
                if (ateRun.wHwnd.snText.Count == ateRun.wBase.C_MAX_CHAN)
                    ateRun.wHwnd.bSnTextEnd = true;
            }
        }
        /// <summary>
        /// 条码确定按钮
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="szClass"></param>
        /// <param name="szCaption"></param>
        private void FindSnBtnHwnd(int hWndChild, string szClass, string szCaption)
        {
            if (ateRun.wHwnd.bSnBtnOK)
                return;
            if (szClass == "Button" && szCaption == c_Btn_OK[language])
            {
                ateRun.wHwnd.bSnBtnOK = true;
                ateRun.wHwnd.snBtnOK = hWndChild;
            }
        }
        /// <summary>
        /// 主菜单工具栏句柄
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="szClass"></param>
        /// <param name="szCaption"></param>
        private void FindMainToolBar(int hWndChild, string szClass, string szCaption)
        {
            if (ateRun.wHwnd.bMainToolBar)
                return;
            if (szClass == "ToolbarWindow32")
            {
                ateRun.wHwnd.bMainToolBar = true;
                ateRun.wHwnd.mainToolBar = hWndChild;
            }
        }
        /// <summary>
        /// 获取停止状态
        /// </summary>
        /// <param name="szClass"></param>
        /// <param name="szCaption"></param>
        private void getDoFinish(string szClass, string szCaption)
        {
            if (ateRun.wHwnd.bDoFinish)
                return;
            if (szClass == "Static" && szCaption == c_repeatTimes[language])
            {
                ateRun.wHwnd.bDoFinishStart = true;
                return;
            }
            if (ateRun.wHwnd.bDoFinishStart)
            {
                ateRun.wHwnd.bDoFinish = true;
                if (szClass == "Static" && szCaption == "0/1")
                    ateRun.wPara.doFinish = 0;
                else
                    ateRun.wPara.doFinish = 1;
            }
        }
        /// <summary>
        /// 找测试结果图片句柄
        /// </summary>
        private void FindResultImgHwnd(int hWndChild, string szClass, string szCaption)
        {
            if (!ateRun.wHwnd.bImgResult && szClass == "AfxFrameOrView42")
            {
                ateRun.wHwnd.bImgResult = true;
                ateRun.wHwnd.imgResult = hWndChild;
                return;
            }
        }
        /// <summary>
        /// 获取程序名和机种名
        /// </summary>
        /// <param name="szClass"></param>
        /// <param name="szCaption"></param>
        private void getModelInfo(string szClass, string szCaption)
        {
            if (!ateRun.wHwnd.bInfoStep && szClass == "Static" && szCaption == c_ModelName[language])
            {
                ateRun.wHwnd.bInfoStep = true;
                ateRun.wHwnd.InfoStep = 0;
                return;
            }
            if (!ateRun.wHwnd.bInfoStep)
                return;
            if (szClass == "Static")
            {
                if (ateRun.wHwnd.InfoStep < 2)
                {
                    ateRun.wHwnd.InfoStep++;
                    if (ateRun.wHwnd.InfoStep == 1)
                        ateRun.wPara.programName = szCaption;
                    if (ateRun.wHwnd.InfoStep == 2)
                        ateRun.wPara.modelName = szCaption;
                }
            }
        }
        /// <summary>
        /// 获取测试时间
        /// </summary>
        /// <param name="szClass"></param>
        /// <param name="szCaption"></param>
        private void getTestTimes(string szClass, string szCaption)
        {
            if (!ateRun.wHwnd.bTestTimes && szClass == "Static" && szCaption == c_TestTimes[language])
            {
                ateRun.wHwnd.bTestTimes = true;
                ateRun.wHwnd.testTimesStep = 0;
                return;
            }
            if (!ateRun.wHwnd.bTestTimes)
                return;
            if (szClass == "Static")
            {
                if (ateRun.wHwnd.testTimesStep < 1)
                {
                    ateRun.wHwnd.testTimesStep++;
                    if (ateRun.wHwnd.testTimesStep == 1)
                        ateRun.wPara.elapsedTime = szCaption;
                }
            }
        }

        /// <summary>
        /// 获取外部条码输入框
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetExtSnBar(ref string er)
        {
            try
            {
                //初始化
                ateRun.wHwnd.extSnOK = 0;
                ateRun.wHwnd.extSnText.Clear();

                ateRun.wHwnd.extSnBar = FindWindow(null, ateRun.wBase.ateExtSnBar);
                if (ateRun.wHwnd.extSnBar == 0)
                {
                    er = "检查不到扫描条码窗体:" + ateRun.wBase.ateExtSnBar;
                    return false;
                }

                FindExtSnBarChild(ateRun.wHwnd.extSnBar);

                if (ateRun.wHwnd.extSnText.Count != ateRun.wBase.C_MAX_CHAN)
                {
                    er = "获取不到条码条码框[" + ateRun.wHwnd.extSnText.Count.ToString() + "],请确认ATE程序已运行?";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 找父窗体所有子窗体句柄
        /// </summary>
        /// <param name="ptrhParent"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private void FindExtSnBarChild(int ptrhParent)
        {
            try
            {
                int hWndChild = 0;

                StringBuilder szClass = new StringBuilder(255);

                StringBuilder szCaption = new StringBuilder(255);

                int szLen = 0;

                hWndChild = GetWindow(ptrhParent, GW_CHILD);

                if (hWndChild == 0)
                    return;

                hWndChild = GetWindow(hWndChild, GW_HWNDFIRST);

                while (hWndChild != 0)
                {
                    //控件类型及名称
                    GetClassName(hWndChild, szClass, szClass.Capacity - 1);
                    szLen = GetWindowTextLength(hWndChild);
                    GetWindowText(hWndChild, szCaption, szCaption.Capacity - 1);

                    FindExtSnTextHwnd(hWndChild, szClass.ToString(), szCaption.ToString());

                    FindExtSnBarChild(hWndChild);

                    hWndChild = GetWindow(hWndChild, GW_HWNDNEXT);

                }
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 找预扫描文本条码框
        /// </summary>
        private void FindExtSnTextHwnd(int hWndChild, string szClass, string szCaption)
        {
            if (ateRun.wHwnd.snText.Count < ateRun.wBase.C_MAX_CHAN)
            {
                if (szClass == "Edit")
                    ateRun.wHwnd.extSnText.Add(hWndChild);
            }
            if (szCaption == c_Btn_OK[language])
                ateRun.wHwnd.extSnOK = hWndChild;
        }

        /// <summary>
        /// 获取弹出消息对话框
        /// </summary>
        /// <returns></returns>
        public void ConfirmMsgBoxYes(List<string> info, List<string> yesCaption)
        {
            for (int i = 0; i < info.Count; i++)
            {
                int messageHwnd = FindWindow(null, info[i]);

                if (messageHwnd == 0)
                    continue;

                int ptrYes = 0;

                FindConfirmMsgBoxChlid(messageHwnd, ref ptrYes, yesCaption[i]);

                if (ptrYes == 0)
                    continue;

                SendMessage(ptrYes, BM_CLICK, 0, 0);
            }
        }
        private void FindConfirmMsgBoxChlid(int ptrhParent, ref int ptrYes, string yesCaption)
        {
            int hWndChild = 0;

            StringBuilder szClass = new StringBuilder(255);

            StringBuilder szCaption = new StringBuilder(255);

            int szLen = 0;

            hWndChild = GetWindow(ptrhParent, GW_CHILD);

            if (hWndChild == 0)
                return;

            hWndChild = GetWindow(hWndChild, GW_HWNDFIRST);

            while (hWndChild != 0)
            {
                //控件类型及名称
                GetClassName(hWndChild, szClass, szClass.Capacity - 1);
                szLen = GetWindowTextLength(hWndChild);
                GetWindowText(hWndChild, szCaption, szCaption.Capacity - 1);

                if (szClass.ToString() == "Button" && szCaption.ToString() == yesCaption)
                    ptrYes = hWndChild;

                FindConfirmMsgBoxChlid(hWndChild, ref ptrYes, yesCaption);

                hWndChild = GetWindow(hWndChild, GW_HWNDNEXT);

            }
        }

        /// <summary>
        /// 读取工具栏按钮状态
        /// </summary>
        /// <param name="hTBWnd">
        ///TBBUTTON ------------utBtn
        ///iBitmap As Long       -4
        ///idCommand As Long     -4
        ///fsState As Byte       -1
        ///fsStyle As Byte       -1
        ///bReserved(1) As Byte  -2
        ///dwData As Long        -4
        ///iString As Long       -4
        /// </param>
        /// <param name="lIndex"></param>
        /// <param name="fEnabled"></param>
        /// <param name="fIsIndex"></param>
        /// <returns></returns>
        private int ReadTBButtionEnable(int hTBWnd, int lIndex, bool fEnabled = true, bool fIsIndex = true)
        {
            try
            {
                byte[] utBtn = new byte[19];
                int lpRemote_utBtn = 0;
                int lProcessId = 0;
                int hProcess = 0;
                int lpNumberOfBytesRead = 0;
                int BtnEnable = 0;
                GetWindowThreadProcessId(hTBWnd, out lProcessId);
                hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, lProcessId);
                if (hProcess != 0)
                {
                    lpRemote_utBtn = VirtualAllocEx(hProcess, 0, (uint)utBtn.Length, MEM_COMMIT, PAGE_READWRITE);
                    SendMessage(hTBWnd, TB_GETBUTTON, lIndex, lpRemote_utBtn);
                    ReadProcessMemory(hProcess, lpRemote_utBtn, utBtn, utBtn.Length, lpNumberOfBytesRead);
                    if (utBtn[8] == 4)
                        BtnEnable = 0; //Disable
                    else
                        BtnEnable = 1; //Enable
                    VirtualFreeEx(hProcess, lpRemote_utBtn, (uint)0, (uint)MEM_DECOMMIT);
                }
                CloseHandle(hProcess);
                return BtnEnable;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 设置工具栏按钮状态
        /// </summary>
        /// <param name="hTBWnd"></param>
        /// <param name="lIndex"></param>
        /// <param name="fEnabled"></param>
        /// <param name="fIsIndex"></param>
        /// <returns></returns>
        private void EnableTBButtion(int hTBWnd, int lIndex, bool fEnabled = true, bool fIsIndex = true)
        {
            byte[] utBtn = new byte[19];
            int lpRemote_utBtn = 0;
            int lProcessId = 0;
            int hProcess = 0;
            int lpNumberOfBytesRead = 0;
            int lButtonID = 0;
            GetWindowThreadProcessId(hTBWnd, out lProcessId);
            if (!fIsIndex)
                lButtonID = lIndex;
            else
            {
                hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, lProcessId);
                if (hProcess != 0)
                {
                    lpRemote_utBtn = VirtualAllocEx(hProcess, 0, (uint)utBtn.Length, MEM_COMMIT, PAGE_READWRITE);
                    SendMessage(hTBWnd, TB_GETBUTTON, lIndex, lpRemote_utBtn);
                    ReadProcessMemory(hProcess, lpRemote_utBtn, utBtn, utBtn.Length, lpNumberOfBytesRead);
                    VirtualFreeEx(hProcess, lpRemote_utBtn, (uint)0, (uint)MEM_DECOMMIT);
                    lButtonID = utBtn[4];
                }
                CloseHandle(hProcess);
            }
            SendMessage(hTBWnd, WM_COMMAND, lButtonID, MAKELONG(System.Convert.ToInt32(fEnabled), 0));
        }
        private int MAKELONG(int a, int b)
        {
            return (int)((b & 0xFFFF) * 0x10000 + (a & 0xFFFF));
        }

        /// <summary>
        /// 传产品条码到条码框中
        /// </summary>
        /// <param name="serialNos"></param>
        /// <returns></returns>
        public bool SendSnToSnText(List<string> serialNos, ref string er, bool preSn = true)
        {
            try
            {
                er = string.Empty;

                if (preSn)
                {
                    if (serialNos.Count != ateRun.wHwnd.snText.Count)
                    {
                        er = "传入条码数量=" + serialNos.Count.ToString() +
                             "与获取ATE条码框数量=" + ateRun.wHwnd.snText.Count.ToString() + "不一致";
                        return false;
                    }
                    if (!setSnStartRun(serialNos, ref er))
                        return false;
                    for (int i = 0; i < serialNos.Count; i++)
                    {
                        StringBuilder snBuider = new StringBuilder(serialNos[i]);
                        SendMessage(ateRun.wHwnd.snText[i], WM_SETTEXT, 0, snBuider);
                    }
                    System.Windows.Forms.Application.DoEvents();
                    SendMessage(ateRun.wHwnd.snBtnOK, BM_CLICK, 0, 0);
                }
                else
                {
                    if (serialNos.Count != ateRun.wHwnd.extSnText.Count)
                    {
                        er = "传入条码数量=" + serialNos.Count.ToString() +
                             "与获取ATE条码框数量=" + ateRun.wHwnd.extSnText.Count.ToString() + "不一致";
                        return false;
                    }
                    if (!setSnStartRun(serialNos, ref er))
                        return false;
                    for (int i = 0; i < serialNos.Count; i++)
                    {
                        StringBuilder snBuider = new StringBuilder(serialNos[i]);
                        SendMessage(ateRun.wHwnd.extSnText[i], WM_SETTEXT, 0, snBuider);
                    }
                    SendMessage(ateRun.wHwnd.extSnOK, BM_CLICK, 0, 0);
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }

        }

        /// <summary>
        /// 设置启动或停止
        /// </summary>
        public void SetDoRun(bool run = true)
        {
            if (ateRun.wHwnd.mainProg == 0)
                return;
            if (run)  //F10            
                PostMessage(ateRun.wHwnd.mainProg, WM_SYSKEYDOWN, WM_F10, 0);
            else      //F2
                PostMessage(ateRun.wHwnd.mainProg, WM_SYSKEYDOWN, WM_F2, 0);
        }

        #endregion

        #region 测试结果
        /// <summary>
        /// 启动条码测试
        /// </summary>
        private bool setSnStartRun(List<string> serialNos, ref string er)
        {
            try
            {
                er = string.Empty;
                ateRun.wResult.serialNos.Clear();
                ateRun.wResult.snTestTimes.Clear();
                ateRun.wResult.result.Clear();
                for (int i = 0; i < serialNos.Count; i++)
                {
                    ateRun.wResult.serialNos.Add(serialNos[i]);
                    ateRun.wResult.snTestTimes.Add(-1);
                    ateRun.wResult.result.Add(-1);
                }
                if (!Directory.Exists(ateRun.wBase.ateResultFolder))
                {
                    er = "找不到测试数据文件夹:" + ateRun.wBase.ateResultFolder;
                    return false;
                }
                if (ateRun.wPara.programName == string.Empty)
                {
                    er = "获取不到Chroma程序名称";
                    return false;
                }
                //第1次调用ＡＴＥ程序不会产生以下２个文件
                string dbNameFolder = ateRun.wBase.ateResultFolder + "\\" + ateRun.wPara.programName.Replace(".prg", "");
                string dbName = dbNameFolder + "\\TESTINFO.MDB";
                if (!Directory.Exists(dbNameFolder))
                    return true;
                if (!File.Exists(dbName))
                    return true;
                //判断文件是否被占用？
                int vHandle = _lopen(dbName, OF_READWRITE | OF_SHARE_DENY_NONE);
                if (vHandle != HFILE_ERROR)
                    CloseHandle(vHandle);
                else
                {
                    CloseHandle(vHandle);
                    er = "[" + Path.GetFileName(dbName) + "]数据库被占用";
                    return false;
                }
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", dbName, "", "");
                string sqlCmd = string.Empty;
                DataSet ds = null;
                for (int i = 0; i < serialNos.Count; i++)
                {
                    if (serialNos[i] != string.Empty)
                    {
                        ds = new DataSet();
                        sqlCmd = "select * from UUTReTest where SerialNo='" + serialNos[i] + "' order by Times desc";
                        if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                            return false;
                        if (ds.Tables[0].Rows.Count == 0)
                            continue;
                        ateRun.wResult.snTestTimes[i] = System.Convert.ToInt32(ds.Tables[0].Rows[0]["Times"].ToString());
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 获取测试条码结果
        /// </summary>
        /// <param name="resultIsExist"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool getSnResultFromDB(ref bool resultIsExist, ref string er)
        {
            try
            {
                resultIsExist = false;

                er = string.Empty;

                if (ateRun.wResult.serialNos.Count != ateRun.wBase.C_MAX_CHAN)
                {
                    er = "获取产品条码数量错误=" + ateRun.wResult.serialNos.Count.ToString();
                    return false;
                }
                if (!Directory.Exists(ateRun.wBase.ateResultFolder))
                {
                    er = "找不到测试数据文件夹:" + ateRun.wBase.ateResultFolder;
                    return false;
                }
                if (ateRun.wPara.programName == string.Empty)
                {
                    er = "获取不到Chroma程序名称";
                    return false;
                }
                //第1次调用ＡＴＥ程序不会产生以下２个文件
                string dbNameFolder = ateRun.wBase.ateResultFolder + "\\" + ateRun.wPara.programName.Replace(".prg", "");
                string dbName = dbNameFolder + "\\TESTINFO.MDB";
                if (!Directory.Exists(dbNameFolder))
                {
                    er = "[" + dbNameFolder + "]文件夹不存在";
                    return true;
                }
                if (!File.Exists(dbName))
                {
                    er = "[" + dbName + "]数据库不存在";
                    return true;
                }
                //判断文件是否被占用？
                int vHandle = _lopen(dbName, OF_READWRITE | OF_SHARE_DENY_NONE);
                if (vHandle != HFILE_ERROR)
                    CloseHandle(vHandle);
                else
                {
                    CloseHandle(vHandle);
                    er = "[" + Path.GetFileName(dbName) + "]数据库被占用";
                    return true;
                }
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", dbName, "", "");
                string sqlCmd = string.Empty;
                DataSet ds = null;
                resultIsExist = true;
                for (int i = 0; i < ateRun.wResult.serialNos.Count; i++)
                {
                    if (ateRun.wResult.serialNos[i] != string.Empty)
                    {
                        ds = new DataSet();
                        sqlCmd = "select * from UUTReTest where Times>" + ateRun.wResult.snTestTimes[i] +
                                 " and SerialNo='" + ateRun.wResult.serialNos[i] + "' order by Times desc";
                        if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                            return false;
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            resultIsExist = false;
                            return true;
                        }
                        int result = System.Convert.ToInt16(ds.Tables[0].Rows[0]["Result"].ToString());
                        if (result == 1)
                            ateRun.wResult.result[i] = 0;
                        else
                            ateRun.wResult.result[i] = 1;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion

        #region Halcon获取结果
        /// <summary>
        /// 通过图片获取结果
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool GetResultFromImage(out string er,bool hPower=false )
        {
            er = string.Empty;

            try
            {

                if (ateRun.wHwnd.imgResult == 0)
                {
                    er = "获取测试结果句柄为空";
                    return false;
                }

                string imgFile = Application.StartupPath + "\\ATE.bmp";

                IntPtr mainHandler = new IntPtr(ateRun.wHwnd.mainProg);

                IntPtr handler = new IntPtr(ateRun.wHwnd.imgResult);

                setWindowToMax(mainHandler);

                Image img = PrintWindow(handler);

                img.Save(imgFile, System.Drawing.Imaging.ImageFormat.Bmp);

                List<int> imgResult = new List<int>();
                if (!hPower)
                {
                    if (!halcon_get_result(ateRun.wBase.C_MAX_CHAN, imgFile, ref imgResult, out er))
                        return false;

                    if (imgResult.Count != ateRun.wBase.C_MAX_CHAN)
                    {
                        er = "获取测试结果数量【" + ateRun.wBase.C_MAX_CHAN.ToString() + "】错误:" + imgResult.Count.ToString();
                        return false;
                    }

                    if (ateRun.wResult != null)
                    {
                        for (int i = 0; i < ateRun.wResult.result.Count; i++)
                            ateRun.wResult.result[i] = imgResult[i];
                    }
                }
                else
                {
                    if (!halcon_get_result(ateRun.wBase.C_MAX_CHAN/2, imgFile, ref imgResult, out er))
                        return false;

                    if (imgResult.Count != ateRun.wBase.C_MAX_CHAN/2)
                    {
                        er = "获取测试结果数量【" + (ateRun.wBase.C_MAX_CHAN/2).ToString() + "】错误:" + imgResult.Count.ToString();
                        return false;
                    }

                    if (ateRun.wResult != null)
                    {
                        for (int i = 0; i < ateRun.wResult.result.Count/2; i++)
                            ateRun.wResult.result[i*2] = imgResult[i];
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {
            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hObjectSource,
            int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
            int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }
        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            public enum E_SW
            {
                SW_HIDE = 0,
                SW_NORMAL = 1,
                SW_MAXIMIZE = 3,
                SW_SHOWNOACTIVATE = 4,
                SW_SHOW = 5,
                SW_MINIMIZE = 6,
                SW_SHOWNA = 8,
                SW_RESTORE = 9,
                SW_SHOWDEFAULT = 10
            }
            public enum E_After
            {
                /// <summary>
                /// 将窗口置于Z序的底部
                /// </summary>
                HWND_BOTTOM = 1,
                /// <summary>
                /// 将窗口置于所有非顶层窗口之上（即在所有顶层窗口之后）
                /// </summary>
                HWND_NOTOPMOST = -2,
                /// <summary>
                /// 将窗口置于Z序的顶部
                /// </summary>
                HWND_TOP = 0,
                /// <summary>
                /// 将窗口置于所有非顶层窗口之上
                /// </summary>
                HWND_TOPMOST = -1
            }
            public enum E_uFlag
            {
                SWP_NOSIZE = 0x0001,
                SWP_NOMOVE = 0x0002,
                SWP_NOZORDER = 0x0004,
                SWP_NOREDRAW = 0x0008,
                SWP_NOACTIVATE = 0x0010,
                SWP_FRAMECHANGED = 0x0020,
                SWP_SHOWWINDOW = 0x0040,
                SWP_HIDEWINDOW = 0x0080,
                SWP_NOCOPYBITS = 0x0100,
                SWP_NOOWNERZORDER = 0x0200,
                SWP_NOSENDCHANGING = 0x0400,
                TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE
            }
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
            [DllImport("user32.dll")]
            public static extern int PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
            [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
            public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
            //此处用于将窗口设置在最前
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool SetWindowPos(IntPtr hWnd,
            int hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            int uFlags
            );
        }
        private void setWindowToMax(IntPtr handle)
        {
            User32.ShowWindow(handle, (int)User32.E_SW.SW_MAXIMIZE);
        }
        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="dwRop"></param>
        /// <returns></returns>
        private Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }
        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        private Image PrintWindow(IntPtr handle)
        {
            User32.RECT windowRect = new User32.RECT();

            User32.GetWindowRect(handle, ref windowRect);

            int width = windowRect.right - windowRect.left;

            int height = windowRect.bottom - windowRect.top;

            Bitmap bitMap = new Bitmap(width, height);

            //	Graphics gp = bitMap.GetHbitmap

            Graphics gp = Graphics.FromImage(bitMap);

            User32.PrintWindow(handle, gp.GetHdc(), 0);

            gp.Dispose();

            return bitMap;

        }
        //Halcon获取图片测试结果 
        private bool halcon_get_result(int maxUUT, string imgFile, ref List<int> imgResult, out string er)
        {
            er = string.Empty;
            // Local iconic variables 
            HObject ho_Image = null, ho_Red = null, ho_Green = null, ho_Blue = null;
            HObject ho_Hue = null, ho_Saturation = null, ho_Intensity = null, ho_ObjectsConcat = null;
            HObject ho_RegionUnion = null, ho_ImageReduced = null, ho_Regions = null, ho_RegionErosion = null;
            HObject ho_ConnectedRegions = null, ho_SelectedRegions = null, ho_SortedRegions = null;
            // Local control variables 
            HTuple hv_Width, hv_Height, hv_Mean, hv_Deviation;
            HTuple hv_Result, hv_Number, hv_Index;
            // Initialize local and output iconic variables 
            try
            {
                //HOperatorSet.GenEmptyObj(out ho_Image);
                //HOperatorSet.GenEmptyObj(out ho_Red);
                //HOperatorSet.GenEmptyObj(out ho_Green);
                //HOperatorSet.GenEmptyObj(out ho_Blue);
                //HOperatorSet.GenEmptyObj(out ho_Hue);
                //HOperatorSet.GenEmptyObj(out ho_Saturation);
                //HOperatorSet.GenEmptyObj(out ho_Intensity);
                //HOperatorSet.GenEmptyObj(out ho_ObjectsConcat);
                //HOperatorSet.GenEmptyObj(out ho_RegionUnion);
                //HOperatorSet.GenEmptyObj(out ho_ImageReduced);
                //HOperatorSet.GenEmptyObj(out ho_Regions);
                //HOperatorSet.GenEmptyObj(out ho_RegionErosion);
                //HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
                //HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
                //HOperatorSet.GenEmptyObj(out ho_SortedRegions);
                HOperatorSet.ReadImage(out ho_Image, imgFile);
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.Decompose3(ho_Image, out ho_Red, out ho_Green, out ho_Blue);
                HOperatorSet.TransFromRgb(ho_Red, ho_Green, ho_Blue, out ho_Hue, out ho_Saturation,
                    out ho_Intensity, "hsv");
                HOperatorSet.GenRectangle1(out ho_ObjectsConcat, 0, 0, hv_Height, hv_Width);
                HOperatorSet.Union1(ho_ObjectsConcat, out ho_RegionUnion);
                HOperatorSet.ReduceDomain(ho_Blue, ho_RegionUnion, out ho_ImageReduced);
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Regions, 0, 180);
                HOperatorSet.ErosionCircle(ho_Regions, out ho_RegionErosion, 6);
                HOperatorSet.Connection(ho_RegionErosion, out ho_ConnectedRegions);
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 100, 279000);
                HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "first_point",
                    "true", "row");
                HOperatorSet.Intensity(ho_SortedRegions, ho_Red, out hv_Mean, out hv_Deviation);
                hv_Result = new HTuple();
                HOperatorSet.CountObj(ho_SortedRegions, out hv_Number);
                if (imgResult != null)
                    imgResult.Clear();
                else
                    imgResult = new List<int>();
                for (hv_Index = 1; hv_Index.Continue(hv_Number, 1); hv_Index = hv_Index.TupleAdd(1))
                {
                    //绿色 OK 1
                    if ((int)((new HTuple(((hv_Mean.TupleSelect(hv_Index - 1))).TupleGreaterEqual(
                        0))).TupleAnd(new HTuple(((hv_Mean.TupleSelect(hv_Index - 1))).TupleLessEqual(
                        10)))) != 0)
                    {

                        hv_Result[hv_Index - 1] = 1;
                        imgResult.Add(0);
                    }
                    //灰色  2
                    else if ((int)((new HTuple(((hv_Mean.TupleSelect(hv_Index - 1))).TupleLessEqual(150))).TupleAnd(
                        new HTuple(((hv_Mean.TupleSelect(hv_Index - 1))).TupleGreaterEqual(80)))) != 0)
                    {
                        hv_Result[hv_Index - 1] = 2;
                        imgResult.Add(0);
                    }
                    //    * 255 180
                    //红色 NG 3
                    else if ((int)((new HTuple(((hv_Mean.TupleSelect(hv_Index - 1))).TupleLessEqual(255))).TupleAnd(
                        new HTuple(((hv_Mean.TupleSelect(hv_Index - 1))).TupleGreaterEqual(180)))) != 0)
                    {
                        hv_Result[hv_Index - 1] = 3;
                        imgResult.Add(1);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
            finally
            {
                if (ho_Image != null)
                    ho_Image.Dispose();
                if (ho_Red != null)
                    ho_Red.Dispose();
                if (ho_Green != null)
                    ho_Green.Dispose();
                if (ho_Blue != null)
                    ho_Blue.Dispose();
                if (ho_Hue != null)
                    ho_Hue.Dispose();
                if (ho_Saturation != null)
                    ho_Saturation.Dispose();
                if (ho_Intensity != null)
                    ho_Intensity.Dispose();
                if (ho_ObjectsConcat != null)
                    ho_ObjectsConcat.Dispose();
                if (ho_RegionUnion != null)
                    ho_RegionUnion.Dispose();
                if (ho_ImageReduced != null)
                    ho_ImageReduced.Dispose();
                if (ho_Regions != null)
                    ho_Regions.Dispose();
                if (ho_RegionErosion != null)
                    ho_RegionErosion.Dispose();
                if (ho_ConnectedRegions != null)
                    ho_ConnectedRegions.Dispose();
                if (ho_SelectedRegions != null)
                    ho_SelectedRegions.Dispose();
                if (ho_SortedRegions != null)
                    ho_SortedRegions.Dispose();
            }
        }
        #endregion
    }
}
