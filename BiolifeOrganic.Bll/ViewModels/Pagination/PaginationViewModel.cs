namespace BiolifeOrganic.Bll.ViewModels.Pagination;

public class PaginationViewModel
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}
