namespace PropertiesListings.Models
{
    public class City
    {
        public int cityId { get; set; }
        public string? cityName { get; set; }
        public int population { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int updatedBy { get; set; }
    }
}
