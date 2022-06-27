using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertiesListings.DataContext;
using PropertiesListings.Interfaces;
using PropertiesListings.Models;

namespace SignalRWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CityController : ControllerBase
    {
        private IUnitOfWork uow;
        public CityController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        //Get city
        [HttpGet]
        public async Task<ActionResult> GetCities()
        {
            var res = await uow.CityRepository.GetCitiesAync();
            return Ok(res);
        }
        //add a city
        [HttpPost]
        public async Task<ActionResult>AddCity(City city)
        {
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
