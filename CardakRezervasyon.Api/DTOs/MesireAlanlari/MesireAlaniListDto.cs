namespace CardakRezervasyon.Api.DTOs.MesireAlanlari
{
    public class MesireAlaniListDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Mahalle { get; set; } = string.Empty;
        public bool AktifMi { get; set; }
    }
}