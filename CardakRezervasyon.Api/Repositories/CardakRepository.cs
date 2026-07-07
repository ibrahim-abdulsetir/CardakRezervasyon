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
    }
}