using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BooleanLogic.Time
{
    public class TimeInTimespan : ITimeOfDay
    {
         TimeSpan m_timeOfDay;

        public TimeInTimespan(DateTime timeOfDay)
        {
            DateTime todayMorning = DateTime.Now;
            todayMorning.AddMilliseconds(-todayMorning.Millisecond);
            todayMorning.AddSeconds(-todayMorning.Second);
            todayMorning.AddMinutes(-todayMorning.Minute);
            todayMorning.AddHours(-todayMorning.Hour);
            m_timeOfDay = (timeOfDay- todayMorning);
        }
        public TimeInTimespan(TimeSpan timeOfDay)
        {
            m_timeOfDay = timeOfDay;
        }

        public ushort GetHourOn24HFromat()
        {
            return (ushort) m_timeOfDay.Hours;
        }

        public ushort GetMilliseconds()
        {
            return (ushort)m_timeOfDay.Milliseconds;
        }

        public ushort GetMinutes()
        {
            return (ushort)m_timeOfDay.Minutes;
        }

        public ushort GetSeconds()
        {
            return (ushort)m_timeOfDay.Seconds;
        }

        public override string ToString()
        {
            return string.Format( "T{0}h{1}m{2}s{3}", GetHourOn24HFromat(), GetMinutes(), GetSeconds(), GetMilliseconds() );
        }
    }
}
