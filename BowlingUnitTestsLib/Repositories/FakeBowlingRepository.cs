using BowlingLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BowlingLib.Models;
using System.Threading.Tasks;

namespace BowlingUnitTestsLib.Repositories
{
    public class FakeBowlingRepository : IBowlingRepository
    {
        private List<Match> _matches { get; set; } = new List<Match>();

        public async Task<Match> AddMatch(Match match)
        {
            _matches.Add(match);
            return await Task.FromResult(match);
        }

        public Task<Competition> CreateCompetition(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Match> CreateEmptyMatch()
        {
            throw new NotImplementedException();
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
    }
}
