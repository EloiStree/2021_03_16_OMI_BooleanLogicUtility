using BooleanRegisterUtilityAPI.BooleanLogic.BooleanRef;
using BooleanRegisterUtilityAPI.BooleanLogic.Custom;
using BooleanRegisterUtilityAPI.BooleanLogic.Single;
using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.BoolHistoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.BooleanLogic.BoolDebug
{
    public class BooleanableDebug
    {
        public static string StringView(BooleanableOR value)
        {
            return string.Format("OR:{0}=>{1}", StringViewInterface(value.m_items), StringViewInterface(value));
        }
        public static string StringView(BooleanableNOR value)
        {
            return string.Format("NOR:{0}=>{1}", StringViewInterface(value.m_items), StringViewInterface(value));
        }
        public static string StringView(BooleanableAND value)
        {
            return string.Format("AND:{0}=>{1}", StringViewInterface(value.m_items), StringViewInterface(value));
        }
        public static string StringView(BooleanableNAND value)
        {
            return string.Format("NAND:{0}=>{1}", StringViewInterface(value.m_items), StringViewInterface(value));
        }
        public static string StringView(BooleanableXOR value)
        {
            return string.Format("XOR:{0} - {1} => {2}", StringViewInterface(value.m_leftBoolean)
                , StringViewInterface(value.m_rightBoolean), StringViewInterface(value));
        }
        public static string StringView(BooleanableNXOR value)
        {
            return string.Format("NXOR:{0} - {1} => {2}", StringViewInterface(value.m_leftBoolean)
                , StringViewInterface(value.m_rightBoolean), StringViewInterface(value));
        }
        public static string StringView(BooleanableRawRegisterTarget value)
        {
            if (value == null || !value.IsValide())
                return string.Format("RAWREG:invalide");

            return string.Format("RAWREG:{0} {1} => {2}",
           
                value.GetIndexNameInRegister(), value.GetIndexInRegister(), StringViewInterface(value));
        }
        public static string StringView(BooleanableIsMaintaining value)
        {
            return string.Format("b:{0}|{1}=>{2}",
                value.GetMaintainType(), 
                value.GetTimeObservedInSecond(), 
                StringViewInterface(value));
        }
        public static string StringView(BooleanableBoolHistoryMaintaining value)
        {
            return string.Format("b{0}|{1}|{2}{3}=>{4}",
                value.GetMaintainType(),
                value.GetTimeObservedInSecond(),
                value.GetLinkedHistory().GetInProgressState().GetState(),
                value.GetLinkedHistory().GetInProgressState().GetElpasedTimeAsSecond(),
                StringViewInterface(value));
        }
        public static string StringView(BooleanableSwitchRecently value)
        {
            return string.Format("b{0}|{1}=>{2}",
                value.GetSwitchType(),
                value.GetTimeObservedInSecond(),
                StringViewInterface(value));
        }
        public static string StringView(BooleanableBoolHistoryChangeListener value)
        {
            return string.Format("b{0}|{1}|{2}{3}=>{4}",
                value.GetSwitchType(),
                value.GetTimeObservedInSecond(),
                value.GetLinkedHistory().GetInProgressState().GetChangeType(),
                value.GetLinkedHistory().GetInProgressState().GetElpasedTimeAsSecond(),
                StringViewInterface(value)); 
        }

        public static string StringViewInterface(IBooleanableRef value)
        {
            if (value == null)
                return "null";
            bool v, e;
            value.GetBooleanableState(out v, out e);
            return string.Format("{0}{1}", v?'1':'0', e ? "" : "e");
        }
        public static string StringViewInterface(IEnumerable<IBooleanableRef> value, char spliter=' ')
        {
            StringBuilder b = new StringBuilder();
            foreach (IBooleanableRef item in value)
            {
                b.Append(spliter);
                b.Append(StringViewInterface(item));
            }
            b.Append(spliter);
            return b.ToString();
        }
    }
}
