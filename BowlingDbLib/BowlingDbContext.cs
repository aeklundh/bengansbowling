using AccountabilityLib.Models;
using BowlingLib.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlingDbLib
{
    public class BowlingDbContext : DbContext
    {
        public virtual DbSet<Accountability> Accountabilities { get; set; }
        public virtual DbSet<AccountabilityType> AccountabilityTypes { get; set; }
        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<Lane> Lanes { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<TimePeriod> TimePeriods { get; set; }

        public BowlingDbContext(DbContextOptions<BowlingDbContext> options) : base(options)
        { }
    }
}