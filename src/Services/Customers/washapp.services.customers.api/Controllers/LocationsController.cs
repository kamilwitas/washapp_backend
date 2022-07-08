using washapp.services.customers.api.Models;
using washapp.services.customers.application.Commands;
using washapp.services.customers.application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using washapp.services.customers.api.Models.Locations;
using washapp.services.customers.application.Commands.Locations;

namespace washapp.services.customers.api.Controllers
{
    [Authorize]
    [ApiController]    
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public LocationsController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }
        
        [HttpGet]
        [SwaggerOperation(Summary = "Endpoint do wyświetlania dostępnych lokalizacji. Wymagana rola: Admin,User", Tags = new string[] {"Locations"})]
        public async Task<ActionResult> GetAllLocations()
        {
            var locations = await _mediatR.Send(new GetLocations());
            return Ok(locations);
        }
        
        
        [HttpPost]
        [Authorize(Policy = "Administrator")]
        [SwaggerOperation(Summary = "Endpoint do tworzenia lokalizacji. Wymagana rola: Admin", Tags = new string[] {"Locations"})]
        public async Task<ActionResult>CreateLocation([FromBody] CreateLocationRequest request)
        {
            var command = request.AsCommand();
            await _mediatR.Send(command);
            return Ok();
        }
        
        [HttpPut]
        [Authorize(Policy = "Administrator")]
        [Route("{locationId}")]
        [SwaggerOperation(Summary = "Endpoint do edycji lokalizacji. Wymagana rola: Admin", Tags = new string[] {"Locations"})]
        public async Task<ActionResult>UpdateLocation(Guid locationId, [FromBody]UpdateLocationRequest request)
        {
            var command = request.AsCommand(locationId);
            await _mediatR.Send(command);
            return Ok();
        }
        
        [HttpDelete]
        [Authorize(Policy = "Administrator")]
        [SwaggerOperation(Summary = "Endpoint do kasowania lokalizacji. Wymagana rola: Admin", Tags = new string[] {"Locations"})]
        [Route("{locationId}")]
        public async Task<ActionResult>RemoveLocation(Guid locationId)
        {
            await _mediatR.Send(new DeleteLocation() { LocationId = locationId });
            return Ok();
        }
    }
}
