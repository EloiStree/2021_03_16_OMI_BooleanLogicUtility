using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemExist : BL_BooleanItemDefault
    {
        public BoolExistanceState m_existanceCheck;

        public BL_BooleanItemExist(string boolNamedId) : base(boolNamedId)
        {
        }

        public BL_BooleanItemExist(string boolNamedId, BoolExistanceState existanceCheck) : base(boolNamedId)
        {
            m_existanceCheck = existanceCheck;
        }
        public override string ToString()
        {
            return string.Format(" [B?{0}_{1}] ", m_existanceCheck, GetTargetName());
        }
    }
}
