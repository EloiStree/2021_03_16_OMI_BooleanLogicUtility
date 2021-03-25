using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.Enum;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.RegisterRefBlock
{
    public class RegisterRefTimecountInRange : AbstractRegisterRefBlock
    {
        public BL_BooleanItemTimeCountInRange m_value;
        public RegisterRefTimecountInRange(RefBooleanRegister defaultregister, BL_BooleanItemTimeCountInRange value) : base(defaultregister)
        {
            m_value = value;
        }
        public override void Get(out bool value, out bool computed, DateTime when)
        {
            value = false;
            computed = false;
            string name = m_value.GetTargetName();
            if (!IsBoolAndRegisterExist(name))
            { return; }

            IBooleanHistory history;
            bool historyExist;
            base.m_defaultregister.GetRef().GetHistoryAccess(name, out history, out historyExist);
            if (!historyExist)
            { return; }

            IBoolObservedTime time = m_value.GetObservedTime();
            if (time.GetTimeKey() != null)
            {
                throw new Exception("The code is design to work in range not in key");

            }
            else if (time.GetTimeRange() != null)
            {

                DateTime near, far;
                time.GetTimeRange().GetTime(when, out near, out far);

                ulong timeInMs = 0;
                history.GetTimeCount(true,  out timeInMs,when, near, far);
                ulong msObserved = (ulong)m_value.GetMilliSeconds();

                if (m_value.m_sideType == ValueDualSide.Less && timeInMs < msObserved  )
                {
                    value = true;
                }
                else if (m_value.m_sideType == ValueDualSide.More && timeInMs > msObserved  )
                {
                    value = true;
                }
                else value = false;

                computed = true;
                return;

            }
        }

        public override void Get(out bool value, out bool computed)
        {
            Get(out value, out computed, DateTime.Now);
        }

        public override bool IsTimeNotUsefulForComputing()
        {
            return true;
        }

        public override string ToString()
        {
            return m_value.ToString(); 
        }
    }
}