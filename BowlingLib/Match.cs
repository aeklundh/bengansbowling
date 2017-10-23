using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib
{
    public class Match
    {
        public int MatchId { get; set; }
        public int LaneId { get; set; }
        public int? CompetitionId { get; set; }
        public DateTime PlayedOn { get; set; }

        public Competition Competition { get; set; }
        public ICollection<Round> Rounds { get; set; }
        public Lane Lane { get; set; }
    }
}
