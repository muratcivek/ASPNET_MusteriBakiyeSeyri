using ASPNETChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETChallenge.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Musteri> Musteriler { get; set; } = null!;
        public DbSet<Fatura> Faturalar { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Fatura>()
                .Property(f => f.FaturaTutari)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Fatura>()
                .HasOne(f => f.Musteri)
                .WithMany(m => m.Faturalar)
                .HasForeignKey(f => f.MusteriId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
