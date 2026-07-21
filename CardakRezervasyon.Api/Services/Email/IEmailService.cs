namespace CardakRezervasyon.Api.Services.Email
{
    public interface IEmailService
    {
        Task SendDogrulamaKoduAsync(string eposta, string kod);
    }
}