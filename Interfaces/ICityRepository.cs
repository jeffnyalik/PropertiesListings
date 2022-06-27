using PropertiesListings.Models;

namespace PropertiesListings.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAync();
        Task GetCity(int cityId);
        void AddCity(City city);
        void DeleteCity(int id);
        void UpdateCity(int id, City city);
    }
}
