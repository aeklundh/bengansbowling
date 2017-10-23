using System.ComponentModel.DataAnnotations;

namespace AccountabilityLib.Classes
{
    public class AccountabilityType
    {
        public int AccountabilityTypeId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}