using BiolifeOrganic.Bll.ViewModels.Review;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IReviewService : ICrudService<Review, ReviewViewModel, CreateReviewViewModel, UpdateReviewViewModel>
{
    Task AddReview(ReviewViewModel viewModel);
    Task<List<ReviewViewModel>> GetByProductIdAsync(int  productId);
}

