using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public interface IRezervasyonRepository
    {
        Task<Cardak?> GetCardakByIdAsync(int cardakId);

        Task<bool> HasOverlapAsync(int cardakId, DateTime baslangic, DateTime bitis);

        Task<Rezervasyon> AddAsync(Rezervasyon rezervasyon);
        Task<Rezervasyon?> GetByIdAsync(int id);

        Task<(List<Rezervasyon> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize, int? cardakId, RezervasyonDurumu? durum);
    }
}