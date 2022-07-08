using System.Runtime.InteropServices;
using washapp.services.customers.api.Models;
using washapp.services.customers.application.Exceptions;
using washapp.services.customers.application.Queries.Assortment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using washapp.services.customers.api.Models.Assortments;

namespace washapp.services.customers.api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AssortmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssortmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    [Authorize(Policy = "Administrator")]
    [Route("addAssortment/{customerId}")]
    [SwaggerOperation(Summary = "Endpoint do dodawania asortymentu dla klienta. Wymagana rola: Admin", Tags = new string[] {"Assortment"})]
    public async Task<ActionResult>AddAssortment(Guid customerId, [FromBody] AddItemToCustomerRequest request)
    {
        var command = request.AsCommand(customerId);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    [Route("{customerId}")]
    [SwaggerOperation(Summary = "Endpoint do wyświetlania asortymentu wybranego klienta. Wymagana rola: Admin, User",
        Tags = new string[] {"Assortment"})]
    public async Task<ActionResult> GetCustomerAssortment(Guid customerId)
    {
        var query = new GetCustomerAssortment() {CustomerId = customerId};
        var assortments = await _mediator.Send(query);
        return Ok(assortments);
    }

    [HttpDelete]
    [Authorize(Policy = "Administrator")]
    [Route("{customerId}")]
    [SwaggerOperation(Summary = "Endpoint do kasowania asortymentu klienta. Wymagana rola: Admin",
        Tags = new string[] {"Assortment"})]
    public async Task<ActionResult> DeleteCustomerItem(Guid customerId, [FromBody] DeleteCustomerItemRequest request)
    {
        var command = request.AsCommand(customerId);
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut]
    [Authorize(Policy = "Administrator")]
    [Route("updateCustomerAssortment/{customerId}")]
    [SwaggerOperation(Summary = "Endpoint do edycji  pojedynczego asortymentu klienta. Wymagana rola: Admin", Tags = new string[] {"Assortment"})]
    public async Task<ActionResult>EditCustomerAssortment(Guid customerId, [FromBody]UpdateCustomerItemRequest request)
    {
        var command = request.AsCommand(customerId);
        await _mediator.Send(command);
        return Ok();
    }
    
    
}