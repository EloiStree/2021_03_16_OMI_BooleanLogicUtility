using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;

namespace BooleanRegisterUtilityAPI.Beans
{
    [System.Serializable]
    public class BooleanIndexGroup
    {

        string[] m_booleanNames;

        public BooleanIndexGroup(params string[] booleanNames)
        {
            m_booleanNames = booleanNames;
        }
        public string[] GetNames() { return m_booleanNames; }
        public int GetCount() { return m_booleanNames.Length; }

    }
  
}