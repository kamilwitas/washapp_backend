using System.ComponentModel.DataAnnotations;

namespace washapp.services.customers.api.Models.Customers;

public class CreateCustomerRequest
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Street { get; set; }
    [Required]
    public string LocalNumber { get; set; }
    [Required]
    public string PostCode { get; set; }
    [Required]
    public Guid LocationId { get; set; }
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public string CustomerColor { get; set; }
}