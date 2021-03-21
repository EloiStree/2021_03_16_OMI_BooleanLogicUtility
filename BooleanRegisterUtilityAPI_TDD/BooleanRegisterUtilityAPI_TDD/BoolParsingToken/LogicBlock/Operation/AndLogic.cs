using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;

namespace BooleanRegisterUtilityAPI_TDD
{
    internal class AndLogic : ParamsArrayLogicBlock
    {
        private RegisterRefStateTrueBlock registerRefStateTrueBlock;
        private RegisterRefBoolExistBlock registerRefBoolExistBlock;
        private RegisterRefStateBlock registerRefStateBlock;
        private RegisterRefStateAtBlock registerRefStateAtBlock;
        private RegisterRefMaintainingBlock registerRefMaintainingBlock;
        private RegisterRefSwitchBetweenBlock registerRefSwitchBetweenBlock;

        public AndLogic(params LogicBlock[] values) : base(values)
        {
        }

        public AndLogic(RegisterRefStateTrueBlock registerRefStateTrueBlock, RegisterRefBoolExistBlock registerRefBoolExistBlock, RegisterRefStateBlock registerRefStateBlock, RegisterRefStateAtBlock registerRefStateAtBlock, RegisterRefMaintainingBlock registerRefMaintainingBlock, RegisterRefSwitchBetweenBlock registerRefSwitchBetweenBlock)
        {
            this.registerRefStateTrueBlock = registerRefStateTrueBlock;
            this.registerRefBoolExistBlock = registerRefBoolExistBlock;
            this.registerRefStateBlock = registerRefStateBlock;
            this.registerRefStateAtBlock = registerRefStateAtBlock;
            this.registerRefMaintainingBlock = registerRefMaintainingBlock;
            this.registerRefSwitchBetweenBlock = registerRefSwitchBetweenBlock;
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