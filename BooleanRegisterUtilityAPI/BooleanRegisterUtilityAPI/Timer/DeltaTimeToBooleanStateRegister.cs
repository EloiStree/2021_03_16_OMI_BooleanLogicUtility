using BooleanRegisterUtilityAPI.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.Timer
{
    public class DeltaTimeToBooleanStateRegister
    {

        public List<BooleanStateRegister> m_registers = new List<BooleanStateRegister>();

        public DeltaTimeToBooleanStateRegister(IEnumerable<BooleanStateRegister> registers)
        {

            now = previous = DateTime.Now;
            Add(registers);
        }
        public DeltaTimeToBooleanStateRegister(params BooleanStateRegister[] registers)
        {
            now = previous = DateTime.Now;
            Add(registers);
        }

        private void Add(IEnumerable<BooleanStateRegister> registers)
        {
            foreach (BooleanStateRegister item in registers)
            {
                if (!m_registers.Contains(item))
                    m_registers.Add(item);
            }
        }

        public DateTime now;
        public DateTime previous;
        private float timepast;


        public void ComputeAndPushPastTime() {

            now = DateTime.Now;
            timepast = (float)(now - previous).TotalSeconds;
            PushTimePast(timepast);
            previous = now;
        }

        public void PushTimePast(float timepastInSecond)
        {
            foreach (BooleanStateRegister item in m_registers)
            {
                item.AddSecondsElapsedTimeToAll(timepastInSecond);
            }
        }

       
        private Thread m_defaultThread=null;
        private uint m_timeThreadSleepInMs=0;
        public void CreateThread(ThreadPriority priority, uint minTimeInMs=1) {
            if (m_defaultThread != null) { 
                m_defaultThread.Abort();
                m_defaultThread = null;
            }

            m_defaultThread = new Thread(ComputeAndPushPastTimeLoop);
            m_defaultThread.Priority = priority;
            m_timeThreadSleepInMs = minTimeInMs;
            m_defaultThread.Start();
        }
         void ComputeAndPushPastTimeLoop() {
            while (m_defaultThread != null) {
                ComputeAndPushPastTime();
                Thread.Sleep((int)m_timeThreadSleepInMs);
            }
        }
    }
}
