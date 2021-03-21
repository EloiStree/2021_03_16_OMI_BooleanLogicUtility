using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item
{
    public class BL_BooleanItemBumpsInRange : BL_BooleanItemWithObservedTime
    {
        public BumpOrHole m_bumpType;
        public ObservedBumpType m_observedType;
        public int m_bumpCount;

        public BL_BooleanItemBumpsInRange(string boolNamedId,ObservedBumpType observedType, BumpOrHole bumpType, int bumpCount, IBoolObservedTime observedTime) : base(boolNamedId, observedTime)
        {
            m_bumpType = bumpType;
            m_observedType = observedType;
            m_bumpCount = bumpCount;
        }

    }
    public enum ObservedBumpType { LessOrEqual, Equal, MoreOrEqual }
    public enum BumpOrHole { Bump, Hole }
}
