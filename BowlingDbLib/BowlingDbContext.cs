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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Accountability>().HasKey(a => new { a.AccountabilityTypeId, a.CommissionerId, a.ResponsibleId });

            builder.Entity<Accountability>().HasOne(a => a.Commissioner)
                .WithMany(r => r.Commissions)
                .HasForeignKey(a => a.CommissionerId);

            builder.Entity<Accountability>().HasOne(a => a.Responsible)
                .WithMany(r => r.Responsibilities)
                .HasForeignKey(a => a.ResponsibleId);

            builder.Entity<LaneTimePeriod>().HasKey(ltp => new { ltp.LaneId, ltp.TimePeriodId });

            builder.Entity<LaneTimePeriod>().HasOne(ltp => ltp.Lane)
                .WithMany(l => l.LaneTimePeriods)
                .HasForeignKey(ltp => ltp.LaneId);
        }
    }
}