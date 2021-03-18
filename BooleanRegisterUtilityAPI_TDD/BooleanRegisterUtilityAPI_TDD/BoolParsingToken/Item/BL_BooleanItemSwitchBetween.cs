using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item.Time;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Unstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item
{
    public class BL_BooleanItemSwitchBetween : BL_BooleanItemWithObservedTime
    {
        public BooleanSwitchType m_switchObserved;

        public BL_BooleanItemSwitchBetween(string boolNamedId, IBoolObservedTime observedTime, BooleanSwitchType booleanSwitchType) : base(boolNamedId, observedTime)
        {
            m_switchObserved = booleanSwitchType;
        }
        public BL_BooleanItemSwitchBetween(string boolNamedId, IBoolObservedTime observedTime, bool switchToTrue) : base(boolNamedId, observedTime)
        {
            m_switchObserved = switchToTrue ? BooleanSwitchType.SetAsTrue : BooleanSwitchType.SetAsFalse;
        }
        public override string ToString()
        {
            return string.Format(" [BOS_{0}_{1}] ", GetTargetName(), GetObservedTime().ToString());
        }
    }
}
