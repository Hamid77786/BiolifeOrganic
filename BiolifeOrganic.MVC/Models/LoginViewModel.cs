using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.MVC.Models;

public class LoginViewModel
{
   
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }
  
}
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

