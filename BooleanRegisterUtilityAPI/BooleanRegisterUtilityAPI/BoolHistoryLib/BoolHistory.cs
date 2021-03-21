
using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoolHistory
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

    public long GetTimeOverwatch()
    {
        long t = 0;
        for (int i = 0; i < m_switchesTracks.Count; i++)
        {
            t += m_switchesTracks[i].GetElpasedTimeAsLong();

        }
        return t;
    }
    public BoolStatePeriode GetInProgressState()
    {
        return m_switchesTracks[0];
    }
    public void AddElapsedTime(ITimeValue time)
    {
        long ms;
        time.GetAsMilliSeconds(out ms);
        GetInProgressState().AddSomeElapsedTime(ms);
    }
    public void AddElapsedTime(long timeInMs)
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

        elipsedValue = new TimeInMsLong((long)(Math.Abs((now - time).TotalMilliseconds)));
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
        long currentPeriodeMs = 0;
        long time = 0;
        relativeTime.GetAsMilliSeconds(out time);

        long start = 0, end = 0;

        int i = 0;
        bool isInRange;
        do
        {
            currentPeriodeMs = m_switchesTracks[i].GetElpasedTimeAsLong();

            start = end;
            end += currentPeriodeMs;
            state = m_switchesTracks[i];


            isInRange = (time >= start && time < end);
            i++;

        } while (!isInRange && i < m_switchesTracks.Count);
    }
    //Truncate an part of the history
    public void GetTruncatedHistoryCopy(ITimeValue nearestFromNow, ITimeValue farestFromNow, out RelativeTruncatedBoolHistory troncatedHistory)
    {
        troncatedHistory = new RelativeTruncatedBoolHistory(this, nearestFromNow, farestFromNow);

        BoolStatePeriode current;
        List<BoolStatePeriode> l = new List<BoolStatePeriode>();

        long start = 0, end = 0;


        long timeNearest = 0;
        long timeFarest = 0;

        nearestFromNow.GetAsMilliSeconds(out timeNearest);
        farestFromNow.GetAsMilliSeconds(out timeFarest);

        long currentPeriodeMs = 0;

        int i = 0;
        bool isInStartRange;
        bool isInEndRange;
        do
        {
            currentPeriodeMs = m_switchesTracks[i].GetElpasedTimeAsLong();

            start = end;
            end += currentPeriodeMs;
            current = m_switchesTracks[i];


            isInStartRange = (timeNearest >= start && timeNearest < end);
            isInEndRange = (timeFarest >= start && timeFarest < end);
            //if (isInStartRange)
            //{

            //    // NOT CODED troncate the time
            //    troncatedHistory.AddInLaterBack(end - timeNearest, current.GetState());
            //}
            //else if (isInEndRange)
            //{
            //    troncatedHistory.AddInLaterBack(current);
            //}
            //else 
            if (end >= timeNearest && end < timeFarest)
            {

                if (troncatedHistory.GetSwitchCount() == 0)
                    troncatedHistory.AddInLaterBack(end - timeNearest, current.GetState());
                else troncatedHistory.AddInLaterBack(current);

            }
            i++;

            //if (timeAFound && timeBFound)
            //    break;

        } while (i < m_switchesTracks.Count);

        if (troncatedHistory.IsEmpty())
        {
            BoolStatePeriode p;
            GetPeriodeAt(nearestFromNow, out p);
            troncatedHistory.SetValueIfEmpty(p.GetState());
        }
    }




    /*
    public bool HasChange(ITimeValue nearestFromNow, ITimeValue farestFromNow, out bool current, out int trueCount, out int falseCount)
        {
            double start = nearestFromNow.GetAsSeconds(), end = farestFromNow.GetAsSeconds();
            bool state;
            double startBed, endBed;
            double bedTime;
            for (int i = 0; i < m_switchesTracks.Count; i++)
            {
                if (i == 0)
                {
                    bedTime = m_switchesTracks[i].GetElpasedTimeAsSecond();
                    startBed = 0;
                    endBed = bedTime;
                }

            }

            current = m_inProgress.GetState();
            trueCount = 0;
            falseCount = 0;
            if (timeInSecondStart >= timeInSecond) { return false; }

            BoolStatePeriode[] history;
            GetFromNowToPast(out history, false);
            int index = 0;
            while (timeInSecondStart < timeInSecond && index < history.Length)
            {
                if (history[index].GetState())
                    trueCount++;
                else
                    falseCount++;

                timeInSecondStart += history[index].GetElpasedTimeAsSecond();
                index++;
            }

            return trueCount > 0 || falseCount > 0;
        }


        public bool HasChangeRecenlty(float timeInSecond, out bool current, out int trueCount, out int falseCount)
        {
            float timeCount = m_inProgress.GetElpasedTimeAsSecond();

            current = m_inProgress.GetState();
            trueCount = 0;
            falseCount = 0;
            if (timeCount >= timeInSecond) { return false; }

            BoolStatePeriode[] history;
            GetFromNowToPast(out history, false);
            int index = 0;
            while (timeCount < timeInSecond && index < history.Length)
            {
                if (history[index].GetState())
                    trueCount++;
                else
                    falseCount++;

                timeCount += history[index].GetElpasedTimeAsSecond();
                index++;
            }

            return trueCount > 0 || falseCount > 0;
        }

   */







    public bool HasChange(ITimeValue nearestFromNow, ITimeValue farestFromNow, out int trueCount, out int falseCount)
    {
        RelativeTruncatedBoolHistory history;
        GetTruncatedHistoryCopy(nearestFromNow, farestFromNow, out history);
        history.GetSwitchCount(out trueCount, out falseCount);
        return trueCount > 0 || falseCount > 0;
    }
    public void WasSwitchToRecently(bool observed, out bool result, out bool succedToAnwser, ITimeValue timeObservedInMs)
    {
        //if (observed && !GetState())
        //{
        //    result = false;
        //    succedToAnwser = true;
        //}
        //else if (!observed && GetState())
        //{
        //    result = false;
        //    succedToAnwser = true;
        //}
        //else
        //{
        //    bool current;
        //    int t, f;
        //    HasChangeRecenlty((float)timeObservedInMs.GetAsSeconds(), out current, out t, out f);
        //    if (observed)
        //        result = current == true && t > 0;
        //    else
        //        result = current == false && f > 0;

        //    succedToAnwser = true;

        //}
        throw new NotImplementedException();
    }




    public void WasSwitchToTrueRecently(out bool result, out bool succedToAnwser, ITimeValue timeObservedInMs)
    {
        WasSwitchToRecently(true, out result, out succedToAnwser, timeObservedInMs);
    }

    public void WasSwitchToFalseRecently(out bool result, out bool succedToAnwser, ITimeValue timeObservedInMs)
    {
        WasSwitchToRecently(false, out result, out succedToAnwser, timeObservedInMs);
    }

    public void WasSwitchToTrue(out bool result, out bool succedToAnwser, ITimeValue from, ITimeValue to)
    {
        throw new NotImplementedException();
    }

    public void WasSwitchToFalse(out bool result, out bool succedToAnwser, ITimeValue from, ITimeValue to)
    {
        throw new NotImplementedException();
    }

    public void WasSwitchToTrue(out bool result, out bool succedToAnwser, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public void WasSwitchToFalse(out bool result, out bool succedToAnwser, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public void WasMaintainedTrue(out bool result, out bool succedToAnwser, ITimeValue from, ITimeValue to)
    {
        throw new NotImplementedException();
    }

    public void WasMaintainedFalse(out bool result, out bool succedToAnwser, ITimeValue from, ITimeValue to)
    {
        throw new NotImplementedException();
    }

    public void WasMaintainedTrue(out bool result, out bool succedToAnwser, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public void WasMaintainedFalse(out bool result, out bool succedToAnwser, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public void GetSwitchCount(out ushort switch2True, out ushort switch2False, ITimeValue from, ITimeValue to)
    {
        throw new NotImplementedException();
    }

    public void GetSwitchCount(out ushort switch2True, out ushort switch2False, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public void GetState(out bool value, out bool succedToAnwser, ITimeValue when)
    {
        throw new NotImplementedException();
    }

    public void GetState(out bool value, out bool succedToAnwser, DateTime when)
    {
        throw new NotImplementedException();
    }



}







