namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Category:TimeStample
{
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? CategoryIcon { get; set; }
    public List<Product> Products { get; set; } = [];
}
