using BowlingLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AccountabilityLib.Models;
using BowlingLib.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BowlingDbLib
{
    public class SqlBowlingRepository : IBowlingRepository
    {
        private readonly BowlingDbContext _context;

        public SqlBowlingRepository(BowlingDbContext context)
        {
            _context = context;
        }

        public Task<Competition> CreateCompetition(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Match> AddMatch(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return match;
        }

        public Task<Round> CreateRound(Match match)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Lane>> GetAllLanes()
        {
            throw new NotImplementedException();
        }

        public Task<Competition> GetCompetition(int competitionId)
        {
            throw new NotImplementedException();
        }

        public Task<Lane> GetLane(int laneId)
        {
            throw new NotImplementedException();
        }

        public Task<Match> GetMatch(int matchId)
        {
            throw new NotImplementedException();
        }

        public Task<Round> GetRound(int roundId)
        {
            throw new NotImplementedException();
        }

        public Task<Series> GetSeries(int seriesId)
        {
            throw new NotImplementedException();
        }

        public async Task<Match> CreateEmptyMatch()
        {
            Match match = new Match();
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return match;
        }

        public async Task<ICollection<Match>> GetMatchesByYear(int year)
        {
            return await _context.Matches.Where(x => x.PlayedOn.Year == year).ToListAsync();
        }
    }
}
