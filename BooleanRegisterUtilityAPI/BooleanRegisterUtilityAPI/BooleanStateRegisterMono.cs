using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BoolHistoryLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace BooleanRegisterUtilityAPI
{

    public class BooleanStateRegisterMono //: MonoBehaviour
    {
        public BooleanStateRegister m_register = new BooleanStateRegister();
        public enum TimeUpdateType { Update, Thread }
        public TimeUpdateType m_timeUpdate = TimeUpdateType.Thread;
        public BooleanStateRegister GetRegister() { return m_register; }

        public void GetRegister(ref BooleanStateRegister registerRef)
        {
            registerRef = m_register;
        }

        public DateTime m_lastCheck;
        public DateTime m_now;
        private Thread m_thread;
        public void Update()
        {
            if (!(m_timeUpdate == TimeUpdateType.Update))
                return;
            UpdateTime();
        }

        private void UpdateTime()
        {
            m_now = DateTime.Now;
            float timePast = (float)(m_now - m_lastCheck).TotalSeconds;
            m_register.AddElapsedTimeToAll(timePast);

            m_lastCheck = m_now;
        }

        public void Switch(List<string> booleanNames)
        {
            m_register.SwitchValue(booleanNames);
        }

        public void Awake()
        {
            m_thread = new Thread(UpdateTimeThread);
            m_thread.Start();
        }

        private void UpdateTimeThread()
        {
            while (true)
            {
                Thread.Sleep(10);
                UpdateTime();
            }
        }

        public void Set(string nameofboolean, bool value, bool createItIfNotFound)
        {
            if (!m_register.Contains(nameofboolean))
            {
                if (createItIfNotFound)
                    m_register.Set(nameofboolean, value);
            }
            else { m_register.Set(nameofboolean, value); }
        }
        public bool Contain(string nameofboolean)
        {
            return m_register.Contains(nameofboolean);
        }
        public void Get(string nameofboolean, out bool containt, out bool value)
        {
            containt = Contain(nameofboolean);
            if (containt)
                value = m_register.GetStateOf(nameofboolean).GetValue();
            else
                value = false; ;
        }

        public void OnDestroy()
        {
            if (m_thread != null)
                m_thread.Abort();
        }

        public void Set(bool value, List<string> names)
        {
            foreach (string item in names)
            {
                Set(item, value, true);
            }
        }
    }
}