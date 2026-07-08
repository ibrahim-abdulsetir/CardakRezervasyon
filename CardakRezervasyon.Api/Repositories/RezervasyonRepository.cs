using Microsoft.EntityFrameworkCore;
using CardakRezervasyon.Api.Data;
using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public class RezervasyonRepository : IRezervasyonRepository
    {
        private readonly AppDbContext _context;

        public RezervasyonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cardak?> GetCardakByIdAsync(int cardakId)
        {
            return await _context.Cardaklar
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cardakId);
        }

        public async Task<bool> HasOverlapAsync(int cardakId, DateTime baslangic, DateTime bitis)
        {
            var aktifDurumlar = new[] { RezervasyonDurumu.Beklemede, RezervasyonDurumu.Onaylandi };

            return await _context.Rezervasyonlar
                .AsNoTracking()
                .Where(r => r.CardakId == cardakId)
                .Where(r => aktifDurumlar.Contains(r.Durum))
                .AnyAsync(r => baslangic < r.BitisZamani && bitis > r.BaslangicZamani);
        }

        public async Task<Rezervasyon> AddAsync(Rezervasyon rezervasyon)
        {
            _context.Rezervasyonlar.Add(rezervasyon);
            await _context.SaveChangesAsync();
            return rezervasyon;
        }
    }
}