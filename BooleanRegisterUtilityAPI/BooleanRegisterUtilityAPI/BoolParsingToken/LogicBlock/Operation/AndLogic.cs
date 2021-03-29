using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI
{
    public class AndLogic : ParamsArrayLogicBlock
    {
        public AndLogic(params LogicBlock[] values) : base(values)
        {
        }

       

        public override bool ComputedBoolean( ref bool[] values)
        {
            return BoolOperationLogic.AND(values);
        }
        public override string ToString()
        {
            return string.Format(" [& {0} ] ", LogicBlock.JoinString(m_values));
        }
    }
}