
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    public long sizeTest;
    public long sizeTest2;
    [System.Serializable]
    public class LogicUnitTest{

        public bool m_boolValue;
        public bool m_boolComputed;
        public LogicBlock m_boolLogic;
        public string m_boolLogicString;
        public string m_boolLogicStringType;
        public string m_boolLogicStringOrigine;
    }
    public bool catchException=true;

    public override void DoTheThing()
    {
        for (int i = 0; i < m_result.Count; i++)
        {
            if (catchException) { 
               try
               {
                 m_result[i].m_boolLogic.Get(out m_result[i].m_boolValue, out m_result[i].m_boolComputed);
               }
               catch (Exception e) {
                   Debug.Log("Did not computed: " +e.StackTrace);
               }
            }
            else
                m_result[i].m_boolLogic.Get(out m_result[i].m_boolValue, out m_result[i].m_boolComputed);
        }
    }

    public long GetSizeOfObject(object target) {
        long size = 0;
        using (Stream s = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, target);
            size = s.Length;
        }
        return size;
    }
    public override void Init()
    {
     

        for (int i = 0; i < m_toTest.Length; i++)
        {
            try
            {
                LogicBlock lb = RegisterRefStringParser.TryToParseItem(m_refregister, m_toTest[i]);
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
            catch (Exception e) { Debug.Log("Hum," + m_toTest[i] + ":" +e.StackTrace ); }
        }
    }
}
