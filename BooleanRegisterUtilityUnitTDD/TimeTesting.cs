using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.Unstore;
using BooleanRegisterUtilityAPI.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityUnitTDD
{
    [TestClass]
    public class TimeTesting
    {
        static bool m_timeReadyToBeCheck;
        static BooleanStateRegister register = new BooleanStateRegister();
        static BoolHistory m_upHistory;

        static TimeTesting() {


            register.AddTimePastFromDefaultTimer();
            register.Set("up", true);
            register.AddTimePastFromDefaultTimer();

            Thread.Sleep(500);
            register.AddTimePastFromDefaultTimer();
            register.Set("up", false);

            Thread.Sleep(5000);
            register.AddTimePastFromDefaultTimer();
            register.Set("up", true);

            Thread.Sleep(1000);
            register.AddTimePastFromDefaultTimer();
            register.Set("up", false);

            Thread.Sleep(1000);
            register.AddTimePastFromDefaultTimer();
            register.Set("up", true);

            register.AddTimePastFromDefaultTimer();
            Thread.Sleep(1500);
            register.AddTimePastFromDefaultTimer();
            register.Set("up", false);
            Thread.Sleep(800);
            register.AddTimePastFromDefaultTimer();
            register.Set("up", true);
            Console.WriteLine(register.GetNumericalDebugText("up"));
            m_upHistory = register.GetStateOf("up").GetHistory();
            m_timeReadyToBeCheck = true;
        }
        [TestMethod]
        public void DifferentWayToSetRegister2()
        {


            Console.WriteLine(register.GetCurveDebugText("up"));


        }
        [TestMethod]
        public void DifferentWayToSetRegister()
        {


            Console.WriteLine(register.GetNumericalDebugText("up"));


        }

        [TestMethod]
        public void TestStateATTrue()
        {
            bool state;
            m_upHistory.GetState(out state, new TimeInMsUnsignedInteger(1000));
            Console.WriteLine("State true 1000ms:{0}", state);
        }
        [TestMethod]
        public void TestStateAtFalse()
        {
            bool state;
            m_upHistory.GetState(out state, new TimeInMsUnsignedInteger(500));
            Console.WriteLine("State false 500ms:{0}", !state);
        }

        [TestMethod]
        public void TestMaintainingTrue()
        {
            bool maintain;
            m_upHistory.WasMaintainedTrue(out maintain, new TimeInMsUnsignedInteger(100), new TimeInMsUnsignedInteger(300));
            Console.WriteLine("Maintain true  100-300ms:{0}", maintain);
        }
        [TestMethod]
        public void TestMaintainingFalse()
        {
            bool maintain;
            m_upHistory.WasMaintainedFalse(out maintain, new TimeInMsUnsignedInteger(100), new TimeInMsUnsignedInteger(300));
            Console.WriteLine("Maintaint false  100-300ms:{0}", maintain);
        }

        [TestMethod]
        public void TestStateAtFalseDate()
        {
            DateTime now = DateTime.Now;
            DateTime t= now.AddSeconds(-1);
            bool state;
            m_upHistory.GetState(out state,t);
            Console.WriteLine("State false DateNow 1000ms:{0}", state);
        }

        [TestMethod]
        public void TestMaintainingTrueDate()
        {
            DateTime now = DateTime.Now;
            DateTime t1= now.AddMilliseconds(-100), t2= now.AddMilliseconds(-300);
            bool maintain;
            m_upHistory.WasMaintainedTrue(out maintain, t1, t2);
            Console.WriteLine("Maintain true DateNow  100-300ms:{0}", maintain);
        }
        [TestMethod]
        public void TestMaintainingFalseDate()
        {
            DateTime now = DateTime.Now;
            DateTime t1 = now.AddMilliseconds(-100), t2 = now.AddMilliseconds(-300);
            bool maintain;
            m_upHistory.WasMaintainedFalse(out maintain,t1,t2);
            Console.WriteLine("Maintaint false DateNow 100-300ms:{0}", maintain);
        }
        [TestMethod]
        public void TestBumpCount()
        {
            uint count;
            m_upHistory.GetBumpsCount(AllBumpType.FalseBump, out count, new TimeInMsUnsignedInteger(0), new TimeInMsUnsignedInteger(5000));
            Console.WriteLine("Bump:{0}:{1}", AllBumpType.FalseBump, count);
        }
        [TestMethod]
        public void TestCeilBumpCount()
        {
            uint count;
            m_upHistory.GetBumpsCount(AllBumpType.TrueBump, out count, new TimeInMsUnsignedInteger(0), new TimeInMsUnsignedInteger(5000));
            Console.WriteLine("C Bump:{0}:{1}", AllBumpType.TrueBump, count);
        }
        [TestMethod]
        public void TestHoleCount()
        {
            uint count;
            m_upHistory.GetBumpsCount(AllBumpType.FalseHole, out count, new TimeInMsUnsignedInteger(0), new TimeInMsUnsignedInteger(5000));
            Console.WriteLine("Hole:{0}:{1}", AllBumpType.FalseHole, count);
        }
        [TestMethod]
        public void TestCeilHoleCount()
        {
            uint count;
            m_upHistory.GetBumpsCount(AllBumpType.TrueHole, out count, new TimeInMsUnsignedInteger(0), new TimeInMsUnsignedInteger(5000));
            Console.WriteLine("C Hole:{0}:{1}", AllBumpType.TrueHole, count);
        }
        [TestMethod]
        public void TestPourcentCount()
        {
            double pct;
            m_upHistory.GetPoucentOfState(true, out pct, new TimeInMsUnsignedInteger(0), new TimeInMsUnsignedInteger(5000));
            Console.WriteLine("True Pourcent:{0}", pct);
        }
        [TestMethod]
        public void TestTimeCount()
        {
            uint count;
            m_upHistory.GetTimeCount(true, out count, new TimeInMsUnsignedInteger(0), new TimeInMsUnsignedInteger(5000));
            Console.WriteLine("True TimeCount:{0}", count);
        }


        [TestMethod]
        public void TestWTF()
        {
            RefBooleanRegister rf = new RefBooleanRegister(register);
            RegisterRefStateAtBlock test = new RegisterRefStateAtBlock(rf,
                new BL_BooleanItemIsTrueOrFalseAt(
                "up", 
                    new BL_TimeToObserve(
                        true, new BL_RelativeTimeDurationFromNow(100,200)) 
                , BoolState.True));

            ulong count;
            bool value, computed;
            test.Get(out value, out computed);
            Console.WriteLine("THUMM:{0}{1}", value, computed);
        }



    }
}
