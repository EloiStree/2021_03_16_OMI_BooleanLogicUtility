using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.Beans;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCodeNamedToMonoRegister : MonoBehaviour
{

    public RefRegisterMono m_refRegister;
    public List<KeyCodeToNamedBoolean> m_keyListened = new List<KeyCodeToNamedBoolean>();


    [System.Serializable]
    public class KeyCodeToNamedBoolean {
        public KeyCode m_keyCode;
        public string m_name;
    }
   
    void Update()
    {
        IBooleanStorage register = m_refRegister.GetRef();
      //  if (register != null)
        { 
            for (int i = 0; i < m_keyListened.Count; i++)
            {
                register.Set(m_keyListened[i].m_name, Input.GetKey(m_keyListened[i].m_keyCode) );

            }
        }

    }
}
