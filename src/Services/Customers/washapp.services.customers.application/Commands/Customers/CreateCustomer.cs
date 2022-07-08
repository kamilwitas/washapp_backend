using washapp.services.customers.domain.Entities;
using MediatR;

namespace washapp.services.customers.application.Commands.Customers;

public class CreateCustomer : IRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string LocalNumber { get; set; }
    public string PostCode { get; set; }
    public Guid LocationId { get; set; }
    public string CompanyName { get; set; }
    public string CustomerColor { get; set; }

    public CreateCustomer(string firstName, string lastName, string street, string localNumber, string postCode, 
        Guid locationId, string companyName, string customerColor)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        LocalNumber = localNumber;
        PostCode = postCode;
        LocationId = locationId;
        CompanyName = companyName;
        CustomerColor = customerColor;
    }
}