using washapp.orders.application.Models.Pagination;
using washapp.orders.application.Queries;
using washapp.orders.core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using washapp.orders.api.Models;

namespace washapp.orders.api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class OrdersController : Controller
{
    private readonly IMediator _mediatr;

    public OrdersController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }
    
    [HttpPost]
    [Route("customerId")]
    [SwaggerOperation(Summary = "Endpoint do tworzenia zamówień.", Tags = new string[] {"Orders"})]
    public async Task<ActionResult> CreateOrder(Guid customerId, [FromBody]CreateOrderRequest request)
    {
        var command = request.AsCommand(customerId);
        await _mediatr.Send(command);
        return Ok();
    }

    [HttpGet]
    [Route("orderDetails/{orderId}")]
    [SwaggerOperation(Summary = "Endpoint do pobierania szczegółów zamówienia.", Tags = new string[] {"Orders"})]
    public async Task<ActionResult> GetOrderDetails(Guid orderId)
    {
        var query = new GetOrderDetails() {OrderId = orderId};
        var orderDetails = await _mediatr.Send(query);
        return Ok(orderDetails);
    }

    [HttpGet]
    [Route("{customerId}")]
    [SwaggerOperation(Summary = "Endpoint do pobierania zamówień wybranego klienta.", Tags = new string[] { "Orders" })]
    public async Task<ActionResult> GetCustomerOrders(Guid customerId)
    {
        var query = new GetCustomerOrders() {CustomerId = customerId};
        var orders = await _mediatr.Send(query);
        return Ok(orders);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Endpoint do pobierania paginowanej listy wszystkich zamówień z możliwością filtrowania.", Tags = new string[] { "Orders" })]
    public async Task<ActionResult>GetFilteredOrders([FromQuery]int pageNumber=1, [FromQuery] int pageSize=5,
        [FromQuery] string searchPhrase="all", [FromQuery] string sortBy="createdAt",
        [FromQuery] SortDirection sortDirection=SortDirection.DESC)
    {
        var command = new GetAllPagedOrders(searchPhrase,pageNumber,pageSize,sortBy,sortDirection);
        var result = await _mediatr.Send(command);

        return Ok(result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Endpoint do pobierania paginowanej listy wszystkich zamówień na podstawie wybranego statusu zamówienia.", Tags = new string[] { "Orders" })]    
    [Route("getByOrderState")]
    public async Task<ActionResult> GetOrdersByOrderState([FromQuery] int pageNumber, [FromQuery] int pageSize,
        [FromQuery] OrderState orderState)
    {
        var query = new GetOrdersByOrderState(orderState, pageSize, pageNumber);
        var orders = await _mediatr.Send(query);
        return Ok(orders);
    }

    [HttpPost]
    [Route("orderExecution")]
    [SwaggerOperation(Summary = "Endpoint do wydawania zamówień.", Tags = new string[] { "Orders" })]
    public async Task<ActionResult>ExecuteOrder([FromQuery]Guid orderId, [FromBody]CreateOrderExecutionsRequest request)
    {
        var command = request.AsCommand(orderId);
        await _mediatr.Send(command);
        return Ok();
    }


}