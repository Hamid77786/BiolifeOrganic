using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Slider;
using BiolifeOrganic.Bll.ViewModels.WebContact;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class WebContactManager : CrudManager<WebContact,WebContactViewModel,CreateWebContactViewModel,UpdateWebContactViewModel>,IWebContactService
{


    public WebContactManager(IWebContactRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public async Task<WebContactViewModel?> GetDefaultContactAsync(ContactOption option)
    {
        var entity = await Repository.GetAsync(c => c.ContactOption == option && c.IsDefault);
        return Mapper.Map<WebContactViewModel>(entity);
    }

    public async Task<bool> SetDefaultContactAsync(int id, ContactOption option)
    {
        var contacts = await Repository.GetAllAsync(c => c.ContactOption == option);

        if (!contacts.Any(c => c.Id == id))
            return false;

        foreach (var c in contacts)
        {
            c.IsDefault = c.Id == id;
            await Repository.UpdateAsync(c);
        }
        return true;
    }

    public override async Task CreateAsync(CreateWebContactViewModel model)
    {
        if (model == null) return;

        var entity = Mapper.Map<WebContact>(model);

        if (entity.IsDefault)
        {
            var others = await Repository.GetAllAsync(c => c.ContactOption == entity.ContactOption);
            foreach (var c in others)
            {
                c.IsDefault = false;
                await Repository.UpdateAsync(c);
            }
        }

        await Repository.CreateAsync(entity);
    }

    public async Task<List<WebContactViewModel>> GetByOptionAsync(ContactOption option)
    {
        var webcontacts = await Repository.GetAllAsync(c => c.ContactOption == option);
        
        return Mapper.Map<List<WebContactViewModel>>(webcontacts);
    }
}



  


