using BooleanRegisterUtilityAPI.BoolHistoryLib;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.Beans
{
    public class BooleanNamedHistory : INamedBooleanHistory
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

        public void WasSwitchToTrueRecently(out bool result, out bool succedToAnwser, ITimeValue timeObservedInMs)
        {
            m_boolState. WasSwitchToTrueRecently(out result, out succedToAnwser, timeObservedInMs);
        }

        public void WasSwitchToFalseRecently(out bool result, out bool succedToAnwser, ITimeValue timeObservedInMs)
        {
            m_boolState.WasSwitchToFalseRecently(out result, out succedToAnwser, timeObservedInMs);
        }

        public void WasSwitchToTrue(out bool result, out bool succedToAnwser, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasSwitchToTrue(out result, out succedToAnwser, from, to);
        }

        public void WasSwitchToFalse(out bool result, out bool succedToAnwser, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasSwitchToFalse(out result, out succedToAnwser, from, to);
        }

        public void WasSwitchToTrue(out bool result, out bool succedToAnwser, DateTime from, DateTime to)
        {
            m_boolState.WasSwitchToTrue(out result, out succedToAnwser, from, to);
        }

        public void WasSwitchToFalse(out bool result, out bool succedToAnwser, DateTime from, DateTime to)
        {
            m_boolState.WasSwitchToFalse(out result, out succedToAnwser, from, to);
        }

        public void WasMaintainedTrue(out bool result, out bool succedToAnwser, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasMaintainedTrue(out result, out succedToAnwser, from, to);
        }

        public void WasMaintainedFalse(out bool result, out bool succedToAnwser, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasMaintainedFalse(out result, out succedToAnwser, from, to);
        }

        public void WasMaintainedTrue(out bool result, out bool succedToAnwser, DateTime from, DateTime to)
        {
            m_boolState.WasMaintainedTrue(out result, out succedToAnwser, from, to);
        }

        public void WasMaintainedFalse(out bool result, out bool succedToAnwser, DateTime from, DateTime to)
        {
            m_boolState.WasMaintainedFalse(out result, out succedToAnwser, from, to);
        }

        public void GetSwitchCount(out ushort switch2True, out ushort switch2False, ITimeValue from, ITimeValue to)
        {
            m_boolState.GetSwitchCount(out switch2True, out switch2False, from,to );
        }

        public void GetSwitchCount(out ushort switch2True, out ushort switch2False, DateTime from, DateTime to)
        {
            m_boolState.GetSwitchCount(out switch2True, out switch2False, from, to);
        }

        public void GetState(out bool value, out bool succedToAnwser, ITimeValue when)
        {
            m_boolState.GetState(out value, out succedToAnwser,when );
        }

        public void GetState(out bool value, out bool succedToAnwser, DateTime when)
        {
            m_boolState.GetState(out value, out succedToAnwser,when );
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            m_boolState.GetBooleanableState(out value, out wasBooleanable );
        }

        public bool GetCurrentValue()
        {
            return m_boolState.GetState();
        }
    }
}
