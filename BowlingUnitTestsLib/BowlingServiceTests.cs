using AccountabilityLib.Models;
using BowlingLib.Interfaces;
using BowlingLib.Models;
using BowlingLib.Services;
using BowlingUnitTestsLib.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BowlingUnitTestsLib
{
    public class BowlingServiceTests
    {
        [Theory]
        [InlineData(2, 2, 1, 2)]
        [InlineData(3, 2, 2, 2)]
        [InlineData(2, 4, 1, 2)]
        public void StandardMatch(int playerAmount, int laneAmount, int expectedOnFirstLane, int expectedLanes)
        {
            //Assemble
            IBowlingRepository fakeProvider = new FakeBowlingRepository();
            BowlingService sut = new BowlingService(fakeProvider);
            ICollection<Party> players = new List<Party>();
            ICollection<Lane> lanes = new List<Lane>();

            for (int i = 0; i < playerAmount; i++)
                players.Add(new Party());

            for (int i = 0; i < laneAmount; i++)
                lanes.Add(new Lane());

            //Act
            Match result = sut.CreateStandardMatch(players, lanes).Result;

            //Assert
            Assert.Equal(result.Rounds.First().Series.Count, expectedOnFirstLane);
            Assert.Equal(result.Rounds.GroupBy(x => x.Lane).Count(), expectedLanes);
        }

        [Fact]
        public void CorrectWinnerInMatch()
        {
            //Assemble
            IBowlingRepository fakeProvider = new FakeBowlingRepository();
            Party player1 = new Party() { PartyId = 1 };
            Party player2 = new Party() { PartyId = 2 };
            Match match = new Match()
            {
                MatchId = 1,
                Rounds = new List<Round>() {
                    new Round() {
                        Series = new List<Series>() {
                            new Series() { Player = player1, Score = 20 },
                            new Series() { Player = player2, Score = 500 }
                        }
                    },
                    new Round() {
                        Series = new List<Series>() {
                            new Series() { Player = player1, Score = 50 },
                            new Series() { Player = player2, Score = 10 }
                        }
                    }
                }
            };
            fakeProvider.AddMatch(match);
            BowlingService sut = new BowlingService(fakeProvider);

            //Act
            Party result = sut.GetMatchWinner(1).Result;

            //Assert
            Assert.Same(result, player2);
        }
    }
}
