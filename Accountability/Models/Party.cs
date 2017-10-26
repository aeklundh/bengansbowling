using System;
using System.Collections.Generic;
using System.Text;

namespace AccountabilityLib.Models
{
    public class Party
    {
        public int PartyId { get; set; }
        public string Name { get; set; }
        public string LegalId { get; set; }

        public ICollection<Accountability> Commissions { get; set; }
        public ICollection<Accountability> Responsibilities { get; set; }
    }
}
