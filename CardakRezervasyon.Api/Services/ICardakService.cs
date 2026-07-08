using CardakRezervasyon.Api.DTOs;
using CardakRezervasyon.Api.DTOs.Cardaklar;
using CardakRezervasyon.Api.DTOs.MesireAlanlari;

namespace CardakRezervasyon.Api.Services
{
    public interface ICardakService
    {
        Task<PagedResult<CardakListDto>?> GetPagedByParkAsync(
            int mesireAlaniId, int page, int pageSize, string? blok, int? minKapasite);

        Task<CardakListDto?> CreateAsync(int mesireAlaniId, CreateCardakDto dto);
        Task<BoslukDto?> GetBoslukAsync(int mesireAlaniId, DateTime baslangic, DateTime bitis);
    }
}