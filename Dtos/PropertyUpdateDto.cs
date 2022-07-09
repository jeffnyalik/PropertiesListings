using PropertiesListings.Authentication;
using PropertiesListings.Models;

namespace PropertiesListings.Dtos
{
    public class PropertyUpdateDto
    {
        public string Name { get; set; }
        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int BuiltArea { get; set; }
        public int CarpetArea { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Price { get; set; }
        public int FloorNo { get; set; }
        public int TotalFloors { get; set; }
        public bool ReadyTomove { get; set; }
        public string MainEntrance { get; set; }
        public bool Security { get; set; }
        public bool Gated { get; set; }
        public int Maintainance { get; set; }
        public DateTime EstablishedDate { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public DateTime PostedOn { get; set; } = DateTime.Now;
        public int PostedBy { get; set; }
        public ApplicationUser User { get; set; }
    }
}
