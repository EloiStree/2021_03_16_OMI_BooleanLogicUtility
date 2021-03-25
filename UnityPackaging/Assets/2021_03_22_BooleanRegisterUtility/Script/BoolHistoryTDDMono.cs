using BooleanRegisterUtilityAPI.BooleanLogic.Time;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolHistoryTDDMono : MonoBehaviour
{
    public BoolHistory m_history = new BoolHistory(false, 20);

    public int m_maxMermory = 300;
    public float m_timeObserved;
    public double m_currentTime;
    public bool m_currentState;

    public long m_timeAt=2000;
    public bool m_timeAtState;

    public KeyCode m_key= KeyCode.Space;
    public string m_debugStringNowToPast;
    public Text m_debugDisplayNowToPast;
    public string m_debugStringNowToPastNumerical;
    public Text m_debugDisplayNowToPastNumerical;
    public string m_debugStringPastToNow;
    public Text m_debugDisplayPastToNow;

    public long m_timeA=3000;
    public long m_timeB=5000;
    public string m_debugStringAB;
    public Text m_debugDisplayAB;
    public bool m_isMaintaining;
    public bool m_isRealeasing;

    public long m_overwatch;
    public long m_overwatchTrue;
    public long m_overwatchFalse;

    public bool m_wasSwitchAndStillTrue;
    public bool m_wasSwitchAndStillFalse;


    [Range(0f,1f)]
    public double m_pourcentTrue;
    [Range(0f, 1f)]
    public double m_pourcentFalse;

    public BumpGroundType m_firstBumpTruetype;
    public HoleType m_firstBumpTruetypeHole;
    public List<BinaryBump> m_bumpsTrue;
    public List<BinaryBump> m_bumpsFalse;
    public List<BinaryHole> m_holesOnTrueGround;
    public List<BinaryHole> m_holesOnFalseGround;

    public RelativeTruncatedBoolHistory history;
    void Update()
    {

        m_history.SetMaxSaveTo(m_maxMermory);
        m_history.AddMilliSecondElapsedTime((long)(Time.deltaTime*1000.0));
        m_history.SetState(Input.GetKey(KeyCode.Space));

        m_timeObserved = m_history.GetTimeOverwatch();
        m_currentTime = m_history.GetInProgressState().GetElpasedTimeAsSecond();
        m_currentState = m_history.GetInProgressState().GetState();

        BoolStatePeriode p;
        m_history.GetPeriodeAt(new TimeInMsLong(m_timeAt), out p) ;
        m_timeAtState = p.GetState();
        m_debugStringNowToPast = BoolHistoryDescription.GetDescriptionNowToPast(m_history, m_timeAt / 1000f);
        if (m_debugDisplayNowToPast != null)
            m_debugDisplayNowToPast.text = m_debugStringNowToPast;
        m_debugStringNowToPastNumerical = BoolHistoryDescription.GetNumericDescriptionNowToPast(m_history);
        if (m_debugDisplayNowToPastNumerical != null)
            m_debugDisplayNowToPastNumerical.text = m_debugStringNowToPastNumerical;
        m_debugStringPastToNow = BoolHistoryDescription.GetDescriptionPastToNow(m_history, m_timeAt / 1000f);
        if (m_debugDisplayPastToNow != null)
            m_debugDisplayPastToNow.text = m_debugStringPastToNow;

        BoolStatePeriode[] ap;
        m_history.GetTruncatedHistoryCopy(new TimeInMsLong(m_timeA), new TimeInMsLong(m_timeB), out history ) ;
        history.GetArray(out ap);
        m_debugStringAB = BoolHistoryDescription.GetDescriptionNowToPast(history, m_timeAt / 1000f);
        if (m_debugDisplayAB != null)
            m_debugDisplayAB.text = m_debugStringAB;


        m_isMaintaining = history.IsMaintaining(true);
        m_isRealeasing = history.IsMaintaining(false);


        history.GetTrueAndFalseTimecount(out m_overwatchTrue, out m_overwatchFalse, out m_overwatch);
        m_wasSwitchAndStillTrue = history.WasSwitchAndStillIsTrue();
        m_wasSwitchAndStillFalse = history.WasSwitchAndStillIsFalse();

        history.GetTrueAndFalsePourcent(out m_pourcentTrue, out m_pourcentFalse);

        history.GetBumps(out m_bumpsTrue, out m_bumpsFalse);
        if (m_bumpsFalse.Count > 0)
        {
            m_firstBumpTruetype = m_bumpsFalse[0].GetBumpGroundType();
            m_firstBumpTruetypeHole = m_bumpsFalse[0].GetHoleType();
        }
        history.GetHoles(out m_holesOnFalseGround, out m_holesOnTrueGround);
      
    }
}
