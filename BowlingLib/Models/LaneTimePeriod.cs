using AccountabilityLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BowlingLib.Models
{
    public class LaneTimePeriod
    {
        public int LaneId { get; set; }
        public int TimePeriodId { get; set; }

        public Lane Lane { get; set; }
        [ForeignKey(nameof(TimePeriodId))]
        public TimePeriod TimePeriod { get; set; }
    }
}
