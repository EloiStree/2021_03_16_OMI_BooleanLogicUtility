using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Unstore
{
    public class BL_RelativeTimeFromNow : IBoolTimeKey
    {
        public ITimeValue m_relativeToNow;

        public BL_RelativeTimeFromNow(ITimeValue relativeToNow)
        {
            m_relativeToNow = relativeToNow;
        }


        public void GetTime(DateTime now, out DateTime observed)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("Trk{0}", m_relativeToNow);
        }
    }
}
