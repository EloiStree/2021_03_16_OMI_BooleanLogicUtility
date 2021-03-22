using BooleanRegisterUtilityAPI.Beans;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BooleanRegisterUtilityAPI
{
    
    public class BooleanTextDebugUtility {


        public static void GetTextDescriptionOfRegister(ref BooleanStateRegister register,  out string resultText, int clampLenght=50) {

            StringBuilder sb = new StringBuilder();
            List<BooleanNamedHistory> stateRef;
            register.GetAllState(out  stateRef);

            for (int i = 0; i < stateRef.Count; i++)
            {
                string d = BoolHistoryDescription.GetDescriptionNowToPast(stateRef[i].GetHistory());
                sb.Append(string.Format("{0}: {1}\n", stateRef[i].GetName(), StringClamp(d, clampLenght)));

            }

            resultText = sb.ToString();
        }

        public  static string StringClamp(string text, int count)
        {
            if (text.Length < count)
                return text;
            return text.Substring(0, count);
        }

    }
}