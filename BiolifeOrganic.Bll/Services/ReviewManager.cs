using AutoMapper;
using BiolifeOrganic.Bll.Constants;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Review;
using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Bll.Services;

public class ReviewManager : CrudManager<Review, ReviewViewModel, CreateReviewViewModel, UpdateReviewViewModel>, IReviewService
{
    private readonly AppDbContext _dbContext;
    private readonly FileService _fileService;
    public ReviewManager(FileService fileService,AppDbContext dbContext ,IReviewRepository respository, IMapper mapper) : base(respository, mapper)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    
    public async Task AddReview(ReviewViewModel viewModel)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == viewModel.ProductId);
       
        if (product == null)
            throw new Exception("Product not found");

        string? photoUrl = null;

        if (viewModel.Photo != null)
        {
            string fileName = await _fileService.SaveFileAsync(viewModel.Photo, FilePathConstants.ReviewImagePath);
            photoUrl = _fileService.GetFileUrl(FilePathConstants.ReviewImagePath, fileName);
        }

        var review = new Review
        {
            ProductId = viewModel.ProductId,
            Name = viewModel.Name!,
            EmailAdress = viewModel.EmailAddress!,
            Note = viewModel.Note,
            Stars = viewModel.Stars,
            AppUserId = viewModel.AppUserId,
            PostedDate = DateTime.Now,
            PhotoPath = photoUrl,
        };
        await _dbContext.Reviews.AddAsync(review);

        if (review.Stars > 0 && review.Stars <= 5)
            product.IsRated = true;

        await _dbContext.SaveChangesAsync();

    }
    public async Task<List<ReviewViewModel>> GetByProductIdAsync(int productId)
    {
        return await _dbContext.Reviews
            .Where(r => r.ProductId == productId)
            .OrderByDescending(r => r.PostedDate)
            .Include(r => r.Product)
             .Select(r => new ReviewViewModel
             {
                 Id = r.Id,
                 Name = r.Name,
                 EmailAddress = r.EmailAdress,
                 Note = r.Note,
                 Stars = r.Stars,
                 PostedDate = r.PostedDate,
                 ProductId = r.ProductId,
                 AppUserId = r.AppUserId,
                
             })
            .ToListAsync();
    }
}
