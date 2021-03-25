using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef
{
    public class BooleanableDelegate : IBooleanableRef
    {
        BooleanableFunction m_function;

        public BooleanableDelegate(BooleanableFunction function)
        {
            m_function = function;
        }

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {

            wasBooleanable = false;
            value = false
                ;
            try
            {
                if(m_function!=null)
                    m_function(out value, out wasBooleanable);
            }
            catch (System.Exception ) {
                wasBooleanable = false;
                value = false;
            }
        }

        public delegate void BooleanableFunction(out bool result, out bool errorOrExceptionOccured);
    }
}
