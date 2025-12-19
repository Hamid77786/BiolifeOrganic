namespace BiolifeOrganic.Bll.ViewModels.Discount;

public class DiscountValidationResult
{
    public bool IsValid { get; set; }
    public int Percentage { get; set; }
    public string? Error { get; set; }
    public int DiscountId { get; set; }
}
