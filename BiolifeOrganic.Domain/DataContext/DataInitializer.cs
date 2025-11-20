using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Dll.DataContext;

public class DataInitializer
{
    private readonly AppDbContext _dbContext;

    public DataInitializer(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.MigrateAsync();
    }
}
