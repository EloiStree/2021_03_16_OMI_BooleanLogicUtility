using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTestMono : MonoBehaviour
{
    public string m_ulong;
    public string m_long;
    public string m_int;
    public string m_uint;
    public string m_ushort;
    public string m_short;


    public string m_ulongSec;
    public string m_longSec;
    public string m_intSec;
    public string m_uintSec;
    public string m_ushortSec;
    public string m_shortSec;

    public string m_ulongHour;
    public string m_longHour;
    public string m_intHour;
    public string m_uintHour;

    public string m_ulongDay;
    public string m_longDay;
    public string m_intDay;
    public string m_uintDay;

    public string m_ulongYear;
    public string m_longYear;

    private void OnValidate()
    {

        m_ulong = "" + ulong.MaxValue;
        m_long = "" + long.MaxValue;
        m_int = "" + int.MaxValue;
        m_uint = "" + uint.MaxValue;
        m_ushort = "" + ushort.MaxValue;
        m_short = "" + short.MaxValue;

        m_ulongSec = "" + ulong.MaxValue / 1000.0;
        m_longSec = "" + long.MaxValue / 1000.0;
        m_intSec = "" + int.MaxValue / 1000.0;
        m_uintSec = "" + uint.MaxValue / 1000.0;
        m_ushortSec = "" + ushort.MaxValue / 1000.0;
        m_shortSec = "" + short.MaxValue / 1000.0;

        m_ulongHour = "" + ulong.MaxValue / 3600000.0;
        m_longHour = "" + long.MaxValue / 3600000.0;
        m_intHour = "" + int.MaxValue / 3600000.0;
        m_uintHour = "" + uint.MaxValue / 3600000.0;

        ulong daysInsMs =(long)(  3600 * 24 *1000);
        m_ulongDay = "" + ulong.MaxValue / (daysInsMs);
        m_longDay = "" + long.MaxValue / (daysInsMs);
        m_intDay = "" + int.MaxValue / (daysInsMs);
        m_uintDay = "" + uint.MaxValue / (daysInsMs);

        m_ulongYear = "" + (ulong.MaxValue / (daysInsMs))/365;
        m_longYear = "" + (long.MaxValue / (daysInsMs))/365;

    }
}
