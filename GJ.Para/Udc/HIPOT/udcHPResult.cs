using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJ.Para.Udc.HIPOT
{
    public partial class udcHPResult : UserControl
    {
        #region 构造函数
        public udcHPResult(int slotMax=8)
        {

            this.slotMax = slotMax;

            InitializeComponent();

            IntialControl();

            SetDoubleBuffered();

        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void IntialControl()
        {
            labId = new Label[]{
                               labId1, labId2,labId3,labId4,labId5,labId6,labId7,labId8,
                               labId9,labId10,labId11,labId12,labId13,labId14,labId15,labId16
                              };
            labSn = new Label[]{
                               labSn1,labSn2,labSn3,labSn4,labSn5,labSn6,labSn7,labSn8,
                               labSn9,labSn10,labSn11,labSn12,labSn13,labSn14,labSn15,labSn16
                               };
            labResult = new Label[]{
                                  labResult1,labResult2,labResult3,labResult4,labResult5,labResult6,labResult7,labResult8,
                                  labResult9,labResult10,labResult11,labResult12,labResult13,labResult14,labResult15,labResult16
                                  };
            labSlot = new Label[]{
                                labSlot1,labSlot2,labSlot3,labSlot4,labSlot5,labSlot6,labSlot7,labSlot8,
                                labSlot9,labSlot10,labSlot11,labSlot12,labSlot13,labSlot14,labSlot15,labSlot16
                                };
            imgUUT = new PictureBox[]{
                                    imgUUT1,imgUUT2,imgUUT3,imgUUT4,imgUUT5,imgUUT6,imgUUT7,imgUUT8,
                                    imgUUT9,imgUUT10,imgUUT11,imgUUT12,imgUUT13,imgUUT14,imgUUT15,imgUUT16
                                    };
            for (int i = slotMax; i < labSn.Length; i++)
            {
                labId[i].Visible = false;  
                labSn[i].Visible = false;
                labResult[i].Visible = false;
                labSlot[i].Visible = false;  
                imgUUT[i].Visible = false;  
            }
        }
        /// <summary>
        /// 设置双缓冲,防止界面闪烁
        /// </summary>
        private void SetDoubleBuffered()
        {
            panel1.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel1, true, null);
            panel2.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel2, true, null);
            panel3.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel3, true, null);
            panel4.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel4, true, null);
            panel5.GetType().GetProperty("DoubleBuffered",
                                          System.Reflection.BindingFlags.Instance |
                                          System.Reflection.BindingFlags.NonPublic)
                                          .SetValue(panel5, true, null);

        }
        #endregion

        #region 面板控件
        private Label[] labId = null; 
        private Label[] labSn = null;
        private Label[] labResult = null;
        private Label[] labSlot = null;
        private PictureBox[] imgUUT = null;
        #endregion

        #region 字段
        private int slotMax = 8;
        #endregion

        #region 面板回调函数
        private void udcHPResult_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region 私有方法
        
        #endregion

        #region 委托
        private delegate void clrResultHandler(int idNo);
        private delegate void SetFixHandler(string idCard, List<string> serialNos,int idNo);
        private delegate void SetResultHandler(List<int> result, int idNo);
        #endregion

        #region 共享方法
        /// <summary>
        /// 清除状态
        /// </summary>
        public void clrResult(int idNo=-1)
        {
            if (this.InvokeRequired)
                this.Invoke(new clrResultHandler(clrResult), idNo);
            else
            {
                if (idNo == -1)
                {
                    for (int i = 0; i < labResult.Length; i++)
                    {
                        labResult[i].Text = "";
                        imgUUT[i].Image = null;
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                      //  int index = idNo * 8 + i;
                        int index = i;
                        labResult[index].Text = "";
                        imgUUT[index].Image = null;
                    }
                }
              
            }
        }
        /// <summary>
        /// 设置治具状态
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="serialNo"></param>
        public void SetFix(string idCard, List<string> serialNos,int idNo=-1)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetFixHandler(SetFix), idCard, serialNos,idNo);
            else
            {
                labIdCard.Text = idCard;
                if (idNo == -1)
                {
                    for (int i = 0; i < serialNos.Count; i++)
                    {
                        labSn[i].Text = serialNos[i];
                        labResult[i].Text = "";
                        imgUUT[i].Image = null;
                    }
                }
                else
                {
                    for (int i = 0; i < serialNos.Count; i++)
                    {
                        int index = idNo * 8+i;
                        labSn[index].Text = serialNos[i];
                        labResult[index].Text = "";
                        imgUUT[index].Image = null;
                    }
                }
            }
        }
        /// <summary>
        /// 设置测试结果
        /// </summary>
        /// <param name="result"></param>
        public void SetResult(List<int> result, int idNo = 0)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetResultHandler(SetResult), result, idNo);
            else
            {
                for (int i = 0; i < result.Count; i++)
                {
                    int index = idNo * 8+i;

                    if (labSn[index].Text != "")
                    {
                        if (result[i] == -1)    //空闲
                        {
                            labResult[index].Text = "";
                            labResult[index].ForeColor = Color.Black;
                            imgUUT[index].Image =null;
                        }
                        else if (result[i] == -2)   //报警
                        {
                            labResult[index].Text = "NA";
                            labResult[index].ForeColor = Color.Red;
                            imgUUT[index].Image = ImageList1.Images["NA"];
                        }
                        else if (result[i] == 0)
                        {
                            labResult[index].Text = "PASS";
                            labResult[index].ForeColor = Color.Blue;
                            imgUUT[index].Image = ImageList1.Images["PASS"];
                        }
                        else
                        {
                            labResult[index].Text = "FAIL";
                            labResult[index].ForeColor = Color.Red;
                            imgUUT[index].Image = ImageList1.Images["FAIL"];
                        }
                    }
                } 
            }
        }
        public void SetResult(int idNo, int chanNo, int result)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<int, int, int>(SetResult), idNo, chanNo, result);
            else
            {
                //int index = idNo * 8 + chanNo;
                int index = chanNo;
                if (labSn[index].Text != "")
                {
                    if (result == -1)    //空闲
                    {
                        labResult[index].Text = "";
                        labResult[index].ForeColor = Color.Black;
                        imgUUT[index].Image = null;
                    }
                    else if (result == -2)   //报警
                    {
                        labResult[index].Text = "NA";
                        labResult[index].ForeColor = Color.Red;
                        imgUUT[index].Image = ImageList1.Images["NA"];
                    }
                    else if (result == 0)
                    {
                        labResult[index].Text = "PASS";
                        labResult[index].ForeColor = Color.Blue;
                        imgUUT[index].Image = ImageList1.Images["PASS"];
                    }
                    else
                    {
                        labResult[index].Text = "FAIL";
                        labResult[index].ForeColor = Color.Red;
                        imgUUT[index].Image = ImageList1.Images["FAIL"];
                    }
                }
            }
        }
        #endregion

    }
}
