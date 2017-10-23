using System;

namespace AccountabilityLib.Models
{
    public class Accountability
    {
        public int CommissionerId { get; set; }
        public int ResponsibleId { get; set; }
        public int AccountabilityTypeId { get; set; }
        public int? TimePeriodId { get; set; }

        public Party Commissioner { get; set; }
        public Party Responsible { get; set; }
        public AccountabilityType AccountabilityType { get; set; }
        public TimePeriod TimePeriod { get; set; }
    }
}
