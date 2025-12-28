using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Dll.Reprositories.Contracts;

public interface IUserDiscountRepository : IRepository<UserDiscount>
{
    Task<UserDiscount?> GetAsync(string userId, int discountId);
    Task<bool> ExistsAsync(string userId, int discountId);
    Task AddAsync(UserDiscount userDiscount);
    Task MarkAsUsedAsync(string userId, int discountId);
}
