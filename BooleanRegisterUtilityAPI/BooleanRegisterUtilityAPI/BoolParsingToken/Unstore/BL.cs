using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using BooleanRegisterUtilityAPI.BoolParsingToken.Unstore;
using System.Collections.Generic;
using System;

namespace BooleanRegisterUtilityAPI
{
    public class BL
    {
        public static RefBooleanRegister m_defaultregister = new RefBooleanRegister( new BooleanStateRegister() );
        public static Dictionary<string, LogicBlock> m_dictionnary = new Dictionary<string, LogicBlock>();


        public static void SetDefaultRegister(BooleanStateRegister register)
        {
            m_defaultregister.m_target = register;
            FlushLogicBuild();
        }


        public static void FlushLogicBuild() {
            m_dictionnary.Clear();
        }


        public static BoolConditionResult If(string condition, bool recordLogic=true)
        {
            return If(condition ,null);
        }

        public static BoolConditionResult If(string condition, string storeResultIn, bool recordLogic = true)
        {
            try
            {
                if (!IsRegisterDefined()) {
                    return new BoolConditionResult(false, false, "Register are not defined");   
                }

                BoolConditionResult result;
                bool found;
                LogicBlock logic;
                ContaintLogic(condition, out found, out logic);
                if (!found)
                {
                    CreateLogic(condition,  out logic, recordLogic);
                }

                if (logic == null)
                {
                    return new BoolConditionResult(false, false, "Did not succed to get or create the logic block");
                }

                bool value;
                bool computed;
                ComputeWith( logic, out value, out computed);
                result = new BoolConditionResult(value, computed);

                if (!string.IsNullOrEmpty(storeResultIn) && result.DidCompute) {
                   m_defaultregister.m_target.Set(storeResultIn, result.Value);
                }

                return result;
            }
            catch (Exception e)
            {
                return new BoolConditionResult(false, false, e.StackTrace);
            }
        }

        public static LogicBlock GetRecordedLogic(string condition)
        {
            if (m_dictionnary.ContainsKey(condition))
                return m_dictionnary[condition];
             return null;
        }

        public static IBooleanStorage GetRegister()
        {
            return m_defaultregister.GetRef();
        }

        public static bool IsRegisterDefined()
        {
            return m_defaultregister != null && m_defaultregister.m_target != null;
        }

        private static void ComputeWith( LogicBlock logic, out bool value, out bool computed)
        {
            if (logic == null)
            {
                value = false;
                computed = false;
            }
            else { 
                logic.Get(out value, out computed);
            }

        }

        private static void CreateLogic(string condition, out LogicBlock logic, bool recordLogic)
        {
            logic = null;

            LogicBlockBuilder builder = new LogicBlockBuilder();
            ITimeValue tf = new TimeInMsLong(5000);
            ITimeValue tn = new TimeInMsLong(0);

            IBoolObservedTime key = new BL_TimeToObserve(true, new BL_RelativeTimeFromNow(tf));
            IBoolObservedTime range = new BL_TimeToObserve(true, new BL_RelativeTimeDurationFromNow(tn,tf));
            //logic = builder.Start(new AndLogic(
            //    new RegisterRefStateTrueBlock(m_defaultregister, new BL_BooleanItemDefault("up")),
            //    new RegisterRefBoolExistBlock(m_defaultregister, new BL_BooleanItemExist("up")),
            //    new RegisterRefStateBlock(m_defaultregister, new BL_BooleanItemIsTrueOrFalse("up")),
            //    new RegisterRefStateAtBlock(m_defaultregister, new BL_BooleanItemIsTrueOrFalseAt("up", range, true)),
            //    new RegisterRefMaintainingBlock(m_defaultregister, new BL_BooleanItemMaintaining("up", range, true)),
            //    new RegisterRefSwitchBetweenBlock(m_defaultregister, new BL_BooleanItemSwitchBetween("up", range, true))
            //    )).GetCurrent();

            //logic = builder.AppendLeft(AppendDuoType.And,
            //    new RegisterRefStateTrueBlock(m_defaultregister, new BL_BooleanItemDefault("up"))).
            //  AppendLeft(AppendDuoType.And,
            //  new RegisterRefBoolExistBlock(m_defaultregister, new BL_BooleanItemExist("up"))).
            //  AppendLeft(AppendDuoType.And,
            //   new RegisterRefStateBlock(m_defaultregister, new BL_BooleanItemIsTrueOrFalse("up"))).
            //  AppendLeft(AppendDuoType.And,
            //   new RegisterRefStateAtBlock(m_defaultregister, new BL_BooleanItemIsTrueOrFalseAt("up", key, true))).
            //  AppendLeft(AppendDuoType.And,
            //   new RegisterRefMaintainingBlock(m_defaultregister, new BL_BooleanItemMaintaining("up", range, true))).
            //  AppendLeft(AppendDuoType.And,
            //   new RegisterRefSwitchBetweenBlock(m_defaultregister, new BL_BooleanItemSwitchBetween("up", range, true))
            //   ).GetCurrent();


            //logic = builder.AppendLeft(AppendDuoType.And,
            //    new RegisterRefStateTrueBlock(m_defaultregister, new BL_BooleanItemDefault("up"))).
            //  AppendLeft(AppendDuoType.And,
            //  new RegisterRefBoolExistBlock(m_defaultregister, new BL_BooleanItemExist("up"))).
            //  AppendLeft(AppendDuoType.And,
            //   new RegisterRefStateBlock(m_defaultregister, new BL_BooleanItemIsTrueOrFalse("up",true)))
            //   .GetCurrent();



            if (recordLogic) {

                if (m_dictionnary.ContainsKey(condition))
                    m_dictionnary[condition] = logic;
                else m_dictionnary.Add(condition, logic);
            }

        }

        private static void ContaintLogic(string condition, out bool found, out LogicBlock logic)
        {
            if (m_dictionnary.ContainsKey(condition))
            {
                found = true;
                logic = m_dictionnary[condition];
            }
            else { 
                found = false;
                logic = null;
            }
        }
        public class DidNotComputeForSomeReasonException : Exception {}

        
    }
    public class BoolConditionResult
    {
        bool m_isConditionTrue;
        bool m_computedWithoutProblem;
        string m_errorInformation = "";

        public BoolConditionResult(bool isConditionTrue, bool computedWithoutProblem)
        {
            m_isConditionTrue = isConditionTrue;
            m_computedWithoutProblem = computedWithoutProblem;
        }
        public BoolConditionResult(bool isConditionTrue, bool computedWithoutProblem, string errorInformation)
        {
            m_isConditionTrue = isConditionTrue;
            m_computedWithoutProblem = computedWithoutProblem;
            m_errorInformation = errorInformation;
        }

        public bool Value { get { return m_isConditionTrue; } }
        public bool DidCompute { get { return m_computedWithoutProblem; } }
        public bool DidNotCompute { get { return !m_computedWithoutProblem; } }
        public string ErrorStack { get { return m_errorInformation; } }


        public bool TrueWithoutError { get { return m_computedWithoutProblem && true == m_isConditionTrue; } }
        public bool FalseWithoutError { get { return m_computedWithoutProblem && false == m_isConditionTrue; } }

        public bool TnE { get { return TrueWithoutError; } }
        public bool FnE { get { return FalseWithoutError; } }
    }

    public class RefBooleanRegister
    {
        public IBooleanStorage m_target;
        public ChangeOfRegister m_onChange;

        public RefBooleanRegister(IBooleanStorage register)
        {
            RedefineRegister(register);
        }
        public IBooleanStorage GetRef() { return m_target; }
        public bool HasRef() { return m_target != null; }

        public void RedefineRegister(IBooleanStorage register)
        {
            IBooleanStorage previous = m_target;
            IBooleanStorage current = register;
            m_target = current;
            if (m_onChange != null)
                m_onChange(previous, current);
        }
        public void AddRefChangeListener(ChangeOfRegister changeListener)
        {
            m_onChange += changeListener;
        }
        public void RemoveRefChangeListener(ChangeOfRegister changeListener)
        {
            m_onChange -= changeListener;
        }
        public delegate void ChangeOfRegister(IBooleanStorage previousRegister, IBooleanStorage newRegister);
    }
}