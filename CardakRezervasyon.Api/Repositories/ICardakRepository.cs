using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public interface ICardakRepository
    {
        Task<(List<Cardak> Items, int TotalCount)> GetPagedByParkAsync(
            int mesireAlaniId, int page, int pageSize, string? blok, int? minKapasite);

        Task<MesireAlani?> GetParkByIdAsync(int mesireAlaniId);

        Task<Cardak> AddAsync(Cardak cardak);
        Task<(int AktifCount, int DoluCount)> GetBoslukSayilariAsync(
    int mesireAlaniId, DateTime baslangic, DateTime bitis);
    }
}