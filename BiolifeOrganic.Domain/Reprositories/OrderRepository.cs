using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BiolifeOrganic.Dll.Reprositories;

public class OrderRepository : EfCoreRepository<Order>, IOrderRepository
{
    private readonly AppDbContext _dbContext;
    public OrderRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Order>> GetUserOrdersAsync(string userId)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.AppUserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderWithDetailsAsync(int orderId, string userId)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .ThenInclude(i=>i.Category)
            .Include(o => o.ShippingContact)
            .FirstOrDefaultAsync(o => o.Id == orderId && o.AppUserId == userId);
    }

    public async Task<List<Order>> GetOrdersByStatusAsync(string userId, OrderStatus status)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.AppUserId == userId && o.Status == status)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Order>> GetAllOrdersWithUserAsync()
    {
        return await _dbContext.Orders
            .Include(o => o.AppUser)
            .Include(o => o.OrderItems)
            .Where(o => !o.IsDeleted)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdWithDetailsAsync(int orderId)
    {
        return await _dbContext.Orders
            .Include(o => o.AppUser)
            .Include(o => o.OrderItems)
            .Include(o => o.ShippingContact)
            .Include(o => o.BillingContact)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate)
    {
        return await _dbContext.Orders.AnyAsync(predicate);
    }

}
