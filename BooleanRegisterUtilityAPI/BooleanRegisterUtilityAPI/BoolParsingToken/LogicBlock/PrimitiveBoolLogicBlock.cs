using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock
{
    public class PrimitiveBoolLogicBlock : LogicBlock
    {
        bool m_boolValue;

        public PrimitiveBoolLogicBlock(bool boolValue)
        {
            m_boolValue = boolValue;
        }

        public override string ToString()
        {
            bool v, c;
            Get(out v, out c);
            return string.Format(" {0} ", v ? '1' : '0');
        }
        public void SetBool(bool value) { m_boolValue = value; }
        public bool GetBool() { return m_boolValue; }

        public override void Get(out bool value, out bool computed)
        {
            computed = true;
            value = GetBool();
        }

        public void Switch()
        {
            m_boolValue = !m_boolValue;
        }
    }
}
