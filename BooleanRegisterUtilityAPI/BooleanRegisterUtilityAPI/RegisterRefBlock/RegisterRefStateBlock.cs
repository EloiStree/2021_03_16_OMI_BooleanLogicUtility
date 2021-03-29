using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System;
using BooleanRegisterUtilityAPI.BooleanLogic;

namespace BooleanRegisterUtilityAPI
{
    public class RegisterRefStateBlock : AbstractRegisterRefBlock
    {
        private BL_BooleanItemIsTrueOrFalse m_booleanItemIsTrueOrFalse;
        IBooleanableRef m_access;

        public RegisterRefStateBlock(RefBooleanRegister defaultregister, BL_BooleanItemIsTrueOrFalse booleanItemIsTrueOrFalse): base(defaultregister)
        {
            this.m_booleanItemIsTrueOrFalse = booleanItemIsTrueOrFalse;
        }

        public override void Get(out bool value, out bool computed)
        {

            if (m_access == null)
            {
                string name = m_booleanItemIsTrueOrFalse.GetTargetName();
                if (!IsBoolAndRegisterExist(name))
                { value = false; computed = false; return; }

                TryToInitWeightLightAccess();
            }

            if (m_access == null)
            { value = false; computed = false; return; }
            bool currentValue;
            m_access.GetBooleanableState(out currentValue, out computed);
            value = currentValue == m_booleanItemIsTrueOrFalse.GetObservedAsBool();
        }

        private void TryToInitWeightLightAccess()
        {
            bool tmp;
            m_defaultregister.GetRef().GetFastAccess(m_booleanItemIsTrueOrFalse.GetTargetName(), out m_access, out tmp);
            if (!tmp)
                m_access = null;
        }


        public override void Get(out bool value, out bool computed, DateTime when)
        {
            Get(out value, out computed);
        }

        public override bool IsTimeNotUsefulForComputing()
        {
            return false;
        }

        public override string ToString()
        {
            return m_booleanItemIsTrueOrFalse.ToString();
        }
    }
}