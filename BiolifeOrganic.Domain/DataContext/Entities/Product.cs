namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Product:TimeStample
{
    public string Name { get; set; } = null!;
    public decimal OriginalPrice { get; set; }
    public string? Description { get; set; }
    public string? AdditionalInformation { get; set; }
    public string? ImageUrl { get; set; }
    public int QuantityAvailable { get; set; }
    public bool IsBestSeller {  get; set; }
    public bool IsAvailable { get; set; }
    public bool IsRated { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsOnSale { get; set; }  
    public decimal? DiscountPercent { get; set; } 
    public DateTime? SaleStartDate { get; set; }
    public DateTime? SaleEndDate { get; set; }
    public decimal DiscountedPrice =>
        IsOnSale && DiscountPercent.HasValue
    ? OriginalPrice - (OriginalPrice * DiscountPercent.Value / 100)
        : OriginalPrice;

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public List<ProductImage> ProductImages { get; set; } = [];
    public List<OrderItem> OrderItems { get; set; } = [];

}




    
    
    












