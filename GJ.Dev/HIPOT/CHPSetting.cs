using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJ.Dev.HIPOT
{
    public class CHPSetting
    {

        #region 高压项目定义

        public enum EStepName
        {
            AC,
            DC,
            IR,
            OSC,
            GC,
            PA
        }

        //public static string[] c_ACItem = new string[] { "VOLTAGE","HIGHT LIMIT","LOW LIMIT","ARC LIMIT",
        //                                         "RAMP TIME","TEST TIME","FALL TIME" };
        //public static string[] c_ACUnit = new string[] { "kV(0.05-5kV)","mA(0.001-10mA)","mA(0-10mA)","mA(0,1-20mA)",
        //                                         "s(0,0.1-999.9s)","s(0,0.03-999.9s)","s(0,0.1-999.9s)"};
        //public static string[] c_DCItem = new string[] { "VOLTAGE","HIGHT LIMIT","LOW LIMIT","ARC LIMIT",
        //                                                 "RAMP TIME","DWELL TIME","TEST TIME","FALL TIME" };
        //public static string[] c_DCUnit = new string[] { "kV(0.05-6kV)","mA(0.0001-5mA)","mA(0-5mA)","mA(0,1-10mA)",
        //                                                 "s(0,0.1-999.9s)","s(0,0.1-999.9s)","s(0,0.03-999.9s)","s(0,0.1-999.9s)"};
        //public static string[] c_IRItem = new string[] { "VOLTAGE","LOW LIMIT","HIGHT LIMIT",
        //                                                 "RAMP TIME","TEST TIME","FALL TIME" };
        //public static string[] c_IRUnit = new string[] { "kV(0.05-1kV)","MΩ(0.1MΩ-50GΩ)","MΩ(0-50GΩ)",
        //                                                 "s(0,0.1-999.9s)","s(0,0.3-999.9s)","s(0,0.1-999.9s)"};
        //public static string[] c_OSCItem = new string[] { "OPEN", "SHORT" };
        //public static string[] c_OSCUnit = new string[] { "%(10%-100%)", "%(0,100%-500%)" };


        public static string[] c_ACItem = new string[] { "VOLTAGE","HIGHT LIMIT","LOW LIMIT","ARC LIMIT",
                                                 "RAMP TIME","TEST TIME","OFFSET","Real HI Limit","Real LOW Limit"};
        public static string[] c_ACUnit = new string[] { "kV(0.05-5kV)","mA(0.001-10mA)","mA(0-10mA)","mA(1-9Level)",
                                                 "s(0,0.1-999.9s)","s(0,0.03-999.9s)","mA","mA","mA"};
        public static string[] c_DCItem = new string[] { "VOLTAGE","HIGHT LIMIT","LOW LIMIT","ARC LIMIT",
                                                         "RAMP TIME","TEST TIME","OFFSET","Charge LOW"};
        public static string[] c_DCUnit = new string[] { "kV(0.05-5kV)","uA(0-20000uA)","uA(0-20000uA)","uA(1-9Level)",
                                                         "s(0,0.1-999.9s)","s(0,0.03-999.9s)","uA","uA"};
        public static string[] c_IRItem = new string[] { "VOLTAGE","HIGHT LIMIT","LOW LIMIT",
                                                         "RAMP TIME","TEST TIME","DEALY TIME","OFFSET","CHARGE LOW"};
        public static string[] c_IRUnit = new string[] { "kV(0.05-1kV)","MΩ(0.1MΩ-50GΩ)","MΩ(0-50GΩ)",
                                                         "s(0,0.1-999.9s)","s(0,0.5-999.9s)","s(0.5-999.9s)","MΩ","uA"};

        public static string[] c_GCItem = new string[] { "CURRENT","VOLTAGE","LOW LIMIT","HIGHT LIMIT",
                                                         "TEST TIME" };
        public static string[] c_GCUnit = new string[] { "A(1-40A)","kV(3-8V)","MΩ(0MΩ-600MΩ)","MΩ(0-600MΩ)",
                                                         "s(0,0.5-999.9s)"};
        public static string[] c_OSCItem = new string[] { "OPEN", "SHORT" };
        public static string[] c_OSCUnit = new string[] { "%(10%-100%)", "%(0,100%-500%)" };

        /// <summary>
        /// 测试项目
        /// </summary>
        [Serializable] 
        public class CItem
        {
            public string name;
            public double setVal;
            public string unitDes;
        }
        /// <summary>
        /// 测试步骤
        /// </summary>
        [Serializable] 
        public class CStep
        {
            public int stepNo;
            public EStepName name;
            public string des;
            public List<CItem> para = new List<CItem>();
        }
        /// <summary>
        ///  项目值
        /// </summary>
        public class CVal
        {
            public EStepName name;
            public int result=0;
            public string code="";
            public string value = "";
            public string unit = string.Empty;  
        }
        /// <summary>
        /// 测试结果
        /// </summary>
        public class CStepVal
        {
            public int chanNo;
            public int result;
            public List<CVal> mVal = new List<CVal>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 增加测试项目
        /// </summary>
        /// <param name="stepName"></param>
        /// <param name="itemVal"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static CStep iniStep(EStepName stepName,int stepNo, List<double> itemVal)
        {
            try
            {

                CStep stepItem = new CStep();

                stepItem.stepNo = stepNo;  

                stepItem.name = stepName;

                switch (stepName)
                {
                    case EStepName.AC:
                        stepItem.des = "交流电压耐压(AC)测试";
                        for (int i = 0; i < c_ACItem.Length; i++)
                        {
                            if (i < itemVal.Count)
                            {
                                CItem item = new CItem();
                                item.name = c_ACItem[i];
                                item.unitDes = c_ACUnit[i];
                                item.setVal = itemVal[i];
                                stepItem.para.Add(item);
                            }
                        }
                        break;
                    case EStepName.DC:
                        stepItem.des = "直流电压耐压(DC)测试";
                        for (int i = 0; i < c_DCItem.Length; i++)
                        {
                            if (i < itemVal.Count)
                            {
                                CItem item = new CItem();
                                item.name = c_DCItem[i];
                                item.unitDes = c_DCUnit[i];
                                item.setVal = itemVal[i];
                                stepItem.para.Add(item);
                            }
                        }
                        break;
                    case EStepName.IR:
                        stepItem.des = "绝缘阻抗(IR)测试";
                        for (int i = 0; i < c_IRItem.Length; i++)
                        {
                            if (i < itemVal.Count)
                            {
                                CItem item = new CItem();
                                item.name = c_IRItem[i];
                                item.unitDes = c_IRUnit[i];
                                item.setVal = itemVal[i];
                                stepItem.para.Add(item);
                            }
                        }
                        break;
                    case EStepName.GC:
                        stepItem.des = "接地导通(GC)测试";
                        for (int i = 0; i < c_GCItem.Length; i++)
                        {
                            if (i < itemVal.Count)
                            {
                                CItem item = new CItem();
                                item.name = c_GCItem[i];
                                item.unitDes = c_GCUnit[i];
                                item.setVal = itemVal[i];
                                stepItem.para.Add(item);
                            }
                        }
                        break;
                    case EStepName.OSC:
                        stepItem.des = "开短路侦测(OS)测试";
                        stepItem.des = "绝缘阻抗(IR)测试";
                        for (int i = 0; i < c_OSCItem.Length; i++)
                        {
                            if (i < itemVal.Count)
                            {
                                CItem item = new CItem();
                                item.name = c_OSCItem[i];
                                item.unitDes = c_IRUnit[i];
                                item.setVal = itemVal[i];
                                stepItem.para.Add(item);
                            }
                        }
                        break;
                    default:
                        break;
                }

                return stepItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// 设置测试项目
        /// </summary>
        /// <param name="stepName"></param>
        /// <param name="itemVal"></param>
        /// <param name="step"></param>
        /// <param name="er"></param>
        /// <returns></returns>
        public static CStep iniStep(EStepName stepName, int stepNo)
        {
            try
            {

                CStep stepItem = new CStep();

                stepItem.stepNo = stepNo; 

                stepItem.name = stepName;

                switch (stepName)
                {
                    case EStepName.AC:
                        stepItem.des = "交流电压耐压(AC)测试";
                        for (int i = 0; i < c_ACItem.Length; i++)
                        {
                            CItem item = new CItem();
                            item.name = c_ACItem[i];
                            item.unitDes = c_ACUnit[i];
                            item.setVal = 0;
                            stepItem.para.Add(item);                            
                        }
                        break;
                    case EStepName.DC:
                        stepItem.des = "直流电压耐压(DC)测试";
                        for (int i = 0; i < c_DCItem.Length; i++)
                        {
                            CItem item = new CItem();
                            item.name = c_DCItem[i];
                            item.unitDes = c_DCUnit[i];
                            item.setVal = 0;
                            stepItem.para.Add(item);    
                        }
                        break;
                    case EStepName.IR:
                        stepItem.des = "绝缘阻抗(IR)测试";
                        for (int i = 0; i < c_IRItem.Length; i++)
                        {
                            CItem item = new CItem();
                            item.name = c_IRItem[i];
                            item.unitDes = c_IRUnit[i];
                            item.setVal = 0;
                            stepItem.para.Add(item);                            
                        }
                        break;
                    case EStepName.GC:
                        stepItem.des = "接地导通(GC)测试";
                        for (int i = 0; i < c_GCItem.Length; i++)
                        {
                            CItem item = new CItem();
                            item.name = c_GCItem[i];
                            item.unitDes = c_GCUnit[i];
                            item.setVal = 0;
                            stepItem.para.Add(item);
                        }
                        break;
                    case EStepName.OSC:
                        stepItem.des = "开短路侦测(OS)测试";
                        for (int i = 0; i < c_OSCItem.Length; i++)
                        {
                            CItem item = new CItem();
                            item.name = c_OSCItem[i];
                            item.unitDes = c_OSCUnit[i];
                            item.setVal =0;
                            stepItem.para.Add(item);
                        }
                        break;
                    default:
                        break;
                }
                return stepItem;
            }
            catch (Exception)
            {             
                return null; 
            }
        }
        #endregion

    }
}
