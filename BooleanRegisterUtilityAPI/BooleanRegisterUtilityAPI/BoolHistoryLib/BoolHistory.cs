using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Enum;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BooleanRegisterUtilityAPI.BoolHistoryLib
{
    public class BoolHistory : IBooleanHistory
    {
        public BoolHistory(bool startState, int maxChangeSave = 50)
        {
            m_maxSave = maxChangeSave;
            Clear(startState);
        }
        List<BoolStatePeriode> m_switchesTracks = new List<BoolStatePeriode>();
        int m_maxSave = 50;

        public void Clear(bool startState) {
            m_switchesTracks.Clear();
            m_switchesTracks.Add(new BoolStatePeriode(startState));

        }
        public BoolStatePeriode GetInProgressState()
        {
            return m_switchesTracks[0];
        }

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
                toUse.SetElapsedTime(0);
                toUse.SetStateTo(state);
            }
            else
            {
                toUse = new BoolStatePeriode(state);
            }
            m_switchesTracks.Insert(0, toUse);
        }
        public void AddElapsedTime(float time)
        {
            GetInProgressState().AddSomeElapsedTime(time);
        }

        public bool CanBeScaleDownInSize(int value) { return m_switchesTracks.Count < value; }
        public void SetMaxSaveTo(int value)
        {
            if (!CanBeScaleDownInSize(value))
                throw new NotImplementedException("Did  not code the ability to scale down the size of a boolhistory. Please manage the scale by recovering clearing and adding what you condiser must be restored or not.");
            m_maxSave = value;
        }


        public void GetTruncatedHistoryCopy(ITimeValue nearestFromNow, ITimeValue farestFromNow, out RelativeTruncatedBoolHistory troncatedHistory)
        {

            troncatedHistory = new RelativeTruncatedBoolHistory( this, nearestFromNow, farestFromNow);
            double start =  nearestFromNow.GetAsSeconds(), end = farestFromNow.GetAsSeconds();
            bool state;
            double startBed, endBed;

            throw new NotImplementedException();
        }





        public void GetPeriodeAt(ITimeValue relativeTime, ref BoolStatePeriode state) {

            double t = relativeTime.GetAsSeconds();
            double start=0, end=0;
            int i = 0;
            while ( ! (t >= start && t < end)) {
                if (i == 0) { 
                
                }
                i++;
            }


        }

        public bool HasChange(ITimeValue nearestFromNow, ITimeValue farestFromNow, out bool current, out int trueCount, out int falseCount)
        {
            double start = nearestFromNow.GetAsSeconds(), end = farestFromNow.GetAsSeconds();
            bool state;
            double startBed, endBed;
            double bedTime;
            for (int i = 0; i < m_switchesTracks.Count; i++)
            {
                if (i == 0) {
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

        public static void DateTimeToTimeValue(DateTime now, DateTime time, out ITimeValue elipsedValue)
        {

            elipsedValue = new TimeInMsLong((long)(Math.Abs((now - time).TotalMilliseconds)));
        }
        public static void DateTimeToTimeValueFromNow( DateTime time, out ITimeValue elipsedValue)
        {
             DateTimeToTimeValue(DateTime.Now, time, out elipsedValue);
        }

      


        public bool HasChange(ITimeValue nearestFromNow, ITimeValue farestFromNow, out int trueCount, out int falseCount)
        {
            if (nearestFromNow.GetAsMilliSeconds() > farestFromNow.GetAsMilliSeconds()) { 
            
            }
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



        public void GetFromNowToPast(out BoolStatePeriode[] history, bool countingCurrent)
        {
            BoolStatePeriode[] result;
            GetFromPastToNow(out result, countingCurrent);
            Array.Reverse(result);
            history = result;
        }
        public void GetFromPastToNow(out BoolStatePeriode[] history, bool countingCurrent)
        {
            List<BoolStatePeriode> l = new List<BoolStatePeriode>();
            l.AddRange(m_switchesTracks.ToArray());
            if (countingCurrent)
                l.Add(m_inProgress);
            history = l.ToArray();
        }

        public bool GetState()
        {
            return m_inProgress.GetState();
        }

        public void GetFromPastToNow(out TimedBooleanChange[] history, DateTime from)
        {
            TimedBooleanChange[] tmp;
            GetFromNowToPast(out tmp, from);

            //history = tmp.OrderByDescending(k => k.GetTime()).ToArray();
            history = tmp.Reverse().ToArray();

        }
        public void GetFromNowToPast(out TimedBooleanChange[] history)
        {
            GetFromNowToPast(out history, DateTime.Now);
        }
        public bool WasSetTrue(float time, bool countCurrentState = true)
        {
            if (countCurrentState)
            {
                if (m_inProgress.GetElpasedTimeAsSecond() < time)
                {
                    if (m_inProgress.GetState())
                        return true;
                }
            }

            bool current;
            int trueCountSwitch, falseCountSwitch;
            HasChangeRecenlty(time, out current, out trueCountSwitch, out falseCountSwitch);
            return trueCountSwitch > 0;
        }
        public bool WasSetFalse(float time, bool countCurrentState = true)
        {
            if (countCurrentState)
            {
                if (m_inProgress.GetElpasedTimeAsSecond() < time)
                {
                    if (!m_inProgress.GetState())
                        return true;
                }
            }

            bool current;
            int trueCountSwitch, falseCountSwitch;
            HasChangeRecenlty(time, out current, out trueCountSwitch, out falseCountSwitch);
            return falseCountSwitch > 0;
        }
        public void GetFromNowToPast(out TimedBooleanChange[] history, DateTime fromNowValue)
        {
            BoolStatePeriode[] periodeChange;
            GetFromNowToPast(out periodeChange, true);

            history = new TimedBooleanChange[periodeChange.Length];
            DateTime timeIndex = fromNowValue;
            for (int i = 0; i < periodeChange.Length; i++)
            {
                BooleanChangeType changeType = periodeChange[i].GetChangeType();
                timeIndex = timeIndex.AddSeconds(-periodeChange[i].GetElpasedTimeAsSecond());
                history[i] = new TimedBooleanChange(changeType, timeIndex);
            }
        }

        public bool HasHistory()
        {
            return m_switchesTracks.Count > 0;
        }
            //NOT TESTED YET
        public void WasSwitchToRecently(bool observed, out bool result, out bool succedToAnwser, ITimeValue timeObservedInMs)
        {
            if (observed && ! GetState())
            {
                result = false;
                succedToAnwser = true;
            }
            else if (! observed && GetState())
            {
                result = false;
                succedToAnwser = true;
            }
            else
            {
                bool current;
                int t, f;
                HasChangeRecenlty((float)timeObservedInMs.GetAsSeconds(), out current, out t, out f);
                if(observed)
                    result = current==true && t > 0;
                else
                    result = current==false && f > 0;

                succedToAnwser = true;

            }
        }

        public void WasSwitchToTrueRecently(out bool result, out bool succedToAnwser, ITimeValue timeObservedInMs)
        {
            WasSwitchToRecently(true, out result, out succedToAnwser,  timeObservedInMs);
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

        public void GetBooleanableState(out bool value, out bool wasBooleanable)
        {
            wasBooleanable = true;
            value = GetState();
        }
    }

    



   


   
}