
using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.Beans;
using System;
using UnityEngine;

public abstract class AbstractRegisterTestMono : MonoBehaviour
{
    public bool up, down, left, right, space;
    public RefBooleanRegister m_refregister;
    public BooleanStateRegister m_register;



    [Range(0.0f, 6.0f)]
    public double m_key = 2;
    [Range(0.0f, 10f)]
    public double m_start = 2, m_end = 5;

    [TextArea(2, 15)]
    public string m_debugRegister;

    void Start()
    {
        m_register = new BooleanStateRegister();
        m_refregister = new RefBooleanRegister(m_register);

        Init();
    }

    public abstract void Init();

    void Update()
    {
        m_register.AddTimePastFromDefaultTimer();
        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.RightArrow);
        m_register.Set("up", up);
        m_register.Set("down", down);
        m_register.Set("left", left);
        m_register.Set("right", right);

        if (Input.GetKeyDown(KeyCode.Space))
            m_register.Set("space", true);
        if (Input.GetKeyUp(KeyCode.Space))
            m_register.Set("space", false);

        DoTheThing();
    }

    public abstract void DoTheThing();
}