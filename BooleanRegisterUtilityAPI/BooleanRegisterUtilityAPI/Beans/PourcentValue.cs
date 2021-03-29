using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooleanRegisterUtilityAPI.Beans
{
    public class PourcentValue
    {
        private double m_pourcent;

        public PourcentValue(double pourcent0To1)
        {
            m_pourcent = pourcent0To1;
        }
        public PourcentValue(int pourcent0To100)
        {
            m_pourcent = pourcent0To100/100.0;
        }
        public double GetPourcentAs1To100() { return m_pourcent * 100.0; }
        public double GetPourcentAs0To1() { return m_pourcent; }
        public void SetPourcentAs1To100(double pourcent) {  m_pourcent= pourcent / 100.0; }
        public void SetPourcentAs0To1(double pourcent) {  m_pourcent= pourcent; }
    }
}
