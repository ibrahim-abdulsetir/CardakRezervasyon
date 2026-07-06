using CardakRezervasyon.Api.DTOs.MesireAlanlari;
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
    }
}