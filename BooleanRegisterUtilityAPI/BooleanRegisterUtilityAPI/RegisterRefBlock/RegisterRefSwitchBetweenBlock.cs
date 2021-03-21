using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI
{
    public class RegisterRefSwitchBetweenBlock : AbstractRegisterRefBlock
    {
        private BL_BooleanItemSwitchBetween m_booleanItemSwitchBetween;

        public RegisterRefSwitchBetweenBlock(RefBooleanRegister defaultregister, BL_BooleanItemSwitchBetween booleanItemSwitchBetween): base(defaultregister)
        {
            this.m_booleanItemSwitchBetween = booleanItemSwitchBetween;
        }
        public override bool IsTimeNotUsefulForComputing()
        {
            return true;
        }
        public override void Get(out bool value, out bool computed)
        {
            Get(out value, out computed, DateTime.Now);
        }
        public override void Get(out bool value, out bool computed, DateTime when)
        {
            value = false;
            computed = false;
            string name = m_booleanItemSwitchBetween.GetTargetName();
            if (!IsBoolAndRegisterExist(name))
            { return; }

            IBooleanHistory history;
            bool historyExist;
            base.m_defaultregister.GetRef().GetHistoryAccess(name, out history, out historyExist);
            if (!historyExist || history == null)
            { return; }

            IBoolObservedTime time = m_booleanItemSwitchBetween.GetObservedTime();
            if (time.GetTimeKey() != null)
            {
                throw new System.Exception("No able to compute as switch happend on range of time");
            }
            else if (time.GetTimeRange() != null)
            {

                DateTime t1, t2;
                time.GetTimeRange().GetTime(when, out t1, out t2);

                if (m_booleanItemSwitchBetween.m_switchObserved == BooleanSwitchType.SetAsTrue)
                {
                    history.WasSwitchToTrue(out value, out computed, t1, t2);
                    return;
                }
                if (m_booleanItemSwitchBetween.m_switchObserved == BooleanSwitchType.SetAsFalse)
                {
                    history.WasSwitchToFalse(out value, out computed, t1, t2);
                    return;
                }

            }
        }

        public override string ToString()
        {
            return m_booleanItemSwitchBetween.ToString();
        }
    }
}