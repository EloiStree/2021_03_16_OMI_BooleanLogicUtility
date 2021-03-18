using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item
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
            return string.Format(" [BOT_{0}_{1}] ", GetTargetName(), m_timeObserved.ToString() );
        }
    }
}
