using System.Collections;
using System.Collections.Generic;

namespace BooleanRegisterUtilityAPI.Actions
{
    public class Modificator
    {
    }

    public class BetweenTime : Modificator
    {
         protected float m_minTimeInSecond;
         protected float m_maxTimeInSecond;

        public BetweenTime(float minTimeInSecond, float maxtimeInSecond)
        {
            m_minTimeInSecond = minTimeInSecond;
            m_maxTimeInSecond = maxtimeInSecond;
        }
    }
    public class BetweenTimeValue
    {
        BetweenTime m_info;
        public BetweenTimeValue(BetweenTime info)
        {
            m_info = info;
        }
         float m_time;
        public void Reset()
        {
            m_time = 0;
        }
        public void AddTime(float time)
        {
            m_time = time;
        }
        public float GetTime() { return m_time; }
    }

    public class Cooldown : Modificator
    {
         float m_timeInSecond;

        public Cooldown(float maxTimeInSecond)
        {
            m_timeInSecond = maxTimeInSecond;
        }
    }
    public class CooldownValue
    {
        Cooldown m_info;

        public CooldownValue(Cooldown info)
        {
            m_info = info;
        }
         float m_time;
        public void Reset()
        {
            m_time = 0;
        }
        public void AddTime(float time)
        {
            m_time = time;
        }
        public float GetTime() { return m_time; }
    }
    public class MaxTime : BetweenTime
    {

        public MaxTime(float maxTimeInSecond) : base(0, maxTimeInSecond)
        { }
    }
    public class ChangeBoolTimeObserved : Modificator
    {
         float m_timeInSecond;

        public ChangeBoolTimeObserved(float timeInSecond)
        {
            m_timeInSecond = timeInSecond;
        }
    }
}