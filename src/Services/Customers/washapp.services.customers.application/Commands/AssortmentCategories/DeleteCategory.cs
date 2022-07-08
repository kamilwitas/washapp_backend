using MediatR;

namespace washapp.services.customers.application.Commands.AssortmentCategories;

public class DeleteCategory : IRequest
{
    public Guid CategoryId { get; set; }

    public DeleteCategory(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}