using CardakRezervasyon.Api.DTOs.MesireAlanlari;
using CardakRezervasyon.Api.Models.Entities;
using CardakRezervasyon.Api.Repositories;

namespace CardakRezervasyon.Api.Services
{
    public class MesireAlaniService : IMesireAlaniService
    {
        private readonly IMesireAlaniRepository _repository;

        public MesireAlaniService(IMesireAlaniRepository repository)
        {
            _repository = repository;
        }
 
        public async Task<List<MesireAlaniListDto>> GetAllAsync()
        {
         

            var alanlar = await _repository.GetAllAsync();

            return alanlar.Select(a => new MesireAlaniListDto
            {
                Id = a.Id,
                Ad = a.Ad,
                Mahalle = a.Mahalle,
                AktifMi = a.AktifMi
            }).ToList();
        }

        public async Task<MesireAlaniDetailDto?> GetByIdAsync(int id)
        {
            var alan = await _repository.GetByIdAsync(id);

            if (alan == null)
            {
                return null;
            }

            return new MesireAlaniDetailDto
            {
                Id = alan.Id,
                Ad = alan.Ad,
                Aciklama = alan.Aciklama,
                Mahalle = alan.Mahalle,
                AcilisSaati = alan.AcilisSaati,
                KapanisSaati = alan.KapanisSaati,
                AktifMi = alan.AktifMi,
                ToplamCardakSayisi = alan.Cardaklar.Count,
                AktifCardakSayisi = alan.Cardaklar.Count(c => c.AktifMi)
            };
        }
        public async Task<(MesireAlaniDetailDto? Result, string? HataMesaji)> CreateAsync(CreateMesireAlaniDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Ad))
            {
                return (null, "Ad (park name) cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(dto.Mahalle))
            {
                return (null, "Mahalle cannot be empty.");
            }

            if (dto.KapanisSaati <= dto.AcilisSaati)
            {
                return (null, "KapanisSaati must be after AcilisSaati.");
            }

            var entity = new MesireAlani
            {
                Ad = dto.Ad,
                Aciklama = dto.Aciklama,
                Mahalle = dto.Mahalle,
                AcilisSaati = dto.AcilisSaati,
                KapanisSaati = dto.KapanisSaati,
                AktifMi = true
            };

            var saved = await _repository.AddAsync(entity);

            var resultDto = new MesireAlaniDetailDto
            {
                Id = saved.Id,
                Ad = saved.Ad,
                Aciklama = saved.Aciklama,
                Mahalle = saved.Mahalle,
                AcilisSaati = saved.AcilisSaati,
                KapanisSaati = saved.KapanisSaati,
                AktifMi = saved.AktifMi,
                ToplamCardakSayisi = 0,
                AktifCardakSayisi = 0
            };

            return (resultDto, null);
        }
    }
}