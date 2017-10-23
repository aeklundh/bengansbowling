using AccountabilityLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib.Models
{
    public class Series
    {
        public int SeriesId { get; set; }
        public short Score { get; set; }
        public int RoundId { get; set; }
        public int PlayerId { get; set; }

        public Round Round { get; set; }
        public Party Player { get; set; }
    }
}
