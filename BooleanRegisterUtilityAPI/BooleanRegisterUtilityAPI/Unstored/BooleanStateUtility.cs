
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BoolHistoryLib;
using BooleanRegisterUtilityAPI.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BooleanRegisterUtilityAPI
{

    public class BooleanStateUtility
    {

        public enum SortType { RecentToLatest, LatestToRecent }

        public static bool OR(BooleanStateRegister registre, BooleanIndexGroup group)
        {
            List<BooleanValueRef> booleanTracked = new List<BooleanValueRef>();
            foreach (var item in group.GetNames())
            {
                booleanTracked.Add(new BooleanValueRef(item));
            }
            return OR(registre, booleanTracked);
        }
        public static bool NOR(BooleanStateRegister registre, List<BooleanValueRef> booleanTracked, List<BooleanValueChangeRef> booleanChangeTracked)
        {
            return !OR(registre, booleanTracked, booleanChangeTracked);
        }

        public static bool AreBooleansRegistered(BooleanStateRegister register, List<BooleanValueRef> observedState, List<BooleanValueChangeRef> observedChange)
        {
            for (int i = 0; i < observedState.Count; i++)
            {
                if (!register.Contains(observedState[i].GetName()))
                    return false;

            }
            for (int i = 0; i < observedChange.Count; i++)
            {
                if (!register.Contains(observedChange[i].GetName()))
                    return false;

            }
            return true;
        }

        public static bool OR(BooleanStateRegister registre, List<BooleanValueRef> booleanTracked, List<BooleanValueChangeRef> booleanChangeTracked)
        {
            return OR(registre, booleanTracked) || OR(registre, booleanChangeTracked);
        }
        public static bool OR(BooleanStateRegister registre, List<BooleanValueRef> booleanTracked)
        {
            for (int i = 0; i < booleanTracked.Count; i++)
            {
                if (registre.Contains(booleanTracked[i].GetName()))
                {
                    bool isOn = registre.GetValue(booleanTracked[i].GetName());
                    if (booleanTracked[i].IsRequestingActive() && isOn)
                    {
                        return true;
                    }
                    else if (booleanTracked[i].IsRequestingInverse() && !isOn)
                    {
                        return true;
                    }
                }
                //else throw new Exception("Boolean value is not in the register: " + booleanTracked[i].GetName());
            }
            return false;
        }

        public static bool OR(BooleanStateRegister registre, List<BooleanValueChangeRef> booleanChangeTracked)
        {
            for (int i = 0; i < booleanChangeTracked.Count; i++)
            {
                if (registre.Contains(booleanChangeTracked[i].GetName()))
                {
                    float time = booleanChangeTracked[i].GetSecondToCheck();
                    BooleanNamedHistory isOnState = registre.GetStateOf(booleanChangeTracked[i].GetName());
                    if (booleanChangeTracked[i].IsRequestingActive())
                    {
                        throw new NotImplementedException();
                        //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetTrue
                        //    && isOnState.WasSetFalse(time, false))
                        //{
                        //    return true;
                        //}

                        //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetFalse
                        //   && isOnState.WasSetTrue(time, false))
                        //{
                        //    return true;
                        //}
                    }
                    else if (booleanChangeTracked[i].IsRequestingInverse())
                    {
                        throw new NotImplementedException();
                        //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetTrue
                        //     && !isOnState.WasSetFalse(time, false))
                        //{
                        //    return true;
                        //}
                        //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetFalse
                        //   && !isOnState.WasSetTrue(time, false))
                        //{
                        //    return true;
                        //}
                    }
                }
                // else throw new Exception("Boolean value is not in the register: " + booleanChangeTracked[i].GetName());
            }
            return false;
        }
        public static bool XOR(BooleanStateRegister register, List<BooleanValueRef> observedState, List<BooleanValueChangeRef> observedChange)
        {
            return XOR(register, observedState) ^ XOR(register, observedChange);
        }
        public static bool XOR(BooleanStateRegister register, List<BooleanValueRef> observedState)
        {
            if (observedState.Count > 0)
            {
                int found = 0;
                for (int i = 0; i < observedState.Count; i++)
                {
                    if (register.Contains(observedState[i].GetName()))
                    {
                        bool isOnState = register.GetValue(observedState[i].GetName());
                        if (observedState[i].IsRequestingActive() && isOnState)
                        {
                            found++;
                        }
                        else if (observedState[i].IsRequestingInverse() && !isOnState)
                        {
                            found++;
                        }
                        if (found > 1) return false;
                    }
                }
                return found == 1;
            }
            return false;
        }

        public static bool XOR(BooleanStateRegister register, List<BooleanValueChangeRef> booleanChangeTracked)
        {

            if (booleanChangeTracked.Count > 0)
            {
                int found = 0;
                for (int i = 0; i < booleanChangeTracked.Count; i++)
                {
                    if (register.Contains(booleanChangeTracked[i].GetName()))
                    {
                        float time = booleanChangeTracked[i].GetSecondToCheck();
                        BooleanNamedHistory isOnState = register.GetStateOf(booleanChangeTracked[i].GetName());
                        if (booleanChangeTracked[i].IsRequestingActive())
                        {
                            throw new NotImplementedException();
                            //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetTrue
                            //    && isOnState.WasSetFalse(time, false))
                            //{
                            //    found++;
                            //}

                            //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetFalse
                            //   && isOnState.WasSetTrue(time, false))
                            //{
                            //    found++;
                            //}
                        }
                        else if (booleanChangeTracked[i].IsRequestingInverse())
                        {
                            throw new NotImplementedException();
                            //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetTrue
                            //     && !isOnState.WasSetFalse(time, false))
                            //{
                            //    found++;
                            //}
                            //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetFalse
                            //   && !isOnState.WasSetTrue(time, false))
                            //{
                            //    found++;
                            //}
                        }
                        if (found > 1) return false;
                    }
                }
                return found == 1;
            }
            return false;
        }

        public static bool AND(BooleanStateRegister registre, BooleanIndexGroup group)
        {
            List<BooleanValueRef> booleanTracked = new List<BooleanValueRef>();
            foreach (var item in group.GetNames())
            {
                booleanTracked.Add(new BooleanValueRef(item));
            }
            return AND(registre, booleanTracked);
        }
        public static bool NAND(BooleanStateRegister registre, List<BooleanValueRef> booleanTracked, List<BooleanValueChangeRef> booleanChangeTracked)
        {
            return NAND(registre, booleanTracked, booleanChangeTracked);
        }
        public static bool AND(BooleanStateRegister registre, List<BooleanValueRef> booleanTracked, List<BooleanValueChangeRef> booleanChangeTracked)
        {
            return AND(registre, booleanTracked) && AND(registre, booleanChangeTracked);
        }

        public static bool AND(BooleanStateRegister registre, List<BooleanValueChangeRef> booleanChangeTracked)
        {
            for (int i = 0; i < booleanChangeTracked.Count; i++)
            {
                if (registre.Contains(booleanChangeTracked[i].GetName()))
                {
                    float time = booleanChangeTracked[i].GetSecondToCheck();
                    BooleanNamedHistory isOnState = registre.GetStateOf(booleanChangeTracked[i].GetName());
                    if (booleanChangeTracked[i].IsRequestingActive())
                    {
                        throw new NotImplementedException();
                        //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetTrue
                        //    && !isOnState.WasSetFalse(time, false))
                        //{
                        //    return false;
                        //}

                        //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetFalse
                        //   && !isOnState.WasSetTrue(time, false))
                        //{
                        //    return false;
                        //}
                    }
                    else if (booleanChangeTracked[i].IsRequestingInverse())
                    {
                        throw new NotImplementedException();
                        //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetTrue
                        //     && isOnState.WasSetFalse(time, false))
                        //{
                        //    return false;
                        //}
                        //if (booleanChangeTracked[i].GetChangeType() == BooleanChangeType.SetFalse
                        //   && isOnState.WasSetTrue(time, false))
                        //{
                        //    return false;
                        //}
                    }
                }
                // else throw new Exception("Boolean value is not in the register: " + booleanChangeTracked[i].GetName());
            }
            return true;
        }
        public static bool AND(BooleanStateRegister registre, List<BooleanValueRef> booleanTracked)
        {
            for (int i = 0; i < booleanTracked.Count; i++)
            {
                if (registre.Contains(booleanTracked[i].GetName()))
                {
                    bool isOn = registre.GetValue(booleanTracked[i].GetName());
                    if (isOn == true && booleanTracked[i].IsRequestingInverse())
                    {
                        return false;
                    }
                    else if (isOn == false && booleanTracked[i].IsRequestingActive())
                    {
                        return false;
                    }
                }
                //else throw new Exception("Boolean value is not in the register: " + booleanTracked[i].GetName());
            }
            return true;
        }





        public static bool XOR(BooleanStateRegister registre, BooleanIndexGroup group)
        {
            List<BooleanValueRef> booleanTracked = new List<BooleanValueRef>();
            foreach (var item in group.GetNames())
            {
                booleanTracked.Add(new BooleanValueRef(item));
            }
            return XOR(registre, booleanTracked);
        }
        public static bool NOR(BooleanStateRegister registre, BooleanIndexGroup group)
        {
            return !OR(registre, group);
        }
        public static bool NAND(BooleanStateRegister registre, BooleanIndexGroup group)
        {
            return !AND(registre, group);
        }

        public static BooleanIndexGroup Except(BooleanIndexGroup range, BooleanIndexGroup toRemove)
        {//http://dotnetpattern.com/linq-except-operator
            return new BooleanIndexGroup(range.GetNames().Except(toRemove.GetNames()).ToArray());
        }
        public static BooleanIndexGroup Union(BooleanIndexGroup g1, BooleanIndexGroup g2)
        {//http://dotnetpattern.com/linq-union-operator
            return new BooleanIndexGroup(g1.GetNames().Union(g2.GetNames()).ToArray());
        }
        public static BooleanIndexGroup CommonElements(BooleanIndexGroup g1, BooleanIndexGroup g2)
        {//http://dotnetpattern.com/linq-intersect-operator
            return new BooleanIndexGroup(g1.GetNames().Intersect(g2.GetNames()).ToArray());
        }

        public static bool HasChanged(BooleanStateRegister register, BooleanValueChangeRef booleanValueChange)
        {
            string name = booleanValueChange.GetName();
            if (!register.Contains(name))
                return false;
            BoolHistory h = register.GetStateOf(name).GetHistory();
            TimedBooleanChange[] changes;
            DateTime to = DateTime.Now;
            DateTime from = to.AddSeconds(-booleanValueChange.GetSecondToCheck());

            throw new NotImplementedException();
            //h.GetFromNowToPast(out changes, DateTime.Now);
            //TimedBooleanChange[] valideChanges =
            //    changes.Where(k => k.GetChange() == booleanValueChange.GetChangeType()
            //    && (k.GetTime() > from && k.GetTime() <= to)).ToArray();
            //return valideChanges.Length > 0;
        }

        public static BooleanIndexGroup RemoveDuplicate(BooleanIndexGroup range)
        {// http://dotnetpattern.com/linq-distinct-operator
            return new BooleanIndexGroup(range.GetNames().Distinct().ToArray());
        }
        public static bool Contains(BooleanIndexGroup range, string name)
        {// http://dotnetpattern.com/linq-distinct-operator
            return range.GetNames().Contains(name);
        }
        public static bool DontContains(BooleanIndexGroup range, string name)
        {// http://dotnetpattern.com/linq-distinct-operator
            return !Contains(range, name);
        }

        public static void SortRecentToLatest(List<NamedBooleanChangeTimed> boolChange)
        {
            SortLatestToRecent(boolChange);
            boolChange.Reverse();
        }
        public static void SortLatestToRecent(List<NamedBooleanChangeTimed> boolChange)
        {
            boolChange.Sort((x, y) => DateTime.Compare(x.GetWhenChanged().GetTime(), y.GetWhenChanged().GetTime()));

        }


        public static List<NamedBooleanChangeTimed> GetBoolChangedBetweenTimeRangeFromNow(BooleanStateRegister reg, BooleanIndexGroup group, double timeToCheck, SortType sortType = SortType.RecentToLatest)
        {
            DateTime now = DateTime.Now;
            return GetBoolChangedBetweenFromNow(reg, group, now.AddSeconds(-timeToCheck), now, now);
        }
        public static List<NamedBooleanChangeTimed> GetBoolChangedBetweenFromNow(BooleanStateRegister reg, BooleanIndexGroup group, DateTime from, DateTime to, DateTime now, SortType sortType = SortType.RecentToLatest)
        {
            List<NamedBooleanChangeTimed> result = new List<NamedBooleanChangeTimed>();
            if (group == null)
                reg.GetAll(out group);
            foreach (string item in group.GetNames())
            {
                TimedBooleanChange[] boolChanged;

                if (reg.Contains(item))
                {

                    BoolHistory select = reg.GetStateOf(item).GetHistory();
                    //if (select.HasHistory()) { 

                    throw new NotImplementedException();
                    //select.GetFromNowToPast(out boolChanged, now);
                    //boolChanged = boolChanged.Where(k => (k.GetTime() >= from) && (k.GetTime() <= to)).ToArray();
                    //for (int i = 0; i < boolChanged.Length; i++)
                    //{
                    //    result.Add(new NamedBooleanChangeTimed(item, boolChanged[i]));
                    //}
                    //}
                }
            }
            if (sortType == SortType.RecentToLatest)
                SortRecentToLatest(result);
            else
                SortLatestToRecent(result);
            return result;
        }

        public static string[] GetAllOnAsString(BooleanStateRegister reg)
        {
            List<string> foundActive = new List<string>();
            foreach (string name in reg.GetAllKeys())
            {
                if (reg.GetStateOf(name).GetValue())
                    foundActive.Add(name);
            }
            return foundActive.ToArray();
        }
        public static string[] GetAllOffAsString(BooleanStateRegister reg)
        {
            string[] all = reg.GetAllKeys();
            return all.Except(GetAllOnAsString(reg)).ToArray();
        }


        public static bool AreDown(BooleanStateRegister registre, BooleanIndexGroup toCheck)
        {
            return AND(registre, toCheck);
        }

        public static bool AreUp(BooleanStateRegister registre, BooleanIndexGroup toCheck)
        {
            return toCheck.GetNames().All(k => registre.Contains(k) && !registre.GetStateOf(k).GetValue());
        }
        public static bool AreDownStrict(BooleanStateRegister registre, BooleanIndexGroup range, BooleanIndexGroup toCheck)
        {
            return AreUp(registre, Except(range, toCheck)) && AreDown(registre, toCheck);
        }
        public static bool AreUpStrict(BooleanStateRegister registre, BooleanIndexGroup range, BooleanIndexGroup toCheck)
        {
            return AreUp(registre, toCheck) && AreDown(registre, Except(range, toCheck));
        }

        public class Description
        {
            public static string Join(string joinText, IEnumerable<NamedBooleanChangeTimed> changes)
            {
                return string.Join(joinText, changes.Select(k => GetDescriptionOf(k)).ToArray());
            }
            private static string GetDescriptionOf(NamedBooleanChangeTimed k)
            {
                return k.GetName() + ((k.GetWhenChanged().GetChange() == BooleanChangeType.SetTrue) ? '↓' : '↑');
            }
            public static string Join(string textJoin, BoolStatePeriode[] change)
            {
                return string.Join(textJoin, change.Select(k => k.GetState() ? "↓" : "↑").ToArray());
            }
        }
        // https://en.wikipedia.org/wiki/Bitwise_operation#XOR



        public static string GetTextFor(RegexableValueType textToApplyType, BooleanStateRegister register, BooleanIndexGroup group, float changeIntervalTracked)
        {
            string txt = "";
            if (textToApplyType == RegexableValueType.On)
            {
                txt = string.Join(" ", BooleanStateUtility.GetAllOnAsString(register));
            }
            else if (textToApplyType == RegexableValueType.Off)
            {
                txt = string.Join(" ", BooleanStateUtility.GetAllOffAsString(register));
            }
            else
            {
                BooleanStateUtility.SortType sort = textToApplyType == RegexableValueType.LastToNew ? BooleanStateUtility.SortType.LatestToRecent : BooleanStateUtility.SortType.RecentToLatest;
                List<NamedBooleanChangeTimed> change = new List<NamedBooleanChangeTimed>();

                change = BooleanStateUtility.GetBoolChangedBetweenTimeRangeFromNow(
                    register, group, changeIntervalTracked, sort);
                txt =BooleanStateUtility.Description.Join("", change);
            }
            return txt;

        }
    }

    public enum RegexableValueType { NewToLast, LastToNew, On, Off }
}