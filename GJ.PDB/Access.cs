using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.OleDb;
using System.IO;
using System.Data;


namespace GJ.PDB
{
   public class Access:IDataBase 
   {

      #region 读写锁
      private ReaderWriterLock wrLock = new ReaderWriterLock();
      #endregion

      #region 字段
      private string serverName = "localhost";
      private string dbName = "SysConfig.accdb";
      private string userName = "GUANJIA";
      private string passWord = "GuanJia";
      /// <summary>
      /// 连接
      /// </summary>
      private OleDbConnection objConn = new OleDbConnection();
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
            if (!File.Exists(dbName))
               throw new Exception("数据库文件不存在.");
            string extName = Path.GetExtension(dbName).ToLower();
            string provider = string.Empty;
            if (extName == ".accdb")
                provider = "provider=microsoft.ace.oledb.12.0;Data Source=" + dbName +
                                ";Jet OLEDB:DataBase password=" + passWord;
            else
                //provider = "provider=microsoft.ace.oledb.4.0;Data Source=" + dbName +
                 //               ";Jet OLEDB:DataBase password=" + passWord;
                provider = "provider=microsoft.ace.oledb.12.0;Data Source=" + dbName +
                              ";Jet OLEDB:DataBase password=" + passWord;
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
            OleDbDataAdapter adapter = new OleDbDataAdapter(sqlCmd, objConn);
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
            OleDbCommand objCmd = new OleDbCommand(sqlCmd, objConn);
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
               OleDbCommand objCmd = new OleDbCommand(sqlCmd[i].ToString(), objConn);
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
       /// <summary>
       /// sql语句需要主键
       /// </summary>
       /// <param name="sqlCmd"></param>
       /// <param name="dt"></param>
       /// <param name="er"></param>
       /// <returns></returns>
      public bool updateTableCmd(string sqlCmd, System.Data.DataTable dt, ref string er)
      {

          try
          {
              wrLock.AcquireWriterLock(-1);
              if (!connect())
                  return false;
              OleDbDataAdapter adapter = new OleDbDataAdapter(sqlCmd, objConn);
              OleDbCommandBuilder dbbuilder = new OleDbCommandBuilder(adapter);
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
            DataTable dt = new DataTable();
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
            tableNames = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               tableNames[i] = dt.Rows[i].ItemArray[2].ToString();
            }
            dt.Dispose();
            dt = null;
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

      /// <summary>
      /// 压缩和备份数据库
      /// </summary>
      /// <param name="dbName"></param>
      /// <param name="passWord"></param>
      /// <param name="backUp"></param>
      /// <returns></returns>
      public static bool CompactDatabase(string dbName,bool backUp = false, string passWord = "")
      {
          if (!File.Exists(dbName))
              return false;
          try
          {
              string tempDB = dbName + "bak";
              string extName = Path.GetExtension(dbName).ToLower();
              string provider1 = string.Empty;
              string provider2 = string.Empty;
              //建立临时数据库
              if (extName == ".accdb")
              {
                  provider1 = "provider=microsoft.ace.oledb.12.0;Data Source=" + dbName +
                                 ";Jet OLEDB:DataBase password=" + passWord;
                  provider2 = "provider=microsoft.ace.oledb.12.0;Data Source=" + tempDB +
                                      ";Jet OLEDB:DataBase password=" + passWord + ";Jet OLEDB:Engine Type=5";

              }
              else
              {
                  provider1 = "provider=microsoft.ace.oledb.4.0;Data Source=" + dbName +
                                                 ";Jet OLEDB:DataBase password=" + passWord;
                  provider2 = "provider=microsoft.ace.oledb.4.0;Data Source=" + tempDB +
                                      ";Jet OLEDB:DataBase password=" + passWord + ";Jet OLEDB:Engine Type=5";

              }
              //删除临时文件
              if (System.IO.File.Exists(tempDB))
                  System.IO.File.Delete(tempDB);
              JRO.JetEngine jet = new JRO.JetEngine();
              jet.CompactDatabase(provider1, provider2);
              File.Copy(tempDB, dbName, true);
              if (!backUp)
                  System.IO.File.Delete(tempDB);
              return true;
          }
          catch (Exception)
          {
              return false;
          }
      }
      #endregion

      
   }
}
