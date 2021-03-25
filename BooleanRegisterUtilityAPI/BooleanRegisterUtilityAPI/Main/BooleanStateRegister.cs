using BooleanRegisterUtilityAPI.BooleanLogic;
using BooleanRegisterUtilityAPI.BoolHistoryLib;
using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static BooleanRegisterUtilityAPI.BoolHistoryLib.BooleanRawRegister;

namespace BooleanRegisterUtilityAPI.Beans
{

    public class BooleanStateRegister : IBooleanStorage
    {


        public BooleanRawRegister m_rawSaveAccess = new BooleanRawRegister();
        private Dictionary<string, BooleanNamedHistory> m_storedHistory = new Dictionary<string, BooleanNamedHistory>();

       
        private Dictionary<string, BooleanRawRegister.DirectAccess> m_quickAccesToBoolRef = new Dictionary<string, BooleanRawRegister.DirectAccess>();

        public BooleanNamedHistory[] m_valuesRef = new BooleanNamedHistory[0];
        public string[] m_keysRef= new string[0];

        public BooleanStateRegisterTimeTracker m_defaultTimeTracker;

        public BooleanStateRegister()
        {
            m_defaultTimeTracker =  new BooleanStateRegisterTimeTracker(this);
        }

        public BooleanStateRegisterTimeTracker GetDefaultTimer() {
            return m_defaultTimeTracker;
        }
        public void AddTimePastFromDefaultTimer() {
            m_defaultTimeTracker.AddTimePast();
        }

        #region DESCRIPTION
        /// <summary>
        /// WARNING THIS FUNCTION IS "PERFORMENCE HEAVY"
        /// </summary>
        /// <param name="maxLineLenght"></param>
        /// <returns></returns>
        public string GetFullHistoryDebugText(int maxLineLenght = 50)
        {
            BooleanStateRegister r = this;
            string result;
            BooleanTextDebugUtility.GetTextDescriptionOfRegister(
               ref r,
               out result, maxLineLenght);
            return result;
        }
       
        public string GetCurveDebugText(string boolName, int maxLineLenght = 50, float timeInSecond=5)
        {
            BooleanNamedHistory target = GetStateOf(boolName);
            if (target == null)
                return boolName + ": Not registered";

            string d = BoolHistoryDescription.GetDescriptionNowToPast(target.GetHistory(), timeInSecond);
            return string.Format("{0}: {1}\n", target.GetName(), d);
        }
        public string GetNumericalDebugText(string boolName, int maxLineLenght = 50)
        {
            BooleanNamedHistory target = GetStateOf(boolName);
            if (target == null)
                return boolName + ": Not registered";

            string d = BoolHistoryDescription.GetNumericDescriptionNowToPast(target.GetHistory());
            return string.Format("{0}: {1}\n", target.GetName(), StringClamp(d, maxLineLenght));
        }
        public static string StringClamp(string text, int count)
        {
            if (text.Length < count)
                return text;
            return text.Substring(0, count);
        }
        #endregion

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

        public void QuickSetAllFalse( params string[] names)
        {
            QuickSetAll(false, names);
        }
        public void QuickSetAllTrue( params string[] names)
        {
            QuickSetAll(true, names);
        }
        public void QuickSetAll(bool value, params string [] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                Set(names[i], value);
            }
        }

        public void SetGroup( string[] names,  bool[] booleanValue)
        {
            if (names.Length != booleanValue.Length)
                throw new Exception("In set group, both array must have the same length");
            for (int i = 0; i < names.Length; i++)
            {
                this.Set(names[i], booleanValue[i]);
            }
        }
        /// <summary>
        /// up = set up to true, !up = set up to false
        /// </summary>
        /// <param name="names"></param>
        /// <param name="booleanValue"></param>
        public void QuickSetGroup(params string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                if ( !string.IsNullOrEmpty(names[i]) && names[i].Length>0) {
                    if (names[i][0] == '!')
                        Set(names[i].Substring(1), false);
                    if (names[i][0] != '!')
                        Set(names[i].Substring(0), true);
                }
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

        public bool GetValue(string name)
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
        public BooleanNamedHistory[] GetAllState() { 
            return m_valuesRef.ToArray();
        
        }
        public void AddSecondsElapsedTimeToAll(float timePastInSecond)
        {
            AddSecondsElapsedTimeToAll((double)timePastInSecond);
        }
        public void AddSecondsElapsedTimeToAll(double timePastInSecond)
        {
            foreach (var item in GetAllState())
            {
                item.GetHistory().AddMilliSecondElapsedTime((long)(timePastInSecond*1000.0));
            }
        }
        public void AddMilliSecondsElapsedTimeToAll(long totalMilliseconds)
        {
            foreach (var item in GetAllState())
            {
                item.GetHistory().AddMilliSecondElapsedTime(totalMilliseconds );
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
                bool value = GetValue(booleanName);
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



        public void Add(string name, bool defaultValue)
        {
            AddNewEntry(name, default);
        }

        public uint GetBooleanCount()
        {
            return (uint)m_keysRef.Length;
        }

        public void GetValue(string name, out bool value, out bool succedToReach)
        {
            if (Contains(name))
            {
                value = GetStateOf(name).GetValue();
                succedToReach = true;
            }
            else {
                value = succedToReach = false;
            }
        }

        public void GetFastAccess(string name, out IBooleanableRef value, out bool succedToReach)
        {
            DirectAccess access;
            GetDirectionAccessToState(name, out access, out succedToReach);
            value = access;
        }

        public void GetFastAccess(string name, out INamedBooleanableRef value, out bool succedToReach)
        {
            DirectAccess access;
            GetDirectionAccessToState(name, out access, out succedToReach);
            value = access;
        }

        public void GetHistoryAccess(string name, out IBooleanHistory value, out bool succedToReach)
        {
            if (Contains(name))
            {
                value = GetStateOf(name);
                succedToReach = true;
            }
            else
            {
                value = null;
                  succedToReach = false;
            }
        }

        public void GetHistoryAccess(string name, out INamedBooleanHistory value, out bool succedToReach)
        {
            if (Contains(name))
            {
                value = GetStateOf(name);
                succedToReach = true;
            }
            else
            {
                value = null;
                succedToReach = false;
            }
        }

        public void GetNamesRegistered(out IEnumerable<string> names)
        {
            names =  m_keysRef.AsEnumerable();
        }

        public void GetArrayCurrentState(out IArrayStateOfBooleanRegister currentStateRef)
        {
            currentStateRef = m_rawSaveAccess;
        }
        #endregion
    }

}
