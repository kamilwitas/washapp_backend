using washapp.orders.application.DTO;

namespace washapp.orders.api.Models
{
    public class CreateOrderExecutionsRequest
    {       
        public IEnumerable<OrderExecutionRequestDto> OrderExecutionRequests { get; set; }
    }
}
