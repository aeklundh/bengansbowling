using AccountabilityLib.Models;
using BowlingLib.Interfaces;
using BowlingLib.Models;
using BowlingLib.Services;
using BowlingUnitTestsLib.Repositories;
using BowlingUnitTestsLib.Utilities;
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
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(1, new DateTime(), new List<Party>() { player1, player2 }));
            BowlingService sut = new BowlingService(fakeProvider);

            //Act
            Party result = sut.GetMatchWinner(1).Result;

            //Assert
            Assert.Same(result, player2);
        }

        [Fact]
        public void CorrectChampionOfYear()
        {
            //Assemble
            IBowlingRepository fakeProvider = new FakeBowlingRepository();
            List<Party> players = new List<Party>()
            {
                new Party() { Name = "Expected third" },
                new Party() { Name = "Expected second" },
                new Party() { Name = "Expected winner" }
            };

            List<Party> playersFromOtherYears = new List<Party>()
            {
                new Party() { Name = "Expected wrong year" },
                new Party() { Name = "Expected wrong year" },
                new Party() { Name = "Expected wrong year" },
                new Party() { Name = "Expected wrong year" }
            };

            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(1, new DateTime(1994, 01, 01), players));
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(2, new DateTime(1994, 11, 01), players));
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(3, new DateTime(1995, 05, 29), players));
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(3, new DateTime(1995, 05, 29), playersFromOtherYears));
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(3, new DateTime(1995, 05, 29), playersFromOtherYears));
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(3, new DateTime(1995, 05, 29), playersFromOtherYears));

            BowlingService sut = new BowlingService(fakeProvider);

            //Act
            Party result = sut.GetChampionOfYear(1994).Result;

            //Assert
            Assert.Equal("Expected winner", result.Name);
        }
    }
}
