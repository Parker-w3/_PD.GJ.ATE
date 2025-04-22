using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GJ
{
    public class CFile
    {
        /// <summary>
        /// 删除过期文件
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileExtend"></param>
        /// <param name="dayLimit"></param>
        /// <returns></returns>
        public static bool deleteFileDateOut(string folder, string fileExtend, int dayLimit)
        {
            try
            {
                if (!Directory.Exists(folder))
                    return false;
                string[] fileName;
                fileName = Directory.GetFiles(folder, fileExtend);
                for (int i = 0; i < fileName.Length; i++)
                {
                    DateTime dt = File.GetCreationTime(fileName[i]);
                    TimeSpan t = DateTime.Now.Subtract(dt);
                    if (t.Days > dayLimit)
                        File.Delete(fileName[i]);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
