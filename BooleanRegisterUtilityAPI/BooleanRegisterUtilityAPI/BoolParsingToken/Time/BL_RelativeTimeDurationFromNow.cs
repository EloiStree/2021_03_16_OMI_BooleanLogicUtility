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
        private int v1;
        private int v2;

        public BL_RelativeTimeDurationFromNow(ITimeValue relativeToNowNearest, ITimeValue relativeToNowFarest)
        {
            m_relativeToNowNearest = relativeToNowNearest;
            m_relativeToNowFarest = relativeToNowFarest;
        }

        public BL_RelativeTimeDurationFromNow(uint relativeToNowNearest, uint relativeToNowFarest)
        {
            m_relativeToNowNearest = new TimeInMsUnsignedInteger(relativeToNowNearest);
            m_relativeToNowFarest = new TimeInMsUnsignedInteger(relativeToNowFarest); 
        }

        public void GetTime(DateTime now, out DateTime nearestOfNow, out DateTime farestOfNow)
        {
            uint t =0;
            m_relativeToNowNearest.GetAsMilliSeconds(out t);
            nearestOfNow = now.AddMilliseconds(-t);

            m_relativeToNowFarest.GetAsMilliSeconds(out t);
            farestOfNow = now.AddMilliseconds(-t);
        }
        public override string ToString()
        {
            return string.Format(" [{0} >-> {1}] ", m_relativeToNowNearest, m_relativeToNowFarest);
        }
    }
}
