using CardakRezervasyon.Api.DTOs;
using CardakRezervasyon.Api.DTOs.Cardaklar;

namespace CardakRezervasyon.Api.Services
{
    public interface ICardakService
    {
        Task<PagedResult<CardakListDto>?> GetPagedByParkAsync(
            int mesireAlaniId, int page, int pageSize, string? blok, int? minKapasite);

        Task<CardakListDto?> CreateAsync(int mesireAlaniId, CreateCardakDto dto);
    }
}