using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.RegisterRefBlock
{
    public class RegisterRefBumpsInRange : AbstractRegisterRefBlock
    {
        public BL_BooleanItemBumpsInRange m_value;
        public RegisterRefBumpsInRange(RefBooleanRegister defaultregister, BL_BooleanItemBumpsInRange value) : base(defaultregister)
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
            if (!time.IsDefined())
            { return; 
            
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
            uint count;
            history.GetBumpsCount(m_value.m_bumpType, out count, when, near, far);

            if (m_value.m_observedType == ObservedBumpType.LessOrEqual && count <= m_value.m_bumpCount)
            {
                value = true;
            }
            else if (m_value.m_observedType == ObservedBumpType.Equal && count == m_value.m_bumpCount)
            {
                value = true;
            }
            else if (m_value.m_observedType == ObservedBumpType.MoreOrEqual && count >= m_value.m_bumpCount)
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
    }
}
