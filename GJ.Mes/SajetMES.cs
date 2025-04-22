using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data;
using System.Runtime.InteropServices;

namespace GJ.Mes
{
    public class SajetMES
    {

        #region "读写锁"
        private ReaderWriterLock wrLock = new ReaderWriterLock();
        #endregion

        #region "Sajet"

        [DllImport("SajetConnect.dll", CharSet = CharSet.Ansi)]
        static extern bool SajetTransData(int f_iCommandNo, ref string f_pData, ref int f_pLen);

        [DllImport("SajetConnect.dll", CharSet = CharSet.Unicode)]
        static extern bool SajetTransData(int f_iCommandNo, byte[] f_pData, ref int f_pLen);


        //[DllImport("SajetConnect.dll", CharSet = CharSet.Unicode)]
        [DllImport("SajetConnect.dll",CharSet=CharSet.Unicode)]
        static extern bool SajetTransStart();


        [DllImport("SajetConnect.dll", CharSet = CharSet.Unicode)]
        static extern bool SajetTransClose();
        #endregion


        /// <summary>
        /// 开启连接
        /// </summary>
        public bool   StartConnect(int aCount)
        {
            try
            {

                for (int i = 0; i < aCount; i++)
                {
                    bool  rint = SajetTransStart();
                    if (rint == true )
                        return true;
                    else
                        return false;

                }

                return false;
            }

            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public bool StopConnect()
        {
            try
            {
                return SajetTransClose();
            }
            catch (Exception )
            {
                return false;
            }
        }

        /// <summary>
        /// 发送数据给Sajet
        /// </summary>
        /// <param name="aCode"></param>
        /// <param name="aCmd"></param>
        /// <param name="aRetString"></param>
        /// <param name="aTimeOut"></param>
        /// <param name="aTimes"></param>
        /// <returns></returns>
        public bool SendSajetCmd(int aCode, string aCmd, ref string aRetString, int aTimes = 1)
        {

            try
            {
                wrLock.AcquireWriterLock(-1);
                    

            bool  ret =false ;
            int Len=0;

            byte[] SendByte = null;
            int SendLen  = aCmd.Length;
            string recString  = String.Empty;
                
            aCmd = aCmd + new String(System.Convert .ToChar (" "), 512);
           
            SendByte = System.Text.Encoding.ASCII.GetBytes(aCmd);
            Len = SendByte.Length;
          
            for (int countLoop =0 ;countLoop <aTimes ;countLoop ++)
            {
                recString = String.Empty;

                ret = SajetTransData(aCode, SendByte , ref Len);
                if (!GetString(SendByte, ref recString))
                    return false;
                aRetString = recString.Substring (0, Len);
                int RecLen  = Len;

                if (SendLen != RecLen ) // '如果发送字符长度不等于接收到字符的长度，则认为发送成功；因为发送成功以后，会更改返回的字符串
                   return true;
                else
                {
                    System.Threading.Thread.Sleep(200);
                    continue;
                }
                
            }
                return true;
            }

            catch (Exception ex)
            {
                aRetString = ex.ToString();
                return false;
            }


            finally
            {
                wrLock.ReleaseWriterLock();
            }
        }

        public bool GetString(byte[] mByte, ref string mRecString)
        {
            try
            {
                System .Text .ASCIIEncoding ASCII= new System.Text .ASCIIEncoding ();
                //for (int i = 0; i < mByte.Length; i++)
                //{
                    mRecString  = mRecString + ASCII .GetString (mByte);
               // }
                return true;
            }

            catch (Exception ex)
            {
                mRecString = ex.ToString();
                return false;
            }

        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPassWord"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool Login(string UserName, string UserPassWord, ref string er)
        {
            try
            {
                string SendString =string.Empty ;
                string RecString =string.Empty ;
                SendString =UserName +";" +UserPassWord  + ";";
                int mCmdNo = 1;
                if (!SendSajetCmd(mCmdNo , SendString, ref RecString, 3))
                {
                    er = RecString;
                    return false;
                }
                if (string.IsNullOrEmpty(RecString )) return false ;

                if (RecString.Substring(0, 2) == "OK")
                    return true;
                er = RecString;
                return false;

            }

            catch (Exception ex)
            {
                er = ex.ToString();
                return false;

            }

        }
        /// <summary>
        /// 检查条码
        /// </summary>
        /// <param name="mSN"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool Chk_SN(string mSN,ref string mModeName,ref string mStation, ref string er)
        {

            try
            {
                string SendString = string.Empty;
                string RecString = string.Empty;
                SendString = mSN  + ";";
                int mCmdNo = 2;
                if (!SendSajetCmd(mCmdNo, SendString, ref RecString, 3))
                {
                    er = RecString;
                    return false;
                }
                if (string.IsNullOrEmpty(RecString)) return false;

                if (RecString.Substring(0, 2) == "OK")
                {
                    string[] ValList =null;
                    ValList = RecString.Split('');
                    if (ValList.Length < 6)
                        return false;
                    mModeName = ValList[5];
                    mStation = ValList[6];
                    return true;
                }

                er = RecString;
                return false;

            }

            catch (Exception ex)
            {
                er = ex.ToString();
                return false;

            }

        }
        /// <summary>
        /// 上传条码
        /// </summary>
        /// <param name="mSN"></param>
        /// <param name="mResult"></param>
        /// <param name="er"></param>
        /// <returns></returns>

        public bool Tran_SN(string mSN, string mResult, ref string er)
        {
            try
            {
                string SendString = string.Empty;
                string RecString = string.Empty;
                SendString = mSN + ";" + DateTime .Now .ToString ("yyyy/MM/dd HH/mm/ss") + ";" + mResult +";";
               
                int mCmdNo = 3;
                if (!SendSajetCmd(mCmdNo, SendString, ref RecString, 3))
                {
                    er = RecString;
                    return false;
                }
                if (string.IsNullOrEmpty(RecString)) return false;

                if (RecString.Substring(0, 2) == "OK")
                {

                    return true;

                }
                er = RecString;
                return false;

            }

            catch (Exception ex)
            {
                er = ex.ToString();
                return false;

            }
        }

        /// <summary>
        /// 读取内条码
        /// </summary>
        /// <param name="mSN"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool Read_internalSN(string mSN, ref string internalSN,  ref string er)
        {

            try
            {
                string SendString = string.Empty;
                string RecString = string.Empty;
                SendString = mSN + ";";
                int mCmdNo = 2;
                if (!SendSajetCmd(mCmdNo, SendString, ref RecString, 3))
                {
                    er = RecString;
                    return false;
                }
                if (string.IsNullOrEmpty(RecString)) return false;

                if (RecString.Substring(0, 2) == "OK")
                {
                    string[] ValList = null;
                    ValList = RecString.Split('');
                    if (ValList.Length < 6)
                        return false;
                    internalSN = ValList[3];
                  
                    return true;
                }

                er = RecString;
                return false;

            }

            catch (Exception ex)
            {
                er = ex.ToString();
                return false;

            }

        }
    }

}
