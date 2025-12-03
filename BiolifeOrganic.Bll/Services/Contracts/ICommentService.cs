using BiolifeOrganic.Bll.ViewModels.Comment;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface ICommentService : ICrudService<Comment, CommentViewModel, CreateCommentViewModel, UpdateCommentViewModel>
{
}

