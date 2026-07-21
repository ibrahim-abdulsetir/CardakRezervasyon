namespace CardakRezervasyon.Api.DTOs.Vatandaslar
{
    public class RegisterVatandasDto
    {
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string Eposta { get; set; } = string.Empty;
        public string Parola { get; set; } = string.Empty;
    }
}