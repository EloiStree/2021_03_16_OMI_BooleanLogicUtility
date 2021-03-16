using BooleanRegisterUtilityAPI.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.Delegate
{
    //#if true
#if UNITY_EDITOR

    [System.Serializable]
    public class TextEmittedEvent : UnityEvent<EmitTextAction> { }
    [System.Serializable]
    public class CallFunctionEvent : UnityEvent<CallFunctionAction> { }
    [System.Serializable]
    public class SetBooleanEvent : UnityEvent<SetBooleanStateAction> { }
#endif

    public delegate void TextEmittedListener(EmitTextAction obj);
    public delegate void CallFunctionListener(CallFunctionAction obj);
    public delegate void SetBooleanListener(SetBooleanStateAction obj);
}
