using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using System;

namespace BooleanRegisterUtilityAPI.RegisterRefBlock
{
    public abstract class TimeLogicBlock : LogicBlock
    {
        public abstract bool IsTimeNotUsefulForComputing();
        public abstract void Get(out bool value, out bool computed, DateTime when);

    }
}