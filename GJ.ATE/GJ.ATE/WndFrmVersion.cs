using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.ATE
{
    public partial class WndFrmVersion : Form
    {
        private class CVersion
        {
            public int level;
            public string upDate;
            public string upContext;
            public string upVer;
            public string upAthour;
        }

        #region 字段
        private List<CVersion> updateVer = new List<CVersion>(); 
        #endregion

        #region 面板回调函数
        public WndFrmVersion()
        {
            InitializeComponent();

            writeVersion();

            refreshView();

        }
        #endregion

        #region 方法
        private void writeVersion()
        {
            updateVer.Clear();

            CVersion ver = null;           
            ver = new CVersion();
            ver.level = 0;
            ver.upDate = "2020/04/15";
            ver.upContext = "群光自动测试线正式导入.";
            ver.upVer = "V1.0.0";
            ver.upAthour = "pf.xu";
            updateVer.Add(ver);

            ver = new CVersion();
            ver.level = 0;
            ver.upDate = "2021/03/18";
            ver.upContext = "更新高压测试结果显示错误的问题";
            ver.upVer = "V1.0.1";
            ver.upAthour = "pf.xu";
            updateVer.Add(ver);

            ver = new CVersion();
            ver.level = 0;
            ver.upDate = "2021/03/27";
            ver.upContext = "更新RRCC测试测试模式";
            ver.upVer = "V1.0.2";
            ver.upAthour = "pf.xu";
            updateVer.Add(ver);

            ver = new CVersion();
            ver.level = 0;
            ver.upDate = "2022/03/27";
            ver.upContext = "更新高压测试模式";
            ver.upVer = "V1.0.3";
            ver.upAthour = "pf.xu";
            updateVer.Add(ver);

            ver = new CVersion();
            ver.level = 0;
            ver.upDate = "2022/03/27";
            ver.upContext = "增加RRCC测试项目";
            ver.upVer = "V1.0.3";
            ver.upAthour = "pf.xu";
            updateVer.Add(ver);

            ver = new CVersion();
            ver.level = 0;
            ver.upDate = "2019/03/27";
            ver.upContext = "更新高压报表";
            ver.upVer = "V1.0.4";
            ver.upAthour = "pf.xu";
            updateVer.Add(ver);

            ver = new CVersion();
            ver.level = 0;
            ver.upDate = "2023/12/22";
            ver.upContext = "增加获取内码保存报表功能";
            ver.upVer = "V1.0.5";
            ver.upAthour = "pf.xu";
            updateVer.Add(ver);
        }
        private void refreshView()
        {
            VerView.Rows.Clear();
 
            for (int i = 0; i < updateVer.Count; i++)
            {
                VerView.Rows.Add(
                                 (i + 1),
                                 imageList1.Images[updateVer[i].level.ToString()],
                                 updateVer[i].upDate,
                                 updateVer[i].upContext,
                                 updateVer[i].upVer,
                                 updateVer[i].upAthour
                                 );
                VerView.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleLeft; 
            }        
        }
        #endregion


    }
}
