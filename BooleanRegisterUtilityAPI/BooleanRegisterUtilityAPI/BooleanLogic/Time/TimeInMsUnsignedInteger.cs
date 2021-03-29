using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic.Time
{
    public struct TimeInMsUnsignedInteger : ITimeValue
    {
        uint m_timeInMilliSeconds;
        public TimeInMsUnsignedInteger(uint valueInMilliSeconds) { m_timeInMilliSeconds = valueInMilliSeconds; }
        public void GetAsMilliSeconds(out uint value) { value = m_timeInMilliSeconds; }
        public void SetTime(uint value) { m_timeInMilliSeconds = (uint)value; }
        public double GetAsMilliSeconds() { return m_timeInMilliSeconds; }
        public double GetAsSeconds() { return m_timeInMilliSeconds / 1000.0; }
        public double GetAsMinutes() { return m_timeInMilliSeconds / 60000.0; }
        public double GetAsHours() { return m_timeInMilliSeconds / 3600000.0; }

        public void SetAsMilliSeconds(double valueInMs)
        {
            m_timeInMilliSeconds = (uint)valueInMs;
        }
        public override string ToString()
        {
            return string.Format("T{0}ms", m_timeInMilliSeconds);
        }
    }
  
}
