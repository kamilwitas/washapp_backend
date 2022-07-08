using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;

namespace washapp.services.customers.application.Services;

public interface ICustomerOperationsValidator
{
    Task CheckForCustomerDuplicates(string firstName, string lastName, string companyName, string customerColor, bool forUpdate = false,
    Guid originalCustomerId = new Guid());
    Task<Location> CheckIfLocationExists(Guid locationId);
}