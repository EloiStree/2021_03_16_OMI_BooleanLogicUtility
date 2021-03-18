﻿using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI_TDD
{
    internal class NotOrDuoLogic : DoubleLogicBlock
    {
        public NotOrDuoLogic(LogicBlock left, LogicBlock right) : base(left, right)
        {
        }

        public override bool ComputedBoolean(bool vl, bool vr)
        {
            return BoolOperationLogic.NOR(vl, vr);
        }
        public override string ToString()
        {
            return string.Format("! ( {0} | {1} ) ", m_left,m_right);
        }
    }
}