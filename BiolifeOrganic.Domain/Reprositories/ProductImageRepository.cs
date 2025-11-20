using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class ProductImageRepository : EfCoreRepository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
