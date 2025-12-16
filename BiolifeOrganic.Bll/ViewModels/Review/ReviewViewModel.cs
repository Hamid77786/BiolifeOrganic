using Microsoft.AspNetCore.Http;

namespace BiolifeOrganic.Bll.ViewModels.Review;

public class ReviewViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public string Note { get; set; } = null!;
    public int Stars { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public DateTime? PostedDate { get; set; }
    public string? AppUserId { get; set; }
    public string? AppUserName { get; set; }
    public string? AppUserPhoto {  get; set; }
   

    

}

public class CreateReviewViewModel
{
    
}

public class UpdateReviewViewModel
{
    
}

