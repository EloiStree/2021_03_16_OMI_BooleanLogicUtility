using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.Beans
{
    [System.Serializable]
    public class BooleanGroupValueRef
    {
        List<BooleanValueRef> m_booleanTracked = new List<BooleanValueRef>();
        List<BooleanValueChangeRef> m_booleanChangeTracked = new List<BooleanValueChangeRef>();

        public BooleanGroupValueRef(List<BooleanValueRef> booleanTracked, List<BooleanValueChangeRef> booleanChangeTracked)
        {
            this.m_booleanTracked = booleanTracked;
            this.m_booleanChangeTracked = booleanChangeTracked;
        }

        public string[] GetNames()
        {
            List<string> result = new List<string>();
            result.AddRange(m_booleanTracked.Select(k => k.GetName()));
            result.AddRange(m_booleanChangeTracked.Select(k => k.GetName()));
            return result.ToArray();
        }
        public string[] GetValueDescriptions()
        {
            List<string> result = new List<string>();
            result.AddRange(m_booleanTracked.Select(k => k.GetNameWithDesciption()));
            result.AddRange(m_booleanChangeTracked.Select(k => k.GetNameWithDesciption()));
            return result.ToArray();
        }
        public int GetCount() { return m_booleanTracked.Count + m_booleanChangeTracked.Count; }

    }

}
