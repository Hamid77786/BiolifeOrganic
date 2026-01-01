using BiolifeOrganic.Bll.ViewModels.User;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IUserService
{
    Task<bool> IsBlockedAsync(string userId);
    Task<bool> IsAdminAsync(string userId);
    Task<List<UserViewModel>> GetAllUsersWithDetailsAsync();
    Task ToggleBlockAsync(string userId);
    Task ToggleAdminRoleAsync(string userId);
    Task<UserDetailsViewModel?> GetUserDetailsAsync(string userId);




}
