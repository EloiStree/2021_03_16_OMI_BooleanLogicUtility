using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Beans;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBoolRegisterDebugMono : MonoBehaviour
{
    RefBooleanRegister m_refregister;
    BooleanStateRegister m_register;

    public List<KeyCodeToNamedBoolean> m_keyListened = new List<KeyCodeToNamedBoolean>();

    public Text m_curveDebug;
    public int m_clamp=50;

    [System.Serializable]
    public class KeyCodeToNamedBoolean {
        public KeyCode m_keyCode;
        public string m_name;
    }
    void Start()
    {

        m_register = new BooleanStateRegister();
        m_refregister = new RefBooleanRegister(m_register);
    }

    // Update is called once per frame
    void Update()
    {
        m_register.AddTimePastFromDefaultTimer();
        for (int i = 0; i < m_keyListened.Count; i++)
        {
            m_register.Set(m_keyListened[i].m_name, Input.GetKey(m_keyListened[i].m_keyCode) );

        }
        m_curveDebug.text = m_register.GetFullHistoryDebugText();

    }
}
