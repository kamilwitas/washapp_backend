namespace washapp.orders.infrastructure.Mongo.Documents
{
    public class OrderExecutionDocument : OrderActionDocument
    {
        public Guid OrderLineId { get; }
        public string AssortmentName { get; }
        public int ExecutedQuantity { get; }

        public OrderExecutionDocument(Guid id, EmployeeDocument employee, DateTime actionDateTime,
            Guid orderLineId, string assortmentName, int executedQuantity) : base(id, employee, actionDateTime)
        {
            OrderLineId = orderLineId;
            AssortmentName = assortmentName;
            ExecutedQuantity = executedQuantity;
        }
    }
}
