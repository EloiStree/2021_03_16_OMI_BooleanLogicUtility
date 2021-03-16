using System;
using System.Collections;
using System.Collections.Generic;
namespace BooleanRegisterUtilityAPI.Actions
{
    [System.Serializable]
    public class SetBooleanStateAction //: BooleanStateAction
    {
         string m_booleanStateNameToAffect;
         bool m_valueToAffect;

        public SetBooleanStateAction(string registerName, string booleanStateName, bool value)
        {
            m_booleanStateNameToAffect = booleanStateName;
            m_valueToAffect = value;
        }

        public string GetBooleanVariableName()
        {
            return m_booleanStateNameToAffect;
        }

        public bool GetBooleanValue()
        {
            return m_valueToAffect;
        }

        public override string ToString()
        {
            return "[SET|" + m_booleanStateNameToAffect + "|" + m_valueToAffect + "]";
        }
    }
}