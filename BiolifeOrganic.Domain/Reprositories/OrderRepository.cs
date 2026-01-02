using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.ReadModels.Contact;
using BiolifeOrganic.Dll.ReadModels.Order;
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

    public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .ThenInclude(i => i.Category)
            .Include(o => o.ShippingContact)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

   






    public async Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate)
    {
        return await _dbContext.Orders.AnyAsync(predicate);
    }

    public async Task<bool> HasOrdersAsync(string userId)
       => await _dbContext.Orders
           .AnyAsync(o => o.AppUserId == userId && !o.IsDeleted);

    public async Task<int> CountAsync(string userId)
    {
        var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
        return await _dbContext.Orders
                     .Include(o => o.AppUser)
                     .Where(o => o.AppUserId == userId && !o.IsDeleted && o.CreatedAt >= oneMonthAgo)
                     .CountAsync();
    }

    public async Task<List<OrderListRM>> GetOrdersForUserAsync(string userId)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Where(o => o.AppUserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderListRM
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                OrderDate = o.CreatedAt,
                Status = o.Status.ToString(),

                SubtotalAmount = o.SubTotalAmount,
                DiscountAmount = o.DiscountAmount,
                TotalAmount = o.TotalAmount,

                DiscountPercentage = o.DiscountPercentage,
                DiscountCode = o.DiscountCode,

                ItemCount = o.OrderItems.Sum(i => i.Quantity)
            })
            .ToListAsync();
    }

    public async Task<OrderDetailsRM?> GetOrderDetailsAsync(int orderId)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Where(o => o.Id == orderId)
            .Select(o => new OrderDetailsRM
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Status = o.Status.ToString(),
                OrderDate = o.CreatedAt,

                SubtotalAmount = o.SubTotalAmount,
                TotalAmount = o.TotalAmount,
                DiscountAmount = o.DiscountAmount,
                DiscountPercentage = o.DiscountPercentage,
                DiscountCode = o.DiscountCode,

                CourierService = o.CourierService,
                TrackingNumber = o.TrackingNumber,
                Warehouse = o.Warehouse,
                EstimatedDeliveryDate = o.EstimatedDeliveryDate,
                ShippedDate = o.ShippedDate,

                ShippingContactId = o.ShippingContact.Id,
                ShippingFirstName = o.ShippingContact.FirstName,
                ShippingLastName = o.ShippingContact.LastName,
                ShippingAddress = o.ShippingContact.Address,
                ShippingCity = o.ShippingContact.City,
                ShippingCountry = o.ShippingContact.Country,
                ShippingPostalCode = o.ShippingContact.PostalCode,
                ShippingPhone = o.ShippingContact.PhoneNumber,
                ShippingEmail = o.ShippingContact.Email,

                OrderItems = o.OrderItems.Select(i => new OrderItemRM
                {
                    Id = i.Id,
                    ProductName = i.Product.Name,
                    ImageUrl = i.Product.ImageUrl,
                    Color = i.Color,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    SubTotal = i.Price * i.Quantity
                    
                }).ToList(),

                Categories = o.OrderItems
                    .Select(i => i.Product.Category.Name)
                    .Distinct()
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ShippingContactRM?> GetShippingContactForEditAsync(int orderId, string userId)
    {
        return await _dbContext.Orders
            .Where(o => o.Id == orderId
                        && o.AppUserId == userId
                        && o.Status == OrderStatus.OnHold)
            .Select(o => new ShippingContactRM
            {
                OrderId = o.Id,
                ContactId = o.ShippingContact.Id,
                FirstName = o.ShippingContact.FirstName,
                LastName = o.ShippingContact.LastName,
                Company = o.ShippingContact.Company,
                Address = o.ShippingContact.Address,
                City = o.ShippingContact.City,
                Country = o.ShippingContact.Country,
                PostalCode = o.ShippingContact.PostalCode,
                PhoneNumber = o.ShippingContact.PhoneNumber,
                Email = o.ShippingContact.Email,
                IsDefault = o.ShippingContact.IsDefault
            })
            .FirstOrDefaultAsync();
    }

    public async Task UpdateShippingContactAsync(UpdateShippingContactRM model, string userId)
    {
        var order = await _dbContext.Orders
            .Where(o => o.Id == model.OrderId
                        && o.AppUserId == userId
                        && o.Status == OrderStatus.OnHold)
            .Include(o => o.ShippingContact)
            .FirstOrDefaultAsync();

        if (order == null)
            throw new Exception("Order not found or cannot be edited");

        var contact = order.ShippingContact;

        contact.FirstName = model.FirstName;
        contact.LastName = model.LastName;
        contact.Company = model.Company;
        contact.Address = model.Address;
        contact.City = model.City;
        contact.Country = model.Country;
        contact.PostalCode = model.PostalCode;
        contact.PhoneNumber = model.PhoneNumber;
        contact.Email = model.Email;
        contact.IsDefault = model.IsDefault;

        await _dbContext.SaveChangesAsync();
    }





}
