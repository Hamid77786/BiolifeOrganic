namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Comment: TimeStample
{
    public string Description { get; set; } = null!;
    public string? UserImage { get; set; }
    public string? CommentImage { get; set; }
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
