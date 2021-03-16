using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD
{
    public class ParserListToExperiemntTDD
    {
        public string[] conditions = new string[] {
            //GROUP
            "[AND left up right]", //All forward
            "[AND left up right down]", // All down
            "[AND left down right]", // all backward
            "[OR left up right down]", // IsMoving
            "[AND left up right down]", // IsMoving
            "[XOR left right]", // One is down but not the other
            "[NAND left right]", // All but not both true
            "[NOR left right]", // Non are used
            "[NXOR left right]", // Are both equal false false or true true
            "[XOR up down]", // One is down but not the other
            "[XEQ up down]", // equivalent all are true
            "[NXEQ up down]", // equivalent all are false
            "[⊗ left right]",//One of them must be true
            "[⊗ left right up top]",//One of them must be true
            "[& left right]",
            "[| left right]",
            "[| left right]",
            "[≡ left right]", // equivalent all true or all false
            "[left right 🀲  up ]", // left and right are fals and up is true
            "[up down 🀸 left right]", // up and down are true. Left and right are false.
            //INVERSE
            "¬up",
            "!left",
            //1 ≥ 1  1
            //1 ≥ 0  1
            //0 ≥ 1  0
            //0 ≥ 0  1
            "left ≥ right",
            "left ≤ right",
             
            //SEPARATOR and PRIORITY
            "(left | right) & (up | down)", // Diagonal
            "(left ∨ right) ∧ (up ∨ down)", // Diagonal
            "(left or right) and (up or down)", // Diagonal
            
            //Or and Priority
            //"left || right |||| up + down ++ up",
            "(left | (right | up) ) + (down + up)",
            

            //Or and Priority upper then 3-4
            // + ++ +++ ++++ +5 +8 +12
            // - -- --- ---- -5 -8 -12
            // ⊗ ⊗⊗ ⊗⊗⊗ ⊗5 ⊗8 ⊗12
            // ≡ ≡ ≡ ≡5 ≡8 ≡12
            //"left |4 right ++ up + down + up",
            "((( (left | right) + up )  + (down + up))",
        
            //ITEM
            "shift⌊200",// pressed since 200ms
            "shift⌈200",// released since 200ms
            "shift↓200", //switch true since 200ms
            "shift↑200",//switch false since 200ms
            "shift⌊200:400",// maintaining true between 200ms and 400ms
            "shift↓200:400",// pressed since 200 ms
            "shift↑1.453s",//switch false since 1453ms
            "shift↑1.5m",//switch false since 1m 30 seconds
            "shift‾200", //was false there is 200ms
            "shift_200",//Was true there is 200ms
            "shift‾⏰12h45m30s456",//was false at 12 h 45m 30s 456 millisecond base on DateTime.Now
            "shift_⏰14:20:30",//Was true at 14 h 20m 30s base on DateTime.Now
            "shift_⏰14:20",//Was true at 14 h 20m  base on DateTime.Now
            
            "[ ]",
            "[ ]",
            ""


        };

    }
}
