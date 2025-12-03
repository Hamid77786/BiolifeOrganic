using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.MVC.Models;

public class RegisterViewModel
{
    public required string UserName { get; set; }

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
   


}
