using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GJ;
using GJ.PDB;
using GJ.Para;
namespace GJ.ATE
{
    public partial class WndSelectFlow : Form
    {
        public WndSelectFlow()
        {
            InitializeComponent();
        }


        private void WndSelectFlow_Load(object sender, EventArgs e)
        {
            try
            {
                listFlow.Clear();  
                string er = string.Empty;
                List<string> flowGUID = new List<string>();
                List<string> flowName = new List<string>();
                List<int> iconKey = new List<int>();
                if (!getFlowInfo(ref flowGUID,ref flowName, ref iconKey, ref er))
                {
                    MessageBox.Show(er.ToString());
                    return;
                }
                for (int i = 0; i < flowName.Count; i++)
                {
                    listFlow.Items.Add(flowName[i], iconKey[i]);
                    listFlow.Items[i].ToolTipText = flowGUID[i]; 
                }
                  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());   
            }                   
        }

        private bool getFlowInfo(ref List<string> flowGUID, ref List<string> flowName, ref List<int> iconKey,ref string er)
        {
            try
            {
                CDBCom db = new CDBCom(CDBCom.EDBType.Access);
                string sqlCmd = "Select * from FlowInfo where used=1 order by idNo";
                DataSet ds=new DataSet();
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    flowGUID.Add(ds.Tables[0].Rows[i]["GUID"].ToString());  
                    flowName.Add(ds.Tables[0].Rows[i]["FlowName"].ToString());
                    iconKey.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["Icon"].ToString()) - 1);  
                }
                return true; 
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false; 
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if(listFlow.SelectedItems.Count!=0)
            {
                if (MessageBox.Show("确定要加载该测试工位[" + listFlow.SelectedItems[0].Text + "]?", "测试工位选择",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string er = string.Empty; 
                    int idNo = listFlow.SelectedItems[0].Index;
                    if (!CGlobal.CFlow.getFlowInfo(listFlow.Items[idNo].ToolTipText, ref er))
                    {
                        MessageBox.Show(er);  
                    }
                    this.DialogResult = DialogResult.OK;
                }              
            }            
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
