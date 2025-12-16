using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.Bll.ViewModels.Contact;

public class ContactViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "City is required")]
    public string? City { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public string? Country { get; set; }

    [Required(ErrorMessage = "Postal code is required")]
    public string? PostalCode { get; set; }
    
    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string? Phone { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }
    public string Message { get; set; } = null!;

    public string? AppUserId { get; set; }
    public string? AppUserUserName { get; set; }
    public bool IsDefault { get; set; }

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
    

