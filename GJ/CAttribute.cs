using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ
{
    /// <summary>
    /// 作者特性定义
    /// </summary>
    [AttributeUsage(AttributeTargets.All,AllowMultiple=true,Inherited =false)]
    public class Author:System.Attribute
    {
        public Author(string name, string version, string verDate,string context="")
        {
            this.name = name;
            this.version = version;
            this.verDate = verDate;
            this.context = context;
        }
        private string name;
        private string version;
        private string verDate;
        private string context;
        public string mName
        {
            get { return name; }
        }
        public string mVersion
        {
            get { return mVersion; }
        }
        public string mVerDate
        {
            get { return verDate; }
        }
        public string mContext
        {
            get { return context; }
        }
    }

}
