using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BoolStatePeriode
{
    public BooleanChangeType m_state;
    public uint m_elapsedTimeMs;

    public BoolStatePeriode(bool state, uint elapsedTimeMs = 0)
    {
        m_state = GetChangeType(state);
        m_elapsedTimeMs = elapsedTimeMs;
    }

    public void SetStateTo(bool value) { m_state = GetChangeType(value); }
    public void SetStateTo(BooleanChangeType value) { m_state = value; }
    public bool GetState() { return m_state == BooleanChangeType.SetTrue; }
    public double GetElpasedTimeAsSecond() { return ((double)m_elapsedTimeMs)/1000.0; }
    public uint GetElpasedTimeAsLongMs() { return m_elapsedTimeMs; }

    public void AddSomeElapsedTime(uint timeInMs)
    {
        m_elapsedTimeMs += timeInMs;
    }
    public void AddSomeElapsedTime(ITimeValue time) {
        uint t;
        time.GetAsMilliSeconds(out t);
        m_elapsedTimeMs += t;
    }
    public void SetElapsedTime(ITimeValue time) { 
        time.GetAsMilliSeconds( out m_elapsedTimeMs); 
    }

    public BooleanChangeType GetChangeType()
    {
        return m_state;
    }
    public BooleanChangeType GetChangeType(bool value)
    {
        return value ? BooleanChangeType.SetTrue : BooleanChangeType.SetFalse;
    }
    public bool GetChangeType(BooleanChangeType value)
    {
        return value == BooleanChangeType.SetTrue;
    }

    public void ResetTimeToZero()
    {
        m_elapsedTimeMs = 0;
    }
}

    public enum BooleanChangeType { SetTrue, SetFalse}