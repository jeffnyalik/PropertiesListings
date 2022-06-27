using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertiesListings.DataContext;
using PropertiesListings.Dtos;
using PropertiesListings.Interfaces;
using PropertiesListings.Models;

namespace SignalRWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        //Get city
        [HttpGet]
        public async Task<ActionResult> GetCities()
        {   
            var cities = await uow.CityRepository.GetCitiesAync();
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }
        //add a city
        [HttpPost]
        public async Task<ActionResult>AddCity(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return Ok(city);
        }

        //Delete a city
        [HttpDelete("{id}")]
        public async Task<ActionResult>DeleteCity(int id)
        {   

            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        
    }
}
