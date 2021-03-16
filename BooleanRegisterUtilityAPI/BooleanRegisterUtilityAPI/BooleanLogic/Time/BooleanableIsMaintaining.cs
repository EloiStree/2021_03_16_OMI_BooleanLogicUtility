using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BooleanLogic.Time
{
    public abstract class BooleanableIsMaintaining : IBooleanableRef
    {

         BoolMaintainType m_maintainingType;

        float m_maxTimeMaintainingInSecond =50;

        public BooleanableIsMaintaining(BoolMaintainType maintainType, float maxTimeMaintaining)
        {
            m_maintainingType = maintainType;
            m_maxTimeMaintainingInSecond = maxTimeMaintaining;
        }

        public abstract void GetBooleanableState(out bool value, out bool wasBooleanable);

        public float GetTimeObservedInSecond() { return m_maxTimeMaintainingInSecond; }
        public BoolMaintainType GetMaintainType() { return m_maintainingType; }
    }
    public enum BoolMaintainType { MaintainTrue, MaintainFalse };
}
