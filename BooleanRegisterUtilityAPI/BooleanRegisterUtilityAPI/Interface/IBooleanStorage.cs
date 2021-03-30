using BooleanRegisterUtilityAPI.BooleanLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BooleanRegisterUtilityAPI.Interface
{
    public interface IBooleanStorage
    {

        void Add(string name, bool defaultValue);
        void Set(string name, bool value);
        /// <summary>
        /// Throw exception if something when wrong.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool GetValue(string name);
        bool Contains(string name);
        uint GetBooleanCount();

        void GetValue(string name, out bool value, out bool succedToReach);
        void GetFastAccess(string name, out IBooleanableRef value, out bool succedToReach);
        void GetFastAccess(string name, out INamedBooleanableRef value, out bool succedToReach);
        void GetHistoryAccess(string name, out IBooleanHistory value, out bool succedToReach);
        void GetHistoryAccess(string name, out INamedBooleanHistory value, out bool succedToReach);
        void GetNamesRegistered(out IEnumerable<string> names);
        void GetArrayCurrentState(out IArrayStateOfBooleanRegister currentStateRef);


    }

    public interface INamedBooleanableRef: IBooleanableRef {
        string GetName();
    
    }
    /// <summary>
    /// Any Boolean is the API has a value and named. Some don't but should not be accessible by the users of this code as they are temporary or composite of a logic.
    /// </summary>
    public interface INamedBoolean {

        /// <summary>
        /// Name the user gave to this boolean
        /// </summary>
        /// <returns></returns>
        string GetName();
        /// <summary>
        /// Give you the value of the boolean. Trigger exception on any problem.
        /// If you want no exception but check if the access is possible try to use @IBooleanableRef
        /// </summary>
        /// <returns></returns>
        bool GetCurrentValue();
    }

    /// <summary>
    /// List of boolean to manipulate based on the name given.
    /// </summary>
    public interface INamedBooleanGroup {
        string GetGroupName();
        IEnumerable<string> GetTargetedBooleanNames();

    }


    /// <summary>
    /// The code allow to chech the state of a boolean through switch in time.
    /// </summary>
    public interface IBooleanHistory : IBooleanableRef{

        void WasSwitchTo(bool stateObserved, out bool result,  ITimeValue from, ITimeValue to);

        void WasSwitchToTrue(out bool result,  ITimeValue from, ITimeValue to);
        void WasSwitchToTrue(out bool result,  DateTime now, DateTime from, DateTime to);
        void WasSwitchToTrue(out bool result,  DateTime from, DateTime to);

        void WasSwitchToFalse(out bool result, ITimeValue from, ITimeValue to);
        void WasSwitchToFalse(out bool result, DateTime now, DateTime from, DateTime to);
        void WasSwitchToFalse(out bool result, DateTime from, DateTime to);


        void StartAndFinishState(bool stateStart, bool statEnd,out bool result, ITimeValue from, ITimeValue to);
        void StartAndFinishState(bool stateStart, bool statEnd, out bool result, DateTime now, DateTime from, DateTime to);
        void StartAndFinishState(bool stateStart, bool statEnd, out bool result, DateTime from, DateTime to);


        void WasMaintained(bool stateObserved, out bool result,  ITimeValue from, ITimeValue to);

        void WasMaintainedTrue(out bool result, ITimeValue from, ITimeValue to);
        void WasMaintainedTrue(out bool result, DateTime now, DateTime from, DateTime to);
        void WasMaintainedTrue(out bool result,  DateTime from, DateTime to);

        void WasMaintainedFalse(out bool result, ITimeValue from, ITimeValue to);
        void WasMaintainedFalse(out bool result, DateTime now, DateTime from, DateTime to);
        void WasMaintainedFalse(out bool result, DateTime from, DateTime to);

        void GetSwitchCount(out ushort switch2True, out ushort switch2False, ITimeValue from, ITimeValue to);
        void GetSwitchCount(out ushort switch2True, out ushort switch2False, DateTime now, DateTime from, DateTime to);
        void GetSwitchCount(out ushort switch2True, out ushort switch2False, DateTime from, DateTime to);


        void GetPoucentOfState(bool stateObserved, out double pourcentState);
        void GetPoucentOfState(bool stateObserved, out double pourcentState, ITimeValue from, ITimeValue to);
        void GetPoucentOfState(bool stateObserved, out double pourcentState, DateTime now, DateTime from, DateTime to);
        void GetPoucentOfState(bool stateObserved, out double pourcentState, DateTime from, DateTime to);


        void GetBumpsCount(AllBumpType bumb, out uint count, ITimeValue from, ITimeValue to);
        void GetBumpsCount(AllBumpType bumb, out uint count, DateTime now, DateTime from, DateTime to);
        void GetBumpsCount(AllBumpType bumb, out uint count, DateTime from, DateTime to);



        void GetTimeCount(bool stateObserved, out uint pourcent);
        void GetTimeCount(bool stateObserved, out uint timeFound, ITimeValue from, ITimeValue to);
        void GetTimeCount(bool stateObserved, out uint timeFound, DateTime now, DateTime from, DateTime to);
        void GetTimeCount(bool stateObserved, out uint timeFound, DateTime from, DateTime to);


        void GetState(out bool value,  ITimeValue when);
        void GetState(out bool value,  DateTime now, DateTime when);
        void GetState(out bool value,  DateTime when);

        bool IsInRange(ITimeValue value);
        bool IsInRange(DateTime when, DateTime time);
    }

    public interface INamedBooleanHistory : IBooleanHistory, INamedBoolean { }

    /// <summary>
    /// History take space/resource and for reason wanted or not this information is not accessible.
    /// </summary>

    public interface IBooleanFastAccess : IBooleanableRef, INamedBoolean
    {
    
    }

    public interface IArrayStateOfBooleanRegister {
        uint GetArraySize();

        bool[] GetValuesCopy();
        string[] GetIndexNamesCopy();
        
        void GetValuesRef(ref bool[] value);
        void GetIndexNamesREf(ref string[] value);

        void GetWith(uint index, out IBooleanFastAccess access, out bool found);
        void GetWith(string name, out IBooleanFastAccess access, out bool found);

        bool Exists(uint index);
        bool Exists(string name);

    }

    public interface ITimeValue {
        void  SetAsMilliSeconds(double valueInMs);
        void GetAsMilliSeconds(out uint valueInMs);
        double GetAsMilliSeconds();
        double GetAsSeconds();
        double GetAsMinutes();
        double GetAsHours();
    }

    public interface ITimeOfDay {
        ushort GetHourOn24HFromat();
        ushort GetMinutes();
        ushort GetSeconds();
        ushort GetMilliseconds();
    }

    public interface IBooleanLogicParser
    {
        void GetLogicFrom( string text, out bool parsed, out IBooleanLogicCompiled compiledLogic);
    }
    public interface IBooleanLogicCompiled
    {
        void LinkToStorage(IBooleanStorage storage, out bool linked, out IBooleanableRef booleanableLogic);

    }

    public interface IRefBooleanRegister {

        IBooleanStorage GetRef(); 
        bool HasRef();
        void RedefineRegister(IBooleanStorage reg, out bool changed, out IBooleanStorage previous);
    }


    public interface IBoolObservedTime {
        bool IsTimeKey();
        bool IsTimeRange();
        IBoolTimeRange GetTimeRange();
        IBoolTimeKey GetTimeKey();
        // Just a informative value to say if the time linked it calculate using the now value.
        bool IsRelativeToNow();
        // Just a information to know if the now can affect the return time value.
        bool IsAbsolute();
        void SetWith(IBoolTimeRange timeRange, bool isRelative);
        void SetWith(IBoolTimeKey timeRange, bool isRelative);
        ObservedTimeType GetObservedType();
        bool IsDefined();
    }
    public enum ObservedTimeType {
        KeyRelative, RangeRelative, KeyAbsolute, RangeAbsolute,
        Undefined
    }

    public interface IBoolTimeRange
    {

        void GetTime(DateTime now, out DateTime nearestOfNow, out DateTime farestOfNow);
    }
    public interface IBoolTimeKey
    {

        void GetTime(DateTime now, out DateTime observed);
    }


    public interface ILogicBlock
    {
        void Get(out bool value, out bool computed);

    }


    public class NotRecoredInHistroyException : System.Exception { }
    public class DidNotSuccedToAccessTheBooleanValue : System.Exception { }
    public class DidNotSuccedToAccessTheBoolean : System.Exception { }


    public enum BooleanSwitchType { SetAsTrue, SetAsFalse }
    public enum BoolState { True, False }
    public enum BoolExistanceState { Exist, DontExist }
}
