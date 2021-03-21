using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using System;

namespace BooleanRegisterUtilityAPI
{
    public class DominoLogic : LogicBlock
    {
        private LogicBlock[] m_mustBeTrue;
        private LogicBlock[] m_mustBeFalse;

        public DominoLogic(LogicBlock[] mustBeTrue, LogicBlock[] mustBeFalse)
        {
            this.m_mustBeTrue = mustBeTrue;
            this.m_mustBeFalse = mustBeFalse;
        }

        public override void Get(out bool value, out bool computed)
        {
            value = false;
            computed = false;
            bool valide;
            bool[] mt = new bool[m_mustBeTrue.Length], mf = new bool[m_mustBeFalse.Length];
            for (int i = 0; i < m_mustBeTrue.Length; i++)
            {
                m_mustBeTrue[i].Get(out mt[i], out valide);
                if (!valide)
                    return;
            }
            for (int i = 0; i < m_mustBeFalse.Length; i++)
            {
                m_mustBeFalse[i].Get(out mf[i], out valide);
                if (!valide)
                    return;
            }
            computed = true;
            value = BoolOperationLogic.Domino(mt, mf);
        }
      

        public override string ToString()
        {
            return string.Format(" [ {0} 🀸 {1} ] ", LogicBlock.JoinString(m_mustBeTrue), LogicBlock.JoinString(m_mustBeFalse) );
        }
    }
}