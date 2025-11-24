namespace BiolifeOrganic.Dll.DataContext.Entities;

public class ProductImage: TimeStample
{
    public string? ImageUrl { get; set; }
    public bool IsMain { get; set; }
    public bool IsSecondary { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
