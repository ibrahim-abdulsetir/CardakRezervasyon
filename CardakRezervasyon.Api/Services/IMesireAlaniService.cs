using CardakRezervasyon.Api.DTOs.MesireAlanlari;

namespace CardakRezervasyon.Api.Services
{
    public interface IMesireAlaniService
    {
        Task<List<MesireAlaniListDto>> GetAllAsync();
        Task<MesireAlaniDetailDto?> GetByIdAsync(int id);
    }
}