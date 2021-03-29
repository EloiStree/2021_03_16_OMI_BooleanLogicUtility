using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooleanRegisterUtilityAPI.RegisterRefBlock
{
    class RegisterRefBoolExistAtBlock : AbstractRegisterRefBlock
    {
        private BL_BooleanItemExistAt m_booleanItemExistAt;

        public RegisterRefBoolExistAtBlock(RefBooleanRegister defaultregister, BL_BooleanItemExistAt booleanItemExistAt) : base(defaultregister)
        {
            this.m_booleanItemExistAt = booleanItemExistAt;
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
            string name = m_booleanItemExistAt.GetTargetName();
            if (!IsBoolAndRegisterExist(name))
            { return; }

            IBooleanHistory history;
            bool historyExist;
            base.m_defaultregister.GetRef().GetHistoryAccess(name, out history, out historyExist);
            if (history == null)
            { return; }

            computed = true;

            IBoolObservedTime time = m_booleanItemExistAt.GetObservedTime();

            if (time == null || (time.GetTimeKey() == null && time.GetTimeRange() == null)) {
                if ( m_booleanItemExistAt.GetBoolAsValue())
                    value= true;
                else 
                    value=false;
                return;
            }

            if (time.GetTimeKey() != null)
            {

                DateTime t;
                time.GetTimeKey().GetTime(when, out t);
                bool isInRange = history.IsInRange(when, t);
                if (isInRange ==true && m_booleanItemExistAt.GetBoolAsValue()==true)
                {
                    value = true;
                    return;
                }
                if (isInRange ==false && m_booleanItemExistAt.GetBoolAsValue()==false)
                {
                    value = true;
                    return;
                }
                return;
            }
            else if (time.GetTimeRange() != null)
            {

                DateTime t1, t2;
                time.GetTimeRange().GetTime(when, out t1, out t2);

                bool isInRange = history.IsInRange(when, t2);
              

                if (isInRange == true && m_booleanItemExistAt.GetBoolAsValue() == true)
                {
                    value = true;
                    return;
                }
                if (isInRange == false && m_booleanItemExistAt.GetBoolAsValue() == false)
                {
                    value = true;
                    return;
                }
                return;

            }
            if (time == null || (time.GetTimeKey() == null && time.GetTimeRange() == null))
                throw new Exception("If you create a State AT you must check time. Else use RefStateBlock that us more direct access to the data.");
        }


        public override string ToString()
        {
            return m_booleanItemExistAt.ToString();
        }
    }
}
