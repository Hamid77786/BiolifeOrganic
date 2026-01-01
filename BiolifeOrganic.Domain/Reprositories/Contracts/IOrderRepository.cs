using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.ReadModels.Order;
using System.Linq.Expressions;

namespace BiolifeOrganic.Dll.Reprositories.Contracts;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetUserOrdersAsync(string userId);
    Task<Order?> GetOrderWithDetailsAsync(int orderId, string userId);
    Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate);
    Task<bool> HasOrdersAsync(string userId);
    Task<int> CountAsync(string userId);
    Task<List<OrderListRM>> GetOrdersForUserAsync(string userId);
    Task<OrderDetailsRM?> GetOrderDetailsAsync(int orderId);

}


