using System;

namespace AccountabilityLib.Models
{
    public class TimePeriod
    {
        public int TimePeriodId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}