using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroupCoursework.Models
{
    public class DatabaseContext : IdentityDbContext

    {
        public DbSet<Actor> Actor { get; set; }
        public DbSet<CastMember> CastMember { get; set; }
        public DbSet<DVDCategory> DVDCategory { get; set; }
        public DbSet<DVDCopy> DVDCopy{ get; set; }
        public DbSet<DVDTitle> DVDTitle { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<LoanType> LoanType { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipCategory> MembershipCategory { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public DbSet<Studio> Studio { get; set; }
        public DbSet<User> User { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CastMember>().HasKey(table => new
            {
                table.DVDNumber,
                table.ActorNumber
            });
            base.OnModelCreating(builder);
        }
    }

}
