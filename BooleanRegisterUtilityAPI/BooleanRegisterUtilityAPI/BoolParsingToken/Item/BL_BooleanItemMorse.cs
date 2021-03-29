using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    class BL_BooleanItemMorse
    {
        public List<BLMorse> m_morseSequence = new List<BLMorse>();
        private string boolName;
        private object v;
        private IBoolObservedTime observed;

        public BL_BooleanItemMorse(string boolName, List<BLMorse> morseSequence, IBoolObservedTime observed)
        {
            this.boolName = boolName;
            this.m_morseSequence = morseSequence;
            this.observed = observed;
        }

        public static List<BLMorse> Convert(string sequence, char dot='.', char dash='-', char space='_')
        {
            List<BLMorse> l = new List<BLMorse>();
            for (int i = 0; i < sequence.Length; i++)
            {
                if (sequence[i] == dot)
                    l.Add(BLMorse.Dot);
                if (sequence[i] == dash)
                    l.Add(BLMorse.Dash);
                if (sequence[i] == space)
                    l.Add(BLMorse.Space);

            }
            return l;

        }
    }

    public enum BLMorse { Dot,Dash,Space}
}
