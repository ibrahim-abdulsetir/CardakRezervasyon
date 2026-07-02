using Microsoft.EntityFrameworkCore;

namespace CardakRezervasyon.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // We'll add DbSet<T> properties here once we create our entities (next milestone)
    }
}