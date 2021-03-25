using BooleanRegisterUtilityAPI.Enum;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemTimeCountInRange : BL_BooleanItemWithObservedTime
    {
        public ITimeValue m_timeObserved;
        public ValueDualSide m_sideType;
        public BoolState m_stateObserved;

        public BL_BooleanItemTimeCountInRange(string boolNamedId, ITimeValue timeObserved, ValueDualSide sideType, IBoolObservedTime observedTime, BoolState stateObserved) : base(boolNamedId, observedTime)
        {
            m_sideType = sideType;
            m_timeObserved = timeObserved;
            m_stateObserved = stateObserved;
        }

        public long GetMilliSeconds()
        {
            long tmp;
            m_timeObserved.GetAsMilliSeconds(out tmp);
            return tmp;
        }
        public override string ToString()
        {
            char bc = ' ';
            if (m_stateObserved == BoolState.True )
                bc = '_';
            if (m_stateObserved == BoolState.False )
                bc = '‾';
            char s = ' ';
            if (m_sideType == ValueDualSide.Less)
                s = '-';
            if (m_sideType == ValueDualSide.More)
                s = '+';

            long ms=0;
            if(m_timeObserved!=null)
            m_timeObserved.GetAsMilliSeconds(out ms);
            //⊓⊔-+
            return string.Format(" [⏱{0}{1}{2},{3}:{4}] ", bc, s, ms, GetTargetName(), GetObservedTime());
        }
    }
    public enum ObserveTimeCountType { LessThatTime, MoreThatTime }
}
