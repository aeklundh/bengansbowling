using AccountabilityLib.Interfaces;
using AccountabilityLib.Models;
using BowlingDbLib;
using BowlingLib.Services;
using Xunit;

namespace BowlingIntegrationTestsLib
{
    public class SqlPartyRepositoryTests
    {
        private string _inMemoryDbName {
            get { return "SqlPartyRepositoryTestsDb"; }
        }

        [Fact]
        public void CanCreateNewParty()
        {
            using (BowlingDbContext context = new BowlingDbContextFactory().CreateInMemoryDbContext(_inMemoryDbName))
            {
                //Assemble
                SqlPartyRepository sut = new SqlPartyRepository(context);

                //Act
                sut.CreateParty("aaa", "AAA").Wait();

                //Assert
                Assert.NotNull(sut.GetParty("AAA").Result);
            }
        }

        [Fact]
        public void CanCreateAndGetPlayers()
        {
            using (BowlingDbContext context = new BowlingDbContextFactory().CreateInMemoryDbContext(_inMemoryDbName))
            {
                //Assemble
                IPartyRepository partyRepository = new SqlPartyRepository(context);
                partyRepository.CreateParty("bengan", "bengansLegalId");
                partyRepository.CreateAccountabilityType("Customer");
                PartySystem sut = new PartySystem(partyRepository, "bengansLegalId");

                //Act
                Party result = sut.RegisterNewPlayer("aaa", "AAA").Result;

                //Assert
                Assert.NotNull(result);
                Assert.True(result.Commissions.Count == 1);
            }
        }
    }
}
