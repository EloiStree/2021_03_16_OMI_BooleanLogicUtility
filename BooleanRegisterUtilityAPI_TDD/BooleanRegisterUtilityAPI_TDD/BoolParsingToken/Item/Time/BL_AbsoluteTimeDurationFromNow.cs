using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Unstore
{
    public class BL_AbsoluteTimeDurationFromNow: IBoolTimeRange
    {
        public ITimeOfDay m_timeOfDayToNowNearest;
        public ITimeOfDay m_timeOfDayToNowFarest;

        public BL_AbsoluteTimeDurationFromNow(ITimeOfDay NowNearest, ITimeOfDay NowFarest)
        {
            m_timeOfDayToNowNearest = NowNearest;
            m_timeOfDayToNowFarest = NowFarest;
        }

        public void GetTime(DateTime now, out DateTime nearestOfNow, out DateTime farestOfNow)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return string.Format("Tad{0}-{1}", m_timeOfDayToNowNearest, m_timeOfDayToNowFarest);
        }
    }
}
