namespace CardakRezervasyon.Api.DTOs.MesireAlanlari
{
    public class MesireAlaniDetailDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
        public string Mahalle { get; set; } = string.Empty;
        public TimeSpan AcilisSaati { get; set; }
        public TimeSpan KapanisSaati { get; set; }
        public bool AktifMi { get; set; }
        public int ToplamCardakSayisi { get; set; }
        public int AktifCardakSayisi { get; set; }
    }
}