using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Repositories
{
    public interface IMesireAlaniRepository
    {
        Task<List<MesireAlani>> GetAllAsync();
        Task<MesireAlani?> GetByIdAsync(int id);
        Task<MesireAlani> AddAsync(MesireAlani mesireAlani);
    }
}