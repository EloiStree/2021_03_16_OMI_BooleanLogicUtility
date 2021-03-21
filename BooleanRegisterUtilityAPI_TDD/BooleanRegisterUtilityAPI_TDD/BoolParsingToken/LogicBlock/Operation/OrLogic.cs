using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;
using System;

namespace BooleanRegisterUtilityAPI_TDD
{
    internal class OrLogic : ParamsArrayLogicBlock
    {
        public OrLogic(params LogicBlock[] values) : base(values)
        {
        }

        public override bool ComputedBoolean(ref bool[] values)
        {
            return BoolOperationLogic.OR(values);
        }
        public override string ToString()
        {
            return string.Format(" [| {0} ] ", LogicBlock.JoinString(m_values));
        }
        

    }
}