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
            var builder = new DbContextOptionsBuilder<BowlingDbContext>()
                .UseSqlServer("Server=.\\SQLEXPRESS;Database=BowlingByBengan;Trusted_Connection=True;");
            return new BowlingDbContext(builder.Options);
        }

        public BowlingDbContext CreateInMemoryDbContext(string databaseName)
        {
            var builder = new DbContextOptionsBuilder<BowlingDbContext>()
                .UseInMemoryDatabase(databaseName);
            BowlingDbContext context = new BowlingDbContext(builder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
