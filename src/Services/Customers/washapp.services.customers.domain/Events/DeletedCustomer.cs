using washapp.services.customers.domain.Entities;

namespace washapp.services.customers.domain.Events;

public class DeletedCustomer : IDomainEvent
{
    public Customer Customer { get; set; }

    public DeletedCustomer(Customer customer)
    {
        Customer = customer;
    }
}