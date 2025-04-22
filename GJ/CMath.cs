using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ
{
   public class CMath
   {
      /// <summary>
      /// CRC16校验算法,低字节在前，高字节在后
      /// </summary>
      /// <param name="data">要校验的数组</param>
      /// <returns>返回校验结果，低字节在前，高字节在后</returns>
      public static string crc16(int[] data)
      {
         if (data.Length == 0)
            throw new Exception("调用CRC16校验算法,（低字节在前，高字节在后）时发生异常，异常信息：被校验的数组长度为0。");
         int[] temdata = new int[data.Length + 2];
         int xda, xdapoly;
         int i, j, xdabit;
         xda = 0xFFFF;
         xdapoly = 0xA001;
         for (i = 0; i < data.Length; i++)
         {
            xda ^= data[i];
            for (j = 0; j < 8; j++)
            {
               xdabit = (int)(xda & 0x01);
               xda >>= 1;
               if (xdabit == 1)
                  xda ^= xdapoly;
            }
         }
         temdata = new int[2] { (int)(xda & 0xFF), (int)(xda >> 8) };
         string crc = temdata[0].ToString("X2") + temdata[1].ToString("X2");
         return crc;
      }
      /// <summary>
      /// CRC16校验算法,低字节在前，高字节在后
      /// </summary>
      /// <param name="data">要校验的数组</param>
      /// <returns>返回校验结果，低字节在前，高字节在后</returns>
      public static string crc16(string strHex)
      {
         int count = strHex.Length / 2;
         int[] data = new int[count];
         for (int z = 0; z < count; z++)
         {
            data[z] = System.Convert.ToInt32(strHex.Substring(z * 2, 2), 16);
         }
         if (data.Length == 0)
            throw new Exception("调用CRC16校验算法,（低字节在前，高字节在后）时发生异常，异常信息：被校验的数组长度为0。");
         int[] temdata = new int[data.Length + 2];
         int xda, xdapoly;
         int i, j, xdabit;
         xda = 0xFFFF;
         xdapoly = 0xA001;
         for (i = 0; i < data.Length; i++)
         {
            xda ^= data[i];
            for (j = 0; j < 8; j++)
            {
               xdabit = (int)(xda & 0x01);
               xda >>= 1;
               if (xdabit == 1)
                  xda ^= xdapoly;
            }
         }
         temdata = new int[2] { (int)(xda & 0xFF), (int)(xda >> 8) };
         string crc =temdata[0].ToString("X2") + temdata[1].ToString("X2");
         return crc;
      }

      /// <summary>
      /// 计算时间差
      /// </summary>
      /// <param name="startTime"></param>
      /// <param name="endTime"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      public static int DifTime(string startTime, string endTime="")
      {
         try
         {
             if (endTime == "")
                 endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); 
            TimeSpan s1 = new TimeSpan(System.Convert.ToDateTime(startTime).Ticks);
            TimeSpan s2 = new TimeSpan(System.Convert.ToDateTime(endTime).Ticks);
            TimeSpan s = s2.Subtract(s1);
            return (int)s.TotalSeconds;
         }
         catch (Exception)
         {
            return 0;
         }
      }
      /// <summary>
      /// 延时
      /// </summary>
      /// <param name="msTimes"></param>
      public static void delayMs(int msTimes)
      {
          System.Threading.Thread.Sleep(msTimes);
      }
      /// <summary>
      /// 延时
      /// </summary>
      /// <param name="msTimes"></param>
      public static void WaitMs(int msTimes)
      {
          int nowTimes = System.Environment.TickCount;

          do
          {
              System.Windows.Forms.Application.DoEvents();

          } while (System.Environment.TickCount - nowTimes < msTimes);
      }

   }
}
