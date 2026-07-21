using Microsoft.EntityFrameworkCore;
using CardakRezervasyon.Api.Data;
using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public class VatandasRepository : IVatandasRepository
    {
        private readonly AppDbContext _context;

        public VatandasRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Vatandas?> GetByEpostaAsync(string eposta)
        {
            return await _context.Vatandaslar
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Eposta == eposta);
        }

        public async Task<Vatandas> AddAsync(Vatandas vatandas)
        {
            _context.Vatandaslar.Add(vatandas);
            await _context.SaveChangesAsync();
            return vatandas;
        }
    }
}