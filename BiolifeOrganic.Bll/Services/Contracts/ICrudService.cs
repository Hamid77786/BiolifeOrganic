using BiolifeOrganic.Dll.DataContext.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;


namespace BiolifeOrganic.Bll.Services.Contracts;

public interface ICrudService<TEntity,TViewModel,TCreateViewModel,TUpdateViewModel>
    where TEntity : Entity
{
    Task<IEnumerable<TViewModel>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           bool AsNoTracking = false);

    Task<TViewModel?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool AsNoTracking = false);

    IQueryable<TEntity> GetQuery(
    Expression<Func<TEntity, bool>>? predicate = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
    bool AsNoTracking = false);

    Task<TViewModel?> GetByIdAsync(int id);
    Task CreateAsync(TCreateViewModel model);
    Task<bool> UpdateAsync(int id, TUpdateViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

}
