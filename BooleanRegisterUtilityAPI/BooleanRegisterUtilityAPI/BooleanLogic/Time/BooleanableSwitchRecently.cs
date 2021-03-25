using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic.Time
{
    public abstract class BooleanableSwitchRecently : IBooleanableRef
    {

        BoolSwitchType m_switchType;

        float m_timeObserveInSecond = 50;

        public BooleanableSwitchRecently(BoolSwitchType type, float maxTimeMaintainingInSecond)
        {
            m_switchType = type;
            m_timeObserveInSecond = maxTimeMaintainingInSecond;
        }

        public abstract void GetBooleanableState(out bool value, out bool wasBooleanable);

        public BoolSwitchType GetSwitchType() { return m_switchType; }
        public float GetTimeObservedInSecond() { return m_timeObserveInSecond; }
    }
    public enum BoolSwitchType { SwtichToTrue, SwitchToFalse };
}
