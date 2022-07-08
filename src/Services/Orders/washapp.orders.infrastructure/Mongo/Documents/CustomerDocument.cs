using Convey.Types;

namespace washapp.orders.infrastructure.Mongo.Documents
{
    public class CustomerDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CustomerColor { get; set; }
        public Guid LocationId { get; set; }
        public List<AssortmentDocument> Assortments { get; set; }
        public List<Guid> Orders { get; set; }
        public bool IsDeleted { get; set; }

        public CustomerDocument(Guid id, string companyName, string customerColor, Guid locationId, 
            List<AssortmentDocument> assortments, List<Guid> orders, bool isDeleted)
        {
            Id = id;
            CompanyName = companyName;
            CustomerColor = customerColor;
            LocationId = locationId;
            Assortments = assortments;
            Orders = orders;
            IsDeleted = isDeleted;
        }
    }
}
