using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.MVC.Models;

public class ForgotPasswordViewModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email required")]
    public string Email { get; set; } = string.Empty;

    public string? ResetPasswordToken { get; set; }
}
