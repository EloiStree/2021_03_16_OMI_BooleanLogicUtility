using BooleanRegisterUtilityAPI.Beans;

namespace BooleanRegisterUtilityAPI.BooleanLogic.Unstored
{
    public class BooleanableToRegisterLink
    {
        string m_booleanName;
        IBooleanableRef m_booleanLogic;
        BooleanStateRegister m_register;

        public BooleanableToRegisterLink(string booleanName, IBooleanableRef booleanLogic, BooleanStateRegister register)
        {
            m_booleanName = booleanName;
            m_booleanLogic = booleanLogic;
            m_register = register;
        }

        public void ComputeAndPushResultIfValide() {

            if (m_booleanLogic == null || m_register==null)
                return;
            bool v, ok;
            m_booleanLogic.GetBooleanableState(out v, out ok);
            if (ok)
            {
               
                m_register.Set(m_booleanName, v);

            }
        }
    }
}
