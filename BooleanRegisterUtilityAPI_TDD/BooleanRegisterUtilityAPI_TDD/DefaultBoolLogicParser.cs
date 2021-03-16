using BooleanRegisterUtilityAPI.BooleanLogic;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD
{
    public class DefaultBoolLogicParser : IBooleanLogicParser
    {
        

        public void GetLogicFrom(string text, out bool parsed, out IBooleanLogicCompiled compiledLogic)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanGenericLogic : IBooleanLogicCompiled
    {
        public void LinkToStorage(IBooleanStorage storage, out bool linked, out IBooleanableRef booleanableLogic)
        {
            throw new NotImplementedException();
        }
    }
}
