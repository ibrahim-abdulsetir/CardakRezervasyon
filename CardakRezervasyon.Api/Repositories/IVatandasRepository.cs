using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public interface IVatandasRepository
    {
        Task<Vatandas?> GetByEpostaAsync(string eposta);
        Task<Vatandas> AddAsync(Vatandas vatandas);

        Task InvalidateEskiKodlarAsync(int vatandasId);

        Task<DogrulamaKodu> AddKodAsync(DogrulamaKodu kod);
    }

}