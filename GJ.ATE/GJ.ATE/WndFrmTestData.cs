using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ.PDB;
using GJ.Para;
 
namespace GJ.ATE
{
    public partial class WndFrmTestData : Form
    {
        public WndFrmTestData()
        {
            InitializeComponent();
        }

        #region 面板回调函数
        private void WndFrmTestData_Load(object sender, EventArgs e)
        {
            int subNo = 0;
            if (CGlobal.CFlow.flowGUID == "BOB1")            
                subNo = 1;            
            else if (CGlobal.CFlow.flowGUID == "BOB2")            
                subNo = 2;            
            else if (CGlobal.CFlow.flowGUID == "LOADUP")
                subNo = 3;
            IniDataView(subNo);
        }
        #endregion

        #region 方法
        private void IniDataView(int subNo=0)
        {
            try
            {
                string sysDB = "DBLog\\" + CGlobal.CFlow.flowGUID + ".accdb";
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", sysDB);
                string er = string.Empty;
                DataSet ds = new DataSet();
                string sqlCmd = string.Empty;
                if (subNo == 0)
                    sqlCmd = "Select (slotNo+1) as 槽位编号,slotName as 槽位名,result as 结果,serialNo as 条码," +
                             "StartTime as 开始时间,TestTime as 测试时间,ReTimes as 重测次数 from slotData order by startTime";
                else if (subNo == 1)
                    sqlCmd = "Select slotNo,slotName,result,serialNo,StartTime,TestTime,ReTimes from slotData" + subNo.ToString() + " order by startTime";
                else if (subNo == 2)
                    sqlCmd = "Select slotNo,slotName,result,serialNo,StartTime,TestTime,ReTimes from slotData" + subNo.ToString() + " order by startTime";
                else
                    sqlCmd = "Select * from slotData order by StartTime";
                if (db.QuerySQL(sqlCmd, ref ds, ref er))
                {
                    testDataView.ReadOnly = true;
                    testDataView.RowHeadersWidth = 28;
                    testDataView.ColumnHeadersHeight = 30;
                    testDataView.DataSource = ds.Tables[0];
                    for (int i = 0; i < testDataView.ColumnCount; i++)                    
                        testDataView.Columns[i].Width = 150;                   
                }
            }
            catch (Exception)
            {
                
                throw;
            }           
        }
        #endregion

    }
}
