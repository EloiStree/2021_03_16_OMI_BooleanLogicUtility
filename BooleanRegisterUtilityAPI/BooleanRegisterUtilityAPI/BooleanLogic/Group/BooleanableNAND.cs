using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef
{
    public class BooleanableNAND :IBooleanableRef
    {
        public IBooleanableRef[] m_items ;

        public BooleanableNAND(params IBooleanableRef[] items)
        {
            m_items = items;
        }
        public BooleanableNAND(IEnumerable<IBooleanableRef> items)
        {
            m_items = items.ToArray();
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            BooleanableUtility.NAND(out value, out wasBooleanable, m_items);
        }
    }
}
