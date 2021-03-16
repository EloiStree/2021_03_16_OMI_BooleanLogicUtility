using BooleanRegisterUtilityAPI.BoolHistoryLib;
using BooleanRegisterUtilityAPI.Enum;

namespace BooleanRegisterUtilityAPI.Beans
{
    [System.Serializable]
    public class NamedBooleanChangeTimed
    {
        string m_named;
        TimedBooleanChange m_whenChanged;

        public NamedBooleanChangeTimed(string name, TimedBooleanChange whenChanged)
        {
            m_named = name;
            m_whenChanged = whenChanged;
        }

        public string GetName()
        {
            return m_named;
        }

        public TimedBooleanChange GetWhenChanged()
        {
            return m_whenChanged;
        }

        public BooleanChangeType GetChangeType()
        {
            return m_whenChanged.GetChange();
        }

    }
}
