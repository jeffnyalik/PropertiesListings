using PropertiesListings.Data.Repo;
using PropertiesListings.DataContext;
using PropertiesListings.Interfaces;

namespace PropertiesListings.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        IPropertyRepo IUnitOfWork.PropertyRepository =>
            new PropertyRepository(_context);

        ICityRepository IUnitOfWork.CityRepository =>
            new CityRepository(_context);
        

        public async Task<bool>SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
