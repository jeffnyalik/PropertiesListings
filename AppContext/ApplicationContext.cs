using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PropertiesListings.Authentication;
using PropertiesListings.Models;

namespace PropertiesListings.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<City>Cities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties  { get; set; }
        public DbSet<PropertyType> propertyTypes { get; set; }
    }
}
