namespace EventBus.Messages.IntegrationEvents.customers_service;

public class CustomerUpdated
{
    public Guid CustomerId { get; set; }

    public CustomerUpdated(Guid customerId)
    {
        CustomerId = customerId;
    }
}