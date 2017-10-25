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
    }
}
