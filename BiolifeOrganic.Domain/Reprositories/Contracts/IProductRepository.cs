using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Dll.Reprositories.Contracts;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByIdWithDetailsAsync(int id);
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId, int excludeProductId);

}


