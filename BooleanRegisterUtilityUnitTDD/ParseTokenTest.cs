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
            toConvert = "!up +  ! down ";
            toConvert = "up + ( ! down + 1 ) + ( ! [AND 1 [ 1 0 1 0] 0  1] )+ ( ! 1 +! 0) =  1 ";
            toConvert = "up⤓5s + ( ! down + 1 ) + ( ! [AND 1 [ 1 0 1 0] 0  1] )+ ( ! 1 +! 0) =  1 ";
            Console.Write(toConvert);
            BL.GetRegister();
            BooleanStateRegister register = new BooleanStateRegister();
            register.QuickSetGroup("up", "!down", "!left", "!right");
            RefBooleanRegister refRegister = new RefBooleanRegister(register);

            BL_BuilderElements elements;
            TextLineSpliteAsBooleanLogicTokens t = new TextLineSpliteAsBooleanLogicTokens(
              toConvert,
                false);
            BLTokensToBLBuilder tokenbuilder = new BLTokensToBLBuilder(t, out elements);

            LogicBlock startlogic;
            List< LogicBlock > createdLogic;
            BLElementToLogicBuilder logicBuilder = new BLElementToLogicBuilder(elements, out startlogic, out createdLogic, false);
           
            

            Console.WriteLine("\n--------------------------------------------");
            //   Console.WriteLine("<<| " + startlogic);

            for (int i = 0; i < createdLogic.Count; i++)
            {
                if (createdLogic[i] is BL_ToBeDefined)
                {
                    BL_ToBeDefined tb = (BL_ToBeDefined)createdLogic[i];
                    RegisterRefStringParser.TryToParseAndSetItem(refRegister, tb);
                   // Console.WriteLine("TB| " + tb);

                }
                else
                {
                   // Console.WriteLine("C| " + createdLogic[i]);
                }
            }
            Console.WriteLine("<<| " + startlogic);
            bool value, compute;
            startlogic.Get(out value, out compute);
            Console.WriteLine("Result| " + value+"-"+compute);

            //foreach (var item in elements.GetItems())
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //foreach (var item in elements.GetTokens())
            //{
            //    Console.WriteLine(item.ToString());
            //}

        }
    }
}
