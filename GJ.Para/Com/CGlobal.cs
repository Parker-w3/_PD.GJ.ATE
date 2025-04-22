using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using GJ;
using GJ.PDB;

namespace GJ.Para
{
   public class CGlobal
   {
      /// <summary>
      /// 用户登录
     /// </summary>
      public class User
      {
         public static string mlogName;
         public static bool mLog = false;
         public static int[] pwrLevel = new int[8];
      }
      /// <summary>
      /// 测试站信息
      /// </summary>
      public class CFlow
      {

          #region 字段

          public static int idNo;
          /// <summary>
          /// 站别名称
          /// </summary>
          public static string flowName;
          /// <summary>
          /// 站别编号
          /// </summary>
          public static int flowId;
          /// <summary>
          /// 子站别:0,1,2
          /// </summary>
          public static int flowSide;
          /// <summary>
          /// 站别唯一标识
          /// </summary>
          public static string flowGUID;
          public static int slotNum;
          public static int slotRow;
          public static int slotCol;

          private static string IniFile = Application.StartupPath + "\\iniFile.ini";

          #endregion
         
          /// <summary>
          /// 加载当前测试位
          /// </summary>
          /// <returns></returns>
          public static bool load(ref string er)
          {
              try
              {
                  if (!File.Exists(IniFile))
                  {
                      er = "找不到系统配置文件";
                      return false;
                  }
                  string GUIDTemp = CIniFile.ReadFromIni("Station", "GUID", IniFile);
                  if (GUIDTemp == "")
                  {
                      er = "初始化系统配置文件错误";
                      return false;
                  }
                  return getFlowInfo(GUIDTemp, ref er);
              }
              catch (Exception ex)             
              {
                  er = ex.ToString(); 
                  return false;
              }
          }
          /// <summary>
          /// 保存当前测试站
          /// </summary>
          /// <param name="idNo"></param>
          public static void save()
          {
              CIniFile.WriteToIni("Station", "GUID",flowGUID, IniFile);
          }
          /// <summary>
          /// 获取当前测试站信息
          /// </summary>
          /// <param name="wGUID"></param>
          /// <param name="er"></param>
          /// <returns></returns>
          public static bool getFlowInfo(string  wGUID,ref string er)
          {
              try
              {
                  //获取测试站
                  CDBCom db = new CDBCom(CDBCom.EDBType.Access);
                  DataSet ds = new DataSet();
                  string sqlCmd = "select * from FlowInfo where GUID='" + wGUID+"'";
                  if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                     return false;                  
                  if (ds.Tables[0].Rows.Count == 0)
                  {
                    er="当前测试站不存在";
                    return false;
                  }
                  idNo = System.Convert.ToInt16(ds.Tables[0].Rows[0]["idNo"].ToString());
                  flowGUID = ds.Tables[0].Rows[0]["GUID"].ToString();
                  flowName = ds.Tables[0].Rows[0]["FlowName"].ToString();
                  flowId = System.Convert.ToInt16(ds.Tables[0].Rows[0]["FlowID"].ToString());
                  flowSide = System.Convert.ToInt16(ds.Tables[0].Rows[0]["FlowSide"].ToString());
                  slotNum = System.Convert.ToInt16(ds.Tables[0].Rows[0]["SlotNum"].ToString());
                  slotRow = System.Convert.ToInt16(ds.Tables[0].Rows[0]["SlotRow"].ToString());
                  slotCol = System.Convert.ToInt16(ds.Tables[0].Rows[0]["SlotCol"].ToString());
                  return true;
              }
              catch (Exception ex)
              {                  
                  er=ex.ToString();
                  return false; 
              }          
          }
         
      }
   }
}
