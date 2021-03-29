using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RelativeTruncatedBoolHistory
{
    public BoolHistory m_origine;
    public ITimeValue m_nearestFromNow;
    public ITimeValue m_farestFromNow;
    public bool m_isInRecordedRange;
    public bool m_valueIfEmpty;

    public List<BoolStatePeriode> m_foundFromNowToPast = new List<BoolStatePeriode>();


    public RelativeTruncatedBoolHistory(BoolHistory origine, ITimeValue nearestFromNow, ITimeValue farestFromNow, bool isInRecordedRange)
    {
        m_origine = origine;
        m_nearestFromNow = nearestFromNow;
        m_farestFromNow = farestFromNow;
        m_isInRecordedRange = isInRecordedRange;
    }
    public void AddInNowFront(uint elipseTimeInMs, bool state)
    {
        m_foundFromNowToPast.Insert(0, new BoolStatePeriode(state, elipseTimeInMs));
    }
    public void AddInNowFront(BoolStatePeriode periode)
    {

        m_foundFromNowToPast.Insert(0, periode);
    }
    public void AddInLaterBack(uint elipseTimeInMs, bool state)
    {

        m_foundFromNowToPast.Add(new BoolStatePeriode(state, elipseTimeInMs));
    }
    public void AddInLaterBack(BoolStatePeriode periode)
    {

        m_foundFromNowToPast.Add(periode);
    }

    public bool IsEmpty() { return m_foundFromNowToPast.Count == 0; }
    public void SetValueIfEmpty(bool value) { m_valueIfEmpty = value; }

    public bool GetValueIfEmpty() { return m_valueIfEmpty; }

    public BoolStatePeriode GetFirst() { return m_foundFromNowToPast[0]; }
    public BoolStatePeriode GetLast() { return m_foundFromNowToPast[m_foundFromNowToPast.Count - 1]; }

    public void GetArray(out BoolStatePeriode[] value) { value = m_foundFromNowToPast.ToArray(); }
    public void GetList(out List<BoolStatePeriode> value) { value = m_foundFromNowToPast; }


    public bool GetStartState() {
        if (IsEmpty())
            return GetValueIfEmpty();
        return GetFirst().GetState(); }
    public bool GetEndState()
    {
        if (IsEmpty())
            return GetValueIfEmpty();
        return ! GetLast().GetState(); }

    public int GetSwitchCount()
    {
        return m_foundFromNowToPast.Count;
    }


    public bool WasSwitchAndStillIsTrue()
    {
        int t, f;
        GetSwitchCount(out t, out f);
        return GetStartState() && t > 0;
    }
    public bool WasSwitchAndStillIsFalse()
    {
        int t, f;
        GetSwitchCount(out t, out f);
        return !GetStartState() && f > 0;
    }

    public void GetSwitchCountAndState(out bool startState, out bool endState, out ushort trueCount, out ushort falseCount)
    {
        startState = GetStartState();
        endState = GetEndState();

        trueCount = falseCount = 0;
        if (IsEmpty())
        {
            return;
        }
        else
        {
            for (int i = 0; i < m_foundFromNowToPast.Count; i++)
            {
                if (m_foundFromNowToPast[i].GetChangeType() == BooleanChangeType.SetTrue)
                    trueCount++;
                if (m_foundFromNowToPast[i].GetChangeType() == BooleanChangeType.SetFalse)
                    falseCount++;
            }
        }
    }
    public bool IsInObserveRange() {
        return m_isInRecordedRange;
    }
    public void GetSwitchCount(out int trueCount, out int falseCount)
    {
        trueCount = falseCount = 0;
        if (IsEmpty()) { 
            return;
        }
        else {
            for (int i = 0; i < m_foundFromNowToPast.Count; i++)
            {
                if (m_foundFromNowToPast[i].GetChangeType() == BooleanChangeType.SetTrue)
                    trueCount++;
                if (m_foundFromNowToPast[i].GetChangeType() == BooleanChangeType.SetFalse)
                    falseCount++;
            }
        }
    }



    public bool IsMaintaining(bool maintainingType)
    {
        if (IsEmpty()) {
            if (maintainingType == true && GetValueIfEmpty()==true)
                return true;
            if (maintainingType == false && GetValueIfEmpty()==false)
                return true; 
            return false;
        }

        double pt, pf;
        GetTrueAndFalsePourcent(out pt, out pf);
        if (maintainingType==true && pt >=1.0)
            return true;
        if (maintainingType==false && pf >= 1.0)
            return true;
        return false;
    }
    public bool IsMaintaining()
    {
       return IsMaintaining(true);
    }
    public bool IsRealeasing()
    {
        return IsMaintaining(false);
    }


    public void GetPeriodeAt(ITimeValue relativeTime, out BoolStatePeriode state)
    {
        state = null;
        uint currentPeriodeMs = 0;
        uint time = 0;
        relativeTime.GetAsMilliSeconds(out time);

        uint start = 0, end = 0;

        int i = 0;
        bool isInRange;
        do
        {
            currentPeriodeMs = m_foundFromNowToPast[i].GetElpasedTimeAsLongMs();

            start = end;
            end += currentPeriodeMs;
            state = m_foundFromNowToPast[i];


            isInRange = (time >= start && time < end);
            i++;

        } while (!isInRange && i < m_foundFromNowToPast.Count);
    }

    public ITimeValue GetStartTime() { return m_nearestFromNow; }
    public ITimeValue GetEndTime() { return m_farestFromNow; }


    public void GetTrueAndFalsePourcent(out double pourcentTrue, out double pourcentFalse) {
        uint t, f, o;
        GetTrueAndFalseTimecount(out t, out f, out o);
        if (o == 0) { pourcentTrue = pourcentFalse = 0; return; 
        
        }
        pourcentTrue = t / (double) o;
        pourcentFalse = f / (double) o;
    }

    public void GetTrueAndFalseTimecount(out uint trueCountInMs , out uint falseCountInMs , out uint observedTime)
    {
        uint n, f;
        m_farestFromNow.GetAsMilliSeconds(out f);
        m_nearestFromNow.GetAsMilliSeconds(out n);
        observedTime = f - n;
        if (IsEmpty())
        { if (GetStartState())
            {
                trueCountInMs = observedTime;
                falseCountInMs = 0;
            }
            else {
                falseCountInMs  = observedTime;
                trueCountInMs = 0;
            }
            return;
        }
         trueCountInMs=0;
         falseCountInMs=0;

        uint currentPeriodeMs = 0;
        int i = 0;
        do
        {
            currentPeriodeMs = m_foundFromNowToPast[i].GetElpasedTimeAsLongMs();
            if (m_foundFromNowToPast[i].GetState())
                trueCountInMs += currentPeriodeMs;
           else
                falseCountInMs += currentPeriodeMs;
            i++;
        } while ( i < m_foundFromNowToPast.Count);

        uint leftover = observedTime - (trueCountInMs + falseCountInMs);
        if (GetEndState())
            trueCountInMs += leftover;
        else
            falseCountInMs += leftover;

    }


    public bool HasNoSwitch() {
        int t, f;
        GetSwitchCount(out t, out f);
        return t == 0 && f == 0;
    }

    public void GetBumps(out List<BinaryBump> bumpTrue, out List<BinaryBump> bumpFalse)
    {
        bumpTrue = new List<BinaryBump>();
        bumpFalse = new List<BinaryBump>();
        if (GetSwitchCount() < 2)
        {
            return;
        }

        bool isup = false;
        BoolStatePeriode current;
        for (int i = 0; i < m_foundFromNowToPast.Count - 1; i++)
        {
            current = m_foundFromNowToPast[i];
            if (m_foundFromNowToPast[i].GetChangeType() == BooleanChangeType.SetFalse &&
                m_foundFromNowToPast[i + 1].GetChangeType() == BooleanChangeType.SetTrue)
            {
                bumpTrue.Add(new BinaryBump(m_foundFromNowToPast[i + 1]));
            }
            if (m_foundFromNowToPast[i].GetChangeType() == BooleanChangeType.SetTrue &&
                   m_foundFromNowToPast[i + 1].GetChangeType() == BooleanChangeType.SetFalse)
            {
                bumpFalse.Add(new BinaryBump(m_foundFromNowToPast[i + 1]));
            }



        }

    }
    public void GetHoles(out List<BinaryHole> holeFalseGround, out List<BinaryHole> holeTrueGround)
    {
        holeFalseGround = new List<BinaryHole>();
        holeTrueGround = new List<BinaryHole>();
        if (GetSwitchCount() < 2)
        {
            return;
        }

        bool isup = false;
        BoolStatePeriode current;
        for (int i = 0; i < m_foundFromNowToPast.Count - 1; i++)
        {
            current = m_foundFromNowToPast[i];
            if (m_foundFromNowToPast[i].GetChangeType() == BooleanChangeType.SetFalse &&
                m_foundFromNowToPast[i + 1].GetChangeType() == BooleanChangeType.SetTrue)
            {
                holeFalseGround.Add(new BinaryHole(m_foundFromNowToPast[i + 1]));
            }
            if (m_foundFromNowToPast[i].GetChangeType() == BooleanChangeType.SetTrue &&
                   m_foundFromNowToPast[i + 1].GetChangeType() == BooleanChangeType.SetFalse)
            {
                holeTrueGround.Add(new BinaryHole(m_foundFromNowToPast[i + 1]));
            }



        }

    }

    public BoolHistory GetBooleanOrigine()
    {
        return m_origine;
    }


    public void GetBumpsCount(AllBumpType type, out uint count, ITimeValue from, ITimeValue to) {
        count = 0;
        switch (type)
        {
            case AllBumpType.FalseBump:
                GetBumpsCount(out count, from, to);
                break;
            case AllBumpType.FalseHole:
                GetHolesCount(out count, from, to);
                break;
            case AllBumpType.TrueBump:
                GetCeilingBumpsCount(out count, from, to);
                break;
            case AllBumpType.TrueHole:
                GetCeilingHolesCount(out count, from, to);
                break;
            default:
                break;
        }
    }
    public void GetBumpsCount( out uint bumbs, ITimeValue from, ITimeValue to)
    {
        bumbs = 0;
        if (GetSwitchCount() < 2)
        {
            return;
        }

        bool isup = false;
        BoolStatePeriode current;
        for (int i = 0; i < m_foundFromNowToPast.Count - 1; i++)
        {

            if (m_foundFromNowToPast[i].GetState() == false &&
                m_foundFromNowToPast[i + 1].GetState() == true)
            {
                bumbs++;
            }
            
        }
    }
    public void GetHolesCount( out uint holes, ITimeValue from, ITimeValue to)
    {
        holes = 0;
        if (GetSwitchCount() < 2)
        {
            return;
        }

        bool isup = false;
        BoolStatePeriode current;
        for (int i = 0; i < m_foundFromNowToPast.Count - 1; i++)
        {

           
            if (m_foundFromNowToPast[i].GetState() == true &&
                m_foundFromNowToPast[i + 1].GetState() == false)
            {
                holes++;
            }
        }
    }
    public void GetCeilingBumpsCount( out uint bumbs , ITimeValue from, ITimeValue to)
    {
        bumbs  = 0;
        if (GetSwitchCount() < 2)
        {
            return;
        }

        bool isup = false;
        BoolStatePeriode current;
        for (int i = 0; i < m_foundFromNowToPast.Count - 1; i++)
        {

            if (m_foundFromNowToPast[i].GetState() == true &&
                m_foundFromNowToPast[i + 1].GetState() == false)
            {
                bumbs++;
            }
        }
    }
    public void GetCeilingHolesCount( out uint holes, ITimeValue from, ITimeValue to)
    {
         holes = 0;
        if (GetSwitchCount() < 2)
        {
            return;
        }

        bool isup = false;
        BoolStatePeriode current;
        for (int i = 0; i < m_foundFromNowToPast.Count - 1; i++)
        {

            if (m_foundFromNowToPast[i].GetState() == false &&
                m_foundFromNowToPast[i + 1].GetState() == true)
            {
                holes++;
            }
            
        }
    }
}

public enum AllBumpType { FalseBump,FalseHole, TrueBump, TrueHole}

public enum BumpGroundType { GroundFalse, GroundTrue }
public enum HoleType { CeilingHole, CliffHole }

[System.Serializable]
public class BinaryBump
{
    public bool m_bumpState;
    public ITimeValue m_duration;

    public BinaryBump(BoolStatePeriode targetPeriode)
    {
        m_bumpState = targetPeriode.GetState() ;
        m_duration = new TimeInMsUnsignedInteger( targetPeriode.GetElpasedTimeAsLongMs() );
    }

    public bool GetBumpValue() { return m_bumpState; }
    public bool GetBumpGroundValue() { return !m_bumpState; }
    public BumpGroundType GetBumpGroundType() { return m_bumpState ? BumpGroundType.GroundFalse : BumpGroundType.GroundTrue; }
    public HoleType GetHoleType() { return m_bumpState ? HoleType.CeilingHole : HoleType.CliffHole; }
    public ITimeValue GetBumpDuration() { return m_duration; }
}

[System.Serializable]
public class BinaryHole
{
    public bool m_holeState;
    public ITimeValue m_duration;

    public BinaryHole(BoolStatePeriode targetPeriode)
    {
        m_holeState = targetPeriode.GetState();
        m_duration = new TimeInMsUnsignedInteger(targetPeriode.GetElpasedTimeAsLongMs());
    }

    public bool GetHoleValue() { return m_holeState; }
    public bool GetGroundValue() { return !m_holeState; }
    public HoleType GetHoleType() { return m_holeState ? HoleType.CeilingHole : HoleType.CliffHole; }
    public BumpGroundType GetBumpGroundType() { return m_holeState ? BumpGroundType.GroundTrue  : BumpGroundType.GroundFalse; }
    public ITimeValue GetBumpDuration() { return m_duration; }
}

//Should put code on that...
public class BinaryBumpWithOrigine {
    public RelativeTruncatedBoolHistory m_origine;
    public ITimeValue m_whenStartedFromNow;
    public ITimeValue m_whenStartedFromRange;

    public BoolHistory GetBooleanOrigine() { return m_origine.GetBooleanOrigine(); }
    public RelativeTruncatedBoolHistory GetTruncateOrigine() { return m_origine; }
}
