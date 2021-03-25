using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock
{
    public abstract class ParamsArrayLogicBlock: LogicBlock
    {
        public LogicBlock [] m_values;

        public ParamsArrayLogicBlock(params LogicBlock[] values)
        {
            m_values = values;
        }

        public override void Get(out bool value, out bool computed)
        {
            computed = false;
            value = false;
            bool[] comptedValues = new bool[m_values.Length];

            for (int i = 0; i < m_values.Length; i++)
            {
                m_values[i].Get(out comptedValues[i], out computed);

                if (!computed)
                    return;

            }

            value = ComputedBoolean( ref comptedValues);

            computed = true;
        }

        public abstract bool ComputedBoolean( ref bool [] values);
    }
}
