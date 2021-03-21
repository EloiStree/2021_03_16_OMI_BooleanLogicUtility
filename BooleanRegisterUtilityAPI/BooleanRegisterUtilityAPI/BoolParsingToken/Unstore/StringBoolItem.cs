using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.Unstore
{

    public class StringTokensRegister {

        public uint m_itemCount = 0;
        public List<StringBoolItem> m_itemRegister = new List<StringBoolItem>();

        public void AddItem(string item, out bool added, out uint count)
        {

            added = false;
            count = 0;
            if (string.IsNullOrEmpty(item))
                return;

            if (!StringBoolItem.Containt(item, ref m_itemRegister))
            {
                m_itemRegister.Add(new StringBoolItem(m_itemCount, item));
                added = true;
                count = m_itemCount;
                m_itemCount++;
            }
        }

        public string GetFromIndex(uint index)
        {
            return StringBoolItem.Search(index, ref m_itemRegister);
        }

        public int GetCount()
        {
            return m_itemRegister.Count;
        }
    }


    public class StringBoolItem
    {
        public uint m_index;
        public string m_textAssociated;

        public StringBoolItem(uint index, string textAssociated)
        {
            m_index = index;
            m_textAssociated = textAssociated;
        }

        public static String Search(uint index, ref List<StringBoolItem> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].m_index == index)
                    return list[i].m_textAssociated;
            }
            return "";
        }
        public static bool Containt(uint index, ref List<StringBoolItem> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].m_index == index)
                    return true;
            }
            return false;
        }
        public static bool Containt(string value, ref List<StringBoolItem> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].m_textAssociated == value)
                    return true;
            }
            return false;
        }
    }

}
