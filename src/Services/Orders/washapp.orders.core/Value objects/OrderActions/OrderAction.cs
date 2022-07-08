using washapp.orders.core.Entities;

namespace washapp.orders.core.Value_objects.OrderActions;

public abstract class OrderAction
{
    public Guid Id { get; }
    public Employee Employee { get; }
    public DateTime ActionDateTime { get; }

    public OrderAction(Guid orderActionId, Employee employee)
    {
        Id = orderActionId;
        Employee = employee;
        ActionDateTime = DateTime.UtcNow;
    }
}
