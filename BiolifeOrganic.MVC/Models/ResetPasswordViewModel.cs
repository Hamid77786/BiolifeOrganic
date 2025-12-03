using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.MVC.Models;

public class ResetPasswordViewModel
{

    [DataType(DataType.Password)]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
    public string Password { get; set; } = string.Empty;


    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public required string ResetPasswordToken { get; set; }
}
