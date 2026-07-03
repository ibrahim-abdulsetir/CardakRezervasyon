using System;

namespace CardakRezervasyon.Api.Models.Entities
{
    public class Rezervasyon
    {
        public int Id { get; set; }

        public int CardakId { get; set; }
        public Cardak Cardak { get; set; } = null!;

        public int VatandasId { get; set; }
        public Vatandas Vatandas { get; set; } = null!;

        public DateTime BaslangicZamani { get; set; }
        public DateTime BitisZamani { get; set; }
        public int KisiSayisi { get; set; }

        public RezervasyonDurumu Durum { get; set; } = RezervasyonDurumu.Beklemede;

        public string? Not { get; set; }
        public DateTime OlusturmaTarihi { get; set; } = DateTime.UtcNow;
    }
}