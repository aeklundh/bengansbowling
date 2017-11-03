using BowlingLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BowlingLib.Models;
using System.Threading.Tasks;
using System.Linq;

namespace BowlingUnitTestsLib.Repositories
{
    public class FakeBowlingRepository : IBowlingRepository
    {
        private List<Match> _matches { get; set; } = new List<Match>();
        private List<Competition> _competitions { get; set; } = new List<Competition>();

        public async Task<Match> AddMatch(Match match)
        {
            _matches.Add(match);
            return await Task.FromResult(match);
        }

        public Task<Competition> CreateCompetition(string name)
        {
            int id = 1;
            if (_competitions.Any())
                id = _competitions.OrderByDescending(x => x.CompetitionId).First().CompetitionId + 1;

            Competition retVal = new Competition() { Name = name, CompetitionId = id };
            _competitions.Add(retVal);
            return Task.FromResult(retVal);
        }

        public Task<Match> CreateEmptyMatch()
        {
            throw new NotImplementedException();
        }

        public Task<Lane> CreateLane(string name)
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
            return Task.FromResult(_competitions.FirstOrDefault(x => x.CompetitionId == competitionId));
        }

        public Task<Lane> GetLane(int laneId)
        {
            throw new NotImplementedException();
        }

        public async Task<Match> GetMatch(int matchId)
        {
            return await Task.FromResult(_matches.SingleOrDefault(x => x.MatchId == matchId));
        }

        public async Task<ICollection<Match>> GetMatchesByYear(int year)
        {
            return await Task.FromResult(_matches.Where(x => x.PlayedOn.Year == year).ToList());
        }

        public Task<Round> GetRound(int roundId)
        {
            throw new NotImplementedException();
        }

        public Task<Series> GetSeries(int seriesId)
        {
            foreach (Match match in _matches)
            {
                Round round = match.Rounds.SingleOrDefault(x => x.Series.Any(y => y.SeriesId == seriesId));
                if (round != null)
                {
                    return Task.FromResult(round.Series.SingleOrDefault(x => x.SeriesId == seriesId));
                }
            }
            return Task.FromResult<Series>(null);
        }

        public Task UpdateSeries(Series series)
        {
            return Task.CompletedTask;
        }
    }
}
