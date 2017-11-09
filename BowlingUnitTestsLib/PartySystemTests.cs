using AccountabilityLib.Interfaces;
using AccountabilityLib.Models;
using BowlingLib.Services;
using BowlingUnitTestsLib.Repositories;
using Xunit;

namespace BowlingUnitTestsLib
{
    public class PartySystemTests
    {
        private string _accountabilityOrigin {
            get { return "bengansLegalId"; }
        }

        [Fact]
        public void PlayerRegistrationAccountabilityWorks()
        {
            //Assemble
            IPartyRepository fakeProvider = new FakePartyRepository();
            fakeProvider.CreateAccountabilityType("Customer");
            Party origin = fakeProvider.CreateParty("Bengan", _accountabilityOrigin).Result;
            PartySystem sut = new PartySystem(fakeProvider, _accountabilityOrigin);

            //Act
            Party result = sut.RegisterNewPlayer("aaa", "AAA").Result;

            //Assert
            Assert.True(origin.Commissions.Count == 0);
            Assert.True(origin.Responsibilities.Count == 1);
            Assert.True(result.Commissions.Count == 1);
            Assert.True(result.Responsibilities.Count == 0);
        }
    }
}
