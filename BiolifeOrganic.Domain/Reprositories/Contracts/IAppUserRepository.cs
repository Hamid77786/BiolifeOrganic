using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.ReadModels.User;

namespace BiolifeOrganic.Dll.Reprositories.Contracts;

public interface IAppUserRepository
{
   
    Task<List<UserRM>> GetAllUsersForAdminAsync();
    Task<UserDetailsRM?> GetUserDetailsAsync(string userId);
    Task<List<AppUser>> GetDeletedUsersAsync();

}
