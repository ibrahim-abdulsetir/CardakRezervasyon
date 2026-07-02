namespace CardakRezervasyon.Api.Models.Entities
{
    public class Cardak
    {
        public int Id { get; set; }
        public int MesireAlaniId { get; set; }

        // We'll add the rest of the fields (Numara, Blok, Kapasite, etc.)
        // in the next step — this is just enough to make MesireAlani compile.
    }
}