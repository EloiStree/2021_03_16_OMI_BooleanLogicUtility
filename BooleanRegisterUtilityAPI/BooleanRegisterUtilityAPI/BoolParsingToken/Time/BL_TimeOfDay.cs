using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item.Time
{
    public class BL_TimeOfDay : ITimeOfDay
    {
        ushort m_hour24h;
        ushort m_minute;
        ushort m_second;
        ushort m_millisecond;
        public BL_TimeOfDay(string hour24h, string minute, string second, string millisecond)
        {
            ushort v;
            if (ushort.TryParse(hour24h, out v))
                m_hour24h = v;
            if (ushort.TryParse(minute, out v))
                m_minute = v;
            if (ushort.TryParse(second, out v))
                m_second = v;
            if (ushort.TryParse(millisecond, out v))
                m_millisecond = v;
        }
        public BL_TimeOfDay(ushort hour24h, ushort minute, ushort second, ushort millisecond)
        {
            m_hour24h = hour24h;
            m_minute = minute;
            m_second = second;
            m_millisecond = millisecond;
        }

        public ushort GetHourOn24HFromat()
        {
            return m_hour24h;
        }

        public ushort GetMilliseconds()
        {
            return m_millisecond;
        }

        public ushort GetMinutes()
        {
            return m_minute;
        }

        public ushort GetSeconds()
        {
            return m_second;
        }

        public override string ToString()
        {
            return string.Format(" [T{0}h{1}m{2}s{3}] ", GetHourOn24HFromat(), GetMinutes(), GetSeconds(), GetMilliseconds());
        }
    
    }
}
