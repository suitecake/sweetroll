using System;
using System.Collections.Generic;

namespace CakeAF.Entity
{
    public partial class Friend
    {
        public Friend()
        {
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
