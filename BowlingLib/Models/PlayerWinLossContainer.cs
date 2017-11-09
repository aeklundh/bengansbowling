using AccountabilityLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib.Models
{
    internal class PlayerWinLossContainer
    {
        internal Party Player { get; set; }
        internal int Wins { get; set; }
        internal int Losses { get; set; }
        internal double WinPercentage {
            get {
                return ((double)Wins / (Wins + Losses)) * 100;
            }
        }
    }
}
