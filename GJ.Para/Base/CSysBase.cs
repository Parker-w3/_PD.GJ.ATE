using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GJ;
namespace GJ.Para.Base
{
    [Serializable]
    public class CSysBase
    {
        /// <summary>
        /// 获取机种路径
        /// </summary>
        public string modelPath;
        /// <summary>
        /// 保存测试报表
        /// </summary>
        public bool saveReport;
        /// <summary>
        /// 保存报表间隔时间
        /// </summary>
        public int saveReportTimes;
        /// <summary>
        /// 测试报表路径
        /// </summary>
        public string reportPath;
    }
    /// <summary>
    /// 系统参数加载和保存
    /// </summary>
    public class CSysSet<T> where T:CSysBase
    {
        #region 字段
        private static string sysFile;
        #endregion

        #region 方法
        /// <summary>
        /// 加载系统参数
        /// </summary>
        /// <returns></returns>
        public static bool load(string fileName,ref T mVal)
        {
            try
            {
                sysFile = fileName;
                if (!File.Exists(fileName))
                    return false;
                CSerializable<T>.Read(sysFile, ref mVal);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存系统参数
        /// </summary>
        /// <returns></returns>
        public static bool save(T mVal)
        {
            try
            {
                CSerializable<T>.Write(sysFile, mVal);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
