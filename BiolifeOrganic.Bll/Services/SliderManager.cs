using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Slider;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class SliderManager : CrudManager<Slider, SliderViewModel, CreateSliderViewModel, UpdateSliderViewModel>, ISliderService
{
    public SliderManager(ISliderRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
