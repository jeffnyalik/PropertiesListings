using PropertiesListings.Models;

namespace PropertiesListings.Interfaces
{
    public interface IPropertyRepo
    {
        Task<IEnumerable<Property>> GetPropertiesAync();
        Task<Property> FindProperty(int propertyId);
        void UpdateProperty(Property property);
        void AddProperty(Property property);
        void DeleteProperty(int id);
    }
}
