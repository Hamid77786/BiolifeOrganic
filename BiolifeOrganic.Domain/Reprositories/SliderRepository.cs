using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class SliderRepository : EfCoreRepository<Slider>, ISliderRepository
{
    public SliderRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
