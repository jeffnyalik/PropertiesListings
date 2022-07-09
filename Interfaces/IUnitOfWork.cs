namespace PropertiesListings.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IPropertyRepo PropertyRepository { get; }
        Task<bool> SaveAsync();
    }
}
