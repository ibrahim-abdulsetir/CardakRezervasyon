namespace CardakRezervasyon.Api.Models.Entities
{
    public enum RezervasyonDurumu
    {
        Beklemede,      // waiting for approval
        Onaylandi,      // approved
        Reddedildi,     // rejected
        IptalEdildi,    // cancelled
        Tamamlandi,     // completed (already happened)
        GelMedi         // no-show
    }
}