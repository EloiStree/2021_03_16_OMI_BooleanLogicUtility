using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI_TDD.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI_TDD
{
    public class RegisterRefStateAtBlock : AbstractRegisterRefBlock
    {
        private BL_BooleanItemIsTrueOrFalseAt m_booleanItemIsTrueOrFalseAt;

        public RegisterRefStateAtBlock(RefBooleanRegister defaultregister, BL_BooleanItemIsTrueOrFalseAt booleanItemIsTrueOrFalse) : base(defaultregister)
        {
            this.m_booleanItemIsTrueOrFalseAt = booleanItemIsTrueOrFalse;
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
            string name = m_booleanItemIsTrueOrFalseAt.GetTargetName();
            if (!IsBoolAndRegisterExist(name))
            { return; }

            IBooleanHistory history;
            bool historyExist;
            base.m_defaultregister.GetRef().GetHistoryAccess(name, out history, out historyExist);
            if (!historyExist)
            { return; }

            IBoolObservedTime time = m_booleanItemIsTrueOrFalseAt.GetObservedTime();
            if (time.GetTimeKey() != null)
            {
                DateTime t;
                time.GetTimeKey().GetTime(when, out t);
                bool state, computedstate;
                history.GetState(out state, out computedstate, t);
                computed = computedstate;
                value = state == (m_booleanItemIsTrueOrFalseAt.m_stateObserved == BoolState.True);
                return;

            }
            else if (time.GetTimeRange() != null)
            {

                DateTime t1, t2;
                time.GetTimeRange().GetTime(when, out t1, out t2);

                bool state, computedstate;
                history.WasMaintainedTrue(out state, out computedstate, t1, t2);
                computed = computedstate;
                value = state == (m_booleanItemIsTrueOrFalseAt.m_stateObserved == BoolState.True);
                return;

            }
        }


        public override string ToString()
        {
            return m_booleanItemIsTrueOrFalseAt.ToString();
        }
    }
}