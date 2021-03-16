using BooleanRegisterUtilityAPI.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BooleanRegisterUtilityAPI.BoolHistoryLib
{
    public class BoolHistory
    {
        public BoolHistory(bool startState, int maxChangeSave = 50)
        {
            m_inProgress = new BoolStatePeriode(startState);
            m_maxSave = maxChangeSave;
        }
        BoolStatePeriode m_inProgress = new BoolStatePeriode(false);
        Queue<BoolStatePeriode> m_previousSave = new Queue<BoolStatePeriode>();
        int m_maxSave = 50;

        public BoolStatePeriode GetInProgressState()
        {
            return m_inProgress;
        }

        public void SetState(bool state)
        {
            if (m_inProgress.GetState() == state) return;
            BoolStatePeriode toUse;
            if (m_previousSave.Count >= m_maxSave)
            {
                toUse = m_previousSave.Dequeue();
                toUse.SetElapsedTime(0);
                toUse.SetStateTo(state);
            }
            else
            {
                toUse = new BoolStatePeriode(state);
            }
            m_previousSave.Enqueue(m_inProgress);
            m_inProgress = toUse;
        }
        public void AddElapsedTime(float time)
        {
            m_inProgress.AddSomeElapsedTime(time);
        }

        public void SetMaxSaveTo(int value)
        {
            m_maxSave = value;
        }

        public bool HasChangeRecenlty(float time, out bool current, out int trueCount, out int falseCount)
        {
            float timeCount = m_inProgress.GetElpasedTimeAsSecond();

            current = m_inProgress.GetState();
            trueCount = 0;
            falseCount = 0;
            if (timeCount >= time) { return false; }

            BoolStatePeriode[] history;
            GetFromNowToPast(out history, false);
            int index = 0;
            while (timeCount < time && index < history.Length)
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
            l.AddRange(m_previousSave.ToArray());
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
            return m_previousSave.Count > 0;
        }
    }

    



   


   
}