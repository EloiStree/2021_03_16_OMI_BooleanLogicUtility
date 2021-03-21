using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI
{
    public class NotEquivalentLogic : ParamsArrayLogicBlock
    {
        public NotEquivalentLogic(params LogicBlock[] values) : base(values)
        {
        }

        public override bool ComputedBoolean(ref bool[] values)
        {
            return BoolOperationLogic.NOTEQUIVALANCE(values);
        }
        public override string ToString()
        {
            return string.Format("! [≡ {0} ] ", LogicBlock.JoinString(m_values));
        }
    }
}