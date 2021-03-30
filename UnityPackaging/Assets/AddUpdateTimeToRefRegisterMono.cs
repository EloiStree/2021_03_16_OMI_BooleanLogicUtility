using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUpdateTimeToRefRegisterMono : MonoBehaviour
{
    public RefRegisterMono m_target;
   
    void Update()
    {
        if (m_target != null)
        m_target.UpdateTimePast();


    }
}
