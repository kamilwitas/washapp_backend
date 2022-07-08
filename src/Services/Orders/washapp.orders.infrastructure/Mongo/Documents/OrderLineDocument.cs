using washapp.orders.core.Enums;
using Convey.Types;

namespace washapp.orders.infrastructure.Mongo.Documents
{
    public class OrderLineDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public AssortmentDocument Assortment { get; set; }
        public int Quantity { get; set; }
        public double TotalWeight { get; set; }
        public WeightUnit WeightUnit { get; set; }
        public bool IsExecuted { get; set; }

        public OrderLineDocument(Guid id, AssortmentDocument assortment, int quantity, double totalWeight, WeightUnit weightUnit,
            bool isExecuted)
        {
            Id = id;
            Assortment = assortment;
            Quantity = quantity;
            TotalWeight = totalWeight;
            WeightUnit = weightUnit;
            IsExecuted = isExecuted;
        }
    }
}
