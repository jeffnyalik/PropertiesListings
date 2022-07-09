using Microsoft.EntityFrameworkCore;
using PropertiesListings.DataContext;
using PropertiesListings.Interfaces;
using PropertiesListings.Models;

namespace PropertiesListings.Data.Repo
{
    public class PropertyRepository : IPropertyRepo
    {
        private readonly ApplicationDbContext _context;
        public PropertyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddProperty(Property property)
        {
            _context.Add(property);
        }

        public void DeleteProperty(int id)
        {
            var props = _context.Properties.Find(id);
            _context.Remove(props);
        }

        public async Task<Property> FindProperty(int propertyId)
        {
            return await _context.Properties.FindAsync(propertyId);
        }

        public async Task<IEnumerable<Property>> GetPropertiesAync()
        {
            return await _context.Properties.ToListAsync();
        }

        public void UpdateProperty(Property property)
        {
            _context.Properties.Update(property);
        }
    }
}
