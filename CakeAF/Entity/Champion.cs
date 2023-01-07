using System;
using System.Collections.Generic;

namespace CakeAF.Entity
{
    public partial class Champion
    {
        public Champion()
        {
            SoloRecords = new HashSet<SoloRecord>();
            TeamRecordMembers = new HashSet<TeamRecordMember>();
        }

        public int Id { get; set; }
        public string InternalName { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<SoloRecord> SoloRecords { get; set; }
        public virtual ICollection<TeamRecordMember> TeamRecordMembers { get; set; }
    }
}
