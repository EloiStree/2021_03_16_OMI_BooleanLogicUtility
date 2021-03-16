using BooleanRegisterUtilityAPI.BoolHistoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.Beans
{
    public class BooleanNamedHistory
    {
        string m_name = "";
        BoolHistory m_boolState;

        public BooleanNamedHistory(string name, bool startState)
        {
            m_boolState = new BoolHistory(startState);
            name = name.ToLower();
            m_name = name;
        }
        public void SetValue(bool value)
        {
            m_boolState.SetState(value);
        }
        public bool GetValue()
        {
            return m_boolState.GetState();
        }
        public bool WasSetTrue(float time, bool countCurrentState)
        {

            return m_boolState.WasSetTrue(time, countCurrentState);
        }
        public bool WasSetFalse(float time, bool countCurrentState)
        {
            return m_boolState.WasSetFalse(time, countCurrentState);
        }

        public BoolHistory GetHistory() { return m_boolState; }

        public string GetName()
        {
            return m_name;
        }
    }
}
