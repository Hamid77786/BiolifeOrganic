namespace BiolifeOrganic.Bll.ViewModels.UserDiscount;

public class UserDiscountViewModel
{
    public int DiscountId { get; set; }
    public string DiscountName { get; set; } = string.Empty;
    public decimal DiscountPercentage { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? UsedAt { get; set; }
}

public class CreateUserDiscountViewModel
{
}

public class UpdateUserDiscountViewModel
{
}

