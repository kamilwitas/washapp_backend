namespace washapp.orders.infrastructure.Mongo.Documents
{
    public class OrderActionDocument
    {
        public Guid Id { get; }
        public EmployeeDocument Employee { get; }
        public DateTime ActionDateTime { get; }

        public OrderActionDocument(Guid id, EmployeeDocument employee, DateTime actionDateTime)
        {
            Id = id;
            Employee = employee;
            ActionDateTime = actionDateTime;
        }
    }
}
