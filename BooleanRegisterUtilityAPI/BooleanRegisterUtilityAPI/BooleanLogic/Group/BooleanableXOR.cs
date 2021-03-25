using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef
{
    public class BooleanableXOR : IBooleanableRef
    {
        public IBooleanableRef m_leftBoolean;
        public IBooleanableRef m_rightBoolean;

        public BooleanableXOR(IBooleanableRef leftBoolean, IBooleanableRef rightBoolean)
        {
            m_leftBoolean = leftBoolean;
            m_rightBoolean = rightBoolean;
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            BooleanableUtility.XOR(out value, out wasBooleanable, m_leftBoolean, m_rightBoolean);
        }
    }
}
