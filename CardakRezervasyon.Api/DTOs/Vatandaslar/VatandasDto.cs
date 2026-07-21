namespace CardakRezervasyon.Api.DTOs.Vatandaslar
{
    public class VatandasDto
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string Eposta { get; set; } = string.Empty;
    }
}