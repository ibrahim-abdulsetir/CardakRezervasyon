using System;
using System.Collections.Generic;

namespace CardakRezervasyon.Api.Models.Entities
{
    public class MesireAlani
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
        public string Mahalle { get; set; } = string.Empty;
        public TimeSpan AcilisSaati { get; set; }
        public TimeSpan KapanisSaati { get; set; }
        public bool AktifMi { get; set; } = true;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.UtcNow;

        // Navigation property: one park has many çardaks
        public ICollection<Cardak> Cardaklar { get; set; } = new List<Cardak>();
    }
}