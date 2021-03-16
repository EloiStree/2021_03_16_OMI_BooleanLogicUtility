using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BooleanLogic.Time
{
    public struct TimeInMsUnsignedShort : ITimeValue
    {
        public static ushort MAXSIZE = ushort.MaxValue;
        public ushort m_timeInMilliSeconds;
        public TimeInMsUnsignedShort(ushort valueInMilliSeconds) { m_timeInMilliSeconds = valueInMilliSeconds; }
        public void SetTime(ushort value) { m_timeInMilliSeconds = (ushort)value; }
        public void GetAsMilliSeconds(out ushort value) { value = m_timeInMilliSeconds; }
        public double GetAsMilliSeconds() { return m_timeInMilliSeconds; }
        public double GetAsSeconds() { return m_timeInMilliSeconds / 1000.0; }
        public double GetAsMinutes() { return m_timeInMilliSeconds / 60000.0; }
        public double GetAsHours() { return m_timeInMilliSeconds / 3600000.0; }

        public void SetAsMilliSeconds(double valueInMs)
        {
            m_timeInMilliSeconds = (ushort) valueInMs;
        }

        public override string ToString()
        {
            return string.Format("T{0}ms", m_timeInMilliSeconds);
        }
    }
}
