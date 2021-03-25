using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Unstore
{
    public class BL_RelativeTimeDurationFromNow : IBoolTimeRange
    {
        public ITimeValue m_relativeToNowNearest;
        public ITimeValue m_relativeToNowFarest;

        public BL_RelativeTimeDurationFromNow(ITimeValue relativeToNowNearest, ITimeValue relativeToNowFarest)
        {
            m_relativeToNowNearest = relativeToNowNearest;
            m_relativeToNowFarest = relativeToNowFarest;
        }

        public void GetTime(DateTime now, out DateTime nearestOfNow, out DateTime farestOfNow)
        {
            nearestOfNow = now.AddSeconds(m_relativeToNowNearest.GetAsSeconds());
            farestOfNow = now.AddSeconds(m_relativeToNowFarest.GetAsSeconds());
        }
        public override string ToString()
        {
            return string.Format(" [{0} >-> {1}] ", m_relativeToNowNearest, m_relativeToNowFarest);
        }
    }
}
