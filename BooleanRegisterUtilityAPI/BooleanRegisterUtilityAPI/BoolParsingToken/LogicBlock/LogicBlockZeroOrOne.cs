using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock
{
    public class LogicBlockZeroOrOne : LogicBlock
    {
        public bool  m_value;

        public LogicBlockZeroOrOne(BoolState value)
        {
            m_value = value== BoolState.True;
        }
        public LogicBlockZeroOrOne(ushort value)
        {
            m_value = value == 1;
        }
        public LogicBlockZeroOrOne(bool value)
        {
            m_value = value;
        }

        public override void Get(out bool value, out bool computed)
        {
            computed = true;
            value = m_value; 
        }

        public override string ToString()
        {
            return m_value ? "1" : "0";
        }
    }
}
