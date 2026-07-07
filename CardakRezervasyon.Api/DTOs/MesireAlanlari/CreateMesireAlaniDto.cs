namespace CardakRezervasyon.Api.DTOs.MesireAlanlari
{
    public class CreateMesireAlaniDto
    {
        public string Ad { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
        public string Mahalle { get; set; } = string.Empty;
        public TimeSpan AcilisSaati { get; set; }
        public TimeSpan KapanisSaati { get; set; }
    }
}