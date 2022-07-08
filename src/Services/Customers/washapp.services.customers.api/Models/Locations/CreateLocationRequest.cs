using System.ComponentModel.DataAnnotations;

namespace washapp.services.customers.api.Models.Locations;

public class CreateLocationRequest
{
    [Required]
    public string LocationName { get; set; }
    [Required]
    public string LocationColor { get; set; }
}