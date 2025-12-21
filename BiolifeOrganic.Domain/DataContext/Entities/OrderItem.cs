using System.ComponentModel.DataAnnotations.Schema;

namespace BiolifeOrganic.Dll.DataContext.Entities;

public class OrderItem : TimeStample
{
    public string ProductName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Subtotal { get; set; }
    
    
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
