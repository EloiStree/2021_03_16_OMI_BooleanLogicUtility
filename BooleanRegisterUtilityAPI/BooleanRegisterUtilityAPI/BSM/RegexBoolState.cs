
using BooleanRegisterUtilityAPI.Beans;
using System.Text.RegularExpressions;


namespace BooleanRegisterUtilityAPI.BSM

{
    [System.Serializable]
    public class RegexBoolState 
    {
         string m_regex;
         BooleanIndexGroup m_observed;
         float m_timeToCheckChange;
         RegexableValueType m_textToApplyType = RegexableValueType.NewToLast;



        public RegexBoolState(string regex, BooleanIndexGroup groupObserved, RegexableValueType textToApplyType, float timeToCheckChange)
        {
            m_regex = regex;
            m_observed = groupObserved;
            m_textToApplyType = textToApplyType;
            m_timeToCheckChange = timeToCheckChange;

        }

        public  bool IsConditionValide(BooleanStateRegister register)
        {
            return Regex.IsMatch(GetTextToRegex(register), m_regex);
        }

        private string GetTextToRegex(BooleanStateRegister register)
        {
            return BooleanStateUtility.GetTextFor(m_textToApplyType, register, m_observed, m_timeToCheckChange);
        }

        public override string ToString()
        {
            return "(BSM,Regex:" + m_regex + ")";
        }
    }
}