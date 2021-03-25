using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.LogicBlockOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock
{
    public abstract class LogicBlock : ILogicBlock
    {

        public abstract void Get(out bool value, out bool computed);

        public static object JoinString(LogicBlock[] m_values)
        {
            return string.Join(" ", m_values.Select(k => k.ToString()).ToArray());
        }
    }

 
}
