using MediatR;

namespace washapp.services.customers.application.Commands.Assortments;

public class DeleteCustomerItem : IRequest
{
    public Guid CustomerId { get; set; }
    public Guid AssortmentId { get; set; }

    public DeleteCustomerItem(Guid customerId, Guid assortmentId)
    {
        CustomerId = customerId;
        AssortmentId = assortmentId;
    }
}