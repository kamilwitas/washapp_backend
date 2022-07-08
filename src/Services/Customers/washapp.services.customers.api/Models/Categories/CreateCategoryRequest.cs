using System.ComponentModel.DataAnnotations;

namespace washapp.services.customers.api.Models.Categories;

public class CreateCategoryRequest
{
    [Required]
    public string CategoryName { get; set; }
}