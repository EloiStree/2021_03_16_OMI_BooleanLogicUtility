using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item.Builder
{
    public class BL_BuilderElements
    {
        List<StringTokenTypeAndSource> m_tokens = new List<StringTokenTypeAndSource>();
        List<BL_BooleanItem> m_items = new List<BL_BooleanItem>();

        public int GetCount() { return m_items.Count; }

        public void SetItemAs(List<BL_BooleanItem> items)
        {
            m_items = items;
        }

        public void SetTokens(List<StringTokenTypeAndSource> tokens)
        {
            m_tokens = tokens;
        }

        public BL_BooleanItem[] GetItems()
        {
            return m_items.ToArray();
        }
        public StringTokenTypeAndSource[] GetTokens()
        {
            return m_tokens.ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Tokens:\n");
            for (int i = 0; i < m_tokens.Count; i++)
            {
                sb.Append(m_tokens[i]);
                sb.Append("\n");

            }
            sb.Append("Items:\n");
            for (int i = 0; i < m_items.Count; i++)
            {
                sb.Append(m_items[i]);
                sb.Append("\n");

            }
            return sb.ToString();
        }
    }
}
