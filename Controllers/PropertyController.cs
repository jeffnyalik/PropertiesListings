using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertiesListings.Dtos;
using PropertiesListings.Interfaces;
using PropertiesListings.Models;

namespace PropertiesListings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

       public PropertyController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetProperties()
        {
            var properties = await uow.PropertyRepository.GetPropertiesAync();
            var propertyDto = mapper.Map<IEnumerable<PropertyDto>>(properties);
            return Ok(propertyDto);
        }
    }
}
