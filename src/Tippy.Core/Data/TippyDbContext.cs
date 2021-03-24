using Microsoft.EntityFrameworkCore;
using Tippy.Core.Models;

namespace Tippy.Core.Data
{
    public class TippyDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TippyDbContext(DbContextOptions<TippyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Token> Tokens { get; set; } = null!;
        public DbSet<FailedTransaction> FailedTransactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .Property(p => p.Chain)
                .HasConversion<string>();

            modelBuilder.Entity<Project>()
                .Property(p => p.IsActive)
                .HasDefaultValue(false);

            modelBuilder.Entity<Token>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tokens)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FailedTransaction>()
                .HasOne(t => t.Project)
                .WithMany(p => p.FailedTransactions)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
