using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.Bll.ViewModels.Contact;

public class ContactViewModel
{
    public int Id { get; set; }
    public string? FullName { get; set; }

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
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = string.Empty;

    public string? Company { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postal code is required")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    public bool IsDefault { get; set; }
}

public class UpdateContactViewModel
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public string? Company { get; set; }

    [Required]
    public string Address { get; set; } = string.Empty;


    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = string.Empty;

    [Required]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    public bool IsDefault { get; set; }
}


