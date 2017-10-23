using AccountabilityLib.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountabilityLib.Interfaces
{
    public interface IPartyRepository
    {
        Task<Party> GetParty(string legalId);
        Task<Party> GetParty(int partyId);
        Task<Party> CreateParty(string name, string legalId);
        void CreateAccountability(Party commissioner, Party responsible, AccountabilityType type, DateTime? start = null, DateTime? end = null);
        Task<TimePeriod> GetTimePeriod(int timePeriodId);
        Task<TimePeriod> SearchForPeriod(DateTime startTime, DateTime endTime);
        Task<ICollection<Party>> GetCommissioners(Party responsible, AccountabilityType type);
        Task<ICollection<Party>> GetResponsible(Party commissioner, AccountabilityType type);
        Task<ICollection<Party>> GetAllParties();
    }
}
