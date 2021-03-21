using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI
{
    public class XorDuoLogic : DoubleLogicBlock
    {
        public XorDuoLogic(LogicBlock left, LogicBlock right) : base(left, right)
        {
        }

        public override bool ComputedBoolean(bool vl, bool vr)
        {
            return BoolOperationLogic.XOR(vl, vr);
        }
        public override string ToString()
        {
            return string.Format(" ( {0} ⊗ {1} ) ", m_left,m_right);
        }
    }
}