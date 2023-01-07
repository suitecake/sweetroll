using System;
using System.Collections.Generic;

namespace CakeAF.Entity
{
    public partial class Member
    {
        public Member()
        {
            TeamRecordMembers = new HashSet<TeamRecordMember>();
        }

        public int Id { get; set; }
        public int FriendId { get; set; }
        public string SummonerName { get; set; }
        public string Puuid { get; set; }
        public long LastProcessedGameTime { get; set; }

        public virtual Friend Friend { get; set; }
        public virtual ICollection<TeamRecordMember> TeamRecordMembers { get; set; }
    }
}
