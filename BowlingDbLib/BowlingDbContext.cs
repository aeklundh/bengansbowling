using AccountabilityLib.Models;
using BowlingLib.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlingDbLib
{
    public class BowlingDbContext : DbContext
    {
        public DbSet<Accountability> Accountabilities { get; set; }
        public DbSet<AccountabilityType> AccountabilityTypes { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Lane> Lanes { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<TimePeriod> TimePeriods { get; set; }

        public BowlingDbContext(DbContextOptions<BowlingDbContext> options) : base(options)
        { }
    }
}