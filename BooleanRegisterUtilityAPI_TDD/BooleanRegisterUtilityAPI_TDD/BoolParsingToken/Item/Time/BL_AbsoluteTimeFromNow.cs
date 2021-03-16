using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Unstore
{
    public class BL_AbsoluteTimeFromNow : IBoolTimeKey
    {
        public ITimeOfDay m_timeOfDay;

        public BL_AbsoluteTimeFromNow(ITimeOfDay timeOfDay)
        {
            m_timeOfDay = timeOfDay;
        }

        public void GetTime(DateTime now, out DateTime observed)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return string.Format("Tak{0}", m_timeOfDay);
        }
    }
}
