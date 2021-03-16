using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef
{
    public struct  BooleanablePrimitiveValue: IBooleanableRef
    {
        public bool m_value;

        public BooleanablePrimitiveValue(bool givenValue)
        {
            this.m_value = givenValue;
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            value = m_value;
            wasBooleanable = true;
        }
    }
}
