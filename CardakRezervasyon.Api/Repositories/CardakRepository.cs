using Microsoft.EntityFrameworkCore;
using CardakRezervasyon.Api.Data;
using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public class CardakRepository : ICardakRepository
    {
        private readonly AppDbContext _context;

        public CardakRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Cardak> Items, int TotalCount)> GetPagedByParkAsync(
            int mesireAlaniId, int page, int pageSize, string? blok, int? minKapasite)
        {
            var query = _context.Cardaklar
                .AsNoTracking()
                .Where(c => c.MesireAlaniId == mesireAlaniId);

            if (!string.IsNullOrEmpty(blok))
            {
                query = query.Where(c => c.Blok == blok);
            }

            if (minKapasite.HasValue)
            {
                query = query.Where(c => c.Kapasite >= minKapasite.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(c => c.Numara)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<MesireAlani?> GetParkByIdAsync(int mesireAlaniId)
        {
            return await _context.MesireAlanlari
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == mesireAlaniId);
        }

        public async Task<Cardak> AddAsync(Cardak cardak)
        {
            _context.Cardaklar.Add(cardak);
            await _context.SaveChangesAsync();
            return cardak;
        }
        public async Task<(int AktifCount, int DoluCount)> GetBoslukSayilariAsync(
    int mesireAlaniId, DateTime baslangic, DateTime bitis)
        {
            var aktifDurumlar = new[] { RezervasyonDurumu.Beklemede, RezervasyonDurumu.Onaylandi };

            // Step 1: how many active çardaks does this park have in total?
            var aktifCount = await _context.Cardaklar
                .AsNoTracking()
                .Where(c => c.MesireAlaniId == mesireAlaniId && c.AktifMi)
                .CountAsync();

            // Step 2: how many DISTINCT çardaks (in this park) have an overlapping
            // active reservation in the requested time range?
            var doluCount = await _context.Rezervasyonlar
                .AsNoTracking()
                .Where(r => r.Cardak.MesireAlaniId == mesireAlaniId)
                .Where(r => aktifDurumlar.Contains(r.Durum))
                .Where(r => baslangic < r.BitisZamani && bitis > r.BaslangicZamani)
                .Select(r => r.CardakId)
                .Distinct()
                .CountAsync();

            return (aktifCount, doluCount);
        }
    }
}