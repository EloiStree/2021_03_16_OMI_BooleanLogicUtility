
using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD
{
    class ProgramLogicBuilder
    {
        static void Main(string[] args)
        {
            LogicBlockBuilder builder = new LogicBlockBuilder();

            Console.WriteLine(builder.GetCurrent());
            builder.AppendLeft(AppendDuoType.And, new PrimitiveBoolLogicBlock(true));
            Console.WriteLine(builder.GetCurrent());
            builder.AppendLeft(AppendDuoType.And, new PrimitiveBoolLogicBlock(false));
            Console.WriteLine(builder.GetCurrent());
            builder.InverseWarp();
            builder.InverseWarp();
            builder.AppendRight("|", new XorDuoLogic(new PrimitiveBoolLogicBlock(false),new InverseLogic( new PrimitiveBoolLogicBlock(false))) );
            Console.WriteLine(builder.GetCurrent());
            builder.Append(AppendSide.Left, AppendDuoType.And, new PrimitiveBoolLogicBlock(true));
            builder.Append(AppendSide.Left, AppendDuoType.LessEq, new PrimitiveBoolLogicBlock(true));
            Console.WriteLine(builder.GetCurrent());

            Console.ReadLine();
        }
        
    }
}
