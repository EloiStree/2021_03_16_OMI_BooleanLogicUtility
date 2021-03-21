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
    public class BL_BooleanItemMaintaining : BL_BooleanItemWithObservedTime
    {
        public BoolState m_switchObserved;
   

        public BL_BooleanItemMaintaining(string boolNamedId, IBoolObservedTime observedTime, BoolState booleanSwitchType) : base(boolNamedId, observedTime)
        {
            m_switchObserved = booleanSwitchType;
        }
        public BL_BooleanItemMaintaining(string boolNamedId, IBoolObservedTime observedTime, bool switchToTrue) : base(boolNamedId, observedTime)
        {
            m_switchObserved = switchToTrue ? BoolState.True : BoolState.False;
        }

        public override string ToString()
        {
            return string.Format(" [BM{2}_{0}_{1}] ", GetTargetName(), GetObservedTime().ToString(), m_switchObserved);
        }

        
    }

}
