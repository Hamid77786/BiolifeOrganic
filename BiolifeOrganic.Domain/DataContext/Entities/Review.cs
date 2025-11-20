namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Review:TimeStample
{
    public string Name { get; set; } = null!;
    public string EmailAdress { get; set; } = null!;
    public string Note { get; set; } = null!;
    public int Stars { get; set; }
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

}
