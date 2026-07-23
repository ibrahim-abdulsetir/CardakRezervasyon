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
        public async Task InvalidateEskiKodlarAsync(int vatandasId)
        {
            var eskiKodlar = await _context.DogrulamaKodlari
                .Where(k => k.VatandasId == vatandasId && !k.KullanildiMi)
                .ToListAsync();

            foreach (var kod in eskiKodlar)
            {
                kod.KullanildiMi = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<DogrulamaKodu> AddKodAsync(DogrulamaKodu kod)
        {
            _context.DogrulamaKodlari.Add(kod);
            await _context.SaveChangesAsync();
            return kod;
        }
        public async Task<DogrulamaKodu?> GetGecerliKoduAsync(int vatandasId, string kod)
        {
            return await _context.DogrulamaKodlari
                .Where(k => k.VatandasId == vatandasId && k.Kod == kod && !k.KullanildiMi)
                .OrderByDescending(k => k.OlusturmaTarihi)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Vatandas vatandas)
        {
            _context.Vatandaslar.Update(vatandas);
            await _context.SaveChangesAsync();
        }

        public async Task MarkKoduKullanildiAsync(DogrulamaKodu kod)
        {
            kod.KullanildiMi = true;
            await _context.SaveChangesAsync();
        }
    }
}