using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item.Builder;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item.Time;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Unstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static BooleanRegisterUtilityAPI_TDD.ProgramParser;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken
{


    

    public class BLTokensToBLBuilder
    {
        TextLineSpliteAsBooleanLogicTokens m_tokenSpliter;
        List<BL_BooleanItem> items = new List<BL_BooleanItem>();

        public BLTokensToBLBuilder(TextLineSpliteAsBooleanLogicTokens tokenSpliter, out BL_BuilderElements elements)
        {
            elements = new BL_BuilderElements();
            // Console.WriteLine("A");
            m_tokenSpliter = tokenSpliter;

            IEnumerable<string> tokensAsString = tokenSpliter.GetTokens();
            List<StringTokenTypeAndSource> tokens = new List<StringTokenTypeAndSource>();
            foreach (string item in tokensAsString)
            {
                StringTokenTypeAndSource t;
                GetTokenFrom(item, out t);
                tokens.Add(t);
            }

            elements.SetTokens(tokens);

            StringTokensRegister itemtoken = m_tokenSpliter.m_items;
            items = new List<BL_BooleanItem>();
            bool found;
            // Console.WriteLine("C");
            for (uint i = 0; i < itemtoken.GetCount(); i++)
            {
                BL_BooleanItem bItem;
                string textOfItem = itemtoken.GetFromIndex(i);
                //Console.WriteLine("D");
                TryToParse(textOfItem, out found, out bItem);
                if (found)
                {
                    //    Console.WriteLine("-D>"+bItem);
                    items.Add(bItem);
                }
            }
            elements.SetItemAs(items);

           // Console.WriteLine("B");
        }

        public static Regex booleanNameFormat = new Regex("[a-zA-Z][a-zA-Z0-9]*");
        private void TryToParse(string textOfItem, out bool found, out BL_BooleanItem bItem)
        {
            found = false;
            bItem = null;
            textOfItem = textOfItem.Trim();
            string boolName;
            ExtractBooleanName(textOfItem, out boolName);
            string boolmeta = textOfItem.Substring(boolName.Length).Trim();
            Console.WriteLine("DD:" + boolName + " -- " + boolmeta);

            if (boolName.Trim().Length == 0)
                return;

            bool manadged=false;
            if (string.IsNullOrEmpty(boolmeta))
            {

                bItem = new BL_BooleanItemDefault(boolName);
                manadged = true;
            }
            else if (boolmeta.Length == 1)
            {
                if (boolmeta[0] == '?')
                {
                    bItem = new BL_BooleanItemExist(boolName, BoolExistanceState.Exist);
                    manadged = true;
                }
                else if (boolmeta[0] == '⁉')
                {
                    bItem = new BL_BooleanItemExist(boolName, BoolExistanceState.DontExist);
                    manadged = true;
                }
                else if (boolmeta[0] == '‾'  || boolmeta[0] == '↑')
                {
                    bItem = new BL_BooleanItemIsTrueOrFalse(boolName, false);
                    manadged = true;
                }
                else if (boolmeta[0] == '_' || boolmeta[0] == '↓')
                {
                    bItem = new BL_BooleanItemIsTrueOrFalse(boolName, true);
                    manadged = true;
                }
            }
            else if (boolmeta.Length == 2)
            {

                if (boolmeta[0] == '!' && boolmeta[0] == '?')
                {
                    bItem = new BL_BooleanItemExist(boolName, BoolExistanceState.DontExist);
                    manadged = true;
                }

            }
            else if (!manadged && boolmeta.Length > 1)
            {
                char checktype = boolmeta[0];
                string timevalue = boolmeta.Substring(1);
                IBoolObservedTime timeObserved = ParseTimeObserved(timevalue);

                if (timeObserved != null) { 

                    if (boolmeta[0] == '_' || boolmeta[0] == '‾')
                    {
                        bItem = new BL_BooleanItemMaintaining(boolName, timeObserved, checktype == '_');
                        manadged = true;
                    }

                    else if (boolmeta[0] == '↓' || boolmeta[0] == '↑')
                    {
                        bItem = new BL_BooleanItemSwitchBetween(boolName, timeObserved, checktype == '↓');
                        manadged = true;
                    }
                }
            }


            found = bItem != null;
        }

        private IBoolObservedTime ParseTimeObserved(string timevalue)
        {
            timevalue = timevalue.ToLower();
            IBoolObservedTime result = null;

            if (timevalue.IndexOf("⏰") > -1)
            {
                timevalue = timevalue.Replace("⏰", "");
                //ABSOLUTE TIME
                if (timevalue.IndexOf("#") > -1)
                {
                    ITimeOfDay lt = null;
                    ITimeOfDay rt = null;
                    string[] tokens = timevalue.Split('#');
                    if (tokens.Length == 2)
                    {

                        ConvertToAbsoluteTime(tokens[0], out lt);
                        ConvertToAbsoluteTime(tokens[1], out rt);
                        result = new BL_TimeToObserve(false, new BL_AbsoluteTimeDurationFromNow(lt,rt));
                    }

                }
                else {

                    ITimeOfDay timeOfDay = null;
                    ConvertToAbsoluteTime(timevalue, out timeOfDay);
                    result = new BL_TimeToObserve(false, new BL_AbsoluteTimeFromNow(timeOfDay));
                }

            }
            else {
                //RELATIVE TIME
                if (timevalue.IndexOf("#") > -1)
                {
                    string[] tokens = timevalue.Split('#');
                    if (tokens.Length == 2)
                    {
                        ITimeValue part1 = GetTimeOf(tokens[0]),
                            part2 = GetTimeOf(tokens[1]);
                        if(part1!=null && part2!=null)
                        result = new BL_TimeToObserve(false, new BL_RelativeTimeDurationFromNow(part1, part2));
                    }

                }
                else {

                    ITimeValue value = GetTimeOf(timevalue);
                    if(value!=null)
                        result = new BL_TimeToObserve(false, new BL_RelativeTimeFromNow(value));
                }

            }
            return result;
        }

        private static void ConvertToAbsoluteTime(string timevalue, out ITimeOfDay timeOfDay)
        {
            timeOfDay = null;
            if (timevalue.IndexOf(":") > -1)
            {
                string[] tokens = timevalue.Split(':');
                if (tokens.Length == 2)
                {
                    timeOfDay = new BL_TimeOfDay(tokens[0], tokens[1], "0", "0");
                }
                else if (tokens.Length == 3)
                {
                    timeOfDay = new BL_TimeOfDay(tokens[0], tokens[1], tokens[2], "0");
                }
                else if (tokens.Length == 4)
                {
                    timeOfDay = new BL_TimeOfDay(tokens[0], tokens[1], tokens[3], tokens[4]);
                }
            }
            else
            {
                string h = "0", m = "0", s = "0", ms = "0";
                string left = "ms";
                Match match = Regex.Match(timevalue, "[0-9]+h");
                if (match.Success)
                {
                    h = match.Value.Replace("h", "");
                    left = "m";
                }

                match = Regex.Match(timevalue, "[0-9]+m[^s]");
                if (match.Success)
                {
                    m = match.Value.Replace("m", "");
                    left = "s";
                }

                match = Regex.Match(timevalue, "[0-9]+s");
                if (match.Success)
                {
                    s = match.Value.Replace("s", "");
                    left = "ms";
                }

                match = Regex.Match(timevalue, "[0-9]+ms");
                if (match.Success)
                {
                    ms = match.Value.Replace("ms", "");
                    left = "ms";
                }
                match = Regex.Match(timevalue, "[0-9]+$");
                if (match.Success)
                {
                    if (left == "m") m = match.Value;
                    if (left == "s") s = match.Value;
                    if (left == "ms") ms = match.Value;

                }

                timeOfDay = new BL_TimeOfDay(h, m, s, ms);
            }

        }

        private ITimeValue GetTimeOf(string timeAsString)
        {
            
            long mutiplicator = 1;
            if (timeAsString.IndexOf("s") > -1)
                mutiplicator = 1000;
            else if (timeAsString.IndexOf("m") > -1)
                mutiplicator = 60000;
            else if (timeAsString.IndexOf("h") > -1)
                mutiplicator = 3600000;

            timeAsString = timeAsString.Replace(",", ".");
            timeAsString = Regex.Replace(timeAsString, "[^0-9\\.,]", "");
            double time;
            if (double.TryParse(timeAsString, out time))
            {
                return new TimeInMsLong((long)(time * (double)(mutiplicator)));
            }
            return null;

        }

        private static void ExtractBooleanName(string textOfItem, out string boolName)
        {
            StringBuilder  sb = new StringBuilder();
            int c = 0;
            while (c < textOfItem.Length && IsAlpahNumeric(textOfItem[c]))
            {
                sb.Append(textOfItem[c]);
                c++;

            }
            boolName = sb.ToString();
        }

        public static bool IsAlpahNumeric(char c) {
            return
            c == 'a' || c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f' || c == 'g' || c == 'h' || c == 'i' || c == 'j' || c == 'k' || c == 'l' || c == 'm' || c == 'n' || c == 'o' || c == 'p' || c == 'q' || c == 'r' || c == 's' || c == 't' || c == 'u' || c == 'v' || c == 'w' || c == 'x' || c == 'y' || c == 'z'
          || c== 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'F' || c == 'G' || c == 'H' || c == 'I' || c == 'J' || c == 'K' || c == 'L' || c == 'M' || c == 'N' || c == 'O' || c == 'P' || c == 'Q' || c == 'R' || c == 'S' || c == 'T' || c == 'U' || c == 'V' || c == 'W' || c == 'X' || c == 'Y' || c == 'Z'
          || c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9';
        }

        public static void GetTokenFrom(string text, out StringTokenTypeAndSource token) {

            token = new StringTokenTypeAndSource(GetTokenType(text), text);
        }

        public static TokenType GetTokenType(string text)
        {
            text = text.Trim();
            TokenType ttype = TokenType.UNKOWN;

            if (text.Length > 1 && text[0] == 'I')
                return ttype = TokenType.BooleanToken;
            if (text == "(") return ttype = TokenType.S_BRACKET;
            if (text == ")") return ttype = TokenType.E_BRACKET;
            if (text == "[") return ttype = TokenType.S_SQUAREBRACKET;
            if (text == "]") return ttype = TokenType.E_SQUAREBRACKET;
            if (text == "|") return ttype = TokenType.B_OR;
            if (text == "&") return ttype = TokenType.B_AND;
            if (text == "≡") return ttype = TokenType.B_EQU;
            if (text == "¬") return ttype = TokenType.NEGATIVE;
            if (text == "⊗") return ttype = TokenType.B_XOR;
            if (text == "<") return ttype = TokenType.B_ALESSB;
            if (text == ">") return ttype = TokenType.B_AMOREB;
            if (text == "≤") return ttype = TokenType.B_ALESSOREQUALB;
            if (text == "≥") return ttype = TokenType.B_AMOREOREQUALB;
            if (text == "🀸") return ttype = TokenType.DominoLeftTrue;
            if (text == "🀲") return ttype = TokenType.DominoRightTrue;

            if (text == "[⊗") return ttype = TokenType.S_XOR;
            if (text == "[&") return ttype = TokenType.S_AND;
            if (text == "[|") return ttype = TokenType.S_OR;
            if (text == "[≡") return ttype = TokenType.S_XEQ;
            return ttype;
        }

    }
    public class StringTokenTypeAndSource
    {

        TokenType m_token;
        string m_linkedSource;

        public StringTokenTypeAndSource(TokenType token, string linkedSource)
        {
            m_token = token;
            m_linkedSource = linkedSource;
        }

        public string GetStringSource() { return m_linkedSource; }
        public TokenType GetTokenType() { return m_token; }

        public bool IsBoolToken()
        {
            return m_token == TokenType.BooleanToken;
        }
        public bool IsBoolToken(out int index) {
            index = 0;
            if (IsBoolToken())
            {

               if(int.TryParse( m_linkedSource.Replace("I", ""), out index)){
                    return true;
               }
            }
            return false;
        }

        public override string ToString()
        {
            return "["+m_token + ":" + m_linkedSource+"]";
        }
    }
    public enum TokenType
    {
        UNKOWN,
        S_BRACKET, E_BRACKET,
        S_SQUAREBRACKET, E_SQUAREBRACKET,
        S_AND, S_OR,
        S_XOR, S_XEQ,

        DominoLeftTrue,
        DominoRightTrue,

        BooleanToken,
        NEGATIVE,
        B_ALESSB, B_AMOREB,
        B_OR, B_AND,
        B_ALESSOREQUALB,
        B_AMOREOREQUALB,
        B_XOR,
        B_EQU
    }
}