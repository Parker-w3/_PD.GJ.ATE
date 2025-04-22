using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GJ.PDB;
namespace GJ.Mes
{
    public class CGJMES
    {
       // public static string mesDB = "127.0.0.1";
        public static string mesDB = "192.168.3.200";
        public static string userName = "root";
        public static string password = "P@ssw0rd";
        public static int slotNum = 16;
        /// <summary>
        /// 定义流程代码
        /// </summary>
        public enum EFlowCode
        {
            正常流程,
            操作错误,
            产品不良,
            流程错误,
            流程过站,
            空治具过站
        }
        /// <summary>
        /// 基本信息
        /// </summary>
        public class CBase
        {
            public int flowId;
            public string serialNo;
        }
        /// <summary>
        /// 变量信息
        /// </summary>
        public class CPara
        {
            public int flowId;
            public string flowName;
            public string idCard;
            public int slotNo;
            public string serialNo;
            public int result;
            public int fixIsNull;
            public int testTimes;
            public string failInfo;
            public string flowModel;
            public string flowStartTime;
            public string flowEndTime;
            public Dictionary<int, string> remark = new Dictionary<int, string>();
            public EFlowCode flowCode;
        }
        /// <summary>
        /// 产品信息
        /// </summary>
        public class CUUT
        {
            public CBase Base = new CBase();
            public CPara Para = new CPara();
        }
        /// <summary>
        /// 治具信息
        /// </summary>
        public class CFIX
        {
            public int fixIsNull = 0;
            public List<int> flowId = new List<int>();
            public List<string> flowName = new List<string>();
            public List<string> serialNo = new List<string>();
            public List<int> result = new List<int>();
            public List<int> testTimes = new List<int>();
            public List<string> failInfo = new List<string>();

        }
        /// <summary>
        /// 产品条码记录
        /// </summary>
        public class CSnRecord
        {
            public string serialNo;
            public int slotNo;
            public string idCard;
            public int statId;
            public string statName;
            public string startTime;
            public string endTime;
            public int testResult;
            public string testData;
            public int testTime;
        }

        /// <summary>
        /// 检测服务器
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool CheckMySQL(ref string er)
        {
            try
            {
                er = string.Empty;
               // mesDB = "192.168.3.200";
                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                         "autoflow", userName, password);

                string sqlCmd = string.Empty;

                DataSet ds = new DataSet();

                sqlCmd = "select * from stationmap order by StationId";

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 绑定治具与产品条码
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="er"></param>
        /// <param name="serialNo"></param>
        /// <returns></returns>
        public static bool bandFlow(string idcard, ref string er, List<string> serialNo, int fixIsNull, string modelName = "", string flowName = "UNLOAD")
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                         "autoflow", userName, password);

                string sqlCmd = string.Empty;

                bool isExist = false;

                DataSet ds = new DataSet();

                sqlCmd = "select * from udtFixture where idCard='" + idcard + "' order by idNo";

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count == serialNo.Count)
                        isExist = true;
                    else
                    {
                        sqlCmd = "delete from udtFixture where idCard='" + idcard + "'";
                        if (!db.excuteSQL(sqlCmd, ref er))
                            return false;
                    }
                }
                List<string> cmdList = new List<string>();

                int uutResult = 0;

                if (!isExist)
                {
                    for (int i = 0; i < serialNo.Count; i++)
                    {
                        sqlCmd = "insert into udtFixture(idCard,slotNo,serialNo,fixIsNull," +
                                "curStationId,curStationName,curResult,curTestTimes,curFailInfo," +
                                "flowStartTime,flowEndTime,flowModel,remark0,remark1,remark2,remark3,remark4) values ('" +
                                idcard + "'," + i.ToString() + ",'" + serialNo[i] + "'," + fixIsNull.ToString() + ",0,'" +
                                flowName + "'," + uutResult + ",0,''," + "'','','" + modelName + "','','','','','')";
                        cmdList.Add(sqlCmd);
                    }
                }
                else
                {
                    for (int i = 0; i < serialNo.Count; i++)
                    {
                        sqlCmd = "update udtFixture Set serialNo='" + serialNo[i] + "',fixIsNull=" + fixIsNull.ToString() +
                                 ",curStationId=0,curStationName='" + flowName + "'," +
                                 "curResult=" + uutResult + ",curTestTimes=0,curFailInfo='',flowStartTime='',flowEndTime=''" +
                                 ",flowModel='" + modelName + "',remark0='',remark1='',remark2='',remark3='',remark4=''" +
                                 " where idCard='" + idcard + "' and slotNo=" + i;
                        cmdList.Add(sqlCmd);
                    }
                }

                return db.excuteSQLArray(cmdList, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 获取治具使用次数
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="usedNum"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool getFixtureUsedNum(string idcard, ref int ttNum, ref int failNum, ref Dictionary<int, string> snFailInfo, ref string er)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                        "autoflow", userName, password);

                string sqlCmd = string.Empty;

                DataSet ds = new DataSet();

                //获取治具使用次数
                sqlCmd = "select * from fixtureInfo where stationId=0 and idCard='" + idcard + "' order by idNo";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    ttNum = 0;
                    failNum = 0;
                    return true;
                }

                ttNum = System.Convert.ToInt32(ds.Tables[0].Rows[0]["ttNum"].ToString());

                //获取治具不良次数
                ds = new DataSet();

                sqlCmd = "select * from fixtureInfo where idCard='" + idcard + "' order by failNum desc";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    failNum = 0;
                    return true;
                }

                failNum = System.Convert.ToInt32(ds.Tables[0].Rows[0]["failNum"].ToString());

                //获取站别编号
                ds = new DataSet();

                sqlCmd = "select * from stationmap order by idNo";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    er = "该治具未设置可用站别号,请检查.";
                    return false;
                }
                int stationNum = ds.Tables[0].Rows.Count;
                Dictionary<int, string> stationName = new Dictionary<int, string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int keyVal = System.Convert.ToInt32(ds.Tables[0].Rows[i]["StationId"].ToString());
                    string TVal = ds.Tables[0].Rows[i]["StationName"].ToString();
                    if (!stationName.ContainsKey(keyVal))
                        stationName.Add(keyVal, TVal);
                }
                //获取各站别不良次数
                ds = new DataSet();
                sqlCmd = "select * from fixtureInfo where idCard='" + idcard + "' and stationId>0 and failNum>0 order by slotNo";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                if (ds.Tables[0].Rows.Count == 0)
                    return true;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int slotNo = System.Convert.ToInt32(ds.Tables[0].Rows[0]["slotNo"].ToString());
                    int sationId = System.Convert.ToInt32(ds.Tables[0].Rows[0]["stationId"].ToString());
                    int sationFailNum = System.Convert.ToInt32(ds.Tables[0].Rows[0]["failNum"].ToString());
                    string sationFailInfo = string.Empty;
                    if (!stationName.ContainsKey(sationId))
                        continue;
                    sationFailInfo = "[" + stationName[sationId] + "]:" + sationFailNum + ";";
                    if (!snFailInfo.ContainsKey(slotNo))
                        snFailInfo.Add(slotNo, sationFailInfo);
                    else
                        snFailInfo[slotNo] += sationFailInfo;
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 设置治具使用次数
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="er"></param>
        /// <param name="reset"></param>
        /// <returns></returns>
        public static bool setFixtureUsedNum(string idcard, ref string er, bool reset = false)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                         "autoflow", userName, password);

                string sqlCmd = string.Empty;

                DataSet ds = new DataSet();

                sqlCmd = "select * from fixtureInfo where stationId=0 and idCard='" + idcard + "' order by idNo";

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                if (ds.Tables[0].Rows.Count == 0)
                    sqlCmd = "Insert into fixtureInfo(idCard,slotNo,stationId,ttNum,failNum) values('" +
                           idcard + "',-1,0,0,0)";
                else
                {
                    if (reset)
                        sqlCmd = "update fixtureInfo set ttNum=0,failNum=0 where idCard='" + idcard + "'";
                    else
                        sqlCmd = "update fixtureInfo set ttNum=ttNum+1 where stationId=0 and idCard='" + idcard + "'";
                }

                return db.excuteSQL(sqlCmd, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 由ID获取产品条码
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="serialNo"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool getSnFromId(string idcard, ref List<string> serialNo, ref string er)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                       "autoflow", userName, password);

                string sqlCmd = string.Empty;

                DataSet ds = new DataSet();

                sqlCmd = "select * from udtFixture where idCard='" + idcard + "' order by slotNo";

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                if (ds.Tables[0].Rows.Count != 0)
                {
                    er = "由治具ID[" + idcard + "]获取不到产品条码";
                    return false;
                }

                serialNo = new List<string>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    serialNo.Add(ds.Tables[0].Rows[i]["serialNo"].ToString());

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }

        }
        /// <summary>
        /// 检查条码是否到该站别
        /// </summary>
        /// <param name="para"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool checkCurSnFlow(CUUT uut, ref string er)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                        "autoflow", userName, password);

                string sqlCmd = string.Empty;

                DataSet ds = new DataSet();

                sqlCmd = "select * from udtFixture where serialNo='" + uut.Base.serialNo + "' order by idNo";

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    er = "产品条码[" + uut.Base.serialNo + "]不存在,请重新绑定";
                    uut.Para.flowCode = EFlowCode.操作错误;
                    return false;
                }
                uut.Para.idCard = ds.Tables[0].Rows[0]["idCard"].ToString();
                uut.Para.slotNo = System.Convert.ToInt32(ds.Tables[0].Rows[0]["slotNo"].ToString());
                uut.Para.serialNo = ds.Tables[0].Rows[0]["serialNo"].ToString();
                uut.Para.fixIsNull = System.Convert.ToInt32(ds.Tables[0].Rows[0]["fixIsNull"].ToString());
                uut.Para.flowId = System.Convert.ToInt32(ds.Tables[0].Rows[0]["curStationId"].ToString());
                uut.Para.flowName = ds.Tables[0].Rows[0]["curStationName"].ToString();
                uut.Para.result = System.Convert.ToInt32(ds.Tables[0].Rows[0]["curResult"].ToString());
                uut.Para.testTimes = System.Convert.ToInt32(ds.Tables[0].Rows[0]["curTestTimes"].ToString());
                uut.Para.failInfo = ds.Tables[0].Rows[0]["curFailInfo"].ToString();
                uut.Para.flowModel = ds.Tables[0].Rows[0]["flowModel"].ToString();
                uut.Para.flowStartTime = ds.Tables[0].Rows[0]["flowStartTime"].ToString();
                uut.Para.flowEndTime = ds.Tables[0].Rows[0]["flowEndTime"].ToString();
                uut.Para.remark.Add(0, ds.Tables[0].Rows[0]["remark0"].ToString());
                uut.Para.remark.Add(1, ds.Tables[0].Rows[0]["remark1"].ToString());
                uut.Para.remark.Add(2, ds.Tables[0].Rows[0]["remark2"].ToString());
                uut.Para.remark.Add(3, ds.Tables[0].Rows[0]["remark3"].ToString());
                uut.Para.remark.Add(4, ds.Tables[0].Rows[0]["remark4"].ToString());

                //检查是否为空治具
                if (uut.Para.fixIsNull == 1)
                {
                    uut.Para.failInfo = "该治具为空治具,准备过站";
                    uut.Para.flowCode = EFlowCode.流程过站;
                    return false;
                }

                //获取当前产品站别
                ds = new DataSet();
                sqlCmd = "select * from stationMap where stationId=" + uut.Para.flowId;
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    uut.Para.failInfo = "产品条码[" + uut.Base.serialNo + "]当前站别不在流程中,请检查";
                    uut.Para.flowCode = EFlowCode.操作错误;
                    return false;
                }
                string curStationName = ds.Tables[0].Rows[0]["StationName"].ToString();

                if (uut.Para.result > 0)  //当前站不良
                {
                    uut.Para.failInfo = "产品条码[" + uut.Base.serialNo + "]不良:" + curStationName +
                         "_FAIL-" + uut.Para.result.ToString();
                    uut.Para.flowCode = EFlowCode.产品不良;
                    return false;
                }
                else
                {
                    if (uut.Para.flowId < uut.Base.flowId - 1) //未到当前站别   
                    {
                        uut.Para.failInfo = "产品条码[" + uut.Base.serialNo + "]流程错误:" + "当前站别为[" + curStationName + "]";
                        uut.Para.flowCode = EFlowCode.流程错误;
                        return false;
                    }
                    else if (uut.Para.flowId > uut.Base.flowId)
                    {
                        uut.Para.failInfo = "产品条码[" + uut.Base.serialNo + "]该站已测试,当前站别为[" + curStationName + "]";
                        uut.Para.flowCode = EFlowCode.流程过站;
                        return false;
                    }
                }
                uut.Para.flowCode = EFlowCode.正常流程;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 检查治具流程
        /// </summary>
        /// <param name="curStationId"></param>
        /// <param name="idCard"></param>
        /// <param name="uut"></param>
        /// <param name="fixFlowCode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool checkFixFlow(int curStationId, string idCard, ref Dictionary<int, CPara> uut, ref EFlowCode fixFlowCode, ref string er, bool chkRepeat = false, bool ForceIn = false)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                        "autoflow", userName, password);
                string sqlCmd = string.Empty;

                DataSet ds = new DataSet();

                sqlCmd = "select * from udtFixture where idCard='" + idCard + "' order by slotNo";

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    er = "治具ID[" + idCard + "]不存在,请重新绑定";
                    return false;
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CPara para = new CPara();
                    para.idCard = ds.Tables[0].Rows[i]["idCard"].ToString();
                    para.slotNo = System.Convert.ToInt32(ds.Tables[0].Rows[i]["slotNo"].ToString());
                    para.serialNo = ds.Tables[0].Rows[i]["serialNo"].ToString();
                    para.flowId = System.Convert.ToInt32(ds.Tables[0].Rows[i]["curStationId"].ToString());
                    para.flowName = ds.Tables[0].Rows[i]["curStationName"].ToString();
                    para.result = System.Convert.ToInt32(ds.Tables[0].Rows[i]["curResult"].ToString());
                    para.fixIsNull = System.Convert.ToInt32(ds.Tables[0].Rows[i]["fixIsNull"].ToString());
                    para.testTimes = System.Convert.ToInt32(ds.Tables[0].Rows[i]["curTestTimes"].ToString());
                    para.failInfo = ds.Tables[0].Rows[i]["curFailInfo"].ToString();
                    para.flowModel = ds.Tables[0].Rows[i]["flowModel"].ToString();
                    para.flowStartTime = ds.Tables[0].Rows[i]["flowStartTime"].ToString();
                    para.flowEndTime = ds.Tables[0].Rows[i]["flowEndTime"].ToString();
                    para.remark.Add(0, ds.Tables[0].Rows[i]["remark0"].ToString());
                    para.remark.Add(1, ds.Tables[0].Rows[i]["remark1"].ToString());
                    para.remark.Add(2, ds.Tables[0].Rows[i]["remark2"].ToString());
                    para.remark.Add(3, ds.Tables[0].Rows[i]["remark3"].ToString());
                    para.remark.Add(4, ds.Tables[0].Rows[i]["remark4"].ToString());
                    if (!uut.ContainsKey(para.slotNo))
                        uut.Add(para.slotNo, para);
                }
                //检查是否为空治具
                if (uut[0].fixIsNull == 1)
                {
                    er = "该治具为空治具,准备过站";
                    fixFlowCode = EFlowCode.空治具过站;
                    return false;
                }

                //获取流程站别名
                Dictionary<int, string> sationNames = new Dictionary<int, string>();

                ds = new DataSet();
                sqlCmd = "select * from stationMap order by StationId";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    er = "该治具站别不在流程中,请检查";
                    fixFlowCode = EFlowCode.操作错误;
                    return false;
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int keyVal = System.Convert.ToInt32(ds.Tables[0].Rows[i]["StationId"].ToString());
                    string TVal = ds.Tables[0].Rows[i]["StationName"].ToString();
                    if (!sationNames.ContainsKey(keyVal))
                        sationNames.Add(keyVal, TVal);
                }
                bool HaveUUT = false;
                //检查产品条码
                foreach (int slotNo in uut.Keys)
                {
                    if (uut[slotNo].serialNo != "")
                    {
                        if (uut[slotNo].result > 0) //产品不良
                        {
                            if (ForceIn && uut[slotNo].flowId == curStationId) //允许重测
                            {
                                HaveUUT = true;
                                uut[slotNo].result = 0;
                                uut[slotNo].flowCode = EFlowCode.正常流程;
                            }
                            else
                            {
                                if (chkRepeat) //允许同站不良重测
                                {
                                    HaveUUT = true;
                                    uut[slotNo].result = 0;
                                    uut[slotNo].flowCode = EFlowCode.正常流程;
                                }
                                else
                                {
                                    if (sationNames.ContainsKey(uut[slotNo].flowId))
                                        uut[slotNo].failInfo = "产品不良:" + sationNames[uut[slotNo].flowId] + "_FAIL-" + uut[slotNo].result.ToString();
                                    else
                                        uut[slotNo].failInfo = "产品不良:" + uut[slotNo].flowId.ToString() + "_FAIL-" + uut[slotNo].result.ToString();
                                    uut[slotNo].flowCode = EFlowCode.产品不良;
                                }
                            }
                        }
                        else
                        {
                            HaveUUT = true;
                            if (uut[slotNo].flowId == curStationId - 1) //产品到当前站别   
                            {
                                uut[slotNo].flowCode = EFlowCode.正常流程;
                            }
                            else if (uut[slotNo].flowId < curStationId - 1) //未到当前站别   
                            {
                                if (!ForceIn)
                                {
                                    if (sationNames.ContainsKey(uut[slotNo].flowId))
                                        uut[slotNo].failInfo = "流程错误:当前站别为[" + sationNames[uut[slotNo].flowId] + "]";
                                    else
                                        uut[slotNo].failInfo = "流程错误:当前站别为[" + uut[slotNo].flowId.ToString() + "]";
                                    uut[slotNo].flowCode = EFlowCode.流程错误;
                                    fixFlowCode = EFlowCode.流程错误;
                                    er = uut[slotNo].failInfo;
                                }
                                else
                                {
                                    uut[slotNo].flowCode = EFlowCode.正常流程;
                                }
                            }
                            else if (uut[slotNo].flowId > curStationId - 1)
                            {
                                if (ForceIn && uut[slotNo].flowId == curStationId) //允许重测   
                                {
                                    uut[slotNo].flowCode = EFlowCode.正常流程;
                                }
                                else
                                {
                                    if (chkRepeat)  //允许过站重测
                                    {
                                        uut[slotNo].flowCode = EFlowCode.正常流程;
                                    }
                                    else
                                    {
                                        if (sationNames.ContainsKey(uut[slotNo].flowId))
                                            uut[slotNo].failInfo = "该站已测试,当前站别为[" + sationNames[uut[slotNo].flowId] + "]";
                                        else
                                            uut[slotNo].failInfo = "该站已测试,当前站别为[" + uut[slotNo].flowId.ToString() + "]";
                                        uut[slotNo].flowCode = EFlowCode.流程过站;
                                        fixFlowCode = EFlowCode.流程过站;
                                        er = uut[slotNo].failInfo;
                                    }
                                }
                            }
                        }

                    }
                }
                if (!HaveUUT)
                {
                    er = "该治具无可测试产品,请检查";
                    fixFlowCode = EFlowCode.产品不良;
                    return false;
                }
                if (fixFlowCode != EFlowCode.正常流程)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 修改测试结果
        /// </summary>
        /// <param name="para"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool updateFixResult(int curStationId, string idCard, List<String> serialNos, List<int> result, ref string er, string curStationName = "", string failInfo = "", bool updateFixNull = false)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                      "autoflow", userName, password);

                string sqlCmd = string.Empty;

                List<string> sqlCmdList = new List<string>();

                if (curStationId == 1)
                {
                    string flowStartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    for (int i = 0; i < result.Count; i++)
                    {
                        string strTestTime = string.Empty;
                        if (result[i] == 0)
                            strTestTime = "curTestTimes=0";
                        else
                            strTestTime = "curTestTimes=curTestTimes+1";
                        if (!updateFixNull)
                            sqlCmd = "update udtFixture Set curStationId=" + curStationId + ",curStationName='" + curStationName +
                                     "',curResult=" + result[i] + "," + strTestTime + ",curFailInfo='" + failInfo +
                                     "',flowStartTime='" + flowStartTime + "'" +
                                     " where idCard='" + idCard + "' and slotNo=" + i + " and fixIsNull<>1";
                        else
                            sqlCmd = "update udtFixture Set curStationId=" + curStationId + ",curStationName='" + curStationName +
                                     "',curResult=" + result[i] + "," + strTestTime + ",curFailInfo='" + failInfo +
                                     "',flowStartTime='" + flowStartTime + "'" +
                                     " where idCard='" + idCard + "' and slotNo=" + i;
                        sqlCmdList.Add(sqlCmd);
                    }
                }
                else
                {

                    DataSet ds = new DataSet();
                    sqlCmd = "select * from stationmap";
                    if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                        return false;
                    if (ds.Tables[0].Rows.Count == curStationId) //末尾站
                    {
                        string flowEndTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        for (int i = 0; i < result.Count; i++)
                        {
                            if (serialNos[i] != string.Empty)
                            {
                                string strTestTime = string.Empty;
                                if (result[i] == 0)
                                    strTestTime = "curTestTimes=0";
                                else
                                    strTestTime = "curTestTimes=curTestTimes+1";
                                if (!updateFixNull)
                                    sqlCmd = "update udtFixture Set curStationId=" + curStationId + ",curStationName='" + curStationName +
                                                 "',curResult=" + result[i] + "," + strTestTime + ",curFailInfo='" + failInfo +
                                                 "',flowEndTime='" + flowEndTime + "'" +
                                                 " where idCard='" + idCard + "' and slotNo=" + i + " and  fixIsNull<>1";
                                else
                                    sqlCmd = "update udtFixture Set curStationId=" + curStationId + ",curStationName='" + curStationName +
                                                 "',curResult=" + result[i] + "," + strTestTime + ",curFailInfo='" + failInfo +
                                                 "',flowEndTime='" + flowEndTime + "'" +
                                                 " where idCard='" + idCard + "' and slotNo=" + i;
                                sqlCmdList.Add(sqlCmd);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            if (serialNos[i] != string.Empty)
                            {
                                string strTestTime = string.Empty;
                                if (result[i] == 0)
                                    strTestTime = "curTestTimes=0";
                                else
                                    strTestTime = "curTestTimes=curTestTimes+1";
                                if (!updateFixNull)
                                    sqlCmd = "update udtFixture Set curStationId=" + curStationId + ",curStationName='" + curStationName +
                                             "',curResult=" + result[i] + "," + strTestTime + ",curFailInfo='" + failInfo + "'" +
                                             " where idCard='" + idCard + "' and slotNo=" + i + " and  fixIsNull<>1 and curResult =" +0 ;
                                else
                                    sqlCmd = "update udtFixture Set curStationId=" + curStationId + ",curStationName='" + curStationName +
                                             "',curResult=" + result[i] + "," + strTestTime + ",curFailInfo='" + failInfo + "'" +
                                             " where idCard='" + idCard + "' and slotNo=" + i + " and curResult =" + 0;
                                sqlCmdList.Add(sqlCmd);

                            }
                        }
                    }
                }
                return db.excuteSQLArray(sqlCmdList, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 获取当前产品条码和结果
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="result"></param>
        /// <param name="flowCode"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool getCurResultFromFix(int curStationId, string idCard, ref CFIX fix, ref string er)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                      "autoflow", userName, password);
                string sqlCmd = string.Empty;

                DataSet ds = new DataSet();

                sqlCmd = "select * from udtFixture where idCard='" + idCard + "' order by slotNo";

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    er = "治具ID[" + idCard + "]不存在,请重新绑定";
                    return false;
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    fix.fixIsNull = System.Convert.ToInt32(ds.Tables[0].Rows[i]["fixIsNull"].ToString());
                    fix.serialNo.Add(ds.Tables[0].Rows[i]["serialNo"].ToString());
                    fix.flowId.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["curStationId"].ToString()));
                    fix.flowName.Add(ds.Tables[0].Rows[i]["curStationName"].ToString());
                    fix.result.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["curResult"].ToString()));
                    fix.testTimes.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["curTestTimes"].ToString()));
                    fix.failInfo.Add(ds.Tables[0].Rows[i]["curFailInfo"].ToString());
                }

                //获取流程站别名
                Dictionary<int, string> sationNames = new Dictionary<int, string>();

                ds = new DataSet();
                sqlCmd = "select * from stationMap order by StationId";
                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    er = "该治具站别不在流程中,请检查";
                    return false;
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int keyVal = System.Convert.ToInt32(ds.Tables[0].Rows[i]["StationId"].ToString());
                    string TVal = ds.Tables[0].Rows[i]["StationName"].ToString();
                    if (!sationNames.ContainsKey(keyVal))
                        sationNames.Add(keyVal, TVal);
                }

                //获取产品结果
                for (int i = 0; i < fix.serialNo.Count; i++)
                {
                    if (fix.serialNo[i] != "")
                    {
                        if (fix.result[i] != -1)
                        {
                            if (fix.result[i] > 0) //产品不良
                            {
                                if (sationNames.ContainsKey(fix.flowId[i]))
                                    fix.failInfo[i] = "产品不良:" + sationNames[fix.flowId[i]] + "_FAIL-" + fix.result[i].ToString();

                                else
                                    fix.failInfo[i] = "产品不良:" + fix.flowId[i].ToString() + "_FAIL-" + fix.result[i].ToString();
                            }
                            else
                            {
                                if (fix.flowId[i] < curStationId) //未到当前站别   
                                {
                                    if (sationNames.ContainsKey(fix.flowId[i]))
                                        fix.failInfo[i] = "产品流程错误:当前站别为[" + sationNames[fix.flowId[i]] + "]";
                                    else
                                        fix.failInfo[i] = "产品流程错误:当前站别为[" + fix.flowId[i].ToString() + "]";
                                    fix.result[i] = curStationId;
                                }
                                else
                                    fix.failInfo[i] = "产品结果:PASS";
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 删除不良产品条码
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool clearFixFailSn(string idCard, ref string er)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                      "autoflow", userName, password);
                string sqlCmd = string.Empty;

                sqlCmd = "update udtFixture set serialNo='',curResult=0,curTestTimes=0 where curResult<>0";

                return db.excuteSQL(sqlCmd, ref er);

            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 清除治具测试站产能
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool clearStatTTNum(string idCard, ref string er)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                      "autoflow", userName, password);
                string sqlCmd = string.Empty;

                sqlCmd = "update FixtureInfo set ttNum=0,failNum=0  where idCard='" + idCard + "'";

                return db.excuteSQL(sqlCmd, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 记录治具ID测试站产能
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="statId"></param>
        /// <param name="ttNum"></param>
        /// <param name="failNum"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool updateStatTTNum(string idCard, int statId, List<string> serialNos, List<int> result, ref string er)
        {
            try
            {
                er = string.Empty;

                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                      "autoflow", userName, password);
                string sqlCmd = string.Empty;

                DataSet ds = new DataSet();

                bool IsExist = false;

                sqlCmd = "Select * from FixtureInfo where idCard='" + idCard + "' and stationId=" + statId;

                if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                    return false;

                if (ds.Tables[0].Rows.Count != 0)
                    IsExist = true;

                List<string> sqlCmdList = new List<string>();

                if (!IsExist)
                {
                    for (int i = 0; i < serialNos.Count; i++)
                    {
                        int ttNum = 0;

                        int failNum = 0;

                        if (serialNos[i] != "")
                        {
                            ttNum = 1;
                            if (result[i] == statId)
                                failNum = 1;
                        }

                        sqlCmd = "insert into FixtureInfo(idCard,slotNo,stationId,ttNum,failNum) values ('" +
                                 idCard + "'," + i.ToString() + "," + statId + "," + ttNum + "," + failNum + ")";
                        sqlCmdList.Add(sqlCmd);
                    }
                }
                else
                {
                    for (int i = 0; i < serialNos.Count; i++)
                    {
                        int ttNum = 0;

                        int failNum = 0;

                        if (serialNos[i] != "")
                        {
                            ttNum = 1;
                            if (result[i] == statId)
                                failNum = 1;
                        }

                        sqlCmd = "update FixtureInfo set ttNum=ttNum+" + ttNum + ",failNum=failNum+" + failNum +
                                 " where idCard='" + idCard + "' and stationId=" + statId + " and slotNo=" + i;
                        sqlCmdList.Add(sqlCmd);
                    }
                }
                return db.excuteSQLArray(sqlCmdList, ref er);
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 获取测试站总数和不良数
        /// </summary>
        /// <param name="statId"></param>
        /// <param name="ttNum"></param>
        /// <param name="failNum"></param>
        /// <returns></returns>
        public static bool getStatTTNum(string idCard, List<int> statId, ref List<int> ttNum, ref List<int> failNum, ref string er)
        {
            try
            {
                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                      "autoflow", userName, password);
                string sqlCmd = string.Empty;

                er = string.Empty;

                DataSet ds = null;

                for (int i = 0; i < statId.Count; i++)
                {

                    ds = new DataSet();

                    sqlCmd = "Select * from FixtureInfo where idCard='" + idCard + "' and stationId=" +
                              statId[i] + " order by failNum desc";
                    if (!db.QuerySQL(sqlCmd, ref ds, ref er))
                        return false;
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        ttNum[i] = 0;
                        failNum[i] = 0;
                    }
                    else
                    {
                        ttNum[i] = System.Convert.ToInt32(ds.Tables[0].Rows[0]["ttNum"].ToString());
                        failNum[i] = System.Convert.ToInt32(ds.Tables[0].Rows[0]["failNum"].ToString());
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 删除过期文件记录
        /// </summary>
        /// <param name="outDay"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool deleteSnRecord(int outDay, ref string er)
        {
            try
            {
                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                    "autoFlow", userName, password);
                string sqlCmd = "delete from snrecord where to_days(now()) - to_days(updateTime)>" + outDay;
                if (!db.excuteSQL(sqlCmd, ref er))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// 记录测试信息
        /// </summary>
        /// <param name="snRecord"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static bool recordSnFlow(List<CSnRecord> snRecord, ref string er)
        {
            try
            {
                CDBCom db = new CDBCom(CDBCom.EDBType.MySQL, mesDB,
                                    "autoflow", userName, password);

                List<string> sqlCmdList = new List<string>();

                string sqlCmd = string.Empty;

                string nowTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                for (int i = 0; i < snRecord.Count; i++)
                {
                    sqlCmd = "delete from snrecord where serialNo='" + snRecord[i].serialNo + "'";
                    sqlCmdList.Add(sqlCmd);
                }

                for (int i = 0; i < snRecord.Count; i++)
                {
                    sqlCmd = "insert into snrecord(serialNo,idCard,slotNo,statId,statName,startTime,endTime," +
                           "testTime,testResult,testData,updateTime)" +
                           " values ('" + snRecord[i].serialNo + "','" + snRecord[i].idCard + "'," + snRecord[i].slotNo + "," +
                           snRecord[i].statId + ",'" + snRecord[i].statName + "','" + snRecord[i].startTime + "','" +
                           snRecord[i].endTime + "'," + snRecord[i].testTime + "," + snRecord[i].testResult + ",'" +
                           snRecord[i].testData + "','" + nowTime + "')";
                    sqlCmdList.Add(sqlCmd);
                }
                if (!db.excuteSQLArray(sqlCmdList, ref er))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                er = ex.ToString();
                return false;
            }
        }
    }

}
