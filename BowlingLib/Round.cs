using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib
{
    public class Round
    {
        public int RoundId { get; set; }
        public int MatchId { get; set; }

        public ICollection<Series> Series { get; set; }
        public Match Match { get; set; }
    }
}
