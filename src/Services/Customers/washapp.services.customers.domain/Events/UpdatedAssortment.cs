using washapp.services.customers.domain.Entities;

namespace washapp.services.customers.domain.Events;

public class UpdatedAssortment : IDomainEvent
{
    public Assortment Assortment { get; set; }

    public UpdatedAssortment(Assortment assortment)
    {
        Assortment = assortment;
    }
}