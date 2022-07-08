using washapp.orders.core.Entities;

namespace washapp.orders.application.DTO;

public class OrderDto
{
    public Guid OrderId { get; set; }
    public string OrderState { get; set; }
    public Guid CustomerId { get; set; }
    public string CompanyName { get; set; }
    public DateTime CreatedAt { get; set; }

    public OrderDto(Guid orderId, string orderState, Guid customerId, string companyName, DateTime createdAt)
    {
        OrderId = orderId;
        OrderState = orderState;
        CompanyName = companyName;
        CustomerId = customerId;
        CreatedAt = createdAt;
    }
}