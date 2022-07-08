
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using washapp.services.customers.domain.Enums;
using washapp.services.customers.domain.Events;
using washapp.services.customers.domain.Exceptions;

namespace washapp.services.customers.domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Address Address { get; set; }
        public List<Assortment> AssortmentItems { get; set; }
        public string CompanyName { get; set; }
        
        public string CustomerColor { get; set; }
        
        public DateTime CreatedAt { get; }
        
        [NotMapped]
        public List<IDomainEvent> Events { get; set; }

        public Customer() {}

        public Customer(string firstName, string lastName, Address address,  
            string companyName, string customerColor, DateTime createdAt)
        {
            if (!IsCustomerDetailsValid(firstName, lastName,companyName,customerColor))
            {
                throw new InvalidCustomersDetailsException();
            }

            if (address is null || address.Location is null)
            {
                throw new LocationCannotBeNullException();
            }
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AssortmentItems = new List<Assortment>();
            CompanyName = companyName;
            CustomerColor = customerColor;
            CreatedAt = createdAt;
            Events = new List<IDomainEvent>();

        }

        public static Customer Create(string firstName, string lastName, Address address, 
            string companyName, string customerColor, DateTime createdAt)
        {
            Customer customer = new Customer(firstName, lastName, address, companyName,
                customerColor, DateTime.UtcNow);
            
            customer.AddEvent(new CreatedCustomer(customer));
            return customer;
        }

        public void Update(string firstName, string lastName, string companyName, string customerColor, string street, string localNumber, string postCode, Location location)
        {
            if (!IsCustomerDetailsValid(firstName,lastName,companyName,customerColor))
            {
                throw new InvalidCustomersDetailsException();
            }
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
            CustomerColor = customerColor;
            Address.UpdateAddress(street,localNumber,postCode,location);

            AddEvent(new UpdatedCustomer(this));
        }

        public void DeleteAssortmentItem(Assortment assortment)
        {
            AssortmentItems.Remove(assortment);
            AddEvent(new UpdatedCustomer(this));
        }

        public void AddAssortment(Assortment assortment)
        {
            AssortmentItems.Add(assortment);
            AddEvent(new UpdatedCustomer(this));
        }

        public void DeleteCustomer()
        {
            AddEvent(new DeletedCustomer(this));
        }

        public void UpdateAssortment(Guid assortmentId, string assortmentName, double width, double height,
            double weight, AssortmentCategory category, MeasurementUnit measurementUnit = MeasurementUnit.Cm, WeightUnit weightUnit = WeightUnit.Kg)
        {
            var assortment = AssortmentItems.FirstOrDefault(x => x.Id == assortmentId);

            if (assortment is null)
            {
                throw new AssortmentDoesNotExistsException(assortmentId);
            }
            assortment.Update(new Assortment(assortmentName,width,height,weight,category,measurementUnit,weightUnit));
            AddEvent(new UpdatedCustomer(this));
        }

        private void AddEvent(IDomainEvent @event)
        {
            if (Events is null)
            {
                Events = new List<IDomainEvent>();
            }
            Events.Add(@event);
        }

        private bool IsCustomerDetailsValid(string firstName, string lastName, string companyName, string customerColor)
        {
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(companyName) ||
                string.IsNullOrWhiteSpace(customerColor)
               )
            {
                return false;
            }

            return true;
        }
    }

    
}
