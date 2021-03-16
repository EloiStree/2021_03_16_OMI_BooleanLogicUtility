using BooleanRegisterUtilityAPI.Beans;
using BooleanRegisterUtilityAPI.BoolHistoryLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BooleanRegisterUtilityAPI
{
    public class BooleanStateUtilityQuickDemo 
    {
        public BooleanStateRegisterMono m_linked;
        public BooleanIndexGroup m_observeGroup = new BooleanIndexGroup("ArrowUp", "ArrowLeft", "ArrowRight", "ArrowDown");
        public BooleanIndexGroup m_listened = new BooleanIndexGroup("ArrowUp", "ArrowDown");

        public string m_areUp;
        public string m_areDown;


        public bool m_groupOR;
        public bool m_groupAND;
        public bool m_groupXOR;
        public bool m_groupNOR;
        public bool m_groupNAND;

        public bool m_listenedAreDown;
        public bool m_listenedAreDownStrict;
        public bool m_listenedAreUp;
        public bool m_listenedAreUpStrict;


        public float m_timeToCheck = 1;
        public string m_recentToLatestChange = "";
        public string m_latestToRecentChange = "";
        public string m_test = "";

        public string m_regex;
        public string[] on;
        public string[] off;


        void Update()
        {

            BooleanStateRegister reg = m_linked.GetRegister();
           
            m_groupOR = BooleanStateUtility.OR(reg, m_listened);
            m_groupAND = BooleanStateUtility.AND(reg, m_listened);
            m_groupXOR = BooleanStateUtility.XOR(reg, m_listened);
            m_groupNOR = BooleanStateUtility.NOR(reg, m_listened);
            m_groupNAND = BooleanStateUtility.NAND(reg, m_listened);

            m_listenedAreDown = BooleanStateUtility.AreDown(reg, m_listened);
            m_listenedAreDownStrict = BooleanStateUtility.AreDownStrict(reg, m_observeGroup, m_listened);
            m_listenedAreUp = BooleanStateUtility.AreUp(reg, m_listened);
            m_listenedAreUpStrict = BooleanStateUtility.AreUpStrict(reg, m_observeGroup, m_listened);


            List<NamedBooleanChangeTimed> recentToLatest = BooleanStateUtility.GetBoolChangedBetweenTimeRangeFromNow(reg, m_observeGroup, m_timeToCheck, BooleanStateUtility.SortType.RecentToLatest);
            NamedBooleanChangeTimed[] latestToRecent = recentToLatest.ToArray();
            Array.Reverse(latestToRecent);

            m_latestToRecentChange = BooleanStateUtility.Description.Join("", recentToLatest);
            m_recentToLatestChange = BooleanStateUtility.Description.Join("", latestToRecent);
           
            BoolHistory rbh = reg.GetStateOf("R").GetHistory();
            BoolStatePeriode[] change;
            rbh.GetFromPastToNow(out change, true);
            m_test = BooleanStateUtility.Description.Join("", change);

             on = BooleanStateUtility.GetAllOnAsString(reg);
             off = BooleanStateUtility.GetAllOffAsString(reg);


        }

        public void SetRegex(string regex) { m_regex = regex; }
        public void SetGroupSplitBySpace(string groupAsString)
        {
            string[] tokens = groupAsString.Split();
            m_observeGroup = new BooleanIndexGroup(tokens.Where(k => k.Length > 0).ToArray());
        }

      

    }

}