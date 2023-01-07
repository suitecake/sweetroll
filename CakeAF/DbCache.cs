using System;
using System.Collections.Generic;
using System.Linq;

using CakeAF.Entity;

namespace CakeAF
{
    public class DbCache
    {
        public List<Member> Members;

        public HashSet<string> MemberPuuidSet
        {
            get
            {
                return Members.Select(x => x.Puuid).ToHashSet();
            }
        }

        public List<SoloRecord> SoloRecords;

        public List<TeamRecord> TeamRecords;
    }
}
