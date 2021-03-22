using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using BooleanRegisterUtilityAPI.BoolParsingToken.Unstore;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD
{
    class ProgramBLIF
    {

        static void Main(string[] args)
        {

            //DeleguateBoolLogicBlock up = new DeleguateBoolLogicBlock(GetUp);
            //DeleguateBoolLogicBlock down = new DeleguateBoolLogicBlock(GetDown);
            //DeleguateBoolLogicBlock left = new DeleguateBoolLogicBlock(GetLeft);
            //DeleguateBoolLogicBlock right = new DeleguateBoolLogicBlock(GetRight);

            PrimitiveBoolLogicBlock up = new PrimitiveBoolLogicBlock(true);
            PrimitiveBoolLogicBlock down = new PrimitiveBoolLogicBlock(true);
            PrimitiveBoolLogicBlock left = new PrimitiveBoolLogicBlock(true);
            PrimitiveBoolLogicBlock right = new PrimitiveBoolLogicBlock(true);


            ConsoleKey key;
            do
            {
                BooleanStateRegister register = new BooleanStateRegister();
                BL.SetDefaultRegister(register);
                bool valide;
                bool bup, bdown, bleft, bright;
                up.Get(out bup, out valide);
                down.Get(out bdown, out valide);
                left.Get(out bleft, out valide);
                right.Get(out bright, out valide);


                Console.WriteLine(string.Format("INPUT: u{0} d{1} l{2} r{3}", bup, bdown, bleft, bright));

                string condition = "up + down";
                if (BL.If("up + !down + shift? + shift_500ms").TnE) 
                {

                    
                    Console.WriteLine(condition);
                    Console.WriteLine(BL.GetRecordedLogic(condition));
                }
                condition = "left | right|up |down";
                if (BL.If(condition, "keyalldown").TnE) 
                {

                    Console.WriteLine(condition);
                    Console.WriteLine(BL.GetRecordedLogic(condition));
                }

                Testwith(register);

               
                    bool value, found;
                    BL.GetRegister().GetValue("keyalldown", out value, out found);
                    Console.WriteLine(string.Format("OBSERVED: keyalldown {0}{1}", value?'1':'0', found? "" : "e"));
                

                key = Console.ReadKey().Key;
                if (key == ConsoleKey.UpArrow) { 
                    up.Switch();
                    register.Set("up", up.GetBool());
                }
                if (key == ConsoleKey.DownArrow)
                { 
                    down.Switch();
                    register.Set("down", down.GetBool());
                }
                if (key == ConsoleKey.LeftArrow)
                { 
                    left.Switch();
                    register.Set("left", left.GetBool());
                }
                if (key == ConsoleKey.RightArrow)
                { 
                
                    right.Switch();
                    register.Set("right", right.GetBool());
                }
            } while (key != ConsoleKey.Escape);

        }

        private static void Testwith(BooleanStateRegister register)
        {
            RefBooleanRegister refregister = new RefBooleanRegister(register);
            LogicBlock logic = null;

            LogicBlockBuilder builder = new LogicBlockBuilder();

            ITimeValue tf = new TimeInMsLong(5000);
            ITimeValue tn = new TimeInMsLong(0);

            IBoolObservedTime key = new BL_TimeToObserve(true, new BL_RelativeTimeFromNow(tf));
            IBoolObservedTime range = new BL_TimeToObserve(true, new BL_RelativeTimeDurationFromNow(tn, tf));

            //logic = builder.Start(new AndLogic(
            //    new RegisterRefStateTrueBlock(m_defaultregister, new BL_BooleanItemDefault("up")),
            //    new RegisterRefBoolExistBlock(m_defaultregister, new BL_BooleanItemExist("up")),
            //    new RegisterRefStateBlock(m_defaultregister, new BL_BooleanItemIsTrueOrFalse("up")),
            //    new RegisterRefStateAtBlock(m_defaultregister, new BL_BooleanItemIsTrueOrFalseAt("up", range, true)),
            //    new RegisterRefMaintainingBlock(m_defaultregister, new BL_BooleanItemMaintaining("up", range, true)),
            //    new RegisterRefSwitchBetweenBlock(m_defaultregister, new BL_BooleanItemSwitchBetween("up", range, true))
            //    )).GetCurrent();


            logic = builder.AppendLeft(AppendDuoType.And,
            new RegisterRefStateTrueBlock(refregister, new BL_BooleanItemDefault("up"))
           ).GetCurrent();
            DisplayLogic(logic);
            builder.Flush();

            logic = builder.AppendLeft(AppendDuoType.And,
        new RegisterRefBoolExistBlock(refregister, new BL_BooleanItemExist("up"))
         ).GetCurrent();
            DisplayLogic(logic);
            builder.Flush();

            logic = builder.AppendLeft(AppendDuoType.And,
         new RegisterRefStateBlock(refregister, new BL_BooleanItemIsTrueOrFalse("up", false))
         ).GetCurrent();
            DisplayLogic(logic);
            builder.Flush();

            logic = builder.AppendLeft(AppendDuoType.And,
                     new RegisterRefStateAtBlock(refregister, new BL_BooleanItemIsTrueOrFalseAt("up", key, true))
                     ).GetCurrent();
            DisplayLogic(logic);
            builder.Flush(); 
            
            //logic = builder.AppendLeft(AppendDuoType.And,
            //       new RegisterRefStateAtBlock(refregister, new BL_BooleanItemIsTrueOrFalseAt("up", range, true))
            //       ).GetCurrent();
            //DisplayLogic(logic);
            //builder.Flush();


        }

        private static void DisplayLogic(LogicBlock logic)
        {
            bool value, computed;
            logic.Get(out value, out computed);
            Console.WriteLine(string.Format("Result: {0}{1}", value ? "1" : "0", computed ? "" : "e"));
        }
    }
}
