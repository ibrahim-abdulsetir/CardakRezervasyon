using Microsoft.AspNetCore.Identity;
using CardakRezervasyon.Api.DTOs.Vatandaslar;
using CardakRezervasyon.Api.Models.Entities;
using CardakRezervasyon.Api.Repositories;

namespace CardakRezervasyon.Api.Services
{
    public class VatandasService : IVatandasService
    {
        private readonly IVatandasRepository _repository;
        private readonly PasswordHasher<Vatandas> _passwordHasher;

        public VatandasService(IVatandasRepository repository)
        {
            _repository = repository;
            _passwordHasher = new PasswordHasher<Vatandas>();
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
    }
}