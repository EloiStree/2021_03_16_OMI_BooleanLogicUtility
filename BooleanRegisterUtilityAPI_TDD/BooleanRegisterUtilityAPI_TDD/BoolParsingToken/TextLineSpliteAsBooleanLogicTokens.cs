using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Unstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken
{
    public class TextLineSpliteAsBooleanLogicTokens
    {
        public string m_textSource = "";
        public string m_currentTranslation = "";
        public string[] m_textTokens = new string[0];

        public StringTokensRegister m_items = new StringTokensRegister();






    public IEnumerable<string> GetTokens()
        {
            return m_textTokens;
        }

        public TextLineSpliteAsBooleanLogicTokens(string text, bool useDebug)
        {
            m_textSource = text;
            string currentText = text;
           
            if(useDebug)
                Console.WriteLine( "0:" + currentText );

            currentText = PutSomeSpace(ReserveKeywords(currentText));
            if (useDebug) 
                Console.WriteLine("1:" + currentText);

            currentText = IsolateItems(currentText);

             currentText = RemoveSpace(currentText);
            if (useDebug) 
                Console.WriteLine("2:" + currentText);

            m_textTokens = currentText.Trim().Split(' ');
            m_currentTranslation = currentText;
        }

        private string IsolateItems(string t)
        {
            t = RemoveSpace(t);
            Regex r = new Regex("[a-zA-Z][a-zA-Z0-9]*[^\\s]*");
            //t = r.Replace(t, " I ")⌃⌄;

            bool added; uint count;
            foreach (Match item in r.Matches(t)) {
                string value = item.Value;
                m_items.AddItem(value, out added, out count);
                if (added) {
                    t=t.Replace(value, " I" + count+" ");
                
                }
            }
            
            return RemoveSpace(t);
        }

        private string PutSomeSpace(string t)
        {
         


            t = t.Replace("]", " ] ");
            t = t.Replace("!", " ¬ ");
            t = t.Replace("~", " ¬ ");
            t = t.Replace("¬", " ¬ ");
            t = t.Replace("(", " ( ");
            t = t.Replace(")", " ) ");
            t = t.Replace("+", " & ");
            t = t.Replace("∧", " & ");
            t = t.Replace("∨", " | ");
            t = t.Replace("<", " < ");
            t = t.Replace(">", " > ");
            t = t.Replace("≤", " ≤ ");
            t = t.Replace("≥", " ≥ ");
            t = t.Replace("🀸", " 🀸 ");
            t = t.Replace("🀲", " 🀲 ");

            return t;
        }

        private string ReserveKeywords(string t)
        {
            t = Regex.Replace(t, "\\[and\\s", " [& ", RegexOptions.IgnoreCase);
            t = Regex.Replace(t, "\\[or\\s", " [| ", RegexOptions.IgnoreCase);
            t = Regex.Replace(t, "\\[xor\\s", " [⊗ ", RegexOptions.IgnoreCase);
            t = Regex.Replace(t, "\\[xeq\\s", " [≡ ", RegexOptions.IgnoreCase);
            t = Regex.Replace(t, "\\[nand\\s", " ! [& ", RegexOptions.IgnoreCase);
            t = Regex.Replace(t, "\\[nor\\s", " ! [| ", RegexOptions.IgnoreCase);
            t = Regex.Replace(t, "\\[nxor\\s", " ! [⊗ ", RegexOptions.IgnoreCase);
            t = Regex.Replace(t, "\\[nxeq\\s", " ! [≡ ", RegexOptions.IgnoreCase);

            t = t.Replace(" or ", " | ");
            t = t.Replace(" and ", " & ");
            t = t.Replace(" xor ", " ⊗ ");
            t = t.Replace(" equal ", " ≡ ");
            t = t.Replace(" not ", " ! ");



            return t;
        }

        public string RemoveSpace(string t) {
            while (t.IndexOf("  ") >= 0) {
                t = t.Replace("  ", " ");
            }
            return t;
        }

    }
}
