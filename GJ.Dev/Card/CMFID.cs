using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.Card
{
    /// <summary>
    /// 读卡器版本
    /// </summary>
    public enum EVersion
    {
        A,   //旧版本
        B    //新版本
    }
    /// <summary>
    /// 读卡器工作模式
    /// </summary>
    public enum EMode
    {
        /// <summary>
        /// 指令读取模式: 只能用相关指令读取卡片资料(出厂模式)
        /// </summary>
        A,
        /// <summary>
        /// 直接模式:读到卡片资料自动发送 (如果连续在0.5S内读到同一卡片将不发送)
        /// </summary>
        B,
        /// <summary>
        /// 外部触发模式:  收到外部电平触发时读取卡片资料并发送(电平触发保持时间>200ms)
        /// </summary>
        C
    }

    public class CMFID:ICARD
    {

    #region 构造函数
      public CMFID(EVersion ver=EVersion.B)
      {
         com = new COM.CSerialPort();         
         if(ver==EVersion.A)
             TYPE = "A";
         else
             TYPE ="B";        
      }
      #endregion

    #region 字段
    private string name = "MFID-485-02";
    private int idNo = 0;
    private COM.ICom com;
    #endregion

    #region 属性
    public string mName
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public int mIdNo
    {
        get
        {
            return idNo;
        }
        set
        {
            idNo = value;
        }
    }
    /// <summary>
    /// 版本
    /// </summary>
    public string mVersion
    {
        get { return TYPE; }
        set { TYPE = value; }
    }
    public COM.ICom mCom
    {
        set { com = value; }
    }
    #endregion

    #region 方法
    public bool open(string comName, ref string er, string setting = "19200,E,8,1")
    {
        if (com == null)
            return false;
        return com.open(comName, ref er, setting);
    }
    public void close()
    {
        if (com == null)
            return;
        com.close();
    }
    /// <summary>
    /// 读取对应地址的卡号
    /// </summary>
    /// <param name="idAddr"></param>
    /// <param name="er"></param>
    /// <returns></returns>
    public bool GetRecorderSn(int idAddr,ref string rSn, ref string er)
    {
        try
        {

            if (idAddr < 0 || idAddr > 99)
            {
                er = "请输入0-99之间的有效地址";
                return false;
            }

            if (com == null)
                return false;

            string wCmd = string.Empty;

            int rLen = 16;

            if(TYPE=="A")
            {
                ID = idAddr.ToString("D1");
                rLen = 15;
            }              
            else
            {
                ID = idAddr.ToString("D2");
            }

            FC="B";

            DATA = string.Empty;

            wCmd = SOH + TYPE + ID + FC + DATA;

            if(!BBCCode(wCmd,ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (!SendCmdToCom(wCmd, rLen,8, ref DATA, ref er))
                return false;

            rSn = DATA; 

            return true;
        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    }
    /// <summary>
    /// 设置卡号的地址
    /// </summary>
    /// <param name="strSn"></param>
    /// <param name="idAddr"></param>
    /// <returns></returns>
    public bool SetRecorderID(string strSn,int idAddr,ref string er)
    {
        try
        {

            if (idAddr < 0 || idAddr > 99)
            {
                er = "请输入0-99之间的有效地址";
                return false;
            }

            if (com == null)
                return false;

            string wCmd = string.Empty;            
            int rLen = 8;

            ID = "X";

            FC = "C";

            strSn = "00000000" + strSn;

            strSn = strSn.Substring(strSn.Length - 8, 8);

            if (TYPE == "A")
            {
                DATA = strSn + idAddr.ToString("D1");
                rLen = 7;
            }
            else
            {
                DATA = strSn + idAddr.ToString("D2");
            }

            wCmd = SOH + TYPE + ID + FC + DATA;

            if (!BBCCode(wCmd, ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (!SendCmdToCom(wCmd, rLen,0, ref DATA, ref er))
                return false;

            return true;

        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    }
    /// <summary>
    /// 读取卡号的地址
    /// </summary>
    /// <param name="strSn"></param>
    /// <param name="idAddr"></param>
    /// <param name="er"></param>
    /// <returns></returns>
    public bool GetRecorderID(string strSn,ref int idAddr,ref string er)
    {
        try
        {
            if (com == null)
                return false;

            string wCmd = string.Empty;

            int rLen = 9;

            ID = "X";

            FC = "D";

            strSn = "00000000" + strSn;

            strSn = strSn.Substring(strSn.Length - 8, 8);

            DATA = strSn;

            wCmd = SOH + TYPE + ID + FC + DATA;

            if (!BBCCode(wCmd, ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (TYPE == "A")
            {
                rLen = 8;
                if (!SendCmdToCom(wCmd, rLen, 1, ref DATA, ref er))
                    return false;
            }
            else
            {
                if (!SendCmdToCom(wCmd, rLen, 2, ref DATA, ref er))
                    return false;
            }

            idAddr = System.Convert.ToInt32(DATA);  

            return true;
        }
        catch (Exception e)
        {
            er = e.ToString();
            return false; 
        }
    }
    /// <summary>
    /// 读取地址编号卡号资料
    /// </summary>
    /// <param name="idAddr"></param>
    /// <param name="rIDCard"></param>
    /// <param name="er"></param>
    /// <returns></returns>
    public bool GetRecord(int idAddr,ref string rIDCard,ref string er)
    {
        try
        {
            if (idAddr < 0 || idAddr > 99)
            {
                er = "请输入0-99之间的有效地址";
                return false;
            }

            if (com == null)
                return false;

            string wCmd = string.Empty;
            int rLen = 17;

            if (TYPE == "A")
            {
                ID = idAddr.ToString("D1"); 
                rLen = 16;
            }
            else
            {
                ID = idAddr.ToString("D2"); 
            }

            FC = "F";

            DATA = string.Empty;

            wCmd = SOH + TYPE + ID + FC + DATA;

            if (!BBCCode(wCmd, ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (!SendCmdToCom(wCmd, rLen, 9, ref DATA, ref er))
                return false;

            int iSn = System.Convert.ToInt32(DATA, 16);

            rIDCard = iSn.ToString("D10");  

            return true;
        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    }
    /// <summary>
    /// 读取地址编号卡号资料
    /// </summary>
    /// <param name="idAddr"></param>
    /// <param name="rIDCard"></param>
    /// <param name="er"></param>
    /// <returns></returns>
    public bool GetRecordAgain(int idAddr, ref string rIDCard, ref string er)
    {
        try
        {
            if (idAddr < 0 || idAddr > 99)
            {
                er = "请输入0-99之间的有效地址";
                return false;
            }

            if (com == null)
                return false;

            string wCmd = string.Empty;
            int rLen = 17;

            if (TYPE == "A")
            {
                ID = idAddr.ToString("D1");
                rLen = 16;
            }
            else
            {
                ID = idAddr.ToString("D2");
            }

            FC = "G";

            DATA = string.Empty;

            wCmd = SOH + TYPE + ID + FC + DATA;

            if (!BBCCode(wCmd, ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (!SendCmdToCom(wCmd, rLen, 9, ref DATA, ref er))
                return false;

            int iSn = System.Convert.ToInt32(DATA, 16);

            rIDCard = iSn.ToString("D10");

            return true;
        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    }
    /// <summary>
    /// 读取触发信号状态和卡片资料(有卡片感应)-触发信号为1
    /// </summary>
    /// <param name="idAddr"></param>
    /// <param name="rIDCard"></param>
    /// <param name="er"></param>
    /// <returns></returns>
    public bool GetRecordTriggerSignal(int idAddr, ref string rIDCard,ref bool rTrigger, ref string er)
    {
        try
        {
            if (idAddr < 0 || idAddr > 99)
            {
                er = "请输入0-99之间的有效地址";
                return false;
            }

            if (com == null)
                return false;

            string wCmd = string.Empty;
            int rLen = 18;

            ID = idAddr.ToString("D2");

            FC = "I";

            DATA = string.Empty;

            TYPE = "B";

            wCmd = SOH + TYPE + ID + FC + DATA;

            if (!BBCCode(wCmd, ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (!SendCmdToCom(wCmd, rLen, 10, ref DATA, ref er))
                return false;

            rIDCard = DATA.Substring(0, 9);

            if (DATA.Substring(9, 1) == "1")
                rTrigger = true;
            else
                rTrigger = false; 

            if (rIDCard == "000000000")
            {
                er = "无卡片感应";
                return false;
            }

            int iSn = System.Convert.ToInt32(rIDCard, 16);

            rIDCard = iSn.ToString("D10");

            return true;
        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    }
    /// <summary>
    /// 设置读卡器工作模式(广播)
    /// </summary>
    /// <param name="idAddr"></param>
    /// <param name="mode"></param>
    /// <param name="er"></param>
    /// <returns></returns>
    public bool SetRecorderWorkMode(EMode mode, ref string er)
    {
        try
        {
            if (com == null)
                return false;

            string wCmd = string.Empty;
            int rLen = 0;

            ID = "X";

            FC = "N";

            DATA = mode.ToString();

            TYPE = "B";

            wCmd = SOH +TYPE + ID + FC + DATA;

            if (!BBCCode(wCmd, ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (!SendCmdToCom(wCmd, rLen, 0, ref DATA, ref er))
                return false;

            return true;

        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    }
    /// <summary>
    /// 设置读卡器工作模式
    /// </summary>
    /// <param name="idAddr"></param>
    /// <param name="mode"></param>
    /// <param name="er"></param>
    /// <returns></returns>
    public bool SetRecorderWorkMode(int idAddr,EMode mode,ref string er)
    {
        try
        {

            if (idAddr < 0 || idAddr > 99)
            {
                er = "请输入0-99之间的有效地址";
                return false;
            }

            if (com == null)
                return false;

            string wCmd = string.Empty;
            int rLen = 9;

            ID = idAddr.ToString("D2");

            FC = "H";

            DATA = mode.ToString();

            TYPE = "B";

            wCmd = SOH + TYPE + ID + FC + DATA;

            if (!BBCCode(wCmd, ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (!SendCmdToCom(wCmd, rLen, 1, ref DATA, ref er))
                return false;

            return true;

        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    }
    /// <summary>
    /// 读取读卡器工作模式
    /// </summary>
    /// <param name="idAddr"></param>
    /// <param name="mode"></param>
    /// <param name="er"></param>
    /// <returns></returns>
    public bool GetRecordMode(int idAddr, ref EMode mode, ref string er)
    {
        try
        {
            if (idAddr < 0 || idAddr > 99)
            {
                er = "请输入0-99之间的有效地址";
                return false;
            }

            if (com == null)
                return false;

            string wCmd = string.Empty;
            int rLen = 9;

            ID = idAddr.ToString("D2");

            FC = "J";

            DATA = string.Empty;

            TYPE = "B";

            wCmd = SOH + TYPE + ID + FC + DATA;

            if (!BBCCode(wCmd, ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (!SendCmdToCom(wCmd, rLen, 1, ref DATA, ref er))
                return false;

            if (DATA == EMode.A.ToString())
                mode = EMode.A;
            else if (DATA == EMode.B.ToString())
                mode = EMode.B;
            else if (DATA == EMode.C.ToString())
                mode = EMode.C;
            else
            {
                er = "返回工作模式错误:"+DATA;
                return false; 
            }
            return true;
        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    }
    /// <summary>
    /// 读取读卡器触发信号状态
    /// </summary>
    /// <param name="idAddr"></param>
    /// <param name="Trigger"></param>
    /// <param name="er"></param>
    /// <returns></returns>
    public bool GetTriggerSignal(int idAddr, ref bool Trigger, ref string er)
    {
        try
        {
            if (idAddr < 0 || idAddr > 99)
            {
                er = "请输入0-99之间的有效地址";
                return false;
            }

            if (com == null)
                return false;

            string wCmd = string.Empty;
            int rLen = 9;

            ID = idAddr.ToString("D2");

            FC = "K";

            DATA = string.Empty;

            TYPE = "B";

            wCmd = SOH + TYPE + ID + FC + DATA;

            if (!BBCCode(wCmd, ref BBC))
            {
                er = "计算BBC Check错误";
                return false;
            }

            wCmd += BBC + EOH;

            if (!SendCmdToCom(wCmd, rLen, 1, ref DATA, ref er))
                return false;

            if (DATA == "1")
                Trigger = true;
            else
                Trigger = false;  

            return true;
        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    }
    #endregion

    #region 协议
    /*
    *   HEAD(SOH+TYPE+ID+FC) + DATA + BCC CHECK +EOH
    *   TYPE为模块型式编号,固定为一个字节.新版本为“B”，旧版本为“A”
    *   ID为模块端的识别代码<2字节>.这两字节的ASCII字符必须是在0-99
    *   FC是通讯功能码,和资料有相关性,固定为一个字节
    */
    private string SOH=new string(new char[]{'\x09'});
    private string EOH = new string(new char[] {'\x0D'});
    private string TYPE="A";
    private string FC = string.Empty;
    private string ID = string.Empty;
    private string BBC = string.Empty;
    private string DATA = string.Empty; 
    /// <summary>
    /// 发送和接收串口数据
    /// </summary>
    /// <param name="wData"></param>
    /// <param name="rLen"></param>
    /// <param name="rData"></param>
    /// <param name="er"></param>
    /// <param name="wTimeOut"></param>
    /// <returns></returns>
    private bool SendCmdToCom(string wData, int rLen,int rDataLen, ref string rData, ref string er, int wTimeOut =500)
    {
        try
        {
            string recvData=string.Empty;
            com.mComDataType = 1; 
            if(!com.send(wData,rLen,ref recvData,ref er,wTimeOut))
                return false;
            if(rLen!=0 && !BBCCheck(recvData))
            {
                er = "BBC数据校验错误";
                return false;
            }
            if (rDataLen != 0)
                rData = recvData.Substring(recvData.Length - rDataLen - 3, rDataLen);
            else
                rData = string.Empty;
            return true;
        }
        catch (Exception e)
        {
            er = e.ToString();
            return false;
        }
    
    }
    /// <summary>
    /// BBC检验和
    /// </summary>
    /// <param name="strData"></param>
    /// <param name="rCode"></param>
    /// <returns></returns>
    private bool BBCCode(string strData, ref string rCode)
    {
        try
        {
            Byte[] byteArray=System.Text.Encoding.Default.GetBytes(strData);    
            Byte byteTemp=byteArray[0]; 
            for (int i = 1; i < byteArray.Length; i++)
			{
			  byteTemp ^=byteArray[i]; 
			}
            rCode = byteTemp.ToString("X2");
            return true;  
        }
        catch (Exception)
        {
            return false;
        }
    }
    /// <summary>
    /// 检查BBC
    /// </summary>
    /// <param name="strData"></param>
    /// <returns></returns>
    private bool BBCCheck(string strData)
    {
        try
        {
            string getBBC = strData.Substring(strData.Length - 3, 2);

            string strBBC = strData.Substring(0, strData.Length - 3);  

            string calBBC=string.Empty;

            if (!BBCCode(strBBC, ref calBBC))
                return false; 

            if(calBBC!=getBBC)
                return false; 

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion

    }
}
