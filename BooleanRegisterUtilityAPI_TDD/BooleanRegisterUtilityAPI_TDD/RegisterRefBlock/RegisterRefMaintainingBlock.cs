using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI_TDD.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI_TDD
{
    public class RegisterRefMaintainingBlock : RegisterRefStateAtBlock
    {
        public RegisterRefMaintainingBlock(RefBooleanRegister defaultregister, BL_BooleanItemMaintaining m) :
            base(defaultregister, new BL_BooleanItemIsTrueOrFalseAt(m.GetTargetName(), m.GetObservedTime(), m.m_switchObserved))
        { }
    }
}