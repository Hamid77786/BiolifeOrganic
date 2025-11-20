using Microsoft.AspNetCore.Http;

namespace BiolifeOrganic.Bll.ViewModels.Product;

public class ProductViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }

    public string? Description { get; set; }
    public string? AdditionalInformation { get; set; }
    public string? ImageUrl { get; set; }

    public int QuantityAvailable { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsRated { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsOnSale { get; set; }

    public decimal? DiscountPercent { get; set; }
    public DateTime? SaleStartDate { get; set; }
    public DateTime? SaleEndDate { get; set; }

    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public List<string> ProductImageUrls { get; set; } = [];
}

public class CreateProductViewModel
{
    public string Name { get; set; } = null!;
    public decimal OriginalPrice { get; set; }

    public string? Description { get; set; }
    public string? AdditionalInformation { get; set; }

    public IFormFile? ImageFile { get; set; }
    public List<IFormFile> ProductImages { get; set; } = [];

    public int QuantityAvailable { get; set; }

    public bool IsBestSeller { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsRated { get; set; }
    public bool IsOnSale { get; set; }

    public decimal? DiscountPercent { get; set; }
    public DateTime? SaleStartDate { get; set; }
    public DateTime? SaleEndDate { get; set; }

    public int CategoryId { get; set; }
}

public class UpdateProductViewModel
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public decimal OriginalPrice { get; set; }

    public string? Description { get; set; }
    public string? AdditionalInformation { get; set; }

    public string? ExistingImageUrl { get; set; }
    public IFormFile? NewImageFile { get; set; }

    public List<string>? ExistingProductImages { get; set; }
    public List<IFormFile>? NewProductImages { get; set; }

    public int QuantityAvailable { get; set; }

    public bool IsBestSeller { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsRated { get; set; }
    public bool IsOnSale { get; set; }
    public bool IsDeleted { get; set; }

    public decimal? DiscountPercent { get; set; }
    public DateTime? SaleStartDate { get; set; }
    public DateTime? SaleEndDate { get; set; }

    public int? CategoryId { get; set; }
}

