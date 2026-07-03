using System.Collections.Generic;

namespace CardakRezervasyon.Api.Models.Entities
{
    public class Cardak
    {
        public int Id { get; set; }

        public int MesireAlaniId { get; set; }
        public MesireAlani MesireAlani { get; set; } = null!;

        public int Numara { get; set; }
        public string? Blok { get; set; }
        public int Kapasite { get; set; }
        public bool MangalliMi { get; set; }
        public bool AktifMi { get; set; } = true;

        public ICollection<Rezervasyon> Rezervasyonlar { get; set; } = new List<Rezervasyon>();
    }
}