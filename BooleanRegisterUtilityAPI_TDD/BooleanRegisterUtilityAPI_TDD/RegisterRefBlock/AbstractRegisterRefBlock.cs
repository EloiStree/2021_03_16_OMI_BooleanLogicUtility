using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.RegisterRefBlock
{
    public abstract class AbstractRegisterRefBlock : TimeLogicBlock
    {

        protected RefBooleanRegister m_defaultregister;

        protected AbstractRegisterRefBlock(RefBooleanRegister defaultregister)
        {
            SetRefRegister(defaultregister);
        }

        public void SetRefRegister(RefBooleanRegister register) { m_defaultregister = register; }
        public bool IsBoolAndRegisterExist(string boolName) {
            return IsRegisterExist() && IsRegisterNameExist(boolName);
        }
        public bool IsRegisterExist() { return m_defaultregister != null && m_defaultregister.GetRef() != null; }
        public bool IsRegisterNameExist(string boolName) { return m_defaultregister.GetRef().Contains(boolName); }
    }
    
}
