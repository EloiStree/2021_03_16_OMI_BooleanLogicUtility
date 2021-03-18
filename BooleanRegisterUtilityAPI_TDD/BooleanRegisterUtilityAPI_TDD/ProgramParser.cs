using BooleanRegisterUtilityAPI_TDD.BoolParsingToken;
using BooleanRegisterUtilityAPI_TDD.BoolParsingToken.Item.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD
{
    class ProgramParser
    {
        static void Main(string[] args)
        {
            BL_BuilderElements builder;
            TextLineSpliteAsBooleanLogicTokens tokener;

            ParserListToExperiemntTDD tdd = new ParserListToExperiemntTDD();
            foreach (string item in tdd.conditions)
            {
                Parse(item, out tokener);
                BLTokensToBLBuilder test = new BLTokensToBLBuilder(tokener, out builder);
                Console.WriteLine(builder);

            }

            string[] quicktest = new string[] {
                "[OR ¬[AND up ∨ left left] [XOR up ¬[XEQ ¬ left ¬up]]]",
                " ⊗ 🀲 🀸 ⌃ ⌄",
                "!shift? + (left || right)"
            };
            foreach (string item in quicktest)
            {
                Parse(item, out tokener);
                BLTokensToBLBuilder test = new BLTokensToBLBuilder(tokener, out builder);
                Console.WriteLine(builder);

            }


            ConsoleKey key;
            Console.WriteLine("Yo");
            Console.WriteLine("");
            string givenText;
            do
            {

                Console.WriteLine("Give condition to Parse ?");
                givenText = Console.ReadLine();
                Parse(givenText, out tokener);

            } while (givenText.Length > 0);


        
        }

        private static void DisplayAsDebugLinesInConsole(TextLineSpliteAsBooleanLogicTokens tokener)
        {
            uint level=0;
            foreach (string item in tokener.GetTokens())
            {
                String v = item.Trim();
                if (v.Length > 0 && (v[0] == '(' ||  v[0] == '['))
                {
                    DisplayLine(level, item);
                    level++;
                }
                else if (v.Length > 0 && (v[0] == ')' ||  v[0] == ']'))
                {
                    level--;
                    DisplayLine(level, item);
                }
                else
                {
                    if (v.Length > 0 && v[0] == 'I')
                    {
                        level++;
                        DisplayLine(level, item, ref tokener);
                        level--;
                    }
                    else if (v.Length > 0 && v[0] == '¬')
                    {
                        DisplayLine(level, item);
                    }
                    else {
                        DisplayLine(level, item);
                    }
                }
            }
        }



      
        private static void DisplayLine(uint level, string text)
        {
            TextLineSpliteAsBooleanLogicTokens t = null;
            Console.WriteLine(">" + GetTab(level) + "|" + text + "|\t\t" + BLTokensToBLBuilder. GetTokenType(text));
        }

        private static void DisplayLine(uint level, string text, ref TextLineSpliteAsBooleanLogicTokens tokener)
        {
            string value = "";
            if (text.StartsWith("I"))
            {
                value = tokener.m_items.GetFromIndex(uint.Parse(text.Substring(1)));
            }
            Console.WriteLine(">" + GetTab(level)+ "|" + text + "| " + value + "\t\t" + BLTokensToBLBuilder.GetTokenType(text));
        }

    

        public static string GetTab(uint level) {
            string s = "";
            for (int i = 0; i < level; i++)
            {
                s += "  ";

            }
            return s;
        }

        private static void Parse(string givenText, out TextLineSpliteAsBooleanLogicTokens tokener)
        {
            Console.WriteLine("\n\n\n" );
            Console.WriteLine(">>>Text: " + givenText);
            tokener = new TextLineSpliteAsBooleanLogicTokens(givenText, true);

            //Console.WriteLine("\n>>>Tokens");
            //foreach (string item in tokener.GetTokens())
            //{
            //    Console.WriteLine(">" + item);

            //}
            //Console.WriteLine("\n\n\n");


            DisplayAsDebugLinesInConsole(tokener);
        }
    }
}
