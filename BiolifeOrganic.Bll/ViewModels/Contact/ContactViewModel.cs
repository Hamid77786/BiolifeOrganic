namespace BiolifeOrganic.Bll.ViewModels.Contact;

public class ContactViewModel
{
    public int Id { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? AppUserId { get; set; }
    public string? AppUserUserName { get; set; }

}
    
public class CreateContactViewModel
{
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? PostalCode { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public string? AppUserId { get; set; }

    
}
public class UpdateContactViewModel
{

    public int Id { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? AppUserId { get; set; }

}
    

