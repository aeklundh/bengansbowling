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

        public BowlingDbContext CreateInMemoryDbContext()
        {
            var builder = new DbContextOptionsBuilder<BowlingDbContext>()
                .UseInMemoryDatabase("database");
            BowlingDbContext context = new BowlingDbContext(builder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
