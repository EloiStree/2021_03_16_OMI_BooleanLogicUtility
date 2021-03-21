using BooleanRegisterUtilityAPI.BooleanLogic;
using BooleanRegisterUtilityAPI.BooleanLogic.BoolDebug;
using BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef;
using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.BooleanLogic.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BooleanRegisterUtilityAPI.BoolHistoryLib;
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.BooleanLogic.Single;
using BooleanRegisterUtilityAPI.Timer;
using BooleanRegisterUtilityAPI.BooleanLogic.Unstored;

namespace BooleanRegisterUtilityAPI_TDD
{
    class Program
    {
        public static BooleanStateRegister m_register;
        static void Main(string[] args)
        {
             m_register = new BooleanStateRegister();
            m_register.Set("Yo", false);

            DeltaTimeToBooleanStateRegister timerRegister = new DeltaTimeToBooleanStateRegister(m_register);
            timerRegister.CreateThread(ThreadPriority.Normal, 20);

             Thread d = new Thread(new ThreadStart(DoStuffSomeTime));
            d.Start();
            Thread.Sleep(150);
            m_register.Set("Yo", true);
            ConsoleKey key;
            ConvertionTest tdd = new ConvertionTest(m_register);
            BooleanNamedHistory yo = m_register.GetStateOf("Yo");
            tdd.SetBoolHistoryToObserved(yo);

            BooleanRawRegister.DirectAccess directAccess;
            bool found;
            m_register.GetDirectionAccessToState("Yo", out directAccess, out found);
            tdd.SetDirectAccess(directAccess);

            do
            {

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.LeftArrow)
                    tdd.SetA(!tdd.GetA());
                if (key == ConsoleKey.RightArrow)
                    tdd.SetB(!tdd.GetB());
                tdd.RandomTest();
            } while (key != ConsoleKey.Escape);
        }

        private static void AddTimeToRegister()
        {
            DateTime now, previous;
            float timepast;
            now = previous = DateTime.Now;
            while (m_register != null)
            {
                now = DateTime.Now;
                timepast = (float)(now - previous).TotalSeconds;
                m_register.AddElapsedTimeToAll(timepast);
                previous = now;
            }

        }
        private static void DoStuffSomeTime()
        {
            while (m_register != null)
            {

                if (DateTime.Now.Second % 3 == 0) { 
                 m_register.Set("Yo", DateTime.Now.Millisecond % 2 == 0);
                }
                Thread.Sleep(50);
            }

        }
    }

    public class ConvertionTest {


        BooleanablePrimitiveValue
              a = new BooleanablePrimitiveValue(false)
            , b = new BooleanablePrimitiveValue(true);
        public List<IBooleanableRef> t = new List<IBooleanableRef>();

        BooleanableDelegate modulo2;
        BooleanableDelegate modulo3;
        BooleanableDelegate modulo7;
        BooleanNamedHistory historyObserved;
        BooleanableRawRegisterTarget directAccess=null;
        BooleanStateRegister register;

        public ConvertionTest(BooleanStateRegister register) {

            this.register = register;
            modulo7 = new BooleanableDelegate(TrueModulo7);
            modulo3 = new BooleanableDelegate(TrueModulo3);
            modulo2 = new BooleanableDelegate(TrueModulo2);

        }
        public void SetBoolHistoryToObserved(BooleanNamedHistory history) {
            historyObserved = history;
        }

        public void SetDirectAccess(BooleanRawRegister.DirectAccess directionAccess) {
            directAccess = new BooleanableRawRegisterTarget(directionAccess);
        }

        public void SetAB(bool a, bool b) {

            this.a = new BooleanablePrimitiveValue(a);
             this.b = new BooleanablePrimitiveValue(b);
        }

        public void SetList(params bool[] boolValues) {
            t.Clear();
            for (int i = 0; i < boolValues.Length; i++)
            {
                t.Add(new BooleanablePrimitiveValue(boolValues[i]));
            }
        }


        public void RandomTest() {

            BooleanableOR or = new BooleanableOR(a, b);
            BooleanableAND and = new BooleanableAND(a, b);
            BooleanableXOR xor = new BooleanableXOR(a, b);
            BooleanableNOR  nor = new BooleanableNOR(a, b);
            BooleanableNAND nand = new BooleanableNAND(a, b);
            BooleanableNXOR nxor = new BooleanableNXOR(a, b);



            Console.WriteLine(string.Format("# Time {0}s ### ", DateTime.Now.Second));

            Console.WriteLine(string.Format("### Left {0} Right {1} ### ", a.m_value, b.m_value));
            Console.WriteLine(BooleanableDebug.StringView(or));
            Console.WriteLine(BooleanableDebug.StringView(and));
            Console.WriteLine(BooleanableDebug.StringView(xor));
            Console.WriteLine(BooleanableDebug.StringView(nor ));
            Console.WriteLine(BooleanableDebug.StringView(nand));
            Console.WriteLine(BooleanableDebug.StringView(nxor));

            string larName = "LAndR";
            BooleanableToRegisterLink leftAndRigth = new BooleanableToRegisterLink(larName, and, register);
            leftAndRigth.ComputeAndPushResultIfValide();
            //bool containlar= register.Contains(larName);
            //bool valuelar= containlar ? register.GetStateOf(larName) : false;
            //Console.WriteLine(string.Format("Left & Right: c{0} v{1}", , ) ));

            if (historyObserved != null)
            {
                string histDescription = BoolHistoryDescription.GetDescriptionNowToPast(historyObserved.GetHistory());
                if (histDescription.Length > 50)
                    histDescription = histDescription.Substring(0, 50);
                Console.WriteLine(string.Format("Bool, {0}|{1}: {2}", historyObserved.GetName(), historyObserved.GetValue(),
                   histDescription));

                BooleanableBoolHistoryMaintaining maintaining = new BooleanableBoolHistoryMaintaining(historyObserved.GetHistory(), BoolMaintainType.MaintainTrue, 1);
                BooleanableBoolHistoryChangeListener switching = new BooleanableBoolHistoryChangeListener(historyObserved.GetHistory(), BoolSwitchType.SwtichToTrue, 1);
                Console.WriteLine(BooleanableDebug.StringView(maintaining));
                Console.WriteLine(BooleanableDebug.StringView(switching)); 
                 Console.WriteLine(BooleanableDebug.StringView(directAccess)); 
            }

            //⌛ ➤ ☗ | ↓ ↑
            //"a and b and c↓500 or c˥500 c˩500   d͞   ͟d ↧ ↥ ⊕ ⊗ "
            //[AND [XOR ] [NOR ] ] + c˩500 + [OR [AND left right] UP]
            //"AND[c b dd c˩500 ]"
            //c⅃500 : is active since at least 500 ms
            //c⅂500 : is none active since at least 500 ms
            //c↓500 : has switch to true recently
            //c↑500 : has switch to true recently
        }

        private void TrueModulo3(out bool result, out bool errorOrExceptionOccured)
        {
            result = DateTime.Now.Second % 3 == 0;
            errorOrExceptionOccured = true;
        }
        private void TrueModulo2(out bool result, out bool errorOrExceptionOccured)
        {
            result = DateTime.Now.Second % 2 == 0;
            errorOrExceptionOccured = true;
        }
        private void TrueModulo7(out bool result, out bool errorOrExceptionOccured)
        {
            result = DateTime.Now.Second % 7 == 0;
            errorOrExceptionOccured = true;
        }

        public void SetB(bool v)
        {

            b.m_value = v;
        }

        public void SetA(bool v)
        {
            a.m_value = v;
        }
        public  bool GetB()
        {
            bool bo, berr;
            b.GetBooleanableState(out bo, out berr);
            return bo;
        }

        public  bool GetA()
        {
            bool ao, aerr;
            a.GetBooleanableState(out ao, out aerr);
            return ao;
        }
    }
}






