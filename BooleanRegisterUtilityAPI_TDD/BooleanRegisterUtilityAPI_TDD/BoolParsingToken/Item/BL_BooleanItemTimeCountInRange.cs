using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item
{
    public class BL_BooleanItemTimeCountInRange : BL_BooleanItemWithObservedTime
    {
        public ObserveTimeCountType m_observetType;
        public ITimeValue m_timeObserved;
        
        public BL_BooleanItemTimeCountInRange(string boolNamedId,ObserveTimeCountType observetType, ITimeValue timeObserved, IBoolObservedTime observedTime) : base(boolNamedId, observedTime)
        {
            m_observetType = observetType;
            m_timeObserved = timeObserved;
        }

    }
    public enum ObserveTimeCountType { LessThatTime, MoreThatTime }
}
