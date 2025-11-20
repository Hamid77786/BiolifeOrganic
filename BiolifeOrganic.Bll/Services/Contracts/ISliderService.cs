using BiolifeOrganic.Bll.ViewModels.Slider;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface ISliderService : ICrudService<Slider, SliderViewModel, CreateSliderViewModel, UpdateSliderViewModel>
{
}

