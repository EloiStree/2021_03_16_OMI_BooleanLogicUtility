using System;
using System.Collections;
using System.Collections.Generic;

namespace BooleanRegisterUtilityAPI.Actions
{

    [System.Serializable]
public class CallFunctionAction //: BooleanStateAction
{
     string m_functionName;
     string[] m_arguments;

    public string GetFunctionName()
    {
        return m_functionName;
    }

    public CallFunctionAction(string functionName, string[] arguments)
    {
        m_functionName = functionName;
        m_arguments = arguments;
    }

    public bool HasArguments()
    {
        return m_arguments.Length>0;
    }

    public string [] GetArguments()
    {
        return m_arguments;
    }

    public override string ToString()
    {
        return "[CALL|" + m_functionName + "|" + string.Join(" ", m_arguments) + "]";
    }
}
    }