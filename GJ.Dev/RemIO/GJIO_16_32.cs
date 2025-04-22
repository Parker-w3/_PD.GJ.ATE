using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
namespace GJ.Dev.RemIO
{
    public class GJIO_16_32 : IIO
    {
        #region 构造函数
        public GJIO_16_32()
        {
            com = new COM.CSerialPort();
            com.mComDataType = 0;
        }
        #endregion

        #region 字段
        private string name = "GJIO_16_32";
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
        public COM.ICom mCom
        {
            get { return com; }
            set { com = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comName"></param>
        /// <param name="er"></param>
        /// <param name="setting">波特率，默认9600,n,8,1</param>
        /// <returns></returns>
        public bool open(string comName, ref string er, string setting)
        {
            if (com == null)
                return false;
            return com.open(comName, ref er, setting);
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void close()
        {
            if (com == null)
                return;
            com.close();
        }


        /// <summary>
        /// 设置地址
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setAddr(int wAddr, ref string er)
        {
            string ADDR = "00";
            string CMD = "01";
            string INFO = wAddr.ToString("X4");
            string LENGTH = (INFO.Length / 4).ToString("X2");
            int RTNLEN = 6 + 0 * 2;
            string cmd = ADDR + LENGTH + CMD + INFO;
            cmd += CalCheckSum(cmd);
            cmd = SOI + cmd + EOI;
            string rData = string.Empty;
            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                return false;
            return true;
        }
        /// <summary>
        /// 读地址
        /// </summary>
        /// <param name="curAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readAddr(ref int curAddr, ref string er)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 读错误码
        /// </summary>
        /// <param name="rVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readErrCode(int curAddr, ref int rVal, ref string er)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 读波特率
        /// </summary>
        /// <param name="baud"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readBaud(int curAddr, ref int baud, ref string er)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="baud"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool setBaud(int curAddr, int baud, ref string er)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ReadModule(int wAddr, ref string rDevName, ref string er)
        {
            string ADDR = wAddr.ToString("X2");
            string CMD = "07";
            string INFO = string.Empty;
            string LENGTH = (INFO.Length / 4).ToString("X2");
            int RTNLEN = 6 + 2 * 29;
            string cmd = ADDR + LENGTH + CMD + INFO;
            cmd += CalCheckSum(cmd);
            cmd = SOI + cmd + EOI;
            string rData = string.Empty;
            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                return false;
            int[] byteW = new int[rData.Length / 4];
            for (int i = 0; i < rData.Length / 4; i++)
            {
                byteW[i] = Convert.ToInt32(rData.Substring(i * 4, 4), 16);
               // rDevName += Microsoft.VisualBasic.Strings.ChrW(byteW[i]);
                rDevName += byteW[i].ToString();
            }
            return true;
        }
        /// <summary>
        /// 读取版本
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="version"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readVersion(int wAddr, ref int version, ref string er)
        {
            string ADDR = wAddr.ToString("X2");
            string CMD = "06";
            string INFO = string.Empty;
            string LENGTH = (INFO.Length / 4).ToString("X2");
            int RTNLEN = 6 + 1 * 2;
            string cmd = ADDR + LENGTH + CMD + INFO;
            cmd += CalCheckSum(cmd);
            cmd = SOI + cmd + EOI;
            string rData = string.Empty;
            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                return false;
            version = (Int16)Convert.ToInt32(rData, 16);
            return true;
        }

        /// <summary>
        /// 读取电压
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="version"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool readVolt(int wAddr, ref double[]  Volt, ref string er)
        {
            string ADDR = wAddr.ToString("X2");
            string CMD = "19";
            string INFO = string.Empty;
            string LENGTH = (INFO.Length / 4).ToString("X2");
            int RTNLEN = 6 + 2 * 2;
            string cmd = ADDR + LENGTH + CMD + INFO;
            cmd += CalCheckSum(cmd);
            cmd = SOI + cmd + EOI;
            string rData = string.Empty;
            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                return false;
            Volt[1] = (double )Convert.ToInt32 (rData.Substring(0,4), 16)/100;
            Volt[0] = (double)Convert.ToInt32(rData.Substring(4, 4), 16) / 100;
            return true;
        }
        /// <summary>
        /// 读取输入输出点的状态
        /// </summary>
        /// <param name="devAddr">控制板地址</param>
        /// <param name="coilType">点状态</param>
        /// <param name="startAddr">起始位</param>
        /// <param name="rVal">数据</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool read(int devAddr, ECoilType coilType, int startAddr, ref int[] rVal, ref string er)
        {
            string ADDR = devAddr.ToString("X2");

            switch (coilType)
            {
                case ECoilType.Y:               //读取32个点的输出信号
                    {
                        string CMD = "14";
                        string INFO = string.Empty;
                        string LENGTH = (INFO.Length / 4).ToString("X2");
                        int RTNLEN = 6 + 3 * 2;
                        string cmd = ADDR + LENGTH + CMD + INFO;
                        cmd += CalCheckSum(cmd);
                        cmd = SOI + cmd + EOI;
                        string rData = string.Empty;
                        if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                            return false;
                        int word1 = System.Convert.ToInt32(rData.Substring(0, 4), 16);
                        int word2 = System.Convert.ToInt32(rData.Substring(4, 4), 16);
                        for (int i = 0; i < 16; i++)
                        {
                            if ((word1 & (1 << i)) == (1 << i))
                                rVal[i + 16] = 0;
                            else
                                rVal[i + 16] = 1;
                            if ((word2 & (1 << i)) == (1 << i))
                                rVal[i] = 0;
                            else
                                rVal[i] = 1;
                        }                        
                    }
                    return true;

                case ECoilType.A:              //读取AC输入电压换算除100
                    {
                        string CMD = "39";
                        string INFO = string.Empty;
                        string LENGTH = (INFO.Length / 4).ToString("X2");
                        int RTNLEN = 6 + 3 * 2;
                        string cmd = ADDR + LENGTH + CMD + INFO;
                        cmd += CalCheckSum(cmd);
                        cmd = SOI + cmd + EOI;
                        string rData = string.Empty;
                        if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                            return false;
                        for (int i = 0; i < rVal.Length; i++)
                        {
                            rVal[i] = System.Convert.ToInt32(rData.Substring((i + 1) * 4, 4), 16);

                        }                        
                    }
                    return true;
                case ECoilType.X :              //读取16个点的输入信号
                    {
                        string CMD = "15";
                        string INFO = string.Empty;
                        string LENGTH = (INFO.Length / 4).ToString("X2");
                        int RTNLEN = 6 + 2 * 2;
                        string cmd = ADDR + LENGTH + CMD + INFO;
                        cmd += CalCheckSum(cmd);
                        cmd = SOI + cmd + EOI;
                        string rData = string.Empty;
                        if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                            return false;
                        int word1 = System.Convert.ToInt32(rData.Substring(0, 4), 16);
                        for (int i = 0; i < rVal.Length; i++)
                        {
                            if ((word1 & (1 << i)) == (1 << i))
                                rVal[i] = 0;
                            else
                                rVal[i] = 1;
                        }                        
                    }
                    return true;
                case ECoilType.D:               //读取温度换算除100
                    {

                        string CMD = "30";
                        string INFO = string.Empty;
                        string LENGTH = (INFO.Length / 4).ToString("X2");
                        int RTNLEN = 6 + 11 * 2;
                        string cmd = ADDR + LENGTH + CMD + INFO;
                        cmd += CalCheckSum(cmd);
                        cmd = SOI + cmd + EOI;
                        string rData = string.Empty;
                        if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                            return false;
                        for (int i = 0; i < rVal.Length; i++)
                        {
                            rVal[i]= System.Convert.ToInt32(rData.Substring(i * 4, 4), 16);
                        }

                    }
                    return true;
                case ECoilType.T :               //读取设置温度换算除100
                    {
                        string CMD = "36";
                        string INFO = string.Empty;
                        string LENGTH = (INFO.Length / 4).ToString("X2");
                        int RTNLEN = 6 + 8 * 2;
                        string cmd = ADDR + LENGTH + CMD + INFO;
                        cmd += CalCheckSum(cmd);
                        cmd = SOI + cmd + EOI;
                        string rData = string.Empty;
                        if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                            return false;
                        for (int i = 0; i < rVal.Length; i++)
                        {
                            rVal[i] = System.Convert.ToInt32(rData.Substring(i * 4, 4), 16);
                        }
                    }
                    return true;
                case ECoilType.L:               //读取负载区温度设置换算除100
                    {
                        string CMD = "38";
                        string INFO = string.Empty;
                        string LENGTH = (INFO.Length / 4).ToString("X2");
                        int RTNLEN = 6 + 2 * 2;
                        string cmd = ADDR + LENGTH + CMD + INFO;
                        cmd += CalCheckSum(cmd);
                        cmd = SOI + cmd + EOI;
                        string rData = string.Empty;
                        if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                            return false;
                        for (int i = 0; i < rVal.Length; i++)
                        {
                            rVal[i] = System.Convert.ToInt32(rData.Substring(i * 4, 4), 16);
                        }
                    }
                    return true;
                case ECoilType.U :               //读温度使能点
                    {
                        string CMD = "34";
                        string INFO = string.Empty;
                        string LENGTH = (INFO.Length / 4).ToString("X2");
                        int RTNLEN = 6 + 10 * 2;
                        string cmd = ADDR + LENGTH + CMD + INFO;
                        cmd += CalCheckSum(cmd);
                        cmd = SOI + cmd + EOI;
                        string rData = string.Empty;
                        if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                            return false;
                        for (int i = 0; i < rVal.Length; i++)
                        {
                            int word = System.Convert.ToInt32(rData.Substring(i * 4, 4), 16);
                            if (word != 0)
                                rVal[word - 1] = 1;
                        }
                    }
                    return true;
                case ECoilType.B:               //读温度点补偿值
                    {
                        string CMD = "32";
                        string INFO = string.Empty;
                        string LENGTH = (INFO.Length / 4).ToString("X2");
                        int RTNLEN = 6 + 10 * 2;
                        string cmd = ADDR + LENGTH + CMD + INFO;
                        cmd += CalCheckSum(cmd);
                        cmd = SOI + cmd + EOI;
                        string rData = string.Empty;
                        if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                            return false;
                        for (int i = 0; i < rVal.Length; i++)
                        {
                            rVal [i]= System.Convert.ToInt32(rData.Substring(i * 4, 4), 16);
                        }
                    }
                    return true;

                default:
                    return false;
            }

        }
        /// <summary>
        /// 读取输入输出点的状态(x,y)
        /// </summary>
        /// <param name="devAddr">控制板地址</param>
        /// <param name="coilType">点状态</param>
        /// <param name="startAddr">起始位</param>
        /// <param name="rVal">数据</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool read(int devAddr, ECoilType coilType, int startAddr, ref int rVal, ref string er)
        {
            string ADDR = devAddr.ToString("X2");
            if (coilType != ECoilType.X)
            {
                string CMD = "14";
                string INFO = string.Empty;
                string LENGTH = (INFO.Length / 4).ToString("X2");
                int RTNLEN = 6 + 3 * 2;
                string cmd = ADDR + LENGTH + CMD + INFO;
                cmd += CalCheckSum(cmd);
                cmd = SOI + cmd + EOI;
                string rData = string.Empty;
                if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                    return false;
                int word1 = System.Convert.ToInt32(rData.Substring(0, 4), 16);
                int word2 = System.Convert.ToInt32(rData.Substring(4, 4), 16);
                for (int i = 0; i < 16; i++)
                {
                    if ((word1 & (1 << i)) == (1 << i))
                    {
                        if (i + 16 == startAddr)
                            rVal = 0;
                    }
                    else
                    {
                        if (i + 16 == startAddr)
                            rVal = 1;
                    }
                    if ((word2 & (1 << i)) == (1 << i))
                    {
                        if (i == startAddr)
                            rVal = 0;
                    }
                    else
                    {
                        if (i == startAddr)
                            rVal = 1;
                    }
                }
                return true;
            }
            else
            {
                string CMD = "15";
                string INFO = string.Empty;
                string LENGTH = (INFO.Length / 4).ToString("X2");
                int RTNLEN = 6 + 2 * 2;
                string cmd = ADDR + LENGTH + CMD + INFO;
                cmd += CalCheckSum(cmd);
                cmd = SOI + cmd + EOI;
                string rData = string.Empty;
                if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                    return false;
                int word1 = System.Convert.ToInt32(rData.Substring(0, 4), 16);
                for (int i = 0; i < 16; i++)
                {
                    if ((word1 & (1 << i)) == (1 << i))
                    {
                        if (i == startAddr)
                            rVal = 0;
                    }
                    else
                    {
                        if (i == startAddr)
                            rVal = 1;
                    }
                }
                return true;
            }

        }
        /// <summary>
        /// 读取返回数据(x,y)
        /// </summary>
        /// <param name="devAddr">IO板地址</param>
        /// <param name="coilType">读取类型</param>
        /// <param name="startAddr">开始地址</param>
        /// <param name="N">长度</param>
        /// <param name="rData"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool read(int devAddr, ECoilType coilType, int startAddr, int N, ref string rData, ref string er)
        {
            string ADDR = devAddr.ToString("X2");
            if (coilType != ECoilType.X)
            {
                string CMD = "14";
                string INFO = string.Empty;
                string LENGTH = (INFO.Length / 4).ToString("X2");
                int RTNLEN = 6 + 3 * 2;
                string cmd = ADDR + LENGTH + CMD + INFO;
                cmd += CalCheckSum(cmd);
                cmd = SOI + cmd + EOI;
                if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                {
                    return false;  
                }
                return true;
            }
            else
            {
                string CMD = "15";
                string INFO = string.Empty;
                string LENGTH = (INFO.Length / 4).ToString("X2");
                int RTNLEN = 6 + 2 * 2;
                string cmd = ADDR + LENGTH + CMD + INFO;
                cmd += CalCheckSum(cmd);
                cmd = SOI + cmd + EOI;

                if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                {
                    return false;
                }
                return true;
            }

        }

        /// <summary>
        /// 单写线圈和寄存器值(X,Y)
        /// </summary>
        /// <param name="devType">IO板地址</param>
        /// <param name="startAddr">写入地址</param>
        /// <param name="wVal">写入值</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool write(int devAddr, ECoilType coilType, int startAddr, int wVal, ref string er)
        {
            try
            {
                switch (coilType)
                {
                    case ECoilType.Y :      //设定单个Y点输出
                        {
                            string ADDR = devAddr.ToString("X2");
                            string CMD = "13";
                            string INFO = string.Empty;

                            int coil = (1 << (startAddr - 1));
                            INFO = coil.ToString("X8");

                            if (wVal == 1)
                                INFO += "00000000";
                            else
                                INFO = "00000000" + INFO;
                            string LENGTH = (INFO.Length / 4).ToString("X2");
                            int RTNLEN = 6 + 0 * 2;
                            string cmd = ADDR + LENGTH + CMD + INFO;
                            cmd += CalCheckSum(cmd);
                            cmd = SOI + cmd + EOI;
                            string rData = string.Empty;
                            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                                return false;
                        }
                        return true;
                    case ECoilType.R:      //设置运行状态,startAddr运行状态，wVal运行时间
                        {
                            string ADDR = devAddr.ToString("X2");
                            string CMD = "18";
                            string INFO = ((int)startAddr).ToString("X4");
                            if (startAddr == (int)ERunMode.重计时运行)
                                INFO += wVal.ToString("X8");
                            string LENGTH = (INFO.Length / 4).ToString("X2");
                            int RTNLEN = 6 + 5 * 2;
                            string cmd = ADDR + LENGTH + CMD + INFO;
                            cmd += CalCheckSum(cmd);
                            cmd = SOI + cmd + EOI;
                            string rData = string.Empty;
                            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                                return false;
                        }
                        return true;
                    default:
                        return false;

                }
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
        }

        /// <summary>
        /// 单写线圈和寄存器值(X,Y)
        /// </summary>
        /// <param name="devType">IO板地址</param>
        /// <param name="startAddr">写入地址</param>
        /// <param name="wVal">写入值</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool writeY(int devAddr, ECoilType coilType, int startAddr, int[] wVal,bool wOpen , ref string er)
        {
            try
            {
                switch (coilType)
                {
                    case ECoilType.Y:      //设定单个Y点输出
                        {
                            string ADDR = devAddr.ToString("X2");
                            string CMD = "13";
                            string INFO = string.Empty;

                            int coil = 0;
                            for (int i = 0; i < wVal.Length; i++)
                            {
                                if(wVal[i]==1)
                                   coil += (1 << (startAddr+i - 1));
                            }
                            INFO = coil.ToString("X8");

                            if (wOpen == true )
                                INFO += "00000000";
                            else
                                INFO = "00000000" + INFO ;
                            string LENGTH = (INFO.Length / 4).ToString("X2");
                            int RTNLEN = 6 + 0 * 2;
                            string cmd = ADDR + LENGTH + CMD + INFO;
                            cmd += CalCheckSum(cmd);
                            cmd = SOI + cmd + EOI;
                            string rData = string.Empty;
                            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                                return false;
                        }
                        return true;
                   
                       
                    default:
                        return false;

                }
            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
        }

        /// <summary>
        /// 写多个线圈和寄存器(温度值)
        /// </summary>
        /// <param name="devType">IO板地址</param>
        /// <param name="startAddr">写入地址</param>
        /// <param name="wVal">多个值</param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool write(int devAddr, ECoilType coilType, int startAddr, int[] wVal, ref string er)
        {
            try
            {
                switch (coilType)
                {
                    case ECoilType.D:      //温度设定值，上限偏差，下限偏差，超温上限，开启排风温度点，停止排风温度点，单点过温保护温度点，设定低温不计时温度点
                        {
                            string ADDR = devAddr.ToString("X2");
                            string CMD = "35";
                            string INFO = string.Empty;
                            for (int i = 0; i < wVal.Length; i++)
                                INFO += ((int)wVal[i] * 100).ToString("X4");
                            string LENGTH = (INFO.Length / 4).ToString("X2");
                            int RTNLEN = 6 + 0 * 2;
                            string cmd = ADDR + LENGTH + CMD + INFO;
                            cmd += CalCheckSum(cmd);
                            cmd = SOI + cmd + EOI;
                            string rData = string.Empty;
                            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                                return false;

                        }
                        return true;
                    case ECoilType.L :      //设置负载区温度,排风温度点,超温断电点
                        {
                            string ADDR = devAddr.ToString("X2");
                            string CMD = "37";
                            string INFO = string.Empty;
                            for (int i = 0; i < wVal.Length; i++)
                                INFO += ((int)wVal[i] * 100).ToString("X4");
                            string LENGTH = (INFO.Length / 4).ToString("X2");
                            int RTNLEN = 6 + 0 * 2;
                            string cmd = ADDR + LENGTH + CMD + INFO;
                            cmd += CalCheckSum(cmd);
                            cmd = SOI + cmd + EOI;
                            string rData = string.Empty;
                            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                                return false;

                        }
                        return true;
                    case ECoilType.U:      //设定温度点使用状态
                        {
                            string ADDR = devAddr.ToString("X2");
                            string CMD = "33";
                            string INFO = string.Empty;
                            for (int i = 0; i < wVal.Length; i++)
                            {
                                if (wVal[i] == 0)
                                    INFO += "0000";
                                else
                                    INFO += (i + 1).ToString("X4");
                            }
                            string LENGTH = (INFO.Length / 4).ToString("X2");
                            int RTNLEN = 6 + 0 * 2;
                            string cmd = ADDR + LENGTH + CMD + INFO;
                            cmd += CalCheckSum(cmd);
                            cmd = SOI + cmd + EOI;
                            string rData = string.Empty;
                            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                                return false;
                        }
                        return true;
                    case ECoilType.B:      //设置温度点补偿值
                        {
                            string ADDR = devAddr.ToString("X2");
                            string CMD = "32";
                            string INFO = string.Empty;
                            for (int i = 0; i < wVal.Length; i++)
                            {
                                INFO += (i + 1).ToString("X4");
                                INFO += ((int)(wVal[i] * 100 + 10000)).ToString("X4");
                            }
                            string LENGTH = (INFO.Length / 4).ToString("X2");
                            int RTNLEN = 6 + 10 * 2;
                            string cmd = ADDR + LENGTH + CMD + INFO;
                            cmd += CalCheckSum(cmd);
                            cmd = SOI + cmd + EOI;
                            string rData = string.Empty;
                            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                                return false;
                        }
                        return true;

                    default:
                        return false;
                }

            }
            catch (Exception e)
            {
                er = e.ToString();
                return false;
            }
        }

        /// <summary>
        /// 读控制板运行时间
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="status"></param>
        /// <param name="setTime"></param>
        /// <param name="runTime"></param>
        /// <returns></returns>
        public bool ReadRunTime(int wAddr, ref int status, ref int setTime, ref int runTime, ref string er)
        {
            string ADDR = wAddr.ToString("X2");
            string CMD = "18";
            string INFO = string.Empty;
            string LENGTH = (INFO.Length / 4).ToString("X2");
            int RTNLEN = 6 + 5 * 2;
            string cmd = ADDR + LENGTH + CMD + INFO;
            cmd += CalCheckSum(cmd);
            cmd = SOI + cmd + EOI;
            string rData = string.Empty;
            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                return false;
            status = System.Convert.ToInt32(rData.Substring(0, 4), 16);
            setTime = System.Convert.ToInt32(rData.Substring(4, 8), 16);
            runTime = System.Convert.ToInt32(rData.Substring(12, 8), 16);
            return true;

        }
        /// <summary>
        /// 设置CHAN 10段ON/OFF
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="chan"></param>
        /// <param name="wOn1"></param>
        /// <param name="wOff1"></param>
        /// <param name="wOn2"></param>
        /// <param name="wOnOff"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool SetOnOff(int wAddr, int chan, int[] wOn1, int[] wOff1, int[] wOn2, int[] wOnOff, ref string er)
        {
            string ADDR = wAddr.ToString("X2");
            string CMD = "20";
            string INFO = string.Empty;
            INFO += chan.ToString("X4");
            for (int i = 0; i < 10; i++)
            {
                INFO += wOn1[i].ToString("X4");
                INFO += wOff1[i].ToString("X4");
                INFO += wOn2[i].ToString("X4");
                INFO += wOnOff[i].ToString("X4");
            }
            string LENGTH = (INFO.Length / 4).ToString("X2");
            int RTNLEN = 6 + 0 * 2;
            string cmd = ADDR + LENGTH + CMD + INFO;
            cmd += CalCheckSum(cmd);
            cmd = SOI + cmd + EOI;
            string rData = string.Empty;
            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                return false;
            return true;
        }
        /// <summary>
        ///  读取CHAN 10段ON/OFF
        /// </summary>
        /// <param name="wAddr"></param>
        /// <param name="chan"></param>
        /// <param name="wOn1"></param>
        /// <param name="wOff1"></param>
        /// <param name="wOn2"></param>
        /// <param name="wOnOff"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public bool ReadOnOff(int wAddr, int chan, ref int[] wOn1, ref int[] wOff1, ref int[] wOn2, ref int[] wOnOff, ref string er)
        {
            string ADDR = wAddr.ToString("X2");
            string CMD = "21";
            string INFO = string.Empty;
            INFO += chan.ToString("X4");
            string LENGTH = (INFO.Length / 4).ToString("X2");
            int RTNLEN = 6 + 41 * 2;
            string cmd = ADDR + LENGTH + CMD + INFO;
            cmd += CalCheckSum(cmd);
            cmd = SOI + cmd + EOI;
            string rData = string.Empty;
            if (!SendCmdToCom(cmd, RTNLEN, ref rData, ref er))
                return false;
            int rChan = System.Convert.ToInt32(rData.Substring(0 * 4, 4), 16);
            for (int i = 0; i < wOn1.Length; i++)
            {
                wOn1[i] = System.Convert.ToInt32(rData.Substring((i * 16) + 4, 4), 16);
                wOff1[i] = System.Convert.ToInt32(rData.Substring((i * 16) + 8, 4), 16);
                wOn2[i] = System.Convert.ToInt32(rData.Substring((i * 16) + 12, 4), 16);
                wOnOff[i] = System.Convert.ToInt32(rData.Substring((i * 16) + 16, 4), 16);
            }
            return true;
        }


        #endregion

        #region 协议
        //SOI + ADDR + LENGTH + CMD + INFO + CHECKSUM + EOI
        private string SOI = "EE";
        private string EOI = "EF";
        private bool SendCmdToCom(string wData, int rLen, ref string rData, ref string er, int wTimeOut = 200)
        {
            string recvData = string.Empty;
            if (!com.send(wData, rLen, ref recvData, ref er, wTimeOut))
                return false;
            if (rLen > 0)
                if (!CheckSum(recvData, ref rData, ref er))
                    return false;
            
            return true;
        }
        private bool CheckSum(string wRecData, ref string recVal, ref string er)
        {
            if (wRecData.Length < 12)
            {
                er = "接收长度错误:" + wRecData;
                return false;
            }
            string cmd = wRecData.Substring(2, wRecData.Length - 6);
            string calCheckSum = CalCheckSum(cmd);
            if (calCheckSum != wRecData.Substring(wRecData.Length - 4, 2))
            {
                er = "校验和错误:" + wRecData;
                return false;
            }
            string rtn = wRecData.Substring(6, 2);
            switch (rtn)
            {
                case "F0":
                    break;
                case "F1":
                    er = "CHKSUM错误:" + wRecData;
                    return false;
                case "F2":
                    er = "无效数据:" + wRecData;
                    return false;
                case "F3":
                    er = "CMD无效:" + wRecData;
                    return false;
                case "F4":
                    er = "长度错误:" + wRecData;
                    return false;
                default:
                    er = "其他错误:" + wRecData;
                    return false;
            }
            int rLen = Convert.ToInt32(wRecData.Substring(4, 2), 16);
            if (rLen == 0)
                recVal = string.Empty;
            else
                recVal = wRecData.Substring(8, rLen * 4);
            return true;
        }
        private string CalCheckSum(string wData)
        {
            int sum = 0;
            for (int i = 0; i < wData.Length / 2; i++)
                sum += Convert.ToInt32(wData.Substring(i * 2, 2), 16);
            sum = ((0xFF00 - sum) & 0xFF);
            return sum.ToString("X2");
        }
        #endregion

    }
}
