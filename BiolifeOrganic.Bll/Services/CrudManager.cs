using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BiolifeOrganic.Bll.Services;

public class CrudManager<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel> : ICrudService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel>
where TEntity : Entity
{
    protected readonly IRepository<TEntity> Repository;
    protected readonly IMapper Mapper;

    public CrudManager(IRepository<TEntity> respository, IMapper mapper)
    {
        Repository = respository;
        Mapper = mapper;
    }

    public virtual async Task CreateAsync(TCreateViewModel model)
    {
        var entity = Mapper.Map<TEntity>(model);
        await Repository.CreateAsync(entity);
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await Repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        await Repository.DeleteAsync(entity);

        return true;
    }

    public virtual async Task<IEnumerable<TViewModel>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool AsNoTracking = false)
    {
        var entities = await Repository.GetAllAsync(predicate, include, orderBy, AsNoTracking);

        var viewModels = Mapper.Map<IEnumerable<TViewModel>>(entities);

        return viewModels;
    }

    public virtual async Task<TViewModel?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool AsNoTracking = false)
    {
        var entity = await Repository.GetAsync(predicate, include, AsNoTracking);

        var viewModel = Mapper.Map<TViewModel>(entity);

        return viewModel;
    }

    public virtual async Task<TViewModel?> GetByIdAsync(int id)
    {
        var entity = await Repository.GetByIdAsync(id);

        if (entity == null)
            return default;

        var viewModel = Mapper.Map<TViewModel>(entity);

        return viewModel;
    }

    public virtual async Task<bool> UpdateAsync(int id, TUpdateViewModel model)
    {
        var entity = await Repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        Mapper.Map(model, entity);

        await Repository.UpdateAsync(entity);

        return true;
    }
}