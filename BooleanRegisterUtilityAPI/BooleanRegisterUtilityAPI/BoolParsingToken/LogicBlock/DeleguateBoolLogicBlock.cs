using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock
{
    public class DeleguateBoolLogicBlock : LogicBlock
    {

        ComputedBoolean m_action;

        public DeleguateBoolLogicBlock(ComputedBoolean boolComputation)
        {
            m_action = boolComputation;
        }

        public void SetBoolFunction(ComputedBoolean boolComputation) { m_action = boolComputation; }
        public override void Get(out bool value, out bool computed)
        {

            if (m_action != null) { 
                m_action(out value, out computed);
            }
            else {
                value = false;
                computed = false;
            }
        }
        public override string ToString()
        {
            bool v, c;
            Get(out v, out c);
            return string.Format(" {0}{1} ", v ? '1' : '0', c ? "" : "e");
        }
    }
    public delegate void ComputedBoolean(out bool value, out bool computed);
}
