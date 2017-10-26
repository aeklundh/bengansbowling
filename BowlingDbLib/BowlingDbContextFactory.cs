using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingDbLib
{
    public class BowlingDbContextFactory : IDesignTimeDbContextFactory<BowlingDbContext>
    {
        public BowlingDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BowlingDbContext>().UseInMemoryDatabase("database");
            return new BowlingDbContext(builder.Options);
        }
    }
}
