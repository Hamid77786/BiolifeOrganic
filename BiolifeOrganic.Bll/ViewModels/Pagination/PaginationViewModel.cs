namespace BiolifeOrganic.Bll.ViewModels.Pagination;

public class PaginationViewModel
{
    public int CurrentPage { get; set; }
    public int TotalPagesReviews { get; set; }
    public int TotalPagesProducts { get; set; }

    public bool HasPreviousReviews => CurrentPage > 1;
    public bool HasNextReviews => CurrentPage < TotalPagesReviews;
    public bool HasPreviousProducts => CurrentPage > 1;
    public bool HasNextProducts => CurrentPage < TotalPagesProducts;
}
