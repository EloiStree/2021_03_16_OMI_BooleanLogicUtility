using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BLIFTDDMono : MonoBehaviour
{
    public InputField m_inputField;
    public InputField m_logicDebug;
    public InputField m_logicErrorDebug;
    public string m_currentlyTest;
    public bool m_value;
    public bool m_computed;
    public string m_error;
    public Text m_result;


    void Start()
    {
        if (m_inputField != null)
            m_inputField.onEndEdit.AddListener(Test);
        
    }

    private void Test(string arg0)
    {
        m_currentlyTest = arg0;
        BL.PreStoreLogic(m_currentlyTest);
        LogicBlock lb = BL.GetRecordedLogic(m_currentlyTest);
        if (lb == null)
        {
            m_logicDebug.text = "Not parsed";
        }
        else m_logicDebug.text = lb.ToString();

    }

    void Update()
    {
        BoolConditionResult result = BL.If(m_currentlyTest);
        m_value = result.Value;
        m_computed = result.DidCompute;
        m_error = result.ErrorStack;
        if (m_result)
            m_result.text = "" + m_value + " " + (m_computed ? "" : "e");



    }
}
