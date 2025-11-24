using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Dll.Reprositories;

public class ProductRepository : EfCoreRepository<Product>, IProductRepository
{
    private readonly AppDbContext _dbContext;
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetProductDetailsAsync(int id)
    {
        return await _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);

    }

    



}
