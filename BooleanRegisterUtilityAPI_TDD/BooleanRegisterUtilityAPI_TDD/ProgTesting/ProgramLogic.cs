using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD
{
    public  class ProgramLogic
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
            do {

            bool valide;
            bool bup, bdown, bleft, bright;
            up.Get(out bup, out valide);
            down.Get(out bdown, out valide);
            left.Get(out bleft, out valide);
            right.Get(out bright, out valide);

            Console.WriteLine(string.Format("INPUT: u{0} d{1} l{2} r{3}", bup, bdown, bleft, bright));
            List<LogicBlock> logic = new List<LogicBlock>();
            // AND 2
            logic.Insert(0,new AndDuoLogic(left, right));
            DisplayLastLogic(ref logic);
            // AND n
            logic.Insert(0,new AndLogic(left, right , up, down));
            DisplayLastLogic(ref logic);
            // OR 2
            logic.Insert(0,new OrDuoLogic(left, right));
            DisplayLastLogic(ref logic);
            // OR n
            logic.Insert(0,new OrLogic(left, right, up, down));
            DisplayLastLogic(ref logic);
            // XOR 2
            logic.Insert(0,new XorDuoLogic(left, right));
            DisplayLastLogic(ref logic);
            // XOR n
            logic.Insert(0,new XorLogic(left, right, up, down));
            DisplayLastLogic(ref logic);
            // XEQ 2
            logic.Insert(0,new EquivalentDuoLogic(left, right));
            DisplayLastLogic(ref logic);
                // XEQ n; ; ; 
                logic.Insert(0, new EquivalentLogic(left, right, up, down));  ;
            DisplayLastLogic(ref logic);

            // NAND 2
            logic.Insert(0,new NotAndDuoLogic(left, right));
            DisplayLastLogic(ref logic);
            // NAND n
            logic.Insert(0,new NotAndLogic(left, right, up, down));
            DisplayLastLogic(ref logic);
            // NOR 2
            logic.Insert(0,new NotOrDuoLogic(left, right));
            DisplayLastLogic(ref logic);
            // NOR n
            logic.Insert(0,new NotOrLogic(left, right, up, down));
            DisplayLastLogic(ref logic);
            // XOR 2
            logic.Insert(0,new NotXorDuoLogic(left, right));
            DisplayLastLogic(ref logic);
            // XOR n
            logic.Insert(0,new NotXorLogic(left, right, up, down));
            DisplayLastLogic(ref logic);
            // NXEQ 2 

            logic.Insert(0,new NotEquivalentDuoLogic(left, right));
            DisplayLastLogic(ref logic);
            // NXEQ n
            logic.Insert(0,new NotEquivalentLogic(left, right, up, down));
            DisplayLastLogic(ref logic);

            // < 2
            logic.Insert(0,new LessDuoLogic(left, right));
            DisplayLastLogic(ref logic);
            // > 2
            logic.Insert(0,new MoreDuoLogic(left, right));
            DisplayLastLogic(ref logic);

            // ≤ 2
            logic.Insert(0,new LessOrEqualDuoLogic(left, right));
            DisplayLastLogic(ref logic);
            // ≥ 2
            logic.Insert(0,new MoreOrEqualDuoLogic(left, right));
            DisplayLastLogic(ref logic);


            // INVERSE primitive
            logic.Insert(0,new InverseLogic(left));
            DisplayLastLogic(ref logic);

            // DOMINO group
            logic.Insert(0,new DominoLogic(new LogicBlock[] { left, right}, new LogicBlock[] { up, down }));
            DisplayLastLogic(ref logic);



                key = Console.ReadKey().Key;
                if (key == ConsoleKey.UpArrow)
                    up.Switch();
                if (key == ConsoleKey.DownArrow)
                    down.Switch();
                if (key == ConsoleKey.LeftArrow)
                    left.Switch();
                if (key == ConsoleKey.RightArrow)
                    right.Switch();
            } while (key != ConsoleKey.Escape);

        }
        
        private static void DisplayLastLogic(ref List<LogicBlock> logic)
        {
            if (logic == null || logic.Count < 1) return;
            bool value, computed;
            logic[0].Get(out value, out computed);
            Console.WriteLine(""+(value?'1':'0')+(computed ? "" : "e") + "\t> " + logic[0]);
        }

        private static void GetUp(out bool value, out bool computed)
        {
            value = true;
            computed = true;
        }
        private static void GetDown(out bool value, out bool computed)
        {
            value = true;
            computed = true;
        }

        private static void GetLeft(out bool value, out bool computed)
        {
            value = true;
            computed = true;
        }
        private static void GetRight(out bool value, out bool computed)
        {
            value = true;
            computed = true;
        }
    }
}
