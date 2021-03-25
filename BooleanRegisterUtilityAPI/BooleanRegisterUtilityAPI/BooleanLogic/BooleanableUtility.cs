using BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic
{
    public class BooleanableUtility
    {
        private static bool HasBooleanNull(ref IBooleanableRef[] booleans)
        {
            for (int i = 0; i < booleans.Length; i++)
            {
                if (booleans[i] == null)
                    return true;
            }
            return false;
        }
        public static void AND(out bool result, out bool succed, params IBooleanableRef[] booleans)
        {
             AllTrue(out result, out succed, booleans);
        }
        public static void NAND(out bool result, out bool succed, params IBooleanableRef[] booleans)
        {
            AND(out result, out succed, booleans);
            result = !result;

        }
        public static void OR(out bool result, out bool succed, params IBooleanableRef[] booleans)
        {
            if (HasBooleanNull(ref booleans))
            {
                result = false;
                succed = false;
                return;
            }

            result = false;
            bool v, ok;
            for (int i = 0; i < booleans.Length; i++)
            {
                booleans[i].GetBooleanableState(out v, out ok);
                if (!ok)
                {
                    result = false;
                    succed = false;
                    return;
                }
                if (v ==true)
                {
                    result = true;
                    break;
                }

            }
            succed = true;

        }
        public static void NOR(out bool result, out bool succed, params IBooleanableRef[] booleans)
        {
            OR(out result, out succed, booleans);
            result = !result;
           
        }

        public static void XOR(out bool value, out bool succed, IBooleanableRef leftArg, IBooleanableRef rightArg)
        {
            bool a, b, ok;
            leftArg.GetBooleanableState(out a, out ok);
            if (!ok)
            {
                value = false;
                succed = false;
                return;
            }
            rightArg.GetBooleanableState(out b, out ok);
            if (!ok)
            {
                value = false;
                succed = false;
                return;
            }

            succed =true;
            value = a == !b;
        }
        public static void NXOR(out bool result, out bool succed, IBooleanableRef leftArg, IBooleanableRef rightArg)
        {

            XOR(out result, out succed, leftArg, rightArg);
            result = !result;
        }



        public static void Equivalence(out bool value, out bool succed, IBooleanableRef leftArg, IBooleanableRef rightArg)
        {
            bool a, b, ok;
            leftArg.GetBooleanableState(out a, out ok);
            if (!ok)
            {
                value = false;
                succed = false;
                return;
            }
            rightArg.GetBooleanableState(out b, out ok);
            if (!ok)
            {
                value = false;
                succed = false;
                return;
            }

            succed = true;
            value = a == b;
        }
        public static void OnlyRightTrue(out bool value, out bool succed, IBooleanableRef leftArg, IBooleanableRef rightArg)
        {
            bool l, r, ok;
            leftArg.GetBooleanableState(out l, out ok);
            if (!ok)
            {
                value = false;
                succed = false;
                return;
            }
            rightArg.GetBooleanableState(out r, out ok);
            if (!ok)
            {
                value = false;
                succed = false;
                return;
            }

            succed = true;
            value = !l && r;
        }
        public static void OnlyLeftTrue(out bool value, out bool succed, IBooleanableRef leftArg, IBooleanableRef rightArg)
        {
            OnlyRightTrue(out value, out succed, rightArg, leftArg);
        }
        public static void MaterialImplication(out bool value, out bool succed, IBooleanableRef leftArg, IBooleanableRef rightArg)
        {
            bool a, b, ok;
            leftArg.GetBooleanableState(out a, out ok);
            if (!ok)
            {
                value = false;
                succed = false;
                return;
            }
            rightArg.GetBooleanableState(out b, out ok);
            if (!ok)
            {
                value = false;
                succed = false;
                return;
            }

            succed = true;
            value = a==true && b==false;
        }
        public static void CreateInverse( IBooleanableRef[] input, out IBooleanableRef[] ouput, out bool succed) {

            bool value, ok = false;
            ouput = new IBooleanableRef[input.Length];

            for (int i = 0; i < ouput.Length; i++)
            {
                input[i].GetBooleanableState(out value, out ok);
                if (!ok)
                {
                    succed = false ;
                    return;
                }

                ouput[i] = new BooleanablePrimitiveValue(!value);
            }

            succed = true;

        }

        /// <summary>
        /// Check that in the given groupe, that only the given one are true
        /// </summary>
        /// <param name="result"></param>
        /// <param name="error"></param>
        /// <param name="areTrue"></param>
        /// <param name="group"></param>
        public static void AreExclusivelyTrue(out bool result, out bool succed, IBooleanableRef[] areTrue, IBooleanableRef[] group)
        {
            IBooleanableRef[] other = group.Except(areTrue).ToArray();
            bool t,f, ok;
            AllTrue(out t, out ok, areTrue);
            if (!ok) {
                result= false;
                succed = false;
                return;
            }

            AllFalse(out f, out ok, other);
            if (!ok)
            {
                result = false;
                succed = false;
                return;
            }

            result = t && f;
            succed = true;
        }

        /// <summary>
        /// Check that in the given groupe, that only the given one are false
        /// </summary>
        /// <param name="result"></param>
        /// <param name="error"></param>
        /// <param name="areTrue"></param>
        /// <param name="group"></param>
        public static void AreExclusivelyFalse(out bool result, out bool succed, IBooleanableRef[] areFalse, IBooleanableRef[] group)
        {
            IBooleanableRef[] other = group.Except(areFalse).ToArray();
            bool t, f, ok;
            AllFalse(out t, out ok, areFalse);
            if (!ok)
            {
                result = false;
                succed = false;
                return;
            }

            AllTrue(out f, out ok, other);
            if (!ok)
            {
                result = false;
                succed = false;
                return;
            }

            result = t && f;
            succed = true;
        }

        public static void AllFalse(out bool result, out bool succed, IBooleanableRef[] group)
        {

            All(false, out result, out succed, group);
        }

        public static void AllTrue(out bool result, out bool succed, IBooleanableRef[] group)
        {
            All(true, out result, out succed, group);
        }
        private static void All(bool value, out bool result, out bool succed, IBooleanableRef[] group)
        {
            if (HasBooleanNull(ref group))
            {
                result = false;
                succed = false;
                return;
            }

            result = true;
            bool v, ok;
            for (int i = 0; i < group.Length; i++)
            {
                group[i].GetBooleanableState(out v, out ok);
                if (!ok)
                {
                    result = false;
                    succed = false;
                    return;
                }
                if (v == !value)
                {
                    result = false;
                    break;
                }

            }
            succed = true;
        }

        public static void OnlyOneTrue(out bool result, out bool succed, IBooleanableRef[] group)
        {
            OnlyOne(true, out result, out succed, group);
        }
        public static void OnlyOneFalse(out bool result, out bool succed, IBooleanableRef[] group)
        {
            OnlyOne(false,out result,out succed, group);
        }
        private static void OnlyOne(bool value, out bool result, out bool succed, IBooleanableRef[] group)
        {
            if (HasBooleanNull(ref group))
            {
                result = false;
                succed = false;
                return;
            }

            bool v, ok;
            short count = 0;
            for (int i = 0; i < group.Length; i++)
            {
                group[i].GetBooleanableState(out v, out ok);
                if (!ok)
                {
                    result = false;
                    succed = false;
                    return;
                }
                if (v == value)
                {
                    count++;
                    if (count > 1)
                    {
                        result = false;
                        succed = true;
                        return;
                    }
                }

            }
            result = count == 1;
            succed = true;
        }
       

    }


    public class BooleanableBinaryLaw {
        //https://en.wikipedia.org/wiki/Boolean_algebra
        public char m_xor = '⊕';
        public char m_or = '∨';
        public char m_por = '|';
        public char m_and = '∧';
        public char m_pand = '&';
        public char m_inverse = '¬';
        public char m_pinverse = '!';
        public char m_materialImplication = '→';
        public char m_equivalence = '≡';

        public String m_sxor = "xor";
        public String m_sor = "or";
        public String m_sand = "and";
        public String m_snxor = "nxor";
        public String m_snor = "nor";
        public String m_snand = "nand";


    }
}
