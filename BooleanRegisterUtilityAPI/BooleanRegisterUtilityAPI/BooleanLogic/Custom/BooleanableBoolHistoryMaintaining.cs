using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.BoolHistoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BooleanLogic.Custom
{
    public class BooleanableBoolHistoryMaintaining : BooleanableIsMaintaining
    {
        private BoolHistory m_target;

        public BooleanableBoolHistoryMaintaining(BoolHistory target, BoolMaintainType type, float timeObserved)
            : base(type, timeObserved)
        {
            m_target = target;
        }

        public override void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            value = false;
            wasBooleanable = false;

            if (m_target == null) return;

            bool state= m_target.GetInProgressState().GetState();
            if (state == false && base.GetMaintainType() == BoolMaintainType.MaintainTrue)
            {
                value = false;
                wasBooleanable = true;
                return;
            }
            if (state == true && base.GetMaintainType() == BoolMaintainType.MaintainFalse)
            {
                value = false;
                wasBooleanable = true;
                return;
            }

            double time = m_target.GetInProgressState().GetElpasedTimeAsSecond();

            value = time < base.GetTimeObservedInSecond();
            wasBooleanable = true;
        }

        public BoolHistory GetLinkedHistory()
        {
            return m_target;
        }
    }
}
