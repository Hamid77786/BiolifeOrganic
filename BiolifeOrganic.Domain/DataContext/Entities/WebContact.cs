namespace BiolifeOrganic.Dll.DataContext.Entities;

public class WebContact:Entity
{
    public string? EmailAgree { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Schedule { get; set; }
    public bool IsDefault { get; set; }
    public ContactOption ContactOption { get; set; }
}

public enum ContactOption
{
    Web,
    Transport

}
