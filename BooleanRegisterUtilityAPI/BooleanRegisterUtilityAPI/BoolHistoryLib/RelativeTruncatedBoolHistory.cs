using BooleanRegisterUtilityAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI.BoolHistoryLib
{
    public class RelativeTruncatedBoolHistory
    {
        public BoolHistory m_origine;
        public ITimeValue m_nearestFromNow;
        public ITimeValue m_farestFromNow;
        public List<BoolStatePeriode> m_foundFromNowToPast = new List<BoolStatePeriode>();

        
        public RelativeTruncatedBoolHistory( BoolHistory origine, ITimeValue nearestFromNow, ITimeValue farestFromNow)
        {
            m_origine = origine;
            m_nearestFromNow = nearestFromNow;
            m_farestFromNow = farestFromNow;
        }

        public void AddInNowFront(BoolStatePeriode periode) {

            m_foundFromNowToPast.Insert(0,periode);
        }
        public void AddInLaterBack(BoolStatePeriode periode) {

            m_foundFromNowToPast.Add(periode);
        }

    }
}
