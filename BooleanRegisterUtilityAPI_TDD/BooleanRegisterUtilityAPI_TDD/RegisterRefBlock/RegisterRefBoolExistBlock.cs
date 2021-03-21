using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;
using BooleanRegisterUtilityAPI_TDD.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI_TDD
{
    public class RegisterRefBoolExistBlock : AbstractRegisterRefBlock
    {
        private BL_BooleanItemExist m_booleanItemExist;

        public RegisterRefBoolExistBlock(RefBooleanRegister defaultregister, BL_BooleanItemExist booleanItemExist): base(defaultregister)
        {
            this.m_booleanItemExist = booleanItemExist;
        }

        public override void Get(out bool value, out bool computed)
        {
            string name = m_booleanItemExist.GetTargetName();
            
            if (!IsRegisterExist())
            { value = false; computed = false; return; }

            computed = true;
            value = IsRegisterNameExist(name);
        }
        public override void Get(out bool value, out bool computed, DateTime when)
        {
            Get(out value, out computed);
        }

        public override bool IsNotTimeUsefulForComputing()
        {
            return false;
        }
        public override string ToString()
        {
            return m_booleanItemExist.ToString();
        }
    }
}