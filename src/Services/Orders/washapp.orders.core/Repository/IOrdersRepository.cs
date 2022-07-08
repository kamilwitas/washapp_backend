using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using washapp.orders.core.Entities;

namespace washapp.orders.core.Repository
{
    public interface IOrdersRepository
    {
        Task AddAsync(Order order);
        Task<Order> GetAsync(Guid orderId);
        Task DeleteAsync(Guid orderId);
        Task UpdateAsync(Order order);
    }
}
