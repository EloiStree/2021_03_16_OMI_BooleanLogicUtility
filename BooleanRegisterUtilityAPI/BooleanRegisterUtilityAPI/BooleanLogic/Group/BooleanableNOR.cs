using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef
{
    public class BooleanableNOR : IBooleanableRef
    {
        public bool m_inverse;
        public IBooleanableRef[] m_items;

        public BooleanableNOR( params IBooleanableRef[] items)
        {
            m_items = items;
        }
        public BooleanableNOR(IEnumerable<IBooleanableRef> items)
        {
            m_items = items.ToArray();
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            BooleanableUtility.NOR(out value, out wasBooleanable, m_items);
        }
    }
}
