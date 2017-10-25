using AccountabilityLib.Models;
using BowlingLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingUnitTestsLib.Utilities
{
    public static class BowlingTestUtility
    {
        //the last player gets the highest score in every round
        public static Match CreateSampleMatch(int matchId, DateTime playedOn, List<Party> players)
        {
            Match match = new Match()
            {
                MatchId = matchId,
                Rounds = new List<Round>()
                {
                    new Round() { Series = new List<Series>() },
                    new Round() { Series = new List<Series>() }
                },
                PlayedOn = playedOn
            };

            for (short i = 0; i < players.Count; i++)
            {
                foreach (Round round in match.Rounds)
                    round.Series.Add(new Series() { Player = players[i], Score = i });
            }

            return match;
        }
    }
}
