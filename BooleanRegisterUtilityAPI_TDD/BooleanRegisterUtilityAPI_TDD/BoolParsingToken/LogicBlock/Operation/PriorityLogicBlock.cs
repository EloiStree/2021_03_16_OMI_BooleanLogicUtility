
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.LogicBlockOperation
{
    public class PriorityLogicBlock : LogicBlock
    {
        LogicBlock m_target;

        public PriorityLogicBlock(LogicBlock target)
        {
            m_target = target;
        }
        public PriorityLogicBlock(ParamsArrayLogicBlock target)
        {
            m_target = target;
        }

        public override void Get(out bool value, out bool computed)
        {
            m_target.Get(out value, out computed);
        }

        public override string ToString()
        {
            return string.Format(" ( {0} ) ", m_target.ToString());
        }
    }
}
