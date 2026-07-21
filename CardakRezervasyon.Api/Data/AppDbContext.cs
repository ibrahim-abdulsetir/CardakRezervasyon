using Microsoft.EntityFrameworkCore;
using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MesireAlani> MesireAlanlari { get; set; } = null!;
        public DbSet<Cardak> Cardaklar { get; set; } = null!;
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; } = null!;
        public DbSet<Vatandas> Vatandaslar { get; set; } = null!;
        public DbSet<BakimKapaliGun> BakimKapaliGunler { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cardak: (MesireAlaniId, Numara) must be unique —
            // you can't have two çardaks with the same number in the same park
            modelBuilder.Entity<Cardak>()
                .HasIndex(c => new { c.MesireAlaniId, c.Numara })
                .IsUnique();

            // Cardak: speeds up "give me active çardaks in this park" queries
            modelBuilder.Entity<Cardak>()
                .HasIndex(c => new { c.MesireAlaniId, c.AktifMi });

            // Rezervasyon: speeds up overlap-checking queries
            // (the most important index in the whole project)
            modelBuilder.Entity<Rezervasyon>()
                .HasIndex(r => new { r.CardakId, r.BaslangicZamani, r.BitisZamani });
            // Vatandas: Eposta must be unique — no two citizens can share the same email
            modelBuilder.Entity<Vatandas>()
                .HasIndex(v => v.Eposta)
                .IsUnique();
        }
    }

}