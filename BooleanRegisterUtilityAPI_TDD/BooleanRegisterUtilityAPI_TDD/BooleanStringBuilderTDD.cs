using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BooleanLogic;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD
{
    class BooleanStringBuilderTDD
    {


        public void Test(IBooleanStorage register) {

           
            //Controled way
            IBooleanLogicCompiled logicCompiled = Bool.Compile("left + right");
            IBooleanableRef readyToCheckLogic;
            bool parsed;
            logicCompiled.LinkToStorage(register, out parsed, out readyToCheckLogic);


            //Quick And dirty
            if (Bool.Check("left + right"))
            {

            }
            //QUick and "heavy" in memory
            if (Bool.SaveCheck("left + right"))
            {

            }
        }
    }

    public static class Bool {
        public static IBooleanStorage DefaultStorage;//= new BooleanStateRegister();
        public static void Set(IBooleanStorage newStorage) { DefaultStorage = newStorage; }
        public static bool IsDefaultDefined(IBooleanStorage newStorage) { return DefaultStorage != null; }

        public static bool Check(string logicAsString)
        {
            return Check(logicAsString, DefaultStorage);
        }

        public static bool Check(string logicAsString, IBooleanStorage storage) {

            //To avoid converting continusly, code something that can memories logic build.
            ////Try To save the logic compilation
            ////Try To save the logic linked to storage

            return false;
        }


        public static IBooleanLogicParser DefaultParser = new DefaultBoolLogicParser();
        public static void CompileLogicWithDefaultFrom(string logicAsString, out bool parsed, out IBooleanableRef logicBuilded)
        {

            CompileLogicFrom(logicAsString, DefaultStorage, DefaultParser, out parsed, out logicBuilded);

        }
        public static void CompileLogicFrom(string textToParse, IBooleanStorage storage, IBooleanLogicParser parser, out bool parsed, out IBooleanableRef logicBuilded)
        {
            IBooleanLogicCompiled compiled;
            parser.GetLogicFrom(textToParse, out parsed, out compiled);
            if (!parsed)
            {
                logicBuilded = null;
            }
            compiled.LinkToStorage(storage, out parsed, out logicBuilded);

        }

        internal static bool SaveCheck(string v)
        {
            throw new NotImplementedException();
        }

        internal static IBooleanLogicCompiled Compile(string v)
        {
            throw new NotImplementedException();
        }
    }
}
