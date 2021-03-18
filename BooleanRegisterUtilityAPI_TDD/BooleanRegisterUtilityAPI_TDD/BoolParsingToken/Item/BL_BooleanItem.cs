using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item
{
    public abstract class BL_BooleanItem
    {
        string m_boolNamedId;

        public BL_BooleanItem(string boolNamedId)
        {
            m_boolNamedId = boolNamedId;
        }

        public string GetTargetName() { return m_boolNamedId; }

        public override string ToString()
        {
            return string.Format(" [B{0}] ", GetTargetName());
        }
    }
}
