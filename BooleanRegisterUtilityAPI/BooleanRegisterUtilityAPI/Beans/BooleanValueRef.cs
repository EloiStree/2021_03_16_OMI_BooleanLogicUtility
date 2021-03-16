using BooleanRegisterUtilityAPI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.Beans
{

    [System.Serializable]
    public class BooleanValueRef
    {
        string m_name;
        BooleanInverseTag m_inverseTag;

        public BooleanInverseTag GetInverseTag() { return m_inverseTag; }
        public string GetName() { return m_name; }

        public override string ToString()
        {
            return GetNameWithDesciption();
        }

        public BooleanValueRef(string name, BooleanInverseTag inverseTag = BooleanInverseTag.None)
        {
            m_name = name;
            m_inverseTag = inverseTag;
        }

        public static BooleanIndexGroup GetAsGroup(List<BooleanValueRef> observedState)
        {
            return new BooleanIndexGroup(observedState.Select(k => k.GetName()).ToArray());
        }

        //private static string m_pattern = "(^[a-zA-Z]+\\s)|(\\s[a-zA-Z]+\\s)|(\\s[a-zA-Z]+$)";
        public static bool HasArrow(string txt)
        {
            return txt.IndexOf('↑') >= 0 || txt.IndexOf('↓') >= 0;

        }
        public static bool CreateFrom(string[] txt, out List<BooleanValueRef> created)
        {
            BooleanValueRef br;
            created = new List<BooleanValueRef>();
            for (int i = 0; i < txt.Length; i++)
            {
                if (CreateFrom(txt[i], out br))
                    created.Add(br);
            }
            return created.Count > 0;
        }

        public bool IsRequestingActive()
        {
            return m_inverseTag == BooleanInverseTag.None;
        }

        public bool IsRequestingInverse()
        {
            return m_inverseTag == BooleanInverseTag.Inverse;
        }

        public static bool CreateFrom(string txt, out BooleanValueRef created)
        {
            txt = txt.ToLower();
            created = null;
            txt = Regex.Replace(txt, "[^!a-zA-Z0-9\\s↑↓]", "");
            txt = txt.Trim();
            if (HasArrow(txt) || txt.Length <= 0)
                return false;
            bool inverse = txt[0] == '!';
            txt = txt.Replace("!", "");
            created = new BooleanValueRef(txt, inverse ? BooleanInverseTag.Inverse : BooleanInverseTag.None);
            return true;
        }

        public string GetNameWithDesciption()
        {
            return ((m_inverseTag == BooleanInverseTag.None) ? "" : "!") + m_name;
        }

    }
}
