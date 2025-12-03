using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Comment;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class CommentManager : CrudManager<Comment, CommentViewModel, CreateCommentViewModel, UpdateCommentViewModel>, ICommentService
{
    public CommentManager(ICommentRepository respository, IMapper mapper) : base(respository, mapper)
    {
    }
}
