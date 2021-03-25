using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemWithObservedTime : BL_BooleanItem
    {
        IBoolObservedTime m_timeObserved;

        public BL_BooleanItemWithObservedTime(string boolNamedId, IBoolObservedTime observedTime) : base(boolNamedId)
        {
            m_timeObserved = observedTime;

        }


        public IBoolObservedTime GetObservedTime()
        {
            return m_timeObserved;
        }
        public override string ToString()
        {
            return string.Format(" [B{0}:{1}] ", GetTargetName(), m_timeObserved.ToString() );
        }
    }
}
