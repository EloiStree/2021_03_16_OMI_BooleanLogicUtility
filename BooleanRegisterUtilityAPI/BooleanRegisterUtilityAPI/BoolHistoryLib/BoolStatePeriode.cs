using BooleanRegisterUtilityAPI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BoolHistoryLib

{
    public class BoolStatePeriode
    {
         BooleanChangeType m_state;
         float m_elapsedTime;

        public BoolStatePeriode(bool state, float elapsedTime = 0)
        {
            m_state = GetChangeType( state);
            m_elapsedTime = elapsedTime;
        }

        public void SetStateTo(bool value) { m_state = GetChangeType(value); }
        public void SetStateTo(BooleanChangeType value) { m_state = value; }
        public bool GetState() { return m_state == BooleanChangeType.SetTrue; }
        public float GetElpasedTimeAsSecond() { return m_elapsedTime; }
        public void AddSomeElapsedTime(float time) { m_elapsedTime += time; }
        public void SetElapsedTime(float time) { m_elapsedTime = time; }

        public BooleanChangeType GetChangeType()
        {
            return m_state ;
        }
        public BooleanChangeType GetChangeType(bool value)
        {
            return value ? BooleanChangeType.SetTrue : BooleanChangeType.SetFalse;
        }
        public  bool GetChangeType(BooleanChangeType value)
        {
            return value == BooleanChangeType.SetTrue ;
        }
    }
}
