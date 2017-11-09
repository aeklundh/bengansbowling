using BowlingLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BowlingLib.Models;
using System.Threading.Tasks;
using System.Linq;
using AccountabilityLib.Models;

namespace BowlingUnitTestsLib.Repositories
{
    public class FakeBowlingRepository : IBowlingRepository
    {
        private List<Match> _matches { get; set; } = new List<Match>();
        private List<Competition> _competitions { get; set; } = new List<Competition>();
        private List<Lane> _lanes { get; set; } = new List<Lane>();
        private List<TimePeriod> _timePeriods { get; set; } = new List<TimePeriod>();

        public Task AddLane(Lane lane)
        {
            _lanes.Add(lane);
            return Task.CompletedTask;
        }

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
            Lane newLane = new Lane() { LaneId = 1, Name = name };
            Lane previousTopId = _lanes.OrderByDescending(x => x.LaneId).FirstOrDefault();
            if (previousTopId != null)
                newLane.LaneId = previousTopId.LaneId + 1;

            return Task.FromResult(newLane);
        }

        public Task<Round> CreateRound(Match match)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Lane>> GetAllLanes()
        {
            return Task.FromResult((ICollection<Lane>)_lanes);
        }

        public Task<Competition> GetCompetition(int competitionId)
        {
            return Task.FromResult(_competitions.FirstOrDefault(x => x.CompetitionId == competitionId));
        }

        public Task<Lane> GetLane(int laneId)
        {
            return Task.FromResult(_lanes.SingleOrDefault(x => x.LaneId == laneId));
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

        public Task<TimePeriod> GetTimePeriod(DateTime startTime, DateTime endTime)
        {
            return Task.FromResult(_timePeriods.SingleOrDefault(x => x.StartTime == startTime && x.EndTime == endTime));
        }

        public Task UpdateLane(Lane lane)
        {
            return Task.CompletedTask;
        }

        public Task UpdateSeries(Series series)
        {
            return Task.CompletedTask;
        }
    }
}
