using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GJ.Dev;
namespace GJ.Dev.IO
{
    public enum EIOType
    { 
       GJIO_I8_O14
    }
    public enum EX
    { 
      XOFF=0,
      XON=1
    }
    public enum EY
    { 
      YOFF=0,
      YON=1
    }
    public class CIOCom
    {
        #region 构造函数
        public CIOCom(EIOType IOType = EIOType.GJIO_I8_O14)
        {
            switch (IOType)
            {
                case EIOType.GJIO_I8_O14:
                    io = new CIO_I8_O14();
                    io.mCom = new COM.CSerialPort();
                    break;
                default:
                    break;
            }
        }
        #endregion
       
        #region 字段
        private IIO io = null;
        #endregion

        #region 方法
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comName"></param>
        /// <param name="setting"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool open(string comName, ref string er, string setting = "9600,n,8,1")
        {
            return io.open(comName,ref er,setting);
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public void close()
        {
            io.close();
        }
        /// <summary>
        /// 读取X1-X8
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="X"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ReadInSingal(int wAddr, ref List<EX> X, ref string er)
        {
            return io.ReadInSingal(wAddr, ref X, ref er); 
        }
        /// <summary>
        /// 控制K1--K14只能唯一ON iRly=0时 All Relay is OFF
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="relayNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool CtrlRelayByCmd1(int wAddr, int relayNo, ref string er)
        {
            return io.CtrlRelayByCmd1(wAddr, relayNo, ref er);  
        }
        /// <summary>
        /// 任意控制K1--K14 ON/OFF
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="relayNo"></param>
        /// <param name="onOff"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool CtrlRelayByCmd2(int wAddr, int relayNo, EY onOff, ref string er)
        {
            return io.CtrlRelayByCmd2(wAddr, relayNo, onOff, ref er);  
        }
        #endregion
    }
}
