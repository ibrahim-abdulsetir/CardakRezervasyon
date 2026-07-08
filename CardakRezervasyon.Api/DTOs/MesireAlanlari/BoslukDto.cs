namespace CardakRezervasyon.Api.DTOs.MesireAlanlari
{
    public class BoslukDto
    {
        public int MesireAlaniId { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public int AktifCardakSayisi { get; set; }
        public int DoluCardakSayisi { get; set; }
        public int BosCardakSayisi { get; set; }
    }
}