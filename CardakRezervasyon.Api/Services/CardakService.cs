using CardakRezervasyon.Api.DTOs;
using CardakRezervasyon.Api.DTOs.Cardaklar;
using CardakRezervasyon.Api.DTOs.MesireAlanlari;
using CardakRezervasyon.Api.Models.Entities;
using CardakRezervasyon.Api.Repositories;

namespace CardakRezervasyon.Api.Services
{
    public class CardakService : ICardakService
    {
        private readonly ICardakRepository _repository;

        public CardakService(ICardakRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<CardakListDto>?> GetPagedByParkAsync(
            int mesireAlaniId, int page, int pageSize, string? blok, int? minKapasite)
        {
            var park = await _repository.GetParkByIdAsync(mesireAlaniId);
            if (park == null)
            {
                return null; // park doesn't exist — Controller will return 404
            }

            var (items, totalCount) = await _repository.GetPagedByParkAsync(
                mesireAlaniId, page, pageSize, blok, minKapasite);

            return new PagedResult<CardakListDto>
            {
                Items = items.Select(c => new CardakListDto
                {
                    Id = c.Id,
                    Numara = c.Numara,
                    Blok = c.Blok,
                    Kapasite = c.Kapasite,
                    MangalliMi = c.MangalliMi,
                    AktifMi = c.AktifMi
                }).ToList(),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<CardakListDto?> CreateAsync(int mesireAlaniId, CreateCardakDto dto)
        {
            var park = await _repository.GetParkByIdAsync(mesireAlaniId);
            if (park == null)
            {
                return null; // park doesn't exist — Controller will return 404
            }

            var entity = new Cardak
            {
                MesireAlaniId = mesireAlaniId,
                Numara = dto.Numara,
                Blok = dto.Blok,
                Kapasite = dto.Kapasite,
                MangalliMi = dto.MangalliMi,
                AktifMi = true
            };

            var saved = await _repository.AddAsync(entity);

            return new CardakListDto
            {
                Id = saved.Id,
                Numara = saved.Numara,
                Blok = saved.Blok,
                Kapasite = saved.Kapasite,
                MangalliMi = saved.MangalliMi,
                AktifMi = saved.AktifMi
            };
        }
        public async Task<BoslukDto?> GetBoslukAsync(int mesireAlaniId, DateTime baslangic, DateTime bitis)
        {
            var park = await _repository.GetParkByIdAsync(mesireAlaniId);
            if (park == null)
            {
                return null; // park doesn't exist — Controller will return 404
            }

            var (aktifCount, doluCount) = await _repository.GetBoslukSayilariAsync(mesireAlaniId, baslangic, bitis);

            return new BoslukDto
            {
                MesireAlaniId = mesireAlaniId,
                Baslangic = baslangic,
                Bitis = bitis,
                AktifCardakSayisi = aktifCount,
                DoluCardakSayisi = doluCount,
                BosCardakSayisi = aktifCount - doluCount
            };
        }
    }
}