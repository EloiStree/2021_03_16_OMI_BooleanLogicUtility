using BooleanRegisterUtilityAPI_TDD.LogicBlockOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock
{
    public abstract class LogicBlock
    {



        public abstract void Get(out bool value, out bool computed);

        public static object JoinString(LogicBlock[] m_values)
        {
            return string.Join(" ", m_values.Select(k => k.ToString()));
        }
    }

 
}
