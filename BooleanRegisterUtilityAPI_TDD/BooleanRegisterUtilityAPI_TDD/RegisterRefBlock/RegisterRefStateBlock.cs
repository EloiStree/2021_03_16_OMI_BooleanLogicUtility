using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;
using BooleanRegisterUtilityAPI_TDD.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI_TDD
{
    public class RegisterRefStateBlock : AbstractRegisterRefBlock
    {
        private BL_BooleanItemIsTrueOrFalse m_booleanItemIsTrueOrFalse;

        public RegisterRefStateBlock(RefBooleanRegister defaultregister, BL_BooleanItemIsTrueOrFalse booleanItemIsTrueOrFalse): base(defaultregister)
        {
            this.m_booleanItemIsTrueOrFalse = booleanItemIsTrueOrFalse;
        }

        public override void Get(out bool value, out bool computed)
        {
            string name = m_booleanItemIsTrueOrFalse.GetTargetName();
            if (!IsBoolAndRegisterExist(name))
            { value = false; computed = false; return; }


            value = m_defaultregister.GetRef().GetValue(name) == ( m_booleanItemIsTrueOrFalse.GetObserved() == BoolState.True ? true : false );
            computed = true;
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
            return m_booleanItemIsTrueOrFalse.ToString();
        }
    }
}