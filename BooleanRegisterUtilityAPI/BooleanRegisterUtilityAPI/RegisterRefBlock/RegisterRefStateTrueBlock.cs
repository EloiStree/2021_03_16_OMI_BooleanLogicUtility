using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI
{
    public class RegisterRefStateTrueBlock : AbstractRegisterRefBlock
    {
        private BL_BooleanItemDefault m_booleanItemDefault;

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
            string name = m_booleanItemDefault.GetTargetName();
            if (!IsBoolAndRegisterExist(name))
            { value = false; computed = false; return; }


            value = m_defaultregister.GetRef().GetValue(name);
            computed = true;
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