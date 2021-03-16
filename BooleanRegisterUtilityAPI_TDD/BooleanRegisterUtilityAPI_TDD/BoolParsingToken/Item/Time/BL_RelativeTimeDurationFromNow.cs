using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Unstore
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
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return string.Format("Trd{0}-{1}", m_relativeToNowNearest, m_relativeToNowFarest);
        }
    }
}
