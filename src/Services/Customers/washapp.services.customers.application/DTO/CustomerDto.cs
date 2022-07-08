namespace washapp.services.customers.application.DTO;

public class CustomerDto
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public Guid LocationId { get; set; }
    public string Street { get; set; }
    public string LocalNumber { get; set; }
    public string PostCode { get; set; }
    public string CompanyName { get; set; }
    public string CustomerColor { get; set; }
    

    public CustomerDto(Guid customerId, string firstName, string lastName, string city, string street, 
    string localNumber, string postCode, string companyName, string customerColor, Guid locationId)
    {
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
        City = city;
        Street = street;
        LocalNumber = localNumber;
        PostCode = postCode;
        CompanyName = companyName;
        CustomerColor = customerColor;
        LocationId = locationId;
    }
}