using BooleanRegisterUtilityAPI.BoolHistoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.Beans
{

    public class BooleanStateRegister
    {


        public BooleanRawRegister m_rawSaveAccess = new BooleanRawRegister();
        private Dictionary<string, BooleanNamedHistory> m_storedHistory = new Dictionary<string, BooleanNamedHistory>();
        private Dictionary<string, BooleanRawRegister.DirectAccess> m_quickAccesToBoolRef = new Dictionary<string, BooleanRawRegister.DirectAccess>();
       
        public BooleanNamedHistory[] m_valuesRef;
        public string[] m_keysRef;


        public void Set(string name, bool value)
        {
            name = name.ToLower().Trim();
            if (!Contains(name))
            {
                AddNewEntry(name, value);
            }
            else
            {
                ChangeExistingValue(name, value);
            }
        }

        private void AddNewEntry(string name, bool value)
        {
            m_rawSaveAccess.AddClaimAndScalUpIfNeeded(name, value);
            BooleanRawRegister.DirectAccess added =  m_rawSaveAccess.GetBooleanReference(name);
            m_quickAccesToBoolRef.Add(name, added);
            m_storedHistory.Add(name, new BooleanNamedHistory(name, value));
            m_keysRef = m_storedHistory.Keys.ToArray();
            m_valuesRef = m_storedHistory.Values.ToArray();
        }

        public void GetDirectionAccessToState(string name, out BooleanRawRegister.DirectAccess directAccess, out bool found)
        {
            if (m_rawSaveAccess.IsBooleanReferenceExist(name))
            {
                directAccess = m_rawSaveAccess.GetBooleanReference(name);
                found = true;
            }
            else {
                directAccess = null;
                found = false;

            }
        }

        
        public void GetDirectAccessRegister(ref BooleanRawRegister register )
        {
            register = m_rawSaveAccess;
        }

        private void ChangeExistingValue(string name, bool value)
        {
            m_rawSaveAccess.SetValue(name, value);
            m_storedHistory[name].SetValue(value);
        }

        public bool Contains(string name)
        {
            name = name.ToLower().Trim();
            return m_storedHistory.ContainsKey(name);
        }

        public void SwitchValue(IEnumerable<string> booleanNames)
        {
                foreach (var item in booleanNames)
                {
                    SwitchValue(item);
                }
            

        }

        public bool GetValueOf(string name)
        {
            name = name.ToLower();
            return m_storedHistory[name].GetValue();
        }
        public BooleanNamedHistory GetStateOf(string name)
        {
            name = name.ToLower();
            return m_storedHistory[name];
        }

        public void GetAll(out BooleanIndexGroup group)
        {
            group = new BooleanIndexGroup(GetAllKeys());
        }

        public string[] GetAllKeys() { return m_keysRef.ToArray(); }
        public BooleanNamedHistory[] GetAllState() { return m_valuesRef.ToArray(); }

        public void AddElapsedTimeToAll(float timePast)
        {
            foreach (var item in GetAllState())
            {
                item.GetHistory().AddElapsedTime(timePast);
            }
        }

        public void GetAllState(out List<BooleanNamedHistory> stateRef)
        {
            stateRef = m_valuesRef.ToList();
        }
        public void GetAllKeys(out List<string> stateRef)
        {
            stateRef = m_keysRef.ToList();
        }
        public void GetAllStateRef(ref BooleanNamedHistory [] stateRef)
        {
            stateRef = m_valuesRef;
        }
        public void GetAllKeysRef(ref string [] stateRef)
        {
            stateRef = m_keysRef;
        }


        public void SwitchValue(string booleanName)
        {
            bool has = Contains(booleanName);
            if (has)
            {
                bool value = GetValueOf(booleanName);
                Set(booleanName, !value);
            }
        }

        #region Listeners
        private BooleanChange m_newIndexName;
        public void AddNewIndexListener(BooleanChange changeListener)
        {
            m_newIndexName += changeListener;

        }
        public void RemoveNewIndexListener(BooleanChange changeListener)
        {
            m_newIndexName -= changeListener;

        }
        private void NotifyNewIndexChange(string booleanName, bool newValue)
        {
            if (m_newIndexName != null)
                m_newIndexName(booleanName, newValue);
        }

        private BooleanChange m_changeListeners;
        public delegate void BooleanChange(string booleanName, bool newValue);
        public void AddChangeListener(BooleanChange changeListener)
        {
            m_changeListeners += changeListener;

        }
        public void RemoveChangeListener(BooleanChange changeListener)
        {
            m_changeListeners -= changeListener;

        }
        private void NotifyBooleanChange(string booleanName, bool newValue)
        {
            if (m_changeListeners != null)
                m_changeListeners(booleanName, newValue);
        }
        #endregion
    }

}
