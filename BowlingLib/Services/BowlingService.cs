using AccountabilityLib.Models;
using BowlingLib.Interfaces;
using BowlingLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingLib.Services
{
    public class BowlingService
    {
        private readonly IBowlingRepository _bowlingRepository;

        public BowlingService(IBowlingRepository bowlingRepository)
        {
            _bowlingRepository = bowlingRepository;
        }

        public async Task<Match> CreateStandardMatch(ICollection<Party> players, ICollection<Lane> lanes, Competition competition = null)
        {
            if (players.Count < 1 || lanes.Count < 1)
                return null;

            List<Round> allRounds = new List<Round>();
            for (int i = 0; i < 3; i++)
            {
                List<Round> rounds = new List<Round>();
                foreach (var lane in lanes)
                {
                    rounds.Add(new Round() { Lane = lane, Series = new List<Series>() });
                }

                //add players to lanes
                int roundIndex = 0;
                foreach (Party player in players)
                {
                    rounds[roundIndex].Series.Add(new Series() { Player = player });
                    roundIndex++;

                    if (roundIndex >= rounds.Count)
                        roundIndex = 0;
                }
                allRounds.AddRange(rounds);
            }

            Match retVal = new Match() { Rounds = allRounds.Where(x => x.Series.Any()).ToList() };
            return await _bowlingRepository.AddMatch(retVal);
        }

        public async Task<Party> GetMatchWinner(int matchId)
        {
            Match match = await _bowlingRepository.GetMatch(matchId);
            List<Series> allSeries = new List<Series>();

            foreach (Round round in match.Rounds)
                allSeries.AddRange(round.Series);

            return allSeries.GroupBy(x => x.Player).OrderByDescending(x => x.Sum(y => y.Score)).FirstOrDefault().Key;
        }

        public Party GetMatchWinner(Match match)
        {
            List<Series> allSeries = new List<Series>();

            foreach (Round round in match.Rounds)
                allSeries.AddRange(round.Series);

            return allSeries.GroupBy(x => x.Player).OrderByDescending(x => x.Sum(y => y.Score)).FirstOrDefault().Key;
        }

        public async Task<Party> GetChampionOfYear(int year)
        {
            ICollection<Match> matches = await _bowlingRepository.GetMatchesByYear(year);
            List<Party> winners = new List<Party>();

            foreach (Match match in matches)
            {
                winners.Add(GetMatchWinner(match));
            }


            return winners.GroupBy(x => x)
                .Select(x => new { Value = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault().Value;
        }
    }
}
