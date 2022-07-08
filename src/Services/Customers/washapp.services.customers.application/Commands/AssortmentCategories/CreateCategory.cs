using System.ComponentModel.DataAnnotations;
using MediatR;

namespace washapp.services.customers.application.Commands.AssortmentCategories;

public class CreateCategory : IRequest
{
    public string CategoryName { get; set; }

    public CreateCategory(string categoryName)
    {
        CategoryName = categoryName;
    }
}