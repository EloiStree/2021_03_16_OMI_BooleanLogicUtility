using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BoolParsingToken;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Builder;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
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
    public class ParseTokenTest
    {
        BooleanStateRegister register;
        RefBooleanRegister refRegister;
        public ParseTokenTest()
        {
             register = new BooleanStateRegister();
            register.QuickSetGroup("up", "!down", "!left", "!right");
             refRegister = new RefBooleanRegister(register);
        }


        [TestMethod]
        public void ExperimentingFinalResultBLIF()
        {

            BoolConditionResult result = BL.If("1+1");
            if (result.TnE)
            {

                Console.WriteLine("SUCCESS 1+1 = true");
            }
            else if (result.FnE)
            {

                Console.WriteLine("SUCCESS 1+1 = false");
            }
            else
            {

                Console.WriteLine("ERROR: " + result.ErrorStack);
            }

        }

        [TestMethod]
        public void DemoConvertTextToReadyToBeParseTokenElements()
        {


            BL_BuilderElements elements;
            TextLineSpliteAsBooleanLogicTokens t = new TextLineSpliteAsBooleanLogicTokens(" up_50#5500 + down↓4s", false);
            BLTokensToBLBuilder tokenbuilder = new BLTokensToBLBuilder(t, out elements);

            foreach (var item in elements.GetItems())
            {
                Console.WriteLine(item.ToString());
            }
            foreach (var item in elements.GetTokens())
            {
                Console.WriteLine(item.ToString());
            }
        }
        [TestMethod]
        public void BuildBinaryLogic()
        {
            string toConvert = "up + ( ! down + 1 ) + ( ! [AND 1 [ 1 0 1 0] 0 ! 1] + ( ! ! 1 + 0)) = ! 1 ";
            TestConvertionOf(toConvert);
            toConvert = "!up +  ! down ";
            TestConvertionOf(toConvert);
            toConvert = "up + ( ! down + 1 ) + ( ! [AND 1 [ 1 0 1 0] 0  1] )+ ( ! 1 +! 0) =  1 ";
            TestConvertionOf(toConvert);
            toConvert = "up⤓5s + ( ! down + 1 ) + ( ! [AND 1 [ 1 0 1 0] 0  1] )+ ( ! 1 +! 0) =  1 ";

            TestConvertionOf(toConvert);
            toConvert = "up⤓5s + ( ! down + 1 ) + ( ! [AND 1 [ 1 0 1 0] 0  1] )+ ( ! 1 +! 0) =  1 ";

        }
        [TestMethod]
        public void FailedInUnityButShouldNot()
        {

            //TestConvertionOf("(up + down + aaa? ) + 1", true);
            //TestConvertionOf("(up + down  ) + (left + right)", true);
            //TestConvertionOf("(up + down  ) xor (left + right)", true);

            TestConvertionOf("[and up left ] + [and up down ] ", true);
            


        }
        [TestMethod]
        public void TDD_SimpleBoolean()
        {

            Console.WriteLine("########### And #########");
            TestConvertionOf("1+1");
            TestConvertionOf("0+1");
            TestConvertionOf("1+0");
            TestConvertionOf("0+0");
            Console.WriteLine("########### OR #########");
            TestConvertionOf("1 | 1");
            TestConvertionOf("0 | 1");
            TestConvertionOf("1 | 0");
            TestConvertionOf("0 | 0");
            Console.WriteLine("########### XOR #########");
            TestConvertionOf("1 xor 1");
            TestConvertionOf("0 xor 1");
            TestConvertionOf("1 xor 0");
            TestConvertionOf("0 xor 0");
            Console.WriteLine("########### XEQ #########");
            TestConvertionOf("1 ≡ 1");
            TestConvertionOf("0 ≡ 1");
            TestConvertionOf("1 ≡ 0");
            TestConvertionOf("0 ≡ 0");
        }


        [TestMethod]
        public void TDD_MultiDuoBoolean()
        {

            Console.WriteLine("########### And #########");
            TestConvertionOf("1+1+1+1");
            TestConvertionOf("0+1+1+0");
            TestConvertionOf("1+0+1+1");
            TestConvertionOf("0+0+0+0");
            Console.WriteLine("########### OR #########");
            TestConvertionOf("1 | 1 | 1 | 1");
            TestConvertionOf("0 | 1 | 0 | 0");
            TestConvertionOf("1 | 0 | 0 | 0");
            TestConvertionOf("0 | 0 | 0 | 0");
            Console.WriteLine("########### XOR #########");
            TestConvertionOf("1 xor 1 xor 1 xor 1");
            TestConvertionOf("0 xor 1 xor 1");
            TestConvertionOf("1 xor 0 xor 1");
            TestConvertionOf("0 xor 0 xor 0 xor 0");
            Console.WriteLine("########### XEQ #########");
            TestConvertionOf("1 ≡ 1  ≡ 1 ≡ 1");
            TestConvertionOf("0 ≡ 1 ≡ 1");
            TestConvertionOf("1 ≡ 0 ≡ 0");
            TestConvertionOf("0 ≡ 0 ≡ 0 ≡ 0");
        }



        private void TestConvertionOf(string toConvert, bool debug= false)
        {
            LogicBlock startlogic;
            RegisterRefStringParser.TryParseTextToLogicBlockRef(toConvert, refRegister, out startlogic, debug );

            if (startlogic == null)
            {
                Console.WriteLine("------------FAIL-----------------| ");
                Console.WriteLine("|"+toConvert);
                return;

            }

            Console.WriteLine("-----------------------------| ");
            Console.Write(toConvert);

            Console.WriteLine("<<| " + startlogic);
            bool value, compute;
            startlogic.Get(out value, out compute);
            Console.WriteLine("Result| " + value + "-" + compute);
            Console.WriteLine("-----------------------------| ");
        }



        private void TestConvertionOfWithDebug(string v)
        {
            throw new NotImplementedException();
        }

    }
}
