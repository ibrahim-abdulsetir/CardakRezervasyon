using System;

namespace CardakRezervasyon.Api.Models.Entities
{
    public class DogrulamaKodu
    {
        public int Id { get; set; }

        public int VatandasId { get; set; }
        public Vatandas Vatandas { get; set; } = null!;

        public string Kod { get; set; } = string.Empty;
        public DateTime SonGecerlilikZamani { get; set; }
        public bool KullanildiMi { get; set; } = false;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.UtcNow;
    }
}