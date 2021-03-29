using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BoolParsingToken;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityUnitTDD
{
    [TestClass]
    public class StringToBL_Item
    {
        [TestMethod]
        public void TestRefBool()
        {


            BooleanStateRegister m_register= new BooleanStateRegister();
            RefBooleanRegister m_refregister = new RefBooleanRegister(m_register);
            Console.WriteLine("0 " + RegisterRefStringParser.TryToParseItem(m_refregister, "up_500"));
            Console.WriteLine("1 " + RegisterRefStringParser.TryToParseItem(m_refregister, "up↓500#2000"));
            Console.WriteLine("2 " + RegisterRefStringParser.TryToParseItem(m_refregister,"up⏱_500#20#2000"));
            //V

        }
        [TestMethod]
        public void TestLogicMaintainCreation()
        {
            Console.WriteLine("0 " + BLTokensToBLBuilder.TryToParse("up_"));
            Console.WriteLine("1 " + BLTokensToBLBuilder.TryToParse("up‾"));
            Console.WriteLine("2 " + BLTokensToBLBuilder.TryToParse("up_500"));
            Console.WriteLine("3 " + BLTokensToBLBuilder.TryToParse("up‾500"));
            Console.WriteLine("4 " + BLTokensToBLBuilder.TryToParse("up_500#2000"));
            Console.WriteLine("5 " + BLTokensToBLBuilder.TryToParse("up‾500#2000"));
            Console.WriteLine("6 " + BLTokensToBLBuilder.TryToParse("up_14h"));
            Console.WriteLine("7 " + BLTokensToBLBuilder.TryToParse("up‾13:20"));
            Console.WriteLine("8 " + BLTokensToBLBuilder.TryToParse("up‾13:20#16h23m50s512"));
            //V

        }
        [TestMethod]
        public void TestLogicSwitchCreation()
        {
            Console.WriteLine("0 " + BLTokensToBLBuilder.TryToParse("up↓"));
            Console.WriteLine("1 " + BLTokensToBLBuilder.TryToParse("up↑"));
            Console.WriteLine("2 " + BLTokensToBLBuilder.TryToParse("up↓500"));
            Console.WriteLine("3 " + BLTokensToBLBuilder.TryToParse("up↑500"));
            Console.WriteLine("4 " + BLTokensToBLBuilder.TryToParse("up↓500#2000"));
            Console.WriteLine("5 " + BLTokensToBLBuilder.TryToParse("up↑500#2000"));
            Console.WriteLine("6 " + BLTokensToBLBuilder.TryToParse("up↓14h"));
            Console.WriteLine("7 " + BLTokensToBLBuilder.TryToParse("up↑13:20"));
            //V

        }
        [TestMethod]
        public void TestLogicTimeCreation()
        {
            Console.WriteLine("0 " + BLTokensToBLBuilder.TryToParse("up⏱"));
            Console.WriteLine("1 " + BLTokensToBLBuilder.TryToParse("up⏱"));
            Console.WriteLine("2 " + BLTokensToBLBuilder.TryToParse("up⏱‾>500"));
            Console.WriteLine("3 " + BLTokensToBLBuilder.TryToParse("up⏱_<500"));
            Console.WriteLine("4 " + BLTokensToBLBuilder.TryToParse("up⏱‾500#2000"));
            Console.WriteLine("5 " + BLTokensToBLBuilder.TryToParse("up⏱_500#2000"));
            Console.WriteLine("4 " + BLTokensToBLBuilder.TryToParse("up⏱‾500#20#2000"));
            Console.WriteLine("5 " + BLTokensToBLBuilder.TryToParse("up⏱_500#20#2000"));
            Console.WriteLine("6 " + BLTokensToBLBuilder.TryToParse("up⏱‾500#14h"));
            Console.WriteLine("7 " + BLTokensToBLBuilder.TryToParse("up⏱‾500#13:20"));
            Console.WriteLine("6 " + BLTokensToBLBuilder.TryToParse("up⏱‾500#14h#15h"));
            Console.WriteLine("7 " + BLTokensToBLBuilder.TryToParse("up⏱‾500#13:20#16h"));

        }
        [TestMethod]
        public void TestLogicPourcentCreation()
        {
            Console.WriteLine("0 " + BLTokensToBLBuilder.TryToParse("up%"));
            Console.WriteLine("1 " + BLTokensToBLBuilder.TryToParse("up%"));
            Console.WriteLine("2 " + BLTokensToBLBuilder.TryToParse("up%‾500"));
            Console.WriteLine("3 " + BLTokensToBLBuilder.TryToParse("up%_500"));
            Console.WriteLine("4 " + BLTokensToBLBuilder.TryToParse("up%‾500#2000"));
            Console.WriteLine("5 " + BLTokensToBLBuilder.TryToParse("up%_500#2000"));
            Console.WriteLine("6 " + BLTokensToBLBuilder.TryToParse("up%_14h"));
            Console.WriteLine("7 " + BLTokensToBLBuilder.TryToParse("up%_13:20"));

        }


        public static void RemoveStart(ref string boolmeta, params string[] toRemove)
        {
            if (boolmeta == null)
                return;
            for (int i = 0; i < toRemove.Length; i++)
            {
                if (boolmeta.IndexOf(toRemove[i]) == 0)
                    boolmeta = boolmeta.Substring(toRemove[i].Length);
            }
        }
    }
  
}
