using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI_TDD
{
    internal class MoreDuoLogic : DoubleLogicBlock
    {
        public MoreDuoLogic(LogicBlock left, LogicBlock right) : base(left, right)
        {
        }

        public override bool ComputedBoolean(bool vl, bool vr)
        {
            return BoolOperationLogic.AMoreB(vl, vr);
        }
        public override string ToString()
        {
            return string.Format(" ( {0} > {1} ) ", m_left, m_right);
        }
    }
}