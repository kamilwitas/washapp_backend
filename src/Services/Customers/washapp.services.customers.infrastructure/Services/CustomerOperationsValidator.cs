using washapp.services.customers.application.Exceptions;
using washapp.services.customers.application.Services;
using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;

namespace washapp.services.customers.infrastructure.Services
{
    public class CustomerOperationsValidator : ICustomerOperationsValidator
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly ILocationsRepository _locationsRepository;

        public CustomerOperationsValidator(ICustomersRepository customersRepository, ILocationsRepository locationsRepository)
        {
            _customersRepository = customersRepository;
            _locationsRepository = locationsRepository;
        }

        public async Task CheckForCustomerDuplicates(string firstName, string lastName, string companyName, string customerColor, 
        bool forUpdate=false, Guid originalCustomerId = new Guid())
        {
            Func<Customer, bool> predicate = customer =>
                customer.FirstName.ToLower() == firstName.ToLower() &&
                customer.LastName.ToLower() == lastName.ToLower() &&
                customer.CompanyName.ToLower() == companyName.ToLower() &&
                customer.CustomerColor.ToLower() == customerColor.ToLower();

            Func<Customer, bool> predicateForUpdate = customer =>
                customer.FirstName.ToLower() == firstName.ToLower() &&
                customer.LastName.ToLower() == lastName.ToLower() &&
                customer.CompanyName.ToLower() == companyName.ToLower() &&
                customer.CustomerColor.ToLower() == customerColor.ToLower() &&
                customer.Id != originalCustomerId;


            Customer customer;

            if (forUpdate)
            {
                customer = await _customersRepository
                    .GetByExpression(predicateForUpdate);
            }
            else
            {
                customer = await _customersRepository
                    .GetByExpression(predicate);
            }


            if (customer is not null)
            {
                throw new CustomerIsAlreadyExistsException();
            }
        }

        public async Task<Location> CheckIfLocationExists(Guid locationId)
        {
            var location = await _locationsRepository.GetByIdAsync(locationId);

            if (location is null)
            {
                throw new LocationDoesNotExistsException(locationId);
            }

            return location;
        }

    }
}