namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Promocode:TimeStample
{
   
    public string Code { get; set; } = null!;
    public int Discount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsUsed { get; set; } = false;
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}

    

