using System.ComponentModel.DataAnnotations;

namespace BowlingLib
{
    public class Lane
    {
        public int LaneId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
