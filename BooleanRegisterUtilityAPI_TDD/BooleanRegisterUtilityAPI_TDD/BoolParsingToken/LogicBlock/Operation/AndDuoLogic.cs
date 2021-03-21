using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;
using System;

namespace BooleanRegisterUtilityAPI_TDD
{
    public  class AndDuoLogic : DoubleLogicBlock
    {
        public AndDuoLogic(LogicBlock left, LogicBlock right) : base(left, right)
        {
        }

        public override bool ComputedBoolean(bool vl, bool vr)
        {
            return BoolOperationLogic.AND(vl, vr);
        }

      

        public override string ToString()
        {
            return string.Format(" ( {0} + {1} ) ", m_left, m_right);
        }
    }
}