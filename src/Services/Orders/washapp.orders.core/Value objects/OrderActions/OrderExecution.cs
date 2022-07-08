using washapp.orders.core.Entities;

namespace washapp.orders.core.Value_objects.OrderActions
{
    public class OrderExecution : OrderAction
    {
        public Guid OrderLineId { get; }
        public string AssortmentName { get; private set; }
        public int ExecutedQuantity { get; }
        public OrderExecution(Guid orderActionId, Employee employee, Guid orderLineId, 
            string assortmentName, int executedQuantity) : base(orderActionId, employee)
        {
            OrderLineId = orderLineId;
            AssortmentName = assortmentName;
            ExecutedQuantity = executedQuantity;
        }

        public static OrderExecution Create(Employee employee, Guid orderLineId,
            int executedQuantity, string assortmentName = "")
        {
            return new OrderExecution(Guid.NewGuid(),employee,orderLineId, assortmentName, executedQuantity);
        }

        public void SetAssortmentName(string assortmentName)
        {
            AssortmentName = assortmentName;
        }
    }
}
