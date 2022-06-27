using Microsoft.EntityFrameworkCore;
using PropertiesListings.DataContext;
using PropertiesListings.Interfaces;
using PropertiesListings.Models;

namespace PropertiesListings.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private ApplicationDbContext _context;
        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        void ICityRepository.AddCity(City city)
        {
            _context.AddAsync(city);
        }

        void ICityRepository.DeleteCity(int id)
        {
            var city = _context.Cities.Find(id);
            _context.Cities.Remove(city);
        }

        async Task<IEnumerable<City>> ICityRepository.GetCitiesAync()
        {
            return await _context.Cities.ToListAsync();
        }

        Task ICityRepository.GetCity(int cityId)
        {
            throw new NotImplementedException();
        }

        void ICityRepository.UpdateCity(int id, City city)
        {
            throw new NotImplementedException();
        }
    }
}
