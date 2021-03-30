using BooleanRegisterUtilityAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityUnitTDD
{
    [TestClass]
    public class TryParseTextToBoolItem
    {
        //⌛ ⏰ ➤ ☗ | ↓ ↑ ⌈ ⌊ _ ‾ ∨ ∧ 🀲 🀸 ¬ ⊗ ≡ ꜊ ꜏ ≤ ≥ ⌃ ⌄ ⊓ ⊔ ⊏ ⊐ ⤒ ⤓
        public TryParseTextToBoolItem()
        {
            BL.GetRegister().Set("up", false);
            BL.GetRegister().Set("down", false);
            BL.GetRegister().Set("right", false);
            BL.GetRegister().Set("left", false);
        }

        [TestMethod]
        public void UpExist()
        {
            ToTest("up?");
            ToTest("up?!");
            ToTest("up!?");
        }
        [TestMethod]
        public void UpIs()
        {
            ToTest("up_");
            ToTest("up‾");
        }

        

        [TestMethod]
        public void UpIsAt()
        {
            ToTest("up_2s");
            ToTest("up‾2s");
        }
        [TestMethod]
        public void UpIsDuring()
        {
            ToTest("up_0#2s");
            ToTest("up‾0#2s");
        }
        [TestMethod]
        public void UpBump()
        {
            ToTest("up⊔-2#2s#4s");
            ToTest("up⊓+4#2s#4s");
            ToTest("up⊔<2#2s#4s");
            ToTest("up⊓<4#2s#4s");
            ToTest("up⊔>2#2s#4s");
            ToTest("up⊓>4#2s#4s");

            ToTest("up⊓1#2s#4s");
            ToTest("up⊔1#2s#4s");
            ToTest("up⊓1#2s");
            ToTest("up⊔1#2s");
            ToTest("up⊓2s");
            ToTest("up⊔2s");

        }

        [TestMethod]
        public void UpPourcent()
        {
            ToTest("up%+80#2s#4s");
            ToTest("up%-80#2s#4s");
            ToTest("up%>80#2s");
            ToTest("up%<80#2s");

            ToTest("up%_+80#2s#4s");
            ToTest("up%_-80#2s#4s");
            ToTest("up%_>80#2s");
            ToTest("up%_<80#2s");

            ToTest("up%‾+80#2s#4s");
            ToTest("up%‾-80#2s#4s");
            ToTest("up%‾>80#2s");
            ToTest("up%‾<80#2s");
        }
        [TestMethod]
        public void UpTimeCount()
        {
            ToTest("up⏱+800#2s#4s");
            ToTest("up⏱-800#2s#4s");
            ToTest("up⏱>800#2s");
            ToTest("up⏱800#2s");
            ToTest("up⏱800#2s");
            ToTest("up⏱_+800#2s#4s");
            ToTest("up⏱_-800#2s#4s");
            ToTest("up⏱_>800#2s");
            ToTest("up⏱_800#2s");
            ToTest("up⏱_800#2s");
            ToTest("up⏱‾+800#2s#4s");
            ToTest("up⏱‾-800#2s#4s");
            ToTest("up⏱‾>800#2s");
            ToTest("up⏱‾800#2s");
            ToTest("up⏱‾800#2s");

            ToTest("up∑+800#2s#4s");
            ToTest("up∑-800#2s#4s");
            ToTest("up∑>800#2s");
            ToTest("up∑800#2s");
            ToTest("up∑800#2s");
            ToTest("up∑_+800#2s#4s");
            ToTest("up∑_-800#2s#4s");
            ToTest("up∑_>800#2s");
            ToTest("up∑_800#2s");
            ToTest("up∑_800#2s");
            ToTest("up∑‾+800#2s#4s");
            ToTest("up∑‾-800#2s#4s");
            ToTest("up∑‾>800#2s");
            ToTest("up∑‾800#2s");
            ToTest("up∑‾800#2s");
        }


        [TestMethod]
        public void UpStartAndEnd()
        {
            ToTest("up_‾2s#4s");
            ToTest("up_‾4s");

            ToTest("up‾_2s#4s");
            ToTest("up‾_4s");

            ToTest("up‾‾2s#4s");
            ToTest("up‾‾4s");
            ToTest("up__2s#4s");
            ToTest("up__4s");


        }
        [TestMethod]
        public void UpSwitch()
        {
            ToTest("up↑2s#4s");
            ToTest("up↑4s");
            ToTest("up↓2s#4s");
            ToTest("up↓4s");
        }

        [TestMethod]
        public void UpSwitchStayTrue()
        {
            ToTest("up⤒2s#4s");
            ToTest("up⤒4s");
            ToTest("up⤓2s#4s");
            ToTest("up⤓4s");
        }



        [TestMethod]
        public void Domino()
        {
            ToTest("[up down 01 left right]");
            ToTest("[up down 10 left right]");
            ToTest("[up down 🀲 left right]");
            ToTest("[up down 🀸 left right]");
        }
        [TestMethod]
        public void Morse()
        {
            ToTest("up...---...300");
            ToTest("up...---...300");
        }

        


        private static void ToTest(string toTest)
        {
            Console.WriteLine(toTest);
            BL.If(toTest);
            Console.WriteLine(BL.GetRecordedLogic(toTest));
        }
    }
}
