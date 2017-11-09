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
    public class BowlingSystemTests
    {
        [Theory]
        [InlineData(2, 2, 1, 2)]
        [InlineData(3, 2, 2, 2)]
        [InlineData(2, 4, 1, 2)]
        public void StandardMatch(int playerAmount, int laneAmount, int expectedOnFirstLane, int expectedLanes)
        {
            //Assemble
            IBowlingRepository fakeProvider = new FakeBowlingRepository();
            BowlingSystem sut = new BowlingSystem(fakeProvider);
            ICollection<Party> players = new List<Party>();
            ICollection<Lane> lanes = new List<Lane>();

            for (int i = 0; i < playerAmount; i++)
                players.Add(new Party());

            for (int i = 0; i < laneAmount; i++)
                lanes.Add(new Lane());

            //Act
            Match result = sut.CreateStandardMatch(players, lanes, new DateTime()).Result;

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
            BowlingSystem sut = new BowlingSystem(fakeProvider);

            //Act
            Party result = sut.GetMatchWinner(1).Result;

            //Assert
            Assert.Same(result, player2);
        }

        [Fact]
        public void CorrectWinCountChampionOfYear()
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

            BowlingSystem sut = new BowlingSystem(fakeProvider);

            //Act
            Party result = sut.GetWinCountChampionOfYear(1994).Result;

            //Assert
            Assert.Equal("Expected winner", result.Name);
        }

        [Fact]
        public void CorrectWinRateChampionOfYear()
        {
            //Assemble
            IBowlingRepository fakeProvider = new FakeBowlingRepository();
            Party winner = new Party() { Name = "Expected winner" };
            Party second = new Party() { Name = "Expected second" };
            Party third = new Party() { Name = "Expected third" };

            List<Party> playerList1 = new List<Party>() { third, second, winner };
            List<Party> playerList2 = new List<Party>() { winner, third, second};

            List<Party> playersFromOtherYears = new List<Party>()
            {
                new Party() { Name = "Expected wrong year" },
                new Party() { Name = "Expected wrong year" },
                new Party() { Name = "Expected wrong year" },
                new Party() { Name = "Expected wrong year" }
            };

            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(1, new DateTime(1994, 01, 01), playerList1)); //winner wins
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(2, new DateTime(1994, 11, 01), playerList1)); //winner wins
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(3, new DateTime(1994, 11, 01), playerList1)); //winner wins
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(4, new DateTime(1994, 11, 01), playerList2)); //
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(5, new DateTime(1995, 05, 29), playerList1));
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(6, new DateTime(1995, 05, 29), playersFromOtherYears));
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(7, new DateTime(1995, 05, 29), playersFromOtherYears));
            fakeProvider.AddMatch(BowlingTestUtility.CreateSampleMatch(8, new DateTime(1995, 05, 29), playersFromOtherYears));

            BowlingSystem sut = new BowlingSystem(fakeProvider);

            //Act
            Party result = sut.GetWinRateChampionOfYear(1994).Result;

            //Assert
            Assert.Equal("Expected winner", result.Name);
        }

        [Fact]
        public void CanRegisterScore()
        {
            //Assemble
            IBowlingRepository fakeProvider = new FakeBowlingRepository();
            Match match = new Match()
            {
                Rounds = new List<Round>()
                {
                    new Round() { Series = new List<Series>() { new Series() { SeriesId = 1 } } }
                }
            };
            fakeProvider.AddMatch(match);
            BowlingSystem sut = new BowlingSystem(fakeProvider);

            //Act
            sut.RegisterScores(1, 500).Wait();

            //Assert
            Assert.Equal(500, fakeProvider.GetSeries(1).Result.Score);
        }

        [Fact]
        public void CorrectWinnerInCompetition()
        {
            //Assemble
            IBowlingRepository fakeProvider = new FakeBowlingRepository();
            Party player1 = new Party() { PartyId = 1 };
            Party player2 = new Party() { PartyId = 2 };
            Competition competition = fakeProvider.CreateCompetition("test").Result;
            competition.Matches = new List<Match>() { BowlingTestUtility.CreateSampleMatch(1, new DateTime(), new List<Party>() { player1, player2 }, competition) };

            BowlingSystem sut = new BowlingSystem(fakeProvider);

            //Act
            Party result = sut.GetCompetitionWinner(1).Result;

            //Assert
            Assert.Same(player2, result);
        }

        [Fact]
        public void GetVacantLanesProperly()
        {
            //Assemble
            IBowlingRepository fakeProvider = new FakeBowlingRepository();
            BowlingSystem sut = new BowlingSystem(fakeProvider);
            DateTime start = new DateTime(1994, 01, 01, 12, 00, 00);
            DateTime end = new DateTime(1994, 01, 01, 13, 00, 00);
            TimePeriod bookedTime = new TimePeriod() { StartTime = start, EndTime = end };
            Lane aaa = new Lane() { LaneId = 1, Name = "aaa", LaneTimePeriods = new List<LaneTimePeriod>() {
                new LaneTimePeriod() { TimePeriod = bookedTime }
            }};
            Lane bbb = new Lane() { LaneId = 1, Name = "bbb", LaneTimePeriods = new List<LaneTimePeriod>() };
            Lane ccc = new Lane() { LaneId = 1, Name = "ccc", LaneTimePeriods = new List<LaneTimePeriod>() };
            fakeProvider.AddLane(aaa);
            fakeProvider.AddLane(bbb);
            fakeProvider.AddLane(ccc);

            //Act
            ICollection<Lane> result = sut.GetVacantLanes(start, end).Result;

            //Assert
            Assert.True(result.Count == 2);
        }

        [Fact]
        public void CanBookLane()
        {
            //Assemble
            IBowlingRepository fakeProvider = new FakeBowlingRepository();
            BowlingSystem sut = new BowlingSystem(fakeProvider);
            DateTime start = new DateTime(1994, 01, 01, 12, 00, 00);
            DateTime end = new DateTime(1994, 01, 01, 13, 00, 00);
            TimePeriod bookedTime = new TimePeriod() { StartTime = start, EndTime = end };
            Lane aaa = new Lane()
            {
                LaneId = 1,
                Name = "aaa",
                LaneTimePeriods = new List<LaneTimePeriod>()
                {
                    new LaneTimePeriod() { TimePeriod = bookedTime }
                }
            };
            Lane bbb = new Lane() { LaneId = 2, Name = "bbb", LaneTimePeriods = new List<LaneTimePeriod>() };
            fakeProvider.AddLane(aaa);
            fakeProvider.AddLane(bbb);

            //Act & assert
            Assert.False(sut.BookLane(1, start, end).Result);
            Assert.False(sut.BookLane(1, start.AddHours(-1), end).Result);
            Assert.True(sut.BookLane(1, new DateTime(), new DateTime()).Result);
            Assert.True(sut.BookLane(2, start, end).Result);
        }
    }
}
