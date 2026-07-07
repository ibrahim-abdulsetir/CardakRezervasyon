namespace CardakRezervasyon.Api.DTOs.Cardaklar
{
    public class CardakListDto
    {
        public int Id { get; set; }
        public int Numara { get; set; }
        public string? Blok { get; set; }
        public int Kapasite { get; set; }
        public bool MangalliMi { get; set; }
        public bool AktifMi { get; set; }
    }
}