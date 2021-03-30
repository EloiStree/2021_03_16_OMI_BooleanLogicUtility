using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.RegisterRefBlock
{
    public class RegisterRefStartFinishInRange : AbstractRegisterRefBlock
    {
        private BL_BooleanItemStartFinish m_observed;


        public RegisterRefStartFinishInRange(RefBooleanRegister defaultregister, BL_BooleanItemStartFinish observed) : base(defaultregister)
        {
            m_observed = observed;
        }


        public override void Get(out bool value, out bool computed, DateTime when)
        {
            value = false;
            computed = false;
            string name = m_observed.GetTargetName();
            if (!IsBoolAndRegisterExist(name))
            { return; }

            IBooleanHistory history;
            bool historyExist;
            base.m_defaultregister.GetRef().GetHistoryAccess(name, out history, out historyExist);
            if (!historyExist)
            { return; }

            IBoolObservedTime time = m_observed.GetObservedTime();
            if (!time.IsDefined())
            {
                return;

            }


            DateTime near, far;
            near = far = when;
            if (time.GetTimeKey() != null)
            {
                near = when;
                time.GetTimeKey().GetTime(when, out far);

            }
            else if (time.GetTimeRange() != null)
            {

                time.GetTimeRange().GetTime(when, out near, out far);
            }
           

                history.StartAndFinishState(m_observed.GetStartValue(),m_observed.GetEndValue(), out value,
                   when, near, far);
               
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
            return m_observed.ToString();
        }
    }
}
