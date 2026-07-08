using CardakRezervasyon.Api.DTOs;
using CardakRezervasyon.Api.DTOs.Rezervasyonlar;
using CardakRezervasyon.Api.Models.Entities;
using CardakRezervasyon.Api.Repositories;


namespace CardakRezervasyon.Api.Services
{
    public class RezervasyonService : IRezervasyonService
    {
        private readonly IRezervasyonRepository _repository;

        public RezervasyonService(IRezervasyonRepository repository)
        {
            _repository = repository;
        }

        public async Task<(RezervasyonDetailDto? Result, string? HataMesaji)> CreateAsync(CreateRezervasyonDto dto)
        {
            // Rule 1: çardak must exist and be active
            var cardak = await _repository.GetCardakByIdAsync(dto.CardakId);
            if (cardak == null)
            {
                return (null, $"Çardak with id {dto.CardakId} not found.");
            }

            if (!cardak.AktifMi)
            {
                return (null, "This çardak is currently inactive/under maintenance.");
            }

            // Rule 2: cannot reserve in the past
            if (dto.BaslangicZamani < DateTime.Now)
            {
                return (null, "Cannot create a reservation in the past.");
            }

            // Rule 3: end time must be after start time (basic sanity check)
            if (dto.BitisZamani <= dto.BaslangicZamani)
            {
                return (null, "End time must be after start time.");
            }

            // Rule 4: no overlap with existing active reservations
            var overlap = await _repository.HasOverlapAsync(dto.CardakId, dto.BaslangicZamani, dto.BitisZamani);
            if (overlap)
            {
                return (null, "This çardak is already reserved for an overlapping time.");
            }

            // All checks passed — create the reservation
            var entity = new Rezervasyon
            {
                CardakId = dto.CardakId,
                VatandasId = dto.VatandasId,
                BaslangicZamani = dto.BaslangicZamani,
                BitisZamani = dto.BitisZamani,
                KisiSayisi = dto.KisiSayisi,
                Not = dto.Not,
                Durum = RezervasyonDurumu.Beklemede
            };

            var saved = await _repository.AddAsync(entity);

            var resultDto = new RezervasyonDetailDto
            {
                Id = saved.Id,
                CardakId = saved.CardakId,
                VatandasId = saved.VatandasId,
                BaslangicZamani = saved.BaslangicZamani,
                BitisZamani = saved.BitisZamani,
                KisiSayisi = saved.KisiSayisi,
                Durum = saved.Durum,
                Not = saved.Not
            };

            return (resultDto, null);
        }
        public async Task<RezervasyonDetailDto?> GetByIdAsync(int id)
        {
            var rezervasyon = await _repository.GetByIdAsync(id);

            if (rezervasyon == null)
            {
                return null;
            }

            return new RezervasyonDetailDto
            {
                Id = rezervasyon.Id,
                CardakId = rezervasyon.CardakId,
                VatandasId = rezervasyon.VatandasId,
                BaslangicZamani = rezervasyon.BaslangicZamani,
                BitisZamani = rezervasyon.BitisZamani,
                KisiSayisi = rezervasyon.KisiSayisi,
                Durum = rezervasyon.Durum,
                Not = rezervasyon.Not
            };
        }

        public async Task<PagedResult<RezervasyonDetailDto>> GetPagedAsync(
            int page, int pageSize, int? cardakId, RezervasyonDurumu? durum)
        {
            var (items, totalCount) = await _repository.GetPagedAsync(page, pageSize, cardakId, durum);

            return new PagedResult<RezervasyonDetailDto>
            {
                Items = items.Select(r => new RezervasyonDetailDto
                {
                    Id = r.Id,
                    CardakId = r.CardakId,
                    VatandasId = r.VatandasId,
                    BaslangicZamani = r.BaslangicZamani,
                    BitisZamani = r.BitisZamani,
                    KisiSayisi = r.KisiSayisi,
                    Durum = r.Durum,
                    Not = r.Not
                }).ToList(),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}