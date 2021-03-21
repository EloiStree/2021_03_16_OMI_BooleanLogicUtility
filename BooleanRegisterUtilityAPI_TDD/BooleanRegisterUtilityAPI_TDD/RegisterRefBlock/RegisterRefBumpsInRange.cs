using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.RegisterRefBlock
{
    class RegisterRefBumpsInRange : AbstractRegisterRefBlock
    {
        public BL_BooleanItemBumpsInRange m_value;
        public RegisterRefBumpsInRange(RefBooleanRegister defaultregister, BL_BooleanItemBumpsInRange value) : base(defaultregister)
        {
            m_value = value;
        }

        public override void Get(out bool value, out bool computed, DateTime when)
        {
            throw new NotImplementedException();
        }

        public override void Get(out bool value, out bool computed)
        {
            Get(out value, out computed, DateTime.Now);
        }

        public override bool IsTimeNotUsefulForComputing()
        {
            return true;
        }
    }
}
