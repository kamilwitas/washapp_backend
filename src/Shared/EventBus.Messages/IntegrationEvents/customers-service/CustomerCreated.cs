namespace EventBus.Messages.IntegrationEvents.customers_service;

public class CustomerCreated 
{
    public Guid CustomerId { get; set; }

    public CustomerCreated(Guid customerId)
    {
        CustomerId = customerId;
    }
}