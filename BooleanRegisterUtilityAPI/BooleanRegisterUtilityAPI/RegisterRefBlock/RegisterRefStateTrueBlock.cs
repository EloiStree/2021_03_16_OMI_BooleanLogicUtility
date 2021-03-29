using BooleanRegisterUtilityAPI.BooleanLogic;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI
{
    public class RegisterRefStateTrueBlock : AbstractRegisterRefBlock
    {
        private BL_BooleanItemDefault m_booleanItemDefault;
        public IBooleanableRef m_access;

        public RegisterRefStateTrueBlock(RefBooleanRegister defaultregister, BL_BooleanItemDefault booleanItemDefault): base(defaultregister)
        {
            this.m_booleanItemDefault = booleanItemDefault;
        }

        public override bool IsTimeNotUsefulForComputing()
        {
            return false;
        }
        public override void Get(out bool value, out bool computed)
        {

            if (m_access == null)
            {
                string name = m_booleanItemDefault.GetTargetName();
                if (!IsBoolAndRegisterExist(name))
                { value = false; computed = false; return; }

                TryToInitWeightLightAccess();
            }
          
            if (m_access == null)
            { value = false; computed = false; return; }

            m_access.GetBooleanableState(out value, out computed);
        }

        private void TryToInitWeightLightAccess()
        {
            bool tmp;
            m_defaultregister.GetRef().GetFastAccess(m_booleanItemDefault.GetTargetName(), out m_access, out tmp);
            if (!tmp)
                m_access = null;
        }

        public override void Get(out bool value, out bool computed, DateTime when)
        {
            Get(out value, out computed);
        }

        public override string ToString()
        {
            return m_booleanItemDefault.ToString();
        }
    }
}