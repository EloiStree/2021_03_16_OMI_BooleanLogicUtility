using System.Collections;
using System.Collections.Generic;

public class BoolHistoryDescription
{

    public static string GetDescriptionPastToNow(BoolHistory h, float dotPerSecond = 4, string falseSym = "‾", string trueSym = "_", string switchTrueSym = "0↑", string switchFalseSym = "1↓")
    {
        BoolStatePeriode[] history;
        h.GetFromPastToNow(out history, false);
        string result = GetDescriptionPastToNow(history,  dotPerSecond, falseSym, trueSym, switchTrueSym, switchFalseSym);

        return result;
    }

    public static string GetDescriptionPastToNow(BoolStatePeriode[] history,  float dotPerSecond = 4, string falseSym = "‾", string trueSym = "_", string switchTrueSym = "0↑", string switchFalseSym = "1↓")
    {
        string result = "";
        if (dotPerSecond == 0)
            dotPerSecond = 1;

        for (int i = 0; i < history.Length; i++)
        {

            for (int j = 0; j < 1 + (int)(history[i].GetElpasedTimeAsSecond() / (1f / dotPerSecond)); j++)
            {

                result += history[i].GetState() ? trueSym : falseSym;

            }
            result += history[i].GetState() ? switchTrueSym : switchFalseSym;
        }

        return result;
    }

    
    public static string GetDescriptionNowToPast(RelativeTruncatedBoolHistory truncateHistory, float timeWatch = 1f, float dotPerSecond = 4, string falseSym = "‾", string trueSym = "_", string switchTrueSym = "0↑", string switchFalseSym = "1↓")
    {
        BoolStatePeriode[] history;
        truncateHistory.GetArray(out history);
        string result = "";
        if (truncateHistory.IsEmpty())
        {
            result = truncateHistory.GetValueIfEmpty() ? "↑" : "↓";
        }
        else { 
            result = GetDescriptionNowToPast(history, dotPerSecond, falseSym, trueSym, switchTrueSym, switchFalseSym);
        }

        int st, sf;
        truncateHistory.GetSwitchCount(out st, out sf);
        result += string.Format(" {0}↑ {1}↓",  sf, st);


        return result;
    }

    public static string GetDescriptionNowToPast(BoolHistory h,  float dotPerSecond = 4, string falseSym = "‾", string trueSym = "_", string switchTrueSym = "↑", string switchFalseSym = "↓")
    {
        BoolStatePeriode[] history;
        h.GetFromNowToPast(out history, false);

        string result = GetDescriptionNowToPast(history ,  dotPerSecond, falseSym, trueSym, switchTrueSym, switchFalseSym);

        return result;
    }

    public static string GetDescriptionNowToPast(BoolStatePeriode[] history ,  float dotPerSecond = 4, string falseSym = "‾", string trueSym = "_", string switchTrueSym = "↑", string switchFalseSym = "↓") 
    {
        if (history == null || history.Length == 0) {
            return "none";
        }
        string result = "";
        if (dotPerSecond == 0)
            dotPerSecond = 1;


        for (int i = 0; i < history.Length; i++)
        {

            if(i!=0)
                result += history[i].GetState() ? switchTrueSym : switchFalseSym;
            for (int j = 0; j < 1 + (int)(history[i].GetElpasedTimeAsSecond() / (1f / dotPerSecond)); j++)
            {
                
                result += history[i].GetState() ? trueSym : falseSym;

            }
        }

        return result;
    }

    public static string GetNumericDescriptionNowToPast(BoolHistory h,  string switchTrueSym = "_", string switchFalseSym = "‾")
    {
        double timePast = 0;
        string result = "";
        BoolStatePeriode[] history;
        h.GetFromNowToPast(out history, false);

        for (int i = 0; i < history.Length; i++)
        {

            result += string.Format("{1}{1}{0:0}{1}{1}", history[i].GetElpasedTimeAsLongMs(), (history[i].GetState() ? switchTrueSym : switchFalseSym));
            timePast += history[i].GetElpasedTimeAsSecond();

            
        }


        return result;
    }

}