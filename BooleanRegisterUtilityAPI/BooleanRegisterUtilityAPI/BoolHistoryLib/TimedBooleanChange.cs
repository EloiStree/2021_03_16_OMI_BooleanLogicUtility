using BooleanRegisterUtilityAPI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolHistoryLib
{
    public class TimedBooleanChange : BooleanChange
    {
        DateTime m_time;
        public TimedBooleanChange(BooleanChangeType changeType, DateTime time) : base(changeType)
        {
            m_time = time;
        }
        public TimedBooleanChange(BooleanChangeType changeType) : base(changeType)
        {
            m_time = DateTime.Now;
        }

        public DateTime GetTime() { return m_time; }
    }

}
