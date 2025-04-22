using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
namespace GJ.PDB
{
   public class MySQL:IDataBase 
   {
      #region 读写锁
      private ReaderWriterLock wrLock = new ReaderWriterLock();
      #endregion

      #region 字段
      private string serverName = "localhost";
      private string dbName = "autoflow";
      private string userName = "root";
      private string passWord = "P@ssword";
      MySqlConnection objConn = new MySqlConnection();
      #endregion

      #region 属性
      public string mServerName
      {
         set { serverName = value; }
      }
      public string mDbName
      {
         set { dbName = value; }
      }
      public string mUserName
      {
         set { userName = value; }
      }
      public string mPassWord
      {
         set { passWord = value; }
      }
      #endregion

      #region 方法
      public bool connect()
      {
         try
         {
            string provider = "server = " + serverName + "; user id = " + userName + "; password = " + passWord +
                              ";database = " + dbName;
            if (objConn.State == ConnectionState.Open)
               objConn.Close();
            objConn.ConnectionString = provider;
            objConn.Open();
            return true;
         }
         catch (Exception e)
         {
            throw e;
         }
      }
      public void close()
      {
         if (objConn.State == ConnectionState.Open)
            objConn.Close();
      }

      public bool queryCmd(string sqlCmd, ref System.Data.DataSet ds, ref string er)
      {
         try
         {
            wrLock.AcquireWriterLock(-1);
            if (!connect())
               return false;
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd, objConn);
            adapter.Fill(ds);
            adapter.Dispose();
            adapter = null;
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
         finally
         {
            close();
            wrLock.ReleaseWriterLock();
         }
      }

      public bool excuteCmd(string sqlCmd, ref string er)
      {
         try
         {
            wrLock.AcquireWriterLock(-1);
            if (!connect())
               return false;
            MySqlCommand objCmd = new MySqlCommand(sqlCmd, objConn);
            objCmd.ExecuteNonQuery();
            objCmd.Dispose();
            objCmd = null;
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
         finally
         {
            close();
            wrLock.ReleaseWriterLock();
         }
      }

      public bool excuteArrayCmd(List<string> sqlCmd, ref string er)
      {
         try
         {
            wrLock.AcquireWriterLock(-1);
            if (!connect())
               return false;
            for (int i = 0; i < sqlCmd.Count; i++)
            {
               MySqlCommand objCmd = new MySqlCommand(sqlCmd[i].ToString(), objConn);
               objCmd.ExecuteNonQuery();
               objCmd.Dispose();
               objCmd = null;
            }
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
         finally
         {
            close();
            wrLock.ReleaseWriterLock();
         }
      }

      public bool updateTableCmd(string sqlCmd, System.Data.DataTable dt, ref string er)
      {
         try
         {
            wrLock.AcquireWriterLock(-1);
            if (!connect())
               return false;
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd, objConn);
            MySqlCommandBuilder dbbuilder = new MySqlCommandBuilder(adapter);
            DataSet ds = new DataSet();
            string tableName = "newTable";
            adapter.Fill(ds, tableName);
            if (ds.Tables.Count == 0)
            {
               ds.Dispose();
               dbbuilder.Dispose();
               adapter.Dispose();
               throw new Exception("查询表单语句错误.");
            }
            object[] RowVal = new object[ds.Tables[tableName].Columns.Count];
            for (int iRow = 0; iRow < ds.Tables[tableName].Rows.Count; iRow++)
            {
               if (iRow < dt.Rows.Count)
               {
                  for (int iCol = 0; iCol < ds.Tables[tableName].Columns.Count; iCol++)
                  {
                     if (iCol < dt.Columns.Count)
                        RowVal[iCol] = dt.Rows[iRow].ItemArray[iCol];
                  }
                  ds.Tables[tableName].Rows[iRow].ItemArray = RowVal;
               }
            }
            adapter.Update(ds, tableName);
            ds.Dispose();
            dbbuilder.Dispose();
            adapter.Dispose();
            ds = null;
            dbbuilder = null;
            adapter = null;
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
         finally
         {
            close();
            wrLock.ReleaseWriterLock();
         }
      }

      public bool returnTableNames(ref string[] tableNames, ref string er)
      {
         try
         {
            wrLock.AcquireWriterLock(-1);
            if (!connect())
               return false;
            string sqlCmd = "select table_name from information_schema.tables where table_schema='csdb' and table_type='base table'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd, objConn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            tableNames = new string[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
               tableNames[i] = ds.Tables[0].Rows[i][0].ToString();
            }
            adapter.Dispose();
            adapter = null;
            ds.Dispose();
            ds = null;
            return true;
         }
         catch (Exception e)
         {
            er = e.ToString();
            return false;
         }
         finally
         {
            close();
            wrLock.ReleaseWriterLock();
         }
      }
      public bool CompactDatabase(string dbTempPath)
      {
         return false;
      }
      #endregion

   }
}
