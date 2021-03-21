using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item
{
    public class BL_BooleanItemPourcentStateInRange : BL_BooleanItemWithObservedTime
    {
        public BoolState m_observedState;
        public double m_pourcentIn1to100;

        public BL_BooleanItemPourcentStateInRange(string boolNamedId,BoolState observerType, double pourcentIn1to100, IBoolObservedTime observedTime) : base(boolNamedId, observedTime)
        {
            m_observedState = observerType;
            m_pourcentIn1to100 = pourcentIn1to100;
        }

    }
}
