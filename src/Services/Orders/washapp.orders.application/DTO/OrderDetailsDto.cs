using washapp.orders.core.Entities;
using washapp.orders.core.Enums;

namespace washapp.orders.application.DTO;

public class OrderDetailsDto
{
    public Guid OrderId { get; set; }
    public OrderState OrderState { get;  set; }
    public Guid CustomerId { get; set; }
    public string CompanyName { get; set; }
    public EmployeeDto CreatedBy { get; set; }
    public IEnumerable<OrderLineDetailsDto> OrderLines { get; set; }

    public OrderDetailsDto(Guid orderId, OrderState orderState,Guid customerId, string companyName,
        IEnumerable<OrderLineDetailsDto> orderLines, EmployeeDto createdBy)
    {
        OrderId = orderId;
        OrderState = orderState;
        CustomerId = customerId;
        CompanyName = companyName;
        OrderLines = orderLines;
        CreatedBy = createdBy;
    }
}