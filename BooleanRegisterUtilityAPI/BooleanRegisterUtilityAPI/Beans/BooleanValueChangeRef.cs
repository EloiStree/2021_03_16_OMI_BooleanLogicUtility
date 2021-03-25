using BooleanRegisterUtilityAPI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace BooleanRegisterUtilityAPI.Beans
{

    [System.Serializable]
    public class BooleanValueChangeRef
    {
        string m_name;
        BooleanChangeType m_changeType;
        BooleanInverseTag m_inverseTag;
        uint m_millisecondToCheck;

        public BooleanInverseTag GetInverseTag() { return m_inverseTag; }
        public string GetName() { return m_name; }
        public BooleanChangeType GetChangeType() { return m_changeType; }

        public BooleanValueChangeRef(string name, BooleanChangeType changeType, BooleanInverseTag inverseTag, uint millisecondToCheck)
        {
            m_name = name;
            m_changeType = changeType;
            m_inverseTag = inverseTag;
            m_millisecondToCheck = millisecondToCheck;
        }

        public uint GetMillisecondToCheck() { return m_millisecondToCheck; }
        //private static string m_pattern= "!+[a-zA-Z]+[↑↓]";

        public static bool HasArrow(string txt)
        {
            return txt.IndexOf('↑') >= 0 || txt.IndexOf('↓') >= 0;

        }
        public static bool CreateFrom(string[] txt, out List<BooleanValueChangeRef> created)
        {
            BooleanValueChangeRef br;
            created = new List<BooleanValueChangeRef>();
            for (int i = 0; i < txt.Length; i++)
            {
                if (CreateFrom(txt[i], out br))
                    created.Add(br);
            }
            return created.Count > 0;
        }
        private static char[] arrowsUpDown = new char[] { '↑', '↓' };
        public static bool CreateFrom(string txt, out BooleanValueChangeRef created)
        {
            txt = txt.ToLower();
            created = null;
            txt = Regex.Replace(txt, "[^!a-zA-Z0-9\\s↑↓]", "");
            txt = txt.Trim();
            if (!HasArrow(txt) || txt.Length <= 0)
                return false;
            int indexOfLastArrow = txt.LastIndexOfAny(arrowsUpDown);
            if (indexOfLastArrow < 0)
                return false;
            char arrow = txt[indexOfLastArrow];
            uint timeInMs = ConverToMs(txt.Substring(indexOfLastArrow));
            string arrowsOrder = Regex.Replace(txt, "[^↑↓]", "");


            bool inverse = txt[0] == '!';
            string word = Regex.Replace(txt, "[^a-zA-Z]", "");
            created = new BooleanValueChangeRef(word, arrow == '↑' ? BooleanChangeType.SetFalse : BooleanChangeType.SetTrue, inverse ? BooleanInverseTag.Inverse : BooleanInverseTag.None, timeInMs);
            return true;
        }

        private static uint ConverToMs(string text)
        {
            text = Regex.Replace(text, "[^0-9]", "");
            if (text.Length <= 0)
                return 50;
            return uint.Parse(text);
        }

        public override string ToString()
        {
            return GetNameWithDesciption();
        }

        public static BooleanIndexGroup GetAsGroup(List<BooleanValueRef> observedState)
        {
            return new BooleanIndexGroup(observedState.Select(k => k.GetName()).ToArray());
        }

        public string GetNameWithDesciption()
        {
            return ((m_inverseTag == BooleanInverseTag.None) ? "" : "!") + m_name + (m_changeType == BooleanChangeType.SetTrue ? '↓' : '↑');
        }

        public bool IsRequestingInverse()
        {
            return m_inverseTag == BooleanInverseTag.Inverse;
        }

        public bool IsRequestingActive()
        {
            return m_inverseTag == BooleanInverseTag.None;
        }

        public float GetSecondToCheck()
        {
            return GetMillisecondToCheck() / 1000f;
        }
    }
}
