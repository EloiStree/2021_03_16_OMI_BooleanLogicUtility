using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI
{
    public class NotXorDuoLogic : DoubleLogicBlock
    {
        public NotXorDuoLogic(LogicBlock left, LogicBlock right) : base(left, right)
        {
        }

        public override bool ComputedBoolean(bool vl, bool vr)
        {
            return BoolOperationLogic.NXOR(vl, vr);
        }
        public override string ToString()
        {
            return string.Format("! ( {0} ⊗ {1} ) ", m_left, m_right);
        }
    }
}