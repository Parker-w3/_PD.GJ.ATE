using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ;
using GJ.Dev.COM;

namespace GJ.Dev.VoltMeter
{
   [Author("pf.xu","V1.0.0","2019/06/29")]
    public class ZH40063
   {
       #region 构造函数
       public ZH40063()
       {
           com = new CSerialPort(); 
       }
	   #endregion

       #region 字段

       private ICom com=null;

       private int idNo = 0;

       private string name = "ZH40063";

       private int plcNo = 1;

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
               com.mComDataType = 0;
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
       /// <param name="acv"></param>
       /// <param name="er"></param>
       /// <returns></returns>
       public bool readACV(ref double[] acv, ref string er)
       {
           try
           {
               int regAddr = 0;

               string rData=string.Empty;

               if (!read(regAddr, 4, ref rData, ref er))
                   return false;
               for (int i = 0; i < acv.Length; i++)
               {
                   int rVal = System.Convert.ToInt32(rData.Substring (i*4,4), 16);

                   acv [i]= ((double)rVal) / 25;

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
       /// <param name="acv"></param>
       /// <param name="er"></param>
       /// <returns></returns>
       public bool readACI(ref double aci, ref string er)
       {
           try
           {
               int regAddr = 0x10;

               string rData = string.Empty;

               if (!read(regAddr, 1, ref rData, ref er))
                   return false;

               int rVal = System.Convert.ToInt16(rData, 16);

               aci = ((double)rVal) / 10;

               return true;
           }
           catch (Exception ex)
           {
               er = ex.ToString();
               return false;
           }
       }
       #endregion

       #region ModBus-RTU通信协议
       /// <summary>
       /// 读寄存器值
       /// 从机地址(1Byte)+功能码(1Byte)+寄存器地址(2Byte)+地址数量(2Byte)+CRC检验(2Byte)
       /// </summary>
       /// <param name="devType">地址类型</param>
       /// <param name="startAddr">开始地址</param>
       /// <param name="N">地址长度</param>
       /// <param name="rData">16进制字符:数据值高位在前,低位在后</param>
       /// <param name="er"></param>
       /// <returns></returns>
       public bool read(int startAddr, int N, ref string rData, ref string er)
       {
           try
           {
               string wCmd = plcNo.ToString("X2");
               int rLen = 0;
               wCmd += "03";      //寄存器功能码为03
               rLen = N * 2;
               wCmd += formatDevAddr(startAddr);  //开始地址
               wCmd += N.ToString("X4");                   //读地址长度
               wCmd += CMath.crc16(wCmd);                  //CRC16 低位前,高位后            
               if (!com.send(wCmd, 5 + rLen, ref rData, ref er))
                   return false;
               if (!checkCRC(rData))
               {
                   er = "crc16检验和错误:" + rData;
                   return false;
               }
               string temp = rData.Substring(6, rLen * 2);               
                rData = temp;     //2个字节为寄存器值，高在前,低位在后，寄存器小排最前面；
                //转换为寄存器小排最后
                 
                return true;
           }
           catch (Exception e)
           {
               er = e.ToString();
               return false;
           }
       }
       /// <summary>
       /// 单写寄存器值
       /// 从机地址(1Byte)+功能码(1Byte)+寄存器地址(2Byte)+地址数量(2Byte)+字节数(1Byte)+数据+CRC检验(2Byte)
       /// </summary>
       /// <param name="devType">地址类型</param>
       /// <param name="startAddr">开始地址</param>
       /// <param name="wVal">单个值</param>
       /// <param name="er"></param>
       /// <returns></returns>
       public bool write(int startAddr, int wVal, ref string er)
       {
           try
           {
               int N = 1;   //单写1个值
               string wCmd = plcNo.ToString("X2");
               int rLen = 0;
               int wLen = 0;
               string wData = string.Empty;
               wCmd += "10";        //寄存器功能码为16
               wLen = N * 2;          //写入字节数
               rLen = 8;           //回读长度
               wData = wVal.ToString("X" + wLen * 2);               
               wCmd += formatDevAddr(startAddr);  //开始地址
               wCmd += N.ToString("X4");         //读地址长度
               wCmd += wLen.ToString("X2");     //写入字节数  
               wCmd += wData;                   //写入数据
               wCmd += CMath.crc16(wCmd);      //CRC16 低位前,高位后   
               string rData = string.Empty;
               if (!com.send(wCmd, rLen, ref rData, ref er))
                   return false;
               if (!checkCRC(rData))
               {
                   er = "crc16检验和错误:" + rData;
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
       /// 格式化地址段
       /// </summary>     
       private string formatDevAddr(int devAddr)
       {
           return devAddr.ToString("X4");
       }
       /// <summary>
       /// 检查CRC
       /// </summary>
       /// <param name="wCmd"></param>
       /// <returns></returns>
       private bool checkCRC(string wCmd)
       {
           string crc = CMath.crc16(wCmd.Substring(0, wCmd.Length - 4));
           if (crc != wCmd.Substring(wCmd.Length - 4, 4))
               return false;
           return true;
       }

       #endregion

   }
}
