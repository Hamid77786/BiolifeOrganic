namespace BiolifeOrganic.Bll.ViewModels.OrderItem;

public class OrderItemViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Subtotal { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }

}

public class CreateOrderItemViewModel
{
   

}

public class UpdateOrderItemViewModel
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; }
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }

}


