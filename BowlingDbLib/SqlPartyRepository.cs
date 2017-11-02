using AccountabilityLib.Interfaces;
using System;
using AccountabilityLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BowlingDbLib
{
    public class SqlPartyRepository : IPartyRepository
    {
        private readonly BowlingDbContext _context;

        public SqlPartyRepository(BowlingDbContext context)
        {
            _context = context;
        }

        public async void CreateAccountability(Party commissioner, Party responsible, AccountabilityType type, DateTime? start = null, DateTime? end = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Party> CreateParty(string name, string legalId)
        {
            Party exists = await _context.Parties.SingleOrDefaultAsync(x => x.LegalId == legalId);
            if (exists != null)
                return exists;

            Party newParty = new Party() { Name = name, LegalId = legalId };
            _context.Parties.Add(newParty);
            await _context.SaveChangesAsync();
            return newParty;
        }

        public async Task<ICollection<Party>> GetAllParties()
        {
            return await _context.Parties.ToListAsync();
        }

        public async Task<ICollection<Party>> GetCommissioners(Party responsible, AccountabilityType type)
        {
            throw new NotImplementedException();
        }

        public async Task<Party> GetParty(string legalId)
        {
            return await _context.Parties.SingleOrDefaultAsync(x => x.LegalId == legalId);
        }

        public async Task<Party> GetParty(int partyId)
        {
            return await _context.Parties.SingleOrDefaultAsync(x => x.PartyId == partyId);
        }

        public async Task<ICollection<Party>> GetResponsible(Party commissioner, AccountabilityType type)
        {
            throw new NotImplementedException();
        }

        public async Task<TimePeriod> GetTimePeriod(int timePeriodId)
        {
            return await _context.TimePeriods.SingleOrDefaultAsync(x => x.TimePeriodId == timePeriodId);
        }

        public async Task<TimePeriod> SearchForPeriod(DateTime startTime, DateTime endTime)
        {
            return await _context.TimePeriods.Where(x => x.StartTime == startTime && x.EndTime == endTime).FirstOrDefaultAsync();
        }
    }
}
