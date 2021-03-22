using System.Collections;
using System.Collections.Generic;

public class BoolHistoryDescription
{

    public static string GetDescriptionPastToNow(BoolHistory h, float timeWatch = 1f, float dotPerSecond = 4, string falseSym = "_", string trueSym = "‾", string switchTrueSym = "1↓", string switchFalseSym = "0↑")
    {
        BoolStatePeriode[] history;
        h.GetFromPastToNow(out history, false);
        string result = GetDescriptionPastToNow(history,  dotPerSecond, falseSym, trueSym, switchTrueSym, switchFalseSym);

        return result;
    }

    public static string GetDescriptionPastToNow(BoolStatePeriode[] history,  float dotPerSecond = 4, string falseSym = "_", string trueSym = "‾", string switchTrueSym = "1↓", string switchFalseSym = "0↑")
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

    
    public static string GetDescriptionNowToPast(RelativeTruncatedBoolHistory truncateHistory, float timeWatch = 1f, float dotPerSecond = 4, string falseSym = "_", string trueSym = "‾", string switchTrueSym = "1↓", string switchFalseSym = "0↑")
    {
        BoolStatePeriode[] history;
        truncateHistory.GetArray(out history);
        string result = "";
        if (truncateHistory.IsEmpty())
        {
            result = truncateHistory.GetValueIfEmpty() ? "↑" : "↓";
        }
        else { 
            result = GetDescriptionNowToPast(history, timeWatch, dotPerSecond, falseSym, trueSym, switchTrueSym, switchFalseSym);
        }

        int st, sf;
        truncateHistory.GetSwitchCount(out st, out sf);
        result += string.Format(" {0}↑ {1}↓", st, sf);


        return result;
    }

    public static string GetDescriptionNowToPast(BoolHistory h, float timeWatch = 1f, float dotPerSecond = 4, string falseSym = "_", string trueSym = "‾", string switchTrueSym = "↓", string switchFalseSym = "↑")
    {
        BoolStatePeriode[] history;
        h.GetFromNowToPast(out history, false);

        string result = GetDescriptionNowToPast(history , timeWatch,  dotPerSecond, falseSym, trueSym, switchTrueSym, switchFalseSym);

        return result;
    }

    public static string GetDescriptionNowToPast(BoolStatePeriode[] history , float timeWatch = 1f,  float dotPerSecond = 4, string falseSym = "_", string trueSym = "‾", string switchTrueSym = "↓", string switchFalseSym = "↑") //🁤🁪 1↓ 0↑
    {
        if (history == null || history.Length == 0) {
            return "none";
        }
        bool watcherUse = false;
        float timePast = 0;
        string result = "";
        if (dotPerSecond == 0)
            dotPerSecond = 1;


        for (int i = 0; i < history.Length; i++)
        {

            if(i!=0)
                result += history[i].GetState() ? switchTrueSym : switchFalseSym;
            for (int j = 0; j < 1 + (int)(history[i].GetElpasedTimeAsSecond() / (1f / dotPerSecond)); j++)
            {
                timePast += (1f / (float)dotPerSecond);
                if (!watcherUse && timePast > timeWatch)
                {
                    watcherUse = true;
                    result += "|";
                }
                result += history[i].GetState() ? trueSym : falseSym;

            }
        }

        return result;
    }

    public static string GetNumericDescriptionNowToPast(BoolHistory h, float timeWatch = 1f, string switchTrueSym = "‾", string switchFalseSym = "_")
    {
        bool watcherUse = false;
        double timePast = 0;
        string result = "";
        BoolStatePeriode[] history;
        h.GetFromNowToPast(out history, false);

        for (int i = 0; i < history.Length; i++)
        {

            result += string.Format("{1}{1}{0:0}{1}{1}", history[i].GetElpasedTimeAsLongMs(), (history[i].GetState() ? switchTrueSym : switchFalseSym));
            timePast += history[i].GetElpasedTimeAsSecond();

            if (!watcherUse && timePast > timeWatch)
            {
                watcherUse = true;
                result += "|";
            }
        }


        return result;
    }

}