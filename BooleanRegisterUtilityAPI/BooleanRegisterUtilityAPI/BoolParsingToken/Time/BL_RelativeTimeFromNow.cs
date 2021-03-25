using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Unstore
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
            observed = now.AddSeconds(m_relativeToNow.GetAsSeconds());
        }

        public override string ToString()
        {
            return string.Format(" [At {0}] ", m_relativeToNow);
        }
    }
}
