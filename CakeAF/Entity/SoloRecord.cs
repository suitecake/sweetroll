using System;
using System.Collections.Generic;

namespace CakeAF.Entity
{
    public partial class SoloRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public int? MemberId { get; set; }
        public decimal? Value { get; set; }
        public DateTime? Time { get; set; }
        public int? ChampionId { get; set; }

        public virtual Champion Champion { get; set; }
    }
}
