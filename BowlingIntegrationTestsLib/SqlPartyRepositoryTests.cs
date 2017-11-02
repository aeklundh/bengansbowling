using BowlingDbLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BowlingIntegrationTestsLib
{
    public class SqlPartyRepositoryTests
    {
        [Fact]
        public void CanCreateNewParty()
        {
            using (BowlingDbContext context = new BowlingDbContextFactory().CreateInMemoryDbContext("SqlPartyRepositoryTestsDb"))
            {
                //Assemble
                SqlPartyRepository sut = new SqlPartyRepository(context);

                //Act
                sut.CreateParty("aaa", "AAA").Wait();

                //Assert
                Assert.NotNull(sut.GetParty("AAA").Result);
            }
        }
    }
}
