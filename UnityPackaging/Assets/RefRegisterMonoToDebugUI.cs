using BooleanRegisterUtilityAPI.Beans;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefRegisterMonoToDebugUI : MonoBehaviour
{
    public RefRegisterMono m_target;
    public Text m_allBoolDescription;
    public int m_clampLenght=50;
    public bool m_everyFrame;
    public bool m_sometime=true;
    [Range(0.1f,1.0f)]
    public float m_someTimeRange=0.2f;
    

    // Start is called before the first frame update
    void Start()
    {
        if (m_sometime)
            InvokeRepeating("UpdateDebug", 0, m_someTimeRange);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_everyFrame)
            UpdateDebug();

    }

    private void UpdateDebug()
    {
        BooleanStateRegister register;
        m_target.GetRegister(out register);
        if (register != null) {

            if (m_allBoolDescription != null)
            {
                m_allBoolDescription.text= register.GetFullHistoryDebugText(m_clampLenght);
            }
        }

    }
}
