using BooleanRegisterUtilityAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBool_PlayerBoss : MonoBehaviour
{
    public RefRegisterMono m_register;

    [Range(0, 1f)]
    public float playerLife=1f;
    public string playerLifeDeathName = "playerdeath";
    public float playerLifeDeath = 0.001f;
    public string playerLifeAlmostDeathName = "playerdanger";
    public float playerLifeAlmostDeath = 0.05f;
    public string playerLifeLowName="playerlow";
    public float playerLifeLow = 0.1f;
    public string playerLifeUnderMidhName="playerundermid";
    public float playerLifeUnderMid = 0.5f;
    public string playerLifFullName="playerfull";
    public float playerLifFull = 0.99f;



    public bool m_almostDieOnce;
    public bool m_almostDieTwice;
    public bool m_almostDieForLongTIme;

    private void Start()
    {
        UpdateInfo();
        InvokeRepeating("CheckSomeTime", 0, 1f);
    }
    public void CheckSomeTime() {

        m_almostDieOnce = BL.If("playerdanger⊔2#20s").TnE ;

        if (m_almostDieOnce == false &&  BL.If("playerdanger⊔1#10s").TnE) {
            m_almostDieOnce = true;
            Debug.Log("Nice !!! :)");
        }
        if (BL.If("playerdanger‾5s").TnE) {
            m_almostDieOnce = false;
        }

        m_almostDieForLongTIme = BL.If("playerdanger_10s").TnE;
    }

    public void Update()
    {

        m_register.GetRef().Set("playermoving", BL.If("[OR up left down right]").TnE);



        //        m_register.GetRef().Set("playermovingrecently", BL.If("[OR up↓5s left↓5s down↓5s right↓5s]").TnE);
        m_register.GetRef().Set("playermaintainonedirection", BL.If("[XOR up_0#1s left_0#1s down_0#1s right_0#1s]").TnE);

        if (BL.If("[AND up_0#1s left_0#1s down_0#1s right_0#1s]").TnE)
        {
            Debug.Log("Oh yeah");
        }
        if (BL.If("(up and down) xor(left & right)", "horizontalorvertical").TnE)
        {
            Debug.Log("Yo");
        }

        
    }



    private void UpdateInfo()
    {
        if (m_register != null && m_register.GetRef() != null)
        {
            m_register.GetRef().Set(playerLifeDeathName, playerLife <= playerLifeDeath);
            m_register.GetRef().Set(playerLifeAlmostDeathName, playerLife < playerLifeAlmostDeath);
            m_register.GetRef().Set(playerLifeLowName, playerLife < playerLifeLow);
            m_register.GetRef().Set(playerLifeUnderMidhName, playerLife < playerLifeUnderMid);
            m_register.GetRef().Set(playerLifFullName, playerLife > playerLifFull);
            m_register.GetRef().Set(playerLifeAlmostDeathName, playerLife > playerLifeAlmostDeath);
        }
    }

    private void OnValidate()
    {

        if( Application.isPlaying){
            UpdateInfo();
        }

    }
}
