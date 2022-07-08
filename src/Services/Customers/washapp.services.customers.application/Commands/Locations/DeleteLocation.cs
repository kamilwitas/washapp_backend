using MediatR;

namespace washapp.services.customers.application.Commands.Locations;

public class DeleteLocation : IRequest
{
    public Guid LocationId { get; set; }
}