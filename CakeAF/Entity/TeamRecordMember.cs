using System;
using System.Collections.Generic;

namespace CakeAF.Entity
{
    public partial class TeamRecordMember
    {
        public int TeamRecordId { get; set; }
        public int MemberId { get; set; }
        public int ChampionId { get; set; }

        public virtual Champion Champion { get; set; }
        public virtual Member Member { get; set; }
        public virtual TeamRecord TeamRecord { get; set; }
    }
}
