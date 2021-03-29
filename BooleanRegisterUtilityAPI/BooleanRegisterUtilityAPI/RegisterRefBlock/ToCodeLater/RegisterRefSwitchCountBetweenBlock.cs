using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI
{
    public class RegisterRefSwitchCountBetweenBlock : AbstractRegisterRefBlock
    {
        /*
        private BL_BooleanItemSwitchBetween m_booleanItemSwitchBetween;

        public RegisterRefSwitchCountBetweenBlock(RefBooleanRegister defaultregister, BL_BooleanItemSwitchBetween booleanItemSwitchBetween): base(defaultregister)
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
            if(!time.IsDefined())
            {
                return;

            }
            computed = true;
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
            if (m_booleanItemSwitchBetween.m_switchObserved == BooleanSwitchType.SetAsTrue
                && m_booleanItemSwitchBetween.m_switchType== SwitchTrackedType.SwitchRecently)
            {
                history.WasSwitchToTrue(out value, near, far);
                return;
            }
            if (m_booleanItemSwitchBetween.m_switchObserved == BooleanSwitchType.SetAsFalse
                && m_booleanItemSwitchBetween.m_switchType == SwitchTrackedType.SwitchRecently)
            {
                history.WasSwitchToFalse(out value, near, far);
                return;
            }
            if (m_booleanItemSwitchBetween.m_switchObserved == BooleanSwitchType.SetAsTrue
                && m_booleanItemSwitchBetween.m_switchType == SwitchTrackedType.SwitchAndStayActive)
            {
                history.WasSwitchToTrue(out value, near, far);
                bool valueAtEnd;
                history.GetState(out valueAtEnd, far);
                if (valueAtEnd==false)
                    value = false;

                return;
            }
            if (m_booleanItemSwitchBetween.m_switchObserved == BooleanSwitchType.SetAsFalse
                && m_booleanItemSwitchBetween.m_switchType == SwitchTrackedType.SwitchAndStayActive)
            {
                history.WasSwitchToFalse(out value, near, far); 
                bool valueAtEnd;
                history.GetState(out valueAtEnd, far);
                if (valueAtEnd == true)
                    value = false;
                return;
            }
        }

        public override string ToString()
        {
            return m_booleanItemSwitchBetween.ToString();
        }
        */
        public RegisterRefSwitchCountBetweenBlock(RefBooleanRegister defaultregister) : base(defaultregister)
        {
        }

        public override void Get(out bool value, out bool computed, DateTime when)
        {
            throw new NotImplementedException();
        }

        public override void Get(out bool value, out bool computed)
        {
            throw new NotImplementedException();
        }

        public override bool IsTimeNotUsefulForComputing()
        {
            throw new NotImplementedException();
        }
    }
}