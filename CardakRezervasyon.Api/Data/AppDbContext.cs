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
    }
}