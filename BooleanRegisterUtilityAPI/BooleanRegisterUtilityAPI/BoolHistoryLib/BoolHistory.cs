
using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoolHistory : IBooleanHistory
{
    #region CORE
    #region SURE
    List<BoolStatePeriode> m_switchesTracks = new List<BoolStatePeriode>();
    int m_maxSave = 50;


    public BoolHistory(bool startState, int maxChangeSave = 50)
    {
        m_maxSave = maxChangeSave;
        Clear(startState);
    }
    public void Clear(bool startState)
    {
        m_switchesTracks.Clear();
        m_switchesTracks.Add(new BoolStatePeriode(startState));

    }

    public uint GetTimeOverwatch()
    {
        uint t = 0;
        for (int i = 0; i < m_switchesTracks.Count; i++)
        {
            t += m_switchesTracks[i].GetElpasedTimeAsLongMs();

        }
        return t;
    }
    public BoolStatePeriode GetInProgressState()
    {
        return m_switchesTracks[0];
    }
    public void AddElapsedTime(ITimeValue time)
    {
        uint ms;
        time.GetAsMilliSeconds(out ms);
        GetInProgressState().AddSomeElapsedTime(ms);
    }
    public void AddMilliSecondElapsedTime(uint timeInMs)
    {
        GetInProgressState().AddSomeElapsedTime(timeInMs);
    }
    public void GetFromNowToPast(out BoolStatePeriode[] history, bool countingCurrent)
    {
        List<BoolStatePeriode> l = new List<BoolStatePeriode>();
        l.AddRange(m_switchesTracks);
        if (countingCurrent)
            l.RemoveAt(0);
        history = l.ToArray();

    }
    public void GetFromPastToNow(out BoolStatePeriode[] history, bool countingCurrent)
    {
        BoolStatePeriode[] result;
        GetFromNowToPast(out result, countingCurrent);
        Array.Reverse(result);
        history = result;

    }
    public bool GetState()
    {
        return GetInProgressState().GetState();
    }
    public bool HasHistory()
    {
        return m_switchesTracks.Count > 1;
    }
    #endregion

    public void SetState(bool state)
    {
        if (GetInProgressState().GetState() == state)
            return;
        BoolStatePeriode toUse;
        if (m_switchesTracks.Count >= m_maxSave)
        {
            int i = m_switchesTracks.Count - 1;
            toUse = m_switchesTracks[i];
            m_switchesTracks.RemoveAt(i);
            toUse.ResetTimeToZero();
            toUse.SetStateTo(state);
        }
        else
        {
            toUse = new BoolStatePeriode(state);
        }
        m_switchesTracks.Insert(0, toUse);
    }

    #region TO BE TESTED
    public bool CanBeScaleDownInSize(int value) { return m_switchesTracks.Count < value; }
    public void SetMaxSaveTo(int value)
    {
        if (value < 2)
            value = 1;

        while (m_switchesTracks.Count > value)
        {
            m_switchesTracks.RemoveAt(m_switchesTracks.Count - 1);
        }
        m_maxSave = value;
    }


    public static void DateTimeToTimeValue(DateTime now, DateTime time, out ITimeValue elipsedValue)
    {

        elipsedValue = new TimeInMsUnsignedInteger((uint)(Math.Abs((now - time).TotalMilliseconds)));
    }
    public static void DateTimeToTimeValueFromNow(DateTime time, out ITimeValue elipsedValue)
    {
        DateTimeToTimeValue(DateTime.Now, time, out elipsedValue);
    }
    #endregion
    #endregion






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
            currentPeriodeMs = m_switchesTracks[i].GetElpasedTimeAsLongMs();

            start = end;
            end += currentPeriodeMs;
            state = m_switchesTracks[i];


            isInRange = (time >= start && time < end);
            i++;

        } while (!isInRange && i < m_switchesTracks.Count);
    }
    //Truncate an part of the history

    public void GetTruncatedHistoryCopy( ITimeValue whenFromNow, out RelativeTruncatedBoolHistory truncatedHistory)
    {
        GetTruncatedHistoryCopy(ZeroTime.Default, whenFromNow, out truncatedHistory);
    }
    
    public void GetTruncatedHistoryCopy(ITimeValue nearestFromNow, ITimeValue farestFromNow, out RelativeTruncatedBoolHistory troncatedHistory)
    {
        troncatedHistory = new RelativeTruncatedBoolHistory(this, nearestFromNow, farestFromNow, IsInRange(farestFromNow));

        BoolStatePeriode current;
        List<BoolStatePeriode> l = new List<BoolStatePeriode>();

        uint start = 0, end = 0;


        uint timeNearest = 0;
        uint timeFarest = 0;

        nearestFromNow.GetAsMilliSeconds(out timeNearest);
        farestFromNow.GetAsMilliSeconds(out timeFarest);

        uint currentPeriodeMs = 0;

        int i = 0;
        bool isInStartRange;
        bool isInEndRange;
        do
        {
            currentPeriodeMs = m_switchesTracks[i].GetElpasedTimeAsLongMs();

            start = end;
            end += currentPeriodeMs;
            current = m_switchesTracks[i];


            isInStartRange = (timeNearest >= start && timeNearest < end);
            isInEndRange = (timeFarest >= start && timeFarest < end);
          
            if (end >= timeNearest && end < timeFarest)
            {

                if (troncatedHistory.GetSwitchCount() == 0)
                    troncatedHistory.AddInLaterBack(end - timeNearest, current.GetState());
                else troncatedHistory.AddInLaterBack(current);

            }
            i++;


        } while (i < m_switchesTracks.Count);

        if (troncatedHistory.IsEmpty())
        {
            BoolStatePeriode p;
            GetPeriodeAt(nearestFromNow, out p);
            troncatedHistory.SetValueIfEmpty(p.GetState());
        }
    }

    public bool IsInRange(ITimeValue farestFromNow)
    {
        long maxTinMs = GetTimeOverwatch();
        uint farestTime;
        farestFromNow.GetAsMilliSeconds(out farestTime);
        return farestTime <= maxTinMs;
    }

    public bool IsInRange(DateTime when, DateTime time)
    {
        ITimeValue t;
        ConvertDateToTime(when, time, out t);
        return IsInRange(t);
    }

    private void ConvertDateToTime(DateTime when, DateTime time, out ITimeValue timeBetween)
    {
        timeBetween = new TimeInMsUnsignedInteger((uint)(Math.Abs((when - time).TotalMilliseconds)));
    }

    public bool HasChange(ITimeValue nearestFromNow, ITimeValue farestFromNow, out int trueCount, out int falseCount)
    {
        RelativeTruncatedBoolHistory truncate;
        GetTruncatedHistoryCopy(nearestFromNow, farestFromNow, out truncate);
        truncate.GetSwitchCount(out trueCount, out falseCount);
        return trueCount > 0 || falseCount > 0;
    }

    public void WasSwitchTo(bool stateObserved, out bool result, ITimeValue from, ITimeValue to)
    {
        bool state = GetState();
        int t=0, f=0;

        RelativeTruncatedBoolHistory truncate;
        GetTruncatedHistoryCopy(from,to, out truncate);
        truncate.GetSwitchCount(out t, out f);
        if (stateObserved ==true && state==true && t > 0)
            result = true;
        else if (stateObserved == false && state==false && f>0)
            result = true;
        else 
            result = false;
    }

    public void WasSwitchToTrue(out bool result, ITimeValue from, ITimeValue to)
    {
        WasSwitchTo(true,out  result, from, to);
    }

    public void WasSwitchToTrue(out bool result, DateTime now, DateTime from, DateTime to)
    {
        ITimeValue fromV, toV;
        ConvertDateToTime(now, from, to, out fromV, out toV);
        WasSwitchTo(true, out result, fromV, toV);
    }

    public void WasSwitchToTrue(out bool result, DateTime from, DateTime to)
    {
        WasSwitchToTrue(out result, DateTime.Now, from, to);
    }

   

    public void WasSwitchToFalse(out bool result, ITimeValue from, ITimeValue to)
    {
        WasSwitchTo(false, out result, from, to);
    }

    public void WasSwitchToFalse(out bool result, DateTime now, DateTime from, DateTime to)
    {
        ITimeValue fromV, toV;
        ConvertDateToTime(now, from, to, out fromV, out toV);
        WasSwitchTo(false, out result, fromV, toV);
    }
    public void ConvertDateToTime(DateTime now, DateTime from, DateTime to, out ITimeValue fromValue, out ITimeValue toValue) {

        fromValue = new TimeInMsUnsignedInteger((uint)(Math.Abs((now - from).TotalMilliseconds)));
        toValue = new TimeInMsUnsignedInteger((uint)(Math.Abs((now - to).TotalMilliseconds)));

    }
    public void WasSwitchToFalse(out bool result, DateTime from, DateTime to)
    {
        WasSwitchToFalse(out result, DateTime.Now, from, to);
    }

    public void WasMaintained(bool stateObserved, out bool result, ITimeValue from, ITimeValue to)
    {
        RelativeTruncatedBoolHistory truncate;
        GetTruncatedHistoryCopy(from, to, out truncate);
        
        result = truncate.IsMaintaining(stateObserved);
        
    }

    public void WasMaintainedTrue(out bool result, ITimeValue from, ITimeValue to)
    {
        WasMaintained(true, out result, from, to);
    }

    public void WasMaintainedTrue(out bool result, DateTime now, DateTime from, DateTime to)
    {
        ITimeValue fromV, toV;
        ConvertDateToTime(now, from, to, out fromV, out toV);
        WasMaintained(true, out result, fromV, toV);
    }

    public void WasMaintainedTrue(out bool result, DateTime from, DateTime to)
    {
        WasMaintainedTrue(out result, DateTime.Now, from, to);
    }

    public void WasMaintainedFalse(out bool result, ITimeValue from, ITimeValue to)
    {
        WasMaintained(false, out result, from, to);
    }

    public void WasMaintainedFalse(out bool result, DateTime now, DateTime from, DateTime to)
    {
        ITimeValue fromV, toV;
        ConvertDateToTime(now, from, to, out fromV, out toV);
        WasMaintained(false, out result, fromV, toV);
    }

    public void WasMaintainedFalse(out bool result, DateTime from, DateTime to)
    {
        WasMaintainedFalse(out result, DateTime.Now, from, to);
    }

    public void GetSwitchCount(out ushort switch2True, out ushort switch2False, ITimeValue from, ITimeValue to)
    {
        RelativeTruncatedBoolHistory truncate;
        GetTruncatedHistoryCopy(from, to, out truncate);
        bool start, end;
        truncate.GetSwitchCountAndState(out start, out end, out switch2True, out switch2False);
    }

    public void GetSwitchCount(out ushort switch2True, out ushort switch2False, DateTime now, DateTime from, DateTime to)
    {
        ITimeValue fromV, toV;
        ConvertDateToTime(now, from, to, out fromV, out toV);
        GetSwitchCount(out switch2True, out switch2False, fromV, toV);
    }

    public void GetSwitchCount(out ushort switch2True, out ushort switch2False, DateTime from, DateTime to)
    {
        GetSwitchCount(out switch2True, out switch2False, DateTime.Now, from, to);
    }


    public void GetPoucentOfState(bool observed, out double pourcent)
    {
        GetPoucentOfState(observed, out pourcent, new TimeInMsUnsignedShort(0), GetTimeOverwatchAsTimeValue());
    }
   

    private ITimeValue GetTimeOverwatchAsTimeValue()
    {
        return new TimeInMsUnsignedInteger(GetTimeOverwatch());
    }

    public void GetPoucentOfState(bool stateObserved, out double pourcentState, ITimeValue from, ITimeValue to)
    {
        RelativeTruncatedBoolHistory truncate;
        GetTruncatedHistoryCopy(from, to, out truncate);
        double tmp;
        truncate.GetTrueAndFalsePourcent(out pourcentState, out tmp);
    }

    public void GetPoucentOfState(bool stateObserved, out double pourcentState, DateTime now, DateTime from, DateTime to)
    {
        ITimeValue fromV, toV;
        ConvertDateToTime(now, from, to, out fromV, out toV);
        GetPoucentOfState(stateObserved, out pourcentState, fromV, toV);
    }

    public void GetPoucentOfState(bool stateObserved, out double pourcentState, DateTime from, DateTime to)
    {
        GetPoucentOfState(stateObserved, out pourcentState, DateTime.Now, from, to);
    }

    public void GetBumpsCount(AllBumpType bumb, out uint count, ITimeValue from, ITimeValue to)
    {
        RelativeTruncatedBoolHistory truncate;
        GetTruncatedHistoryCopy(from, to, out truncate);
        truncate.GetBumpsCount( bumb, out count, from, to);

    }

    public void GetBumpsCount(AllBumpType bumb, out uint count, DateTime now, DateTime from, DateTime to)
    {
        ITimeValue fromV, toV;
        ConvertDateToTime(now, from, to, out fromV, out toV);
        GetBumpsCount( bumb, out count, fromV, toV);
    }

    public void GetBumpsCount(AllBumpType bumb, out uint count, DateTime from, DateTime to)
    {
        GetBumpsCount( bumb, out count, DateTime.Now, from, to);
    }
    public void GetTimeCount(bool stateObserved, out uint timeCountInMs)
    {
        GetTimeCount(stateObserved, out timeCountInMs, new TimeInMsUnsignedShort(0), GetTimeOverwatchAsTimeValue());
    }
    public void GetTimeCount(bool stateObserved, out uint timeFound, ITimeValue from, ITimeValue to)
    {
        RelativeTruncatedBoolHistory truncate;
        GetTruncatedHistoryCopy(from, to, out truncate);
        uint t, f, o;
        truncate.GetTrueAndFalseTimecount(out t, out f ,out o) ;

        if (stateObserved)
            timeFound =  t;
        else
            timeFound =  f;

    }

    public void GetTimeCount(bool stateObserved, out uint timeFound, DateTime now, DateTime from, DateTime to)
    {
        ITimeValue fromV, toV;
        ConvertDateToTime(now, from, to, out fromV, out toV);
        GetTimeCount(stateObserved , out timeFound, fromV, toV);
    }

    public void GetTimeCount(bool stateObserved, out uint timeFound, DateTime from, DateTime to)
    {
        GetTimeCount(stateObserved, out timeFound, DateTime.Now, from, to);
    }

    public void GetState(out bool value, ITimeValue when)
    {
        BoolStatePeriode state;
        GetPeriodeAt(when, out state);
        value = state.GetState();

    }

    public void GetState(out bool value, DateTime now, DateTime when)
    {
        GetState(out value,  new TimeInMsUnsignedInteger((uint)((now - when).TotalMilliseconds)) );
    }

    public void GetState(out bool value, DateTime when)
    {
        GetState(out value, DateTime.Now, when);
    }

    public void GetBooleanableState(out bool value)
    {
        value= GetState();
    }

    public void GetBooleanableState(out bool value, out bool wasBooleanable)
    {
        value = GetState();
        wasBooleanable = true;
    }

    public void StartAndFinishState(bool stateStart, bool statEnd, out bool result, ITimeValue from, ITimeValue to)
    {
        RelativeTruncatedBoolHistory truncate;
        GetTruncatedHistoryCopy(from, to, out truncate);
        result = truncate.GetStartState() == stateStart && truncate.GetEndState() == statEnd;
    }

    public void StartAndFinishState(bool stateStart, bool statEnd, out bool result, DateTime now, DateTime from, DateTime to)
    {
        ITimeValue fromV, toV;
        ConvertDateToTime(now, from, to, out fromV, out toV);
        StartAndFinishState( stateStart,  statEnd, out result, fromV, toV);
    }

    public void StartAndFinishState(bool stateStart, bool statEnd, out bool result, DateTime from, DateTime to)
    {
        StartAndFinishState( stateStart,  statEnd, out result, DateTime.Now, from, to);
    }

    
}







