using System;
using System.Collections.Generic;

namespace CakeAF.Entity
{
    public partial class TeamRecord
    {
        public TeamRecord()
        {
            TeamRecordMembers = new HashSet<TeamRecordMember>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal Value { get; set; }
        public DateTime RecordTime { get; set; }

        public virtual ICollection<TeamRecordMember> TeamRecordMembers { get; set; }
    }
}
