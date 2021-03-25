using BooleanRegisterUtilityAPI.BoolHistoryLib;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
       
        public BoolHistory GetHistory() { return m_boolState; }

        public string GetName()
        {
            return m_name;
        }

      

        public bool GetCurrentValue()
        {
            return m_boolState.GetState();
        }

        public void WasSwitchTo(bool stateObserved, out bool result, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasSwitchTo( stateObserved, out  result,  from,  to);
        }

        public void WasSwitchToTrue(out bool result, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasSwitchToTrue(out  result,  from,  to);
        }

        public void WasSwitchToTrue(out bool result, DateTime now, DateTime from, DateTime to)
        {
            m_boolState.WasSwitchToTrue(out result, now, from, to);
        }

        public void WasSwitchToTrue(out bool result, DateTime from, DateTime to)
        {
            m_boolState.WasSwitchToTrue(out  result,  from,  to);
        }

        public void WasSwitchToFalse(out bool result, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasSwitchToFalse(out  result,  from,  to);
        }

        public void WasSwitchToFalse(out bool result, DateTime now, DateTime from, DateTime to)
        {
            m_boolState.WasSwitchToFalse(out  result,  now,  from,  to);
        }

        public void WasSwitchToFalse(out bool result, DateTime from, DateTime to)
        {
            m_boolState.WasSwitchToFalse(out  result,  from,  to);
        }

        public void WasMaintained(bool stateObserved, out bool result, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasMaintained( stateObserved, out  result,  from,  to);
        }

        public void WasMaintainedTrue(out bool result, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasMaintainedTrue(out  result,  from,  to);
        }

        public void WasMaintainedTrue(out bool result, DateTime now, DateTime from, DateTime to)
        {
            m_boolState.WasMaintainedTrue(out  result,  now,  from,  to);
        }

        public void WasMaintainedTrue(out bool result, DateTime from, DateTime to)
        {
            m_boolState.WasMaintainedTrue(out  result,  from,  to);
        }

        public void WasMaintainedFalse(out bool result, ITimeValue from, ITimeValue to)
        {
            m_boolState.WasMaintainedFalse(out  result,  from,  to);
        }

        public void WasMaintainedFalse(out bool result, DateTime now, DateTime from, DateTime to)
        {
            m_boolState.WasMaintainedFalse(out  result,  now,  from,  to);
        }

        public void WasMaintainedFalse(out bool result, DateTime from, DateTime to)
        {
            m_boolState.WasMaintainedFalse(out  result,  from,  to);
        }

        public void GetSwitchCount(out ushort switch2True, out ushort switch2False, ITimeValue from, ITimeValue to)
        {
            m_boolState.GetSwitchCount(out  switch2True, out  switch2False,  from,  to);
        }

        public void GetSwitchCount(out ushort switch2True, out ushort switch2False, DateTime now, DateTime from, DateTime to)
        {
            m_boolState.GetSwitchCount(out  switch2True, out  switch2False,  now,  from,  to);
        }

        public void GetSwitchCount(out ushort switch2True, out ushort switch2False, DateTime from, DateTime to)
        {
            m_boolState.GetSwitchCount(out  switch2True, out  switch2False,  from,  to);
        }

        public void GetPoucentOfState(bool stateObserved, out double pourcentState, ITimeValue from, ITimeValue to)
        {
            m_boolState.GetPoucentOfState( stateObserved, out  pourcentState,  from,  to);
        }

        public void GetPoucentOfState(bool stateObserved, out double pourcentState, DateTime now, DateTime from, DateTime to)
        {
            m_boolState.GetPoucentOfState( stateObserved, out  pourcentState,  now,  from,  to);
        }

        public void GetPoucentOfState(bool stateObserved, out double pourcentState, DateTime from, DateTime to)
        {
            m_boolState.GetPoucentOfState( stateObserved, out  pourcentState,  from,  to);
        }

        public void GetBumpsCount(AllBumpType bumb, out uint count, ITimeValue from, ITimeValue to)
        {
            m_boolState.GetBumpsCount( bumb, out  count,from,  to);
        }

        public void GetBumpsCount(AllBumpType bumb, out uint count, DateTime now, DateTime from, DateTime to)
        {
            m_boolState.GetBumpsCount( bumb, out  count,  now,  from,  to);
        }

        public void GetBumpsCount(AllBumpType bumb, out uint count, DateTime from, DateTime to)
        {
            m_boolState.GetBumpsCount( bumb, out  count,  from,  to);
        }

        public void GetTimeCount(bool stateObserved, out ulong timeFound, ITimeValue from, ITimeValue to)
        {
            m_boolState.GetTimeCount( stateObserved, out  timeFound,  from,  to);
        }

        public void GetTimeCount(bool stateObserved, out ulong timeFound, DateTime now, DateTime from, DateTime to)
        {
            m_boolState.GetTimeCount( stateObserved, out  timeFound,  now,  from,  to);
        }

        public void GetTimeCount(bool stateObserved, out ulong timeFound, DateTime from, DateTime to)
        {
            m_boolState.GetTimeCount( stateObserved, out  timeFound,  from,  to);
        }

        public void GetState(out bool value, ITimeValue when)
        {
            m_boolState.GetState(out  value,  when);
        }

        public void GetState(out bool value, DateTime now, DateTime when)
        {
            m_boolState.GetState(out  value,  now,  when);
        }

        public void GetState(out bool value, DateTime when)
        {
            m_boolState.GetState(out  value,  when);
        }

        public void GetBooleanableState(out bool value)
        {
            m_boolState.GetBooleanableState(out value);
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            m_boolState.GetBooleanableState(out value, out wasBooleanable);
        }

        public void StartAndFinishState(bool stateStart, bool statEnd, out bool result, ITimeValue from, ITimeValue to)
        {
            m_boolState.StartAndFinishState( stateStart, statEnd, out result, from, to);
        }

        public void StartAndFinishState(bool stateStart, bool statEnd, out bool result, DateTime now, DateTime from, DateTime to)
        {
            m_boolState.StartAndFinishState(stateStart, statEnd, out result,now, from, to);
        }

        public void StartAndFinishState(bool stateStart, bool statEnd, out bool result, DateTime from, DateTime to)
        {
            m_boolState.StartAndFinishState(stateStart, statEnd, out result, from, to);
        }
    }
}
