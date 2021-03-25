using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BoolParsingToken.Item
{
    public class BL_BooleanItemDefault : BL_BooleanItem
    {
        public BL_BooleanItemDefault(string boolNamedId) : base(boolNamedId)
        {
        }
        public override string ToString()
        {
            return string.Format(" [B{0}] ",  GetTargetName());
        }
    }
}
