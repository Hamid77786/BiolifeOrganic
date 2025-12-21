namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Discount : TimeStample
{

    public string Code { get; set; } = null!;
    public decimal Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool OnlyForNewUsers { get; set; }
    public bool OnlyForExistingUsers { get; set; }
    public bool IsActive { get; set; }

    public int? MaxUsageCount { get; set; }
    public int? UsedCount { get; set; }
    public List<UserDiscount> UserDiscounts { get; set; } = [];

}





