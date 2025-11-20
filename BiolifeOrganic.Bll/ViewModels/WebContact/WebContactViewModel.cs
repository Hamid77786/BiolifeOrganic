using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.ViewModels.WebContact;

public class WebContactViewModel
{
    public int Id { get; set; }
    public string? EmailAgree { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Schedule { get; set; }
    public ContactOption ContactOption { get; set; }
}
public class CreateWebContactViewModel
{
    public string? EmailAgree { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Schedule { get; set; }
    public ContactOption ContactOption { get; set; }
}
public class UpdateWebContactViewModel
{
    public int Id { get; set; }
    public string? EmailAgree { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Schedule { get; set; }
    public ContactOption ContactOption { get; set; }
}

