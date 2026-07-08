using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public interface IRezervasyonRepository
    {
        Task<Cardak?> GetCardakByIdAsync(int cardakId);

        Task<bool> HasOverlapAsync(int cardakId, DateTime baslangic, DateTime bitis);

        Task<Rezervasyon> AddAsync(Rezervasyon rezervasyon);
    }
}