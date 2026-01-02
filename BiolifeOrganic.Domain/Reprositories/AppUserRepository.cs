using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.ReadModels.Order;
using BiolifeOrganic.Dll.ReadModels.User;
using BiolifeOrganic.Dll.ReadModels.UserDiscount;
using BiolifeOrganic.Dll.ReadModels.Wislist;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Dll.Reprositories;

public class AppUserRepository:IAppUserRepository
{
    private readonly AppDbContext _context;
    public AppUserRepository(AppDbContext context)
    {
        _context = context;
    }

  

    

    public async Task<List<UserRM>> GetAllUsersForAdminAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .Select(u => new UserRM
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                ProfileImagePath = u.ProfileImagePath,

                OrdersCount = u.Orders.Count,
                WishlistCount = u.Wishlists.Count,
                UserDiscountsCount = u.UserDiscounts.Count,

                IsBlocked = u.LockoutEnd != null &&
                            u.LockoutEnd > DateTimeOffset.UtcNow,

                IsAdmin = _context.UserRoles
                    .Any(ur => ur.UserId == u.Id &&
                               _context.Roles.Any(r =>
                                   r.Id == ur.RoleId && r.Name == "Admin"))
            })
            .ToListAsync();
    }

    public async Task<UserDetailsRM?> GetUserDetailsAsync(string userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserDetailsRM
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                IsBlocked = u.LockoutEnd != null && u.LockoutEnd > DateTimeOffset.UtcNow,
                IsAdmin = _context.UserRoles
                    .Any(ur => ur.UserId == u.Id &&
                               _context.Roles.Any(r => r.Id == ur.RoleId && r.Name == "Admin")),

                Orders = u.Orders.Select(o => new OrderDetailsRM
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



                    OrderItems = o.OrderItems.Select(i => new OrderItemRM
                    {
                        Id = i.Id,
                        ProductName = i.Product.Name,
                        ImageUrl = i.Product.ImageUrl,
                        Color = i.Color,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList(),

                    Categories = o.OrderItems.Select(i => i.Product.Category.Name).Distinct().ToList()
                }).ToList(),

                Wishlists = new List<WishlistRM>
                {
                    new WishlistRM
                    {
                        AppUserId = u.Id,
                        AppUserName = u.UserName,
                        Count = u.Wishlists.Count,
                        Items = u.Wishlists
                            .Where(w => w.Product != null)
                            .Select(w => new WishlistItemRM
                            {
                                ProductId = w.ProductId,
                                ProductName = w.Product.Name,
                                ImageUrl = w.Product.ImageUrl,
                                Price = w.Product.OriginalPrice,
                                IsAvailable = w.Product.IsAvailable
                            })
                            .ToList()
                    }
                },

                Discounts = u.UserDiscounts.Select(ud => new UserDiscountRM
                {
                    DiscountName = ud.Discount.Code,
                    DiscountPercentage = ud.Discount.Percentage,
                    IsUsed = ud.IsUsed,
                    UsedAt = ud.UsedAt
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<AppUser>> GetDeletedUsersAsync()
    {
        return await _context.Users
            .IgnoreQueryFilters()     
            .Where(u => u.IsDeleted)
            .ToListAsync();
    }


}
