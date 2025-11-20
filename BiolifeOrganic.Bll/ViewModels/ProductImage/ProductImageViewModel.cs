using Microsoft.AspNetCore.Http;

namespace BiolifeOrganic.Bll.ViewModels.ProductImage;

public class ProductImageViewModel
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }

    public int ProductId { get; set; }
    public string? ProductName { get; set; }
}

public class CreateProductImageViewModel
{
    public string? ImageUrl { get; set; }
    public IFormFile? ImageFile { get; set; }
    public int ProductId { get; set; }
}

public class UpdateProductImageViewModel
{
    public int Id { get; set; }
    public string? ExistingImageUrl { get; set; }
    public IFormFile? ImageFile { get; set; }
    public int ProductId { get; set; }
}



