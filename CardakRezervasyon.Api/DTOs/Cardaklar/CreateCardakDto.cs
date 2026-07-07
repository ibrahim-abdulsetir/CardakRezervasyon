namespace CardakRezervasyon.Api.DTOs.Cardaklar
{
    public class CreateCardakDto
    {
        public int Numara { get; set; }
        public string? Blok { get; set; }
        public int Kapasite { get; set; }
        public bool MangalliMi { get; set; }
    }
}