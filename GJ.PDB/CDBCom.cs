using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace GJ.PDB
{
   public class CDBCom
   {
      #region 枚举
      public enum EDBType
      { 
       Access,
       MySQL,
       Oracle,
       SQLSever
      }
      #endregion

      #region 构造函数
      public CDBCom(EDBType type, string serverName = "localhost", string dbName = "SysConfig.accdb",
                    string userName = "GUANJIA", string password = "GuanJia")
      {
         switch (type)
         {
            case EDBType.Access:
               dbCon = new Access(); 
               break;
            case EDBType.MySQL:
               dbCon =new MySQL(); 
               break;
            case EDBType.Oracle:
               break;
            case EDBType.SQLSever:
               break;
            default:
               break;
         }
         if (dbCon != null)
         {
            dbCon.mServerName = serverName;
            dbCon.mDbName = dbName;
            dbCon.mUserName = userName;
            dbCon.mPassWord = password; 
         }
      }
	   #endregion 

      #region 字段
      private IDataBase dbCon = null;
      #endregion

      #region 方法
      /// <summary>
      /// 检查是否连接数据库
      /// </summary>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool chkExist(ref string er)
      {
         if (dbCon == null)
         {
            er = "数据对象未引用.";
            return false;
         }
         try
         {           
            if (!dbCon.connect())
               return false;
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
         finally
         {
            dbCon.close(); 
         }
      }
       /// <summary>
      /// 查询SQL语句
      /// </summary>
      /// <param name="sqlCmd"></param>
      /// <param name="ds"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool QuerySQL(string sqlCmd, ref DataSet ds, ref string er)
      {
         if (dbCon == null)
         {
            er = "数据对象未引用.";
            return false;
         }
         return dbCon.queryCmd(sqlCmd, ref ds, ref er); 
      }
      /// <summary>
      /// 执行单个SQL语句
      /// </summary>
      /// <param name="sqlCmd"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool excuteSQL(string sqlCmd, ref string er)
      {
         if (dbCon == null)
         {
            er = "数据对象未引用.";
            return false;
         }
         return dbCon.excuteCmd(sqlCmd, ref er); 
      }
      /// <summary>
      /// 执行多条SQL语句
      /// </summary>
      /// <param name="sqlCmdArray"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool excuteSQLArray(List<string> sqlCmdArray, ref string er)
      {
         if (dbCon == null)
         {
            er = "数据对象未引用.";
            return false;
         }
         return dbCon.excuteArrayCmd(sqlCmdArray, ref er); 
      }
      /// <summary>
      /// 修改整个表单值
      /// </summary>
      /// <param name="sqlCmd"></param>
      /// <param name="dt"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool updateTableSQL(string sqlCmd, DataTable dt, ref string er)
      {
         if (dbCon == null)
         {
            er = "数据对象未引用.";
            return false;
         }
         return dbCon.updateTableCmd(sqlCmd, dt, ref er); 
      }
      /// <summary>
      /// 返回数据库表单名称
      /// </summary>
      /// <param name="tableNames"></param>
      /// <param name="er"></param>
      /// <returns></returns>
      public bool GetTableNames(ref string[] tableNames, ref string er)
      {
         if (dbCon == null)
         {
            er = "数据对象未引用.";
            return false;
         }
         return dbCon.returnTableNames(ref tableNames, ref er); 
      }
      #endregion
   }
}
