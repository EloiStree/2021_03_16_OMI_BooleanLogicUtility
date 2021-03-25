using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using System.Linq;

namespace BooleanRegisterUtilityAPI
{
    public class XorLogic : ParamsArrayLogicBlock
    {
        public XorLogic(params LogicBlock[] values) : base(values)
        {
        }

        public override bool ComputedBoolean(ref bool[] values)
        {
            return BoolOperationLogic.XOR(values);
        }

        public override string ToString()
        {
            return string.Format(" [⊗ {0} ] ", string.Join(" " , m_values.Select(k=>k.ToString()).ToArray()));
        }
    }
}