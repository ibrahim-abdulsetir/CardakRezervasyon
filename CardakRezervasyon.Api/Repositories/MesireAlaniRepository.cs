using Microsoft.EntityFrameworkCore;
using CardakRezervasyon.Api.Data;
using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public class MesireAlaniRepository : IMesireAlaniRepository
    {
        private readonly AppDbContext _context;

        public MesireAlaniRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MesireAlani>> GetAllAsync()
        {
            return await _context.MesireAlanlari
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MesireAlani?> GetByIdAsync(int id)
        {
            return await _context.MesireAlanlari
                .AsNoTracking()
                .Include(m => m.Cardaklar)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}