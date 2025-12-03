using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Http;

namespace BiolifeOrganic.Bll.ViewModels.Comment;

public  class CommentViewModel
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? UserImage { get; set; }
    public string? CommentImage { get; set; }
    public string? AppUserId { get; set; }
    public string? AppUserName { get; set; }
}
public class CreateCommentViewModel
{
    public string? Description { get; set; }
    public IFormFile? CommentImage { get; set; }
    public string? AppUserId { get; set; }

}

public class UpdateCommentViewModel
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string? ExistingCommentImage { get; set; }
    public IFormFile? CommentImage { get; set; }
}

