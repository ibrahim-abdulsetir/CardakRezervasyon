using CardakRezervasyon.Api.DTOs.Rezervasyonlar;

namespace CardakRezervasyon.Api.Services
{
    public interface IRezervasyonService
    {
        Task<(RezervasyonDetailDto? Result, string? HataMesaji)> CreateAsync(CreateRezervasyonDto dto);
    }
}