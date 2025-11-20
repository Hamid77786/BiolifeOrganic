using BiolifeOrganic.Bll.ViewModels.Product;
using Microsoft.AspNetCore.Http;

namespace BiolifeOrganic.Bll.ViewModels.Category;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? CategoryIcon { get; set; }
    public List<ProductViewModel> Products { get; set; } = [];
    
}

public class CreateCategoryViewModel
{
    public string Name { get; set; } = null!;
    public IFormFile? ImageFile { get; set; }
    public string? CategoryIcon { get; set; }

}



public class UpdateCategoryViewModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }  

    public IFormFile? NewImageFile { get; set; }

    public string? CategoryIcon { get; set; }
}

