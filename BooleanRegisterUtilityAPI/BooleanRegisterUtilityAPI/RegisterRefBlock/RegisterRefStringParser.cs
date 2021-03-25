using BooleanRegisterUtilityAPI.BoolParsingToken;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.RegisterRefBlock
{
    public class RegisterRefStringParser
    {
        public static LogicBlock TryToParse(RefBooleanRegister registerRef, string text)
        {
            bool p;
            LogicBlock i;
            TryToParse(registerRef, text, out p, out i);
            return i;
        }
            public static void TryToParse(RefBooleanRegister registerRef, string text,out bool parsed, out LogicBlock item) {
             BL_BooleanItem bi= BLTokensToBLBuilder.TryToParse(text);
            parsed = false;
            item = null;
            
             if (bi.GetType() == typeof(BL_BooleanItemIsTrueOrFalse))
            {
                BL_BooleanItemIsTrueOrFalse tmp = (BL_BooleanItemIsTrueOrFalse)bi;
                item = new RegisterRefStateBlock(
                    registerRef, tmp); 
                parsed = true;
            }
            else if (bi.GetType() == typeof(BL_BooleanItemIsTrueOrFalseAt))
            {
                BL_BooleanItemIsTrueOrFalseAt tmp = (BL_BooleanItemIsTrueOrFalseAt)bi;

                item = new RegisterRefStateAtBlock(
                    registerRef,tmp);
                parsed = true;
            }
            else if (bi.GetType() == typeof(BL_BooleanItemStartFinish))
            {

                item = new RegisterRefStartFinishInRange(
                    registerRef, (BL_BooleanItemStartFinish)bi);
                parsed = true;
            }
            else if (bi.GetType() == typeof(BL_BooleanItemSwitchBetween))
            {
                item = new RegisterRefSwitchBetweenBlock(
                    registerRef, (BL_BooleanItemSwitchBetween)bi);
                parsed = true;

            }
            else if (bi.GetType() == typeof(BL_BooleanItemTimeCountInRange))
            {
                item = new RegisterRefTimecountInRange(
                    registerRef, (BL_BooleanItemTimeCountInRange)bi);
                parsed = true;

            }
            else if (bi.GetType() == typeof(BL_BooleanItemPourcentStateInRange))
            {
                item = new RegisterRefPourcentStateInRange(
                    registerRef, (BL_BooleanItemPourcentStateInRange)bi);
                parsed = true;

            }
            else if (bi.GetType() == typeof(BL_BooleanItemMaintaining))
            {

                item = new RegisterRefMaintainingBlock(
                    registerRef, (BL_BooleanItemMaintaining)bi);
                parsed = true;
            }
            else if (bi.GetType() == typeof(BL_BooleanItemBumpsInRange))
            {

                item = new RegisterRefBumpsInRange(
                    registerRef, (BL_BooleanItemBumpsInRange)bi);
                parsed = true;
            }
           
            else if (bi.GetType() == typeof(BL_BooleanItemDefault))
            {
                BL_BooleanItemDefault tmp = (BL_BooleanItemDefault)bi;
                item = new RegisterRefStateTrueBlock(
                 registerRef, tmp);
                parsed = true;

            }
            else if (bi.GetType() == typeof(BL_BooleanItemExist))
            {

                item = new RegisterRefBoolExistBlock(
                    registerRef, (BL_BooleanItemExist)bi);
                parsed = true;
            }

        }

    }
}
