using Microsoft.AspNetCore.Http;

namespace BiolifeOrganic.Bll.ViewModels.Logo;

public class LogoViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LogoUrl { get; set; }
}
public class CreateLogoViewModel
{
    public string? Name { get; set; }
    public string? LogoUrl { get; set; }
    public IFormFile? LogoFile { get; set; }
}
public class UpdateLogoViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ExistingLogoUrl { get; set; }
    public IFormFile? NewLogoFile { get; set; }
}
