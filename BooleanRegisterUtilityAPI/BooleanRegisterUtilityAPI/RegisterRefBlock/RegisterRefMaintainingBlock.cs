using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System;

namespace BooleanRegisterUtilityAPI
{
    public class RegisterRefMaintainingBlock : RegisterRefStateAtBlock
    {
        public RegisterRefMaintainingBlock(RefBooleanRegister defaultregister, BL_BooleanItemMaintaining m) :
            base(defaultregister, new BL_BooleanItemIsTrueOrFalseAt(m.GetTargetName(), m.GetObservedTime(), m.m_switchObserved))
        { }
    }
}