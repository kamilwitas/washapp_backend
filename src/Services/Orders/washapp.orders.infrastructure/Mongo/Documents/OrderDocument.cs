using washapp.orders.core.Entities;
using washapp.orders.core.Enums;
using Convey.Types;

namespace washapp.orders.infrastructure.Mongo.Documents
{
    public class OrderDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CompanyName { get; set; }
        public List<OrderLineDocument> OrderLines { get; set; }
        public OrderState OrderState { get; set; }
        public List<OrderActionDocument> OrderActions { get; set; }
        public EmployeeDocument CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public OrderDocument(Guid id, Guid customerId, string companyName, List<OrderLineDocument> orderLines,
            OrderState orderState, EmployeeDocument createdBy, DateTime createdAt, List<OrderActionDocument> orderActions)
        {
            Id = id;
            CustomerId = customerId;
            CompanyName = companyName;
            OrderLines = orderLines;
            OrderState = orderState;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            OrderActions = orderActions;
        }
    }
}
