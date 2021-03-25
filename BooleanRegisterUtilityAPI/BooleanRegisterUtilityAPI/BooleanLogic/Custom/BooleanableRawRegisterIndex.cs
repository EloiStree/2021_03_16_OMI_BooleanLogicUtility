using BooleanRegisterUtilityAPI.BoolHistoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic.Single
{
    public class BooleanableRawRegisterTarget : IBooleanableRef
    {
        private BooleanRawRegister.DirectAccess m_target;

        public BooleanableRawRegisterTarget(BooleanRawRegister.DirectAccess target)
        {
            m_target = target;
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            wasBooleanable = IsValide();
            value = m_target.GetState();
        }

        public uint GetIndexInRegister()
        {
            return m_target.GetIndexInRegister();
        }

        public string GetIndexNameInRegister()
        {
            return m_target.GetName();
        }

        public bool IsValide()
        {
            return m_target != null && m_target.IsValide();
        }
    }
}
