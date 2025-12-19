namespace BiolifeOrganic.Dll.DataContext.Entities;

public class UserDiscount:TimeStample
{

    public bool IsUsed { get; set; }
    public DateTime? UsedAt { get; set; }
    public string AppUserId { get; set; } = null!;
    public AppUser? AppUser { get; set; }
    public int DiscountId { get; set; }

    public Discount? Discount { get; set; }
}
