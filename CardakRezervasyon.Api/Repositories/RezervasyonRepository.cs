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
        public async Task<Rezervasyon?> GetByIdAsync(int id)
        {
            return await _context.Rezervasyonlar
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<(List<Rezervasyon> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize, int? cardakId, RezervasyonDurumu? durum)
        {
            var query = _context.Rezervasyonlar.AsNoTracking();

            if (cardakId.HasValue)
            {
                query = query.Where(r => r.CardakId == cardakId.Value);
            }

            if (durum.HasValue)
            {
                query = query.Where(r => r.Durum == durum.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.OlusturmaTarihi)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}