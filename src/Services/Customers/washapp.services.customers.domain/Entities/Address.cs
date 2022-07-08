using washapp.services.customers.domain.Exceptions;

namespace washapp.services.customers.domain.Entities;

public class Address
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string LocalNumber { get; set; }
    public string PostCode { get; set; }
    public Location Location { get; set; }


    public Address() {}
    public Address(string street, string localNumber, string postCode, Location location)
    {
        if (location is null)
        {
            throw new LocationCannotBeNullException();
        }
        Street = street;
        LocalNumber = localNumber;
        PostCode = postCode;
        Location = location;
    }

    public static Address Create(string street, string localNumber, 
        string postCode, Location location)
    {
        Address address = new Address(street, localNumber, postCode, location);
        return address;
    }

    public void UpdateAddress(string street, string localNumber, string postCode, Location location)
    {
        Street = street;
        LocalNumber = localNumber;
        PostCode = postCode;
        Location = location;
    }
}