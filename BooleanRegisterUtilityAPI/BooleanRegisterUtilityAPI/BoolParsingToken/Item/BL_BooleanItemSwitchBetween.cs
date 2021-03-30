using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.Unstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
            char type=' ';
            if (m_switchType == SwitchTrackedType.SwitchRecently && m_switchObserved == BooleanSwitchType.SetAsTrue)
                type = '↓';
            if (m_switchType == SwitchTrackedType.SwitchRecently && m_switchObserved == BooleanSwitchType.SetAsFalse)
                type = '↑';
            if (m_switchType == SwitchTrackedType.SwitchAndStayActive && m_switchObserved == BooleanSwitchType.SetAsTrue)
                type = '↳';
            if (m_switchType == SwitchTrackedType.SwitchAndStayActive && m_switchObserved == BooleanSwitchType.SetAsFalse)
                type = '↱';
            return string.Format(" [B{1},{0}:{2}] ", GetTargetName(), type, GetObservedTime().ToString());
        }
    }

    public enum SwitchTrackedType { SwitchRecently, SwitchAndStayActive }
}
