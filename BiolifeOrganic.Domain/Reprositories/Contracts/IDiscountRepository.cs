using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Dll.Reprositories.Contracts;

public interface IDiscountRepository : IRepository<Discount>
{
    Task<Discount?> GetValidByCodeAsync(string code, DateTime now);
    Task IncrementUsedCountAsync(int discountId);
}
