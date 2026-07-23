using System.Collections.Generic;

namespace CardakRezervasyon.Api.Models.Entities
{
    public class Vatandas
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string TcKimlikNo { get; set; } = string.Empty;
        public string Eposta { get; set; } = string.Empty;
        public string ParolaHash { get; set; } = string.Empty;
        public bool AktifMi { get; set; } = true;
        public string? Token { get; set; }
        public int BasarisizGirisSayisi { get; set; } = 0;
        public DateTime? KilitlenmeZamani { get; set; }

        public ICollection<Rezervasyon> Rezervasyonlar { get; set; } = new List<Rezervasyon>();
    }
}