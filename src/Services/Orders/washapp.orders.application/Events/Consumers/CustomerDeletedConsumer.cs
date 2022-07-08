using washapp.orders.core.Repository;
using EventBus.Messages.IntegrationEvents.customers_service;
using MassTransit;

namespace washapp.orders.application.Events.Consumers
{
    public class CustomerDeletedConsumer : IConsumer<CustomerDeleted>
    {
        private readonly ICustomersMongoRepository _customersRepository;

        public CustomerDeletedConsumer(ICustomersMongoRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task Consume(ConsumeContext<CustomerDeleted> context)
        {
            var customer = await _customersRepository.GetCustomerAsync(context.Message.CustomerId);

            if (customer is null)
            {
                return;
            }

            customer.SoftDelete();
            await _customersRepository.UpdateAsync(customer);
        }
    }
}
