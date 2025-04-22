using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using GJ.PDB; 
namespace GJ.ATE
{
    public partial class WndFrmUsers : Form
    {
        public WndFrmUsers()
        {
            InitializeComponent();

            InitialForm();
        }

        #region 初始化
        private void InitialForm()
        {
           checkLevel = new CheckBox[] { 
                                         chkLevel1, chkLevel2, chkLevel3, chkLevel4, 
                                         chkLevel5,chkLevel6,chkLevel7,chkLevel8
                                         };
           userLevelDB = new CUserLevelDB();

        }
        #endregion

        #region 面板回调函数
        private void WndFrmUsers_Load(object sender, EventArgs e)
        {
           InitialUserGrid(); 
        }
        private void gridUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           int curRow = gridUsers.CurrentCell.RowIndex;
           string curUser = gridUsers.Rows[curRow].Cells[0].Value.ToString();
           DataSet ds = new DataSet();
           string er = string.Empty;
           if(!userLevelDB.GetUserLevel(curUser,ref ds ,ref er))
           {
              MessageBox.Show(er, "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return;
           }
           if (ds.Tables[0].Rows.Count > 0)
           {
              txtUserName.Text = curUser;              
              txtPassWord.Text = ds.Tables[0].Rows[0][1].ToString() ;
              for (int i = 0; i < 8; i++)
              {
                 if (ds.Tables[0].Rows[0][2 + i].ToString() == "1")
                    checkLevel[i].Checked = true;
                 else
                    checkLevel[i].Checked = false;
              }
           }
        }
        private void chkLook_CheckedChanged(object sender, EventArgs e)
        {
           if (!chkLook.Checked)
              txtPassWord.PasswordChar = '*';
           else
              txtPassWord.PasswordChar = '\0'; 
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
         if (txtUserName.Text == string.Empty)
         {
            MessageBox.Show("请输入要新增用户名", "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
         }              
         if (MessageBox.Show("确定要新增该用户信息?", "用户权限",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
         ArrayList pwrLevel = new ArrayList();
         string er = string.Empty;
            for (int i = 0; i < 8; i++)
			{
            pwrLevel.Add(checkLevel[i].Checked ? 1 :0);
			   }
         if (!userLevelDB.AddUserInfo(txtUserName.Text,txtPassWord.Text ,pwrLevel, ref er))
         {
            MessageBox.Show(er, "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
         }
         InitialUserGrid();
         MessageBox.Show("成功新增用户信息", "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           if (txtUserName.Text == string.Empty)
           {
              MessageBox.Show("请选择要修改用户信息", "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return;
           }
           if (MessageBox.Show("确定要修改该用户信息?", "用户权限",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
              return;
           ArrayList pwrLevel = new ArrayList();
           string er = string.Empty;
           for (int i = 0; i < 8; i++)
           {
              pwrLevel.Add(checkLevel[i].Checked ? 1 : 0);
           }
           if (!userLevelDB.UpdateUserInfo(txtUserName.Text, txtPassWord.Text, pwrLevel, ref er))
           {
              MessageBox.Show(er, "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return;
           }
           InitialUserGrid();
           MessageBox.Show("成功修改用户信息", "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
           if (txtUserName.Text == string.Empty)
           {
              MessageBox.Show("请选择删除用户名", "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return;
           }
           if (MessageBox.Show("确定要删除该用户信息?", "用户权限",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
              return;
           string er = string.Empty;
           if (!userLevelDB.deleteUserInfo(txtUserName.Text,ref er))
           {
              MessageBox.Show(er, "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return;
           }
           InitialUserGrid();
           MessageBox.Show("成功删除用户信息", "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
           this.Close();
        }
        #endregion

        #region 字段
        private CheckBox[] checkLevel;
        private CUserLevelDB userLevelDB;
       #endregion

        #region 方法
        private void InitialUserGrid()
        {
           DataSet ds = new DataSet();
           string er = string.Empty;
           if (!userLevelDB.GetUsersInfo(ref ds, ref er))
           {
              MessageBox.Show(er, "用户权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return;
           }
           for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
           {
              string strPwr = ds.Tables[0].Rows[i].ItemArray[1].ToString();
              string strHide = string.Empty;
              for (int j = 0; j < strPwr.Length; j++)
              {
                 strHide += "*";
              }
              ds.Tables[0].Rows[i][1] = strHide;
           }
           gridUsers.ReadOnly = true;
           gridUsers.DataSource = ds.Tables[0];
        }
        #endregion

        #region 数据库用户权限查询
        private class CUserLevelDB 
        {
           private CDBCom dbCom = new CDBCom(CDBCom.EDBType.Access);   
           public CUserLevelDB()
           {
             
           }
           /// <summary>
           /// 获取用户名和密码
           /// </summary>
           /// <param name="ds"></param>
           /// <param name="er"></param>
           /// <returns></returns>
           public bool GetUsersInfo(ref DataSet ds, ref string er)
           {
              string sqlCmd = "Select UserName as 用户名,UserPassWord as 密码 from UserInfo order by UserName";
              if (!dbCom.QuerySQL(sqlCmd, ref ds, ref er))
                 return false;
              return true;
           }
           /// <summary>
           /// 获取当前用户权限
           /// </summary>
           /// <param name="curUser"></param>
           /// <param name="ds"></param>
           /// <param name="er"></param>
           /// <returns></returns>
           public bool GetUserLevel(string curUser, ref DataSet ds, ref string er)
           {
              string sqlCmd = "Select * from UserInfo where UserName='" + curUser + "' order by UserName";
              if (!dbCom.QuerySQL(sqlCmd, ref ds, ref er))
                 return false;
              return true;
           }
           /// <summary>
           /// 添加用户信息
           /// </summary>
           /// <param name="curUser"></param>
           /// <param name="pwr"></param>
           /// <param name="pwrLevel"></param>
           /// <param name="er"></param>
           /// <returns></returns>
           public bool AddUserInfo(string curUser, string pwr, ArrayList pwrLevel, ref string er)
           {
              DataSet ds = new DataSet();
              string sqlCmd = "Select * from UserInfo where UserName='" + curUser + "' order by UserName";
              if (!dbCom.QuerySQL(sqlCmd, ref ds, ref er))
                 return false;
              if (ds.Tables[0].Rows.Count > 0)
              {
                 er = "该用户信息已存在,请重输入用户名.";
                 return false;
              }
              sqlCmd = "insert into UserInfo(UserName,UserPassWord,PWR0,PWR1,PWR2,PWR3,PWR4,PWR5,PWR6,PWR7)" +
                                 "values ('" + curUser + "','" + pwr + "',"
                                 + pwrLevel[0] + "," + pwrLevel[1] + "," + pwrLevel[2] + "," + pwrLevel[3] + ","
                                 + pwrLevel[4] + "," + pwrLevel[5] + "," + pwrLevel[6] + "," + pwrLevel[7] + ")";
              if (!dbCom.excuteSQL(sqlCmd, ref er))
                 return false;
              return true;
           }
           /// <summary>
           /// 修改用户信息
           /// </summary>
           /// <param name="curUser"></param>
           /// <param name="pwr"></param>
           /// <param name="pwrLevel"></param>
           /// <param name="er"></param>
           /// <returns></returns>
           public bool UpdateUserInfo(string curUser, string pwr, ArrayList pwrLevel, ref string er)
           {
              DataSet ds = new DataSet();
              string sqlCmd = "Select * from UserInfo where UserName='" + curUser + "' order by UserName";
              if (!dbCom.QuerySQL(sqlCmd, ref ds, ref er))
                 return false;
              if (ds.Tables[0].Rows.Count == 0)
              {
                 er = "该用户信息不存在,请选择用户名.";
                 return false;
              }
              sqlCmd = "update UserInfo Set UserPassWord='" + pwr + "',PWR0=" + pwrLevel[0] + ",PWR1=" + pwrLevel[1] +
                                            ",PWR2=" + pwrLevel[2] + ",PWR3=" + pwrLevel[3] + ",PWR4=" + pwrLevel[4] +
                                            ",PWR5=" + pwrLevel[5] + ",PWR6=" + pwrLevel[6] + ",PWR7=" + pwrLevel[7] +
                                            " where UserName='" + curUser + "'";
              if (!dbCom.excuteSQL(sqlCmd, ref er))
                 return false;
              return true;

           }
           /// <summary>
           /// 删除用户信息
           /// </summary>
           /// <param name="curUser"></param>
           /// <param name="er"></param>
           /// <returns></returns>
           public bool deleteUserInfo(string curUser, ref string er)
           {
              string sqlCmd = "Delete * from UserInfo where UserName='" + curUser + "'";
              if (!dbCom.excuteSQL(sqlCmd, ref er))
                 return false;
              return true;
           }
        }
        #endregion

    }
   
 }
