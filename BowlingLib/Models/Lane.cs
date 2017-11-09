using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BowlingLib.Models
{
    public class Lane
    {
        public int LaneId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<LaneTimePeriod> LaneTimePeriods { get; set; }
    }
}
