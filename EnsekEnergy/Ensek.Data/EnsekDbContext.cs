using Ensek.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ensek.Data
{
    public class EnsekDbContext : DbContext
    {
        private readonly string _sqlConnetion;
        public EnsekDbContext(DbContextOptions<EnsekDbContext> options) : base(options)
        {

        }
        public EnsekDbContext(string sqlConnetion)
        {
            _sqlConnetion = sqlConnetion;
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_sqlConnetion);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingMSSqlServerSchema(modelBuilder);
        }
        private void OnModelCreatingMSSqlServerSchema(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Account>(entity =>
            {
                entity.HasKey(x => x.AccountId);
                entity.Property(x => x.AccountId).ValueGeneratedNever();
                entity.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.LastName).HasMaxLength(100);
            });

            modelBuilder.Entity<Models.MeterReading>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.MeterReadValue);
                entity.Property(x => x.MeterReadingDateTime).IsRequired();
                entity.HasOne(x => x.Account)
                .WithMany(x => x.MeterReading)
                .HasForeignKey(x => x.AccountId).IsRequired();
            });
        }
    }
}