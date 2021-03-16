using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef
{
    public class BooleanableOR : IBooleanableRef
    {
        public bool m_inverse;
        public IBooleanableRef[] m_items;

        public BooleanableOR( params IBooleanableRef[] items)
        {
            m_items = items;
        }
        public BooleanableOR(IEnumerable<IBooleanableRef> items)
        {
            m_items = items.ToArray();
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            BooleanableUtility.OR(out value, out wasBooleanable, m_items);
        }
    }
}
