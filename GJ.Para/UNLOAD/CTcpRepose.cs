using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GJ.PDB;

namespace GJ.Para.UNLOAD
{
    public class CTcpRepose
    {
        #region 构造函数
        public CTcpRepose(string dbName)
        {
            try
            {
                CDBCom db = new CDBCom(CDBCom.EDBType.Access, "", dbName);

                string er = string.Empty;

                string sqlCmd = string.Empty;

                DataSet ds = new DataSet();

                sqlCmd = "select * from StatInfo order by StatId";

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int statId = System.Convert.ToInt32(ds.Tables[0].Rows[i]["statId"].ToString());
                    string statName = ds.Tables[0].Rows[i]["statName"].ToString();
                    int slotNum = System.Convert.ToInt32(ds.Tables[0].Rows[i]["slotNum"].ToString());
                    if (!statIdList.ContainsKey(statName))
                        statIdList.Add(statName, statId);
                    else
                        statIdList[statName] = statId;
                    if (!statRunList.ContainsKey(statIdList[statName]))
                        statRunList.Add(statIdList[statName], new CSAT(slotNum));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 枚举
        /// <summary>
        /// 命令定义
        /// </summary>
        public enum ECMD
        {
            QUERY_STATE,
            START_TEST,
            END_TEST
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        public enum EERRCODE
        {
            COMMAND_ERROR,
            STATION_IS_NOT_EXIST,
            STATION_IS_NOT_READY,
            ID_CARD_IS_ERROR,
            STATION_INI_FAIL,
            RETURN_RESULT_LENGTH_ERR
        }
        /// <summary>
        /// 运行状态
        /// </summary>
        public enum ERUN
        {
            空闲,
            测试就绪,
            测试中,
            测试结束
        }
        #endregion

        #region 类定义
        /// <summary>
        /// 测试站信息
        /// </summary>
        public class CSAT
        {
            public CSAT(int slotNum)
            {
                this.slotNum = slotNum;

                for (int i = 0; i < slotNum; i++)
                {
                    serialNo.Add("");
                    result.Add(0);
                }
            }
            public int slotNum = 0;
            public int statIdNo;
            public string statName;
            public ERUN doRun = ERUN.空闲;
            public int subNo = 1;
            public string idCard;
            public List<string> serialNo = new List<string>();
            public List<int> result = new List<int>();
            public string modelName = string.Empty;
        }
        #endregion

        #region 字段
        /// <summary>
        /// 站别名->对应站别编号
        /// </summary>
        public Dictionary<string, int> statIdList = new Dictionary<string, int>();
        /// <summary>
        /// 站别号->对应测试站信息
        /// </summary>
        public Dictionary<int, CSAT> statRunList = new Dictionary<int, CSAT>();
        #endregion

        #region 方法
        /// <summary>
        /// 应答客户端数据
        /// </summary>
        /// <param name="recvData"></param>
        /// <returns></returns>
        public string reponse(string recvData)
        {
            try
            {
                string reposeData = string.Empty;

                string er = string.Empty;

                ECMD cmdType = ECMD.QUERY_STATE;

                int statId = 0;

                string statIdName = string.Empty;

                if (statIdList.Count == 0)
                    return EERRCODE.STATION_INI_FAIL.ToString();

                if (!getCmdType(recvData, ref cmdType, ref statId, ref statIdName, ref er))
                    return er;

                switch (cmdType)
                {
                    //接收:QUERY_STATE_XXXX
                    //应答:STATE_XXXX|0|Model|IDCard|Sn1;Sn2…
                    case ECMD.QUERY_STATE:
                        recvData = recvData.Replace("QUERY_STATE", "STATE");
                        if (statRunList[statId].doRun == ERUN.空闲 || statRunList[statId].doRun == ERUN.测试结束)
                        {
                            reposeData = recvData + "|0|";
                        }
                        else
                        {
                            reposeData = recvData + "|" + statRunList[statId].subNo.ToString() + "|";
                            reposeData += statRunList[statId].modelName + "|";
                            reposeData += statRunList[statId].idCard + "|";
                            for (int i = 0; i < statRunList[statId].serialNo.Count; i++)
                                reposeData += statRunList[statId].serialNo[i] + ";";
                        }
                        break;
                    //接收:START_TEST_XXXX
                    //应答:START_TEST_OK
                    case ECMD.START_TEST:
                        if (statRunList[statId].doRun == ERUN.空闲)
                            reposeData = "START_" + statIdName + "_NG:" + EERRCODE.STATION_IS_NOT_READY.ToString();
                        else
                        {
                            reposeData = "START_" + statIdName + "_OK";
                            statRunList[statId].doRun = ERUN.测试中;
                        }
                        break;
                    //接收:END_TEST_XXXX|ID|result1;result2…
                    //应答:END_TEST_OK
                    case ECMD.END_TEST:
                        if (statRunList[statId].doRun == ERUN.空闲)
                            reposeData = "END_TEST_" + statIdName + "_NG:" + EERRCODE.STATION_IS_NOT_READY.ToString();
                        else
                        {
                            string[] recvList = recvData.Split('|');
                            if (recvList.Length < 3)
                            {
                                reposeData = "END_TEST_" + statIdName + "_NG:" + EERRCODE.COMMAND_ERROR.ToString();
                                return reposeData;
                            }
                            if (statRunList[statId].idCard != recvList[1])
                            {
                                reposeData = "END_TEST_" + statIdName + "_NG:" + EERRCODE.ID_CARD_IS_ERROR.ToString() + "_" + statRunList[statId].idCard;
                                return reposeData;
                            }
                            string[] resultList = recvList[2].Split(';');
                            if (resultList.Length < statRunList[statId].serialNo.Count)
                            {
                                reposeData = "END_TEST_" + statIdName + "_NG:" +
                                             EERRCODE.RETURN_RESULT_LENGTH_ERR.ToString() +
                                             "_" + resultList.Length.ToString();
                                return reposeData;
                            }
                            for (int i = 0; i < statRunList[statId].serialNo.Count; i++)
                                statRunList[statId].result[i] = System.Convert.ToInt32(resultList[i]);
                            reposeData = "END_TEST_" + statIdName + "_OK";
                            statRunList[statId].doRun = ERUN.测试结束;
                        }
                        break;
                    default:
                        break;
                }
                return reposeData;
            }
            catch (Exception ex)
            {
                return EERRCODE.COMMAND_ERROR.ToString() + ":" + ex.ToString();
            }
        }
        /// <summary>
        /// 解析获取数据
        /// </summary>
        /// <param name="recvData"></param>
        /// <param name="cmdType"></param>
        /// <param name="statName"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        private bool getCmdType(string recvData, ref ECMD cmdType, ref int statId, ref string statIdName, ref string er)
        {
            try
            {
                //获取命令类型

                string statName = string.Empty;

                if (recvData.Length > (ECMD.QUERY_STATE.ToString().Length + 1) &&
                    recvData.Substring(0, ECMD.QUERY_STATE.ToString().Length) == ECMD.QUERY_STATE.ToString())
                {
                    cmdType = ECMD.QUERY_STATE;

                    statName = recvData.Substring(ECMD.QUERY_STATE.ToString().Length + 1, recvData.Length - ECMD.QUERY_STATE.ToString().Length - 1);
                }
                else if (recvData.Length > (ECMD.START_TEST.ToString().Length + 1) &&
                         recvData.Substring(0, ECMD.START_TEST.ToString().Length) == ECMD.START_TEST.ToString())
                {
                    cmdType = ECMD.START_TEST;

                    statName = recvData.Substring(ECMD.START_TEST.ToString().Length + 1, recvData.Length - ECMD.START_TEST.ToString().Length - 1);

                }
                else if (recvData.Length > (ECMD.END_TEST.ToString().Length + 1) &&
                         recvData.Substring(0, ECMD.END_TEST.ToString().Length) == ECMD.END_TEST.ToString())
                {
                    statName = string.Empty;

                    cmdType = ECMD.END_TEST;

                    string[] strData = recvData.Split('|');

                    if (strData.Length > 1)
                    {
                        statName = strData[0];
                        statName = statName.Substring(ECMD.END_TEST.ToString().Length + 1, statName.Length - ECMD.END_TEST.ToString().Length - 1);
                    }
                }
                else
                {
                    er = EERRCODE.COMMAND_ERROR.ToString();
                    return false;
                }

                //获取站别编号
                if (!statIdList.ContainsKey(statName))
                {
                    er = EERRCODE.STATION_IS_NOT_EXIST.ToString() + ":" + statName;
                    return false;
                }

                statIdName = statName;

                statId = statIdList[statName];


                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置测试站信息
        /// </summary>
        /// <param name="statName"></param>
        /// <param name="runStatus"></param>
        /// <param name="idCard"></param>
        /// <param name="serialNo"></param>
        public bool setStatus(string statName, ERUN runStatus, ref string er, string modelName = "", string idCard = "", List<string> serialNo = null, int subNo = 1)
        {
            try
            {
                if (!statIdList.ContainsKey(statName))
                {
                    er = statName + "测试站不在流程中";
                    return false;
                }
                int statId = statIdList[statName];
                statRunList[statId].doRun = runStatus;
                statRunList[statId].subNo = subNo;
                statRunList[statId].modelName = modelName;
                statRunList[statId].idCard = idCard;
                if (serialNo != null)
                {
                    for (int i = 0; i < serialNo.Count; i++)
                        statRunList[statId].serialNo[i] = serialNo[i];
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        #endregion

    }
}
