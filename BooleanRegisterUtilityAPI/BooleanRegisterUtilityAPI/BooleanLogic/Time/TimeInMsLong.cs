using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BooleanLogic.Time
{
    public struct TimeInMsLong : ITimeValue
    {
        long m_timeInMilliSeconds;
        public TimeInMsLong(long valueInMilliSeconds) { m_timeInMilliSeconds = valueInMilliSeconds; }
        public void GetAsMilliSeconds(out long value) { value = m_timeInMilliSeconds; }
        public void SetTime(long value) { m_timeInMilliSeconds = (long)value; }
        public double GetAsMilliSeconds() { return m_timeInMilliSeconds; }
        public double GetAsSeconds() { return m_timeInMilliSeconds / 1000.0; }
        public double GetAsMinutes() { return m_timeInMilliSeconds / 60000.0; }
        public double GetAsHours() { return m_timeInMilliSeconds / 3600000.0; }

        public void SetAsMilliSeconds(double valueInMs)
        {
            m_timeInMilliSeconds = (long)valueInMs;
        }
        public override string ToString()
        {
            return string.Format("T{0}ms", m_timeInMilliSeconds);
        }
    }
  
}
