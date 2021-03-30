using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Builder;

namespace BooleanRegisterUtilityUnitTDD
{
    internal class LinkLogicBuilderToRegister
    {
        private RefBooleanRegister refRegister;
        private BL_BuilderElements elements;

        public LinkLogicBuilderToRegister(RefBooleanRegister refRegister, BL_BuilderElements elements)
        {
            this.refRegister = refRegister;
            this.elements = elements;
        }
    }
}