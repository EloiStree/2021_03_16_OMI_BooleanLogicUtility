using BooleanRegisterUtilityAPI.Enum;

namespace BooleanRegisterUtilityAPI
{
    [System.Serializable]
    public class BooleanChange
    {
        BooleanChangeType m_changeType;
        public BooleanChange(BooleanChangeType changeType)
        {
            m_changeType = changeType;
        }

        public BooleanChangeType GetChange() { return m_changeType; }
    }
}
