using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemExist : BL_BooleanItemDefault
    {
        public BoolExistanceState m_existanceCheck;

        public BL_BooleanItemExist(string boolNamedId) : base(boolNamedId)
        {
        }

        public BL_BooleanItemExist(string boolNamedId, BoolExistanceState existanceCheck) : base(boolNamedId)
        {
            m_existanceCheck = existanceCheck;
        }
        public override string ToString()
        {
            return string.Format(" [B{0},{1}] ", m_existanceCheck == BoolExistanceState.Exist ? "?" : "⁉", GetTargetName());
        }
    }
    public class BL_BooleanItemExistAt : BL_BooleanItemDefault
    {
        public BoolExistanceState m_existanceCheck;
        public IBoolObservedTime m_observedTime;

        public BL_BooleanItemExistAt(string boolNamedId, IBoolObservedTime observedTime) : base(boolNamedId)
        {
            m_observedTime = observedTime;
        }

        public BL_BooleanItemExistAt(string boolNamedId, BoolExistanceState existanceCheck, IBoolObservedTime observedTime) : base(boolNamedId)
        {
            m_observedTime = observedTime;
            m_existanceCheck = existanceCheck;
        }
        public override string ToString()
        {
            return string.Format(" [B{0},{1}:{2}] ", m_existanceCheck == BoolExistanceState.Exist ? "?" : "⁉", GetTargetName(), m_observedTime);
        }

        public IBoolObservedTime GetObservedTime()
        {
            return m_observedTime;
        }

        public bool GetBoolAsValue()
        {
            return m_existanceCheck == BoolExistanceState.Exist;
        }
    }
}
