using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefRegisterMono : MonoBehaviour , IRefBooleanRegister
{

    public BooleanStateRegister m_register;
    public RefBooleanRegister m_refRegister;
    public bool m_setAsDefaultBLIF=true;

    public void Awake()
    {
        m_register = new BooleanStateRegister();
        m_refRegister = new RefBooleanRegister(m_register);
        if (m_setAsDefaultBLIF) { 
            BL.SetDefaultRegister(m_register);
            BL.FlushLogicBuild();
        }
    }

    public IBooleanStorage GetRef()
    {
        return m_refRegister.GetRef();
    }

    public void GetReferenceToRegister(out RefBooleanRegister refRegister)
    {
        refRegister = m_refRegister;

    }
    public void GetRegister(out BooleanStateRegister register)
    {
        register = m_register;

    }

    public bool HasRef()
    {
        return m_refRegister.HasRef();
    }

    public void RedefineRegister(IBooleanStorage reg, out bool changed, out IBooleanStorage previous)
    {
        m_refRegister.RedefineRegister(reg, out changed, out previous);
    }

    public void UpdateTimePast()
    {
        m_register.AddTimePastFromDefaultTimer();
    }
}
