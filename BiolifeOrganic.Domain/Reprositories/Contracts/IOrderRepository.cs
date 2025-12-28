using BiolifeOrganic.Dll.DataContext.Entities;
using System.Linq.Expressions;

namespace BiolifeOrganic.Dll.Reprositories.Contracts;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetUserOrdersAsync(string userId);
    Task<Order?> GetOrderWithDetailsAsync(int orderId, string userId);
    Task<List<Order>> GetOrdersByStatusAsync(string userId, OrderStatus status);
    Task<List<Order>> GetAllOrdersWithUserAsync();
    Task<Order?> GetOrderByIdWithDetailsAsync(int orderId);
    Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate);
    Task<bool> HasOrdersAsync(string userId);

}


