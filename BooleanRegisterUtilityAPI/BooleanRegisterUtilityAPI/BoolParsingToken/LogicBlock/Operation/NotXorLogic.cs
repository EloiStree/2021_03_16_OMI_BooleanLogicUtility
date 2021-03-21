using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI
{
    public class NotXorLogic : ParamsArrayLogicBlock
    {
        public NotXorLogic(params LogicBlock[] values) : base(values)
        {
        }

        public override bool ComputedBoolean(ref bool[] values)
        {
            return BoolOperationLogic.NXOR(values);
        }
        public override string ToString()
        {
            return string.Format("! [⊗ {0} ] ", LogicBlock.JoinString(m_values));
        }
    }
}