using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item
{
    public class BL_BooleanItemIsTrueOrFalseAt : BL_BooleanItemWithObservedTime
    {
        public BoolState m_stateObserved;
        public BL_BooleanItemIsTrueOrFalseAt(string boolNamedId, IBoolObservedTime observedTime, BoolState stateObserved) : base(boolNamedId, observedTime)
        {
            m_stateObserved = stateObserved;
        }
        public BL_BooleanItemIsTrueOrFalseAt(string boolNamedId, IBoolObservedTime observedTime, bool stateObserved) : base(boolNamedId, observedTime)
        {
            m_stateObserved = stateObserved ? BoolState.True: BoolState.False;
        }
        public override string ToString()
        {
            return string.Format(" [BtfAT{2}_{0}_{1}] ", GetTargetName(), GetObservedTime().ToString(), m_stateObserved == BoolState.True);
        }

    }
}
