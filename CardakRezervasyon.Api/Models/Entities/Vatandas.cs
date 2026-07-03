using System.Collections.Generic;

namespace CardakRezervasyon.Api.Models.Entities
{
    public class Vatandas
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string TcKimlikNo { get; set; } = string.Empty;
        public string? Eposta { get; set; }

        public ICollection<Rezervasyon> Rezervasyonlar { get; set; } = new List<Rezervasyon>();
    }
}