using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace GJ.Para.Base
{
    [Serializable]
    public class CModelBase
    {
        public string model;
        public string custom;
        public string version;

        public string releaseName;
        public string releaseDate;
    }
    /// <summary>
    /// 机种参数加载和保存
    /// </summary>
    public class CModelSet<T> where T : CModelBase
    {

        #region 方法
        /// <summary>
        /// 加载机种参数
        /// </summary>
        /// <returns></returns>
        public static bool load(string fileName, ref T mVal)
        {
            try
            {
                if (!File.Exists(fileName))
                    return false;
                CSerializable<T>.Read(fileName, ref mVal);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存机种参数
        /// </summary>
        /// <returns></returns>
        public static bool save(string fileName,T mVal)
        {
            try
            {
                CSerializable<T>.Write(fileName, mVal);
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
