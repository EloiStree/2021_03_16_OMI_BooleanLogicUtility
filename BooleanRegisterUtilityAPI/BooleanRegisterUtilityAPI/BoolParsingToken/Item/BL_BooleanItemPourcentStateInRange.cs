using BooleanRegisterUtilityAPI.Enum;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemPourcentStateInRange : BL_BooleanItemWithObservedTime
    {
        public BoolState m_observedState;
        public double m_pourcentIn1to100;
        public ValueDualSide m_observedSide;

        public BL_BooleanItemPourcentStateInRange(string boolNamedId,BoolState observerType, double pourcentIn1to100, ValueDualSide observedSide, IBoolObservedTime observedTime) : base(boolNamedId, observedTime)
        {
            m_observedState = observerType;
            m_pourcentIn1to100 = pourcentIn1to100;
            m_observedSide = observedSide;
        }

        public override string ToString()
        {
            char bc = ' ';
            if (m_observedState == BoolState.True)
                bc = '_';
            if (m_observedState == BoolState.False)
                bc = '‾';
            char s = ' ';
            if (m_observedSide == ValueDualSide.Less)
                s = '-';
            if (m_observedSide == ValueDualSide.More)
                s = '+';

            //⊓⊔-+
            return string.Format(" [⏱{0}{1}{2},{3}:{4}] ", bc, s, m_pourcentIn1to100, GetTargetName(), GetObservedTime());
        }
    }
}
