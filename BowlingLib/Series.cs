using AccountabilityLib.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib
{
    public class Series
    {
        public int SeriesId { get; set; }
        public int PlayerId { get; set; }
        public short Score { get; set; }

        public Party Player { get; set; }
    }
}
