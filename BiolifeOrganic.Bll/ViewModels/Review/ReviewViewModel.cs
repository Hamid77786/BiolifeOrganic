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
    
    public bool ProductIsRated { get; set; }
    public DateTime? PostedDate { get; set; }

    public string? AppUserId { get; set; }
    public string? AppUserName { get; set; }
}

public class CreateReviewViewModel
{
    public string Name { get; set; } = null!;
    public string EmailAdress { get; set; } = null!;
    public string Note { get; set; } = null!;
    public int Stars { get; set; }

    public string? AppUserId { get; set; }
}

public class UpdateReviewViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? EmailAdress { get; set; }
    public string? Note { get; set; }
    public int Stars { get; set; }

    public string? AppUserId { get; set; }
}

