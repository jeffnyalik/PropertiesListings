using AutoMapper;
using PropertiesListings.Dtos;
using PropertiesListings.Models;

namespace PropertiesListings.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
