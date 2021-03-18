using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock
{
    public abstract class DoubleLogicBlock : LogicBlock
    {
        public LogicBlock m_left;
        public LogicBlock m_right;

        protected DoubleLogicBlock(LogicBlock left, LogicBlock right)
        {
            m_left = left;
            m_right = right;
        }

        public override void Get(out bool value, out bool computed)
        {
            value = false;
            bool vl;
            m_left.Get(out vl, out computed);

            if (!computed)
                return;

            bool vr;
            m_right.Get(out vr, out computed);

            if (!computed)
                return;

            value = ComputedBoolean(vl, vr);
             
        }

        public abstract bool ComputedBoolean(bool vl, bool vr);
    }
}
