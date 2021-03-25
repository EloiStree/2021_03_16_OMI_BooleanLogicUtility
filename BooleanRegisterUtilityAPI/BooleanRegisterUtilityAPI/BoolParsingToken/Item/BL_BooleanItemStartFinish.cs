using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemStartFinish : BL_BooleanItemWithObservedTime
    {
        public BoolState m_start;
        public BoolState m_end;
        public BL_BooleanItemStartFinish(BoolState start ,BoolState end, string boolNamedId, IBoolObservedTime time) : base(boolNamedId,time)
        {
            m_start = start;
            m_end = end;
        }
        public override string ToString()
        {
            return string.Format(" [BST{0} {1}-{2}] ", GetTargetName(), m_start == BoolState.True ? '1' : '0', m_end == BoolState.True ? '1' : '0');
        }

        public BoolState GetStart() { return m_start; }
        public BoolState GetEnd() { return m_end; }

        public bool GetStartValue()
        {
            return m_start == BoolState.True;
        }

        public bool GetEndValue()
        {
            return m_end == BoolState.True;
        }
    }
}
