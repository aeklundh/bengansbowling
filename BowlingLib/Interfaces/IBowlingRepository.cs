using AccountabilityLib.Models;
using BowlingLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingLib.Interfaces
{
    public interface IBowlingRepository
    {
        Task<Competition> GetCompetition(int competitionId);
        Task<Match> GetMatch(int matchId);
        Task<ICollection<Match>> GetMatchesByYear(int year);
        Task<Round> GetRound(int roundId);
        Task<Series> GetSeries(int seriesId);
        Task<ICollection<Lane>> GetAllLanes();
        Task<Lane> GetLane(int laneId);

        Task<Competition> CreateCompetition(string name);
        Task<Match> CreateEmptyMatch();
        Task<Match> AddMatch(Match match);
        Task<Round> CreateRound(Match match);
        Task<Lane> CreateLane(string name);
    }
}
