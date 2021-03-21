using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI
{
    public class LessDuoLogic : DoubleLogicBlock
    {
        public LessDuoLogic(LogicBlock left, LogicBlock right) : base(left, right)
        {
        }

        public override bool ComputedBoolean(bool vl, bool vr)
        {
            return BoolOperationLogic.ALessB(vl, vr);
        }
        public override string ToString()
        {
            return string.Format(" ( {0} < {1} ) ", m_left, m_right);
        }
    }
}