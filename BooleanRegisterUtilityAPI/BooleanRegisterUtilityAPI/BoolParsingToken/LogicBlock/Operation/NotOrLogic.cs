using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI
{
    public class NotOrLogic : ParamsArrayLogicBlock
    {
        public NotOrLogic(params LogicBlock[] values) : base(values)
        {
        }

        public override bool ComputedBoolean(ref bool[] values)
        {
            return BoolOperationLogic.NOR(values);
        }
        public override string ToString()
        {
            return string.Format("! [| {0} ] ", LogicBlock.JoinString(m_values));
        }
    }
}