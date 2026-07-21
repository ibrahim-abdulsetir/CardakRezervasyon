using System;
using System.Collections.Generic;
using System.Linq;
using CardakRezervasyon.Api.Models.Entities;

namespace CardakRezervasyon.Api.Data.Seed
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Don't seed again if data already exists
            if (context.MesireAlanlari.Any())
            {
                return;
            }

            var random = new Random();

            var alanlar = new List<MesireAlani>
{
    new MesireAlani { Ad = "Paşabahçe", Mahalle = "Paşabahçe Mah.", AcilisSaati = new TimeSpan(8,0,0), KapanisSaati = new TimeSpan(22,0,0) },
    new MesireAlani { Ad = "Karşıyaka", Mahalle = "Karşıyaka Mah.", AcilisSaati = new TimeSpan(8,0,0), KapanisSaati = new TimeSpan(22,0,0) },
    new MesireAlani { Ad = "Osman Seçilmiş", Mahalle = "Osman Seçilmiş Mah.", AcilisSaati = new TimeSpan(8,0,0), KapanisSaati = new TimeSpan(22,0,0) }
};

            context.MesireAlanlari.AddRange(alanlar);
            context.SaveChanges(); // saves now, so each 'alan' gets a real Id from the database
            var tumCardaklar = new List<Cardak>();

            foreach (var alan in alanlar)
            {
                int cardakSayisi = random.Next(500, 1001); // random number between 500 and 1000

                for (int i = 1; i <= cardakSayisi; i++)
                {
                    tumCardaklar.Add(new Cardak
                    {
                        MesireAlaniId = alan.Id,
                        Numara = i,
                        Blok = ((char)('A' + random.Next(0, 4))).ToString(), // random block: A, B, C, or D
                        Kapasite = random.Next(4, 13),
                        MangalliMi = random.Next(0, 2) == 1,
                        AktifMi = true
                    });
                }
            }

            context.Cardaklar.AddRange(tumCardaklar);
            context.SaveChanges(); // ONE save for potentially 1500-3000 çardaks
            var vatandaslar = new List<Vatandas>();
            for (int i = 1; i <= 100; i++)
            {
                vatandaslar.Add(new Vatandas
                {
                    Ad = $"Vatandas{i}",
                    Soyad = $"Soyadi{i}",
                    Telefon = $"05{random.Next(100000000, 999999999)}",
                    TcKimlikNo = random.Next(10000000, 99999999).ToString(),
                    Eposta = $"vatandas{i}@example.com",
                    ParolaHash = "SEED_PLACEHOLDER_HASH",
                    AktifMi = true
                });
            }

            context.Vatandaslar.AddRange(vatandaslar);
            context.SaveChanges();
            var rezervasyonlar = new List<Rezervasyon>();
            var durumlar = new[] { RezervasyonDurumu.Beklemede, RezervasyonDurumu.Onaylandi, RezervasyonDurumu.Tamamlandi, RezervasyonDurumu.IptalEdildi };

            // Only ~40% of çardaks get reservations, so the rest stay "empty" on purpose
            var rezerveEdilecekler = tumCardaklar.Where(c => random.NextDouble() < 0.4).ToList();

            foreach (var cardak in rezerveEdilecekler)
            {
                int sayisi = random.Next(1, 4); // 1 to 3 bookings for this çardak

                for (int j = 0; j < sayisi; j++)
                {
                    var baslangic = DateTime.Today.AddDays(random.Next(-10, 20)).AddHours(random.Next(8, 20));
                    var bitis = baslangic.AddHours(random.Next(1, 4));

                    rezervasyonlar.Add(new Rezervasyon
                    {
                        CardakId = cardak.Id,
                        VatandasId = vatandaslar[random.Next(vatandaslar.Count)].Id,
                        BaslangicZamani = baslangic,
                        BitisZamani = bitis,
                        KisiSayisi = random.Next(2, cardak.Kapasite + 1),
                        Durum = durumlar[random.Next(durumlar.Length)],
                        OlusturmaTarihi = DateTime.UtcNow
                    });
                }
            }

            context.Rezervasyonlar.AddRange(rezervasyonlar);
            context.SaveChanges();
        }
    }
}