namespace EventBus.Messages.IntegrationEvents.customers_service;

public class CustomerDeleted
{
    public Guid CustomerId { get; set; }

    public CustomerDeleted(Guid customerId)
    {
        CustomerId = customerId;
    }
}