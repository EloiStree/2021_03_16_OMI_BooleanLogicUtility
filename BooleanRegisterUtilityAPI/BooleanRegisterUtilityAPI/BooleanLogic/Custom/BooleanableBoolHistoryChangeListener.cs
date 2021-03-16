using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.BoolHistoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BooleanLogic.Custom
{
    public class BooleanableBoolHistoryChangeListener : BooleanableSwitchRecently
    {

        private BoolHistory m_target;

        public BooleanableBoolHistoryChangeListener(BoolHistory target, BoolSwitchType type, float timeObservedInSecond)
            : base(type, timeObservedInSecond)
        {
            m_target = target;
        }

        public override void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            value = false;
            wasBooleanable = false;

            if (m_target == null) return;

            if (base.GetSwitchType() == BoolSwitchType.SwtichToTrue)
                value = m_target.WasSetTrue(GetTimeObservedInSecond(), true);
            if (base.GetSwitchType() == BoolSwitchType.SwitchToFalse)
                value = m_target.WasSetFalse(GetTimeObservedInSecond(),true);
            wasBooleanable = true;
        }

        public BoolHistory GetLinkedHistory()
        {
            return m_target;
        }
    }
}
