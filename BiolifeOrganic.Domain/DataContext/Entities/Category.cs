using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Category:TimeStample
{
    public string Name { get; set; } = null!;
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? ImageUrl { get; set; }
    public string? CategoryIcon { get; set; }
    public bool IsRelated { get; set; }
    public List<Product> Products { get; set; } = [];
}
