using AccountabilityLib.Interfaces;
using System;
using System.Collections.Generic;
using AccountabilityLib.Models;
using System.Threading.Tasks;
using System.Linq;

namespace BowlingUnitTestsLib.Repositories
{
    public class FakePartyRepository : IPartyRepository
    {
        private ICollection<Party> _parties = new List<Party>();
        private ICollection<AccountabilityType> _accountabilityTypes = new List<AccountabilityType>();
        private ICollection<Accountability> _accountability = new List<Accountability>();

        public Task CreateAccountability(Party commissioner, Party responsible, AccountabilityType type, DateTime? start = null, DateTime? end = null)
        {
            Accountability accountability = new Accountability()
            {
                Commissioner = commissioner,
                Responsible = responsible,
                AccountabilityType = type
            };

            commissioner.Commissions.Add(accountability);
            responsible.Responsibilities.Add(accountability);

            return Task.FromResult(accountability);
        }

        public Task<AccountabilityType> CreateAccountabilityType(string name)
        {
            AccountabilityType type = _accountabilityTypes.SingleOrDefault(x => x.Description == name);
            if (type != null)
                return Task.FromResult(type);
            
            type = new AccountabilityType() { AccountabilityTypeId = 1, Description = name };
            AccountabilityType highestAccTypeId = _accountabilityTypes.OrderByDescending(x => x.AccountabilityTypeId).FirstOrDefault();
            if (highestAccTypeId != null)
                type.AccountabilityTypeId = highestAccTypeId.AccountabilityTypeId + 1;

            _accountabilityTypes.Add(type);

            return Task.FromResult(type);
        }

        public Task<Party> CreateParty(string name, string legalId)
        {
            Party party = new Party() { PartyId = 1, Name = name, LegalId = legalId, Commissions = new List<Accountability>(), Responsibilities = new List<Accountability>() };
            Party highestPartyId = _parties.OrderByDescending(x => x.PartyId).FirstOrDefault();
            if (highestPartyId != null)
                party.PartyId = highestPartyId.PartyId + 1;

            _parties.Add(party);
            return Task.FromResult(party);
        }

        public Task<AccountabilityType> GetAccountabilityType(string name)
        {
            return Task.FromResult(_accountabilityTypes.SingleOrDefault(x => x.Description == name));
        }

        public Task<ICollection<Party>> GetAllParties()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Party>> GetCommissioners(Party responsible, AccountabilityType type)
        {
            throw new NotImplementedException();
        }

        public Task<Party> GetParty(string legalId)
        {
            return Task.FromResult(_parties.SingleOrDefault(x => x.LegalId == legalId));
        }

        public Task<Party> GetParty(int partyId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Party>> GetResponsible(Party commissioner, AccountabilityType type)
        {
            throw new NotImplementedException();
        }

        public Task<TimePeriod> GetTimePeriod(int timePeriodId)
        {
            throw new NotImplementedException();
        }

        public Task<TimePeriod> SearchForPeriod(DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }
    }
}
