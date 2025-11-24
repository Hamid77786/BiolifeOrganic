using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Review;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class ReviewManager : CrudManager<Review, ReviewViewModel, CreateReviewViewModel, UpdateReviewViewModel>, IReviewService
{
    public ReviewManager(IReviewRepository respository, IMapper mapper) : base(respository, mapper)
    {
    }
}
