namespace washapp.orders.application.DTO
{
    public class OrderExecutionRequestDto
    {
        public Guid OrderLineId { get; set; }
        public int ExecutedQuantity { get; set; }

        public OrderExecutionRequestDto(Guid orderLineId, int executedQuantity)
        {
            OrderLineId = orderLineId;
            ExecutedQuantity = executedQuantity;
        }
    }
}
