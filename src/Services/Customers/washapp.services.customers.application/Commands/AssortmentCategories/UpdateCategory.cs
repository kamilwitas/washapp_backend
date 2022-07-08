using MediatR;

namespace washapp.services.customers.application.Commands.AssortmentCategories;

public class UpdateCategory : IRequest
{
    
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
}