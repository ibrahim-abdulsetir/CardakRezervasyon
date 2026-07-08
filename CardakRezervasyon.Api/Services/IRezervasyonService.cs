using CardakRezervasyon.Api.DTOs;
using CardakRezervasyon.Api.DTOs.Rezervasyonlar;
using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Services
{
    public interface IRezervasyonService
    {
        Task<(RezervasyonDetailDto? Result, string? HataMesaji)> CreateAsync(CreateRezervasyonDto dto);
        Task<RezervasyonDetailDto?> GetByIdAsync(int id);

        Task<PagedResult<RezervasyonDetailDto>> GetPagedAsync(
            int page, int pageSize, int? cardakId, RezervasyonDurumu? durum);
    }
}