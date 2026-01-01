namespace BiolifeOrganic.Dll.ReadModels.UserDiscount;

public class UserDiscountRM
{
    public string DiscountName { get; set; } = string.Empty;
    public decimal DiscountPercentage { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? UsedAt { get; set; }
}
