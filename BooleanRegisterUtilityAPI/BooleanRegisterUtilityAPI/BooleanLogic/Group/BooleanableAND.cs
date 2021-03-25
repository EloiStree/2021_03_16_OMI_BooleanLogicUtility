using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef
{
    public class BooleanableAND :IBooleanableRef
    {
        public IBooleanableRef[] m_items ;

        public BooleanableAND(params IBooleanableRef[] items)
        {
            m_items = items;
        }
        public BooleanableAND(IEnumerable<IBooleanableRef> items)
        {
            m_items = items.ToArray();
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            BooleanableUtility.AND(out value, out wasBooleanable, m_items);
        }
    }
}
