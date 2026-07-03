using System;

namespace CardakRezervasyon.Api.Models.Entities
{
    public class BakimKapaliGun
    {
        public int Id { get; set; }

        public int MesireAlaniId { get; set; }
        public MesireAlani MesireAlani { get; set; } = null!;

        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string? Sebep { get; set; }
    }
}