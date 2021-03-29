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


            uint timeInMs = 0;
            if (time == null || !time.IsDefined())
            {
                history.GetTimeCount(
                   m_value.m_stateObserved == BoolState.True ? true : false,
                   out timeInMs);

            }
            else
            {

                DateTime near = when, far = when;
                if (time.GetTimeKey() != null)
                {
                    near = when;
                    time.GetTimeKey().GetTime(when, out far);

                }
                else if (time.GetTimeRange() != null)
                {

                    time.GetTimeRange().GetTime(when, out near, out far);
                }
                history.GetTimeCount(
                        m_value.m_stateObserved == BoolState.True ? true : false,
                        out timeInMs, when, near, far);
            }
            uint to = 0;
            m_value.m_timeObserved.GetAsMilliSeconds(out to);
            if (m_value.m_sideType == ValueDualSide.Less && timeInMs <= to)
            {
                value = true;
            }
            else if (m_value.m_sideType == ValueDualSide.More && timeInMs >= to)
            {
                value = true;
            }
            else value = false;

            computed = true;
            return;


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