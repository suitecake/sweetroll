using System.Collections.Generic;

using CakeAF.Entity;

namespace CakeAF
{
    public class FoundRecords
    {
        public List<Pentakill> Pentakills { get; set; }

        public List<SoloRecord> SoloRecords { get; set; }

        public List<TeamRecord> TeamRecords { get; set; }
    }
}
