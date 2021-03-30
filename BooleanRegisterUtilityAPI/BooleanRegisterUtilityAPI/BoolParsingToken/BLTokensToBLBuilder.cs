using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Builder;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.Unstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BooleanRegisterUtilityAPI.Enum;
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.RegisterRefBlock;

namespace BooleanRegisterUtilityAPI.BoolParsingToken
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
        public static BL_BooleanItem TryToParse(string textOfItem)
        {
            BL_BooleanItem  b= null;
            bool tmp;
            TryToParse(textOfItem,out tmp, out b);
            return b;
        
        }
        public static void TryToParse(string textOfItem, out bool found, out BL_BooleanItem bItem)
        {
            if (string.IsNullOrEmpty(textOfItem))
            { found = false; bItem = null; return; }


            found = false;
            bItem = null;
            textOfItem = textOfItem.Trim();
            string boolName;
            ExtractBooleanName(textOfItem, out boolName);

            if (string.IsNullOrEmpty(boolName))
            { found = false; bItem = null; return; }
            if (boolName.Length == textOfItem.Length) {
                found = true;
                bItem = new BL_BooleanItemDefault(boolName);
                return;
            }

            string boolmeta = textOfItem.Substring(boolName.Length).Trim();

            if (boolName.Trim().Length == 0)
                return;

            if (boolmeta.Length == 0)
                BuildOnEmptyMeta( out bItem, boolName);

            Switcher(out bItem, boolName, boolmeta);

            found = bItem != null;
        }



        private static void BuildOnEmptyMeta( out BL_BooleanItem bItem, string boolName)
        {
            bItem = new BL_BooleanItemDefault(boolName);
        }

        public static bool StartWith(string toLookAround, ref string toLookIn)
        {
            return toLookIn.IndexOf(toLookAround) == 0;
        }

        private static void Switcher(out BL_BooleanItem bItem, string boolName, string boolmeta)
        {

            bItem = null;

            if (StartWith("%", ref boolmeta))
            {

                bItem = CreatePourcentObserved(boolName, boolmeta);

            }
            else if (StartWith("‾_", ref boolmeta) || StartWith("_‾", ref boolmeta) || StartWith("‾‾", ref boolmeta) || StartWith("__", ref boolmeta))
            {
                bItem = CreateStartFinishInRange(boolName, boolmeta);


            }
            else if (StartWith("_", ref boolmeta))
            {

                bItem = CreateMaintainLogic(boolName, boolmeta);
            }

            else if (StartWith("‾", ref boolmeta) )
            {
                bItem = CreateMaintainLogic(boolName, boolmeta);


            }
           
            else if ( StartWith("↑", ref boolmeta))
            {

                bItem = CreateSwitchRecently(boolName, boolmeta);
            }
            else if (StartWith("↓", ref boolmeta))
            {

                bItem = CreateSwitchRecently(boolName, boolmeta);
            }

            else if (StartWith("↱", ref boolmeta))
            {
                bItem = CreateSwitchAndStay(boolName, boolmeta);
            }
            else if (StartWith("↳", ref boolmeta))
            {
                bItem = CreateSwitchAndStay(boolName, boolmeta);

            }
            else if (StartWith("⊓", ref boolmeta))
            {

                bItem = CreateBumpCount(boolName, boolmeta);
                if (bItem == null)
                    bItem = CreateBumpMorse(boolName, boolmeta);
            }
            else if (StartWith("⊔", ref boolmeta))
            {

                bItem = CreateBumpCount(boolName, boolmeta);
                if (bItem == null)
                    bItem = CreateBumpMorse(boolName, boolmeta);
            }
            else if (StartWith("∑", ref boolmeta))
            {

                bItem = CreateTimeCount(boolName, boolmeta);
            }


            else if (StartWith("?", ref boolmeta) && boolmeta.Length == 1)
            {
                bItem = CreateExistLogic(boolName, true);
            }
            else if (StartWith("¿", ref boolmeta) && boolmeta.Length == 1)
            {
                bItem = CreateExistLogic(boolName, false);
            }
            else if (StartWith("¿", ref boolmeta) )
            {
                bItem = CreateExistLogic(boolName, boolmeta);

            }

            else if (StartWith("?", ref boolmeta) )
            {
                bItem = CreateExistLogic(boolName, boolmeta);
            }


        }

       

        private static BL_BooleanItem CreateStartFinishInRange(string boolName, string boolmeta)
        {
            if (boolmeta.Length  <2) {
                return null;
            }
            if ((boolmeta[0] == '_' || boolmeta[0] == '‾') && (boolmeta[1] == '_' || boolmeta[1] == '‾'))

            {
                BoolState stateStart = boolmeta[0] == '_' ? BoolState.True : BoolState.False;
                BoolState stateEnd = boolmeta[1] == '_' ? BoolState.True : BoolState.False;

                string[] tokens = boolmeta.Split('#');
                tokens[0] = tokens[0].Replace("_'", "").Replace("_", "");
                IBoolObservedTime observed = null;
                if (tokens.Length == 1)
                {

                    observed = TryToCatchObservedTime(tokens[0]);
                }
                else if (tokens.Length == 2)
                {
                    observed = TryToCatchObservedTime(tokens[0], tokens[1]);
                }
                else observed = new BL_TimeToObserve();
                return new BL_BooleanItemStartFinish(stateStart, stateEnd,boolName, observed);
            }
            return null;


        }

        private static BL_BooleanItem CreatePourcentObserved(string boolName, string boolmeta)
        {

            if (StartWith("%", ref boolmeta))
            {

                string[] tokens = boolmeta.Split('#');


                BoolState state = BoolState.True;
                if (tokens[0].IndexOf("‾") > -1) state = BoolState.False;
                else if (tokens[0].IndexOf("_") > -1) state = BoolState.True;

                ValueDualSide valueside = ValueDualSide.More;
                 if (tokens[0].IndexOf("⋗") > -1) valueside = ValueDualSide.More;
                else if (tokens[0].IndexOf("⋖") > -1) valueside = ValueDualSide.Less;
                tokens[0] = tokens[0].Replace("⋖", "").Replace("⋗", "").Replace("<", "").Replace(">", "").Replace("_", "").Replace("‾", "").Replace("%", "");

                int pctCount = 0;
                int.TryParse(tokens[0], out pctCount);
                PourcentValue value = new PourcentValue(pctCount);

                IBoolObservedTime observed = null;
                if (tokens.Length == 2)
                {

                    observed = TryToCatchObservedTime(tokens[1]);
                }
                else if (tokens.Length == 3)
                {
                    observed = TryToCatchObservedTime(tokens[1], tokens[2]);
                }
                else observed = new BL_TimeToObserve();


                return new BL_BooleanItemPourcentStateInRange(boolName, state, value, valueside, observed);
            }
            return null;
        }

        private static BL_BooleanItem CreateTimeCount(string boolName, string boolmeta)
        {
          
            if (StartWith("∑", ref boolmeta) )
            {
               
                string[] tokens = boolmeta.Split('#');

                BoolState state = BoolState.True;
                if (tokens[0].IndexOf("‾") > -1) state = BoolState.False;
                else if (tokens[0].IndexOf("_") > -1) state = BoolState.True;

                ValueDualSide valueside = ValueDualSide.Less;
                if (tokens[0].IndexOf('⋗') > -1) valueside = ValueDualSide.More;
                if (tokens[0].IndexOf('⋖') > -1) valueside = ValueDualSide.Less;
                tokens[0] = tokens[0].Replace("⋗", "").Replace("⋖", "").Replace("∑", "");

                ITimeValue timeCount;
                ConvertStringToRelativeTimeValue(tokens[0], out timeCount);

                if (timeCount == null)
                    return null;

                IBoolObservedTime timeObserved = null;

                if (tokens.Length == 1 && valueside== ValueDualSide.Less)
                {
                    timeObserved = new BL_TimeToObserve(true, new BL_RelativeTimeFromNow( timeCount));
                    return new BL_BooleanItemTimeCountInRange(boolName, timeCount, valueside, timeObserved, state);
                }

                if (tokens.Length == 2)
                {

                    timeObserved = TryToCatchObservedTime(tokens[1]);
                }
                else if (tokens.Length == 3)
                {
                    timeObserved = TryToCatchObservedTime(tokens[1], tokens[2]);
                }


                return new BL_BooleanItemTimeCountInRange(boolName, timeCount, valueside, timeObserved, state);
            }
            return null;
        }

        private static BL_BooleanItem CreateBumpCount(string boolName, string boolmeta)
        {
          
            if (StartWith("⊓", ref boolmeta) || StartWith("⊔", ref boolmeta))
            {
                //Console.WriteLine("->>-->" + boolmeta);
                AllBumpType bumpType = StartWith("⊔", ref boolmeta) ? AllBumpType.FalseBump : AllBumpType.TrueBump;

                string[] tokens = boolmeta.Split('#');

                ObservedBumpType obt = ObservedBumpType.Equal;
                if (tokens[0].IndexOf('⋗') > -1) obt = ObservedBumpType.MoreOrEqual;
                if (tokens[0].IndexOf('⋖') > -1) obt = ObservedBumpType.LessOrEqual;
                tokens[0] = tokens[0].Replace("⋗", "").Replace("⋖", "").Replace("<", "").Replace("-", "").Replace("⊔", "").Replace("⊓", "");
                int count = 0;                 
                int.TryParse(tokens[0], out count);


                IBoolObservedTime observed = null;

                if (tokens.Length == 1)
                {

                    observed = TryToCatchObservedTime(tokens[0]);
                    count = 1;
                }

                else if (tokens.Length == 2)
                {

                    observed = TryToCatchObservedTime(tokens[1]);
                }
                else if (tokens.Length == 3)
                {
                    observed = TryToCatchObservedTime(tokens[1], tokens[2]);
                }

                if (observed == null)
                    return null;

                return new BL_BooleanItemBumpsInRange(boolName, obt, bumpType, count, observed);
            }
            return null;
        }

        private static Regex morseformat = new Regex("[^\\.-]");
        private static BL_BooleanItem CreateBumpMorse(string boolName, string boolmeta)
        {

            //if (StartWith("⊓", ref boolmeta) || StartWith("⊔", ref boolmeta))
            //{
            //    AllBumpType bumpType = StartWith("⊔", ref boolmeta) ? AllBumpType.FalseBump : AllBumpType.TrueBump;

            //    string[] tokens = boolmeta.Split('#');
            //    tokens[0] = morseformat.Replace(tokens[0], "");
            //    if (tokens[0].Length == 0)
            //        return null ;
                

            //    IBoolObservedTime observed = null;
            //    if (tokens.Length == 2)
            //    {

            //        observed = TryToCatchObservedTime(tokens[1]);
            //    }
            //    else if (tokens.Length == 3)
            //    {
            //        observed = TryToCatchObservedTime(tokens[1], tokens[2]);
            //    }

            //    throw new NotImplementedException();
            //   // return new BL_BooleanItemMorse(boolName, BL_BooleanItemMorse.Convert(tokens[0],'.','-','_'), observed);
            //}
            return null;

        }

        private static BL_BooleanItem CreateSwitchAndStay(string boolName, string boolmeta)
        {

            //else if (boolmeta[0] == '⤒' || boolmeta[0] == '⤓')
            //{
            //    bItem = new BL_BooleanItemSwitchBetween(boolName, SwitchTrackedType.SwitchAndStayActive, timeObserved, checktype == '⤓');
            //    managed = true;
            //}
            if (StartWith("↳", ref boolmeta) || StartWith("↱", ref boolmeta))
            {
                bool value = StartWith("↳", ref boolmeta);

                string[] tokens = boolmeta.Split('#');
                RemoveStart(ref tokens[0], "↳", "↱");

                IBoolObservedTime observed = null;
                if (tokens.Length == 1)
                {
                    observed = TryToCatchObservedTime(tokens[0]);
                }
                else if (tokens.Length == 2)
                {
                    observed = TryToCatchObservedTime(tokens[0], tokens[1]);
                }

                if (observed == null)
                    return new BL_BooleanItemIsTrueOrFalse(boolName, value);

                return new BL_BooleanItemSwitchBetween(boolName, SwitchTrackedType.SwitchAndStayActive, observed, value);
            }
            return null;
        }
        public static BL_BooleanItem CreateSwitchRecently(string boolName, string boolmeta)
        {
            if (StartWith("↓", ref boolmeta) || StartWith("↑", ref boolmeta))
            {
                bool value = StartWith("↓", ref boolmeta);

                string[] tokens = boolmeta.Split('#');
                RemoveStart(ref tokens[0], "↓", "↑");

                IBoolObservedTime observed = null;
                if (tokens.Length == 1)
                {
                    observed = TryToCatchObservedTime(tokens[0]);
                }
                else if (tokens.Length == 2)
                {
                    observed = TryToCatchObservedTime(tokens[0], tokens[1]);
                }
                if (observed == null)
                    return new BL_BooleanItemIsTrueOrFalse(boolName, value);

                return new BL_BooleanItemSwitchBetween(boolName, SwitchTrackedType.SwitchRecently, observed, value);
            }
            return null;
        }

        public static  BL_BooleanItem CreateMaintainLogic(string boolName, string boolmeta)
        {
            if (StartWith("‾", ref boolmeta) || StartWith("_", ref boolmeta))
            {
                bool value = StartWith("_", ref boolmeta);

                string[] tokens = boolmeta.Split('#');
                RemoveStart(ref tokens[0], "‾", "_");

                IBoolObservedTime observed = null;
                if (tokens.Length == 1)
                {

                    observed = TryToCatchObservedTime(tokens[0]);
                }
                else if (tokens.Length == 2)
                {
                    observed = TryToCatchObservedTime(tokens[0], tokens[1]);

                }

                if (observed == null)
                    return new BL_BooleanItemIsTrueOrFalse(boolName, value);

                if (observed.IsTimeKey())
                    return new BL_BooleanItemIsTrueOrFalseAt(boolName,observed , value);

                if (observed.IsTimeRange())
                    return new BL_BooleanItemIsTrueOrFalseAt(boolName, observed, value);
            }
            return null;
        }

      

        public static BL_BooleanItem CreateExistLogic(string boolName, string boolmeta)
        {

            if ( StartWith("¿", ref boolmeta) || StartWith("?", ref boolmeta))
            {
                BoolExistanceState value = (StartWith("¿", ref boolmeta)) ? BoolExistanceState.DontExist: BoolExistanceState.Exist;

                string[] tokens = boolmeta.Split('#');
                RemoveStart(ref tokens[0], "¿", "?");

                IBoolObservedTime observed = null;
                if (tokens.Length == 1)
                {

                    observed = TryToCatchObservedTime(tokens[0]);
                }
                else if (tokens.Length == 2)
                {
                    observed = TryToCatchObservedTime(tokens[0], tokens[1]);

                }

                if (observed == null)
                    return new BL_BooleanItemExist(boolName, value);

                if (observed.IsTimeKey())
                    return new BL_BooleanItemExistAt(boolName,  value, observed);

                if (observed.IsTimeRange())
                    return new BL_BooleanItemExistAt(boolName, value , observed);
            }
            return null;

        }

        private static BL_BooleanItem CreateExistLogic(string boolName, bool checkExistance)
        {
            return new BL_BooleanItemExist(boolName, checkExistance ? BoolExistanceState.Exist : BoolExistanceState.DontExist);
        }

        public static  void RemoveStart(ref string boolmeta, string toRemove)
        {
            if (boolmeta == null)
                return;
            if(boolmeta.IndexOf(toRemove)==0)
                boolmeta = boolmeta.Substring(toRemove.Length);
        }
        public static void RemoveStart(ref string boolmeta, params string [] toRemove)
        {
            if (boolmeta == null)
                return;
            for (int i = 0; i < toRemove.Length; i++)
            {
                if (boolmeta.IndexOf(toRemove[i]) == 0)
                    boolmeta = boolmeta.Substring(toRemove[i].Length);
            }
        }




        private static IBoolObservedTime TryToCatchObservedTime(string maybeTime1, string maybeTime2)
        {

            maybeTime1 = maybeTime1.ToLower();
            maybeTime2 = maybeTime2.ToLower();

            ITimeOfDay nt = null;
            ITimeOfDay ft = null;
            ConvertStringToTimeOfDay(maybeTime1, out nt);
            ConvertStringToTimeOfDay(maybeTime2, out ft);
            if (nt != null && ft != null)
            {
                return new BL_TimeToObserve(false, new BL_AbsoluteTimeDurationFromNow(nt, ft));
            }
            else if (nt != null)
            {
                return new BL_TimeToObserve(false, new BL_AbsoluteTimeFromNow(nt));
            }
            else if (ft != null)
            {
                return new BL_TimeToObserve(false, new BL_AbsoluteTimeFromNow(ft));
            }

            ITimeValue nlt = null;
            ITimeValue flt = null;
            ConvertStringToRelativeTimeValue(maybeTime1, out nlt);
            ConvertStringToRelativeTimeValue(maybeTime2, out flt);
            if (nlt != null && flt != null)
            {
                return new BL_TimeToObserve(false, new BL_RelativeTimeDurationFromNow(nlt, flt));
            }
            else if (nlt != null)
            {
                return new BL_TimeToObserve(false, new BL_RelativeTimeFromNow(nlt));
            }
            else if (flt != null)
            {
                return new BL_TimeToObserve(false, new BL_RelativeTimeFromNow(flt));
            }

            return null;
        }
        private static IBoolObservedTime TryToCatchObservedTime(string maybeTime1)
        {
            maybeTime1 = maybeTime1.ToLower();

            ITimeOfDay nt = null;
            ConvertStringToTimeOfDay(maybeTime1, out nt);
            if (nt != null)
            {
                return new BL_TimeToObserve(false, new BL_AbsoluteTimeFromNow(nt));
            }
           
            ITimeValue nlt = null;
            ConvertStringToRelativeTimeValue(maybeTime1, out nlt);
            if (nlt != null)
            {
                return new BL_TimeToObserve(false, new BL_RelativeTimeFromNow(nlt));
            }

            return null;
        }

        private static void ConvertStringToTimeOfDay(string timeAsString, out ITimeOfDay timeOfDay)
        {
            timeOfDay = null;
            if (timeAsString.IndexOf(":") > -1)
            {
                string[] tokens = timeAsString.Split(':');
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
                Match match = Regex.Match(timeAsString, "[0-9]+h");
                if (match.Success)
                {
                    h = match.Value.Replace("h", "");
                    left = "m";

                }
                else {

                    return;
                }

                match = Regex.Match(timeAsString, "[0-9]+m[^s]");
                if (match.Success)
                {
                    m = match.Value.Replace("m", "");
                    left = "s";
                }

                match = Regex.Match(timeAsString, "[0-9]+s");
                if (match.Success)
                {
                    s = match.Value.Replace("s", "");
                    left = "ms";
                }

                match = Regex.Match(timeAsString, "[0-9]+ms");
                if (match.Success)
                {
                    ms = match.Value.Replace("ms", "");
                    left = "ms";
                }
                match = Regex.Match(timeAsString, "[0-9]+$");
                if (match.Success)
                {
                    if (left == "m") m = match.Value;
                    if (left == "s") s = match.Value;
                    if (left == "ms") ms = match.Value;

                }

                timeOfDay = new BL_TimeOfDay(h, m, s, ms);
            }

        }

        private static void ConvertStringToRelativeTimeValue(string timeAsString , out ITimeValue timeResult)
        {

            timeResult = null;

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
                timeResult = new TimeInMsUnsignedInteger((uint)(time * (double)(mutiplicator)));
            }

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
            if (text == "1") return ttype = TokenType.ONE;
            if (text == "0") return ttype = TokenType.ZERO;
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
            if (text == "10") return ttype = TokenType.DominoLeftTrue;
            if (text == "01") return ttype = TokenType.DominoRightTrue;
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
        B_EQU,
        ONE,
        ZERO
    }
}