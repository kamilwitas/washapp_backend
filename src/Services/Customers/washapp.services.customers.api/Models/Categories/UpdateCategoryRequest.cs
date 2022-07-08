using System.ComponentModel.DataAnnotations;

namespace washapp.services.customers.api.Models.Categories;

public class UpdateCategoryRequest
{
    [Required]
    public string CategoryName { get; set; }
}