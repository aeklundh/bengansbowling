using AccountabilityLib.Models;
using BowlingDbLib;
using BowlingLib.Models;
using BowlingLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace BowlingIntegrationTestsLib
{
    public class BowlingSustemTests
    {
        [Fact]
        public void CanCreateDrawScenarioFromScratch()
        {
            //Assemble
            BowlingDbContext context = new BowlingDbContextFactory().CreateInMemoryDbContext("BowlingServiceTestsDb");
            SqlPartyRepository partyRepository = new SqlPartyRepository(context);
            SqlBowlingRepository bowlingRepository = new SqlBowlingRepository(context);
            BowlingSystem bowlingService = new BowlingSystem(bowlingRepository);

            List<Party> players = new List<Party>
            {
                partyRepository.CreateParty("aaa", "AAA").Result,
                partyRepository.CreateParty("bbb", "BBB").Result
            };

            List<Lane> lanes = new List<Lane>
            {
                bowlingRepository.CreateLane("Lane A1").Result
            };

            Competition competition = bowlingRepository.CreateCompetition("The incredible pin smackdown!").Result;

            //Act
            Match match1 = bowlingService.CreateStandardMatch(players, lanes, new DateTime(), competition).Result;
            Match match2 = bowlingService.CreateStandardMatch(players, lanes, new DateTime(), competition).Result;

            short matchScore = 0;
            foreach (Round round in match1.Rounds) //player bbb wins
            {
                for (int i = 0; i < round.Series.Count; i++)
                {
                    bowlingService.RegisterScores(round.Series.ElementAt(i).SeriesId, matchScore).Wait();
                    matchScore++;
                }
            }
            matchScore = 5000;
            foreach (Round round in match2.Rounds) //player aaa wins
            {
                for (int i = 0; i < round.Series.Count; i++)
                {
                    bowlingService.RegisterScores(round.Series.ElementAt(i).SeriesId, matchScore).Wait();
                    matchScore--;
                }
            }

            Party winner1 = bowlingService.GetMatchWinner(match1.MatchId).Result;
            Party winner2 = bowlingService.GetMatchWinner(match2.MatchId).Result;

            Party competitionWinner = bowlingService.GetCompetitionWinner(competition.CompetitionId).Result;

            //Assert
            Assert.Equal(players[1].Name, winner1.Name);
            Assert.Equal(players[0].Name, winner2.Name);
            Assert.Null(competitionWinner);
        }
    }
}
