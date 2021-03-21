using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.Unstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemSwitchBetween : BL_BooleanItemWithObservedTime
    {
        public BooleanSwitchType m_switchObserved;
        public SwitchTrackedType m_switchType;

        public BL_BooleanItemSwitchBetween(string boolNamedId, SwitchTrackedType switchType,  IBoolObservedTime observedTime, BooleanSwitchType booleanSwitchType) : base(boolNamedId, observedTime)
        {
            m_switchType = switchType;
            m_switchObserved = booleanSwitchType;
        }
        public BL_BooleanItemSwitchBetween(string boolNamedId, SwitchTrackedType switchType, IBoolObservedTime observedTime, bool switchToTrue) : base(boolNamedId, observedTime)
        {
            m_switchType = switchType;
            m_switchObserved = switchToTrue ? BooleanSwitchType.SetAsTrue : BooleanSwitchType.SetAsFalse;
        }
        public override string ToString()
        {
            return string.Format(" [BOS_{0}_{1}] ", GetTargetName(), GetObservedTime().ToString());
        }
    }

    public enum SwitchTrackedType { SwitchRecently, SwitchAndStayActive }
}
