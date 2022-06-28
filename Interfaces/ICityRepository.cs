using PropertiesListings.Models;

namespace PropertiesListings.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAync();
        Task<City>FindCity(int cityId);
        void UpdateCity(City city);
        void AddCity(City city);
        void DeleteCity(int id);
    }
}
