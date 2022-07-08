using washapp.services.customers.api.Models;
using washapp.services.customers.application.Commands.Customers;
using washapp.services.customers.application.Queries.Customers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using washapp.services.customers.api.Models.Customers;

namespace washapp.services.customers.api.Controllers
{
    [Authorize]
    [ApiController]    
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public CustomersController(IMediator mediatR)
        {
            _mediatR= mediatR;
        }
        
        [HttpPost]
        [Authorize(Policy = "Administrator")]
        [Route("createCustomer")]
        [SwaggerOperation(Summary = "Endpoint do tworzenia klientów. Wymagana rola: Admin", Tags = new string[] {"Customers"})]
        public async Task<ActionResult<int>>CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            var command = request.AsCommand();
            var id = await _mediatR.Send(command);
            return Created($"", null);
        }
        
        
        [HttpDelete]
        [Authorize(Policy = "Administrator")]
        [Route("{customerId}")]
        [SwaggerOperation(Summary = "Endpoint do kasowania klientów. Wymagana rola: Admin", 
            Description = "Usunięcie klienta jest równoznaczne z usunięciem wszystkich jego asortymentów",Tags = new string[] {"Customers"})]
        public async Task<ActionResult> DeleteCustomer(Guid customerId)
        {
            await _mediatR.Send(new DeleteCustomer() { CustomerId = customerId });
            return Ok();
        
        }

        [HttpPut]
        [Authorize(Policy = "Administrator")]
        [Route("updateCustomer/{customerId}")]
        [SwaggerOperation(Summary = "Endpoint do aktualizacji klientów. Wymagana rola: Admin", Tags = new string[] {"Customers"})]
        public async Task<ActionResult>EditCustomer(Guid customerId,[FromBody]UpdateCustomerRequest request)
        {
            var command = request.AsCommand(customerId);
            await _mediatR.Send(command);
            return Ok();
        }
        
        [HttpGet]
        [SwaggerOperation(Summary = "Endpoint do pobierania listy wszystkich klientów. Wymagana rola: Admin,User", Tags = new string[] {"Customers"})]
        public async Task<ActionResult> GetCustomers()
        {
            var customers = await _mediatR.Send(new GetCustomers() { });
            return Ok(customers);
        }
        
        [HttpGet]
        [Route("location/{locationId}")]
        [SwaggerOperation(Summary = "Endpoint do pobierania listy klientów na podstawie lokalizacji. Wymagana rola: Admin,User", Tags = new string[] {"Customers"})]
        public async Task<ActionResult>GetCustomersByLocationId(Guid locationId)
        {
            var customers = await _mediatR.Send(new GetCustomersByLocation(locationId));
            return Ok(customers);
        }
        

        
    }
}
