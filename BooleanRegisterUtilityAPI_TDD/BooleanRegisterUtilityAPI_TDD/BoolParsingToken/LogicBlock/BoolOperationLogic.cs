using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock
{
    public class BoolOperationLogic
    {
        #region LESS OR MORE

        public static bool ALessB(bool a, bool b)
        {//A B R
         //1 1 0
         //1 0 0
         //0 1 1
         //0 0 0

            return !a && b;
        }
        public static bool ALessOrEqualB(bool a, bool b)
        {//A B R
         //1 1 1
         //1 0 0
         //0 1 1
         //0 0 1

            return !a && b || a==b;
        }
        public static bool AMoreB(bool a, bool b)
        {   //A B R
            //1 1 0
            //1 0 1
            //0 1 0
            //0 0 0
            return a && !b;
        }
        public static bool AMoreOrEqualB(bool a, bool b)
        {   //A B R
            //1 1 1
            //1 0 1
            //0 1 0
            //0 0 1
            return a && !b || a == b;
        }
        #endregion
        #region AND
        public static bool NAND(bool a, bool b) { return !AND(a, b); }
        public static bool AND(bool a, bool b)
        {   //A B R
            //1 1 1
            //1 0 0
            //0 1 0
            //0 0 0
            return a && b;
        }
        public static bool NAND(params bool[] values) { return !AND(values); }
        public static bool AND(params bool[] values)
        {
            if (values.Length == 2)
                AND(values[0], values[1]);
            return AllTrue(values);
        }
        #endregion
        #region OR
        public static bool NOR(bool a, bool b) { return !OR(a, b); }
        public static bool OR(bool a, bool b)
        {   //A B R
            //1 1 1
            //1 0 0
            //0 1 0
            //0 0 0
            return a || b;
        }
        public static bool NOR(params bool[] values) { return !OR(values); }
        public static bool OR(params bool[] values)
        {
            if (values.Length == 2)
                OR(values[0], values[1]);
            return OnlyNeedOneToBeTrue(values);
        }
        #endregion
        #region XOR
        public static bool NXOR(bool a, bool b) { return !XOR(a, b); }
        public static bool XOR(bool a, bool b)
        {   //A B R
            //1 1 0
            //1 0 1
            //0 1 1
            //0 0 0
            return a != b;
        }
        public static bool NXOR(params bool[] values) { return !XOR(values); }
        public static bool XOR(params bool[] values)
        {
            if (values.Length == 2)
                XOR(values[0], values[1]);
            return OnlyOneIs(true,values);
        }


        #endregion
        #region EQUIVALANCE

        public static bool NEQUIVALANCE(bool a, bool b) {
            //A B R
            //1 1 0
            //1 0 1
            //0 1 1
            //0 0 0
            return a != b;
        }
        public static bool EQUIVALANCE(bool a, bool b)
        {   //A B R
            //1 1 1
            //1 0 0
            //0 1 0
            //0 0 1
            return a == b;
        }
        public static bool EQUIVALANCE(params bool[] values)
        {

            if (values.Length == 2)
                EQUIVALANCE(values[0], values[1]);
            return AllTrue(values) || AllFalse(values);

        }
        public static bool NOTEQUIVALANCE(params bool[] values)
        {
            return !EQUIVALANCE(values);

        }
        #endregion


        #region OTHER
        public static bool INVERSE(bool value) { return !value; }
        public static bool AllFalse(bool[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i]==true)
                    return false;

            }
            return true;
        }
        public static bool AllTrue(bool[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == false)
                    return false;

            }
            return true;
        }
        public static bool OnlyNeedOneToBe(bool checkFor, params bool[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == checkFor)
                    return true;
            }
            return false;
        }
        public static bool OnlyNeedOneToBeFalse(bool[] values)
        {
            return OnlyNeedOneToBe(false, values);
        }
        public static bool OnlyNeedOneToBeTrue(bool[] values)
        {
            return OnlyNeedOneToBe(true, values);
        }
        public static bool OnlyOneIs(bool checkFor, params bool[] values)
        {
            int count = 0;
            for (int i = 0; i < values.Length; i++)
            {

                if (values[i] == checkFor)
                    count++;
                if (count > 1)
                    return false;
            }
            return count == 1;
        }
        public static bool OnlyOneIsFalse(bool[] values)
        {
            return OnlyOneIs(false, values);
        }
        public static bool OnlyOneIsTrue(bool[] values)
        {
            return OnlyOneIs(true, values);
        }
        /// <summary>
        ///   a group must all be true when the other must be all false. I call it domino because I use [ some boolean 🀸 some boolean ] in my parser
        /// </summary>
        /// <param name="mustBeTrue"></param>
        /// <param name="mustBeFalse"></param>
        /// <returns></returns>
        public static bool Domino(bool[] mustBeTrue, bool[] mustBeFalse)
        {
            return AllTrue(mustBeTrue) && AllFalse(mustBeFalse);
        }
        public static bool Domino(bool mustBeTrue, bool mustBeFalse)
        {
            return mustBeTrue == true && mustBeFalse == false;
        }
        #endregion
    }
}
