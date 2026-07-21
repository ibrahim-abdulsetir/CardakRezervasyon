using CardakRezervasyon.Api.DTOs.Vatandaslar;
using CardakRezervasyon.Api.Models.Entities;
using CardakRezervasyon.Api.Repositories;
using CardakRezervasyon.Api.Services.Email;
using Microsoft.AspNetCore.Identity;

namespace CardakRezervasyon.Api.Services
{
    public class VatandasService : IVatandasService
    {
        private readonly IVatandasRepository _repository;
        private readonly PasswordHasher<Vatandas> _passwordHasher;
        private readonly IEmailService _emailService;

        public VatandasService(IVatandasRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _passwordHasher = new PasswordHasher<Vatandas>();
            _emailService = emailService;
        }

        public async Task<(VatandasDto? Result, string? HataMesaji)> RegisterAsync(RegisterVatandasDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Ad) || string.IsNullOrWhiteSpace(dto.Soyad))
            {
                return (null, "Ad and Soyad cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(dto.Eposta))
            {
                return (null, "Eposta cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(dto.Parola) || dto.Parola.Length < 6)
            {
                return (null, "Parola must be at least 6 characters.");
            }

            var existing = await _repository.GetByEpostaAsync(dto.Eposta);
            if (existing != null)
            {
                return (null, "This email is already registered.");
            }

            var entity = new Vatandas
            {
                Ad = dto.Ad,
                Soyad = dto.Soyad,
                Eposta = dto.Eposta,
                Telefon = string.Empty,
                TcKimlikNo = string.Empty,
                AktifMi = true
            };

            // Hash the password — this must happen BEFORE saving, never store dto.Parola directly
            entity.ParolaHash = _passwordHasher.HashPassword(entity, dto.Parola);

            var saved = await _repository.AddAsync(entity);

            return (new VatandasDto
            {
                Id = saved.Id,
                Ad = saved.Ad,
                Soyad = saved.Soyad,
                Eposta = saved.Eposta
            }, null);
        }
        public async Task<(bool Basarili, string Mesaj)> LoginAsync(LoginDto dto)
        {
            const string genericHataMesaji = "Eposta veya parola hatali."; // deliberately vague, on purpose

            var vatandas = await _repository.GetByEpostaAsync(dto.Eposta);
            if (vatandas == null)
            {
                return (false, genericHataMesaji);
            }

            if (!vatandas.AktifMi)
            {
                return (false, genericHataMesaji);
            }

            var dogrulamaSonucu = _passwordHasher.VerifyHashedPassword(vatandas, vatandas.ParolaHash, dto.Parola);
            if (dogrulamaSonucu == PasswordVerificationResult.Failed)
            {
                return (false, genericHataMesaji);
            }

            // Password correct — invalidate old codes, generate a new one
            await _repository.InvalidateEskiKodlarAsync(vatandas.Id);

            var kod = new Random().Next(0, 1000000).ToString("D6"); // always exactly 6 digits, zero-padded

            var dogrulamaKodu = new DogrulamaKodu
            {
                VatandasId = vatandas.Id,
                Kod = kod,
                SonGecerlilikZamani = DateTime.UtcNow.AddMinutes(4),
                KullanildiMi = false
            };

            await _repository.AddKodAsync(dogrulamaKodu);

            await _emailService.SendDogrulamaKoduAsync(vatandas.Eposta, kod);

            return (true, "Dogrulama kodu epostaniza gonderildi.");
        }
    }
}