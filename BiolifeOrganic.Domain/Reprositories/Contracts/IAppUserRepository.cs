using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Dll.Reprositories.Contracts;

public interface IAppUserRepository
{
    Task<List<AppUser>> GetAllUsersWithDetailsAsync();
    Task<AppUser?> GetUserWithDetailsAsync(string userId);
}
