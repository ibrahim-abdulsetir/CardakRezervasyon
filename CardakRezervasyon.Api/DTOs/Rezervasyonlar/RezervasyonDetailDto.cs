using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.DTOs.Rezervasyonlar
{
    public class RezervasyonDetailDto
    {
        public int Id { get; set; }
        public int CardakId { get; set; }
        public int VatandasId { get; set; }
        public DateTime BaslangicZamani { get; set; }
        public DateTime BitisZamani { get; set; }
        public int KisiSayisi { get; set; }
        public RezervasyonDurumu Durum { get; set; }
        public string? Not { get; set; }
    }
}