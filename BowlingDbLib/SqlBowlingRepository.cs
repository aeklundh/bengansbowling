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

        public async Task<Competition> CreateCompetition(string name)
        {
            Competition competition = new Competition() { Name = name, Matches = new List<Match>() };
            _context.Competitions.Add(competition);
            await _context.SaveChangesAsync();
            return competition;
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

        public async Task<ICollection<Lane>> GetAllLanes()
        {
            return await _context.Lanes.ToListAsync();
        }

        public async Task<Competition> GetCompetition(int competitionId)
        {
            return await _context.Competitions.SingleOrDefaultAsync(x => x.CompetitionId == competitionId);
        }

        public async Task<Lane> GetLane(int laneId)
        {
            return await _context.Lanes.SingleOrDefaultAsync(x => x.LaneId == laneId);
        }

        public async Task<Match> GetMatch(int matchId)
        {
            return await _context.Matches.SingleOrDefaultAsync(x => x.MatchId == matchId);
        }

        public async Task<Round> GetRound(int roundId)
        {
            return await _context.Rounds.SingleOrDefaultAsync(x => x.RoundId == roundId);
        }

        public async Task<Series> GetSeries(int seriesId)
        {
            return await _context.Series.SingleOrDefaultAsync(x => x.SeriesId == seriesId);
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

        public async Task<Lane> CreateLane(string name)
        {
            Lane lane = new Lane() { Name = name };
            _context.Lanes.Add(lane);
            await _context.SaveChangesAsync();
            return lane;
        }
    }
}
