using Microsoft.EntityFrameworkCore;
using PropertiesListings.Models;

namespace PropertiesListings.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        public DbSet<City>Cities { get; set; }
    }
}
