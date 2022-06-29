using System.ComponentModel.DataAnnotations;

namespace PropertiesListings.Dtos
{
    public class CityDto
    {
        public int cityId { get; set; }

        [Required(ErrorMessage = "City name is required")]
        public string? cityName { get; set; }

        [Required(ErrorMessage = "population field is required")]
        public int population { get; set; }
    }
}
