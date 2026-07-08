namespace CardakRezervasyon.Api.DTOs.Rezervasyonlar
{
    public class CreateRezervasyonDto
    {
        public int CardakId { get; set; }
        public int VatandasId { get; set; }
        public DateTime BaslangicZamani { get; set; }
        public DateTime BitisZamani { get; set; }
        public int KisiSayisi { get; set; }
        public string? Not { get; set; }
    }
}