using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Time;
using BooleanRegisterUtilityAPI.BoolParsingToken.Unstore;
using BooleanRegisterUtilityAPI.Enum;
using BooleanRegisterUtilityAPI.Interface;
using BooleanRegisterUtilityAPI.RegisterRefBlock;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefRegisterLogicMono : MonoBehaviour
{
    public bool up, down, left, right,space;
    RefBooleanRegister m_refregister;
    BooleanStateRegister m_register ;
    RegisterRefBoolExistBlock       m_boolExist;
    RegisterRefBumpsInRange         m_boolbump;
    RegisterRefMaintainingBlock     m_boolMaintaining;
    RegisterRefPourcentStateInRange m_boolPourcent;
    RegisterRefStartFinishInRange   m_boolStartFinish;
    RegisterRefStateAtBlock         m_boolStateAt;
    RegisterRefStateBlock           m_boolState;
    RegisterRefStateTrueBlock       m_boolStateTrue;
    RegisterRefSwitchBetweenBlock   m_boolSwitchBetween;
    RegisterRefTimecountInRange     m_timeCount;

    public bool m_boolExistResult;
    public bool m_boolbumpResult;
    public bool m_boolMaintainingResult;
    public bool m_boolPourcentResult;
    public bool m_boolStartFinishResult;
    public bool m_boolStateAtResult;
    public bool m_boolStateResult;
    public bool m_boolStateTrueResult;
    public bool m_boolSwitchBetweenResult;
    public bool m_timeCountResult;


    [Range(0.0f,6.0f)]
    public double m_key=2;
    [Range(0.0f, 10f)]
    public double m_start=2, m_end=5;

    [TextArea(2,15)]
    public string m_debugRegister;

    void Start()
    {
        m_register = new BooleanStateRegister();
       m_refregister = new RefBooleanRegister(m_register);

        IBoolObservedTime key = new BL_TimeToObserve(true,
            new BL_RelativeTimeFromNow(
                new TimeInMsLong((long)(m_key * 1000.0))));

        IBoolObservedTime range = new BL_TimeToObserve(true,
            new BL_RelativeTimeDurationFromNow(
                new TimeInMsLong((long)(m_start * 1000.0)),
            new TimeInMsLong((long)(m_end * 1000.0))));


        m_boolExist = new RegisterRefBoolExistBlock(m_refregister,
        new BL_BooleanItemExist("space", BoolExistanceState.Exist));

        m_boolStateTrue = new RegisterRefStateTrueBlock(m_refregister,
            new BL_BooleanItemDefault("up"));
        
        m_boolState = new RegisterRefStateBlock(m_refregister,
            new BL_BooleanItemIsTrueOrFalse("up", false));
        
        m_boolStateAt = new RegisterRefStateAtBlock(m_refregister,
            new BL_BooleanItemIsTrueOrFalseAt("up", key, true));
        
        m_boolPourcent = new RegisterRefPourcentStateInRange(m_refregister,
            new BL_BooleanItemPourcentStateInRange("up", BoolState.True, 0.8,
            ValueDualSide.More, range));
        m_timeCount = new RegisterRefTimecountInRange(m_refregister,
            new BL_BooleanItemTimeCountInRange("up", new TimeInMsLong(600), ValueDualSide.More, range, BoolState.True)
            );
        m_boolbump = new RegisterRefBumpsInRange(m_refregister,
            new BL_BooleanItemBumpsInRange("up", ObservedBumpType.Equal, AllBumpType.GroundBump, 2, range));
        m_boolMaintaining = new RegisterRefMaintainingBlock(m_refregister,
            new BL_BooleanItemMaintaining("up", range, true));
        m_boolStartFinish = new RegisterRefStartFinishInRange(m_refregister,
            new BL_BooleanItemStartFinish(BoolState.True, BoolState.False, "up", range));
        m_boolSwitchBetween = new RegisterRefSwitchBetweenBlock(m_refregister,
            new BL_BooleanItemSwitchBetween("up", SwitchTrackedType.SwitchRecently, range, true));
    }

    void Update()
    {
        m_register.AddTimePastFromDefaultTimer();
        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.RightArrow);
        m_register.Set("up", up);
        m_register.Set("down",  down);
        m_register.Set("left",  left);
        m_register.Set("right", right);

        if (Input.GetKeyDown(KeyCode.Space))
            m_register.Set("space", true);
        if (Input.GetKeyUp(KeyCode.Space))
            m_register.Set("space", false);


        bool computed;

        m_boolExist.Get(out m_boolExistResult, out computed);
        m_boolbump.Get(out m_boolbumpResult, out computed);
        m_boolMaintaining.Get(out m_boolMaintainingResult, out computed);
        m_boolPourcent.Get(out m_boolPourcentResult, out computed);
        m_boolStartFinish.Get(out m_boolStartFinishResult, out computed);
        m_boolStateAt.Get(out m_boolStateAtResult, out computed);
        m_boolState.Get(out m_boolStateResult, out computed);
        m_boolStateTrue.Get(out m_boolStateTrueResult, out computed);
        m_boolSwitchBetween.Get(out m_boolSwitchBetweenResult, out computed);
        m_timeCount.Get(out m_timeCountResult, out computed);

        m_debugRegister = m_register.GetFullHistoryDebugText(60);

    }
}
