using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock
{
    public class BL_ToBeDefined : LogicBlock
    {
        public BL_BooleanItem m_item;
        public LogicBlock m_definition;

        public BL_ToBeDefined(BL_BooleanItem item)
        {
            m_item = item;
        }

        public override void Get(out bool value, out bool computed)
        {
            if (m_definition == null)
            { value = computed = false; return; }

            m_definition.Get(out value, out computed);
        }

        public void SetDefinition(LogicBlock logic) {

            m_definition = logic;
        }

        public BL_BooleanItem GetSourceForDefinition() { return m_item; }

        public override string ToString()
        {
            if(m_definition!=null)
                return  m_definition.ToString();
            return m_item.ToString() + " (ND)";
        }
    }
}
