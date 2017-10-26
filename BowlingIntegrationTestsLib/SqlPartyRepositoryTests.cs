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
            //Assemble
            BowlingDbContext context = new BowlingDbContextFactory().CreateDbContext(new string[] { });
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            SqlPartyRepository sut = new SqlPartyRepository(context);

            //Act
            sut.CreateParty("aaa", "AAA").Wait();

            //Assert
            Assert.NotNull(sut.GetParty("AAA").Result);
        }
    }
}
