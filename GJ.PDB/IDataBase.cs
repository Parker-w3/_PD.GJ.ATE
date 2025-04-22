using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 

namespace GJ.PDB
{
   public interface IDataBase
   {
      #region 属性
      /// <summary>
      /// 数据库实例或路径
      /// </summary>
      string mServerName
      { set; }
      /// <summary>
      /// 数据库名称
      /// </summary>
      string mDbName
      { set; }
      /// <summary>
      /// 登陆用户
      /// </summary>
      string mUserName
      { set; }
      /// <summary>
      /// 登陆密码
      /// </summary>
      string mPassWord
      { set; } 
      #endregion

      #region 方法
      /// <summary>
      /// 连接数据库
      /// </summary>
      /// <returns></returns>
      bool connect();
      /// <summary>
      /// 关闭数据库
      /// </summary>
      void close();
      /// <summary>
      /// 查询语句
      /// </summary>
      /// <param name="sqlCmd"></param>
      /// <param name="ds"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool queryCmd(string sqlCmd, ref DataSet ds, ref string er);
      /// <summary>
      /// 执行语句
      /// </summary>
      /// <param name="sqlCmd"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool excuteCmd(string sqlCmd, ref string er);
      /// <summary>
      /// 执行多条语句
      /// </summary>
      /// <param name="sqlCmd"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool excuteArrayCmd(List<string> sqlCmd, ref string er);
      /// <summary>
      /// 修改整个表单
      /// </summary>
      /// <param name="sqlCmd"></param>
      /// <param name="dt"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      bool updateTableCmd(string sqlCmd, DataTable dt, ref string er);
      /// <summary>
      /// 获取数据所有表单名
      /// </summary>
      /// <param name="tableNames"></param>
      /// <param name="ex"></param>
      /// <returns></returns>
      bool returnTableNames(ref string[] tableNames, ref string er);

      #endregion
   }
}
