﻿using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;
using System.Linq;

namespace BooleanRegisterUtilityAPI_TDD
{
    internal class XorLogic : ParamsArrayLogicBlock
    {
        public XorLogic(params LogicBlock[] values) : base(values)
        {
        }

        public override bool ComputedBoolean(ref bool[] values)
        {
            return BoolOperationLogic.XOR(values);
        }

        public override string ToString()
        {
            return string.Format(" [⊗ {0} ] ", string.Join(" " , m_values.Select(k=>k.ToString())));
        }
    }
}