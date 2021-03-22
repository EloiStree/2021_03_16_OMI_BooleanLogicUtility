using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BooleanLogic
{
    public interface IBooleanableRef {
        /// <summary>
        /// Return if the child is true or false and if it had not problem converting information behind to boolean
        /// </summary>
        /// <param name="value">The boolean state of the code behind.</param>
      void GetBooleanableState(out bool value, out bool wasBooleanable);
    }
}
