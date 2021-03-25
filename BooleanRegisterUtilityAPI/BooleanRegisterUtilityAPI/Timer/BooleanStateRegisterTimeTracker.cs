using BooleanRegisterUtilityAPI.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.Timer
{
    public class BooleanStateRegisterTimeTracker
    {
        public BooleanStateRegister m_reg;
        public DateTime m_now;
        public DateTime m_previous;

      
        public BooleanStateRegisterTimeTracker(BooleanStateRegister reg)
        {
            m_now = m_previous = DateTime.Now;
               m_reg = reg;
        }

        public void AddTimePast() {
            m_now = DateTime.Now;
            m_reg.AddMilliSecondsElapsedTimeToAll((long)(m_now - m_previous).TotalMilliseconds);
            m_previous = m_now;
        }
    }
}
