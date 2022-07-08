using washapp.services.customers.domain.Entities;

namespace washapp.services.customers.domain.Events;

public class UpdatedCustomer : IDomainEvent
{
    public Customer Customer { get; set; }

    public UpdatedCustomer(Customer customer)
    {
        Customer = customer;
    }
}