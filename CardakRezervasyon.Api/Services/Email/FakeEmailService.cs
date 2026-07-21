namespace CardakRezervasyon.Api.Services.Email
{
    public class FakeEmailService : IEmailService
    {
        private readonly ILogger<FakeEmailService> _logger;

        public FakeEmailService(ILogger<FakeEmailService> logger)
        {
            _logger = logger;
        }

        public Task SendDogrulamaKoduAsync(string eposta, string kod)
        {
            _logger.LogInformation("[FAKE EMAIL] To: {Eposta} — Dogrulama Kodu: {Kod}", eposta, kod);
            return Task.CompletedTask;
        }
    }
}