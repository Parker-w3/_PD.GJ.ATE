using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ;
using GJ.Dev.COM;

namespace GJ.Dev.ChromaRRCC
{
  public  class ChromaLCR
    {

       #region 构造函数
	   public ChromaLCR()
       {
           com = new CSerialPort(); 
       }
	   #endregion

       #region 字段

       private ICom com=null;

       private int idNo = 0;

       private string name = "Chroma11021";

       private int plcNo = 0;

       private string  EOI ="\n";

       #endregion

       #region 属性
       public int mIdNo
       {
           set { idNo = value; }
           get { return idNo; }
       }
       public string mName
       {
           set { name = value; }
           get { return name; }
       }
       public int mPLCNo
       {
           set { plcNo = value; }
           get { return plcNo; }
       }
       public COM.ICom mCom
       {
           set { com = value; }
           get { return com; }
       }
       #endregion

       #region 方法
       /// <summary>
       /// 打开串口
       /// </summary>
       /// <param name="comName"></param>
       /// <param name="er"></param>
       /// <param name="setting"></param>
       /// <returns></returns>
       public bool open(string comName, ref string er, string setting = "19200,n,8,1")
       {
           try
           {
               if (com == null)
                   return false;
               com.mComDataType = 1;
               return com.open(comName, ref er, setting);
           }
           catch (Exception ex)
           {
               er = ex.ToString();
               return false; 
           }
       }
       /// <summary>
       /// 关闭串口
       /// </summary>
       public void close()
       {
           com.close();
       }

       /// <summary>
       /// 设置波特率
       /// </summary>
       /// <param name="setting"></param>
       /// <param name="er"></param>
       /// <returns></returns>
       public bool setBaud(ref string er, string setting = "19200,n,8,1")
       {
           return com.setBaud(ref er, setting);  
       }

       private bool SetReset(ref string er)
       {
           try
           {
               string wData = "*RST";

               string rData = string.Empty;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;

               return true;
           }

           catch (Exception ex)
           {
               er = ex.ToString();
               return false;
           }

       }
       /// <summary>
       /// 读取仪器设备
       /// </summary>
       /// <param name="devName"></param>
       /// <param name="er"></param>
       /// <returns></returns>
       private bool readIDN(ref string devName, ref string er)
       {
           try
           {
               string wData = "*IDN?";

               string rData = string.Empty;
               if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                   return false;
               string[] valList = rData.Split(',');
               if (valList.Length < 2)
                   return false;
               devName = valList[0] + valList[1];
               return true;
           }
           catch (Exception ex)
           {
               er = ex.ToString();
               return false;
           }
       }

       /// <summary>
       /// 初始化设备
       /// </summary>
       /// <param name="er"></param>
       /// <returns></returns>
       public bool init(ref string er)
       {
           try
           {
               string devName = string.Empty;

               if (!readIDN(ref devName, ref er))
                   return false;
               if (devName != name)
               {
                   er = "LCR仪器[" + name + "]型号错误:" + devName;
                   return false;
               }

               //if (!remote(ref er))
               //    return false;
               if (!SetReset (ref er))
               {
                   er = "LCR仪器清除错误出错:" + devName;
                   return false;
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
       /// 远程控制
       /// </summary>
       /// <param name="er"></param>
       /// <returns></returns>
       public bool remote(ref string er)
       {
           try
           {
               string wData = "CONF:REM ON?";

               string rData = string.Empty;


               if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                   return false;
               if (rData != "1")
                   return false;
               return true;
             
           }
           catch (Exception ex)
           {
               er = ex.ToString();
               return false;
           }
       }

        /// <summary>
        /// 设置机种参数
        /// </summary>
        /// <param name="mMode"></param>
        /// <param name="mTestMode"></param>
        /// <param name="mHZ"></param>
        /// <param name="mVolt"></param>
        /// <param name="er"></param>
        /// <returns></returns>
       public bool SetModePara(string mMode, string mTestModel,int mHZ, double mVolt,string Rmax,string mSpeed, ref string er)
       {
           try
           {
               string wData = "TRIG:SOUR " + mMode; //设置模式

               string rData = string.Empty;

               if (!SendCmdToChroma (wData,ref rData ,ref er, "" ))
                   return false;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               //if (rData != mMode)
               //    return false;
               string[] testModel = mTestModel.Split(',');
               if (testModel.Length < 2)
               {
                   testModel = new string[2];
                   testModel[0] = "CS";
                   testModel[1] = "RS";
               }
               wData = "CALC1:FORM "+testModel[0];  //测试模式
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               wData = "CALC2:FORM " + testModel[1];
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;

               
                wData = "SENS:FIMP:RANG " + Rmax; //测试电阻挡位
                if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                    return false;
                if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                    return false;
                if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                    return false;

                wData = "SENS:FIMP:APER " + mSpeed ; //测试速度
                if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                    return false;
                if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                    return false;
                if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                    return false;

                wData = "SOUR:FREQ " + mHZ.ToString (); //测试频率
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;

               wData = "SOUR:VOLT " + mVolt.ToString();//电压
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;

               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;


               return true;
           }
           catch (Exception ex)
           {
               er = ex.ToString();
                return false ;
           }

       }
       public bool StartTest(ref string er)
       {
           try
           {
               string wData = "TRIG";

               string rData = string.Empty;

               wData = wData + EOI;
               if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                   return false;
               
    
               return true;
           }

           catch (Exception ex)
           {
               er = ex.ToString();
               return false;
           }
       }
        /// <summary>
        /// 读取测量值
        /// </summary>
        /// <param name="mCC"></param>
        /// <param name="mRR"></param>
        /// <param name="er"></param>
        /// <returns></returns>
       public bool ReadPara(ref double mCC,ref double mRR, ref string er)
       {
           try
           {
               //string wData = "TRIG";

               //string rData = string.Empty;

               //wData = wData + EOI;
               //if (!SendCmdToChroma(wData, ref rData, ref er, ""))
               //    return false;

               string wData = "FETCH?";

               string rData = string.Empty;
               wData = "FETCH?";
               wData = wData + EOI;
               if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                   return false;
               string[] valList = rData.Split(',');
               if (valList.Length < 2)
                   return false;
               mCC = System.Convert.ToDouble(valList[1]);
               mRR = System.Convert.ToDouble(valList[2]);
               return true;
           }
           catch (Exception ex)
           {
               er = ex.ToString();
               return false;

           }


       }

       /// <summary>
       /// 发送和接收数据
       /// </summary>
       /// <param name="wAddr"></param>
       /// <param name="wData"></param>
       /// <param name="rLen"></param>
       /// <param name="rData"></param>
       /// <param name="er"></param>
       /// <param name="wTimeOut"></param>
       /// <returns></returns>
       private bool SendCmdToChroma(string wData, ref string rData, ref string er, string rEOI = "\n", int wTimeOut = 1000)
       {

           string recvData = string.Empty;
           wData += "\n";
           if (!com.send(wData, rEOI, ref rData, ref er, wTimeOut))
               return false;
           rData = rData.Replace("\n", "");
           return true;

       }
       #endregion
      

  

    }
}
