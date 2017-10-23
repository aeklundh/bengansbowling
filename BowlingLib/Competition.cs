using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib
{
    public class Competition
    {
        public int CompetitionId { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
