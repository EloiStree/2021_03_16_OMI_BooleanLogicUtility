
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParseTestMono : AbstractRegisterTestMono
{
    public string[] m_toTest = new string[] { 
    "space?",
    "up_",
    "up",
     ""
    };
    public List<LogicUnitTest> m_result= new List<LogicUnitTest>();
    public List<string> m_didNotParsed = new List<string>();
    [System.Serializable]
    public class LogicUnitTest{

        public bool m_boolValue;
        public bool m_boolComputed;
        public LogicBlock m_boolLogic;
        public string m_boolLogicString;
        public string m_boolLogicStringType;
        public string m_boolLogicStringOrigine;
    }

    public override void DoTheThing()
    {
        for (int i = 0; i < m_result.Count; i++)
        {
            m_result[i].m_boolLogic.Get(out m_result[i].m_boolValue, out m_result[i].m_boolComputed);
        }
    }

    public override void Init()
    {
        Debug.Log(RegisterRefStringParser.TryToParse(m_refregister, "up↓500#2000"));
        
        for (int i = 0; i < m_toTest.Length; i++)
        {
            try
            {
                LogicBlock lb = RegisterRefStringParser.TryToParse(m_refregister, m_toTest[i]);
                if (lb != null)
                    m_result.Add(
                        new LogicUnitTest()
                        {
                            m_boolLogic = lb,
                            m_boolLogicString = lb.ToString(),
                            m_boolLogicStringOrigine = m_toTest[i]
                        ,
                            m_boolLogicStringType = lb.GetType().ToString()
                        });
                else m_didNotParsed.Add(m_toTest[i]);
            }
            catch (Exception) { Debug.Log("Hum:" + m_toTest[i]); }
        }
    }
}
