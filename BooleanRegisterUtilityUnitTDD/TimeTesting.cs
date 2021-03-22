using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BooleanLogic.Time;
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
            m_upHistory.GetState(out state, new TimeInMsLong(500));
            Console.WriteLine("State true 500ms:{0}", state);
        }
        [TestMethod]
        public void TestStateAtFalse()
        {
            bool state;
            m_upHistory.GetState(out state, new TimeInMsLong(500));
            Console.WriteLine("State false 500ms:{0}", !state);
        }

        [TestMethod]
        public void TestMaintainingTrue()
        {
            bool maintain;
            m_upHistory.WasMaintainedTrue(out maintain, new TimeInMsLong(100), new TimeInMsLong(300));
            Console.WriteLine("Maintain true  100-300ms:{0}", maintain);
        }
        [TestMethod]
        public void TestMaintainingFalse()
        {
            bool maintain;
            m_upHistory.WasMaintainedFalse(out maintain, new TimeInMsLong(100), new TimeInMsLong(300));
            Console.WriteLine("Maintaint false  100-300ms:{0}", maintain);
        }
        [TestMethod]
        public void TestBumpCount()
        {
            uint count;
            m_upHistory.GetBumpsCount(AllBumpType.GroundBump, out count, new TimeInMsLong(0), new TimeInMsLong(5000));
            Console.WriteLine("Bump:{0}:{1}", AllBumpType.GroundBump, count);
        }
        [TestMethod]
        public void TestCeilBumpCount()
        {
            uint count;
            m_upHistory.GetBumpsCount(AllBumpType.CeilingBump, out count, new TimeInMsLong(0), new TimeInMsLong(5000));
            Console.WriteLine("C Bump:{0}:{1}", AllBumpType.CeilingBump, count);
        }
        [TestMethod]
        public void TestHoleCount()
        {
            uint count;
            m_upHistory.GetBumpsCount(AllBumpType.GroundHole, out count, new TimeInMsLong(0), new TimeInMsLong(5000));
            Console.WriteLine("Hole:{0}:{1}", AllBumpType.GroundHole, count);
        }
        [TestMethod]
        public void TestCeilHoleCount()
        {
            uint count;
            m_upHistory.GetBumpsCount(AllBumpType.CeilingHole, out count, new TimeInMsLong(0), new TimeInMsLong(5000));
            Console.WriteLine("C Hole:{0}:{1}", AllBumpType.CeilingHole, count);
        }
        [TestMethod]
        public void TestPourcentCount()
        {
            double pct;
            m_upHistory.GetPoucentOfState(true, out pct, new TimeInMsLong(0), new TimeInMsLong(5000));
            Console.WriteLine("True Pourcent:{0}", pct);
        }
        [TestMethod]
        public void TestTimeCount()
        {
            ulong count;
            m_upHistory.GetTimeCount(true, out count, new TimeInMsLong(0), new TimeInMsLong(5000));
            Console.WriteLine("True TimeCount:{0}",  count);
        }



    }
}
