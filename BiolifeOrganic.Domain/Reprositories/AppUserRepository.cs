using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
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

    public async Task<List<AppUser>> GetAllUsersWithDetailsAsync()
    {
        return await _context.Users
            .Include(u => u.Orders).ThenInclude(o => o.OrderItems)
            .Include(u => u.Wishlists).ThenInclude(w => w.Product)
            .Include(u => u.UserDiscounts).ThenInclude(ud => ud.Discount)
            .Include(u => u.Contacts)
            .Include(u => u.Reviews)
            .ToListAsync();
    }

    public async Task<AppUser?> GetUserWithDetailsAsync(string userId)
    {
        return await _context.Users
            .Include(u => u.Orders).ThenInclude(o => o.OrderItems)
            .Include(u => u.Wishlists).ThenInclude(w => w.Product)
            .Include(u => u.UserDiscounts).ThenInclude(ud => ud.Discount)
            .Include(u => u.Contacts)
            .Include(u => u.Reviews)
            .FirstOrDefaultAsync(u => u.Id == userId); 
    }

}
