using AccountabilityLib.Classes;
using System;
using System.Collections.Generic;

namespace AccountabilityLib.Interfaces
{
    public interface IPartyRepository
    {
        Party GetParty(string legalId);
        Party GetParty(int partyId);
        Party CreateParty(string name, string legalId);
        void CreateAccountability(Party commissioner, Party responsible, AccountabilityType type, DateTime? start = null, DateTime? end = null);
        TimePeriod GetTimePeriod(int timePeriodId);
        TimePeriod SearchForPeriod(DateTime startTime, DateTime endTime);
        ICollection<Party> GetCommissioners(Party responsible, AccountabilityType type);
        ICollection<Party> GetResponsible(Party commissioner, AccountabilityType type);
        ICollection<Party> GetAllParties();
    }
}
