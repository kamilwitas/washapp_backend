using washapp.services.customers.domain.Entities;

namespace washapp.services.customers.domain.Events;

public class CreatedCustomer : IDomainEvent
{
    public Customer Customer { get; set; }

    public CreatedCustomer(Customer customer)
    {
        Customer = customer;
    }
}