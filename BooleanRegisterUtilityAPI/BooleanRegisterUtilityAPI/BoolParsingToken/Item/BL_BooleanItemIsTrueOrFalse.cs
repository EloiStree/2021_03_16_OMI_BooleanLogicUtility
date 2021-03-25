using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemIsTrueOrFalse : BL_BooleanItemDefault
    {
         BoolState m_value;

        public BL_BooleanItemIsTrueOrFalse(string boolNamedId) : base(boolNamedId)
        {
        }

        public BL_BooleanItemIsTrueOrFalse(string boolNamedId, BoolState value) : base(boolNamedId)
        {
            m_value = value;
        }
        public BL_BooleanItemIsTrueOrFalse(string boolNamedId, bool value) : base(boolNamedId)
        {
            m_value = value?BoolState.True: BoolState.False;
        }
        public BoolState GetObserved() { return m_value; }

        public override string ToString()
        {
            return string.Format(" [B{0},{1}] ", m_value == BoolState.True? "_" : "‾", GetTargetName());
        }
    }
}
