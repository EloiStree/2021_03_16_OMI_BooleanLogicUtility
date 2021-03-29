using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI
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
            if (history==null)
            { return; }


            IBoolObservedTime time = m_booleanItemIsTrueOrFalseAt.GetObservedTime();
            if (time.GetTimeKey() != null)
            {

                    DateTime t;
                    time.GetTimeKey().GetTime(when, out t);

                    if (!history.IsInRange(when, t)) {
                        computed = false;
                        value = false;
                        return;
                    }
                    bool state;
                    history.GetState(out state,when, t);
                    computed = true;
                    value = state == m_booleanItemIsTrueOrFalseAt.GetBoolAsValue();
                return;
            }
            else if (time.GetTimeRange() != null)
            {

                DateTime t1, t2;
                time.GetTimeRange().GetTime(when, out t1, out t2);
             
                if (!history.IsInRange(when, t2))
                {
                    computed = false;
                    value = false;
                    return;
                }

                if (m_booleanItemIsTrueOrFalseAt.GetBoolAsValue())
                    history.WasMaintainedTrue(out value,when, t1, t2);
                else history.WasMaintainedFalse(out value, when, t1, t2);

                computed = true;
                return;

            }
            if(time==null || (time.GetTimeKey()==null && time.GetTimeRange()==null))
                throw new Exception("If you create a State AT you must check time. Else use RefStateBlock that us more direct access to the data.");
        }


        public override string ToString()
        {
            return m_booleanItemIsTrueOrFalseAt.ToString();
        }
    }
}