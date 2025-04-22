using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Dev.COM;
namespace GJ.Dev.PLC
{
   public class Omron_COM:IPLC
   {
      #region 构造函数
      public Omron_COM()
      {
         com = new CSerialPort();
      }
      public Omron_COM(ICom com)
      {
         this.com = com;
      }
      #endregion

      #region 字段
      private ICom com;
      private string name = "OmronPLC";
      private int plcNo = 0;
      //W,M,D,X,Y
      private string[] c_DataType = new string[] { "B1", "B1", "82", "B0", "B0" }; 
      #endregion

      #region 属性
      public string mName
      {
         get { return name; }
         set { name = value; }
      }
      public int mIdNo
      {
         get { return plcNo; }
         set { plcNo = value; }
      }
      public COM.ICom mCom
      {
          get { return com; }
          set { com = value; } }
      #endregion

      #region 方法
      /// <summary>
      /// 打开串口
      /// </summary>
      /// <param name="comName">115200,E,7,2</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool open(string comName, ref string er, string setting = "115200,E,7,2")
      {
          if (!com.open(comName, ref er, setting))
            return false;
         com.mComDataType = 1;
         return true;
      }
      /// <summary>
      /// 关闭串口
      /// </summary>
      public void close()
      {
         com.close();
      }
      /// <summary>
      /// 读线圈和寄存器值
      /// 从机地址(1Byte)+功能码(1Byte)+寄存器地址(2Byte)+地址数量(2Byte)+CRC检验(2Byte)
      /// </summary>
      /// <param name="devType">地址类型</param>
      /// <param name="startAddr">开始地址</param>
      /// <param name="N">地址长度</param>
      /// <param name="rData">16进制字符:数据值高位在前,低位在后</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool read(EDevType devType, int startAddr, int N, ref string rData, ref string er)
      {
         return readOmronREG(devType, startAddr, N, ref rData, ref er);  
      }
      /// <summary>
      /// 返回单个数值
      /// </summary>
      /// <param name="devType"></param>
      /// <param name="startAddr"></param>
      /// <param name="N"></param>
      /// <param name="rVal">开始地址值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool read(EDevType devType, int startAddr, ref int rVal, ref string er)
      {
         string rData=string.Empty;
         if (!readOmronREG(devType, startAddr,1, ref rData, ref er))
            return false;
         rVal = System.Convert.ToInt32(rData, 16);
         return true;
      }
      /// <summary>
      /// 返回数值
      /// </summary>
      /// <param name="devType"></param>
      /// <param name="startAddr"></param>
      /// <param name="N"></param>
      /// <param name="rVal">地址N值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool read(EDevType devType, int startAddr, ref int[] rVal, ref string er)
      {
         string rData = string.Empty;
         int N;
         if (devType == EDevType.D)
            N = rVal.Length;
         else
            N = (rVal.Length+15) / 16;
         if (!readOmronREG(devType, startAddr, N, ref rData, ref er))
            return false;
         int temVal = 0;
         int z = 0;
         for (int i = 0; i < N; i++)
         {
            temVal =System.Convert.ToInt32 (rData.Substring(i * 4, 4),16);
            if (devType == EDevType.D)
            {
               rVal[i] = temVal;
            }
            else
            {
               for (int j = 0; j < 16; j++)
               {
                  if (z < rVal.Length)
                  {
                     if ((temVal & (1 << j)) == (1 << j))
                        rVal[z] = 1;
                     else
                        rVal[z] = 0;
                     z++;
                  }
               }
            }
         }
         return true;
      }
      /// <summary>
      /// 单写线圈和寄存器值
      /// </summary>
      /// <param name="devType">地址类型</param>
      /// <param name="startAddr">开始地址</param>
      /// <param name="wVal">单个值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool write(EDevType devType, int startAddr, int wVal, ref string er)
      {
         string rData = string.Empty;
         return SendOmronCommand("0102", devType, startAddr, 1, wVal.ToString(), ref rData, ref er);
      }
      /// <summary>
      /// 写多个线圈和寄存器
      /// </summary>
      /// <param name="devType">地址类型</param>
      /// <param name="startAddr">开始地址</param>
      /// <param name="wVal">多个值</param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool write(EDevType devType, int startAddr, int[] wVal, ref string er)
      {
         string wContent = string.Empty;
         int N = 0;
         if (devType == EDevType.D)
         {
            N = wVal.Length;
            for (int i = 0; i < wVal.Length; i++)
            {
               wContent += wVal[i].ToString("X4");
            }
         }
         else
         {
            N = (wVal.Length + 15) / 16;
            int z=0;            
            for (int i = 0; i < N; i++)
            {
               int bitData = 0;
               for (int j = 0; j < 16; j++)
               {
                  if (z < wVal.Length)
                  {
                     if (wVal[z] == 1)
                        bitData += (1 << j);
                  }
                  z++;
               }
               wContent += bitData.ToString("X4");
            }
         }         
         string rData = string.Empty;
         return SendOmronCommand("0102", devType, startAddr, N, wContent, ref rData, ref er);
      }

      #endregion

      #region 协议
      private string c_SOI = "@";
      private string c_EOI =new string(new char[]{'\x2A','\x0D'}) ;

      private bool readOmronREG(EDevType wDataType,int wStartAddr,int wLen,ref string rData,ref string er)
      {        
         return SendOmronCommand("0101",wDataType,wStartAddr,wLen,string.Empty,ref rData,ref er); 
      }
      private bool writeOmronREG(EDevType wDataType, int wStartAddr, int wLen, string wWriteContent, ref string rData, ref string er)
      {
         return SendOmronCommand("0102", wDataType, wStartAddr, wLen, wWriteContent, ref rData, ref er); 
      }
      private bool SendOmronCommand(string wCommandType,EDevType wDataType,int wStartAddr,int wWordCount,string wWriteContent,
                                    ref string rData, ref string er)
      {
         try 
	      {	        		    
           if(wWriteContent!=string.Empty)
           {
            for (int i = 0; i < wWordCount; i++)
			   {
			    wWriteContent="0000"+wWriteContent;
			   }
            wWriteContent=wWriteContent.Substring(wWriteContent.Length - wWordCount * 4, wWordCount * 4);
           }
           string strCommand;
           strCommand=c_SOI+plcNo.ToString("X2")+"FA000000000";
           strCommand+=wCommandType; 
           strCommand+=c_DataType[(int)wDataType];  
           strCommand+=wStartAddr.ToString("X4")+"00";  
           strCommand+=wWordCount.ToString("X4");  
           strCommand+=wWriteContent;  
           strCommand+=codeFcs(strCommand);
           strCommand+=c_EOI;
           int lngWordLen=0;
           if(wWriteContent==string.Empty)
              lngWordLen =4;
           int rLen=27 + lngWordLen * wWordCount;
           if(!com.send(strCommand,rLen,ref rData,ref er))
              return false; 
            if(!VerifyReceive(rData,wCommandType))
            {
               er= "接收数据checkSum错误:" + rData;
               return false; 
            }
            if (wWriteContent != string.Empty)      //write       
               rData = "OK";
            else                                    //read
               rData = rData.Substring(23, lngWordLen * wWordCount);                
            return true;
	      }
	      catch (Exception e)
	      {
		      er=e.ToString(); 
		      return false;
	      }
      }
      /// <summary>
      /// 检查接收数据是否正确
      /// </summary>
      /// <param name="wResultString"></param>
      /// <param name="wAddr"></param>
      /// <param name="wCommandType"></param>
      /// <returns></returns>
      private bool VerifyReceive(string wResultString,string wCommandType)
      {
        if(plcNo.ToString("X2")!=wResultString.Substring(1,2))
           return false;
         if(wCommandType!=wResultString.Substring(15,4))
            return false;
         if(!checkFcs(wResultString))
            return false;
        return true;
      }
      /// <summary>
      /// codeFcs校验和
      /// </summary>
      /// <param name="wData"></param>
      /// <returns></returns>
      private string codeFcs(string wData)
      {
        byte[] byteData=System.Text.Encoding.ASCII.GetBytes(wData);
        byte byteTemp=byteData[0];
        for (int i = 1; i < byteData.Length; i++)
			  byteTemp ^=byteData[i]; 
		  return byteTemp.ToString("X2");  
      }
      /// <summary>
      /// 检查FCS校验和
      /// </summary>
      /// <param name="wResultString"></param>
      /// <returns></returns>
      private bool checkFcs(string wResultString)
      {
        int _LastCount=4;
        string _temp=wResultString.Substring(0,wResultString.Length -_LastCount);  
        string _Fcs=codeFcs(_temp);
        if(_Fcs!=wResultString.Substring(wResultString.Length-_LastCount,2))
           return false;
         return true;
      }
      #endregion

   }
}
