using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BooleanRegisterUtilityUnitTDD
{
    [TestClass]
    public class UnitTest1
    {
        public BooleanStateRegister m_register = new BooleanStateRegister();

        PrimitiveBoolLogicBlock up = new PrimitiveBoolLogicBlock(true);
        PrimitiveBoolLogicBlock down = new PrimitiveBoolLogicBlock(true);
        PrimitiveBoolLogicBlock left = new PrimitiveBoolLogicBlock(true);
        PrimitiveBoolLogicBlock right = new PrimitiveBoolLogicBlock(true);

        public UnitTest1() {

            BL.SetDefaultRegister(m_register);
            m_register.Set("up", up.GetBool());
            m_register.Set("down", down.GetBool());
            m_register.Set("left", left.GetBool());
            m_register.Set("right", right.GetBool());
        }

        [TestMethod]
        public void DifferentWayToSetRegister()
        {
            //One by one

            m_register.AddTimePastFromDefaultTimer();
            m_register.QuickSetAllFalse("up", "down", "left", "right");
            m_register.Set("down", true);
            DisplayInformatoinAboutKeys();

            m_register.AddTimePastFromDefaultTimer();
            m_register.QuickSetAllFalse("up", "down", "left", "right");
            m_register.QuickSetAllTrue("up", "down", "left", "right");
            DisplayInformatoinAboutKeys();

            m_register.AddTimePastFromDefaultTimer();
            m_register.QuickSetAllFalse("up", "down", "left", "right");
            m_register.QuickSetGroup("!up", "down", "!left", "right");
            DisplayInformatoinAboutKeys();

            m_register.AddTimePastFromDefaultTimer();
            m_register.SetGroup(
                new string[] { "up", "down", "left", "right" },
                new bool[] { false, false, false, false });
            DisplayInformatoinAboutKeys();

            BL_BooleanItemBumpsInRange bumpRang = new BL_BooleanItemBumpsInRange("test", ObservedBumpType.Equal, AllBumpType.GroundBump, 3, null);
            Console.WriteLine("Test t " + (m_register == BL.GetRegister()));
 
            Console.WriteLine("> REGISTER STATE:\n " + m_register.GetFullHistoryDebugText());
            Console.WriteLine("> UP STATE:\n " + m_register.GetCurveDebugText("up"));

        }

        private void DisplayInformatoinAboutKeys()
        {
            Console.WriteLine(string.Format("INPUT: u{0} d{1} l{2} r{3}",
                m_register.GetStateOf("up").GetValue(),
                m_register.GetStateOf("down").GetValue(),
                m_register.GetStateOf("left").GetValue(),
                m_register.GetStateOf("right").GetValue()));
        }
    }
}
