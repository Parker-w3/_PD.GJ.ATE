using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ;
using GJ.Dev.COM;
namespace GJ.Dev.ChromaEload
{
   public class Chroma6334
    {
      #region 构造函数
	   public Chroma6334()
       {
           com = new CSerialPort(); 
       }
	   #endregion

       #region 字段

       private ICom com=null;

       private int idNo = 0;

       private string name = "Chroma6334A";

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
       public bool open(string comName, ref string er, string setting = "9600,n,8,1")
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
       public bool setBaud(ref string er, string setting = "9600,n,8,1")
       {
           return com.setBaud(ref er, setting);  
       }
        /// <summary>
        /// 读电压
        /// </summary>
        /// <param name="V"></param>
        /// <param name="er"></param>
        /// <param name="wTimeOut"></param>
        /// <returns></returns>
       public bool readVolt(ref List<double>V, ref string er,int wTimeOut =1000)
       {
           try
           {
              

               string rData=string.Empty;
               //string xData = "CHAN " + (1).ToString();

               //if (!SendCmdToChroma(xData, ref rData, ref er, ""))
               //    return false;
               //xData = "MEAS:INP UUT";
               //if (!SendCmdToChroma(xData, ref rData, ref er, ""))
               //    return false;

               //xData = "MEAS:INP LOAD";
               //if (!SendCmdToChroma(xData, ref rData, ref er, ""))
               //    return false;
               //xData = "MEAS:INP?";
               //if (!SendCmdToChroma(xData, ref rData, ref er, EOI))
               //    return false;

               //xData = "MEAS:VOLT?";
               //if (!SendCmdToChroma(xData, ref rData, ref er, EOI))
               //    return false;

               string wData = "MEAS:ALLV?";
               if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                   return false;

               rData = rData.Replace("\r\n", ""); 

               string[] valList =rData.Split(',');

               int stepno = valList.Length;
               for (int i = 0; i < stepno; i++)
               {
                   double resultcode = System.Convert.ToDouble(valList[i]);
                   V.Add(resultcode);
                   i++;
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
       /// 读电流
       /// </summary>
       /// <param name="A"></param>
       /// <param name="er"></param>
       /// <param name="wTimeOut"></param>
       /// <returns></returns>
       public bool readCur(ref List<double> A, ref string er, int wTimeOut = 1000)
       {
           try
           {

               string rData = string.Empty;


               string wData = "MEAS:ALLC?";
               wData += "\r\n";
              // string wDate = "FETC:ALLC?";
               if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                   return false;

               rData = rData.Replace("\r\n", "");

               string[] valList = rData.Split(',');

               int stepno = valList.Length;
               for (int i = 0; i < stepno; i++)
               {
                   double resultcode = System.Convert.ToDouble(valList[i]);
                   A.Add(resultcode);
                   i++;
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
               wData += "\n";
               if (!com.send(wData, EOI, ref rData, ref er))
                   return false;
               rData = rData.Replace("\n", "");
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
               {
                   if (!readIDN(ref devName, ref er))
                       return false;
               }
               if (devName != name)
               {
                   er = "电子负载仪器[" + name + "]型号错误:" + devName;
                   return false;
               }

               //if (!remote(ref er))
               //    return false;
               string rData = string.Empty;
               com.send("*CLS", EOI, ref rData, ref er); //清除错误
                 //  return false;


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
       private bool remote(ref string er)
       {
           try
           {
               string wData = "CONF:REM ON?";

               string rData = string.Empty;

               wData = wData + EOI;
               if (!com.send (wData ,EOI, ref rData, ref er))
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
       #endregion
        /// <summary>
        /// 设置负载模式
        /// </summary>
        /// <param name="setMode"></param>
        /// <param name="setVal"></param>
        /// <param name="er"></param>
        /// <param name="wTimeOut"></param>
        /// <returns></returns>
       public bool SetPara(List<int> mPort, string setMode, double setVal, double wVon, ref string er, int wTimeOut = 1000)
       {
           try
           {
                  string rData = string.Empty;
                  int mChanel = 0;
                  string wData = string.Empty;
                  for (int i = 0; i < mPort.Count; i++)
                  {
                  
 
                      mChanel = mPort[i]; //设置通道
                      wData = "CHAN " + mChanel .ToString ();

                      if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                          return false;
                      if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                          return false;
                      if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                          return false;
                      wData = "CHAN?" ;
                      if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                          return false;
                      if (!(System.Convert.ToInt32(rData) == mChanel))
                      {
                          er = "设置通道" +  mChanel .ToString() +"失败";
                          return false;
                      }

                      wData = "LOAD OFF";
                      if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                          return false;
                      wData = "LOAD OFF";
                      if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                          return false;
                      wData = "LOAD OFF";
                      if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                          return false;
                      CMath.WaitMs(30);

                      wData = "MODE " + setMode;  //设置模式
                      if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                          return false;
                      wData = "MODE " + setMode;  //设置模式
                      if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                          return false;
                      wData = "MODE " + setMode;  //设置模式
                      if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                          return false;
                      wData = "MODE?";  //设置模式
                      if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                          return false;
                      if (!(rData == setMode))
                      {
                          er = "设置通道" + mChanel.ToString() + "模式失败";
                          return false;
                      }
                      //设载
                      switch (setMode)
                      {
                          case "CCL":
                              wData = "CURR:STAT:L1 " + setVal.ToString() + ";:" + "CURR:STAT:L2 " + setVal.ToString();
                              break ;
                          case "CCH":
                              wData = "CURR:STAT:L1 " + setVal.ToString() + ";:" + "CURR:STAT:L2 " + setVal.ToString();
                              break;
                          case "CV":
                              wData = "VOLT:L1 "+setVal.ToString() + "V";
                              break;
                          case  "CPH" :
                            //  wData = "POW:STAT:L1 " + setVal.ToString() + ";:" + "POW:STAT:L2 " + setVal.ToString();
                              wData = "POW:STAT:L1 " + setVal.ToString();
                              break ;
                          default :
                              break;
                      }

                      if (wData != "")
                      {
                          if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                              return false;
                          if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                              return false;
                          if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                              return false;

                          CMath.WaitMs(30);
                          wData = "CONF:VOLT:ON " + wVon.ToString();
                          if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                              return false;
                          if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                              return false;
                          if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                              return false;
                          CMath.WaitMs(100);

                          wData = "LOAD ON";
                          if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                              return false;
                          wData = "LOAD ON";
                          if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                              return false;
                          wData = "LOAD ON";
                          if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                              return false;

                          //wData = "CURR:STAT:L2?";
                          //if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                          //    return false;

                      }

                  }
                  er = "设置负载参数成功";
               return true;
           }

           catch (Exception ex)
           {
               er = ex.ToString();
               return false;
           }
       }

       /// <summary>
       /// 设置负载模式
       /// </summary>
       /// <param name="setMode"></param>
       /// <param name="setVal"></param>
       /// <param name="er"></param>
       /// <param name="wTimeOut"></param>
       /// <returns></returns>
       public bool SetParaOFF(List<int> mPort, ref string er, int wTimeOut = 1000)
       {
           try
           {
               string rData = string.Empty;
               int mChanel = 0;
               string wData = string.Empty;
               for (int i = 0; i < mPort.Count; i++)
               {
                   mChanel = mPort[i]; //设置通道
                   wData = "CHAN " + mChanel.ToString();

                   if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                       return false;
                   if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                       return false;
                   if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                       return false;
                   wData = "CHAN?";
                   if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                       return false;
                   if (!(System.Convert.ToInt32(rData) == mChanel))
                   {
                       er = "设置通道" + mChanel.ToString() + "失败";
                       return false;
                   }
                 

                       wData = "LOAD OFF";
                       if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                           return false;
                       wData = "LOAD OFF";
                       if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                           return false;
                       wData = "LOAD OFF";
                       if (!SendCmdToChroma(wData, ref rData, ref er, ""))
                           return false;

                       //wData = "CURR:STAT:L2?";
                       //if (!SendCmdToChroma(wData, ref rData, ref er, EOI))
                       //    return false;

                   

               }
               er = "设置负载参数成功";
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
           er = string.Empty;
           string recvData = string.Empty;
           wData += "\n";
           if (!com.send(wData, rEOI, ref rData, ref er, wTimeOut))
               return false;
           rData = rData.Replace("\n", "");
           return true;
   
       }

    }
}
