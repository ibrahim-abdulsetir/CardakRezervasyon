using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public interface IVatandasRepository
    {
        Task<Vatandas?> GetByEpostaAsync(string eposta);
        Task<Vatandas> AddAsync(Vatandas vatandas);

        Task InvalidateEskiKodlarAsync(int vatandasId);

        Task<DogrulamaKodu> AddKodAsync(DogrulamaKodu kod);
        Task<DogrulamaKodu?> GetGecerliKoduAsync(int vatandasId, string kod);

        Task UpdateAsync(Vatandas vatandas);

        Task MarkKoduKullanildiAsync(DogrulamaKodu kod);
    }

}