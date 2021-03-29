using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemBumpsInRange : BL_BooleanItemWithObservedTime
    {
        public AllBumpType m_bumpType;
        public ObservedBumpType m_observedType;
        public int m_bumpCount;

        public BL_BooleanItemBumpsInRange(string boolNamedId,ObservedBumpType observedType, AllBumpType bumpType, int bumpCount, IBoolObservedTime observedTime) : base(boolNamedId, observedTime)
        {
            m_bumpType = bumpType;
            m_observedType = observedType;
            m_bumpCount = bumpCount;
        }

        public override string ToString()
        {
            char bc = ' ';
            if (m_bumpType == AllBumpType.FalseBump || m_bumpType == AllBumpType.TrueHole)
                bc = '⊔';
            if (m_bumpType == AllBumpType.FalseHole || m_bumpType == AllBumpType.TrueBump)
                bc = '⊓';
            char s = '=';
            if (m_observedType == ObservedBumpType.LessOrEqual)
                s = '-';
            if (m_observedType == ObservedBumpType.MoreOrEqual )
                s = '+';

            //⊓⊔-+
            return string.Format(" [{0}{1}{2},{3}:{4}] ",bc,s, m_bumpCount, GetTargetName(), GetObservedTime() );
        }
    }
    public enum ObservedBumpType { LessOrEqual, Equal, MoreOrEqual }
    public enum BumpOrHole { Bump, Hole }
}
