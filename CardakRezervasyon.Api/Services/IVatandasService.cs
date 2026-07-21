using CardakRezervasyon.Api.DTOs.Vatandaslar;

namespace CardakRezervasyon.Api.Services
{
    public interface IVatandasService
    {
        Task<(VatandasDto? Result, string? HataMesaji)> RegisterAsync(RegisterVatandasDto dto);
        Task<(bool Basarili, string Mesaj)> LoginAsync(LoginDto dto);
    }
}