using System.ComponentModel.DataAnnotations;
using washapp.orders.application.DTO;

namespace washapp.orders.api.Models;

public class CreateOrderRequest
{
    [Required]
    public List<CreateOrderLineRequest> OrderLines { get; set; }
}